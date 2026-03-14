namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class UserJourneyDiagramDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildSimpleDiagramWithoutTitle()
    {
        string diagram = Mermaid
            .UserJourneyDiagram()
            .AddTask("Task 1", 1, "Actor 1", "Actor 2")
            .AddTask("Task 2", 2)
            .AddSection("Section 1")
            .AddTask("Task 3", 3)
            .AddTask("Task 4", 4, "Actor 3")
            .AddSection("Section 2")
            .AddTask("Task 5", 5, "Actor 1", "Actor 3")
            .AddTask("Task 6", 6, "Actor 2")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("journey", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildSimpleDiagramWithTitle()
    {
        string diagram = Mermaid
            .UserJourneyDiagram("My title")
            .AddTask("Task 1", 1, "Actor 1", "Actor 2")
            .AddTask("Task 2", 2)
            .AddSection("Section 1")
            .AddTask("Task 3", 3)
            .AddTask("Task 4", 4, "Actor 3")
            .AddSection("Section 2")
            .AddTask("Task 5", 5, "Actor 1", "Actor 3")
            .AddTask("Task 6", 6, "Actor 2")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("journey", toolingFixture.GetDiagramType(diagramResult));
    }
}

public class UserJourneyDiagramNoValidationNoSanitizationOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildSimpleDiagramWithoutTitle()
    {
        string diagram = Mermaid
            .UserJourneyDiagram(options: _options)
            .AddTask("Task 1", 1, "Actor 1", "Actor 2")
            .AddTask("Task 2", 2)
            .AddSection("Section 1")
            .AddTask("Task 3", 3)
            .AddTask("Task 4", 4, "Actor 3")
            .AddSection("Section 2")
            .AddTask("Task 5", 5, "Actor 1", "Actor 3")
            .AddTask("Task 6", 6, "Actor 2")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("journey", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildSimpleDiagramWithTitle()
    {
        string diagram = Mermaid
            .UserJourneyDiagram("My title", options: _options)
            .AddTask("Task 1", 1, "Actor 1", "Actor 2")
            .AddTask("Task 2", 2)
            .AddSection("Section 1")
            .AddTask("Task 3", 3)
            .AddTask("Task 4", 4, "Actor 3")
            .AddSection("Section 2")
            .AddTask("Task 5", 5, "Actor 1", "Actor 3")
            .AddTask("Task 6", 6, "Actor 2")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("journey", toolingFixture.GetDiagramType(diagramResult));
    }
}
