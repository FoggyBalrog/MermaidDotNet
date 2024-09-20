using FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.ClassDiagram;

public class ClassDiagramBuilderUnsafeModeValidationTests
{
    [Fact]
    public void ClassDiagram_DoesNotThrowIfWhiteSpaceTitle()
    {
        Mermaid
            .Unsafe
            .ClassDiagram(" ")
            .Build();
    }

    [Fact]
    public void AddClass_DoesNotThrowIfWhiteSpaceName()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass(" ", out _)
            .Build();
    }

    [Fact]
    public void AddClass_DoesNotThrowIfWhiteSpaceLabel()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out _, label: " ")
            .Build();
    }

    [Fact]
    public void AddClass_DoesNotThrowIfWhiteSpaceAnnotation()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out _, annotation: " ")
            .Build();
    }

    [Fact]
    public void AddNamespace_DoesNotThrowIfWhiteSpaceName()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddNamespace(" ", _ => { })
            .Build();
    }

    [Fact]
    public void AddProperty_DoesNotThrowIfForeignClass()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a);

        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddProperty(a, "B", "C")
            .Build();
    }

    [Fact]
    public void AddProperty_DoesNotThrowIfWhiteSpaceType()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a)
            .AddProperty(a, " ", "C")
            .Build();
    }

    [Fact]
    public void AddProperty_DoesNotThrowIfWhiteSpaceName()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a)
            .AddProperty(a, "B", " ")
            .Build();
    }

    [Fact]
    public void AddMethod_DoesNotThrowIfForeignClass()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a);

        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddMethod(a, "B", "C")
            .Build();
    }

    [Fact]
    public void AddMethod_DoesNotThrowIfWhiteSpaceReturnType()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a)
            .AddMethod(a, " ", "C")
            .Build();
    }

    [Fact]
    public void AddMethod_DoesNotThrowIfWhiteSpaceName()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a)
            .AddMethod(a, "B", " ")
            .Build();
    }

    [Fact]
    public void AddMethod_DoesNotThrowIfWhiteSpaceParameterType()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a)
            .AddMethod(a, "B", "C", parameters: new[] { (" ", "D") })
            .Build();
    }

    [Fact]
    public void AddMethod_DoesNotThrowIfWhiteSpaceParameterName()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a)
            .AddMethod(a, "B", "C", parameters: new[] { ("D", " ") })
            .Build();
    }

    [Fact]
    public void AddRelationship_DoesNotThrowIfForeignFromClass()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a);

        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("B", out var b)
            .AddRelationship(a, b, RelationshipType.Association)
            .Build();
    }

    [Fact]
    public void AddRelationship_DoesNotThrowIfForeignToClass()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("B", out var b);

        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a)
            .AddRelationship(a, b, RelationshipType.Association)
            .Build();
    }

    [Fact]
    public void AddRelationship_DoesNotThrowIfWhitespaceLabel()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a)
            .AddClass("B", out var b)
            .AddRelationship(a, b, RelationshipType.Inheritance, label: " ")
            .Build();
    }

    [Fact]
    public void AddNote_DoesNotThrowIfWhiteSpaceText()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a)
            .AddNote(" ", a)
            .Build();
    }

    [Fact]
    public void AddNote_DoesNotThrowIfForeignClass()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a);

        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddNote("foo", a)
            .Build();
    }

    [Fact]
    public void AddCallback_DoesNotThrowIfForeignClass()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a);

        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddCallback(a, "foo")
            .Build();
    }

    [Fact]
    public void AddCallback_DoesNotThrowIfWhiteSpaceName()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a)
            .AddCallback(a, " ")
            .Build();
    }

    [Fact]
    public void AddCallback_DoesNotThrowIfWhitespaceTooltip()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a)
            .AddCallback(a, "foo", " ")
            .Build();
    }

    [Fact]
    public void AddHyperlink_DoesNotThrowIfForeignClass()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a);

        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddHyperlink(a, "foo", "bar")
            .Build();
    }

    [Fact]
    public void AddHyperlink_DoesNotThrowIfWhiteSpaceUri()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a)
            .AddHyperlink(a, " ", "bar")
            .Build();
    }

    [Fact]
    public void AddHyperlink_DoesNotThrowIfWhiteSpaceTooltip()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a)
            .AddHyperlink(a, "foo", " ")
            .Build();
    }

    [Fact]
    public void StyleWithRawCss_DoesNotThrowIfForeignClass()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a);

        Mermaid
            .Unsafe
            .ClassDiagram()
            .StyleWithRawCss(a, "foo")
            .Build();
    }

    [Fact]
    public void StyleWithRawCss_DoesNotThrowIfWhiteSpaceCss()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a)
            .StyleWithRawCss(a, " ")
            .Build();
    }

    [Fact]
    public void StyleWithCssClass_DoesNotThrowIfWhiteSpaceCssClass()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a)
            .StyleWithCssClass(" ", a)
            .Build();
    }

    [Fact]
    public void StyleWithCssClass_DoesNotThrowIfForeignClass()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("B", out var b);

        Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("A", out var a)
            .AddClass("C", out var c)
            .StyleWithCssClass("foo", a, b, c)
            .Build();
    }

    [Fact]
    public void StyleWithCssClass_DoesNotThrowIfEmptyClasses()
    {
        Mermaid
            .Unsafe
            .ClassDiagram()
            .StyleWithCssClass("foo")
            .Build();
    }
}
