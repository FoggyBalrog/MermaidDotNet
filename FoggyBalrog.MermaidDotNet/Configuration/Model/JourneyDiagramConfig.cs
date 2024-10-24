using FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;
using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record JourneyDiagramConfig : BaseDiagramConfig
{
    [YamlMember(Alias = "diagramMarginX")]
    public int? DiagramMarginX { get; set; }

    [YamlMember(Alias = "diagramMarginY")]
    public int? DiagramMarginY { get; set; }

    [YamlMember(Alias = "leftMargin")]
    public int? LeftMargin { get; set; }

    [YamlMember(Alias = "width")]
    public int? Width { get; set; }

    [YamlMember(Alias = "height")]
    public int? Height { get; set; }

    [YamlMember(Alias = "boxMargin")]
    public int? BoxMargin { get; set; }

    [YamlMember(Alias = "boxTextMargin")]
    public int? BoxTextMargin { get; set; }

    [YamlMember(Alias = "noteMargin")]
    public int? NoteMargin { get; set; }

    [YamlMember(Alias = "messageMargin")]
    public int? MessageMargin { get; set; }

    [YamlMember(Alias = "messageAlign")]
    public MessageAlign? MessageAlign { get; set; }

    [YamlMember(Alias = "bottomMarginAdj")]
    public int? BottomMarginAdj { get; set; }

    [YamlMember(Alias = "rightAngles")]
    public bool? RightAngles { get; set; }

    [YamlMember(Alias = "taskFontSize")]
    public string? TaskFontSize { get; set; }

    [YamlMember(Alias = "taskFontFamily")]
    public string? TaskFontFamily { get; set; }

    [YamlMember(Alias = "taskMargin")]
    public int? TaskMargin { get; set; }

    [YamlMember(Alias = "activationWidth")]
    public int? ActivationWidth { get; set; }

    [YamlMember(Alias = "textPlacement")]
    public string? TextPlacement { get; set; }

    [YamlMember(Alias = "actorColours")]
    public string[]? ActorColours { get; set; }

    [YamlMember(Alias = "sectionFills")]
    public string[]? SectionFills { get; set; }

    [YamlMember(Alias = "sectionColours")]
    public string[]? SectionColours { get; set; }
}
