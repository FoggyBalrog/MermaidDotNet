using System.Text;
using FoggyBalrog.MermaidDotNet.StateDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.StateDiagram;

public class StateDiagramBuilder
{
    private string _indent = "    ";
    private readonly string? _title;
    private readonly StateDiagramDirection? _direction;
    private readonly List<IStateDiagramItem> _items = [];

    internal StateDiagramBuilder(string? title, StateDiagramDirection? direction)
    {
        _title = title;
        _direction = direction;
    }

    public StateDiagramBuilder AddState(string description, out State state)
    {
        state = new State($"s{_items.Count + 1}", description, StateKind.Simple);
        _items.Add(state);
        return this;
    }

    public StateDiagramBuilder AddCompositeState(string description, out State state, Action<StateDiagramBuilder> action)
    {
        state = new State($"s{_items.Count + 1}", description, StateKind.Composite);
        _items.Add(state);
        action(this);
        _items.Add(new CompositeStateEnd());
        return this;
    }

    public StateDiagramBuilder AddChoiceState(out State state)
    {
        state = new State($"s{_items.Count + 1}", string.Empty, StateKind.Choice);
        _items.Add(state);
        return this;
    }

    public StateDiagramBuilder AddForkState(out State state)
    {
        state = new State($"s{_items.Count + 1}", string.Empty, StateKind.Fork);
        _items.Add(state);
        return this;
    }

    public StateDiagramBuilder AddJoinState(out State state)
    {
        state = new State($"s{_items.Count + 1}", string.Empty, StateKind.Join);
        _items.Add(state);
        return this;
    }

    public StateDiagramBuilder AddNote(State state, NotePosition position, string text)
    {
        _items.Add(new Note(state, position, text));
        return this;
    }

    public StateDiagramBuilder AddStateTransition(State from, State to, string? description = null)
    {
        _items.Add(new StateTransition(from, to, description));
        return this;
    }

    public StateDiagramBuilder AddTransitionFromStart(State to, string? description = null)
    {
        _items.Add(new TransitionFromStart(to, description));
        return this;
    }

    public StateDiagramBuilder AddTransitionToEnd(State from, string? description = null)
    {
        _items.Add(new TransitionToEnd(from, description));
        return this;
    }

    public StateDiagramBuilder Concurrency(string description, out State state, params Action<StateDiagramBuilder>[] actions)
    {
        state = new State($"s{_items.Count + 1}", description, StateKind.Composite);
        _items.Add(state);

        for (int i = 0; i < actions.Length; i++)
        {
            actions[i](this);

            if (i < actions.Length - 1)
            {
                _items.Add(new ConcurrencySeparator());
            }
        }

        _items.Add(new CompositeStateEnd());

        return this;
    }

    public StateDiagramBuilder StyleWithRawCss(State state, string css)
    {
        _items.Add(new RawCssStyle(state, css));
        return this;
    }

    public StateDiagramBuilder StyleWithCssClass(string cssClass, params State[] states)
    {
        _items.Add(new CssClassStyle(states, cssClass));
        return this;
    }

    public string Build()
    {
        var builder = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(_title))
        {
            builder.AppendLine("---");
            builder.AppendLine($"title: {_title}");
            builder.AppendLine("---");
        }

        builder.AppendLine("stateDiagram-v2");

        if (_direction is not null)
        {
            string directionString = _direction switch
            {
                StateDiagramDirection.TopToBottom => "TB",
                StateDiagramDirection.BottomToTop => "BT",
                StateDiagramDirection.LeftToRight => "LR",
                StateDiagramDirection.RightToLeft => "RL",
                _ => throw new InvalidOperationException($"Unknown direction: {_direction}")
            };
            builder.AppendLine($"{_indent}direction {directionString}");
        }

        foreach (IStateDiagramItem? item in _items)
        {
            switch (item)
            {
                case State { Kind: StateKind.Choice } state:
                    builder.AppendLine($"{_indent}state {state.Id} <<choice>>");
                    break;

                case State { Kind: StateKind.Fork } state:
                    builder.AppendLine($"{_indent}state {state.Id} <<fork>>");
                    break;

                case State { Kind: StateKind.Join } state:
                    builder.AppendLine($"{_indent}state {state.Id} <<join>>");
                    break;

                case State { Kind: StateKind.Composite } state:
                    builder.AppendLine($"{_indent}state \"{state.Description}\" as {state.Id} {{");
                    _indent += "    ";
                    break;

                case State { Kind: StateKind.Simple } state:
                    builder.AppendLine($"{_indent}{state.Id} : {state.Description}");
                    break;

                case CompositeStateEnd:
                    _indent = _indent[..^4];
                    builder.AppendLine($"{_indent}}}");
                    break;

                case StateTransition stateTransition:
                    BuildStateTransition(builder, stateTransition);
                    break;

                case TransitionFromStart transitionFromStart:
                    BuildTransitionFromStart(builder, transitionFromStart);
                    break;

                case TransitionToEnd transitionToEnd:
                    BuildTransitionToEnd(builder, transitionToEnd);
                    break;

                case Note note:
                    string notePosition = note.Position switch
                    {
                        NotePosition.Right => "right",
                        NotePosition.Left => "left",
                        _ => throw new InvalidOperationException($"Unknown note position: {note.Position}")
                    };
                    builder.AppendLine($"{_indent}note {notePosition} of {note.State.Id}");
                    builder.AppendLine($"{_indent}{_indent}{note.Text}");
                    builder.AppendLine($"{_indent}end note");
                    break;

                case ConcurrencySeparator:
                    builder.AppendLine($"{_indent}--");
                    break;

                case RawCssStyle rawCssStyle:
                    builder.AppendLine($"{_indent}classDef {rawCssStyle.State.Id} {rawCssStyle.Css}");
                    break;

                case CssClassStyle cssClassStyle:
                    builder.AppendLine($"{_indent}class {string.Join(",", cssClassStyle.States.Select(c => c.Id))} {cssClassStyle.CssClass}");
                    break;
            }
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }

    private void BuildTransitionToEnd(StringBuilder builder, TransitionToEnd transitionToEnd)
    {
        string description = transitionToEnd.Description is not null ? $" : {transitionToEnd.Description}" : string.Empty;
        builder.AppendLine($"{_indent}{transitionToEnd.From.Id} --> [*]{description}");
    }

    private void BuildTransitionFromStart(StringBuilder builder, TransitionFromStart transitionFromStart)
    {
        string description = transitionFromStart.Description is not null ? $" : {transitionFromStart.Description}" : string.Empty;
        builder.AppendLine($"{_indent}[*] --> {transitionFromStart.To.Id}{description}");
    }

    private void BuildStateTransition(StringBuilder builder, StateTransition stateTransition)
    {
        string description = stateTransition.Description is not null ? $" : {stateTransition.Description}" : string.Empty;
        builder.AppendLine($"{_indent}{stateTransition.From.Id} --> {stateTransition.To.Id}{description}");
    }
}
