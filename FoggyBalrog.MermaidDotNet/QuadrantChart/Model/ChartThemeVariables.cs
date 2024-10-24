namespace FoggyBalrog.MermaidDotNet.QuadrantChart.Model;

/// <summary>
/// Theme variables for the chart.
/// </summary>
public record ChartThemeVariables
{
    /// <summary>
    /// Fill color of the top right quadrant.
    /// </summary>
    public string? Quadrant1Fill { get; init; }

    /// <summary>
    /// Fill color of the top left quadrant.
    /// </summary>
    public string? Quadrant2Fill { get; init; }

    /// <summary>
    /// Fill color of the bottom left quadrant.
    /// </summary>
    public string? Quadrant3Fill { get; init; }

    /// <summary>
    /// Fill color of the bottom right quadrant.
    /// </summary>
    public string? Quadrant4Fill { get; init; }

    /// <summary>
    /// Text color of the top right quadrant.
    /// </summary>
    public string? Quadrant1TextFill { get; init; }

    /// <summary>
    /// Text color of the top left quadrant.
    /// </summary>
    public string? Quadrant2TextFill { get; init; }

    /// <summary>
    /// Text color of the bottom left quadrant.
    /// </summary>
    public string? Quadrant3TextFill { get; init; }

    /// <summary>
    /// Text color of the bottom right quadrant.
    /// </summary>
    public string? Quadrant4TextFill { get; init; }

    /// <summary>
    /// Points fill color.
    /// </summary>
    public string? QuadrantPointFill { get; init; }

    /// <summary>
    /// Points text color.
    /// </summary>
    public string? QuadrantPointTextFill { get; init; }

    /// <summary>
    /// X-axis text color.
    /// </summary>
    public string? QuadrantXAxisTextFill { get; init; }

    /// <summary>
    /// Y-axis text color.
    /// </summary>
    public string? QuadrantYAxisTextFill { get; init; }

    /// <summary>
    /// Quadrants inner border color.
    /// </summary>
    public string? QuadrantInternalBorderStrokeFill { get; init; }

    /// <summary>
    /// Quadrants outer border color.
    /// </summary>
    public string? QuadrantExternalBorderStrokeFill { get; init; }

    /// <summary>
    /// Title color.
    /// </summary>
    public string? QuadrantTitleFill { get; init; }
}

