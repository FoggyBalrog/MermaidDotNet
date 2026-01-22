using System.Text;
using FoggyBalrog.MermaidDotNet.Configuration;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.StateDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.StateDiagram;

/// <summary>
/// A builder for creating state diagrams.
/// </summary>
public class StateDiagramBuilder
{
    private string _indent = Shared.Indent;
    private readonly string? _title;
    private readonly MermaidConfig? _config;
    private readonly StateDiagramDirection? _direction;
    private readonly bool _isSafe;
    private readonly List<IStateDiagramItem> _items = [];
    private readonly List<CssClass> _cssClasses = [];

    internal StateDiagramBuilder(string? title, MermaidConfig? config, StateDiagramDirection? direction, bool isSafe)
    {
        if (isSafe)
        {
            title?.ThrowIfWhiteSpace();
        }

        _title = title;
        _config = config;
        _direction = direction;
        _isSafe = isSafe;
    }

    /// <summary>
    /// Adds a simple state to the state diagram.
    /// </summary>
    /// <param name="description">The description of the state.</param>
    /// <param name="state">The state that was added.</param>
    /// <returns>The current instance of the <see cref="StateDiagramBuilder"/>.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="description"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public StateDiagramBuilder AddState(string description, out State state)
    {
        if (_isSafe)
        {
            description.ThrowIfWhiteSpace();
        }

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
    /// <exception cref="MermaidException">Thrown when <paramref name="description"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public StateDiagramBuilder AddCompositeState(string description, out State state, Action<StateDiagramBuilder> action)
    {
        if (_isSafe)
        {
            description.ThrowIfWhiteSpace();
        }

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
    /// Adds a hyperlink to the specified state in the diagram, optionally including a tooltip.
    /// </summary>
    /// <param name="state">The state to which the hyperlink will be attached.</param>
    /// <param name="url">The URL to associate with the state.</param>
    /// <param name="tooltip">An optional tooltip to display when hovering over the link.</param>
    /// <returns>The current <see cref="StateDiagramBuilder"/> instance, enabling method chaining.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="state"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="url"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="tooltip"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public StateDiagramBuilder AddStateLink(State state, string url, string? tooltip = null)
    {
        if (_isSafe)
        {
            state.ThrowIfForeignTo(_items);
            url.ThrowIfWhiteSpace();
            tooltip?.ThrowIfWhiteSpace();
        }
        _items.Add(new StateLink(state, url, tooltip));
        return this;
    }

    /// <summary>
    /// Adds a note to a state.
    /// </summary>
    /// <param name="state">The state to add the note to.</param>
    /// <param name="position">The position of the note.</param>
    /// <param name="text">The text of the note.</param>
    /// <returns>The current instance of the <see cref="StateDiagramBuilder"/>.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="state"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="text"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public StateDiagramBuilder AddNote(State state, NotePosition position, string text)
    {
        if (_isSafe)
        {
            state.ThrowIfForeignTo(_items);
            text.ThrowIfWhiteSpace();
        }

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
    /// <exception cref="MermaidException">Thrown when <paramref name="from"/> or <paramref name="to"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="description"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public StateDiagramBuilder AddStateTransition(State from, State to, string? description = null)
    {
        if (_isSafe)
        {
            from.ThrowIfForeignTo(_items);
            to.ThrowIfForeignTo(_items);
            description.ThrowIfWhiteSpace();
        }

        _items.Add(new StateTransition(from, to, description));
        return this;
    }

    /// <summary>
    /// Adds a transition from the start state to another state.
    /// </summary>
    /// <param name="to">The state to transition to.</param>
    /// <param name="description">An optional description of the transition.</param>
    /// <returns>The current instance of the <see cref="StateDiagramBuilder"/>.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="to"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="description"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public StateDiagramBuilder AddTransitionFromStart(State to, string? description = null)
    {
        if (_isSafe)
        {
            to.ThrowIfForeignTo(_items);
            description.ThrowIfWhiteSpace();
        }

        _items.Add(new TransitionFromStart(to, description));
        return this;
    }

    /// <summary>
    /// Adds a transition to the end state from another state.
    /// </summary>
    /// <param name="from">The state to transition from.</param>
    /// <param name="description">An optional description of the transition.</param>
    /// <returns>The current instance of the <see cref="StateDiagramBuilder"/>.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="from"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="description"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public StateDiagramBuilder AddTransitionToEnd(State from, string? description = null)
    {
        if (_isSafe)
        {
            from.ThrowIfForeignTo(_items);
            description.ThrowIfWhiteSpace();
        }

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
    /// <exception cref="MermaidException">Thrown when <paramref name="description"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public StateDiagramBuilder AddConcurrency(string description, out State state, params Action<StateDiagramBuilder>[] actions)
    {
        if (_isSafe)
        {
            description.ThrowIfWhiteSpace();
        }

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
    /// Defines a CSS class to be used to style nodes.
    /// </summary>
    /// <param name="name">The name of the CSS class.</param>
    /// <param name="css">The CSS style to apply to the class.</param>
    /// <param name="class">The CSS class that was defined.</param>
    /// <returns>The current <see cref="StateDiagramBuilder"/> instance.</returns>
    public StateDiagramBuilder DefineCssClass(string name, string css, out CssClass @class)
    {
        if (_isSafe)
        {
            name.ThrowIfWhiteSpace();
            css.ThrowIfWhiteSpace();
        }

        @class = new CssClass(name, css);
        _cssClasses.Add(@class);

        return this;
    }

    /// <summary>
    /// Styles any number of states with a CSS class.
    /// </summary>
    /// <param name="cssClass">The CSS class to apply to the states.</param>
    /// <param name="states">The states to style.</param>
    /// <returns>The current instance of the <see cref="StateDiagramBuilder"/>.</returns>
    public StateDiagramBuilder StyleWithCssClass(CssClass cssClass, params State[] states)
    {
        if (_isSafe)
        {
            cssClass.ThrowIfForeignTo(_cssClasses);
            states.ThrowIfAnyForeignTo(_items);
        }

        _items.Add(new CssClassStyle(states, cssClass.Name));
        return this;
    }

    /// <summary>
    /// Builds the Mermaid code for the state diagram.
    /// </summary>
    /// <returns>The Mermaid code for the state diagram.</returns>
    public string Build()
    {
        var builder = new StringBuilder();

        builder.Append(FrontmatterGenerator.Generate(_title, _config));

        builder.AppendLine("stateDiagram-v2");

        if (_direction is not null)
        {
            string directionString = SymbolMaps.Directions[_direction.Value];
            builder.AppendLine($"{_indent}direction {directionString}");
        }

        foreach (CssClass cssClass in _cssClasses)
        {
            builder.AppendLine($"{_indent}classDef {cssClass.Name} {cssClass.Css}");
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

                case StateLink stateLink:
                    string tooltipPart = stateLink.Tooltip is not null ? $" \"{stateLink.Tooltip}\"" : string.Empty;
                    string hrefPart = stateLink.Tooltip is null ? "href " : string.Empty;
                    builder.AppendLine($"{_indent}click {stateLink.State.Id} {hrefPart}\"{stateLink.Url}\"{tooltipPart}");
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
