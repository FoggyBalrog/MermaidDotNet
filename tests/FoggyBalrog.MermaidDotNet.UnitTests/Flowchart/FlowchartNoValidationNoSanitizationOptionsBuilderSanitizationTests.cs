using FoggyBalrog.MermaidDotNet.Flowchart.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.Flowchart;

public class FlowchartNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .Flowchart(options: _options)
            .AddNode("N\"1\\", out Node n1)
            .AddNode("N2", out Node n2)
            .AddLink(n1, n2, out _, "L\"1\\")
            .AddHyperlink(n1, "https://example.com/a b", "T\"1\\")
            .AddCallback(n2, "cb", "Tip\"2\\")
            .Build();

        Assert.Contains("N\"1\\", diagram);
        Assert.Contains("L\"1\\", diagram);
        Assert.Contains("T\"1\\", diagram);
        Assert.Contains("Tip\"2\\", diagram);
        Assert.Contains("https://example.com/a b", diagram);
        Assert.DoesNotContain("#34;", diagram);
    }
}
