using FoggyBalrog.MermaidDotNet.KanbanDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class KanbanDiagramDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildCompleteDiagram()
    {
        string diagram = Mermaid
            .KanbanDiagram("some title")
            .AddColumn("foo", x => x
                .AddTask("t1")
                .AddTask("t2", assigned: "Alice", ticket: "JIRA-123", priority: Priority.VeryHigh))
            .AddColumn("bar", x => x
                .AddTask("t3", assigned: "Alice", priority: Priority.VeryHigh)
                .AddTask("t4", ticket: "JIRA-123"))
            .AddColumn("baz")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("kanban", toolingFixture.GetDiagramType(diagramResult));
    }
}

public class KanbanDiagramNoValidationNoSanitizationOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildCompleteDiagram()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("kanban", toolingFixture.GetDiagramType(diagramResult));
    }
}
