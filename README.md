# Coaster

[![Build](https://github.com/xafero/Coaster/actions/workflows/dotnet.yml/badge.svg)](https://github.com/xafero/Coaster/actions/workflows/dotnet.yml) [![NuGet](https://img.shields.io/nuget/v/Coaster.svg)](https://www.nuget.org/packages/Coaster/) [![Downloads](https://img.shields.io/nuget/dt/Coaster.svg)](https://www.nuget.org/packages/Coaster/)

The project Coaster is a library that allows easy parsing and formatting of C# source files. Coaster introduces a nice interface to manipulate C# source files, like adding fields, methods, attributes and so on.

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

### C# Source Code Generation API

Coaster provides a nice API to generate C# classes. Here is an example:

```csharp
var unit = new CUnit { Usings = { "System.Linq", "System", "System.IO" }, Members =
    {
        new CNamespace { Name = "Example", Members =
            {
                new CClass { Name = "Person", Members =
                    {
                        new CProperty { Type = "int", Name = "Id" },
                        new CProperty { Type = "string", Name = "FirstName" },
                        new CProperty { Type = "string", Name = "LastName" }
                    }
                }
            }
        }
    }
};
Console.WriteLine(unit.ToText());
```

This will produce:

```csharp
using System;
using System.IO;
using System.Linq;

namespace Example
{
    public class Person 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
```

### C# Source Code Modification API

Of course, it is possible to mix both approaches (parser and writer) to modify C# code programmatically:

```csharp
var unit = Coast.Parse("public class SomeClass {}");
var clazz = unit.Members.Cast<CClass>().Single();

clazz.Members.Add(new CMethod { Name = "Main" });

Console.WriteLine(unit.ToText());
```

### Formatting the C# Source Code

Coaster formats the C# Source Code by calling the `Format()` method:

```csharp
var humanCode = "public class MyClass{ private string field;}";
var formattedCode = Coast.Format(humanCode);
Console.WriteLine(formattedCode);
```

## Building from sources

Just run `dotnet pack` to build the sources.

## Background info

This project uses the syntax tree parsing and writing of:

* [dotnet/roslyn](https://github.com/dotnet/roslyn)

## License

Everything is licensed according to [this](./LICENSE).
