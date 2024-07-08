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

        _items.Add(new Link([from], [to], text, lineStyle, ending, multidirectional, extraLength));
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

        _items.Add(new Link(from, to, text, lineStyle, ending, multidirectional, extraLength));
        return this;
    }

    public FlowchartBuilder AddCallback(Node node, string functionName, string? tooltip = null)
    {
        ThrowIfExternalItem(node);

        node.NodeClickBinding = new NodeCallback(functionName, tooltip);
        return this;
    }

    public FlowchartBuilder AddHyperlink(Node node, string uri, string? tooltip = null, HyperlinkTarget target = HyperlinkTarget.Self)
    {
        ThrowIfExternalItem(node);

        node.NodeClickBinding = new NodeHyperlink(uri, tooltip, target);
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
        var builder = new StringBuilder();

        string orientation = SymbolMaps.Orientation[_orientation];

        builder.AppendLine($"flowchart {orientation}");

        foreach (IFlowItem? flowItem in _items)
        {
            switch (flowItem)
            {
                case Node node:
                    BuildNode(builder, node);
                    break;

                case Link link:
                    BuildLink(builder, link);
                    break;

                case Subgraph subgraph:
                    BuildSubgraph(builder, subgraph);
                    break;

                case End:
                    builder.AppendLine($"{Shared.Indent}end");
                    break;

                case Comment comment:
                    builder.AppendLine($"{Shared.Indent}%% {comment.Text}");
                    break;

                default:
                    throw new InvalidOperationException($"Unknown flow item: {flowItem}");
            }
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }

    private void BuildSubgraph(StringBuilder builder, Subgraph subgraph)
    {
        builder.AppendLine($"{Shared.Indent}subgraph {subgraph.Id} [{subgraph.Text}]");

        if (subgraph.Direction is not null)
        {
            string direction = SymbolMaps.Orientation[subgraph.Direction.Value];
            builder.AppendLine($"{Shared.Indent}direction {direction}");
        }
    }

    private static void BuildLink(StringBuilder builder, Link link)
    {
        string text = link.Text is not null ? $"|\"{link.Text}\"|" : string.Empty;
        (string beginning, string line, string ending) = SymbolMaps.Links(link);
        string from = string.Join(" & ", link.From.Select(n => n.Id));
        string to = string.Join(" & ", link.To.Select(n => n.Id));
        builder.AppendLine($"{Shared.Indent}{from} {beginning}{line}{ending}{text} {to}");
    }

    private static void BuildNode(StringBuilder builder, Node node)
    {
        (string leftBoundary, string rightBoundary) = SymbolMaps.Nodes[node.Shape];

        builder.AppendLine($"{Shared.Indent}{node.Id}{leftBoundary}\"{node.Text}\"{rightBoundary}");

        switch (node.NodeClickBinding)
        {
            case NodeCallback nodeCallback:
                builder.AppendLine($"{Shared.Indent}click {node.Id} {nodeCallback.FunctionName}{(nodeCallback.Tooltip is not null ? $" \"{nodeCallback.Tooltip}\"" : string.Empty)}");
                break;

            case NodeHyperlink nodeHyperlink:
                string target = SymbolMaps.HyperlinksTargets[nodeHyperlink.Target];
                builder.AppendLine($"{Shared.Indent}click {node.Id} \"{nodeHyperlink.Uri}\"{(nodeHyperlink.Tooltip is not null ? $" \"{nodeHyperlink.Tooltip}\"" : string.Empty)} {target}");
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
        foreach (ILinkable item in items)
        {
            ThrowIfExternalItem(item);
        }
    }
}