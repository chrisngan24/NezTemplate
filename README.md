# NezDemo
This a starting point for any Nez based project. Heavily based off [Nez-Samples](https://github.com/prime31/Nez-Samples).

# Getting started
- copy this repo to your machine. Rename any instance of `NezGame` with your project title.
TODO: make this a script to autogenerate a new project
- Rename `NezDemo` to whatever you need




# Quirks for Mac
- Mono is complex to set up for Mac
- according to the docs of Nez, it is best to copy Nez directly to your project instead of relying on Nuget (their release cycle on Nuget is very inconsistent).


# notes!
- tmx type is interesting... it right now cannot be exported to the Debug using the Pipeline content manager. Instead it will rely on the raw file assets (the tileset and the tmx file). CBP uses a raw copy instead of a build command on these files.
