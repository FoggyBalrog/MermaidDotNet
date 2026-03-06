namespace FoggyBalrog.MermaidDotNet.UnitTests.PacketDiagram;

public class PacketDiagramValidationAndSanitizationOptionsTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = true, SanitizeInputs = true };

    [Fact]
    public void SanitizeInputs()
    {
        string diagram = Mermaid
            .PacketDiagram(options: _options)
            .AddFieldWithEnd(1, "a\\b")
            .Build();

        Assert.Contains("0-1: \"a#92;b\"", diagram);
    }
}
