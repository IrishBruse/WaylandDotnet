alias b := build

mod run 'Examples'
mod protocols 'WaylandDotnet.Scanner/Protocols'

build:
    dotnet build

[working-directory: 'WaylandDotnet.Scanner']
gen:
    dotnet run

[working-directory: 'WaylandDotnet.Scanner']
wayland-dotnet-scanner *args:
    clear
    dotnet run -- {{args}}

docs:
    docsify serve docs

update:
    just protocols download
    just gen
