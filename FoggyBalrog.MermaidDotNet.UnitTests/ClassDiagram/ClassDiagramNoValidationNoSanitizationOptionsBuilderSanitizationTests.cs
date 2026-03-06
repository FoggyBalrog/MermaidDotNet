using FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.ClassDiagram;

public class ClassDiagramNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out Class a, label: "L\"<>")
            .AddClass("B", out Class b)
            .AddProperty(a, "T{}", "N{}")
            .AddRelationship(a, b, RelationshipType.Association, label: "rel:x")
            .AddNote("note\"x", a)
            .AddCallback(a, "cb", "tip\"x")
            .AddHyperlink(b, "https://example.com/a b", "tip\"x")
            .Build();

        Assert.Contains("L\"<>", diagram);
        Assert.Contains("T{} N{}", diagram);
        Assert.Contains("rel:x", diagram);
        Assert.Contains("note\"x", diagram);
        Assert.Contains("tip\"x", diagram);
        Assert.Contains("https://example.com/a b", diagram);
        Assert.DoesNotContain("#123;", diagram);
    }
}
