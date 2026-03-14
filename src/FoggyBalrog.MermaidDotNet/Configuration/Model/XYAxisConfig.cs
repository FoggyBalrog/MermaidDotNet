using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record XYAxisConfig
{
    [YamlMember(Alias = "showLabel")]
    public bool? ShowLabel { get; set; }

    [YamlMember(Alias = "labelFontSize")]
    public int? LabelFontSize { get; set; }

    [YamlMember(Alias = "labelPadding")]
    public int? LabelPadding { get; set; }

    [YamlMember(Alias = "showTitle")]
    public bool? ShowTitle { get; set; }

    [YamlMember(Alias = "titleFontSize")]
    public int? TitleFontSize { get; set; }

    [YamlMember(Alias = "titlePadding")]
    public int? TitlePadding { get; set; }

    [YamlMember(Alias = "showTick")]
    public bool? ShowTick { get; set; }

    [YamlMember(Alias = "tickLength")]
    public int? TickLength { get; set; }

    [YamlMember(Alias = "tickWidth")]
    public int? TickWidth { get; set; }

    [YamlMember(Alias = "showAxisLine")]
    public bool? ShowAxisLine { get; set; }

    [YamlMember(Alias = "axisLineWidth")]
    public int? AxisLineWidth { get; set; }
}