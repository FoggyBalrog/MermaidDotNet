using FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.ClassDiagram;

internal static class SymbolMaps
{
    public static Dictionary<Visibilities, (string Prefix, string Suffix)> Visibility { get; } = new()
    {
        [Visibilities.Public] = ("+", ""),
        [Visibilities.Private] = ("-", ""),
        [Visibilities.Protected] = ("#", ""),
        [Visibilities.Internal] = ("~", ""),
        [Visibilities.Abstract] = ("", "*"),
        [Visibilities.Static] = ("", "$")
    };

    public static Dictionary<RelationshipType, (string From, string To)> Relationship { get; } = new()
    {
        [RelationshipType.Inheritance] = ("<|", "|>"),
        [RelationshipType.Composition] = ("*", "*"),
        [RelationshipType.Aggregation] = ("o", "o"),
        [RelationshipType.Association] = ("<", ">"),
        [RelationshipType.Unspecified] = ("", "")
    };

    public static Dictionary<LinkStyle, string> Link { get; } = new()
    {
        [LinkStyle.Solid] = "--",
        [LinkStyle.Dashed] = ".."
    };

    public static Dictionary<ClassDiagramDirection, string> Direction { get; } = new()
    {
        [ClassDiagramDirection.TopToBottom] = "TB",
        [ClassDiagramDirection.BottomToTop] = "BT",
        [ClassDiagramDirection.LeftToRight] = "LR",
        [ClassDiagramDirection.RightToLeft] = "RL"
    };
}
