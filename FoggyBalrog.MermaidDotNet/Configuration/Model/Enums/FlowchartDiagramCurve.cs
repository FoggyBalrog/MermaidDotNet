using System.Runtime.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

public enum FlowchartDiagramCurve
{
    [EnumMember(Value = "basis")]
    Basis,

    [EnumMember(Value = "linear")]
    Linear,

    [EnumMember(Value = "cardinal")]
    Cardinal
}
