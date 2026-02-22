# WaylandDotnet

A .NET 10 C# binding for the Wayland display server protocol. Provides type-safe access to Wayland client functionality with Native AOT compilation support.

## Installation

```bash
dotnet add package WaylandDotnet
```

## Requirements

- .NET 10.0
- Linux with libwayland-client
- Wayland compositor

## Quick Start

```csharp
using WaylandDotnet;

// Connect to Wayland display
var display = WlDisplay.Connect();

// Get registry and discover globals
var registry = display.GetRegistry();
registry.OnGlobal += (name, interfaceName, version) => 
{
    Console.WriteLine($"Global: {interfaceName} v{version}");
};

display.Roundtrip();
display.Dispose();
```

## Built-in Protocols

| Namespace | Protocol | Description |
|-----------|----------|-------------|
| `WaylandDotnet` | wayland | Core protocol (surfaces, inputs, buffers) |
| `WaylandDotnet.Stable` | xdg-shell | Desktop window management |
| `WaylandDotnet.Wlr` | wlr-layer-shell | Layer surfaces (panels, overlays) |
| `WaylandDotnet.River` | river-window-management | Window manager protocol |

## Features

- **Native AOT compatible** - Uses LibraryImport for ahead-of-time compilation
- **Type-safe events** - C# events instead of C callbacks
- **Nullable reference types** - Full null safety support
- **IDisposable pattern** - Proper resource cleanup

## Example: Create a Window

```csharp
using WaylandDotnet;
using WaylandDotnet.Stable;

var display = WlDisplay.Connect();
var registry = display.GetRegistry();

WlCompositor? compositor = null;
XdgWmBase? xdgWmBase = null;

registry.OnGlobal += (name, interfaceName, version) =>
{
    if (interfaceName == "wl_compositor")
        compositor = registry.Bind<WlCompositor>(name, version);
    else if (interfaceName == "xdg_wm_base")
        xdgWmBase = registry.Bind<XdgWmBase>(name, version);
};

display.Roundtrip();

var surface = compositor!.CreateSurface();
var xdgSurface = xdgWmBase!.GetXdgSurface(surface);
var toplevel = xdgSurface.GetToplevel();

toplevel.SetTitle("My Window");
surface.Commit();

// Main loop
while (true)
{
    display.Dispatch();
}
```

## Documentation

- [Full Documentation](https://ethanconneely.com/WaylandDotnet/)
- [GitHub Repository](https://github.com/IrishBruse/WaylandDotnet)
- [Protocol Reference](https://ethanconneely.com/WaylandDotnet/#/Protocols/)

## License

MIT
