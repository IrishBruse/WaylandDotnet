# RiverWindowManager Example

A custom window manager built using the river-window-management-v1 protocol. This example demonstrates how to create a Wayland compositor/window manager using WaylandDotnet.

## Overview

This example shows how to:
- Bind to the river-window-management-v1 global
- Handle, manage and render sequences
- Manage windows, outputs, and seats
- Implement keyboard and pointer bindings

## Running

`just run::RiverWindowManager` stops leftover river sessions, leaves the current session compositor running, and connects this example to a nested river on `wayland-2`.

A previous copy of the example is stopped first, so river's single window-manager slot is free.

```bash
just run::RiverWindowManager
```

## Requirements

- A Wayland compositor supporting river-window-management-v1 (e.g., River)
- .NET 10.0 SDK
- libwayland-client

## Protocol

The river-window-management-v1 protocol enables building custom window managers by providing:

- Window management state (dimensions, fullscreen, focus)
- Rendering state (position, layer order)
- Input handling (keyboard bindings, pointer operations)

See [River Protocol Documentation](../../docs/Protocols/River/river-window-management-v1.md) for details.
