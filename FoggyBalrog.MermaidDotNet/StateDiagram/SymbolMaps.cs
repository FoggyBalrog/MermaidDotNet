using FoggyBalrog.MermaidDotNet.StateDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.StateDiagram;

internal static class SymbolMaps
{
    public static Dictionary<StateDiagramDirection, string> Directions { get; } = new()
    {
        [StateDiagramDirection.TopToBottom] = "TB",
        [StateDiagramDirection.LeftToRight] = "LR",
        [StateDiagramDirection.BottomToTop] = "BT",
        [StateDiagramDirection.RightToLeft] = "RL"
    };

    public static Dictionary<NotePosition, string> NotePositions { get; } = new()
    {
        [NotePosition.Right] = "right",
        [NotePosition.Left] = "left"
    };
}
