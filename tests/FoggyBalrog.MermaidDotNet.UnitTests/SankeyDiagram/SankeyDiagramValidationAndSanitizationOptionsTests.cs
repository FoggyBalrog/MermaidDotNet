namespace FoggyBalrog.MermaidDotNet.UnitTests.SankeyDiagram;

public class SankeyDiagramValidationAndSanitizationOptionsTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = true, SanitizeInputs = true };

    [Fact]
    public void SanitizeInputs()
    {
        string diagram = Mermaid
            .SankeyDiagram(options: _options)
            .AddFlow("A,B", "C,D", 1)
            .Build();

        Assert.Contains("A#44;B,C#44;D,1", diagram);
    }
}
