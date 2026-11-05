namespace FoggyBalrog.MermaidDotNet.UnitTests.BlockDiagram;

public class BlockDiagramValidationAndSanitizationOptionsTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = true, SanitizeInputs = true };

    [Fact]
    public void SanitizeInputs()
    {
        string diagram = Mermaid
            .BlockDiagram(options: _options)
            .AddBlock("f\r\no\"o", out var foo)
            .AddBlock("b\nar", out var bar)
            .AddLink(foo, bar, "q\nu\"x")
            .Build();

        Assert.Contains("f<br/>o#34;o", diagram);
        Assert.Contains("b<br/>ar", diagram);
        Assert.Contains("q<br/>u#34;x", diagram);
    }
}
