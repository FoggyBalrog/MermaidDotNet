using FoggyBalrog.MermaidDotNet.KanbanDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.KanbanDiagram;

public class KanbanDiagramNoValidationNoSanitizationOptionsBuilderTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .KanbanDiagram(options: _options)
            .Build();

        Assert.Equal("kanban", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildCompleteDiagram()
    {
        string diagram = Mermaid
            .KanbanDiagram("some title", options: _options)
            .AddColumn("foo", x => x
                .AddTask("t1")
                .AddTask("t2", assigned: "Alice", ticket: "JIRA-123", priority: Priority.VeryHigh))
            .AddColumn("bar", x => x
                .AddTask("t3", assigned: "Alice", priority: Priority.VeryHigh)
                .AddTask("t4", ticket: "JIRA-123"))
            .AddColumn("baz")
            .Build();

        Assert.Equal(@"---
title: some title
---
kanban
    column0[foo]
        task00[t1]
        task01[t2]@{ assigned: 'Alice', ticket: JIRA-123, priority: 'Very High' }
    column1[bar]
        task10[t3]@{ assigned: 'Alice', priority: 'Very High' }
        task11[t4]@{ ticket: JIRA-123 }
    column2[baz]", diagram, ignoreLineEndingDifferences: true);
    }
}
