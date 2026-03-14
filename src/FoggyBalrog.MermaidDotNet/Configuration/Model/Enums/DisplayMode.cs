using System.Runtime.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

public enum DisplayMode
{
    [EnumMember(Value = "")]
    None,

    [EnumMember(Value = "compact")]
    Compact
}
