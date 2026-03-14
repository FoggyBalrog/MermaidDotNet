using FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;
using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record StateDiagramConfig : BaseDiagramConfig
{
    [YamlMember(Alias = "titleTopMargin")]
    public int? TitleTopMargin { get; set; }

    [YamlMember(Alias = "arrowMarkerAbsolute")]
    public bool? ArrowMarkerAbsolute { get; set; }

    [YamlMember(Alias = "dividerMargin")]
    public int? DividerMargin { get; set; }

    [YamlMember(Alias = "sizeUnit")]
    public int? SizeUnit { get; set; }

    [YamlMember(Alias = "padding")]
    public int? Padding { get; set; }

    [YamlMember(Alias = "textHeight")]
    public int? TextHeight { get; set; }

    [YamlMember(Alias = "titleShift")]
    public int? TitleShift { get; set; }

    [YamlMember(Alias = "noteMargin")]
    public int? NoteMargin { get; set; }

    [YamlMember(Alias = "nodeSpacing")]
    public int? NodeSpacing { get; set; }

    [YamlMember(Alias = "rankSpacing")]
    public int? RankSpacing { get; set; }

    [YamlMember(Alias = "forkWidth")]
    public int? ForkWidth { get; set; }

    [YamlMember(Alias = "forkHeight")]
    public int? ForkHeight { get; set; }

    [YamlMember(Alias = "miniPadding")]
    public int? MiniPadding { get; set; }

    [YamlMember(Alias = "fontSizeFactor")]
    public int? FontSizeFactor { get; set; }

    [YamlMember(Alias = "fontSize")]
    public int? FontSize { get; set; }

    [YamlMember(Alias = "labelHeight")]
    public int? LabelHeight { get; set; }

    [YamlMember(Alias = "edgeLengthFactor")]
    public string? EdgeLengthFactor { get; set; }

    [YamlMember(Alias = "compositTitleSize")]
    public int? CompositTitleSize { get; set; }

    [YamlMember(Alias = "radius")]
    public int? Radius { get; set; }

    [YamlMember(Alias = "defaultRenderer")]
    public Rendered? DefaultRenderer { get; set; }
}
