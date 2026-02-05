using System.Text;
using FoggyBalrog.MermaidDotNet.BlockDiagram.Model;
using FoggyBalrog.MermaidDotNet.Configuration;
using FoggyBalrog.MermaidDotNet.Configuration.Model;

namespace FoggyBalrog.MermaidDotNet.BlockDiagram;

public class BlockDiagramBuilder
{
    private readonly string? _title;
    private readonly MermaidConfig? _config;
    private readonly bool _isSafe;
    private readonly int? _columns;
    private readonly List<IBlockDiagramItem> _items = [];
    private readonly List<Link> _links = [];
    private readonly List<Style> _styles = [];
    private int _blockCount = 0;
    private int _compositeCount = 0;

    internal BlockDiagramBuilder(string? title, MermaidConfig? config, bool isSafe, int? columns)
    {
        _title = title;
        _config = config;
        _isSafe = isSafe;
        _columns = columns;
    }

    /// <summary>
    /// Adds a block to the diagram.
    /// </summary>
    /// <param name="label">The label of the block.</param>
    /// <param name="width">An optional width multiplier for the block (corresponds to the number of columns the block spans). Must be non-negative.</param>
    /// <param name="shape">The shape of the block.</param>
    /// <returns>The current <see cref="BlockDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="width"/> is stricly negative, with the reason <see cref="MermaidExceptionReason.StrictlyNegative"/>.</exception>
    public BlockDiagramBuilder AddBlock(string label, out Block block, int width = 1, BlockShape shape = BlockShape.Rectangle)
    {
        if (_isSafe)
        {
            width.ThrowIfStrictlyNegative();
        }

        block = new Block($"b{_blockCount++}", label, shape);

        _items.Add(block);

        return this;
    }

    /// <summary>
    /// Adds a composite block to the diagram.
    /// </summary>
    /// <param name="buildAction">An action that builds the composite block using a new <see cref="BlockDiagramBuilder"/> instance.</param>
    /// <param name="columns">An optional number of columns for the composite block.</param>
    /// <returns>The current <see cref="BlockDiagramBuilder"/> instance.</returns>
    public BlockDiagramBuilder AddCompositeBlock(Action<BlockDiagramBuilder> buildAction, int? columns = null, int width = 1)
    {
        if (_isSafe)
        {
            width.ThrowIfStrictlyNegative();
        }

        var compositeBuilder = new BlockDiagramBuilder(null, _config, _isSafe, columns)
        {
            _blockCount = _blockCount
        };

        buildAction(compositeBuilder);
        _items.Add(new CompositeBlock(compositeBuilder._items, compositeBuilder._links, compositeBuilder._columns, width));

        _blockCount = compositeBuilder._blockCount;

        return this;
    }

    /// <summary>
    /// Adds space(s) to the diagram.
    /// </summary>
    /// <param name="count">The number of spaces to add. Must be non-negative.</param>
    /// <returns></returns>
    public BlockDiagramBuilder AddSpace(int count = 1)
    {
        _items.Add(new Space(count));
        return this;
    }

    /// <summary>
    /// Adds a link between two blocks in the diagram.
    /// </summary>
    /// <param name="from">The starting block of the link.</param>
    /// <param name="to">The ending block of the link.</param>
    /// <param name="text">An optional text to display on the link.</param>
    /// <returns>The current <see cref="BlockDiagramBuilder"/> instance.</returns>
    public BlockDiagramBuilder AddLink(Block from, Block to, string? text = null)
    {
        _links.Add(new Link(from, to, text));

        return this;
    }

    /// <summary>
    /// Applies custom CSS styles to a block.
    /// </summary>
    /// <param name="block">The block to style.</param>
    /// <param name="css">The CSS styles to apply.</param>
    /// <returns>The current <see cref="BlockDiagramBuilder"/> instance.</returns>
    public BlockDiagramBuilder StyleBlock(Block block, string css)
    {
        if (_isSafe)
        {
            css.ThrowIfWhiteSpace();
        }

        _styles.Add(new Style(block, css));

        return this;
    }

    /// <summary>
    /// Builds the mermaid code for the block diagram.
    /// </summary>
    public string Build()
    {
        var builder = new StringBuilder();

        builder.Append(FrontmatterGenerator.Generate(_title, _config));

        builder.AppendLine("block");

        if (_columns.HasValue)
        {
            builder.AppendLine($"{Shared.Indent}columns {_columns.Value}");
        }

        foreach (var item in _items)
        {
            BuildRecursive(builder, item, Shared.Indent);
        }

        foreach (var link in _links)
        {
            BuildLink(builder, link, Shared.Indent);
        }

        foreach (var style in _styles)
        {
            BuildStyle(builder, style, Shared.Indent);
        }

        // Remove the last newline
        if (builder.Length >= Environment.NewLine.Length)
        {
            builder.Length -= Environment.NewLine.Length;
        }

        return builder.ToString();
    }

    private void BuildRecursive(StringBuilder builder, IBlockDiagramItem item, string indent)
    {
        switch (item)
        {
            case Block block:
                BuildBlock(builder, block, indent);
                break;

            case CompositeBlock composite:
                BuildCompositeBlock(builder, indent, composite);
                break;

            case Space space:
                string countString = space.Count > 1 ? $":{space.Count}" : string.Empty;
                builder.AppendLine($"{indent}space{countString}");
                break;
        }
    }

    private void BuildCompositeBlock(StringBuilder builder, string indent, CompositeBlock composite)
    {
        string sizeInfo = composite.Width > 1 ? $":composite{_compositeCount++}:{composite.Width}" : string.Empty;
        builder.AppendLine($"{indent}block{sizeInfo}");

        var childIndent = indent + Shared.Indent;

        if (composite.Columns.HasValue)
        {
            builder.AppendLine($"{childIndent}columns {composite.Columns.Value}");
        }

        foreach (var nestedItem in composite.Items)
        {
            BuildRecursive(builder, nestedItem, childIndent);
        }

        foreach (var link in composite.Links)
        {
            BuildLink(builder, link, childIndent);
        }

        foreach (var style in _styles)
        {
            BuildStyle(builder, style, childIndent);
        }

        builder.AppendLine($"{indent}end");
    }

    private static void BuildBlock(StringBuilder builder, Block block, string indent)
    {
        (string open, string close) = block.Shape switch
        {
            BlockShape.Rectangle => ("[", "]"),
            BlockShape.RoundEdges => ("(", ")"),
            BlockShape.Stadium => ("([", "])"),
            BlockShape.Subroutine => ("[[", "]]"),
            BlockShape.Cylindrical => ("[(", ")]"),
            BlockShape.Circle => ("((", "))"),
            BlockShape.DoubleCircle => ("(((", ")))"),
            BlockShape.Asymmetric => (">", "]"),
            BlockShape.Rhombus => ("{", "}"),
            BlockShape.Hexagon => ("{{", "}}"),
            BlockShape.Parallelogram => ("[/", "/]"),
            BlockShape.ParallelogramAlt => ("[\\", "\\]"),
            BlockShape.Trapezoid => ("[/", "\\]"),
            BlockShape.TrapezoidAlt => ("[\\", "/]"),
            BlockShape.RightArrow => ("<[", "]>(right)"),
            BlockShape.LeftArrow => ("<[", "]>(left)"),
            BlockShape.UpArrow => ("<[", "]>(up)"),
            BlockShape.DownArrow => ("<[", "]>(down)"),
            BlockShape.XArrow => ("<[", "]>(x)"),
            BlockShape.YArrow => ("<[", "]>(y)"),
            _ => throw MermaidException.InvalidOperation($"Unsupported block shape: {block.Shape}.")
        };

        builder.AppendLine($"{indent}{block.Id}{open}\"{block.Label}\"{close}");
    }

    private static void BuildLink(StringBuilder builder, Link link, string indent)
    {
        string arrow = link.Text is null ? "-->" : $"--\"{link.Text}\"-->";
        builder.AppendLine($"{indent}{link.From.Id} {arrow} {link.To.Id}");
    }

    private static void BuildStyle(StringBuilder builder, Style style, string indent)
    {
        builder.AppendLine($"{indent}style {style.Block.Id} {style.Css}");
    }
}
