# Changelog

## Upcoming

[Diff](https://github.com/IrishBruse/WaylandDotnet/compare/v0.5.1...HEAD)

## 0.5.1

- Refresh protocol XML and regenerate bindings for core `wayland` (v11 `wl_pointer.warp`), `linux-dmabuf-v1`, `xdg-shell`, and `river-window-management-v1`
- Add weekly `update-scanner` GitHub Actions workflow to open PRs with protocol binding updates
- Improve docs site sidebar styling, active highlight, and scroll behavior
- Add page navigation plugin and site footer to generated protocol docs

[Diff](https://github.com/IrishBruse/WaylandDotnet/compare/v0.5.0...v0.5.1)

## 0.5.0

- Add `ext-idle-notify-v1` staging protocol bindings
- Refresh protocol XML and regenerate bindings for core `wayland`, `linux-dmabuf-v1`, `xdg-shell`, and `river-window-management-v1`
- Regenerate River protocol bindings (input management, layer shell, libinput config, xkb bindings, xkb config)
- Add `SourceUrl` to `protocols.json` and wire scanner `download` to refresh XML from upstream
- Improve scanner CLI output with colored, phased logging
- Add `CONTRIBUTING.md` with protocol maintenance workflow
- Streamline README and consolidate docs sidebar

[Diff](https://github.com/IrishBruse/WaylandDotnet/compare/v0.4.0...v0.5.0)

## 0.4.0

- Update bundled protocol XML and regenerate bindings (river window management, tablet-v2, ext-workspace-v1)
- Refresh README and project documentation
- Fix quick-start example in README
- Clean up scanner generator and README

[Diff](https://github.com/IrishBruse/WaylandDotnet/compare/v0.3.0...v0.4.0)

## 0.3.0

- Add wlroots protocol bindings (layer shell, output management, foreign toplevel management)
- Improve generated documentation layout (sidebar, CSS)
- Fix Wayland display discovery in Taskbar example
- Update NuGet release workflow

[Diff](https://github.com/IrishBruse/WaylandDotnet/compare/v0.2.0...v0.3.0)

## 0.2.0

- Add River protocol bindings
- Add all stable Wayland protocols
- Add enum documentation generation
- Add events to generated documentation
- Fix WlArray code generation
- Fix enum color handling
- Simplify constructor generation
- Simplify disposal patterns
- Simplify HTML generation
- Add debug flag to generator

[Diff](https://github.com/IrishBruse/WaylandDotnet/compare/v0.1.0...v0.2.0)

## 0.1.0

- Initial release of WaylandDotnet client library (.NET 10, AOT)
- Add wayland-dotnet-scanner for generating bindings from Wayland XML
- Add core `wayland`, `xdg-shell`, `wlr-layer-shell-unstable-v1`, and `river-window-management-v1` protocol bindings
- Add examples (Minimal, LayerShell, RiverWindowManager)
- Add xUnit test project
- Add documentation site with protocol and tutorial docs
- Publish WaylandDotnet and WaylandDotnet.Scanner to NuGet

[Diff](https://github.com/IrishBruse/WaylandDotnet/commits/v0.1.0)
