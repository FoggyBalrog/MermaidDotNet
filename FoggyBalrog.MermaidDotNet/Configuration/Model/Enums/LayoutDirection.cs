using System.Runtime.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

public enum LayoutDirection
{
    [EnumMember(Value = "TB")]
    TopBottom,

    [EnumMember(Value = "BT")]
    BottomTop,

    [EnumMember(Value = "LR")]
    LeftRight,

    [EnumMember(Value = "RL")]
    RightLeft
}
