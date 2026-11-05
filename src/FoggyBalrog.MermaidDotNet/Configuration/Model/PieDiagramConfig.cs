using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record PieDiagramConfig : BaseDiagramConfig
{
    [YamlMember(Alias = "textPosition")]
    public double? TextPosition { get; set; }
}
