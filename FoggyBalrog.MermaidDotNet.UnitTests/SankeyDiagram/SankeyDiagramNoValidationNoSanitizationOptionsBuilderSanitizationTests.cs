namespace FoggyBalrog.MermaidDotNet.UnitTests.SankeyDiagram;

public class SankeyDiagramNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .SankeyDiagram(options: _options)
            .AddFlow("A,B", "C,D", 1)
            .Build();

        Assert.Contains("A,B,C,D,1", diagram);
        Assert.DoesNotContain("#44;", diagram);
    }
}
