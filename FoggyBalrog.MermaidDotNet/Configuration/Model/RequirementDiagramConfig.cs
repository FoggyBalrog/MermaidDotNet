using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record RequirementDiagramConfig : BaseDiagramConfig
{
    [YamlMember(Alias = "rect_fill")]
    public string? RectFill { get; set; }

    [YamlMember(Alias = "text_color")]
    public string? TextColor { get; set; }

    [YamlMember(Alias = "rect_border_size")]
    public string? RectBorderSize { get; set; }

    [YamlMember(Alias = "rect_border_color")]
    public string? RectBorderColor { get; set; }

    [YamlMember(Alias = "rect_min_width")]
    public int? RectMinWidth { get; set; }

    [YamlMember(Alias = "rect_min_height")]
    public int? RectMinHeight { get; set; }

    [YamlMember(Alias = "fontSize")]
    public int? FontSize { get; set; }

    [YamlMember(Alias = "rect_padding")]
    public int? RectPadding { get; set; }

    [YamlMember(Alias = "line_height")]
    public int? LineHeight { get; set; }
}
