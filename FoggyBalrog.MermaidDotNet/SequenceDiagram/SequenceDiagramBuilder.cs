using System.Drawing;
using System.Globalization;
using System.Text;
using FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.SequenceDiagram;

public class SequenceDiagramBuilder
{
    private readonly bool _autonumber;
    private readonly List<Box> _boxes = [];
    private readonly List<Member> _membersOutsideBoxes = [];
    private readonly List<ISequenceItem> _sequenceItems = [];

    internal SequenceDiagramBuilder(bool autonumber)
    {
        _autonumber = autonumber;
    }

    public SequenceDiagramBuilder AddNoteOver(Member m1, Member m2, string text)
    {
        ThrowIfExternalMember(m1);
        ThrowIfExternalMember(m2);

        _sequenceItems.Add(new Note(text, NotePosition.Over, [m1, m2]));
        return this;
    }

    public SequenceDiagramBuilder AddNoteRightOf(Member m, string text)
    {
        ThrowIfExternalMember(m);

        _sequenceItems.Add(new Note(text, NotePosition.RightOf, [m]));
        return this;
    }

    public SequenceDiagramBuilder AddNoteLeftOf(Member m, string text)
    {
        ThrowIfExternalMember(m);

        _sequenceItems.Add(new Note(text, NotePosition.LeftOf, [m]));
        return this;
    }

    public SequenceDiagramBuilder AddBox(string name, out Box box, Color? color = null)
    {
        box = new Box(name, color ?? Color.Transparent);
        _boxes.Add(box);
        return this;
    }

    public SequenceDiagramBuilder AddRectangle(Color color, Action<SequenceDiagramBuilder> action)
    {
        _sequenceItems.Add(new Rect(color));
        action(this);
        _sequenceItems.Add(new End());
        return this;
    }

    public SequenceDiagramBuilder AddLoop(string description, Action<SequenceDiagramBuilder> action)
    {
        _sequenceItems.Add(new Loop(description));
        action(this);
        _sequenceItems.Add(new End());
        return this;
    }

    public SequenceDiagramBuilder AddMember(string name, MemberType memberType, out Member member, Box? box = null)
    {
        if (_membersOutsideBoxes.Exists(m => m.Name == name) || _boxes.SelectMany(b => b.Members).Any(m => m.Name == name))
        {
            throw new InvalidOperationException($"Member with name '{name}' already exists.");
        }

        member = new Member(name, memberType);

        if (box is not null)
        {
            box.Members.Add(member);
        }
        else
        {
            _membersOutsideBoxes.Add(member);
        }

        return this;
    }

    public SequenceDiagramBuilder AddParticipant(string name, out Member member, Box? box = null)
    {
        return AddMember(name, MemberType.Participant, out member, box);
    }

    public SequenceDiagramBuilder AddActor(string name, out Member member, Box? box = null)
    {
        return AddMember(name, MemberType.Actor, out member, box);
    }

    public SequenceDiagramBuilder SendMessage(
       Member sender,
       Member recipient,
       string description,
       LineType lineType = LineType.Solid,
       ArrowType arrowType = ArrowType.Filled,
       ActivationType activationType = ActivationType.None)
    {
        ThrowIfExternalMember(sender);
        ThrowIfExternalMember(recipient);

        _sequenceItems.Add(new Message(sender, recipient, description, lineType, arrowType, activationType));
        return this;
    }

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
        recipient = new Member(name, memberType);
        _sequenceItems.Add(new CreateMessage(sender, recipient, description, lineType, arrowType, activationType));
        return this;
    }

    public SequenceDiagramBuilder SendDestroyMessage(
        Member sender,
        Member recipient,
        DestructionTarget target,
        string description,
        LineType lineType = LineType.Solid,
        ArrowType arrowType = ArrowType.Filled,
        ActivationType activationType = ActivationType.None)
    {
        ThrowIfExternalMember(sender);
        ThrowIfExternalMember(recipient);

        _sequenceItems.Add(new DestroyMessage(sender, recipient, description, lineType, arrowType, target, activationType));
        return this;
    }

    public SequenceDiagramBuilder Alternatives(params (string description, Action<SequenceDiagramBuilder> action)[] alternatives)
    {
        if (alternatives.Length == 0)
        {
            return this;
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

    public SequenceDiagramBuilder Parallels(params (string description, Action<SequenceDiagramBuilder> action)[] parallels)
    {
        if (parallels.Length == 0)
        {
            return this;
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

    public SequenceDiagramBuilder Optional(string description, Action<SequenceDiagramBuilder> action)
    {
        _sequenceItems.Add(new Opt(description));
        action(this);
        _sequenceItems.Add(new End());
        return this;
    }

    public SequenceDiagramBuilder Critical(
        string description,
        Action<SequenceDiagramBuilder> action,
        params (string description, Action<SequenceDiagramBuilder> action)[] options)
    {
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

    public SequenceDiagramBuilder Break(
        string description,
        Action<SequenceDiagramBuilder> action)
    {
        _sequenceItems.Add(new Break(description));
        action(this);
        _sequenceItems.Add(new End());

        return this;
    }

    public SequenceDiagramBuilder Comment(string text)
    {
        _sequenceItems.Add(new Comment(text));
        return this;
    }

    public SequenceDiagramBuilder AddLink(Member member, string title, string uri)
    {
        ThrowIfExternalMember(member);

        _sequenceItems.Add(new Link(member, title, uri));
        return this;
    }

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

    private static void BuildMembers(string indent, StringBuilder builder, IList<Member> members)
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

    private void ThrowIfExternalMember(Member member)
    {
        if (!_membersOutsideBoxes.Contains(member)
            && !_boxes.SelectMany(b => b.Members).Contains(member)
            && _sequenceItems.OfType<CreateMessage>().All(m => m.Recipient != member))
        {
            throw new InvalidOperationException($"Member '{member.Name}' is not defined in the diagram.");
        }
    }
}
