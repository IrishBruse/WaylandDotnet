# Protocols

WaylandDotnet provides C# bindings for Wayland protocol specifications.
These protocols define how applications communicate with the Wayland compositor (window manager).

## Protocol Categories

Protocols are organized by their stability level in the Wayland ecosystem.

### Core

The foundation of Wayland. All compositors support these protocols.

- **wayland** - Essential objects like displays, surfaces, and input handling

### Stable

Protocols that have reached stable status and won't have breaking changes.

- **xdg-shell** - Standard desktop window management (toplevel windows, popups, window states)

### Wlr

Protocols from wlroots, widely supported by compositors like Sway, Hyprland, and others.

- **wlr-layer-shell** - Layer-based surfaces for panels, overlays, lock screens, and backgrounds

### River

Protocols from the River compositor for building custom window managers.

- **river-window-management-v1** - Window management protocol for building Wayland compositors/window managers

## Using Protocols

WaylandDotnet namespaces protocols to keep them organized:

- `WaylandDotnet` - Core protocol
- `WaylandDotnet.Stable` - Stable protocols
- `WaylandDotnet.Wlr` - Wlr protocols
- `WaylandDotnet.River` - River protocols

## Protocol Discovery

Not all compositors support all protocols. Applications should gracefully handle missing
protocols by checking the registry during startup.

## Adding Protocols

WaylandDotnet uses code generation from XML protocol definitions. New protocols can be added by
placing XML files in the scanner's protocol directory and regenerating.

## See Also

- [Main Documentation](../README.md)
- [Tutorials](../Tutorials/README.md)
