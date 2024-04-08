# MermaidDotNet <!-- omit from toc -->

<img src="./mermaid.png" alt="Mermaid icon" width="100"/>

A .NET library to generate Mermaid diagrams from C# code.

- [Implementation status](#implementation-status)
- [Quick Start](#quick-start)
  - [Sequence diagram](#sequence-diagram)
- [License](#license)
- [Credits](#credits)


## Implementation status

- [ ] Flowchart
- [x] Sequence diagram
- [ ] Class diagram
- [ ] State diagram
- [ ] Entity relationship diagram
- [ ] User journe
- [ ] Gantt
- [ ] Pie chart
- [ ] Quadrant chart
- [ ] Requirement diagram
- [ ] Gitgraph diagram
- [ ] Mindmaps
- [ ] Timeline
- [ ] Zenuml
- [ ] Sankey
- [ ] XYChart
- [ ] Block diagram

Experimental diagrams are not planned to be implemented.

## Quick Start

The following code samples show how to create a simple Mermaid diagram of each implemented diagram type.

### Sequence diagram

```csharp
string diagram = Mermaid
    .SequenceDiagram
    .AddParticipant("Alice", out var a)
    .AddParticipant("Bob", out var b)
    .SendMessage(a, b, $"Hello {b.Name}!")
    .SendMessage(b, a, $"Hello {a.Name}!")
    .Build();
```

Read more at [docs/sequence-diagram.md](docs/sequence-diagram.md).

## License

This project is licensed under the GNU Affero General Public License v3.0. See the [LICENSE](LICENSE) file for details.

## Credits

Mermaid icon created by [Smashicons on Flaticon](https://www.flaticon.com/authors/smashicons).