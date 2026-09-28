namespace TinyRwm;

using System.Diagnostics;
using WaylandDotnet;
using WaylandDotnet.River;

public static class Program
{
    private const uint KeySpace = 0x0020;
    private const uint KeyQ = 0x0071;
    private const uint KeyN = 0x006e;
    private const uint KeyEscape = 0xff1b;
    private const uint BtnLeft = 0x110;
    private const uint BtnRight = 0x111;

    private enum Action
    {
        None,
        SpawnFoot,
        Close,
        FocusNext,
        Move,
        Resize,
        Exit,
    }

    private enum SeatOp
    {
        None,
        Move,
        Resize,
    }

    private sealed class Output
    {
        public required RiverOutputV1 Obj;
        public bool Removed;
    }

    private sealed class Window
    {
        public required RiverWindowV1 Obj;
        public required RiverNodeV1 Node;
        public bool IsNew = true;
        public bool Closed;
        public bool Configured;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public Seat? PointerMoveRequested;
        public Seat? PointerResizeRequested;
        public uint PointerResizeRequestedEdges;
    }

    private sealed class XkbBinding
    {
        public required RiverXkbBindingV1 Obj;
    }

    private sealed class PointerBinding
    {
        public required RiverPointerBindingV1 Obj;
    }

    private sealed class Seat
    {
        public required RiverSeatV1 Obj;
        public bool IsNew = true;
        public bool Removed;
        public Window? Focused;
        public Window? Hovered;
        public Window? Interacted;
        public List<XkbBinding> XkbBindings = [];
        public List<PointerBinding> PointerBindings = [];
        public Action PendingAction;
        public SeatOp Op;
        public Window? OpWindow;
        public int OpStartX;
        public int OpStartY;
        public int OpDx;
        public int OpDy;
        public bool OpRelease;
        public int OpStartWidth;
        public int OpStartHeight;
        public uint OpEdges;
    }

    // Index 0 is the bottom of the stack. The last entry is the top.
    private static readonly List<Output> Outputs = [];
    private static readonly List<Window> Windows = [];
    private static readonly List<Seat> Seats = [];

    private static RiverWindowManagerV1? windowManager;
    private static RiverXkbBindingsV1? xkbBindings;

    public static int Main()
    {
        WlDisplay display;
        try
        {
            display = WlDisplay.Connect();
        }
        catch (InvalidOperationException)
        {
            Console.Error.WriteLine("failed to connect to Wayland server");
            return 1;
        }

        Environment.SetEnvironmentVariable("WAYLAND_DEBUG", null);
        ChildProcess.IgnoreExit();

        WlRegistry registry = display.GetRegistry();
        registry.OnGlobal += (name, interfaceName, version) =>
        {
            if (interfaceName == RiverWindowManagerV1.InterfaceName)
            {
                if (version >= 4)
                {
                    windowManager = registry.Bind<RiverWindowManagerV1>(
                        name,
                        Math.Min(version, (uint)RiverWindowManagerV1.InterfaceVersion));
                }
            }
            else if (interfaceName == RiverXkbBindingsV1.InterfaceName)
            {
                xkbBindings = registry.Bind<RiverXkbBindingsV1>(
                    name,
                    Math.Min(version, (uint)RiverXkbBindingsV1.InterfaceVersion));
            }
        };

        if (display.Roundtrip() < 0)
        {
            Console.Error.WriteLine("roundtrip failed");
            return 1;
        }

        if (windowManager == null || xkbBindings == null)
        {
            Console.Error.WriteLine(
                "river_window_manager_v1 or river_xkb_bindings_v1 not supported by the Wayland server");
            return 1;
        }

        Subscribe(windowManager);

        while (true)
        {
            if (display.Dispatch() < 0)
            {
                Console.Error.WriteLine("dispatch failed");
                return 1;
            }
        }
    }

    private static void Subscribe(RiverWindowManagerV1 manager)
    {
        manager.OnUnavailable += () =>
        {
            Console.Error.WriteLine("error: another window manager is already running");
            Environment.Exit(1);
        };
        manager.OnFinished += () => Environment.Exit(0);
        manager.OnManageStart += OnManageStart;
        manager.OnRenderStart += OnRenderStart;
        manager.OnWindow += OnWindow;
        manager.OnOutput += OnOutput;
        manager.OnSeat += OnSeat;
    }

    private static void OnManageStart()
    {
        for (var i = 0; i < Outputs.Count;)
        {
            if (!Outputs[i].Removed)
            {
                i++;
                continue;
            }

            Outputs[i].Obj.Destroy();
            Outputs.RemoveAt(i);
        }

        for (var i = 0; i < Windows.Count;)
        {
            if (!Windows[i].Closed)
            {
                i++;
                continue;
            }

            DestroyClosedWindow(Windows[i]);
        }

        for (var i = 0; i < Seats.Count;)
        {
            if (!Seats[i].Removed)
            {
                i++;
                continue;
            }

            DestroyRemovedSeat(Seats[i]);
        }

        foreach (var window in Windows.ToArray())
        {
            WindowManage(window);
        }

        foreach (var seat in Seats.ToArray())
        {
            SeatManage(seat);
        }

        windowManager!.ManageFinish();
    }

    private static void OnRenderStart()
    {
        foreach (var seat in Seats)
        {
            SeatRender(seat);
        }

        windowManager!.RenderFinish();
    }

    private static void OnWindow(RiverWindowV1 riverWindow)
    {
        var window = new Window
        {
            Obj = riverWindow,
            Node = riverWindow.GetNode(),
        };

        riverWindow.OnClosed += () => window.Closed = true;
        riverWindow.OnDimensions += (width, height) =>
        {
            var first = !window.Configured;
            window.Width = width;
            window.Height = height;
            window.Configured = true;
            // The first configure has been acked, so the client has a surface.
            // Ask for another manage sequence to give it keyboard focus.
            if (first)
            {
                windowManager!.ManageDirty();
            }
        };
        riverWindow.OnPointerMoveRequested += seat =>
        {
            window.PointerMoveRequested = FindSeat(seat.Handle);
        };
        riverWindow.OnPointerResizeRequested += (seat, edges) =>
        {
            window.PointerResizeRequested = FindSeat(seat.Handle);
            window.PointerResizeRequestedEdges = edges;
        };

        Windows.Add(window);
    }

    private static void OnOutput(RiverOutputV1 riverOutput)
    {
        var output = new Output { Obj = riverOutput };
        riverOutput.OnRemoved += () => output.Removed = true;
        Outputs.Add(output);
    }

    private static void OnSeat(RiverSeatV1 riverSeat)
    {
        var seat = new Seat { Obj = riverSeat };
        riverSeat.OnRemoved += () => seat.Removed = true;
        riverSeat.OnPointerEnter += window => seat.Hovered = FindWindow(window.Handle);
        riverSeat.OnPointerLeave += () => seat.Hovered = null;
        riverSeat.OnWindowInteraction += window => seat.Interacted = FindWindow(window.Handle);
        riverSeat.OnOpDelta += (dx, dy) =>
        {
            seat.OpDx = dx;
            seat.OpDy = dy;
        };
        riverSeat.OnOpRelease += () => seat.OpRelease = true;
        Seats.Add(seat);
    }

    private static void DestroyClosedWindow(Window window)
    {
        foreach (var seat in Seats)
        {
            if (ReferenceEquals(seat.Focused, window))
            {
                // River destroys the window on the following render, and rejects
                // that if a seat is still focused on it.
                seat.Obj.ClearFocus();
                seat.Focused = null;
            }

            if (ReferenceEquals(seat.OpWindow, window))
            {
                seat.Obj.OpEnd();
                seat.Op = SeatOp.None;
                seat.OpWindow = null;
            }

            if (ReferenceEquals(seat.Hovered, window))
            {
                seat.Hovered = null;
            }

            if (ReferenceEquals(seat.Interacted, window))
            {
                seat.Interacted = null;
            }
        }

        window.PointerMoveRequested = null;
        window.PointerResizeRequested = null;
        window.Obj.Destroy();
        Windows.Remove(window);
    }

    private static void DestroyRemovedSeat(Seat seat)
    {
        foreach (var window in Windows)
        {
            if (ReferenceEquals(window.PointerMoveRequested, seat))
            {
                window.PointerMoveRequested = null;
            }

            if (ReferenceEquals(window.PointerResizeRequested, seat))
            {
                window.PointerResizeRequested = null;
            }
        }

        foreach (var binding in seat.XkbBindings)
        {
            binding.Obj.Destroy();
        }

        seat.XkbBindings.Clear();

        foreach (var binding in seat.PointerBindings)
        {
            binding.Obj.Destroy();
        }

        seat.PointerBindings.Clear();
        seat.Obj.Destroy();
        Seats.Remove(seat);
    }

    private static void WindowSetPosition(Window window, int x, int y)
    {
        window.Node.SetPosition(x, y);
        window.X = x;
        window.Y = y;
    }

    private static void WindowManage(Window window)
    {
        if (window.IsNew)
        {
            window.IsNew = false;
            WindowSetPosition(window, 0, 0);
            window.Obj.ProposeDimensions(0, 0);
        }

        if (window.PointerMoveRequested != null)
        {
            SeatPointerMove(window.PointerMoveRequested, window);
            window.PointerMoveRequested = null;
        }

        if (window.PointerResizeRequested != null)
        {
            SeatPointerResize(
                window.PointerResizeRequested,
                window,
                window.PointerResizeRequestedEdges);
            window.PointerResizeRequested = null;
        }
    }

    private static void SeatFocus(Seat seat, Window? window)
    {
        // River sends wl_keyboard.enter before the xdg configure. Foot has no
        // grid until that configure is acked, so focusing a new window in the
        // same manage sequence crashes the client.
        if (window is not { Configured: true })
        {
            window = null;
            for (var i = Windows.Count - 1; i >= 0; i--)
            {
                if (Windows[i].Configured)
                {
                    window = Windows[i];
                    break;
                }
            }
        }

        if (ReferenceEquals(seat.Focused, window))
        {
            return;
        }

        if (window != null)
        {
            seat.Obj.FocusWindow(window.Obj);
            window.Node.PlaceTop();
            Windows.Remove(window);
            Windows.Add(window);
        }
        else
        {
            seat.Obj.ClearFocus();
        }

        seat.Focused = window;
    }

    private static void SeatPointerMove(Seat seat, Window window)
    {
        SeatFocus(seat, window);
        seat.Obj.OpStartPointer();
        seat.Op = SeatOp.Move;
        seat.OpWindow = window;
        seat.OpStartX = window.X;
        seat.OpStartY = window.Y;
        seat.OpDx = 0;
        seat.OpDy = 0;
    }

    private static void SeatPointerResize(Seat seat, Window window, uint edges)
    {
        SeatFocus(seat, window);
        window.Obj.InformResizeStart();
        seat.Obj.OpStartPointer();
        seat.Op = SeatOp.Resize;
        seat.OpWindow = window;
        seat.OpEdges = edges;
        seat.OpStartX = window.X;
        seat.OpStartY = window.Y;
        seat.OpStartWidth = window.Width;
        seat.OpStartHeight = window.Height;
        seat.OpDx = 0;
        seat.OpDy = 0;
    }

    private static void SeatAction(Seat seat, Action action)
    {
        switch (action)
        {
            case Action.None:
                break;
            case Action.SpawnFoot:
                SpawnFoot();
                break;
            case Action.Close:
                seat.Focused?.Obj.Close();
                break;
            case Action.FocusNext:
                if (Windows.Count > 0)
                {
                    SeatFocus(seat, Windows[0]);
                }

                break;
            case Action.Move:
                if (seat.Op == SeatOp.None && seat.Hovered != null)
                {
                    SeatPointerMove(seat, seat.Hovered);
                }

                break;
            case Action.Resize:
                if (seat.Op == SeatOp.None && seat.Hovered != null)
                {
                    SeatPointerResize(
                        seat,
                        seat.Hovered,
                        (uint)(RiverWindowV1.EdgesFlag.Bottom | RiverWindowV1.EdgesFlag.Right));
                }

                break;
            case Action.Exit:
                windowManager!.ExitSession();
                break;
        }
    }

    private static void SeatManage(Seat seat)
    {
        if (seat.IsNew)
        {
            seat.IsNew = false;
            var mods = (uint)RiverSeatV1.ModifiersFlag.Mod4;
            CreateXkbBinding(seat, mods, KeySpace, Action.SpawnFoot);
            CreateXkbBinding(seat, mods, KeyQ, Action.Close);
            CreateXkbBinding(seat, mods, KeyN, Action.FocusNext);
            CreateXkbBinding(seat, mods, KeyEscape, Action.Exit);
            CreatePointerBinding(seat, mods, BtnLeft, Action.Move);
            CreatePointerBinding(seat, mods, BtnRight, Action.Resize);
        }

        // No interaction in this manage sequence focuses the top window.
        // That is what gives a new window keyboard focus.
        SeatFocus(seat, seat.Interacted);
        seat.Interacted = null;

        SeatAction(seat, seat.PendingAction);
        seat.PendingAction = Action.None;

        switch (seat.Op)
        {
            case SeatOp.None:
                break;
            case SeatOp.Move:
                if (seat.OpRelease)
                {
                    seat.Obj.OpEnd();
                    seat.Op = SeatOp.None;
                    seat.OpWindow = null;
                }

                break;
            case SeatOp.Resize:
                if (seat.OpRelease)
                {
                    seat.OpWindow!.Obj.InformResizeEnd();
                    seat.Obj.OpEnd();
                    seat.Op = SeatOp.None;
                    seat.OpWindow = null;
                    break;
                }

                var width = seat.OpStartWidth;
                var height = seat.OpStartHeight;
                if ((seat.OpEdges & (uint)RiverWindowV1.EdgesFlag.Left) != 0)
                {
                    width -= seat.OpDx;
                }

                if ((seat.OpEdges & (uint)RiverWindowV1.EdgesFlag.Right) != 0)
                {
                    width += seat.OpDx;
                }

                if ((seat.OpEdges & (uint)RiverWindowV1.EdgesFlag.Top) != 0)
                {
                    height -= seat.OpDy;
                }

                if ((seat.OpEdges & (uint)RiverWindowV1.EdgesFlag.Bottom) != 0)
                {
                    height += seat.OpDy;
                }

                seat.OpWindow!.Obj.ProposeDimensions(width > 1 ? width : 1, height > 1 ? height : 1);
                break;
        }

        seat.OpRelease = false;
    }

    private static void SeatRender(Seat seat)
    {
        switch (seat.Op)
        {
            case SeatOp.None:
                break;
            case SeatOp.Move:
                WindowSetPosition(
                    seat.OpWindow!,
                    seat.OpStartX + seat.OpDx,
                    seat.OpStartY + seat.OpDy);
                break;
            case SeatOp.Resize:
                var x = seat.OpStartX;
                var y = seat.OpStartY;
                if ((seat.OpEdges & (uint)RiverWindowV1.EdgesFlag.Left) != 0)
                {
                    x += seat.OpStartWidth - seat.OpWindow!.Width;
                }

                if ((seat.OpEdges & (uint)RiverWindowV1.EdgesFlag.Top) != 0)
                {
                    y += seat.OpStartHeight - seat.OpWindow!.Height;
                }

                WindowSetPosition(seat.OpWindow!, x, y);
                break;
        }
    }

    private static void CreateXkbBinding(Seat seat, uint mods, uint keysym, Action action)
    {
        var binding = xkbBindings!.GetXkbBinding(seat.Obj, keysym, mods);
        binding.OnPressed += () => seat.PendingAction = action;
        binding.Enable();
        seat.XkbBindings.Add(new XkbBinding { Obj = binding });
    }

    private static void CreatePointerBinding(Seat seat, uint mods, uint button, Action action)
    {
        var binding = seat.Obj.GetPointerBinding(button, mods);
        binding.OnPressed += () => seat.PendingAction = action;
        binding.Enable();
        seat.PointerBindings.Add(new PointerBinding { Obj = binding });
    }

    private static void SpawnFoot()
    {
        try
        {
            using var process = Process.Start(new ProcessStartInfo("foot")
            {
                UseShellExecute = false,
            });
        }
        catch (Exception)
        {
        }
    }

    private static Window? FindWindow(IntPtr handle)
    {
        foreach (var window in Windows)
        {
            if (window.Obj.Handle == handle)
            {
                return window;
            }
        }

        return null;
    }

    private static Seat? FindSeat(IntPtr handle)
    {
        foreach (var seat in Seats)
        {
            if (seat.Obj.Handle == handle)
            {
                return seat;
            }
        }

        return null;
    }
}
