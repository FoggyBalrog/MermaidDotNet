# MermaidDotNet <!-- omit from toc -->

<img src="./mermaid.png" alt="Mermaid icon" width="100"/>

A .NET library to generate Mermaid diagrams from C# code.

![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/FoggyBalrog/MermaidDotNet/main-workflow.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=FoggyBalrog_MermaidDotNet&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=FoggyBalrog_MermaidDotNet)
[![GitHub License](https://img.shields.io/github/license/FoggyBalrog/MermaidDotNet)](LICENSE)

> [!WARNING]  
> Still under development. Not ready for production.

- [Quick Start](#quick-start)
  - [Flowchart](#flowchart)
  - [Sequence diagram](#sequence-diagram)
  - [Entity relationship diagram](#entity-relationship-diagram)
  - [Pie chart](#pie-chart)
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

## License

This project is licensed under the GNU Affero General Public License v3.0. See the [LICENSE](LICENSE) file for details.

## Credits

Mermaid icon created by [Smashicons on Flaticon](https://www.flaticon.com/authors/smashicons).