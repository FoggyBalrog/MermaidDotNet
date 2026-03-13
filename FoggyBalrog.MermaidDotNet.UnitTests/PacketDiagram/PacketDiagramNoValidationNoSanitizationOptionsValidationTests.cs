namespace FoggyBalrog.MermaidDotNet.UnitTests.PacketDiagram;

public class PacketDiagramNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void PacketDiagramBuilder_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .PacketDiagram(title: " ", options: _options);
    }

    [Fact]
    public void AddFieldWithEnd_DoesNotThrowIfValueIsStrictlyNegative()
    {
        Mermaid
            .PacketDiagram(options: _options)
            .AddFieldWithEnd(-1)
            .Build();
    }

    [Fact]
    public void AddFieldWithBits_DoesNotThrowIfValueIsStrictlyNegative()
    {
        Mermaid
            .PacketDiagram(options: _options)
            .AddFieldWithBits(-1)
            .Build();
    }
}
