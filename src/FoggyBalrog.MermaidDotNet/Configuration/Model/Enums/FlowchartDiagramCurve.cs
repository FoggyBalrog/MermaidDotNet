using System.Runtime.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

public enum FlowchartDiagramCurve
{
    [EnumMember(Value = "basis")]
    Basis,

    [EnumMember(Value = "bumpX")]
    BumpX,

    [EnumMember(Value = "bumpY")]
    BumpY,

    [EnumMember(Value = "cardinal")]
    Cardinal,

    [EnumMember(Value = "catmullRom")]
    CatmullRom,

    [EnumMember(Value = "linear")]
    Linear,

    [EnumMember(Value = "monotoneX")]
    MonotoneX,

    [EnumMember(Value = "monotoneY")]
    MonotoneY,

    [EnumMember(Value = "natural")]
    Natural,

    [EnumMember(Value = "step")]
    Step,

    [EnumMember(Value = "stepAfter")]
    StepAfter,

    [EnumMember(Value = "stepBefore")]
    StepBefore
}
