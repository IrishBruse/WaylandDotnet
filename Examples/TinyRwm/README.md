# tinyrwm

A C# port of https://codeberg.org/river/tinyrwm.
Behavior matches c/tinyrwm.c.

Windows float.
A new window is placed at 0,0.

## Keyboard

- Super+Space: spawn `foot`
- Super+q: close the focused window
- Super+n: cycle keyboard focus through windows (focus the bottom window, which then becomes top)
- Super+Escape: exit the Wayland session

## Pointer

- Super+Left Click: interactive move of the hovered window
- Super+Right Click: interactive resize from the bottom-right edges
- Clicking a window gives it keyboard focus and raises it above other windows

## Running

`just run::TinyRwm` starts river and this window manager together.
River launches the example with `-c`.
If a Wayland session is already running, that river is nested inside it.

```bash
just run::TinyRwm
```

## Requirements

- river (`river-window-management-v1` and `river-xkb-bindings-v1`)
- foot
- .NET 10
- libwayland-client
