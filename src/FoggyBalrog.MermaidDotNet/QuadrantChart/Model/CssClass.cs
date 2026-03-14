namespace FoggyBalrog.MermaidDotNet.QuadrantChart.Model;

public record CssClass
{
    internal CssClass(string name, string css)
    {
        Name = name;
        Css = css;
    }

    internal string Name { get; }

    internal string Css { get; }
}
