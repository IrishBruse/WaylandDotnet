# Contributing

Thanks for helping improve WaylandDotnet. This guide covers local development and maintaining protocol bindings in the repo.

## Source of truth: `protocols.json`

Bundled protocols are defined in [WaylandDotnet.Scanner/protocols.json](WaylandDotnet.Scanner/protocols.json):

| Field        | Role                                                        |
| ------------ | ----------------------------------------------------------- |
| `OutputRoot` | Generated C# output (default: `../WaylandDotnet/Protocols`) |
| `DocsDir`    | Generated docs (default: `../docs`)                         |
| `Protocols`  | List of protocol entries (see below)                        |

Each protocol entry:

| Field       | Required | Role                                                         |
| ----------- | -------- | ------------------------------------------------------------ |
| `Name`      | yes      | Display name (logging and docs)                              |
| `XmlFile`   | yes      | Path to XML, relative to the scanner project                 |
| `Namespace` | yes      | C# namespace segment (`Core`, `Stable`, `Wlr`, `River`, ...) |
| `SourceUrl` | no       | Remote URL; `download` refreshes `XmlFile`                   |
| `Link`      | no       | Override link in generated docs                              |

Do not hand-edit generated files under `WaylandDotnet/Protocols/` or `docs/Protocols/` for routine changes. Update `protocols.json`, refresh XML if needed, then regenerate.

## Layout

| Path                               | Purpose                                   |
| ---------------------------------- | ----------------------------------------- |
| `WaylandDotnet/`                   | Core client library                       |
| `WaylandDotnet.Scanner/`           | Code generator (`wayland-dotnet-scanner`) |
| `WaylandDotnet.Scanner/Protocols/` | Wayland XML inputs                        |
| `WaylandDotnet/Protocols/`         | Generated C# bindings                     |
| `docs/Protocols/`                  | Generated protocol documentation          |
| `Examples/`                        | Sample applications                       |

## Prerequisites

- .NET 10.0 SDK
- `libwayland-client`
- [just](https://github.com/casey/just) (optional, recommended)

## Setup

```bash
git clone https://github.com/IrishBruse/WaylandDotnet.git
cd WaylandDotnet
dotnet build
dotnet test
dotnet run --project Examples/Minimal
```

## Regenerating bindings and docs

From the repo root (with `just`):

```bash
just update    # download XML, then generate
```

Without `just`, from `WaylandDotnet.Scanner/`:

```bash
dotnet run -- download
dotnet run
```

Output:

- C#: `WaylandDotnet/Protocols/{Namespace}/{protocol-name}/`
- Docs: `docs/Protocols/{Namespace}/{protocol-name}/`
- Sidebar: `docs/sidebar.md` protocols section (between `<!-- wayland-dotnet-scanner:protocols -->` markers, when `DocsDir` is set)

## Adding or updating a protocol

1. Edit `protocols.json` (add or change a `Protocols` entry).
2. Vendor XML under `WaylandDotnet.Scanner/Protocols/...`, or set `SourceUrl` and run `just download`.
3. Run `just update` (or `just gen` if XML is already local).
4. Run `dotnet build` and `dotnet test`.
5. Commit `protocols.json`, vendored XML, generated C#, and docs together.

Protocol XML sources:

- [wayland.app/protocols](https://wayland.app/protocols/)
- `/usr/share/wayland-protocols/` on many Linux systems
- Compositor repos (wlroots, River, Hyprland, etc.)

## Scanner tool (external projects)

For consumers outside this repo, the scanner is published as a global .NET tool. See [WaylandDotnet.Scanner/README.md](WaylandDotnet.Scanner/README.md) for `init`, single-file generation, and other commands. In this repo, prefer `protocols.json` and `just update`.

## Pull requests

- Drive protocol changes through `protocols.json` plus regenerated output.
- Run `dotnet build` and `dotnet test` before opening a PR.
- Note which protocols changed and why.

## Links

- [Wayland protocols](https://wayland.app/protocols/)
- [WaylandDotnet docs](https://ethanconneely.com/WaylandDotnet)
- [Protocol browser](https://ethanconneely.com/WaylandDotnet/#/Protocols/Core/wayland/)
