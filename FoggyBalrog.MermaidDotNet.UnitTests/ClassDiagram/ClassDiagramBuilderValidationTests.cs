using FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.ClassDiagram;

public class ClassDiagramBuilderValidationTests
{
    [Fact]
    public void ClassDiagram_ThrowsIfWhiteSpaceTitle()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram(" ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddClass_ThrowsIfWhiteSpaceName()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass(" ", out _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddClass_ThrowsIfWhiteSpaceLabel()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out _, label: " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddClass_ThrowsIfWhiteSpaceAnnotation()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out _, annotation: " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddNamespace_ThrowsIfWhiteSpaceName()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddNamespace(" ", _ => { });
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddProperty_ThrowsIfForeignClass()
    {
        Mermaid
            .ClassDiagram()
            .AddClass("A", out var a);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddProperty(a, "B", "C");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddProperty_ThrowsIfWhiteSpaceType()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out var a)
                .AddProperty(a, " ", "C");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddProperty_ThrowsIfWhiteSpaceName()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out var a)
                .AddProperty(a, "B", " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddMethod_ThrowsIfForeignClass()
    {
        Mermaid
            .ClassDiagram()
            .AddClass("A", out var a);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddMethod(a, "B", "C");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddMethod_ThrowsIfWhiteSpaceReturnType()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out var a)
                .AddMethod(a, " ", "C");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddMethod_ThrowsIfWhiteSpaceName()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out var a)
                .AddMethod(a, "B", " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddMethod_ThrowsIfWhiteSpaceParameterType()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out var a)
                .AddMethod(a, "B", "C", parameters: new[] { (" ", "D") });
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddMethod_ThrowsIfWhiteSpaceParameterName()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out var a)
                .AddMethod(a, "B", "C", parameters: new[] { ("D", " ") });
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddRelationship_ThrowsIfForeignFromClass()
    {
        Mermaid
            .ClassDiagram()
            .AddClass("A", out var a);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("B", out var b)
                .AddRelationship(a, b, RelationshipType.Association);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddRelationship_ThrowsIfForeignToClass()
    {
        Mermaid
            .ClassDiagram()
            .AddClass("B", out var b);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out var a)
                .AddRelationship(a, b, RelationshipType.Association);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddRelationship_ThrowsIfWhitespaceLabel()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out var a)
                .AddClass("B", out var b)
                .AddRelationship(a, b, RelationshipType.Inheritance, label: " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddNote_ThrowsIfWhiteSpaceText()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out var a)
                .AddNote(" ", a);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddNote_ThrowsIfForeignClass()
    {
        Mermaid
            .ClassDiagram()
            .AddClass("A", out var a);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddNote("foo", a);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddCallback_ThrowsIfForeignClass()
    {
        Mermaid
            .ClassDiagram()
            .AddClass("A", out var a);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddCallback(a, "foo");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddCallback_ThrowsIfWhiteSpaceName()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out var a)
                .AddCallback(a, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddCallback_ThrowsIfWhitespaceTooltip()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out var a)
                .AddCallback(a, "foo", " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddHyperlink_ThrowsIfForeignClass()
    {
        Mermaid
            .ClassDiagram()
            .AddClass("A", out var a);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddHyperlink(a, "foo", "bar");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddHyperlink_ThrowsIfWhiteSpaceUri()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out var a)
                .AddHyperlink(a, " ", "bar");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddHyperlink_ThrowsIfWhiteSpaceTooltip()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out var a)
                .AddHyperlink(a, "foo", " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void StyleWithRawCss_ThrowsIfForeignClass()
    {
        Mermaid
            .ClassDiagram()
            .AddClass("A", out var a);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .StyleWithRawCss(a, "foo");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void StyleWithRawCss_ThrowsIfWhiteSpaceCss()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out var a)
                .StyleWithRawCss(a, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void StyleWithCssClass_ThrowsIfWhiteSpaceCssClass()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out var a)
                .StyleWithCssClass(" ", a);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void StyleWithCssClass_ThrowsIfForeignClass()
    {
        Mermaid
            .ClassDiagram()
            .AddClass("B", out var b);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .AddClass("A", out var a)
                .AddClass("C", out var c)
                .StyleWithCssClass("foo", a, b, c);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void StyleWithCssClass_ThrowsIfEmptyClasses()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .ClassDiagram()
                .StyleWithCssClass("foo");
        });

        Assert.Equal(MermaidExceptionReason.EmptyCollection, exception.Reason);
    }
}
