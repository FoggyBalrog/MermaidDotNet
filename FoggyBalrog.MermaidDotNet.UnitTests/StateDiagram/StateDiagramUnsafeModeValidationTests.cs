using FoggyBalrog.MermaidDotNet.StateDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.StateDiagram;

public class StateDiagramUnsafeModeValidationTests
{
    [Fact]
    public void StateDiagramBuilder_DoesNotThrowIfTitleIsWhitespace()
    {
        Mermaid
            .Unsafe
            .StateDiagram(" ")
            .Build();
    }

    [Fact]
    public void AddState_DoesNotThrowIfDescriptionIsWhitespace()
    {
        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState(" ", out var _)
            .Build();
    }

    [Fact]
    public void AddCompositeState_DoesNotThrowIfDescriptionIsWhitespace()
    {
        Mermaid
            .Unsafe
            .StateDiagram()
            .AddCompositeState(" ", out var _, _ =>
            {
            })
            .Build();
    }

    [Fact]
    public void AddStateLink_DoesNotThrowIfStateIsForeign()
    {
        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState("State", out var state);

        Mermaid
            .Unsafe
            .StateDiagram()
            .AddStateLink(state, "http://example.com")
            .Build();
    }

    [Fact]
    public void AddStateLink_DoesNotThrowIfUrlIsWhitespace()
    {
        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState("State", out var state)
            .AddStateLink(state, " ")
            .Build();
    }

    [Fact]
    public void AddStateLink_DoesNotThrowIfTooltipIsWhitespace()
    {
        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState("State", out var state)
            .AddStateLink(state, "http://example.com", " ")
            .Build();
    }

    [Fact]
    public void AddNote_DoesNotThrowIfStateIsForeign()
    {
        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState("State", out var state);

        Mermaid
            .Unsafe
            .StateDiagram()
            .AddNote(state, NotePosition.Right, "Note")
            .Build();
    }

    [Fact]
    public void AddNote_DoesNotThrowIfTextIsWhitespace()
    {
        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState("State", out var state)
            .AddNote(state, NotePosition.Right, " ")
            .Build();
    }

    [Fact]
    public void AddStateTransition_DoesNotThrowIfFromStateIsForeign()
    {
        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState("From", out var from);

        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState("To", out var to)
            .AddStateTransition(from, to)
            .Build();
    }

    [Fact]
    public void AddStateTransition_DoesNotThrowIfToStateIsForeign()
    {
        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState("To", out var to);

        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState("From", out var from)
            .AddStateTransition(from, to)
            .Build();
    }

    [Fact]
    public void AddStateTransition_DoesNotThrowIfDescriptionIsWhitespace()
    {
        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState("From", out var from)
            .AddState("To", out var to)
            .AddStateTransition(from, to, " ")
            .Build();
    }

    [Fact]
    public void AddTransitionFromStart_DoesNotThrowIfToStateIsForeign()
    {
        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState("To", out var to);

        Mermaid
            .Unsafe
            .StateDiagram()
            .AddTransitionFromStart(to)
            .Build();
    }

    [Fact]
    public void AddTransitionFromStart_DoesNotThrowIfDescriptionIsWhitespace()
    {
        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState("To", out var to)
            .AddTransitionFromStart(to, " ")
            .Build();
    }

    [Fact]
    public void AddTransitionToEnd_DoesNotThrowIfFromStateIsForeign()
    {
        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState("From", out var from);

        Mermaid
            .Unsafe
            .StateDiagram()
            .AddTransitionToEnd(from)
            .Build();
    }

    [Fact]
    public void AddTransitionToEnd_DoesNotThrowIfDescriptionIsWhitespace()
    {
        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState("From", out var from)
            .AddTransitionToEnd(from, " ")
            .Build();
    }

    [Fact]
    public void AddConcurrency_DoesNotThrowIfDescriptionIsWhitespace()
    {
        Mermaid
            .Unsafe
            .StateDiagram()
            .AddConcurrency(" ", out var _, _ =>
            {
            })
            .Build();
    }

    [Fact]
    public void StyleWithCssClass_DoesNotThrowIfCssClassIsForeign()
    {
        Mermaid
            .Unsafe
            .StateDiagram()
            .DefineCssClass("css-class", "color: red;", out var cssClass);

        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState("State 1", out var state1)
            .AddState("State 2", out var state2)
            .AddState("State 3", out var state3)
            .StyleWithCssClass(cssClass, state1, state2, state3)
            .Build();
    }

    [Fact]
    public void StyleWithCssClass_DoesNotThrowIfStateIsForeign()
    {
        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState("State 2", out var state2);

        Mermaid
            .Unsafe
            .StateDiagram()
            .AddState("State 1", out var state1)
            .AddState("State 3", out var state3)
            .DefineCssClass("css-class", "color: red;", out var cssClass)
            .StyleWithCssClass(cssClass, state1, state2, state3)
            .Build();
    }
}
