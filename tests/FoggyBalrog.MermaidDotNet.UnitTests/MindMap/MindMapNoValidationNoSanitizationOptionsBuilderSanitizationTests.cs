namespace FoggyBalrog.MermaidDotNet.UnitTests.MindMap;

public class MindMapNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .MindMap("Root:", rootIcon: "fa(", rootClasses: ["a b"], options: _options)
            .AddNode("Child:", out _, icon: "ic(", classes: ["x y"])
            .Build();

        Assert.Contains("Root:", diagram);
        Assert.Contains("::icon(fa()", diagram);
        Assert.Contains("::: a b", diagram);
        Assert.Contains("Child:", diagram);
        Assert.Contains("::icon(ic()", diagram);
        Assert.Contains("::: x y", diagram);
        Assert.DoesNotContain("#58;", diagram);
    }
}
