using System.Runtime.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

public enum SecurityLevel
{
    [EnumMember(Value = "strict")]
    Strict,

    [EnumMember(Value = "loose")]
    Loose,

    [EnumMember(Value = "antiscript")]
    Antiscript,

    [EnumMember(Value = "sandbox")]
    Sandbox
}
