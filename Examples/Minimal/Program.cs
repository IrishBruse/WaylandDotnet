namespace Minimal;

using System.Threading;
using WaylandDotnet;
using WaylandDotnet.Stable;

public class Program
{
    public static void Main(string[] args)
    {
        WlDisplay display = WlDisplay.Connect();
        WlRegistry registry = display.GetRegistry();

        WlCompositor? compositor = null;
        XdgWmBase? xdg = null;
        WlShm? shm = null;

        registry.OnGlobal += (name, interfaceName, version) =>
        {
            switch (interfaceName)
            {
                case WlCompositor.InterfaceName:
                    compositor = registry.Bind<WlCompositor>(name, version);
                    break;
                case XdgWmBase.InterfaceName:
                    xdg = registry.Bind<XdgWmBase>(name, version);
                    break;
                case WlShm.InterfaceName:
                    shm = registry.Bind<WlShm>(name, version);
                    break;
            }
        };
        display.Roundtrip();

        if (compositor == null || xdg == null || shm == null)
        {
            throw new InvalidOperationException("Failed to bind required Wayland interfaces");
        }

        WlSurface surface = compositor.CreateSurface();
        XdgSurface xdgSurface = xdg.GetXdgSurface(surface);
        XdgToplevel topLevel = xdgSurface.GetToplevel();
        topLevel.SetTitle("Minimal Wayland Window");

        xdg.OnPing += xdg.Pong;
        xdgSurface.OnConfigure += xdgSurface.AckConfigure;

        surface.Commit();
        display.Roundtrip();

        int width = 800;
        int height = 600;

        WlBuffer buffer = shm.CreateCheckerboardColorBuffer(width, height, 0x6495EDFF, 0x5384DCFF)!;

        surface.Attach(buffer, 0, 0);
        surface.Damage(0, 0, width, height);
        surface.Commit();
        display.Flush();

        bool running = true;
        topLevel.OnClose += () => running = false;

        while (running)
        {
            display.Dispatch();
            Thread.Sleep(16);
        }
    }
}