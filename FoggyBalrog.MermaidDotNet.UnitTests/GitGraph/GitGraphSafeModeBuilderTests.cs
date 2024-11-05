using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.GitGraph.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class GitGraphSafeModeBuilderTests
{
    [Fact]
    public void CanBuildSimpleGitGraph()
    {
        string graph = Mermaid
            .GitGraph()
            .Commit()
            .Commit()
            .Commit()
            .Build();

        Assert.Equal(@"gitGraph
    commit
    commit
    commit", graph, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildGitGraphWithBranches()
    {
        string graph = Mermaid
            .GitGraph()
            .Commit()
            .Branch("dev", out Branch dev)
            .Commit()
            .Checkout(dev)
            .Commit()
            .Commit()
            .CheckoutMain()
            .Commit()
            .Merge(dev)
            .Commit()
            .Build();

        Assert.Equal(@"gitGraph
    commit
    branch dev
    commit
    checkout dev
    commit
    commit
    checkout main
    commit
    merge dev
    commit", graph, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildGitGraphWithBranchOrdering()
    {
        string graph = Mermaid
            .GitGraph()
            .Branch("dev", out Branch dev, order: 1)
            .Branch("feature", out Branch feature, order: 3)
            .Branch("bugfix", out Branch bugfix, order: 2)
            .Commit()
            .Checkout(feature)
            .Commit()
            .Checkout(bugfix)
            .Commit()
            .Checkout(dev)
            .Commit()
            .Build();

        Assert.Equal(@"gitGraph
    branch dev order: 1
    branch feature order: 3
    branch bugfix order: 2
    commit
    checkout feature
    commit
    checkout bugfix
    commit
    checkout dev
    commit", graph, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildGitGraphWithAdditionalCommitProperties()
    {
        string graph = Mermaid
            .GitGraph()
            .Commit(id: "1", type: CommitType.Normal, tag: "v1.0.0")
            .Commit(id: "2", type: CommitType.Highlight, tag: "v1.0.1")
            .Branch("dev", out Branch dev)
            .Commit(id: "3", type: CommitType.Reverse, tag: "v1.0.2")
            .CheckoutMain()
            .Merge(dev, id: "4", type: CommitType.Normal, tag: "v1.0.3")
            .Build();

        Assert.Equal(@"gitGraph
    commit id: ""1"" tag: ""v1.0.0""
    commit id: ""2"" type: HIGHLIGHT tag: ""v1.0.1""
    branch dev
    commit id: ""3"" type: REVERSE tag: ""v1.0.2""
    checkout main
    merge dev id: ""4"" tag: ""v1.0.3""", graph, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildGitGraphWithAllSettings()
    {
        var config = new MermaidConfig
        {
            Git = new()
            {
                ParallelCommits = true
            }
        };

        string graph = Mermaid
            .GitGraph(
                title: "My Git Graph",
                config: config,
                vertical: true)
            .Commit()
            .Build();

        Assert.Equal(@"---
title: My Git Graph
config:
  gitGraph:
    parallelCommits: true
---
gitGraph TB:
    commit", graph, ignoreLineEndingDifferences: true);
    }
}
