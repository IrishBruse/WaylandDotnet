# WaylandDotnet Example

A GPU-accelerated overlay application demonstrating WaylandDotnet with SDL3 GPU and wlr-layer-shell.

Show a spinning triangle in a layer shell with Exclusive Keyboard Interactivity.
Press `ESC` to close the layer

## Overview

This example creates a semi-transparent overlay window using:
- **WaylandDotnet** - Wayland protocol bindings
- **wlr-layer-shell** - For creating overlay surfaces
- **SDL3 GPU** - For GPU-accelerated rendering
- **SPIR-V shaders** - For the rotating triangle demo

## Architecture

The application integrates these components:

1. **Wayland Layer Shell Surface** - Created via WaylandDotnet, provides the overlay window
2. **SDL3 Window** - Wraps the custom Wayland surface for GPU access
3. **SDL3 GPU** - Handles rendering with SPIR-V shaders
4. **Event Loop** - Processes both SDL and Wayland events

## Running

```bash
cd Example
dotnet run
```

Press `ESC` to exit.

## Files

- **Example.cs** - Main application logic, rendering loop
- **Program.cs** - Entry point
- **SDL3.cs** - SDL3 C# bindings (auto-generated)
- **Shaders/** - SPIR-V vertex and fragment shaders

## Shaders

The triangle demo uses simple SPIR-V shaders:
- **Triangle.vert.spv** - Vertex shader with rotation uniform
- **Triangle.frag.spv** - Fragment shader with vertex colors

## Dependencies

- libwayland-client.so.0
- SDL3 (included in libs/ for Linux, macOS, Windows)
- A Wayland compositor supporting wlr-layer-shell (e.g., Sway, Hyprland)

## How It Works

1. Connects to Wayland display and binds required interfaces
2. Creates a layer shell surface with overlay + exclusive zone
3. Creates SDL window wrapping the custom Wayland surface
4. Initializes SDL GPU device and loads SPIR-V shaders
5. Renders a rotating colored triangle at 60 FPS
6. Handles Wayland configure events to resize accordingly
