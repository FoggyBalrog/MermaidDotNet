using System.Text;
using FoggyBalrog.MermaidDotNet.Configuration;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.PacketDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.PacketDiagram;

/// <summary>
/// A builder for packet diagrams.
/// </summary>
public class PacketDiagramBuilder
{
    private readonly MermaidConfig? _config;
    private readonly string? _title;
    private readonly bool _isSafe;
    private readonly List<AbstractField> _fields = [];

    public PacketDiagramBuilder(string? title, MermaidConfig? config, bool isSafe)
    {
        if (isSafe)
        {
            title.ThrowIfWhiteSpace();
        }

        _title = title;
        _config = config;
        _isSafe = isSafe;
    }

    // <summary>
    /// Adds a field to the diagram, with end bit.
    /// </summary>
    /// <param name="bits">The end bit of the field.</param>
    /// <param name="description">An optional description of the field.</param>
    /// <returns>The current <see cref="PacketDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="bits"/> is strictly negative, with the reason <see cref="MermaidExceptionReason.StrictlyNegative"/>.</exception>
    public PacketDiagramBuilder AddFieldWithEnd(int end, string? description = null)
    {
        if (_isSafe)
        {
            end.ThrowIfStrictlyNegative();
        }

        _fields.Add(new EndField(end, description));

        return this;
    }

    // <summary>
    /// Adds a field to the diagram, with bits size.
    /// </summary>
    /// <param name="bits">The fiels size in bits.</param>
    /// <param name="description">An optional description of the field.</param>
    /// <returns>The current <see cref="PacketDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="bits"/> is strictly negative, with the reason <see cref="MermaidExceptionReason.StrictlyNegative"/>.</exception>
    public PacketDiagramBuilder AddFieldWithBits(int bits, string? description = null)
    {
        if (_isSafe)
        {
            bits.ThrowIfStrictlyNegative();
        }

        _fields.Add(new BitsFiels(bits, description));

        return this;
    }

    /// <summary>
    /// Builds the Mermaid code for the packet diagram.
    /// </summary>
    /// <returns>The Mermaid code for the packet diagram.</returns>
    public string Build()
    {
        var builder = new StringBuilder();

        builder.Append(FrontmatterGenerator.Generate(_title, _config));

        builder.AppendLine("packet");

        int lastEnd = -1;

        foreach (var field in _fields)
        {
            switch (field)
            {
                case EndField endField:
                    int start = lastEnd+1;
                    lastEnd = endField.End;
                    builder.AppendLine($"{start}-{lastEnd}: \"{endField.Description}\"");
                    break;

                case BitsFiels bitsFiels:
                    builder.AppendLine($"+{bitsFiels.Bits}: \"{bitsFiels.Description}\"");
                    lastEnd += bitsFiels.Bits;
                    break;

                default:
                    throw MermaidException.InvalidOperation($"Unsupported field type {field.GetType().Name}");
            }
        }
        
        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }
}
