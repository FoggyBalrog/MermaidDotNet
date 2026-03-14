namespace FoggyBalrog.MermaidDotNet.Flowchart.Model;

internal record NodeCallback(string FunctionName, string? Tooltip) : INodeClickBinding;
