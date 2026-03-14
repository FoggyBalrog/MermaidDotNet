using FoggyBalrog.MermaidDotNet.MindMap.Model;

namespace FoggyBalrog.MermaidDotNet.MindMap;

internal static class SymbolMaps
{
    public static Dictionary<NodeShape, (string Left, string Right)> Nodes = new()
    {
        [NodeShape.Default] = ("", ""),
        [NodeShape.Square] = ("[", "]"),
        [NodeShape.RoundedSquare] = ("(", ")"),
        [NodeShape.Circle] = ("((", "))"),
        [NodeShape.Bang] = ("))", "(("),
        [NodeShape.Cloud] = (")", "("),
        [NodeShape.Hexagon] = ("{{", "}}"),
    };
}
