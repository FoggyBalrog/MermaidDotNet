using FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;
using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record ErDiagramConfig : BaseDiagramConfig
{
    [YamlMember(Alias = "titleTopMargin")]
    public int? TitleTopMargin { get; set; }

    [YamlMember(Alias = "diagramPadding")]
    public int? DiagramPadding { get; set; }

    [YamlMember(Alias = "layoutDirection")]
    public LayoutDirection? LayoutDirection { get; set; }

    [YamlMember(Alias = "minEntityWidth")]
    public int? MinEntityWidth { get; set; }

    [YamlMember(Alias = "minEntityHeight")]
    public int? MinEntityHeight { get; set; }

    [YamlMember(Alias = "entityPadding")]
    public int? EntityPadding { get; set; }

    [YamlMember(Alias = "stroke")]
    public string? Stroke { get; set; }

    [YamlMember(Alias = "fill")]
    public string? Fill { get; set; }

    [YamlMember(Alias = "fontSize")]
    public int? FontSize { get; set; }
}
