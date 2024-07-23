﻿using System.Globalization;
using System.Text;
using FoggyBalrog.MermaidDotNet.QuadrantChart.Model;

namespace FoggyBalrog.MermaidDotNet.QuadrantChart;

/// <summary>
/// A builder for quadrant charts.
/// </summary>
public class QuadrantChartBuilder
{
    private readonly List<Point> _points = [];
    private readonly string? _title;
    private readonly string? _quadrant1;
    private readonly string? _quadrant2;
    private readonly string? _quadrant3;
    private readonly string? _quadrant4;
    private string? _axisLeft;
    private string? _axisRight;
    private string? _axisBottom;
    private string? _axisTop;

    internal QuadrantChartBuilder(
        string? title,
        string? quadrant1,
        string? quadrant2,
        string? quadrant3,
        string? quadrant4)
    {
        _title = title;
        _quadrant1 = quadrant1;
        _quadrant2 = quadrant2;
        _quadrant3 = quadrant3;
        _quadrant4 = quadrant4;
    }

    /// <summary>
    /// Sets the labels for the X axis. If <paramref name="right"/> is not specified, the X axis will have a single label.
    /// </summary>
    /// <param name="left">The label for the left side of the X axis.</param>
    /// <param name="right">The optional label for the right side of the X axis.</param>
    /// <returns>The current <see cref="QuadrantChartBuilder"/> instance.</returns>
    public QuadrantChartBuilder SetXAxisLabel(string left, string? right = null)
    {
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
    public QuadrantChartBuilder SetYAxisLabel(string bottom, string? top = null)
    {
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
    /// <returns>The current <see cref="QuadrantChartBuilder"/> instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown if <paramref name="x"/> or <paramref name="y"/> are not between 0 and 1.</exception>
    public QuadrantChartBuilder AddPoint(string label, double x, double y)
    {
        if (x < 0 || x > 1 || y < 0 || y > 1)
        {
            throw new InvalidOperationException($"X and Y must be between 0 and 1 (actual {x}, {y})");
        }

        _points.Add(new Point(label, x, y));
        return this;
    }

    /// <summary>
    /// Builds the Mermaid code for the quadrant chart.
    /// </summary>
    /// <returns>The Mermaid code for the quadrant chart.</returns>
    public string Build()
    {
        var builder = new StringBuilder();

        builder.AppendLine("quadrantChart");

        if (!string.IsNullOrWhiteSpace(_title))
        {
            builder.AppendLine($"{Shared.Indent}title {_title}");
        }

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
            builder.AppendLine($"{Shared.Indent}{point.Label}: [{x}, {y}]");
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }
}