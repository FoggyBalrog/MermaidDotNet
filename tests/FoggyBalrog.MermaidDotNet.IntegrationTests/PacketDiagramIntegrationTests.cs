namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class PacketDiagramDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .PacketDiagram(options: new MermaidDotNetOptions { TargetMermaidVersion = MermaidVersion.V11_9 })
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("packet", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithEndFieldFirst()
    {
        string diagram = Mermaid
            .PacketDiagram("some title", options: new MermaidDotNetOptions { TargetMermaidVersion = MermaidVersion.V11_9 })
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
            .PacketDiagram("some title", options: new MermaidDotNetOptions { TargetMermaidVersion = MermaidVersion.V11_9 })
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
            .PacketDiagram(options: _options with { TargetMermaidVersion = MermaidVersion.V11_9 })
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("packet", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithEndFieldFirst()
    {
        string diagram = Mermaid
            .PacketDiagram("some title", options: _options with { TargetMermaidVersion = MermaidVersion.V11_9 })
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
            .PacketDiagram("some title", options: _options with { TargetMermaidVersion = MermaidVersion.V11_9 })
            .AddFieldWithBits(5, "foo")
            .AddFieldWithEnd(10, "bar")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("packet", toolingFixture.GetDiagramType(diagramResult));
    }
}
