namespace Example;

using WaylandDotnet;
using WaylandDotnet.Stable;

public class Example
{
    public static int Run()
    {
        WaylandLogger.Initialize();

        // 1. Connect
        WlDisplay display = WlDisplay.Connect();
        WlRegistry registry = display.GetRegistry();

        // 2. Bind protocols
        WlCompositor? compositor = null;
        XdgWmBase? xdg = null;
        registry.OnGlobal += (name, interfaceName, version) =>
        {
            if (interfaceName == WlCompositor.InterfaceName) compositor = registry.Bind<WlCompositor>(interfaceName, version, name);
            if (interfaceName == XdgWmBase.InterfaceName) xdg = registry.Bind<XdgWmBase>(interfaceName, version, name);
        };
        display.Roundtrip();

        // 3. Create window
        WlSurface surface = compositor!.CreateSurface();
        XdgSurface xdgSurface = xdg!.GetXdgSurface(surface);
        xdgSurface.OnConfigure += (serial) => { xdgSurface.AckConfigure(serial); surface.Commit(); };
        XdgToplevel topLevel = xdgSurface.GetToplevel();
        topLevel.SetTitle("Minimal");
        surface.Commit();
        display.Flush();

        // 4. Keep alive
        Console.ReadLine();

        return 0;
    }
}