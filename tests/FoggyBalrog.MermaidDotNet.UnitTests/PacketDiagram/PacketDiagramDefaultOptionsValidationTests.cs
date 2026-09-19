namespace FoggyBalrog.MermaidDotNet.UnitTests.PacketDiagram;

public class PacketDiagramDefaultOptionsValidationTests
{
    [Fact]
    public void PacketDiagramBuilder_ThrowsIfTitleIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid.PacketDiagram(title: " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddFieldWithEnd_ThrowsIfValueIsStrictlyNegative()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .PacketDiagram()
                .AddFieldWithEnd(-1);
        });

        Assert.Equal(MermaidExceptionReason.StrictlyNegative, exception.Reason);
    }

    [Fact]
    public void AddFieldWithBits_ThrowsIfValueIsStrictlyNegative()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .PacketDiagram(options: new MermaidDotNetOptions { TargetMermaidVersion = MermaidVersion.V11_7 })
                .AddFieldWithBits(-1);
        });

        Assert.Equal(MermaidExceptionReason.StrictlyNegative, exception.Reason);
    }
}