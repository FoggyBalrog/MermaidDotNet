namespace FoggyBalrog.MermaidDotNet.UnitTests.BlockDiagram;

public class BlockDiagramNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void AddBlock_DoesNotThrowIfWidthIsStrictlyNegative()
    {
        Mermaid
            .BlockDiagram(options: _options with { TargetMermaidVersion = MermaidVersion.V11_10 })
            .AddBlock("foo", out _, -1)
            .Build();
    }

    [Fact]
    public void AddCompositeBlock_DoesNotThrowIfWidthIsStrictlyNegative()
    {
        Mermaid
            .BlockDiagram(options: _options with { TargetMermaidVersion = MermaidVersion.V11_10 })
            .AddCompositeBlock(b => {}, width: -1)
            .Build();
    }

    [Fact]
    public void StyleBlock_DoesNotThrowIfCssIsWhitespace()
    {
        Mermaid
            .BlockDiagram(options: _options with { TargetMermaidVersion = MermaidVersion.V11_10 })
            .AddBlock("foo", out var b)
            .StyleBlock(b, " ")
            .Build();
    }
}
