using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record NodeLabel
{
    [YamlMember(Alias = "width")]
    public int? Width { get; set; }

    [YamlMember(Alias = "height")]
    public int? Height { get; set; }

    [YamlMember(Alias = "x")]
    public int? X { get; set; }

    [YamlMember(Alias = "y")]
    public int? Y { get; set; }
}
