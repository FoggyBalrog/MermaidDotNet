using FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.ClassDiagram;

public class ClassDiagramValidationAndSanitizationOptionsTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = true, SanitizeInputs = true };

    [Fact]
    public void SanitizeInputs()
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

        Assert.Contains("L#34;#60;#62;", diagram);
        Assert.Contains("T#123;#125; N#123;#125;", diagram);
        Assert.Contains("rel#58;x", diagram);
        Assert.Contains("note#34;x", diagram);
        Assert.Contains("tip#34;x", diagram);
        Assert.Contains("https://example.com/a%20b", diagram);
    }
}
