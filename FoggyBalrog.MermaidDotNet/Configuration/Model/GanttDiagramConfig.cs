using FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;
using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record GanttDiagramConfig : BaseDiagramConfig
{
    [YamlMember(Alias = "titleTopMargin")]
    public int? TitleTopMargin { get; set; }

    [YamlMember(Alias = "barHeight")]
    public int? BarHeight { get; set; }

    [YamlMember(Alias = "barGap")]
    public int? BarGap { get; set; }

    [YamlMember(Alias = "topPadding")]
    public int? TopPadding { get; set; }

    [YamlMember(Alias = "rightPadding")]
    public int? RightPadding { get; set; }

    [YamlMember(Alias = "leftPadding")]
    public int? LeftPadding { get; set; }

    [YamlMember(Alias = "gridLineStartPadding")]
    public int? GridLineStartPadding { get; set; }

    [YamlMember(Alias = "fontSize")]
    public int? FontSize { get; set; }

    [YamlMember(Alias = "sectionFontSize")]
    public string? SectionFontSize { get; set; }

    [YamlMember(Alias = "numberSectionStyles")]
    public int? NumberSectionStyles { get; set; }

    [YamlMember(Alias = "axisFormat")]
    public string? AxisFormat { get; set; }

    [YamlMember(Alias = "tickInterval")]
    public string? TickInterval { get; set; }

    [YamlMember(Alias = "topAxis")]
    public bool? TopAxis { get; set; }

    [YamlMember(Alias = "displayMode")]
    public DisplayMode? DisplayMode { get; set; }

    [YamlMember(Alias = "weekday")]
    public Weekday? Weekday { get; set; }
}
