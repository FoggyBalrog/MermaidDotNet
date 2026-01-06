using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record XYChartConfig : BaseDiagramConfig
{
    [YamlMember(Alias = "width")]
    public int? Width { get; set; }

    [YamlMember(Alias = "height")]
    public int? Height { get; set; }

    [YamlMember(Alias = "titlePadding")]
    public int? TitlePadding { get; set; }

    [YamlMember(Alias = "titleFontSize")]
    public int? TitleFontSize { get; set; }

    [YamlMember(Alias = "showTitle")]
    public bool? ShowTitle { get; set; }

    [YamlMember(Alias = "xAxis")]
    public XYAxisConfig? XAxis { get; set; }

    [YamlMember(Alias = "yAxis")]
    public XYAxisConfig? YAxis { get; set; }

    [YamlMember(Alias = "chartOrientation")]
    public string? ChartOrientation { get; set; }

    [YamlMember(Alias = "plotReservedSpacePercent")]
    public int? PlotReservedSpacePercent { get; set; }

    [YamlMember(Alias = "showDataLabel")]
    public bool? ShowDataLabel { get; set; }
}
