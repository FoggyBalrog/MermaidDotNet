using System.Runtime.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

public enum ElkCycleBreakingStrategy
{
    [EnumMember(Value = "GREEDY")]
    Greedy,

    [EnumMember(Value = "DEPTH_FIRST")]
    DepthFirst,

    [EnumMember(Value = "INTERACTIVE")]
    Interactive,

    [EnumMember(Value = "MODEL_ORDER")]
    ModelOrder,

    [EnumMember(Value = "GREEDY_MODEL_ORDER")]
    GreedyModelOrder
}
