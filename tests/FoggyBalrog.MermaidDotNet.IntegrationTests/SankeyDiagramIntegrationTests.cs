using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class SankeyDiagramDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildDiagramWithFlowsAndEmptyLines()
    {
        string diagram = Mermaid
            .SankeyDiagram()
            .AddFlow("A", "B", 10)
            .AddEmptyLine()
            .AddFlow("B", "C", 20)
            .AddFlow("C", "D", 30)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sankey", toolingFixture.GetDiagramType(diagramResult));
    }
}

public class SankeyDiagramNoValidationNoSanitizationOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildDiagramWithFlowsAndEmptyLines()
    {
        string diagram = Mermaid
            .SankeyDiagram(options: _options)
            .AddFlow("A", "B", 10)
            .AddEmptyLine()
            .AddFlow("B", "C", 20)
            .AddFlow("C", "D", 30)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sankey", toolingFixture.GetDiagramType(diagramResult));
    }
}
