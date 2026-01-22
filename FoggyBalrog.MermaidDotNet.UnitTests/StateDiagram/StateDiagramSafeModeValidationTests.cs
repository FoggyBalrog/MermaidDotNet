using FoggyBalrog.MermaidDotNet.StateDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.StateDiagram;

public class StateDiagramSafeModeValidationTests
{
    [Fact]
    public void StateDiagramBuilder_ThrowsIfTitleIsWhitespace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram(" ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddState_ThrowsIfDescriptionIsWhitespace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddState(" ", out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddCompositeState_ThrowsIfDescriptionIsWhitespace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddCompositeState(" ", out var _, _ =>
                {
                });
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddStateLink_ThrowsIfStateIsForeign()
    {
        Mermaid
            .StateDiagram()
            .AddState("State", out var state);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddStateLink(state, "http://example.com");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddStateLink_ThrowsIfUrlIsWhitespace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddState("State", out var state)
                .AddStateLink(state, " ");
        });
        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddStateLink_ThrowsIfTooltipIsWhitespace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddState("State", out var state)
                .AddStateLink(state, "http://example.com", " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddNote_ThrowsIfStateIsForeign()
    {
        Mermaid
            .StateDiagram()
            .AddState("State", out var state);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddNote(state, NotePosition.Right, "Note");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddNote_ThrowsIfTextIsWhitespace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddState("State", out var state)
                .AddNote(state, NotePosition.Right, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddStateTransition_ThrowsIfFromStateIsForeign()
    {
        Mermaid
            .StateDiagram()
            .AddState("From", out var from);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddState("To", out var to)
                .AddStateTransition(from, to);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddStateTransition_ThrowsIfToStateIsForeign()
    {
        Mermaid
            .StateDiagram()
            .AddState("To", out var to);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddState("From", out var from)
                .AddStateTransition(from, to);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddStateTransition_ThrowsIfDescriptionIsWhitespace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddState("From", out var from)
                .AddState("To", out var to)
                .AddStateTransition(from, to, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddTransitionFromStart_ThrowsIfToStateIsForeign()
    {
        Mermaid
            .StateDiagram()
            .AddState("To", out var to);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddTransitionFromStart(to);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddTransitionFromStart_ThrowsIfDescriptionIsWhitespace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddState("To", out var to)
                .AddTransitionFromStart(to, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddTransitionToEnd_ThrowsIfFromStateIsForeign()
    {
        Mermaid
            .StateDiagram()
            .AddState("From", out var from);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddTransitionToEnd(from);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddTransitionToEnd_ThrowsIfDescriptionIsWhitespace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddState("From", out var from)
                .AddTransitionToEnd(from, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddConcurrency_ThrowsIfDescriptionIsWhitespace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddConcurrency(" ", out var _, _ =>
                {
                });
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void StyleWithCssClass_ThrowsIfCssClassIsForeign()
    {
        Mermaid
            .StateDiagram()
            .DefineCssClass("css-class", "color: red;", out var cssClass);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddState("State 1", out var state1)
                .AddState("State 2", out var state2)
                .AddState("State 3", out var state3)
                .StyleWithCssClass(cssClass, state1, state2, state3);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void StyleWithCssClass_ThrowsIfStateIsForeign()
    {
        Mermaid
            .StateDiagram()
            .AddState("State 2", out var state2);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddState("State 1", out var state1)
                .AddState("State 3", out var state3)
                .DefineCssClass("css-class", "color: red;", out var cssClass)
                .StyleWithCssClass(cssClass, state1, state2, state3);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }
}
