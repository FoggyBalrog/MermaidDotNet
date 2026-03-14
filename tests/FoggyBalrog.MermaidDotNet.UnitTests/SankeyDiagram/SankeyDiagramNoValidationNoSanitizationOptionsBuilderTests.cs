namespace FoggyBalrog.MermaidDotNet.UnitTests.SankeyDiagram;

public class SankeyDiagramNoValidationNoSanitizationOptionsBuilderTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .SankeyDiagram(options: _options)
            .Build();

        Assert.Equal("sankey", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithFlowsAndEmptyLines()
    {
        string diagram = Mermaid
            .SankeyDiagram(options: _options)
            .AddFlow("A", "B", 10)
            .AddEmptyLine()
            .AddFlow("B", "C", 20)
            .AddFlow("C", "D", 30)
            .Build();

        Assert.Equal(@"sankey
A,B,10

B,C,20
C,D,30", diagram, ignoreLineEndingDifferences: true);
    }
}
