namespace FoggyBalrog.MermaidDotNet.XYChart.Model;

internal record NumericAxisInfo(string? Title, double Min, double Max) : AbstractAxisInfo(Title);
