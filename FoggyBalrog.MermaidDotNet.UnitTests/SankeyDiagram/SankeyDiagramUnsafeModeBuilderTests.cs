namespace FoggyBalrog.MermaidDotNet.UnitTests.SankeyDiagram;

public class SankeyDiagramUnsafeModeBuilderTests
{
    [Fact]
    public void CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .Unsafe
            .SankeyDiagram()
            .Build();

        Assert.Equal("sankey", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithFlowsAndEmptyLines()
    {
        string diagram = Mermaid
            .Unsafe
            .SankeyDiagram()
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
