using FoggyBalrog.MermaidDotNet.Flowchart.Model;

namespace FoggyBalrog.MermaidDotNet.Flowchart;

internal static class SymbolMaps
{
    public static Dictionary<FlowchartOrientation, string> Orientation { get; } = new()
    {
        [FlowchartOrientation.TopToBottom] = "TB",
        [FlowchartOrientation.BottomToTop] = "BT",
        [FlowchartOrientation.LeftToRight] = "LR",
        [FlowchartOrientation.RightToLeft] = "RL"
    };

    public static Dictionary<NodeShape, (string Left, string Right)> Nodes { get; } = new()
    {
        [NodeShape.Rectangle] = ("[", "]"),
        [NodeShape.RoundEdges] = ("(", ")"),
        [NodeShape.Stadium] = ("([", "])"),
        [NodeShape.Subroutine] = ("[[", "]]"),
        [NodeShape.Cylindrical] = ("[(", ")]"),
        [NodeShape.Circle] = ("((", "))"),
        [NodeShape.DoubleCircle] = ("(((", ")))"),
        [NodeShape.Asymmetric] = (">", "]"),
        [NodeShape.Rhombus] = ("{", "}"),
        [NodeShape.Hexagon] = ("{{", "}}"),
        [NodeShape.Parallelogram] = ("[/", "/]"),
        [NodeShape.ParallelogramAlt] = ("[\\", "\\]"),
        [NodeShape.Trapezoid] = ("[/", "\\]"),
        [NodeShape.TrapezoidAlt] = ("[\\", "/]")
    };

    public static Dictionary<HyperlinkTarget, string> HyperlinksTargets { get; } = new()
    {
        [HyperlinkTarget.Self] = "_self",
        [HyperlinkTarget.Blank] = "_blank",
        [HyperlinkTarget.Parent] = "_parent",
        [HyperlinkTarget.Top] = "_top"
    };

    public static (string Start, string Line, string End) Links(Link link)
    {
        int extraLength = Math.Max(0, link.ExtraLength);

        string line = link.LineStyle switch
        {
            LinkLineStyle.Solid => $"--{new string('-', extraLength)}",
            LinkLineStyle.Dotted => $"-{new string('.', 1 + extraLength)}-",
            LinkLineStyle.Thick => $"=={new string('=', extraLength)}",
            LinkLineStyle.Invisible => $"~~~{new string('~', extraLength)}",
            _ => throw new InvalidOperationException($"Unknown line style: {link.LineStyle}")
        };

        string ending = (link.LineStyle, link.Ending) switch
        {
            (LinkLineStyle.Invisible, _) => string.Empty,
            (_, LinkEnding.Arrow) => ">",
            (_, LinkEnding.Circle) => "o",
            (_, LinkEnding.Cross) => "x",
            (LinkLineStyle.Solid, LinkEnding.Open) => "-",
            (LinkLineStyle.Thick, LinkEnding.Open) => "=",
            (_, LinkEnding.Open) => "",
            _ => throw new InvalidOperationException($"Unknown ending: {link.Ending}")
        };

        string beginning = link.Multidirectional ? (link.LineStyle, link.Ending) switch
        {
            (LinkLineStyle.Invisible, _) => string.Empty,
            (_, LinkEnding.Arrow) => "<",
            (_, LinkEnding.Circle) => "o",
            (_, LinkEnding.Cross) => "x",
            (_, LinkEnding.Open) => "",
            _ => throw new InvalidOperationException($"Unknown ending: {link.Ending}")
        } : string.Empty;

        return (beginning, line, ending);
    }
}
