# WaylandDotnet

A .NET 10 C# binding for the Wayland display server protocol. This library provides C# access to Wayland client functionality with AOT compilation support.

## Projects

- **WaylandDotnet** - Core library for Wayland client communication
- **WaylandScanner** - XML protocol parser and C# code generator
- **Examples/Minimal** - Minimal window creation example
- **Examples/LayerShell** - GPU-accelerated overlay using SDL3 and wlr-layer-shell
- **Examples/RiverWindowManager** - Custom window manager using river-window-management-v1
- **WaylandDotnet.Tests** - xUnit test suite


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


## Supported Protocols

See [Protocols](https://ethanconneely.com/WaylandDotnet/#/Protocols/)

## Features

- Native AOT compatible (LibraryImport instead of DllImport)
- Automatic code generation from Wayland XML protocols
- Type-safe event handling with C# events

## Requirements

- .NET 10.0 SDK
- libwayland-client
- Wayland compositor

## Code Generation

WaylandDotnet.Scanner generates C# bindings from Wayland XML protocol files.

### Quick Start

```bash
# Create a protocols.json configuration file
wayland-dotnet-scanner init

# Edit protocols.json to add your protocols, then generate code
wayland-dotnet-scanner ./protocols.json
```

### Configuration (protocols.json)

```json
{
  "OutputRoot": "./Generated",
  "DocsDir": "./docs",
  "Protocols": [
    {
      "Name": "My Protocol",
      "XmlFile": "protocols/my-protocol.xml",
      "Namespace": "MyNamespace"
    }
  ]
}
```

### CLI Commands

```bash
# Generate from protocols.json (auto-detected in current directory)
wayland-dotnet-scanner

# Generate a single XML file
wayland-dotnet-scanner protocol.xml ./Output --namespace MyNamespace

# List protocols in config
wayland-dotnet-scanner list

# Show help
wayland-dotnet-scanner --help
```

### Finding Protocol XML Files

- [wayland.app/protocols](https://wayland.app/protocols/) - Official Wayland protocols
- `/usr/share/wayland-protocols/` - System-installed stable protocols
- Compositor-specific protocols (wlroots, River, etc.)

## Development

```bash
# Build the solution
dotnet build

# Run tests
dotnet test

# Generate protocol code (rebuilds C# from XML)
cd WaylandDotnet.Scanner && dotnet run

# Run the minimal window creation example
cd Examples/Minimal && dotnet run

# Run the wlr-layer-shell overlay example
cd Examples/LayerShell && dotnet run

# Run the river window manager example
cd Examples/RiverWindowManager && dotnet run
```

See the [Examples/](Examples/) directory for complete working applications.
