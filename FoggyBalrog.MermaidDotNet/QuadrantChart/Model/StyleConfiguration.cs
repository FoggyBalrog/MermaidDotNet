namespace FoggyBalrog.MermaidDotNet.QuadrantChart.Model;

/// <summary>
/// Configuration for the style of the quadrant chart.
/// </summary>
public record StyleConfiguration
{
    /// <summary>
    /// Chart configuration settings.
    /// </summary>
    public ChartConfigurations? ChartConfigurations { get; init; }

    /// <summary>
    /// Theme variables for the chart.
    /// </summary>
    public ChartThemeVariables? ThemeVariables { get; init; }
}
