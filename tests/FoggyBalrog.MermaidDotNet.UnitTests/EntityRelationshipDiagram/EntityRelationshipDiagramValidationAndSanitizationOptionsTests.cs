using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class EntityRelationshipDiagramValidationAndSanitizationOptionsTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = true, SanitizeInputs = true };

    [Fact]
    public void SanitizeInputs()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram(options: _options)
            .AddEntity("E!?,", out Entity e1, ("int", "id", EntityAttributeKeys.Primary, "c\"\\"))
            .AddEntity("Other", out Entity e2)
            .AddRelationship(Cardinality.ExactlyOne, e1, Cardinality.ExactlyOne, e2, "rel\"\\")
            .Build();

        Assert.Contains("E#33;#63;#44;", diagram);
        Assert.Contains("c#34;#92;", diagram);
        Assert.Contains("rel#34;#92;", diagram);
    }
}
