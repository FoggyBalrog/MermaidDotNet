using FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;
using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record ClassDiagramConfig : BaseDiagramConfig
{
    [YamlMember(Alias = "titleTopMargin")]
    public int? TitleTopMargin { get; set; }

    [YamlMember(Alias = "arrowMarkerAbsolute")]
    public bool? ArrowMarkerAbsolute { get; set; }

    [YamlMember(Alias = "dividerMargin")]
    public int? DividerMargin { get; set; }

    [YamlMember(Alias = "padding")]
    public int? Padding { get; set; }

    [YamlMember(Alias = "textHeight")]
    public int? TextHeight { get; set; }

    [YamlMember(Alias = "defaultRenderer")]
    public Rendered? DefaultRenderer { get; set; }

    [YamlMember(Alias = "nodeSpacing")]
    public int? NodeSpacing { get; set; }

    [YamlMember(Alias = "rankSpacing")]
    public int? RankSpacing { get; set; }

    [YamlMember(Alias = "diagramPadding")]
    public int? DiagramPadding { get; set; }

    [YamlMember(Alias = "htmlLabels")]
    public bool? HtmlLabels { get; set; }
}
