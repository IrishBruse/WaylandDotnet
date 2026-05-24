<p align="center">
  <img src="./logo.png"><br>
</p>

<p align="center">
  <a href="https://www.nuget.org/packages/WaylandDotnet/"><img src="https://img.shields.io/nuget/v/WaylandDotnet?label=WaylandDotnet" /></a>
  <a href="https://www.nuget.org/packages/WaylandDotnet.Scanner/"><img src="https://img.shields.io/nuget/v/WaylandDotnet.Scanner?label=WaylandDotnet.Scanner" /></a>
</p>

# WaylandDotnet

C# bindings for the Wayland protocol with native AOT support via `LibraryImport`.

## Features

- **Native AOT** - Zero-overhead interop for high-performance apps
- **Code generation** - CLI tool turns Wayland XML into type-safe C#
- **Idiomatic C#** - Events instead of C-style callbacks
- **Extensible** - Core, stable, wlroots, River, and custom compositor protocols
- **Documentation** - [Protocol reference and tutorials](https://ethanconneely.com/WaylandDotnet)

## Requirements

- .NET 10.0 SDK
- `libwayland-client` (system library)
- A running Wayland compositor (for running examples)

## Quick example

```csharp
using WaylandDotnet;

// 1. Connect to the Wayland display
using var display = WlDisplay.Connect();

// 2. Get the registry and bind to global objects
var registry = display.GetRegistry();
WlCompositor? compositor = null;

registry.OnGlobal += (name, interfaceName, version) => {
  Console.WriteLine($"Found global: {interfaceName} (v{version})");

  // 3. Bind to a global
  if (interfaceName == WlCompositor.InterfaceName)
  {
    compositor = registry.Bind<WlCompositor>(name, version);
  }
};

// 4. Dispatch events
display.Roundtrip();
```

## Scanner

Install the global tool:

```bash
dotnet tool install --global WaylandDotnet.Scanner
```

Generate from a single XML file:

```bash
wayland-dotnet-scanner wayland.xml ./Protocols/wayland/ --namespace Core
```

Or use a `protocols.json` config (`wayland-dotnet-scanner init`, edit, then run `wayland-dotnet-scanner`). See [WaylandDotnet.Scanner/README.md](WaylandDotnet.Scanner/README.md) for CLI options and configuration fields.

## Contributing

This repo has generated files before editing files.

See: [CONTRIBUTING](CONTRIBUTING.md)
