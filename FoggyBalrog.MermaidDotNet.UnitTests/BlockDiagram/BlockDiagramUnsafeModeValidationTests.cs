namespace FoggyBalrog.MermaidDotNet.UnitTests.BlockDiagram;

public class BlockDiagramUnsafeModeValidationTests
{
    [Fact]
    public void AddBlock_DoesNotThrowIfWidthIsStrictlyNegative()
    {
        Mermaid
            .Unsafe
            .BlockDiagram()
            .AddBlock("foo", out _, -1)
            .Build();
    }

    [Fact]
    public void AddCompositeBlock_DoesNotThrowIfWidthIsStrictlyNegative()
    {
        Mermaid
            .Unsafe
            .BlockDiagram()
            .AddCompositeBlock(b => {}, width: -1)
            .Build();
    }

    [Fact]
    public void StyleBlock_DoesNotThrowIfCssIsWhitespace()
    {
        Mermaid
            .Unsafe
            .BlockDiagram()
            .AddBlock("foo", out var b)
            .StyleBlock(b, " ")
            .Build();
    }
}