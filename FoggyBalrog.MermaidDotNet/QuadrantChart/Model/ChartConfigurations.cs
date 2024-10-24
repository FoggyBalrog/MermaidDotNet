namespace FoggyBalrog.MermaidDotNet.QuadrantChart.Model;

/// <summary>
/// Configuration settings for the chart.
/// </summary>
public record ChartConfigurations
{
    /// <summary>
    /// Width of the chart.
    /// </summary>
    public int? ChartWidth { get; init; }

    /// <summary>
    /// Height of the chart.
    /// </summary>
    public int? ChartHeight { get; init; }

    /// <summary>
    /// Top and bottom padding of the title.
    /// </summary>
    public int? TitlePadding { get; init; }

    /// <summary>
    /// Title font size.
    /// </summary>
    public int? TitleFontSize { get; init; }

    /// <summary>
    /// Padding outside all the quadrants.
    /// </summary>
    public int? QuadrantPadding { get; init; }

    /// <summary>
    /// Quadrant text top padding when text is drawn on top.
    /// </summary>
    public int? QuadrantTextTopPadding { get; init; }

    /// <summary>
    /// Quadrant text font size.
    /// </summary>
    public int? QuadrantLabelFontSize { get; init; }

    /// <summary>
    /// Border stroke width inside the quadrants.
    /// </summary>
    public int? QuadrantInternalBorderStrokeWidth { get; init; }

    /// <summary>
    /// Quadrant external border stroke width.
    /// </summary>
    public int? QuadrantExternalBorderStrokeWidth { get; init; }

    /// <summary>
    /// Top and bottom padding of x-axis text.
    /// </summary>
    public int? XAxisLabelPadding { get; init; }

    /// <summary>
    /// X-axis text font size.
    /// </summary>
    public int? XAxisLabelFontSize { get; init; }

    /// <summary>
    /// Position of x-axis.
    /// </summary>
    public string? XAxisPosition { get; init; }

    /// <summary>
    /// Left and right padding of y-axis text.
    /// </summary>
    public int? YAxisLabelPadding { get; init; }

    /// <summary>
    /// Y-axis text font size.
    /// </summary>
    public int? YAxisLabelFontSize { get; init; }

    /// <summary>
    /// Position of y-axis.
    /// </summary>
    public string? YAxisPosition { get; init; }

    /// <summary>
    /// Padding between point and the below text.
    /// </summary>
    public int? PointTextPadding { get; init; }

    /// <summary>
    /// Point text font size.
    /// </summary>
    public int? PointLabelFontSize { get; init; }

    /// <summary>
    /// Radius of the point to be drawn.
    /// </summary>
    public int? PointRadius { get; init; }
}

