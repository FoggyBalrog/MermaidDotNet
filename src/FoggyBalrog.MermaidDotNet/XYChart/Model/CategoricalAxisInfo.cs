namespace FoggyBalrog.MermaidDotNet.XYChart.Model;

internal record CategoricalAxisInfo(string? Title, ICollection<string> Categories) : AbstractAxisInfo(Title);
