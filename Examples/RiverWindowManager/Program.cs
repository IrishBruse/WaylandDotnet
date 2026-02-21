namespace Example;

using System;
using System.Collections.Generic;
using WaylandDotnet;
using WaylandDotnet.River;
using WaylandDotnet.Stable;

public class Program
{
    private static RiverWindowManagerV1? manager;
    private static RiverOutputV1? output;
    private static int outWidth;
    private static int outHeight;
    private static readonly List<(RiverWindowV1 window, RiverNodeV1 node)> windows = new();

    public static void Main(string[] args)
    {
        WlDisplay display = WlDisplay.Connect();
        WlRegistry registry = display.GetRegistry();

        registry.OnGlobal += (name, interfaceName, version) =>
        {
            if (interfaceName == RiverWindowManagerV1.InterfaceName)
            {
                manager = registry.Bind<RiverWindowManagerV1>(name, Math.Min(version, 3));
                SetupManagerHandlers();
            }
        };

        display.Roundtrip();
        display.Roundtrip();

        if (output != null)
        {
            SetupOutputHandlers();
        }
        display.Roundtrip();

        while (display.Dispatch() != -1)
        {
        }
    }

    private static void SetupManagerHandlers()
    {
        manager.OnUnavailable += () =>
        {
            Console.Error.WriteLine("WM unavailable");
            Environment.Exit(1);
        };

        manager.OnManageStart += () =>
        {
            int w = outWidth / (windows.Count > 0 ? windows.Count : 1);
            foreach (var (window, _) in windows)
            {
                window.ProposeDimensions(w, outHeight);
            }
            manager.ManageFinish();
        };

        manager.OnRenderStart += () =>
        {
            int w = outWidth / (windows.Count > 0 ? windows.Count : 1);
            for (int i = 0; i < windows.Count; i++)
            {
                windows[i].node.SetPosition(i * w, 0);
                windows[i].node.PlaceTop();
            }
            manager.RenderFinish();
        };

        manager.OnWindow += (window) =>
        {
            var node = window.GetNode();
            SetupWindowHandlers(window, node);
            windows.Add((window, node));
        };

        manager.OnOutput += (o) =>
        {
            output = o;
            SetupOutputHandlers();
        };
    }

    private static void SetupOutputHandlers()
    {
        output!.OnDimensions += (w, h) =>
        {
            outWidth = w;
            outHeight = h;
        };
    }

    private static void SetupWindowHandlers(RiverWindowV1 window, RiverNodeV1 node)
    {
        window.OnClosed += () =>
        {
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].window == window)
                {
                    node.Destroy();
                    window.Destroy();
                    windows.RemoveAt(i);
                    return;
                }
            }
        };
    }
}
