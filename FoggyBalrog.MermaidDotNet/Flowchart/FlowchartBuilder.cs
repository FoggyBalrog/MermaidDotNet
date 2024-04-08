using System.Text;
using FoggyBalrog.MermaidDotNet.Flowchart.Model;

namespace FoggyBalrog.MermaidDotNet.Flowchart;

public class FlowchartBuilder
{
    private readonly List<IFlowItem> _items = [];
    private readonly FlowchartOrientation _orientation;

    internal FlowchartBuilder(FlowchartOrientation orientation)
    {
        _orientation = orientation;
    }

    public FlowchartBuilder AddNode(string text, out Node node, NodeShape shape = NodeShape.Rectangle)
    {
        node = new Node($"id{_items.Count + 1}", text, shape, null);
        _items.Add(node);
        return this;
    }

    public FlowchartBuilder AddMarkdownNode(string markdown, out Node node, NodeShape shape = NodeShape.Rectangle)
    {
        return AddNode($"`{markdown}`", out node, shape);
    }

    public FlowchartBuilder AddLink(
        ILinkable from,
        ILinkable to,
        string? text = null,
        LinkLineStyle lineStyle = LinkLineStyle.Solid,
        LinkEnding ending = LinkEnding.Arrow,
        bool multidirectional = false,
        int extraLength = 0)
    {
        ThrowIfExternalItem(from);
        ThrowIfExternalItem(to);

        if (extraLength < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(extraLength), "Extra length must be greater than or equal to 0");
        }

        _items.Add(new Link(from, to, text, lineStyle, ending, multidirectional, extraLength));
        return this;
    }

    public FlowchartBuilder AddLinkChain(
        ILinkable[] from,
        ILinkable[] to,
        string? text = null,
        LinkLineStyle lineStyle = LinkLineStyle.Solid,
        LinkEnding ending = LinkEnding.Arrow,
        bool multidirectional = false,
        int extraLength = 0)
    {
        ThrowIfExternalItem(from);
        ThrowIfExternalItem(to);

        _items.Add(new LinkChain(from, to, text, lineStyle, ending, multidirectional, extraLength));
        return this;
    }

    public FlowchartBuilder AddCallback(Node node, string functionName, string? tooltip = null)
    {
        ThrowIfExternalItem(node);

        node.NodeClickBindind = new NodeCallback(functionName, tooltip);
        return this;
    }

    public FlowchartBuilder AddHyperlink(Node node, string uri, string? tooltip = null, HyperlinkTarget target = HyperlinkTarget.Self)
    {
        ThrowIfExternalItem(node);

        node.NodeClickBindind = new NodeHyperlink(uri, tooltip, target);
        return this;
    }

    public FlowchartBuilder AddSubgraph(string text, out Subgraph subgraph, Action<FlowchartBuilder> action, FlowchartOrientation? direction = null)
    {
        subgraph = new Subgraph($"sub{_items.Count + 1}", text, direction);
        _items.Add(subgraph);
        action(this);
        _items.Add(new End());
        return this;
    }

    public FlowchartBuilder Comment(string text)
    {
        _items.Add(new Comment(text));
        return this;
    }

    public string Build()
    {
        string indent = "    ";
        var builder = new StringBuilder();

        string orientation = _orientation switch
        {
            FlowchartOrientation.TopToBottom => "TB",
            FlowchartOrientation.BottomToTop => "BT",
            FlowchartOrientation.LeftToRight => "LR",
            FlowchartOrientation.RightToLeft => "RL",
            _ => throw new InvalidOperationException($"Unknown orientation: {_orientation}")

        };

        builder.AppendLine($"flowchart {orientation}");

        foreach (var flowItem in _items)
        {
            switch (flowItem)
            {
                case Node node:
                    BuildNode(indent, builder, node);
                    break;

                case Link link:
                    BuildLink(indent, builder, link);
                    break;

                case LinkChain linkChain:
                    BuildLinkChain(indent, builder, linkChain);
                    break;

                case Subgraph subgraph:
                    BuildSubgraph(indent, builder, subgraph);
                    break;

                case End _:
                    builder.AppendLine($"{indent}end");
                    break;

                case Comment comment:
                    builder.AppendLine($"{indent}%% {comment.Text}");
                    break;

                default:
                    throw new InvalidOperationException($"Unknown flow item: {flowItem}");
            }
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }

    private void BuildSubgraph(string indent, StringBuilder builder, Subgraph subgraph)
    {
        builder.AppendLine($"{indent}subgraph {subgraph.Id} [{subgraph.Text}]");
        if (subgraph.Direction is not null)
        {
            string direction = subgraph.Direction switch
            {
                FlowchartOrientation.TopToBottom => "TB",
                FlowchartOrientation.BottomToTop => "BT",
                FlowchartOrientation.LeftToRight => "LR",
                FlowchartOrientation.RightToLeft => "RL",
                _ => throw new InvalidOperationException($"Unknown orientation: {_orientation}")
            };

            builder.AppendLine($"{indent}direction {direction}");
        }
    }

    private static void BuildLinkChain(string indent, StringBuilder builder, LinkChain linkChain)
    {
        string text = linkChain.Text is not null ? $"|\"{linkChain.Text}\"|" : string.Empty;
        string line = linkChain.LineStyle switch
        {
            LinkLineStyle.Solid => $"--{new string('-', linkChain.ExtraLength)}",
            LinkLineStyle.Dotted => $"-{new string('.', 1 + linkChain.ExtraLength)}-",
            LinkLineStyle.Thick => $"=={new string('=', linkChain.ExtraLength)}",
            LinkLineStyle.Invisible => $"~~~{new string('~', linkChain.ExtraLength)}",
            _ => throw new InvalidOperationException($"Unknown line style: {linkChain.LineStyle}")
        };
        string ending = (linkChain.LineStyle, linkChain.Ending) switch
        {
            (LinkLineStyle.Invisible, _) => string.Empty,
            (_, LinkEnding.Arrow) => ">",
            (_, LinkEnding.Circle) => "o",
            (_, LinkEnding.Cross) => "x",
            (LinkLineStyle.Solid, LinkEnding.Open) => "-",
            (LinkLineStyle.Thick, LinkEnding.Open) => "=",
            (_, LinkEnding.Open) => "",
            _ => throw new InvalidOperationException($"Unknown ending: {linkChain.Ending}")
        };
        string beginning = linkChain.Multidirectional ? (linkChain.LineStyle, linkChain.Ending) switch
        {
            (LinkLineStyle.Invisible, _) => string.Empty,
            (_, LinkEnding.Arrow) => "<",
            (_, LinkEnding.Circle) => "o",
            (_, LinkEnding.Cross) => "x",
            (_, LinkEnding.Open) => "",
            _ => throw new InvalidOperationException($"Unknown ending: {linkChain.Ending}")
        } : string.Empty;
        string from = string.Join(" & ", linkChain.From.Select(n => n.Id));
        string to = string.Join(" & ", linkChain.To.Select(n => n.Id));
        builder.AppendLine($"{indent}{from} {beginning}{line}{ending}{text} {to}");
    }

    private static void BuildLink(string indent, StringBuilder builder, Link link)
    {
        string text = link.Text is not null ? $"|\"{link.Text}\"|" : string.Empty;
        string line = link.LineStyle switch
        {
            LinkLineStyle.Solid => $"--{new string('-', link.ExtraLength)}",
            LinkLineStyle.Dotted => $"-{new string('.', 1 + link.ExtraLength)}-",
            LinkLineStyle.Thick => $"=={new string('=', link.ExtraLength)}",
            LinkLineStyle.Invisible => $"~~~{new string('~', link.ExtraLength)}",
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
        builder.AppendLine($"{indent}{link.From.Id} {beginning}{line}{ending}{text} {link.To.Id}");
    }

    private static void BuildNode(string indent, StringBuilder builder, Node node)
    {
        (string leftBoundary, string rightBoundary) = node.Shape switch
        {
            NodeShape.Rectangle => ("[", "]"),
            NodeShape.RoundEdges => ("(", ")"),
            NodeShape.Stadium => ("([", "])"),
            NodeShape.Subroutine => ("[[", "]]"),
            NodeShape.Cylindrical => ("[(", ")]"),
            NodeShape.Circle => ("((", "))"),
            NodeShape.DoubleCircle => ("(((", ")))"),
            NodeShape.Asymmetric => (">", "]"),
            NodeShape.Rhombus => ("{", "}"),
            NodeShape.Hexagon => ("{{", "}}"),
            NodeShape.Parallelogram => ("[/", "/]"),
            NodeShape.ParallelogramAlt => ("[\\", "\\]"),
            NodeShape.Trapezoid => ("[/", "\\]"),
            NodeShape.TrapezoidAlt => ("[\\", "/]"),
            _ => throw new InvalidOperationException($"Unknown shape: {node.Shape}")
        };

        builder.AppendLine($"{indent}{node.Id}{leftBoundary}\"{node.Text}\"{rightBoundary}");

        switch (node.NodeClickBindind)
        {
            case NodeCallback nodeCallback:
                builder.AppendLine($"{indent}click {node.Id} {nodeCallback.FunctionName}{(nodeCallback.Tooltip is not null ? $" \"{nodeCallback.Tooltip}\"" : string.Empty)}");
                break;

            case NodeHyperlink nodeHyperlink:
                string target = nodeHyperlink.Target switch
                {
                    HyperlinkTarget.Self => "_self",
                    HyperlinkTarget.Blank => "_blank",
                    HyperlinkTarget.Parent => "_parent",
                    HyperlinkTarget.Top => "_top",
                    _ => throw new InvalidOperationException($"Unknown target: {nodeHyperlink.Target}")
                };

                builder.AppendLine($"{indent}click {node.Id} \"{nodeHyperlink.Uri}\"{(nodeHyperlink.Tooltip is not null ? $" \"{nodeHyperlink.Tooltip}\"" : string.Empty)} {target}");
                break;
        }
    }

    private void ThrowIfExternalItem(IFlowItem item)
    {
        if (!_items.Contains(item))
        {
            throw new InvalidOperationException($"Item '{item}' is not defined in the diagram.");
        }
    }

    private void ThrowIfExternalItem(ILinkable[] items)
    {
        foreach (var item in items)
        {
            ThrowIfExternalItem(item);
        }
    }
}