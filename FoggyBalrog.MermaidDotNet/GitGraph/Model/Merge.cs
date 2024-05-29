namespace FoggyBalrog.MermaidDotNet.GitGraph.Model;

internal record Merge(string BranchName, string? Id, CommitType Type, string? Tag) : IGitCommand;
