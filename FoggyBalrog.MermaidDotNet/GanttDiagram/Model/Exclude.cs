namespace FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

/// <summary>
/// Represents a time period to exclude from the gantt chart.
/// </summary>
/// <param name="Text">The normalized text of the time period to exclude.</param>
internal record Exclude(string Text)
{
    /// <summary>
    /// Monday
    /// </summary>
    public static Exclude Monday { get; } = new("monday");

    /// <summary>
    /// Tuesday
    /// </summary>
    public static Exclude Tuesday { get; } = new("tuesday");

    /// <summary>
    /// Wednesday
    /// </summary>
    public static Exclude Wednesday { get; } = new("wednesday");

    /// <summary>
    /// Thursday
    /// </summary>
    public static Exclude Thursday { get; } = new("thursday");

    /// <summary>
    /// Friday
    /// </summary>
    public static Exclude Friday { get; } = new("friday");

    /// <summary>
    /// Saturday
    /// </summary>
    public static Exclude Saturday { get; } = new("saturday");

    /// <summary>
    /// Sunday
    /// </summary>
    public static Exclude Sunday { get; } = new("sunday");

    /// <summary>
    /// Weekends
    /// </summary>
    public static Exclude Weekends { get; } = new("weekends");

    /// <summary>
    /// Crreates an exclude for a specific date.
    /// </summary>
    /// <param name="date">The date to exclude.</param>
    /// <param name="format">The format of the date.</param>
    /// <returns>The exclude for the date.</returns>
    public static Exclude Date(DateTimeOffset date, string format) => new(DateFormatConverter.ToDayjsFormat(date, format));
}
