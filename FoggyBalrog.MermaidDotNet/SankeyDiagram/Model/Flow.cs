namespace FoggyBalrog.MermaidDotNet.SankeyDiagram.Model;

internal record Flow(string Source, string Target, double Value) : ISankeyItem;
