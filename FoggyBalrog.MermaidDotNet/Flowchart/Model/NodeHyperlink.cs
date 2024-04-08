namespace FoggyBalrog.MermaidDotNet.Flowchart.Model;

internal record NodeHyperlink(string Uri, string? Tooltip, HyperlinkTarget Target) : INodeClickBindind;
