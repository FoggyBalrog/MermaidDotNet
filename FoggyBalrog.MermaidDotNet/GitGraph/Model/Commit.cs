namespace FoggyBalrog.MermaidDotNet.GitGraph.Model;

internal record Commit(string? Id, CommitType Type, string? Tag) : IGitCommand;
