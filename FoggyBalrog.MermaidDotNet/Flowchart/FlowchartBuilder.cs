using System.Text;
using FoggyBalrog.MermaidDotNet.Configuration;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.Flowchart.Model;

namespace FoggyBalrog.MermaidDotNet.Flowchart;

/// <summary>
/// A builder for creating flowchart diagrams.
/// </summary>
public class FlowchartBuilder
{
    private readonly List<IFlowItem> _items = [];
    private readonly string? _title;
    private readonly MermaidConfig? _config;
    private readonly FlowchartOrientation _orientation;
    private readonly bool _isSafe;

    internal FlowchartBuilder(
        string? title,
        MermaidConfig? config,
        FlowchartOrientation orientation,
        bool isSafe)
    {
        _title = title;
        _config = config;
        _orientation = orientation;
        _isSafe = isSafe;
    }

    /// <summary>
    /// Adds a simple text node to the flowchart.
    /// </summary>
    /// <param name="text">The text of the node.</param>
    /// <param name="node">The node that was added.</param>
    /// <param name="shape">The shape of the node.</param>
    /// <returns>The current <see cref="FlowchartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="text"/> is whitespace, with a reason of <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public FlowchartBuilder AddNode(string text, out Node node, NodeShape shape = NodeShape.Rectangle)
    {
        if (_isSafe)
        {
            text.ThrowIfWhiteSpace();
        }

        node = new Node($"id{_items.Count + 1}", text, shape, null);
        _items.Add(node);
        return this;
    }

    /// <summary>
    /// Adds a markdown node to the flowchart.
    /// </summary>
    /// <param name="markdown">The markdown text of the node.</param>
    /// <param name="node">The node that was added.</param>
    /// <param name="shape">The shape of the node.</param>
    /// <returns>The current <see cref="FlowchartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="markdown"/> is whitespace, with a reason of <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public FlowchartBuilder AddMarkdownNode(string markdown, out Node node, NodeShape shape = NodeShape.Rectangle)
    {
        if (_isSafe)
        {
            markdown.ThrowIfWhiteSpace();
        }

        return AddNode($"`{markdown}`", out node, shape);
    }

    /// <summary>
    /// Adds a link between two nodes in the flowchart.
    /// </summary>
    /// <param name="from">The node to link from.</param>
    /// <param name="to">The node to link to.</param>
    /// <param name="text">An optional text to display on the link.</param>
    /// <param name="lineStyle">The style of the link line.</param>
    /// <param name="ending">The ending of the link.</param>
    /// <param name="multidirectional">Specifies whether the link should be multidirectional, i.e. have an arrow on both ends.</param>
    /// <param name="extraLength">Optional extra length to add to the link.</param>
    /// <returns>The current <see cref="FlowchartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="from"/> or <paramref name="to"/> are not part of the diagram, with a reason of <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="text"/> is whitespace, with a reason of <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="extraLength"/> is strictly negative, with a reason of <see cref="MermaidExceptionReason.StrictlyNegative"/>.</exception>
    public FlowchartBuilder AddLink(
        ILinkable from,
        ILinkable to,
        string? text = null,
        LinkLineStyle lineStyle = LinkLineStyle.Solid,
        LinkEnding ending = LinkEnding.Arrow,
        bool multidirectional = false,
        int extraLength = 0)
    {
        if (_isSafe)
        {
            from.ThrowIfForeignTo(_items);
            to.ThrowIfForeignTo(_items);
            text.ThrowIfWhiteSpace();
            extraLength.ThrowIfStrictlyNegative();
        }

        _items.Add(new Link([from], [to], text, lineStyle, ending, multidirectional, extraLength));
        return this;
    }

    /// <summary>
    /// Adds a link chain between multiple nodes in the flowchart.
    /// </summary>
    /// <param name="from">The nodes to link from.</param>
    /// <param name="to">The nodes to link to.</param>
    /// <param name="text">An optional text to display on the link chain.</param>
    /// <param name="lineStyle">The style of the link line.</param>
    /// <param name="ending">The ending of the link.</param>
    /// <param name="multidirectional">Specifies whether the link should be multidirectional, i.e. have an arrow on both ends.</param>
    /// <param name="extraLength">Optional extra length to add to the link.</param>
    /// <returns>The current <see cref="FlowchartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when any of the nodes in <paramref name="from"/> or <paramref name="to"/> are not part of the diagram, with a reason of <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="text"/> is whitespace, with a reason of <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="extraLength"/> is strictly negative, with a reason of <see cref="MermaidExceptionReason.StrictlyNegative"/>.</exception>
    public FlowchartBuilder AddLinkChain(
        ILinkable[] from,
        ILinkable[] to,
        string? text = null,
        LinkLineStyle lineStyle = LinkLineStyle.Solid,
        LinkEnding ending = LinkEnding.Arrow,
        bool multidirectional = false,
        int extraLength = 0)
    {
        if (_isSafe)
        {
            from.ThrowIfAnyForeignTo(_items);
            to.ThrowIfAnyForeignTo(_items);
            text.ThrowIfWhiteSpace();
            extraLength.ThrowIfStrictlyNegative();
        }

        _items.Add(new Link(from, to, text, lineStyle, ending, multidirectional, extraLength));
        return this;
    }

    /// <summary>
    /// Adds a callback to a node in the flowchart.
    /// </summary>
    /// <param name="node">The node to add the callback to.</param>
    /// <param name="functionName">The name of the function to call when the node is clicked.</param>
    /// <param name="tooltip">An optional tooltip to display when the node is hovered over.</param>
    /// <returns>The current <see cref="FlowchartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="node"/> is not part of the diagram, with a reason of <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="functionName"/> is whitespace, with a reason of <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="tooltip"/> is whitespace, with a reason of <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public FlowchartBuilder AddCallback(Node node, string functionName, string? tooltip = null)
    {
        if (_isSafe)
        {
            node.ThrowIfForeignTo(_items);
            functionName.ThrowIfWhiteSpace();
            tooltip.ThrowIfWhiteSpace();
        }

        node.NodeClickBinding = new NodeCallback(functionName, tooltip);
        return this;
    }

    /// <summary>
    /// Adds a hyperlink to a node in the flowchart.
    /// </summary>
    /// <param name="node">The node to add the hyperlink to.</param>
    /// <param name="uri">The URI to navigate to when the node is clicked.</param>
    /// <param name="tooltip">An optional tooltip to display when the node is hovered over.</param>
    /// <param name="target">The target of the hyperlink.</param>
    /// <returns>The current <see cref="FlowchartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="node"/> is not part of the diagram, with a reason of <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="uri"/> is whitespace, with a reason of <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="tooltip"/> is whitespace, with a reason of <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public FlowchartBuilder AddHyperlink(Node node, string uri, string? tooltip = null, HyperlinkTarget target = HyperlinkTarget.Self)
    {
        if (_isSafe)
        {
            node.ThrowIfForeignTo(_items);
            uri.ThrowIfWhiteSpace();
            tooltip.ThrowIfWhiteSpace();
        }

        node.NodeClickBinding = new NodeHyperlink(uri, tooltip, target);
        return this;
    }

    /// <summary>
    /// Adds a subgraph to the flowchart, in which you can add nodes and links using the provided action. The subgraph will be ended automatically when the action is completed.
    /// </summary>
    /// <param name="text">The text of the subgraph.</param>
    /// <param name="subgraph">The subgraph that was added.</param>
    /// <param name="action">The action to perform within the subgraph.</param>
    /// <param name="direction">An optional direction for the subgraph. If not specified, the default direction from Mermaid will be used on rendering.</param>
    /// <returns>The current <see cref="FlowchartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="text"/> is whitespace, with a reason of <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public FlowchartBuilder AddSubgraph(string text, out Subgraph subgraph, Action<FlowchartBuilder> action, FlowchartOrientation? direction = null)
    {
        if (_isSafe)
        {
            text.ThrowIfWhiteSpace();
        }

        subgraph = new Subgraph($"sub{_items.Count + 1}", text, direction);
        _items.Add(subgraph);
        action(this);
        _items.Add(new End());
        return this;
    }

    /// <summary>
    /// Adds a comment to the flowchart.
    /// </summary>
    /// <param name="text">The text of the comment.</param>
    /// <returns>The current <see cref="FlowchartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="text"/> is whitespace, with a reason of <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public FlowchartBuilder Comment(string text)
    {
        if (_isSafe)
        {
            text.ThrowIfWhiteSpace();
        }

        _items.Add(new Comment(text));
        return this;
    }

    /// <summary>
    /// Builds the Mermaid code for the flowchart.
    /// </summary>
    /// <returns>The Mermaid code for the flowchart.</returns>
    public string Build()
    {
        var builder = new StringBuilder();

        builder.Append(FrontmatterGenerator.Generate(_title, _config));

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
}