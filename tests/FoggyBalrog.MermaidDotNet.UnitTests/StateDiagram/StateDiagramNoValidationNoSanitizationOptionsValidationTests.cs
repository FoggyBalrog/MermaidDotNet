using FoggyBalrog.MermaidDotNet.StateDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.StateDiagram;

public class StateDiagramNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void StateDiagramBuilder_DoesNotThrowIfTitleIsWhitespace()
    {
        Mermaid
            .StateDiagram(" ", options: _options)
            .Build();
    }

    [Fact]
    public void AddState_DoesNotThrowIfDescriptionIsWhitespace()
    {
        Mermaid
            .StateDiagram(options: _options)
            .AddState(" ", out var _)
            .Build();
    }

    [Fact]
    public void AddCompositeState_DoesNotThrowIfDescriptionIsWhitespace()
    {
        Mermaid
            .StateDiagram(options: _options)
            .AddCompositeState(" ", out var _, _ =>
            {
            })
            .Build();
    }

    [Fact]
    public void AddStateLink_DoesNotThrowIfStateIsForeign()
    {
        Mermaid
            .StateDiagram(options: _options)
            .AddState("State", out var state);

        Mermaid
            .StateDiagram(options: _options)
            .AddStateLink(state, "http://example.com")
            .Build();
    }

    [Fact]
    public void AddStateLink_DoesNotThrowIfUrlIsWhitespace()
    {
        Mermaid
            .StateDiagram(options: _options)
            .AddState("State", out var state)
            .AddStateLink(state, " ")
            .Build();
    }

    [Fact]
    public void AddStateLink_DoesNotThrowIfTooltipIsWhitespace()
    {
        Mermaid
            .StateDiagram(options: _options)
            .AddState("State", out var state)
            .AddStateLink(state, "http://example.com", " ")
            .Build();
    }

    [Fact]
    public void AddNote_DoesNotThrowIfStateIsForeign()
    {
        Mermaid
            .StateDiagram(options: _options)
            .AddState("State", out var state);

        Mermaid
            .StateDiagram(options: _options)
            .AddNote(state, NotePosition.Right, "Note")
            .Build();
    }

    [Fact]
    public void AddNote_DoesNotThrowIfTextIsWhitespace()
    {
        Mermaid
            .StateDiagram(options: _options)
            .AddState("State", out var state)
            .AddNote(state, NotePosition.Right, " ")
            .Build();
    }

    [Fact]
    public void AddStateTransition_DoesNotThrowIfFromStateIsForeign()
    {
        Mermaid
            .StateDiagram(options: _options)
            .AddState("From", out var from);

        Mermaid
            .StateDiagram(options: _options)
            .AddState("To", out var to)
            .AddStateTransition(from, to)
            .Build();
    }

    [Fact]
    public void AddStateTransition_DoesNotThrowIfToStateIsForeign()
    {
        Mermaid
            .StateDiagram(options: _options)
            .AddState("To", out var to);

        Mermaid
            .StateDiagram(options: _options)
            .AddState("From", out var from)
            .AddStateTransition(from, to)
            .Build();
    }

    [Fact]
    public void AddStateTransition_DoesNotThrowIfDescriptionIsWhitespace()
    {
        Mermaid
            .StateDiagram(options: _options)
            .AddState("From", out var from)
            .AddState("To", out var to)
            .AddStateTransition(from, to, " ")
            .Build();
    }

    [Fact]
    public void AddTransitionFromStart_DoesNotThrowIfToStateIsForeign()
    {
        Mermaid
            .StateDiagram(options: _options)
            .AddState("To", out var to);

        Mermaid
            .StateDiagram(options: _options)
            .AddTransitionFromStart(to)
            .Build();
    }

    [Fact]
    public void AddTransitionFromStart_DoesNotThrowIfDescriptionIsWhitespace()
    {
        Mermaid
            .StateDiagram(options: _options)
            .AddState("To", out var to)
            .AddTransitionFromStart(to, " ")
            .Build();
    }

    [Fact]
    public void AddTransitionToEnd_DoesNotThrowIfFromStateIsForeign()
    {
        Mermaid
            .StateDiagram(options: _options)
            .AddState("From", out var from);

        Mermaid
            .StateDiagram(options: _options)
            .AddTransitionToEnd(from)
            .Build();
    }

    [Fact]
    public void AddTransitionToEnd_DoesNotThrowIfDescriptionIsWhitespace()
    {
        Mermaid
            .StateDiagram(options: _options)
            .AddState("From", out var from)
            .AddTransitionToEnd(from, " ")
            .Build();
    }

    [Fact]
    public void AddConcurrency_DoesNotThrowIfDescriptionIsWhitespace()
    {
        Mermaid
            .StateDiagram(options: _options)
            .AddConcurrency(" ", out var _, _ =>
            {
            })
            .Build();
    }

    [Fact]
    public void StyleWithCssClass_DoesNotThrowIfCssClassIsForeign()
    {
        Mermaid
            .StateDiagram(options: _options)
            .DefineCssClass("css-class", "color: red;", out var cssClass);

        Mermaid
            .StateDiagram(options: _options)
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
            .StateDiagram(options: _options)
            .AddState("State 2", out var state2);

        Mermaid
            .StateDiagram(options: _options)
            .AddState("State 1", out var state1)
            .AddState("State 3", out var state3)
            .DefineCssClass("css-class", "color: red;", out var cssClass)
            .StyleWithCssClass(cssClass, state1, state2, state3)
            .Build();
    }
}
