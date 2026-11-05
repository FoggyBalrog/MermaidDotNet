namespace FoggyBalrog.MermaidDotNet.UnitTests.MindMap;

public class MindMapValidationAndSanitizationOptionsTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = true, SanitizeInputs = true };

    [Fact]
    public void SanitizeInputs()
    {
        string diagram = Mermaid
            .MindMap("Root:", rootIcon: "fa(", rootClasses: ["a b"], options: _options)
            .AddNode("Child:", out _, icon: "ic(", classes: ["x y"])
            .Build();

        Assert.Contains("Root#58;", diagram);
        Assert.Contains("::icon(fa#40;)", diagram);
        Assert.Contains("::: a#32;b", diagram);
        Assert.Contains("Child#58;", diagram);
        Assert.Contains("::icon(ic#40;)", diagram);
        Assert.Contains("::: x#32;y", diagram);
    }
}
