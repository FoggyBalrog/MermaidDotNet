using FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;
using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

/// <summary>
/// Represents the configuration for Mermaid. See the official documentation for details (<see cref="https://mermaid.js.org/config/setup/interfaces/mermaid.MermaidConfig.html"/>).
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

    [YamlMember(Alias = "elk")]
    public ElkConfig? Elk { get; set; }

    [YamlMember(Alias = "er")]
    public ErDiagramConfig? Er { get; set; }

    [YamlMember(Alias = "flowchart")]
    public FlowchartDiagramConfig? Flowchart { get; set; }

    [YamlMember(Alias = "fontFamily")]
    public string? FontFamily { get; set; }

    [YamlMember(Alias = "fontSize")]
    public int? FontSize { get; set; }

    [YamlMember(Alias = "forceLegacyMathML")]
    public bool? ForceLegacyMathML { get; set; }

    [YamlMember(Alias = "gantt")]
    public GanttDiagramConfig? Gantt { get; set; }

    [YamlMember(Alias = "gitGraph")]
    public GitGraphDiagramConfig? Git { get; set; }

    [YamlMember(Alias = "handDrawnSeed")]
    public int? HandDrawnSeed { get; set; }

    [YamlMember(Alias = "htmlLabels")]
    public bool? HtmlLabels { get; set; }

    [YamlMember(Alias = "journey")]
    public JourneyDiagramConfig? Journey { get; set; }

    [YamlMember(Alias = "layout")]
    public string? Layout { get; set; }

    [YamlMember(Alias = "legacyMathML")]
    public bool? LegacyMathML { get; set; }

    [YamlMember(Alias = "logLevel")]
    public LogLevel? LogLevel { get; set; }

    [YamlMember(Alias = "look")]
    public Look? Look { get; set; }

    [YamlMember(Alias = "markdownAutoWrap")]
    public bool? MarkdownAutoWrap { get; set; }

    [YamlMember(Alias = "maxEdges")]
    public int? MaxEdges { get; set; }

    [YamlMember(Alias = "maxTextSize")]
    public int? MaxTextSize { get; set; }

    [YamlMember(Alias = "mindmap")]
    public MindmapDiagramConfig? Mindmap { get; set; }

    [YamlMember(Alias = "pie")]
    public PieDiagramConfig? Pie { get; set; }

    [YamlMember(Alias = "quadrantChart")]
    public QuadrantChartConfig? QuadrantChart { get; set; }

    [YamlMember(Alias = "requirement")]
    public RequirementDiagramConfig? Requirement { get; set; }

    [YamlMember(Alias = "sankey")]
    public SankeyDiagramConfig? Sankey { get; set; }

    [YamlMember(Alias = "secure")]
    public string[]? Secure { get; set; }

    [YamlMember(Alias = "securityLevel")]
    public SecurityLevel? SecurityLevel { get; set; }

    [YamlMember(Alias = "sequence")]
    public SequenceDiagramConfig? Sequence { get; set; }

    [YamlMember(Alias = "startOnLoad")]
    public bool? StartOnLoad { get; set; }

    [YamlMember(Alias = "state")]
    public StateDiagramConfig? State { get; set; }

    [YamlMember(Alias = "suppressErrorRendering")]
    public bool? SuppressErrorRendering { get; set; }

    [YamlMember(Alias = "theme")]
    public Theme? Theme { get; set; }

    [YamlMember(Alias = "themeCSS")]
    public string? ThemeCSS { get; set; }

    [YamlMember(Alias = "themeVariables")]
    public Dictionary<string, string>? ThemeVariables { get; set; }

    [YamlMember(Alias = "timeline")]
    public TimelineDiagramConfig? Timeline { get; set; }

    [YamlMember(Alias = "wrap")]
    public bool? Wrap { get; set; }
}
