using FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.ClassDiagram;

public class ClassDiagramBuilderNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void ClassDiagram_DoesNotThrowIfWhiteSpaceTitle()
    {
        Mermaid
            .ClassDiagram(" ", options: _options)
            .Build();
    }

    [Fact]
    public void AddClass_DoesNotThrowIfWhiteSpaceName()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass(" ", out _)
            .Build();
    }

    [Fact]
    public void AddClass_DoesNotThrowIfWhiteSpaceLabel()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out _, label: " ")
            .Build();
    }

    [Fact]
    public void AddClass_DoesNotThrowIfWhiteSpaceAnnotation()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out _, annotation: " ")
            .Build();
    }

    [Fact]
    public void AddNamespace_DoesNotThrowIfWhiteSpaceName()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddNamespace(" ", _ => { })
            .Build();
    }

    [Fact]
    public void AddProperty_DoesNotThrowIfForeignClass()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a);

        Mermaid
            .ClassDiagram(options: _options)
            .AddProperty(a, "B", "C")
            .Build();
    }

    [Fact]
    public void AddProperty_DoesNotThrowIfWhiteSpaceType()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a)
            .AddProperty(a, " ", "C")
            .Build();
    }

    [Fact]
    public void AddProperty_DoesNotThrowIfWhiteSpaceName()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a)
            .AddProperty(a, "B", " ")
            .Build();
    }

    [Fact]
    public void AddMethod_DoesNotThrowIfForeignClass()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a);

        Mermaid
            .ClassDiagram(options: _options)
            .AddMethod(a, "B", "C")
            .Build();
    }

    [Fact]
    public void AddMethod_DoesNotThrowIfWhiteSpaceReturnType()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a)
            .AddMethod(a, " ", "C")
            .Build();
    }

    [Fact]
    public void AddMethod_DoesNotThrowIfWhiteSpaceName()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a)
            .AddMethod(a, "B", " ")
            .Build();
    }

    [Fact]
    public void AddMethod_DoesNotThrowIfWhiteSpaceParameterType()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a)
            .AddMethod(a, "B", "C", parameters: new[] { (" ", "D") })
            .Build();
    }

    [Fact]
    public void AddMethod_DoesNotThrowIfWhiteSpaceParameterName()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a)
            .AddMethod(a, "B", "C", parameters: new[] { ("D", " ") })
            .Build();
    }

    [Fact]
    public void AddRelationship_DoesNotThrowIfForeignFromClass()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a);

        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("B", out var b)
            .AddRelationship(a, b, RelationshipType.Association)
            .Build();
    }

    [Fact]
    public void AddRelationship_DoesNotThrowIfForeignToClass()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("B", out var b);

        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a)
            .AddRelationship(a, b, RelationshipType.Association)
            .Build();
    }

    [Fact]
    public void AddRelationship_DoesNotThrowIfWhitespaceLabel()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a)
            .AddClass("B", out var b)
            .AddRelationship(a, b, RelationshipType.Inheritance, label: " ")
            .Build();
    }

    [Fact]
    public void AddNote_DoesNotThrowIfWhiteSpaceText()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a)
            .AddNote(" ", a)
            .Build();
    }

    [Fact]
    public void AddNote_DoesNotThrowIfForeignClass()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a);

        Mermaid
            .ClassDiagram(options: _options)
            .AddNote("foo", a)
            .Build();
    }

    [Fact]
    public void AddCallback_DoesNotThrowIfForeignClass()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a);

        Mermaid
            .ClassDiagram(options: _options)
            .AddCallback(a, "foo")
            .Build();
    }

    [Fact]
    public void AddCallback_DoesNotThrowIfWhiteSpaceName()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a)
            .AddCallback(a, " ")
            .Build();
    }

    [Fact]
    public void AddCallback_DoesNotThrowIfWhitespaceTooltip()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a)
            .AddCallback(a, "foo", " ")
            .Build();
    }

    [Fact]
    public void AddHyperlink_DoesNotThrowIfForeignClass()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a);

        Mermaid
            .ClassDiagram(options: _options)
            .AddHyperlink(a, "foo", "bar")
            .Build();
    }

    [Fact]
    public void AddHyperlink_DoesNotThrowIfWhiteSpaceUri()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a)
            .AddHyperlink(a, " ", "bar")
            .Build();
    }

    [Fact]
    public void AddHyperlink_DoesNotThrowIfWhiteSpaceTooltip()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a)
            .AddHyperlink(a, "foo", " ")
            .Build();
    }

    [Fact]
    public void StyleWithRawCss_DoesNotThrowIfForeignClass()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a);

        Mermaid
            .ClassDiagram(options: _options)
            .StyleWithRawCss(a, "foo")
            .Build();
    }

    [Fact]
    public void StyleWithRawCss_DoesNotThrowIfWhiteSpaceCss()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a)
            .StyleWithRawCss(a, " ")
            .Build();
    }

    [Fact]
    public void StyleWithCssClass_DoesNotThrowIfWhiteSpaceCssClass()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a)
            .StyleWithCssClass(" ", a)
            .Build();
    }

    [Fact]
    public void StyleWithCssClass_DoesNotThrowIfForeignClass()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("B", out var b);

        Mermaid
            .ClassDiagram(options: _options)
            .AddClass("A", out var a)
            .AddClass("C", out var c)
            .StyleWithCssClass("foo", a, b, c)
            .Build();
    }

    [Fact]
    public void StyleWithCssClass_DoesNotThrowIfEmptyClasses()
    {
        Mermaid
            .ClassDiagram(options: _options)
            .StyleWithCssClass("foo")
            .Build();
    }
}
