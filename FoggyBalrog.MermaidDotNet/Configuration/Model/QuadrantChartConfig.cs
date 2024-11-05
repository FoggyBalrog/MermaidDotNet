using FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;
using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record QuadrantChartConfig : BaseDiagramConfig
{
    [YamlMember(Alias = "chartWidth")]
    public int? ChartWidth { get; set; }

    [YamlMember(Alias = "chartHeight")]
    public int? ChartHeight { get; set; }

    [YamlMember(Alias = "titleFontSize")]
    public int? TitleFontSize { get; set; }

    [YamlMember(Alias = "titlePadding")]
    public int? TitlePadding { get; set; }

    [YamlMember(Alias = "quadrantPadding")]
    public int? QuadrantPadding { get; set; }

    [YamlMember(Alias = "xAxisLabelPadding")]
    public int? XAxisLabelPadding { get; set; }

    [YamlMember(Alias = "yAxisLabelPadding")]
    public int? YAxisLabelPadding { get; set; }

    [YamlMember(Alias = "xAxisLabelFontSize")]
    public int? XAxisLabelFontSize { get; set; }

    [YamlMember(Alias = "yAxisLabelFontSize")]
    public int? YAxisLabelFontSize { get; set; }

    [YamlMember(Alias = "quadrantLabelFontSize")]
    public int? QuadrantLabelFontSize { get; set; }

    [YamlMember(Alias = "quadrantTextTopPadding")]
    public int? QuadrantTextTopPadding { get; set; }

    [YamlMember(Alias = "pointTextPadding")]
    public int? PointTextPadding { get; set; }

    [YamlMember(Alias = "pointLabelFontSize")]
    public int? PointLabelFontSize { get; set; }

    [YamlMember(Alias = "pointRadius")]
    public int? PointRadius { get; set; }

    [YamlMember(Alias = "xAxisPosition")]
    public XAxisPosition? XAxisPosition { get; set; }

    [YamlMember(Alias = "yAxisPosition")]
    public YAxisPosition? YAxisPosition { get; set; }

    [YamlMember(Alias = "quadrantInternalBorderStrokeWidth")]
    public int? QuadrantInternalBorderStrokeWidth { get; set; }

    [YamlMember(Alias = "quadrantExternalBorderStrokeWidth")]
    public int? QuadrantExternalBorderStrokeWidth { get; set; }
}
