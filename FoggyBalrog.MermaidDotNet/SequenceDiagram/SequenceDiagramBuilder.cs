using System.Drawing;
using System.Globalization;
using System.Text;
using FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.SequenceDiagram;

/// <summary>
/// A builder for sequence diagrams.
/// </summary>
public class SequenceDiagramBuilder
{
    private readonly bool _autonumber;
    private readonly bool _isSafe;
    private readonly List<Box> _boxes = [];
    private readonly List<Member> _membersOutsideBoxes = [];
    private readonly List<ISequenceItem> _sequenceItems = [];

    internal SequenceDiagramBuilder(bool autonumber, bool isSafe)
    {
        _autonumber = autonumber;
        _isSafe = isSafe;
    }

    /// <summary>
    /// Adds a note over two members.
    /// </summary>
    /// <param name="m1">The first member.</param>
    /// <param name="m2">The second member.</param>
    /// <param name="text">The text of the note.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="m1"/> or <paramref name="m2"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="text"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public SequenceDiagramBuilder AddNoteOver(Member m1, Member m2, string text)
    {
        if (_isSafe)
        {
            ThrowIfForeign(m1);
            ThrowIfForeign(m2);
            text.ThrowIfWhiteSpace();
        }

        _sequenceItems.Add(new Note(text, NotePosition.Over, [m1, m2]));
        return this;
    }

    /// <summary>
    /// Adds a note to the right of a member.
    /// </summary>
    /// <param name="m">The member.</param>
    /// <param name="text">The text of the note.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="m"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="text"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public SequenceDiagramBuilder AddNoteRightOf(Member m, string text)
    {
        if (_isSafe)
        {
            ThrowIfForeign(m);
            text.ThrowIfWhiteSpace();
        }

        _sequenceItems.Add(new Note(text, NotePosition.RightOf, [m]));
        return this;
    }

    /// <summary>
    /// Adds a note to the left of a member.
    /// </summary>
    /// <param name="m">The member.</param>
    /// <param name="text">The text of the note.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="m"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="text"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public SequenceDiagramBuilder AddNoteLeftOf(Member m, string text)
    {
        if (_isSafe)
        {
            ThrowIfForeign(m);
            text.ThrowIfWhiteSpace();
        }

        _sequenceItems.Add(new Note(text, NotePosition.LeftOf, [m]));
        return this;
    }

    /// <summary>
    /// Adds a box to the diagram.
    /// </summary>
    /// <param name="name">The name of the box.</param>
    /// <param name="box">The box that was created.</param>
    /// <param name="color">An optional color for the box.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public SequenceDiagramBuilder AddBox(string name, out Box box, Color? color = null)
    {
        if (_isSafe)
        {
            name.ThrowIfWhiteSpace();
        }

        box = new Box(name, color ?? Color.Transparent);
        _boxes.Add(box);
        return this;
    }

    /// <summary>
    /// Adds a rectangle to the diagram.
    /// </summary>
    /// <param name="color">The color of the rectangle.</param>
    /// <param name="action">An action to add items to the rectangle.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    public SequenceDiagramBuilder AddRectangle(Color color, Action<SequenceDiagramBuilder> action)
    {
        _sequenceItems.Add(new Rect(color));
        action(this);
        _sequenceItems.Add(new End());
        return this;
    }

    /// <summary>
    /// Adds a loop to the diagram.
    /// </summary>
    /// <param name="description">The description of the loop.</param>
    /// <param name="action">An action to add items to the loop.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="description"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public SequenceDiagramBuilder AddLoop(string description, Action<SequenceDiagramBuilder> action)
    {
        if (_isSafe)
        {
            description.ThrowIfWhiteSpace();
        }

        _sequenceItems.Add(new Loop(description));
        action(this);
        _sequenceItems.Add(new End());
        return this;
    }

    /// <summary>
    /// Adds a member to the diagram.
    /// </summary>
    /// <param name="name">The name of the member.</param>
    /// <param name="memberType">The type of the member.</param>
    /// <param name="member">The member that was created.</param>
    /// <param name="box">An optional box to add the member to.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when a member with the same name already exists in the diagram, with the reason <see cref="MermaidExceptionReason.DuplicateValue"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="box"/> is not null and not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    public SequenceDiagramBuilder AddMember(string name, MemberType memberType, out Member member, Box? box = null)
    {
        if (_isSafe)
        {
            name.ThrowIfWhiteSpace();
            _membersOutsideBoxes.Union(_boxes.SelectMany(b => b.Members)).ThrowIfDuplicate(name, m => m.Name);
            box?.ThrowIfForeignTo(_boxes);
        }

        member = new Member(name, memberType);

        if (box is not null)
        {
            box.AddMember(member);
        }
        else
        {
            _membersOutsideBoxes.Add(member);
        }

        return this;
    }

    /// <summary>
    /// Adds a participant (member of type <see cref="MemberType.Participant"/>) to the diagram.
    /// </summary>
    /// <param name="name">The name of the participant.</param>
    /// <param name="member">The participant that was created.</param>
    /// <param name="box">An optional box to add the participant to.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when a member with the same name already exists in the diagram, with the reason <see cref="MermaidExceptionReason.DuplicateValue"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="box"/> is not null and not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    public SequenceDiagramBuilder AddParticipant(string name, out Member member, Box? box = null)
    {
        return AddMember(name, MemberType.Participant, out member, box);
    }

    /// <summary>
    /// Adds an actor (member of type <see cref="MemberType.Actor"/>) to the diagram.
    /// </summary>
    /// <param name="name">The name of the actor.</param>
    /// <param name="member">The actor that was created.</param>
    /// <param name="box">An optional box to add the actor to.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when a member with the same name already exists in the diagram, with the reason <see cref="MermaidExceptionReason.DuplicateValue"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="box"/> is not null and not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    public SequenceDiagramBuilder AddActor(string name, out Member member, Box? box = null)
    {
        return AddMember(name, MemberType.Actor, out member, box);
    }

    /// <summary>
    /// Sends a message between two members.
    /// </summary>
    /// <param name="sender">The sender of the message.</param>
    /// <param name="recipient">The recipient of the message.</param>
    /// <param name="description">The description of the message.</param>
    /// <param name="lineType">The type of the line.</param>
    /// <param name="arrowType">The type of the arrow.</param>
    /// <param name="activationType">The type of activation.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="sender"/> or <paramref name="recipient"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="description"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public SequenceDiagramBuilder SendMessage(
       Member sender,
       Member recipient,
       string description,
       LineType lineType = LineType.Solid,
       ArrowType arrowType = ArrowType.Filled,
       ActivationType activationType = ActivationType.None)
    {
        if (_isSafe)
        {
            ThrowIfForeign(sender);
            ThrowIfForeign(recipient);
            description.ThrowIfWhiteSpace();
        }

        _sequenceItems.Add(new Message(sender, recipient, description, lineType, arrowType, activationType));
        return this;
    }

    /// <summary>
    /// Sends a create message from a member to a new member.
    /// </summary>
    /// <param name="sender">The sender of the message.</param>
    /// <param name="name">The name of the new member.</param>
    /// <param name="memberType">The type of the new member.</param>
    /// <param name="recipient">The new member that was created.</param>
    /// <param name="description">The description of the message.</param>
    /// <param name="lineType">The type of the line.</param>
    /// <param name="arrowType">The type of the arrow.</param>
    /// <param name="activationType">The type of activation.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="sender"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> or <paramref name="description"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public SequenceDiagramBuilder SendCreateMessage(
        Member sender,
        string name,
        MemberType memberType,
        out Member recipient,
        string description,
        LineType lineType = LineType.Solid,
        ArrowType arrowType = ArrowType.Filled,
        ActivationType activationType = ActivationType.None)
    {
        if (_isSafe)
        {
            ThrowIfForeign(sender);
            name.ThrowIfWhiteSpace();
            description.ThrowIfWhiteSpace();
        }

        recipient = new Member(name, memberType);
        _sequenceItems.Add(new CreateMessage(sender, recipient, description, lineType, arrowType, activationType));
        return this;
    }

    /// <summary>
    /// Sends a destroy message from a member to a member.
    /// </summary>
    /// <param name="sender">The sender of the message.</param>
    /// <param name="recipient">The recipient of the message.</param>
    /// <param name="target">The target of the destruction (sender or recipient).</param>
    /// <param name="description">The description of the message.</param>
    /// <param name="lineType">The type of the line.</param>
    /// <param name="arrowType">The type of the arrow.</param>
    /// <param name="activationType">The type of activation.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="sender"/> or <paramref name="recipient"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="description"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public SequenceDiagramBuilder SendDestroyMessage(
        Member sender,
        Member recipient,
        DestructionTarget target,
        string description,
        LineType lineType = LineType.Solid,
        ArrowType arrowType = ArrowType.Filled,
        ActivationType activationType = ActivationType.None)
    {
        if (_isSafe)
        {
            ThrowIfForeign(sender);
            ThrowIfForeign(recipient);
            description.ThrowIfWhiteSpace();
        }

        _sequenceItems.Add(new DestroyMessage(sender, recipient, description, lineType, arrowType, target, activationType));
        return this;
    }

    /// <summary>
    /// Creates alternatives sequences in the sequence.
    /// </summary>
    /// <param name="alternatives">Any number of alternatives. Each alternative is a tuple with a description and an action to add items to it.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when any of the descriptions in <paramref name="alternatives"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public SequenceDiagramBuilder Alternatives(params (string description, Action<SequenceDiagramBuilder> action)[] alternatives)
    {
        if (alternatives.Length == 0)
        {
            return this;
        }

        if (_isSafe)
        {
            alternatives.Select(a => a.description).ThrowIfAnyWhitespace();
        }

        _sequenceItems.Add(new Alt(alternatives[0].description));
        alternatives[0].action(this);

        for (int i = 1; i < alternatives.Length; i++)
        {
            _sequenceItems.Add(new Or(alternatives[i].description));
            alternatives[i].action(this);
        }

        _sequenceItems.Add(new End());

        return this;
    }

    /// <summary>
    /// Creates parallel sequences in the sequence.
    /// </summary>
    /// <param name="parallels">Any number of parallels. Each parallel is a tuple with a description and an action to add items to it.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when any of the descriptions in <paramref name="parallels"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public SequenceDiagramBuilder Parallels(params (string description, Action<SequenceDiagramBuilder> action)[] parallels)
    {
        if (parallels.Length == 0)
        {
            return this;
        }

        if (_isSafe)
        {
            parallels.Select(p => p.description).ThrowIfAnyWhitespace();
        }

        _sequenceItems.Add(new Par(parallels[0].description));
        parallels[0].action(this);

        for (int i = 1; i < parallels.Length; i++)
        {
            _sequenceItems.Add(new And(parallels[i].description));
            parallels[i].action(this);
        }

        _sequenceItems.Add(new End());

        return this;
    }

    /// <summary>
    /// Creates an optional sequence in the sequence.
    /// </summary>
    /// <param name="description">The description of the optional sequence.</param>
    /// <param name="action">An action to add items to the optional sequence.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="description"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public SequenceDiagramBuilder Optional(string description, Action<SequenceDiagramBuilder> action)
    {
        if (_isSafe)
        {
            description.ThrowIfWhiteSpace();
        }

        _sequenceItems.Add(new Opt(description));
        action(this);
        _sequenceItems.Add(new End());
        return this;
    }

    /// <summary>
    /// Creates a critical sequence in the sequence.
    /// </summary>
    /// <param name="description">The description of the critical sequence.</param>
    /// <param name="action">An action to add items to the critical sequence.</param>
    /// <param name="options">Any number of options. Each option is a tuple with a description and an action to add items to it.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="description"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when any of the descriptions in <paramref name="options"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public SequenceDiagramBuilder Critical(
        string description,
        Action<SequenceDiagramBuilder> action,
        params (string description, Action<SequenceDiagramBuilder> action)[] options)
    {
        if (_isSafe)
        {
            description.ThrowIfWhiteSpace();
            options.Select(o => o.description).ThrowIfAnyWhitespace();
        }

        _sequenceItems.Add(new Critical(description));
        action(this);

        foreach ((string description, Action<SequenceDiagramBuilder> action) option in options)
        {
            _sequenceItems.Add(new Option(option.description));
            option.action(this);
        }

        _sequenceItems.Add(new End());

        return this;
    }

    /// <summary>
    /// Adds a break to the sequence.
    /// </summary>
    /// <param name="description">The description of the break.</param>
    /// <param name="action">An action to add items to the break.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="description"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public SequenceDiagramBuilder Break(
        string description,
        Action<SequenceDiagramBuilder> action)
    {
        if (_isSafe)
        {
            description.ThrowIfWhiteSpace();
        }

        _sequenceItems.Add(new Break(description));
        action(this);
        _sequenceItems.Add(new End());

        return this;
    }

    /// <summary>
    /// Adds a comment to the sequence.
    /// </summary>
    /// <param name="text">The text of the comment.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="text"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public SequenceDiagramBuilder Comment(string text)
    {
        if (_isSafe)
        {
            text.ThrowIfWhiteSpace();
        }

        _sequenceItems.Add(new Comment(text));
        return this;
    }

    /// <summary>
    /// Adds a link to a member.
    /// </summary>
    /// <param name="member">The member to add the link to.</param>
    /// <param name="title">The title of the link.</param>
    /// <param name="uri">The URI of the link.</param>
    /// <returns>The current <see cref="SequenceDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="member"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="title"/> or <paramref name="uri"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public SequenceDiagramBuilder AddLink(Member member, string title, string uri)
    {
        if (_isSafe)
        {
            ThrowIfForeign(member);
            title.ThrowIfWhiteSpace();
            uri.ThrowIfWhiteSpace();
        }

        _sequenceItems.Add(new Link(member, title, uri));
        return this;
    }

    /// <summary>
    /// Builds the Mermaid code for the sequence diagram.
    /// </summary>
    /// <returns>The Mermaid code for the sequence diagram.</returns>
    /// <exception cref="InvalidOperationException">Thrown when an unknown sequence item is encountered. Should never happen.</exception>
    public string Build()
    {
        string indent = Shared.Indent;
        var builder = new StringBuilder();

        builder.AppendLine("sequenceDiagram");

        if (_autonumber)
        {
            builder.AppendLine($"{indent}autonumber");
        }

        foreach (Box? box in _boxes)
        {
            string color = BuildColorRgbaExpression(box.Color);
            builder.AppendLine($"{indent}box {color} {box.Name}");

            BuildMembers(indent, builder, box.Members);

            builder.AppendLine($"{indent}end");
        }

        BuildMembers(indent, builder, _membersOutsideBoxes);

        foreach (ISequenceItem? item in _sequenceItems)
        {
#pragma warning disable S1643 // Strings should not be concatenated using '+' in a loop
            switch (item)
            {
                case Note note:
                    BuildNote(indent, builder, note);
                    break;

                case Message message:
                    BuildMessage(indent, builder, message);
                    break;

                case Loop loop:
                    builder.AppendLine($"{indent}loop {loop.Description}");
                    indent += Shared.Indent;
                    break;

                case Alt alt:
                    builder.AppendLine($"{indent}alt {alt.Description}");
                    indent += Shared.Indent;
                    break;

                case Or or:
                    indent = indent[..^4];
                    builder.AppendLine($"{indent}else {or.Description}");
                    indent += Shared.Indent;
                    break;

                case Opt opt:
                    builder.AppendLine($"{indent}opt {opt.Description}");
                    indent += Shared.Indent;
                    break;

                case Par par:
                    builder.AppendLine($"{indent}par {par.Description}");
                    indent += Shared.Indent;
                    break;

                case And and:
                    indent = indent[..^4];
                    builder.AppendLine($"{indent}and {and.Description}");
                    indent += Shared.Indent;
                    break;

                case Critical critical:
                    builder.AppendLine($"{indent}critical {critical.Description}");
                    indent += Shared.Indent;
                    break;

                case Option option:
                    indent = indent[..^4];
                    builder.AppendLine($"{indent}option {option.Description}");
                    indent += Shared.Indent;
                    break;

                case Break @break:
                    builder.AppendLine($"{indent}break {@break.Description}");
                    indent += Shared.Indent;
                    break;

                case Rect rect:
                    string color = BuildColorRgbaExpression(rect.Color);
                    builder.AppendLine($"{indent}rect {color}");
                    indent += Shared.Indent;
                    break;

                case Comment comment:
                    builder.AppendLine($"{indent}%% {comment.Text}");
                    break;

                case Link link:
                    builder.AppendLine($"{indent}link {link.Member.Name}: {link.Title} @ {link.Uri}");
                    break;

                case End:
                    indent = indent[..^Shared.Indent.Length];
                    builder.AppendLine($"{indent}end");
                    break;

                default:
                    throw new InvalidOperationException($"Unknown sequence item type: {item.GetType()}");
            }
#pragma warning restore S1643 // Strings should not be concatenated using '+' in a loop
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }

    private static string BuildColorRgbaExpression(Color color)
    {
        string alpha = (color.A / 255d).ToString("0.##", CultureInfo.InvariantCulture);
        return color.IsNamedColor ? color.Name : $"rgba({color.R}, {color.G}, {color.B}, {alpha})";
    }

    private static void BuildMembers(string indent, StringBuilder builder, IEnumerable<Member> members)
    {
        foreach (Member? member in members)
        {
            string memberType = SymbolMaps.MemberTypes[member.Type];
            builder.AppendLine($"{indent}{memberType} {member.Name}");
        }
    }

    private static void BuildNote(string indent, StringBuilder builder, Note note)
    {
        string notePosition = SymbolMaps.NotePositions[note.Position];
        string members = string.Join(", ", note.Members.Select(m => m.Name));
        builder.AppendLine($"{indent}note {notePosition} {members}: {note.Text}");
    }

    private static void BuildMessage(string indent, StringBuilder builder, Message message)
    {
        string lineType = SymbolMaps.LineTypes[message.LineType];
        string arrowType = SymbolMaps.ArrowTypes[message.ArrowType];
        string activation = SymbolMaps.ActivationTypes[message.ActivationType];

        if (message is DestroyMessage destroyMessage)
        {
            string targetName = destroyMessage.Target switch
            {
                DestructionTarget.Sender => destroyMessage.Sender.Name,
                DestructionTarget.Recipient => destroyMessage.Recipient.Name,
                _ => throw new InvalidOperationException($"Unknown destruction target: {destroyMessage.Target}")
            };
            builder.AppendLine($"{indent}destroy {targetName}");
        }

        if (message is CreateMessage createMessage)
        {
            string memberType = SymbolMaps.MemberTypes[createMessage.Recipient.Type];
            builder.AppendLine($"{indent}create {memberType} {createMessage.Recipient.Name}");
        }

        builder.AppendLine($"{indent}{message.Sender.Name} {lineType}{arrowType}{activation} {message.Recipient.Name}: {message.Description}");
    }

    private void ThrowIfForeign(Member member)
    {
        member.ThrowIfForeignToAll([
            _boxes.SelectMany(b => b.Members),
            _membersOutsideBoxes,
            _sequenceItems.OfType<CreateMessage>().Select(m => m.Recipient)]);
    }
}
