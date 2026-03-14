using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record SankeyDiagramConfig : BaseDiagramConfig
{
    [YamlMember(Alias = "height")]
    public int? Height { get; set; }

    [YamlMember(Alias = "width")]
    public int? Width { get; set; }

    [YamlMember(Alias = "linkColor")]
    public string? LinkColor { get; set; }

    [YamlMember(Alias = "nodeAlignment")]
    public string? NodeAlignment { get; set; }
}
