using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record MindmapDiagramConfig : BaseDiagramConfig
{
    [YamlMember(Alias = "padding")]
    public int? Padding { get; set; }

    [YamlMember(Alias = "maxNodeWidth")]
    public int? MaxNodeWidth { get; set; }
}
