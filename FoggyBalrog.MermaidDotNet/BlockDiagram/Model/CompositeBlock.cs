namespace FoggyBalrog.MermaidDotNet.BlockDiagram.Model;

internal record CompositeBlock(ICollection<IBlockDiagramItem> Items, ICollection<Link> Links, int? Columns, int Width) : IBlockDiagramItem;
