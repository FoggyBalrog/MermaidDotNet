using System.Runtime.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

public enum Rendered
{
    [EnumMember(Value = "dagre-d3")]
    DagreD3,

    [EnumMember(Value = "dagre-wrapper")]
    DagreWrapper,

    [EnumMember(Value = "elk")]
    Elk
}
