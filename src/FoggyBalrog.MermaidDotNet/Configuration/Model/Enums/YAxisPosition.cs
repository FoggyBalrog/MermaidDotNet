using System.Runtime.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

public enum YAxisPosition
{
    [EnumMember(Value = "left")]
    Left,

    [EnumMember(Value = "right")]
    Right
}
