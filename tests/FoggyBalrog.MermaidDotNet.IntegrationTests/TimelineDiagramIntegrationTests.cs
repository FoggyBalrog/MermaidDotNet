namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class TimelineDiagramDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildEmptyTimelineDiagram()
    {
        string diagram = Mermaid
            .TimelineDiagram()
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("timeline", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildEmptyTimelineDiagramWithTitle()
    {
        string diagram = Mermaid
            .TimelineDiagram(title: "Title")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("timeline", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildSimpleTimelineDiagram()
    {
        string diagram = Mermaid
            .TimelineDiagram("Some title")
            .AddEvents("2021", "Event 1", "Event 2")
            .AddEvents("2022", "Event 3")
            .AddEvents("2023", "Event 4", "Event 5", "Event 6")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("timeline", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildTimelineDiagramWithSections()
    {
        string diagram = Mermaid
            .TimelineDiagram("Some title")
            .AddSection("Section 1")
            .AddEvents("2021", "Event 1", "Event 2")
            .AddEvents("2022", "Event 3")
            .AddSection("Section 2")
            .AddEvents("2023", "Event 4", "Event 5", "Event 6")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("timeline", toolingFixture.GetDiagramType(diagramResult));
    }
}

public class TimelineDiagramNoValidationNoSanitizationOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildEmptyTimelineDiagram()
    {
        string diagram = Mermaid
            .TimelineDiagram(options: _options)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("timeline", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildEmptyTimelineDiagramWithTitle()
    {
        string diagram = Mermaid
            .TimelineDiagram(title: "Title", options: _options)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("timeline", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildSimpleTimelineDiagram()
    {
        string diagram = Mermaid
            .TimelineDiagram("Some title", options: _options)
            .AddEvents("2021", "Event 1", "Event 2")
            .AddEvents("2022", "Event 3")
            .AddEvents("2023", "Event 4", "Event 5", "Event 6")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("timeline", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildTimelineDiagramWithSections()
    {
        string diagram = Mermaid
            .TimelineDiagram("Some title", options: _options)
            .AddSection("Section 1")
            .AddEvents("2021", "Event 1", "Event 2")
            .AddEvents("2022", "Event 3")
            .AddSection("Section 2")
            .AddEvents("2023", "Event 4", "Event 5", "Event 6")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("timeline", toolingFixture.GetDiagramType(diagramResult));
    }
}
