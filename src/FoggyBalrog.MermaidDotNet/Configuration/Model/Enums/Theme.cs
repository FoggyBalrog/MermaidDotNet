using System.Runtime.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

public enum Theme
{
    [EnumMember(Value = "default")]
    Default,

    [EnumMember(Value = "base")]
    Base,

    [EnumMember(Value = "dark")]
    Dark,

    [EnumMember(Value = "forest")]
    Forest,

    [EnumMember(Value = "neutral")]
    Neutral,

    [EnumMember(Value = "null")]
    Null
}
