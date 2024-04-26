namespace FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

internal record ClassCallback(string FunctionName, string? Tooltip) : IClassClickBindind;
