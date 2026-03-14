using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class EntityRelationshipDiagramNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .EntityRelationshipDiagram(options: _options)
            .AddEntity("E!?,", out Entity e1, ("int", "id", EntityAttributeKeys.Primary, "c\"\\"))
            .AddEntity("Other", out Entity e2)
            .AddRelationship(Cardinality.ExactlyOne, e1, Cardinality.ExactlyOne, e2, "rel\"\\")
            .Build();

        Assert.Contains("E!?,", diagram);
        Assert.Contains("c\"\\", diagram);
        Assert.Contains("rel\"\\", diagram);
        Assert.DoesNotContain("#33;", diagram);
    }
}
