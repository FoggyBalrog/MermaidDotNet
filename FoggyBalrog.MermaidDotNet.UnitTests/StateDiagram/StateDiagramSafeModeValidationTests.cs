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
    public void StyleWithRawCss_ThrowsIfStateIsForeign()
    {
        Mermaid
            .StateDiagram()
            .AddState("State", out var state);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .StyleWithRawCss(state, "fill: red;");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void StyleWithRawCss_ThrowsIfCssIsWhitespace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddState("State", out var state)
                .StyleWithRawCss(state, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void StyleWithCssClass_ThrowsIfCssClassIsWhitespace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .StateDiagram()
                .AddState("State 1", out var state1)
                .AddState("State 2", out var state2)
                .AddState("State 3", out var state3)
                .StyleWithCssClass(" ", state1, state2, state3);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
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
                .StyleWithCssClass("css-class", state1, state2, state3);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }
}
