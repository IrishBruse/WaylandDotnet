alias b := build

mod run 'Examples'

build:
    dotnet build

[working-directory: 'WaylandDotnetScanner']
gen:
    dotnet run -- ./protocols.json

[working-directory: 'WaylandDotnetScanner']
wayland-dotnet-scanner *args:
    clear
    dotnet run -- {{args}}

docs:
    docsify serve docs

fetch-wayland-protocols:
    wget -O "./WaylandScanner/Protocols/Core/wayland.xml"                           https://gitlab.freedesktop.org/wayland/wayland/-/raw/main/protocol/wayland.xml
    wget -O "./WaylandScanner/Protocols/Stable/xdg-shell.xml"                       https://gitlab.freedesktop.org/wayland/wayland-protocols/-/raw/main/stable/xdg-shell/xdg-shell.xml
    wget -O "./WaylandScanner/Protocols/Wlr/wlr-layer-shell-unstable-v1.xml"        https://gitlab.freedesktop.org/wlroots/wlr-protocols/-/raw/master/unstable/wlr-layer-shell-unstable-v1.xml
    wget -O "./WaylandScanner/Protocols/River/river-window-management-v1.xml"       https://codeberg.org/river/river/raw/branch/main/protocol/river-window-management-v1.xml

wayland-scanner:
    wayland-scanner client-header WaylandScanner/Protocols/Core/wayland.xml                         tmp/wayland.h
    wayland-scanner client-header WaylandScanner/Protocols/Stable/xdg-shell.xml                     tmp/xdg-shell.h
    wayland-scanner client-header WaylandScanner/Protocols/Wlr/wlr-layer-shell-unstable-v1.xml      tmp/wlr-layer-shell-unstable-v1.h
    wayland-scanner client-header WaylandScanner/Protocols/River/river-window-management-v1.xml     tmp/river-window-management-v1.h

pack:
    dotnet pack WaylandDotnet/WaylandDotnet.csproj /p:NuspecFile=WaylandDotnet.nuspec
    dotnet pack WaylandScanner/WaylandScanner.csproj /p:NuspecFile=WaylandScanner.nuspec
