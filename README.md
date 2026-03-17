<p align="center">
  <img src="./logo.png"><br>
  <b>WaylandDotnet</b>
</p>

<p align="center">
  <a href="https://www.nuget.org/packages/WaylandDotnet/"><img src="https://img.shields.io/nuget/v/WaylandDotnet?label=WaylandDotnet" /></a>
  <a href="https://www.nuget.org/packages/WaylandDotnet.Scanner/"><img src="https://img.shields.io/nuget/v/WaylandDotnet.Scanner?label=WaylandDotnet.Scanner" /></a>
</p>

# Description

WaylandDotnet provides C# bindings for the Wayland protocol.
This library provides C# access to Wayland client functionality with AOT compilation support.

## Features

- Native AOT: Uses LibraryImport for zero-overhead interop, optimized for high-performance applications.
- Code Generation: Includes a CLI tool to transform any Wayland XML protocol into type-safe C# code.
- Idiomatic C#: Leverages C# events for Wayland event handling.
- Extensible: Support for binding to custom compositor protocols such as wlroots or River.
- Documentation: The protocol, api and tutorials are located [here](https://ethanconneely.com/WaylandDotnet)

# Getting Started

**Requirements:**

- .NET 10.0 SDK
- libwayland-client (System library)
- A running Wayland Compositor/Desktop

## Quick Example

```csharp
using WaylandDotnet;

// 1. Connect to the Wayland display
using var display = WlDisplay.Connect();

// 2. Get the registry and bind to global objects
var registry = display.GetRegistry();
WlCompositor? compositor = null;

registry.OnGlobal += (name, interfaceName, version) => {
  Console.WriteLine($"Found Global: {interfaceName} (v{version})");

  // 3. Bind to a global
  if (interfaceName == WlCompositor.InterfaceName)
  {
    compositor = registry.Bind<WlCompositor>(name, version);
  }
};

// 4. Dispatch events
display.Roundtrip();
```

# Code Generation (Scanner)

The WaylandDotnet.Scanner tool allows you to generate bindings for any protocol XML file.

## Installation

```bash
dotnet tool install --global WaylandDotnet.Scanner
```

## Usage

You can generate code directly from an XML file or use a configuration file for larger projects.

### Direct Command

```bash
wayland-dotnet-scanner wayland.xml ./Protocols/wayland/ --namespace Core
```

### Using protocols.json

1. Initialize: `wayland-dotnet-scanner init`
2. Configure: Edit the generated protocols.json:
```json
{
  "OutputRoot": "./Generated",
  "Protocols": [
    {
      "Name": "Layer Shell",
      "XmlFile": "Protocols/Wlr/wlr-layer-shell-unstable-v1.xml",
      "Namespace": "Wlr"
    }
  ]
}
```

3. Run: `wayland-dotnet-scanner` in the same dir as the `protocols.json`

# Contributing & Development

## Project Structure

- WaylandDotnet: The core library for client-side Wayland communication.
- WaylandDotnet.Scanner: A C# version of `wayland-scanner` for parsing XML protocols and generating C# bindings.
- Examples: Implementation samples including Minimal (XdgToplevel), LayerShell (SDL3), and River WM.

## Setup

To get started with the source code:

```bash
# Clone and build
git clone https://github.com/IrishBruse/WaylandDotnet.git
dotnet build

# Run the minimal window example
dotnet run --project Examples/Minimal

# Execute tests
dotnet test
```

Note: For protocol changes, navigate to WaylandDotnet.Scanner and run the project to regenerate the core library bindings.

## Resources
- Wayland Protocols: [https://wayland.app/protocols/](https://wayland.app/protocols/)
- WaylandDotnet Protocol Browser: [https://ethanconneely.com/WaylandDotnet/#/Protocols/Core/wayland/](https://ethanconneely.com/WaylandDotnet/#/Protocols/Core/wayland/)
