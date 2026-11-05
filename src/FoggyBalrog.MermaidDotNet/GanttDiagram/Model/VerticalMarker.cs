namespace FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

public record VerticalMarker : IGanttItem
{
    public VerticalMarker(string id, string name, DateTimeOffset position, TimeSpan nextTaskOffset)
    {
        Id = id;
        Name = name;
        Position = position;
        NextTaskOffset = nextTaskOffset;
    }

    public string Id { get; }
    public string Name { get; }
    public DateTimeOffset Position { get; }
    public TimeSpan NextTaskOffset { get; }
}