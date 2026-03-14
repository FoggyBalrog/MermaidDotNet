using FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;
using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record ElkConfig
{
    [YamlMember(Alias = "mergeEdges")]
    public bool? MergeEdges { get; set; }

    [YamlMember(Alias = "nodePlacementStrategy")]
    public ElkNodePlacementStrategy? NodePlacementStrategy { get; set; }

    [YamlMember(Alias = "cycleBreakingStrategy")]
    public ElkCycleBreakingStrategy? CycleBreakingStrategy { get; set; }
}
