using FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;
using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record SequenceDiagramConfig : BaseDiagramConfig
{
    [YamlMember(Alias = "arrowMarkerAbsolute")]
    public bool? ArrowMarkerAbsolute { get; set; }

    [YamlMember(Alias = "hideUnusedParticipants")]
    public bool? HideUnusedParticipants { get; set; }

    [YamlMember(Alias = "activationWidth")]
    public int? ActivationWidth { get; set; }

    [YamlMember(Alias = "diagramMarginX")]
    public int? DiagramMarginX { get; set; }

    [YamlMember(Alias = "diagramMarginY")]
    public int? DiagramMarginY { get; set; }

    [YamlMember(Alias = "actorMargin")]
    public int? ActorMargin { get; set; }

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

    [YamlMember(Alias = "mirrorActors")]
    public bool? MirrorActors { get; set; }

    [YamlMember(Alias = "forceMenus")]
    public bool? ForceMenus { get; set; }

    [YamlMember(Alias = "bottomMarginAdj")]
    public int? BottomMarginAdj { get; set; }

    [YamlMember(Alias = "rightAngles")]
    public bool? RightAngles { get; set; }

    [YamlMember(Alias = "showSequenceNumbers")]
    public bool? ShowSequenceNumbers { get; set; }

    [YamlMember(Alias = "actorFontSize")]
    public string? ActorFontSize { get; set; }

    [YamlMember(Alias = "actorFontFamily")]
    public string? ActorFontFamily { get; set; }

    [YamlMember(Alias = "actorFontWeight")]
    public string? ActorFontWeight { get; set; }

    [YamlMember(Alias = "noteFontSize")]
    public string? NoteFontSize { get; set; }

    [YamlMember(Alias = "noteFontFamily")]
    public string? NoteFontFamily { get; set; }

    [YamlMember(Alias = "noteFontWeight")]
    public string? NoteFontWeight { get; set; }

    [YamlMember(Alias = "noteAlign")]
    public NoteAlign? NoteAlign { get; set; }

    [YamlMember(Alias = "messageFontSize")]
    public string? MessageFontSize { get; set; }

    [YamlMember(Alias = "messageFontFamily")]
    public string? MessageFontFamily { get; set; }

    [YamlMember(Alias = "messageFontWeight")]
    public string? MessageFontWeight { get; set; }

    [YamlMember(Alias = "wrap")]
    public bool? Wrap { get; set; }

    [YamlMember(Alias = "wrapPadding")]
    public int? WrapPadding { get; set; }

    [YamlMember(Alias = "labelBoxWidth")]
    public int? LabelBoxWidth { get; set; }

    [YamlMember(Alias = "labelBoxHeight")]
    public int? LabelBoxHeight { get; set; }
}
