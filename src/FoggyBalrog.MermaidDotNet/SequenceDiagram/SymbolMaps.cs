using FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.SequenceDiagram;

internal static class SymbolMaps
{
    public static Dictionary<MemberType, string> MemberTypes { get; } = new()
    {
        [MemberType.Participant] = "participant",
        [MemberType.Actor] = "actor",
        [MemberType.Boundary] = "boundary",
        [MemberType.Control] = "control",
        [MemberType.Entity] = "entity",
        [MemberType.Database] = "database",
        [MemberType.Collections] = "collections",
        [MemberType.Queue] = "queue",
    };

    public static Dictionary<NotePosition, string> NotePositions { get; } = new()
    {
        [NotePosition.RightOf] = "right of",
        [NotePosition.LeftOf] = "left of",
        [NotePosition.Over] = "over",
    };

    public static Dictionary<LineType, string> LineTypes { get; } = new()
    {
        [LineType.Solid] = "-",
        [LineType.Dotted] = "--",
    };

    public static Dictionary<ArrowType, string> ArrowTypes { get; } = new()
    {
        [ArrowType.None] = ">",
        [ArrowType.Filled] = ">>",
        [ArrowType.Open] = ")",
        [ArrowType.Cross] = "x",
    };

    public static Dictionary<ActivationType, string> ActivationTypes { get; } = new()
    {
        [ActivationType.None] = "",
        [ActivationType.Activate] = "+",
        [ActivationType.Deactivate] = "-",
    };
}
