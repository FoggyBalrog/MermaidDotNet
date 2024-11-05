using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record SubGraphTitleMargin
{
    [YamlMember(Alias = "top")]
    public int? Top { get; set; }

    [YamlMember(Alias = "bottom")]
    public int? Bottom { get; set; }
}
