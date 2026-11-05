namespace FoggyBalrog.MermaidDotNet.UnitTests.BlockDiagram;

public class BlockDiagramNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .BlockDiagram(options: _options)
            .AddBlock("f\r\no\"o", out var foo)
            .AddBlock("b\nar", out var bar)
            .AddLink(foo, bar, "q\nu\"x")
            .Build();

        Assert.Contains("f\r\no\"o", diagram);
        Assert.Contains("b\nar", diagram);
        Assert.Contains("q\nu\"x", diagram);
        Assert.DoesNotContain("#34;", diagram);
    }
}
