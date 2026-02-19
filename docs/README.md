# WaylandDotnet

> A .NET 10 C# binding for the Wayland display server protocol

WaylandDotnet provides C# access to Wayland client functionality with AOT compilation support, enabling you to build native Wayland applications in C#.

## Features

- **Native AOT Compatible** - Uses `LibraryImport` instead of `DllImport` for ahead-of-time compilation
- **Automatic Code Generation** - Generates C# bindings from Wayland XML protocol definitions
- **Type-Safe Events** - C# event handlers for all Wayland protocol events
- **Minimal Overhead** - Direct native interop with minimal marshaling overhead

## Installation

```bash
dotnet add package WaylandDotnet
```

## Quick Start

```csharp
using WaylandDotnet;
using WaylandDotnet.Stable;

// Connect to Wayland display
var display = WlDisplay.Connect();
var registry = display.GetRegistry();

// Bind to required protocols
WlCompositor? compositor = null;
XdgWmBase? xdg = null;

registry.OnGlobal += (name, interfaceName, version) =>
{
    if (interfaceName == WlCompositor.InterfaceName)
        compositor = registry.Bind<WlCompositor>(name, version);
    if (interfaceName == XdgWmBase.InterfaceName)
        xdg = registry.Bind<XdgWmBase>(name, version);
};

display.Roundtrip();

// Create a window
var surface = compositor!.CreateSurface();
var xdgSurface = xdg!.GetXdgSurface(surface);
var toplevel = xdgSurface.GetToplevel();
toplevel.SetTitle("Hello Wayland");
surface.Commit();
```


## Requirements

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- `libwayland-client` installed

## License

MIT License - See [LICENSE](https://github.com/IrishBruse/WaylandDotnet/blob/main/LICENSE) for details.

---

<p align="center">
  <a href="https://github.com/IrishBruse/WaylandDotnet">GitHub</a> •
  <a href="Protocols/wayland.md">Documentation</a> •
  <a href="https://www.nuget.org/packages/WaylandDotnet">NuGet</a>
</p>
