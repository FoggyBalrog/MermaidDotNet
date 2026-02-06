# **FoggyBalrog.MermaidDotNet** Documentation

**FoggyBalrog.MermaidDotNet** is .NET library to generate Mermaid diagrams code.

> [!NOTE]  
> This documentation applies to the latest published version, excluding pre-releases.
> 
> [![NuGet Version](https://img.shields.io/nuget/v/FoggyBalrog.MermaidDotNet?logo=nuget&color=blue)](https://www.nuget.org/packages/FoggyBalrog.MermaidDotNet)

See the source code on GitHub: [FoggyBalrog/MermaidDotNet](https://github.com/FoggyBalrog/MermaidDotNet).

## Add the library to your project

Install the [FoggyBalrog.MermaidDotNet NuGet package from nuget.org](https://www.nuget.org/packages/FoggyBalrog.MermaidDotNet), or download [the *nupkg* file from the latest release on GitHub](https://github.com/FoggyBalrog/MermaidDotNet/releases/latest).

## Compatibility

The library targets **.NET Standard 2.1**, that is notably compatible with .NET Core 3.0 and later, .NET 5.0 and later, and Mono 6.4 and later.

See details [on the package _frameworks_ tab on nuget.org](https://www.nuget.org/packages/FoggyBalrog.MermaidDotNet#supportedframeworks-body-tab) or [on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-2-1#select-net-standard-version).


<!-- Quick Start Placeholder -->


## Unsafe mode

By default, the library uses safe mode, which means that it will throw an exception if the arguments passed to the methods are invalid.

You can disable this behavior by accessing the buiders through the `Unsafe` property in the `Mermaid` class.

Example:

```csharp
string diagram = Mermaid
    .Unsafe
    .Flowchart()
    .AddNode("N1", out var n1)
    .AddNode("N2", out var n2)
    .AddNode("N3", out var n3)
    .AddLink(n1, n2, "some text")
    .AddLink(n2, n3)
    .Build();
```

## License

This project is licensed under the MIT License. See the [LICENSE](https://github.com/FoggyBalrog/MermaidDotNet/blob/main/LICENSE) file for details.

## Credits

Mermaid icon created by [Smashicons on Flaticon](https://www.flaticon.com/authors/smashicons).