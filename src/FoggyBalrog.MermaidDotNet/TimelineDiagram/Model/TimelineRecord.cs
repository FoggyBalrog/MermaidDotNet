namespace FoggyBalrog.MermaidDotNet.TimelineDiagram.Model;

internal record TimelineRecord(string TimePeriod, string[] Events) : ITimelineItem;
