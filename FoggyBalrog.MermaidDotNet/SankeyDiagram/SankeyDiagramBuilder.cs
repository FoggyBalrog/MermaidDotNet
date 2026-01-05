using System.Text;
using FoggyBalrog.MermaidDotNet.Configuration;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.SankeyDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.SankeyDiagram;

public class SankeyDiagramBuilder
{
    private readonly string? _title;
    private readonly MermaidConfig? _config;
    private readonly bool _isSafe;
    private readonly List<ISankeyItem> _items = [];


    internal SankeyDiagramBuilder(string? title, MermaidConfig? config, bool isSafe)
    {
        _title = title;
        _config = config;
        _isSafe = isSafe;
    }

    /// <summary>
    /// Adds a flow to the Sankey diagram.
    /// </summary>
    /// <param name="source">The source label.</param>
    /// <param name="target">The target label.</param>
    /// <param name="value">The value of the flow.</param>
    /// <returns>The current <see cref="SankeyDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="source"/> or <paramref name="target"/> is null or whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public SankeyDiagramBuilder AddFlow(string source, string target, double value)
    {
        if (_isSafe)
        {
            source.ThrowIfWhiteSpace();
            target.ThrowIfWhiteSpace();
        }

        _items.Add(new Flow(source, target, value));

        return this;
    }

    /// <summary>
    /// Adds an empty line to the Sankey diagram. Empty lines are not rendered in the final diagram but can be used for better readability of the generated mermaid code.
    /// </summary>
    /// <returns>The current <see cref="SankeyDiagramBuilder"/> instance.</returns>
    public SankeyDiagramBuilder AddEmptyLine()
    {
        _items.Add(new EmptyLine());
        return this;
    }

    /// <summary>
    /// Builds the mermaid code for the Sankey diagram.
    /// </summary>
    /// <returns></returns>
    public string Build()
    {
        var builder = new StringBuilder();

        builder.Append(FrontmatterGenerator.Generate(_title, _config));

        builder.AppendLine("sankey");

        foreach (var item in _items)
        {
            switch (item)
            {
                case Flow flow:
                    builder.AppendLine($"{flow.Source},{flow.Target},{flow.Value}");
                    break;

                case EmptyLine:
                    builder.AppendLine();
                    break;
            }
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }
}
