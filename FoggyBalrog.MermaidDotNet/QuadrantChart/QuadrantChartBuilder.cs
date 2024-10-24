using System.Globalization;
using System.Text;
using FoggyBalrog.MermaidDotNet.Configuration;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.QuadrantChart.Model;

namespace FoggyBalrog.MermaidDotNet.QuadrantChart;

/// <summary>
/// A builder for quadrant charts.
/// </summary>
public class QuadrantChartBuilder
{
    private readonly List<Point> _points = [];
    private readonly string? _title;
    private readonly MermaidConfig? _config;
    private readonly string? _quadrant1;
    private readonly string? _quadrant2;
    private readonly string? _quadrant3;
    private readonly string? _quadrant4;
    private readonly List<CssClass> _cssClasses = [];
    private readonly bool _isSafe;
    private string? _axisLeft;
    private string? _axisRight;
    private string? _axisBottom;
    private string? _axisTop;

    internal QuadrantChartBuilder(
        string? title,
        MermaidConfig? config,
        string? quadrant1,
        string? quadrant2,
        string? quadrant3,
        string? quadrant4,
        bool isSafe)
    {
        if (isSafe)
        {
            title.ThrowIfWhiteSpace();
            quadrant1.ThrowIfWhiteSpace();
            quadrant2.ThrowIfWhiteSpace();
            quadrant3.ThrowIfWhiteSpace();
            quadrant4.ThrowIfWhiteSpace();
        }

        _title = title;
        _config = config;
        _quadrant1 = quadrant1;
        _quadrant2 = quadrant2;
        _quadrant3 = quadrant3;
        _quadrant4 = quadrant4;
        _isSafe = isSafe;
    }

    /// <summary>
    /// Sets the labels for the X axis. If <paramref name="right"/> is not specified, the X axis will have a single label.
    /// </summary>
    /// <param name="left">The label for the left side of the X axis.</param>
    /// <param name="right">The optional label for the right side of the X axis.</param>
    /// <returns>The current <see cref="QuadrantChartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="left"/> or <paramref name="right"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public QuadrantChartBuilder SetXAxisLabel(string left, string? right = null)
    {
        if (_isSafe)
        {
            left.ThrowIfWhiteSpace();
            right.ThrowIfWhiteSpace();
        }

        _axisLeft = left;
        _axisRight = right;
        return this;
    }

    /// <summary>
    /// Sets the labels for the Y axis. If <paramref name="top"/> is not specified, the Y axis will have a single label.
    /// </summary>
    /// <param name="bottom">The label for the bottom of the Y axis.</param>
    /// <param name="top">The optional label for the top of the Y axis.</param>
    /// <returns>The current <see cref="QuadrantChartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="bottom"/> or <paramref name="top"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public QuadrantChartBuilder SetYAxisLabel(string bottom, string? top = null)
    {
        if (_isSafe)
        {
            bottom.ThrowIfWhiteSpace();
            top.ThrowIfWhiteSpace();
        }

        _axisBottom = bottom;
        _axisTop = top;
        return this;
    }

    /// <summary>
    /// Adds a point to the quadrant chart.
    /// </summary>
    /// <param name="label">The label of the point.</param>
    /// <param name="x">The X coordinate of the point. Must be between 0 and 1.</param>
    /// <param name="y">The Y coordinate of the point. Must be between 0 and 1.</param>
    /// <param name="css">The optional CSS style to apply to the point.</param>
    /// <param name="cssClass">The optional CSS class to apply to the point.</param>
    /// <returns>The current <see cref="QuadrantChartBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="label"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="x"/> or <paramref name="y"/> is out of [0, 1] range, with the reason <see cref="MermaidExceptionReason.OutOfRange"/>.</exception>
    public QuadrantChartBuilder AddPoint(string label, double x, double y, string? css = null, CssClass? cssClass = null)
    {
        if (_isSafe)
        {
            label.ThrowIfWhiteSpace();
            x.ThrowIfOutOfRange(0, 1);
            y.ThrowIfOutOfRange(0, 1);
            css.ThrowIfWhiteSpace();
            cssClass?.ThrowIfForeignTo(_cssClasses);
        }

        _points.Add(new Point(label, x, y, css, cssClass));
        return this;
    }

    /// <summary>
    /// Defines a CSS class to be used to style nodes.
    /// </summary>
    /// <param name="name">The name of the CSS class.</param>
    /// <param name="css">The CSS style to apply to the class.</param>
    /// <param name="class">The CSS class that was defined.</param>
    /// <returns>The current <see cref="QuadrantChartBuilder"/> instance.</returns>
    public QuadrantChartBuilder DefineCssClass(string name, string css, out CssClass @class)
    {
        if (_isSafe)
        {
            name.ThrowIfWhiteSpace();
            css.ThrowIfWhiteSpace();
        }

        @class = new CssClass(name, css);
        _cssClasses.Add(@class);

        return this;
    }

    /// <summary>
    /// Builds the Mermaid code for the quadrant chart.
    /// </summary>
    /// <returns>The Mermaid code for the quadrant chart.</returns>
    public string Build()
    {
        var builder = new StringBuilder();

        builder.Append(FrontmatterGenerator.Generate(_title, _config));

        builder.AppendLine("quadrantChart");

        if (_axisLeft != null)
        {
            builder.AppendLine($"{Shared.Indent}x-axis {_axisLeft}{(_axisRight != null ? $" --> {_axisRight}" : "")}");
        }

        if (_axisBottom != null)
        {
            builder.AppendLine($"{Shared.Indent}y-axis {_axisBottom}{(_axisTop != null ? $" --> {_axisTop}" : "")}");
        }

        if (_quadrant1 != null)
        {
            builder.AppendLine($"{Shared.Indent}quadrant-1 {_quadrant1}");
        }

        if (_quadrant2 != null)
        {
            builder.AppendLine($"{Shared.Indent}quadrant-2 {_quadrant2}");
        }

        if (_quadrant3 != null)
        {
            builder.AppendLine($"{Shared.Indent}quadrant-3 {_quadrant3}");
        }

        if (_quadrant4 != null)
        {
            builder.AppendLine($"{Shared.Indent}quadrant-4 {_quadrant4}");
        }

        foreach (Point? point in _points)
        {
            string x = point.X.ToString(CultureInfo.InvariantCulture);
            string y = point.Y.ToString(CultureInfo.InvariantCulture);
            string classString = point.CssClass != null ? $":::{point.CssClass.Name}" : "";
            string cssString = point.Css != null ? $" {point.Css}" : "";
            builder.AppendLine($"{Shared.Indent}{point.Label}{classString}: [{x}, {y}]{cssString}");
        }

        foreach (CssClass cssClass in _cssClasses)
        {
            builder.AppendLine($"{Shared.Indent}classDef {cssClass.Name} {cssClass.Css}");
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }
}