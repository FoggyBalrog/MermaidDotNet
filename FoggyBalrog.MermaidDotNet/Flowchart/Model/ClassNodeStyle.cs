namespace FoggyBalrog.MermaidDotNet.Flowchart.Model;

internal record ClassNodeStyle(string ClassName, Node[] Nodes) : INodeStyle;