# MermaidDotNet <!-- omit from toc -->

<img src="./mermaid.png" alt="Mermaid icon" width="100"/>

A .NET library to generate Mermaid diagrams code.

![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/FoggyBalrog/MermaidDotNet/main-workflow.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=FoggyBalrog_MermaidDotNet&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=FoggyBalrog_MermaidDotNet)
[![GitHub License](https://img.shields.io/github/license/FoggyBalrog/MermaidDotNet?)](LICENSE)
[![NuGet Version](https://img.shields.io/nuget/v/FoggyBalrog.MermaidDotNet?logo=nuget&color=blue)
](https://www.nuget.org/packages/FoggyBalrog.MermaidDotNet)


> [!WARNING]  
> Still under development. Not ready for production.

- [Quick Start](#quick-start)
  - [Flowchart](#flowchart)
  - [Sequence diagram](#sequence-diagram)
  - [Class diagram](#class-diagram)
  - [Entity relationship diagram](#entity-relationship-diagram)
  - [Pie chart](#pie-chart)
  - [Timeline diagram](#timeline-diagram)
- [License](#license)
- [Credits](#credits)

## Quick Start

The following code samples show how to create a simple Mermaid diagram of each implemented diagram type.

### Flowchart

```csharp
string diagram = Mermaid
    .Flowchart()
    .AddNode("N1", out var n1)
    .AddNode("N2", out var n2)
    .AddNode("N3", out var n3)
    .AddLink(n1, n2, "some text")
    .AddLink(n2, n3)
    .Build();
```

Read more at [docs/flowchart.md](docs/flowchart.md).

### Sequence diagram

```csharp
string diagram = Mermaid
    .SequenceDiagram()
    .AddParticipant("Alice", out var a)
    .AddParticipant("Bob", out var b)
    .SendMessage(a, b, $"Hello {b.Name}!")
    .SendMessage(b, a, $"Hello {a.Name}!")
    .Build();
```

Read more at [docs/sequence-diagram.md](docs/sequence-diagram.md).

### Class diagram

```csharp
var diagram = Mermaid
    .ClassDiagram()
    .AddClass("Animal", out var animal)
    .AddClass("Dog", out var dog)
    .AddProperty(animal, "int", "Age")
    .AddMethod(animal, null, "Breathe")
    .AddMethod(animal, "void", "Eat", parameters: 
    [
        ("Food", "food")
    ])
    .AddMethod(dog, "Sound", "Bark", parameters: 
    [
        ("int", "times"),
        ("int", "volume")
    ])
    .AddRelationship(animal, dog, RelationshipType.Inheritance, label: "A dog is an animal")
    .Build();
```

Read more at [docs/class-diagram.md](docs/class-diagram.md).

### Entity relationship diagram

```csharp
string diagram = Mermaid
    .EntityRelationshipDiagram()
    .AddEntity("Customer", out var c)
    .AddEntity("Order", out var o)
    .AddEntity("Product", out var p)
    .AddRelationship(Cardinality.ExactlyOne, c, Cardinality.ZeroOrMore, o, "places")
    .AddRelationship(Cardinality.ExactlyOne, o, Cardinality.OneOrMore, p, "contains")
    .Build();
```

Read more at [docs/entity-relationship-diagram.md](docs/entity-relationship-diagram.md).

### Pie chart

```csharp
var pieChart = Mermaid
    .PieChart()
    .AddDataSet("Label1", 42.7)
    .AddDataSet("Label2", 57.3)
    .Build();
```

Read more at [docs/pie-chart.md](docs/pie-chart.md).

### Timeline diagram

```csharp
string diagram = Mermaid
    .TimelineDiagram("Some title")
    .AddEvents("2021", "Event 1", "Event 2")
    .AddEvents("2022", "Event 3")
    .AddEvents("2023", "Event 4", "Event 5", "Event 6")
    .Build();
```

Read more at [docs/timeline-diagram.md](docs/timeline-diagram.md).

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Credits

Mermaid icon created by [Smashicons on Flaticon](https://www.flaticon.com/authors/smashicons).