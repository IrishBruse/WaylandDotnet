alias b := build

mod run 'Examples'

build:
    dotnet build

docs:
    docsify serve docs

update:
    just download
    just gen

[working-directory: 'WaylandDotnet.Scanner']
download:
    dotnet run -- download

[working-directory: 'WaylandDotnet.Scanner']
gen:
    dotnet run

[working-directory: 'WaylandDotnet.Scanner']
wayland-dotnet-scanner *args:
    clear
    dotnet run -- {{args}}

