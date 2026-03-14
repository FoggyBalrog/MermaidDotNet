using YamlDotNet.Serialization;

namespace FoggyBalrog.MermaidDotNet.Configuration.Model;

public record KanbanDiagramConfig : BaseDiagramConfig
{
    [YamlMember(Alias = "ticketBaseUrl")]
    public string? TicketBaseUrl { get; set; }
}
