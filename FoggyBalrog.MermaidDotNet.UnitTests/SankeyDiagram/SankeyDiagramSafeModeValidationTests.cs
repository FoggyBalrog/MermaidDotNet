namespace FoggyBalrog.MermaidDotNet.UnitTests.SankeyDiagram;

public class SankeyDiagramSafeModeValidationTests
{
    [Fact]
    public void AddFlow_ThrowsIfSourceIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SankeyDiagram()
                .AddFlow(" ", "B", 10);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddFlow_ThrowsIfTargetIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SankeyDiagram()
                .AddFlow("A", " ", 10);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }
}
