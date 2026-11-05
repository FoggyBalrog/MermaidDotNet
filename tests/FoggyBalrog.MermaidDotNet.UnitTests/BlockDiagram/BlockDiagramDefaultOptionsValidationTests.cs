namespace FoggyBalrog.MermaidDotNet.UnitTests.BlockDiagram;

public class BlockDiagramDefaultOptionsValidationTests
{
    [Fact]
    public void AddBlock_ThrowsIfWidthIsStrictlyNegative()
    {
        var ex = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .BlockDiagram()
                .AddBlock("foo", out _, -1);
        });

        Assert.Equal(MermaidExceptionReason.StrictlyNegative, ex.Reason);
    }

    [Fact]
    public void AddCompositeBlock_ThrowsIfWidthIsStrictlyNegative()
    {
        var ex = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .BlockDiagram()
                .AddCompositeBlock(b => {}, width: -1);
        });

        Assert.Equal(MermaidExceptionReason.StrictlyNegative, ex.Reason);
    }

    [Fact]
    public void StyleBlock_ThrowsIfCssIsWhitespace()
    {
        var ex = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .BlockDiagram()
                .AddBlock("foo", out var b)
                .StyleBlock(b, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, ex.Reason);
    }
}
