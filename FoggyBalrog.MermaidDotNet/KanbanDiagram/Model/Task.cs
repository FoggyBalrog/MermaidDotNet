namespace FoggyBalrog.MermaidDotNet.KanbanDiagram.Model;

internal record Task(string Description, string? Assigned, string? Ticket, Priority? Priority)
{
    public bool HasMetadata => Assigned is not null || Ticket is not null || Priority is not null;
}
