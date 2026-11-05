namespace FoggyBalrog.MermaidDotNet.UnitTests.PacketDiagram;

public class PacketDiagramNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .PacketDiagram(options: _options)
            .AddFieldWithEnd(1, "a\\b")
            .Build();

        Assert.Contains("0-1: \"a\\b\"", diagram);
        Assert.DoesNotContain("#92;", diagram);
    }
}
