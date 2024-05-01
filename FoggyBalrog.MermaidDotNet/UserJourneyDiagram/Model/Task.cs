namespace FoggyBalrog.MermaidDotNet.UserJourneyDiagram.Model;

internal record Task(string Description, int Score, string[] Actors) : IUserJourneyDiagramItem;
