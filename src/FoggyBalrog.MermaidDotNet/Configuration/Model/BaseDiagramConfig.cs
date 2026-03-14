using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public abstract record BaseDiagramConfig
{
    [YamlMember(Alias = "useWidth")]
    public int? UseWidth { get; set; }

    [YamlMember(Alias = "useMaxWidth")]
    public bool? UseMaxWidth { get; set; }
}
