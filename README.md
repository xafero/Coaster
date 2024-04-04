# Coaster

[![Build](https://github.com/xafero/Coaster/actions/workflows/dotnet.yml/badge.svg)](https://github.com/xafero/Coaster/actions/workflows/dotnet.yml) [![NuGet](https://img.shields.io/nuget/v/Coaster.svg)](https://www.nuget.org/packages/Coaster/)

The project Coaster is a library that allows easy parsing and formatting of C# source files. Coaster introduces a fluent interface to manipulate C# source files, like adding fields, methods, attributes and so on.

## Installation

```bash
dotnet add package Coaster
```

## Usage

### C# Parser API

Example:
```csharp
Coast.Parse("public class HelloWorld {}");
```

## License

Everything is licensed according to [this](./LICENSE).
