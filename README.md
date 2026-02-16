# WaylandDotnet

A .NET 10 C# binding for the Wayland display server protocol. This library provides C# access to Wayland client functionality with AOT compilation support.

## Projects

- **WaylandDotnet** - Core library for Wayland client communication
- **WaylandScanner** - XML protocol parser and C# code generator
- **Example** - Sample application using SDL3 GPU with wlr-layer-shell
- **WaylandDotnet.Tests** - xUnit test suite

## Quick Start

```bash
# Build the solution
dotnet build

# Run tests
dotnet test

# Generate protocol code (rebuilds C# from XML)
cd WaylandScanner && dotnet run

# Run the example window creation application
cd Examples/Minimal && dotnet run

# Run the example wlr_layer_shell application
cd Examples/LayerShell && dotnet run
```

## Supported Protocols

- **Core**: wayland
- **Stable**: xdg-shell
- **Wlr**: wlr-layer-shell-v1

## Features

- Native AOT compatible (LibraryImport instead of DllImport)
- Automatic code generation from Wayland XML protocols
- Type-safe event handling with C# events

## Requirements

- .NET 10.0 SDK
- libwayland-client
- Wayland compositor (for running examples)

## Example Usage

```csharp
using WaylandDotnet;

// Connect to Wayland display
var display = WlDisplay.Connect();

// Get registry and bind globals
var registry = display.GetRegistry();
registry.OnGlobal += (name, interfaceName, version) => {
    Console.WriteLine($"Global: {interfaceName}");
};
display.Roundtrip();
```

See the [Examples/](Examples/) directory for a complete GPU-accelerated overlay application.
