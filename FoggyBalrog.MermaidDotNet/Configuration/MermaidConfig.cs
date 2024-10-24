using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration;

/// <summary>
/// Represents the configuration for Mermaid. See the official documentation for details.
/// </summary>
public record MermaidConfig
{
    [YamlMember(Alias = "altFontFamily")]
    public string? AltFontFamily { get; set; }

    [YamlMember(Alias = "arrowMarkerAbsolute")]
    public bool? ArrowMarkerAbsolute { get; set; }

    [YamlMember(Alias = "class")]
    public ClassDiagramConfig? Class { get; set; }

    [YamlMember(Alias = "darkMode")]
    public bool? DarkMode { get; set; }

    [YamlMember(Alias = "deterministicIDSeed")]
    public string? DeterministicIDSeed { get; set; }

    [YamlMember(Alias = "deterministicIds")]
    public bool? DeterministicIds { get; set; }

    [YamlMember(Alias = "dompurifyConfig")]
    public object? DomPurifyConfig { get; set; }

    // elk
    [YamlMember(Alias = "elk")]
    public ElkConfig? Elk { get; set; }

    [YamlMember(Alias = "er")]
    public ErDiagramConfig? Er { get; set; }
}

public enum ElkNodePlacementStrategy
{
    SIMPLE,
    NETWORK_SIMPLEX,
    LINEAR_SEGMENTS,
    BRANDES_KOEPF
}

public enum ElkCycleBreakingStrategy
{
    GREEDY,
    DEPTH_FIRST,
    INTERACTIVE,
    MODEL_ORDER,
    GREEDY_MODEL_ORDER
}

public record ElkConfig
{
    [YamlMember(Alias = "mergeEdges")]
    public bool? MergeEdges { get; set; }

    [YamlMember(Alias = "nodePlacementStrategy")]
    public ElkNodePlacementStrategy? NodePlacementStrategy { get; set; }

    [YamlMember(Alias = "cycleBreakingStrategy")]
    public ElkCycleBreakingStrategy? CycleBreakingStrategy { get; set; }
}

public abstract record BaseDiagramConfig
{
    [YamlMember(Alias = "useWidth")]
    public int? UseWidth { get; set; }

    [YamlMember(Alias = "useMaxWidth")]
    public bool? UseMaxWidth { get; set; }
}

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
    public string? DefaultRenderer { get; set; }

    [YamlMember(Alias = "nodeSpacing")]
    public int? NodeSpacing { get; set; }

    [YamlMember(Alias = "rankSpacing")]
    public int? RankSpacing { get; set; }

    [YamlMember(Alias = "diagramPadding")]
    public int? DiagramPadding { get; set; }

    [YamlMember(Alias = "htmlLabels")]
    public bool? HtmlLabels { get; set; }
}
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

public enum LayoutDirection
{
    TB,
    BT,
    LR,
    RL
}