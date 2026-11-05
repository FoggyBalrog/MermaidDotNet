namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class PacketDiagramDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .PacketDiagram()
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("packet", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithEndFieldFirst()
    {
        string diagram = Mermaid
            .PacketDiagram("some title")
            .AddFieldWithEnd(10, "foo")
            .AddFieldWithBits(5, "bar")
            .AddFieldWithEnd(25, "baz")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("packet", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithBitsFieldFirst()
    {
        string diagram = Mermaid
            .PacketDiagram("some title")
            .AddFieldWithBits(5, "foo")
            .AddFieldWithEnd(10, "bar")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("packet", toolingFixture.GetDiagramType(diagramResult));
    }
}

public class PacketDiagramNoValidationNoSanitizationOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .PacketDiagram(options: _options)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("packet", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithEndFieldFirst()
    {
        string diagram = Mermaid
            .PacketDiagram("some title", options: _options)
            .AddFieldWithEnd(10, "foo")
            .AddFieldWithBits(5, "bar")
            .AddFieldWithEnd(25, "baz")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("packet", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithBitsFieldFirst()
    {
        string diagram = Mermaid
            .PacketDiagram("some title", options: _options)
            .AddFieldWithBits(5, "foo")
            .AddFieldWithEnd(10, "bar")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("packet", toolingFixture.GetDiagramType(diagramResult));
    }
}
