# Screaminlean.Radzen.Blazor.Templates

---

A set of dotnet templates for Blazor using the [RadzenBlazor](https://blazor.radzen.com/) package, all documentation for the Radzen components can be found [here](https://blazor.radzen.com/).

- [Screaminlean.Radzen.Blazor.Templates](#screaminleanradzenblazortemplates)
  - [Build](#build)
  - [Install The Template Pack](#install-the-template-pack)
  - [Uninstall the template pack](#uninstall-the-template-pack)
  - [Blazor Server Side](#blazor-server-side)
    - [Basic](#basic)
    - [Auth](#auth)
  - [Blazor WebAssembly](#blazor-webassembly)
  - [Issues](#issues)
  - [Contribute](#contribute)
  - [License](#license)

## Build

---

- Download the repo.
- In your terminal change inthe the templatepack directory.
- Execute `dotnet pack`.
- You should see **Successfully created package 'C:\templatepack\nupkg\Screaminlean.Radzen.Blazor.Templates.{version}.nupkg'**.

## Install The Template Pack

---

- Install the template pack file with the `dotnet new -i PATH_TO_NUPKG_FILE` command.

**Nuget packasge not uploaded yet! TBA.**
This will change one I can get a code signing certificate to upload to nuget.

## Uninstall the template pack

---

- List the installed templates `dotnet new -u`.
- Find the ** Uninstall Command: ** `dotnet new -u Screaminlean.Radzen.Blazor.Templates.{version}`.
- Run the command `dotnet new -u Screaminlean.Radzen.Blazor.Templates.{version}`.

## Blazor Server Side

---

Blazor Server Side Templates

### Basic

The basic template is just a `dotnet new blazorserver` project with [RadzenBlazor](https://blazor.radzen.com/) package added and setup.

**Create a New Basic Project**
Run `dotnet new rbsbasic -o YourProjectName`
or
Run `dotnet new Screaminlean.Radzen.Blazor.Server.Basic -o YourProjectName`
Then run `dotnet restore` see issues below.
### Auth

Same as the Basic but with the individual auth set `dotnet new blazorserver -o {APP NAME} -au Individual` and the [RadzenBlazor](https://blazor.radzen.com/) package added and setup.

**Create a New Auth Project**
Run `dotnet new rbsauth -o YourProjectName`
or
Run `dotnet new Screaminlean.Radzen.Blazor.Server.Auth -o YourProjectName`
Then run `dotnet restore` see issues below.

## Blazor WebAssembly

---

- Basic

## Issues

---

- Not on Nuget (Need a signing cert)
- dotnet restore not working after new project created (Will look at this soon)

## Contribute

---

You are more than welcome to contribute!

## License

---

MIT

**Free Software, Hell Yeah!**