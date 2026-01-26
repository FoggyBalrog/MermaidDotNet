namespace FoggyBalrog.MermaidDotNet.BlockDiagram.Model;

public record Block : IBlockDiagramItem
{
    internal Block(string id, string label, BlockShape shape)
    {
        Id = id;
        Label = label;
        Shape = shape;
    }

    internal string Id { get; }

    internal string Label { get; }

    internal BlockShape Shape { get; }
}

internal record Style(Block Block, string Css);