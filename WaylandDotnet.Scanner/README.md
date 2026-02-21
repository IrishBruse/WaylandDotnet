# WaylandDotnet.Scanner

A .NET 10 tool that generates C# bindings from Wayland XML protocol files.

## Installation

```bash
dotnet tool install --global WaylandDotnet.Scanner
```

## Quick Start

```bash
# Create a protocols.json configuration file
wayland-dotnet-scanner init

# Edit protocols.json to add your protocols, then generate code
wayland-dotnet-scanner
```

## Configuration (protocols.json)

```json
{
  "OutputRoot": "./Generated",
  "DocsDir": "./docs",
  "Protocols": [
    {
      "Name": "Wayland",
      "XmlFile": "protocols/wayland.xml",
      "Namespace": "Core"
    },
    {
      "Name": "XDG Shell",
      "XmlFile": "protocols/xdg-shell.xml",
      "Namespace": "Stable"
    }
  ]
}
```

### Configuration Options

| Field                   | Description                                  |
| ----------------------- | -------------------------------------------- |
| `OutputRoot`            | Root directory for generated C# files        |
| `DocsDir`               | Optional directory for documentation sidebar |
| `Protocols[]`           | Array of protocol configurations             |
| `Protocols[].Name`      | Display name for the protocol                |
| `Protocols[].XmlFile`   | Path to the Wayland XML protocol file        |
| `Protocols[].Namespace` | Category/namespace (e.g., Core, Stable, Wlr) |
| `Protocols[].Link`      | Optional URL to protocol documentation       |

## CLI Commands

```bash
# Create default protocols.json
wayland-dotnet-scanner init

# Generate from protocols.json (auto-detected in current directory)
wayland-dotnet-scanner

# Generate from specific config file
wayland-dotnet-scanner ./my-protocols.json

# Generate from a single XML file
wayland-dotnet-scanner protocol.xml ./Output --namespace MyNamespace --name "My Protocol"

# List protocols in config
wayland-dotnet-scanner list

# Show help
wayland-dotnet-scanner --help
```

## Finding Protocol XML Files

- [wayland.app/protocols](https://wayland.app/protocols/) - Links to official Wayland protocols
- `/usr/share/wayland-protocols/` - System-installed stable protocols
- Compositor-specific protocols (wlroots, River, Hyprland, etc.)

## Generated Output

For each protocol, the scanner generates:

- `{Namespace}/{ProtocolName}/Protocol.cs` - Main protocol classes
- `{Namespace}/{ProtocolName}/Copyright.txt` - Protocol copyright info

When `DocsDir` is set, also generates:
- `Protocols/_sidebar.md` - Documentation sidebar for Docsify

## Features

- Generates type-safe C# bindings from Wayland XML protocols
- C# events instead of C callbacks
- Nullable reference types support
- Namespace organization for protocol categories
- Optional documentation generation

## Requirements

- .NET 10.0 SDK
- Wayland XML protocol files
