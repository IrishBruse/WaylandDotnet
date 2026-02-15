alias b := build

mod run 'Examples'

build:
    dotnet build

[working-directory: 'WaylandScanner']
gen:
    clear
    dotnet run

docs:
    dotnet run --project DocGenerator/DocGenerator.csproj
    docsify serve docs

fetch-wayland-protocols:
    wget -O "./WaylandScanner/Protocols/Core/wayland.xml"                           https://gitlab.freedesktop.org/wayland/wayland/-/raw/main/protocol/wayland.xml
    wget -O "./WaylandScanner/Protocols/Stable/xdg-shell.xml"                       https://gitlab.freedesktop.org/wayland/wayland-protocols/-/raw/main/stable/xdg-shell/xdg-shell.xml
    wget -O "./WaylandScanner/Protocols/Unstable/wlr-layer-shell-unstable-v1.xml"   https://gitlab.freedesktop.org/wlroots/wlr-protocols/-/raw/master/unstable/wlr-layer-shell-unstable-v1.xml

wayland-scanner:
    wayland-scanner client-header WaylandScanner/Protocols/Core/wayland.xml                         tmp/wayland.h
    wayland-scanner client-header WaylandScanner/Protocols/Stable/xdg-shell.xml                     tmp/xdg-shell.h
    wayland-scanner client-header WaylandScanner/Protocols/Unstable/wlr-layer-shell-unstable-v1.xml tmp/wlr-layer-shell-unstable-v1.h
