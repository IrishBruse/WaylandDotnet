namespace Example;

using System.Threading;
using WaylandDotnet;
using WaylandDotnet.Internal;
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

        WlSurface surface = compositor!.CreateSurface();
        XdgSurface xdgSurface = xdg!.GetXdgSurface(surface);
        XdgToplevel topLevel = xdgSurface.GetToplevel();
        topLevel.SetTitle("Minimal");

        xdg!.OnPing += (serial) => xdg.Pong(serial);

        int width = 800;
        int height = 600;
        bool configured = false;

        xdgSurface.OnConfigure += (serial) =>
        {
            xdgSurface.AckConfigure(serial);
            configured = true;
        };

        topLevel.OnConfigure += (w, h, states) =>
        {
            if (w > 0 && h > 0)
            {
                width = w;
                height = h;
            }
        };

        surface.Commit();
        display.Flush();

        int attempts = 0;
        while (!configured && attempts < 100)
        {
            display.Dispatch();
            Thread.Sleep(10);
            attempts++;
        }

        if (!configured || shm == null) return;

        WlBuffer? buffer = ShmBuffer.CreateSolidColorBuffer(shm, width, height, 0xFF6495ED);
        if (buffer == null) return;

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

        buffer.Destroy();
        surface.Destroy();
        xdgSurface.Destroy();
        topLevel.Destroy();
        display.Disconnect();
    }
}
