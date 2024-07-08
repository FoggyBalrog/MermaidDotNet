using System.Text;
using FoggyBalrog.MermaidDotNet.StateDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.StateDiagram;

/// <summary>
/// A builder for creating state diagrams.
/// </summary>
public class StateDiagramBuilder
{
    private string _indent = Shared.Indent;
    private readonly string? _title;
    private readonly StateDiagramDirection? _direction;
    private readonly List<IStateDiagramItem> _items = [];

    internal StateDiagramBuilder(string? title, StateDiagramDirection? direction)
    {
        _title = title;
        _direction = direction;
    }

    /// <summary>
    /// Adds a simple state to the state diagram.
    /// </summary>
    /// <param name="description">The description of the state.</param>
    /// <param name="state">The state that was added.</param>
    /// <returns>The current instance of the <see cref="StateDiagramBuilder"/>.</returns>
    public StateDiagramBuilder AddState(string description, out State state)
    {
        state = new State($"s{_items.Count + 1}", description, StateKind.Simple);
        _items.Add(state);
        return this;
    }

    /// <summary>
    /// Adds a composite state to the state diagram.
    /// </summary>
    /// <param name="description">The description of the state.</param>
    /// <param name="state">The state that was added.</param>
    /// <param name="action">An action that will be executed to add other items to the composite state.</param>
    /// <returns>The current instance of the <see cref="StateDiagramBuilder"/>.</returns>
    public StateDiagramBuilder AddCompositeState(string description, out State state, Action<StateDiagramBuilder> action)
    {
        state = new State($"s{_items.Count + 1}", description, StateKind.Composite);
        _items.Add(state);
        action(this);
        _items.Add(new CompositeStateEnd());
        return this;
    }

    /// <summary>
    /// Adds a choice state to the state diagram.
    /// </summary>
    /// <param name="state">The state that was added.</param>
    /// <returns>The current instance of the <see cref="StateDiagramBuilder"/>.</returns>
    public StateDiagramBuilder AddChoiceState(out State state)
    {
        state = new State($"s{_items.Count + 1}", string.Empty, StateKind.Choice);
        _items.Add(state);
        return this;
    }

    /// <summary>
    /// Adds a fork state to the state diagram.
    /// </summary>
    /// <param name="state">The state that was added.</param>
    /// <returns>The current instance of the <see cref="StateDiagramBuilder"/>.</returns>
    public StateDiagramBuilder AddForkState(out State state)
    {
        state = new State($"s{_items.Count + 1}", string.Empty, StateKind.Fork);
        _items.Add(state);
        return this;
    }

    /// <summary>
    /// Adds a join state to the state diagram.
    /// </summary>
    /// <param name="state">The state that was added.</param>
    /// <returns>The current instance of the <see cref="StateDiagramBuilder"/>.</returns>
    public StateDiagramBuilder AddJoinState(out State state)
    {
        state = new State($"s{_items.Count + 1}", string.Empty, StateKind.Join);
        _items.Add(state);
        return this;
    }

    /// <summary>
    /// Adds a note to a state.
    /// </summary>
    /// <param name="state">The state to add the note to.</param>
    /// <param name="position">The position of the note.</param>
    /// <param name="text">The text of the note.</param>
    /// <returns>The current instance of the <see cref="StateDiagramBuilder"/>.</returns>
    public StateDiagramBuilder AddNote(State state, NotePosition position, string text)
    {
        _items.Add(new Note(state, position, text));
        return this;
    }

    /// <summary>
    /// Adds a state transition to the state diagram.
    /// </summary>
    /// <param name="from">The state to transition from.</param>
    /// <param name="to">The state to transition to.</param>
    /// <param name="description">An optional description of the transition.</param>
    /// <returns>The current instance of the <see cref="StateDiagramBuilder"/>.</returns>
    public StateDiagramBuilder AddStateTransition(State from, State to, string? description = null)
    {
        _items.Add(new StateTransition(from, to, description));
        return this;
    }

    /// <summary>
    /// Adds a transition from the start state to another state.
    /// </summary>
    /// <param name="to">The state to transition to.</param>
    /// <param name="description">An optional description of the transition.</param>
    /// <returns>The current instance of the <see cref="StateDiagramBuilder"/>.</returns>
    public StateDiagramBuilder AddTransitionFromStart(State to, string? description = null)
    {
        _items.Add(new TransitionFromStart(to, description));
        return this;
    }

    /// <summary>
    /// Adds a transition to the end state from another state.
    /// </summary>
    /// <param name="from">The state to transition from.</param>
    /// <param name="description">An optional description of the transition.</param>
    /// <returns>The current instance of the <see cref="StateDiagramBuilder"/>.</returns>
    public StateDiagramBuilder AddTransitionToEnd(State from, string? description = null)
    {
        _items.Add(new TransitionToEnd(from, description));
        return this;
    }

    /// <summary>
    /// Adds a composite state, in which multiple branches are exisiting concurrently.
    /// </summary>
    /// <param name="description">The description of the composite state.</param>
    /// <param name="state">The composite state that was added.</param>
    /// <param name="actions">Concurrent actions that will be executed to add other items to the composite state.</param>
    /// <returns>The current instance of the <see cref="StateDiagramBuilder"/>.</returns>
    public StateDiagramBuilder AddConcurrency(string description, out State state, params Action<StateDiagramBuilder>[] actions)
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

    /// <summary>
    /// Styles a state with raw CSS.
    /// </summary>
    /// <param name="state">The state to style.</param>
    /// <param name="css">The raw CSS to apply to the state.</param>
    /// <returns>The current instance of the <see cref="StateDiagramBuilder"/>.</returns>
    public StateDiagramBuilder StyleWithRawCss(State state, string css)
    {
        _items.Add(new RawCssStyle(state, css));
        return this;
    }

    /// <summary>
    /// Styles any number of states with a CSS class.
    /// </summary>
    /// <param name="cssClass">The CSS class to apply to the states.</param>
    /// <param name="states">The states to style.</param>
    /// <returns>The current instance of the <see cref="StateDiagramBuilder"/>.</returns>
    public StateDiagramBuilder StyleWithCssClass(string cssClass, params State[] states)
    {
        _items.Add(new CssClassStyle(states, cssClass));
        return this;
    }

    /// <summary>
    /// Builds the Mermaid code for the state diagram.
    /// </summary>
    /// <returns>The Mermaid code for the state diagram.</returns>
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
            string directionString = SymbolMaps.Directions[_direction.Value];
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
                    _indent += Shared.Indent;
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
                    string notePosition = SymbolMaps.NotePositions[note.Position];
                    builder.AppendLine($"{_indent}note {notePosition} of {note.State.Id}");
                    builder.AppendLine($"{_indent.Repeat(2)}{note.Text}");
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
