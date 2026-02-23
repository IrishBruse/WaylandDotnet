alias b := build

mod run 'Examples'

build:
    dotnet build

[working-directory: 'WaylandDotnet.Scanner']
gen:
    dotnet run -- ./protocols.json

[working-directory: 'WaylandDotnet.Scanner']
wayland-dotnet-scanner *args:
    clear
    dotnet run -- {{args}}

docs:
    docsify serve docs

fetch-wayland-protocols:
    wget -O "./WaylandDotnet.Scanner/Protocols/Core/wayland.xml"                        https://gitlab.freedesktop.org/wayland/wayland/-/raw/main/protocol/wayland.xml &

    wget -O "./WaylandDotnet.Scanner/Protocols/Stable/linux-dmabuf-v1.xml"              https://gitlab.freedesktop.org/wayland/wayland-protocols/-/raw/main/stable/linux-dmabuf/linux-dmabuf-v1.xml &
    wget -O "./WaylandDotnet.Scanner/Protocols/Stable/presentation-time.xml"            https://gitlab.freedesktop.org/wayland/wayland-protocols/-/raw/main/stable/presentation-time/presentation-time.xml &
    wget -O "./WaylandDotnet.Scanner/Protocols/Stable/tablet-v2.xml"                    https://gitlab.freedesktop.org/wayland/wayland-protocols/-/raw/main/stable/tablet/tablet-v2.xml &
    wget -O "./WaylandDotnet.Scanner/Protocols/Stable/viewporter.xml"                   https://gitlab.freedesktop.org/wayland/wayland-protocols/-/raw/main/stable/viewporter/viewporter.xml &
    wget -O "./WaylandDotnet.Scanner/Protocols/Stable/xdg-shell.xml"                    https://gitlab.freedesktop.org/wayland/wayland-protocols/-/raw/main/stable/xdg-shell/xdg-shell.xml &

    wget -O "./WaylandDotnet.Scanner/Protocols/Wlr/wlr-layer-shell-unstable-v1.xml"     https://gitlab.freedesktop.org/wlroots/wlr-protocols/-/raw/master/unstable/wlr-layer-shell-unstable-v1.xml &

    wget -O "./WaylandDotnet.Scanner/Protocols/River/river-window-management-v1.xml"    https://codeberg.org/river/river/raw/branch/main/protocol/river-window-management-v1.xml &

wayland-scanner:
    wayland-scanner client-header WaylandScanner/Protocols/Core/wayland.xml                         tmp/wayland.h
    wayland-scanner client-header WaylandScanner/Protocols/Stable/xdg-shell.xml                     tmp/xdg-shell.h
    wayland-scanner client-header WaylandScanner/Protocols/Wlr/wlr-layer-shell-unstable-v1.xml      tmp/wlr-layer-shell-unstable-v1.h
    wayland-scanner client-header WaylandScanner/Protocols/River/river-window-management-v1.xml     tmp/river-window-management-v1.h

pack:
    dotnet pack WaylandDotnet/WaylandDotnet.csproj /p:NuspecFile=WaylandDotnet.nuspec
    dotnet pack WaylandDotnet.Scanner/WaylandDotnet.Scanner.csproj /p:NuspecFile=WaylandScanner.nuspec
