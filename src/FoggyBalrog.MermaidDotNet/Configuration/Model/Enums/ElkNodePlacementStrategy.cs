using System.Runtime.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

public enum ElkNodePlacementStrategy
{
    [EnumMember(Value = "SIMPLE")]
    Simple,

    [EnumMember(Value = "NETWORK_SIMPLEX")]
    NetworkSimplex,

    [EnumMember(Value = "LINEAR_SEGMENTS")]
    LinearSegments,

    [EnumMember(Value = "BRANDES_KOEPF")]
    BrandesKoepf
}
