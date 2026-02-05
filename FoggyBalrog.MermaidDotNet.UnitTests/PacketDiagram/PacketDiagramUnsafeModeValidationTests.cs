namespace FoggyBalrog.MermaidDotNet.UnitTests.PacketDiagram;

public class PacketDiagramUnsafeModeValidationTests
{
    [Fact]
    public void PacketDiagramBuilder_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .PacketDiagram(title: " ");
    }

    [Fact]
    public void AddFieldWithEnd_DoesNotThrowIfValueIsStrictlyNegative()
    {
        Mermaid
            .Unsafe
            .PacketDiagram()
            .AddFieldWithEnd(-1)
            .Build();
    }

    [Fact]
    public void AddFieldWithBits_DoesNotThrowIfValueIsStrictlyNegative()
    {
        Mermaid
            .Unsafe
            .PacketDiagram()
            .AddFieldWithBits(-1)
            .Build();
    }
}