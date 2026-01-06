namespace FoggyBalrog.MermaidDotNet.UnitTests.SankeyDiagram;

public class SankeyDiagramUnsafeModeValidationTests
{
    [Fact]
    public void AddFlow_DoesNotThrowIfSourceIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SankeyDiagram()
            .AddFlow(" ", "B", 10)
            .Build();
    }

    [Fact]
    public void AddFlow_DoesNotThrowIfTargetIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SankeyDiagram()
            .AddFlow("A", " ", 10)
            .Build();
    }
}