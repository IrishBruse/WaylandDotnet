# WaylandDotnet Example

A basic layershell taskbar demonstrating WaylandDotnet with SDL3 GPU and wlr-layer-shell.

Shows a taskbar panel at the bottom of the screen using the wlr-layer-shell protocol.

## Overview

This example creates a bottom-aligned taskbar panel using:
- **WaylandDotnet** - Wayland protocol bindings
- **wlr-layer-shell** - For creating layer shell surfaces
- **SDL3 GPU** - For GPU-accelerated rendering
- **SPIR-V shaders** - For rendering the taskbar background

## Architecture

The application integrates these components:

1. **Wayland Layer Shell Surface** - Created via WaylandDotnet with Bottom layer
2. **SDL3 Window** - Wraps the custom Wayland surface for GPU access
3. **SDL3 GPU** - Handles rendering with SPIR-V shaders
4. **Event Loop** - Processes both SDL and Wayland events

## Running

```bash
just build
cd Examples/Taskbar
dotnet run
```

Press `ESC` or `Q` to exit.

## Files

- **Example.cs** - Main application logic, rendering loop
- **Program.cs** - Entry point
- **Taskbar.csproj** - Project file
- **Shaders/** - SPIR-V vertex and fragment shaders

## Shaders

The taskbar uses simple SPIR-V shaders:
- **Taskbar.vert.spv** - Vertex shader for fullscreen quad
- **Taskbar.frag.spv** - Fragment shader with vertical gradient

## Dependencies

- libwayland-client.so.0
- SDL3 (included in libs/ for Linux, macOS, Windows)
- A Wayland compositor supporting wlr-layer-shell (e.g., Sway, Hyprland)

## How It Works

1. Connects to Wayland display and binds required interfaces
2. Creates a layer shell surface with Bottom layer, anchored to screen bottom
3. Sets exclusive zone to reserve taskbar space from the compositor
4. Creates SDL window wrapping the custom Wayland surface
5. Initializes SDL GPU device and loads SPIR-V shaders
6. Renders a gradient taskbar background at 60 FPS
7. Handles Wayland configure events to resize accordingly
