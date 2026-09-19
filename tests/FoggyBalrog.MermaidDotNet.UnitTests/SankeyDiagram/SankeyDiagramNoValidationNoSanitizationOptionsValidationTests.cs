namespace FoggyBalrog.MermaidDotNet.UnitTests.SankeyDiagram;

public class SankeyDiagramNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void AddFlow_DoesNotThrowIfSourceIsWhiteSpace()
    {
        Mermaid
            .SankeyDiagram(options: _options with { TargetMermaidVersion = MermaidVersion.V11_10 })
            .AddFlow(" ", "B", 10)
            .Build();
    }

    [Fact]
    public void AddFlow_DoesNotThrowIfTargetIsWhiteSpace()
    {
        Mermaid
            .SankeyDiagram(options: _options with { TargetMermaidVersion = MermaidVersion.V11_10 })
            .AddFlow("A", " ", 10)
            .Build();
    }
}
