namespace FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

internal record Exclude(string Text)
{
    public static Exclude Monday { get; } = new("monday");
    public static Exclude Tuesday { get; } = new("tuesday");
    public static Exclude Wednesday { get; } = new("wednesday");
    public static Exclude Thursday { get; } = new("thursday");
    public static Exclude Friday { get; } = new("friday");
    public static Exclude Saturday { get; } = new("saturday");
    public static Exclude Sunday { get; } = new("sunday");
    public static Exclude Weekends { get; } = new("weekends");
    public static Exclude Date(DateTimeOffset date, string format) => new(DateFormatConverter.ToDayjsFormat(date, format));
}
