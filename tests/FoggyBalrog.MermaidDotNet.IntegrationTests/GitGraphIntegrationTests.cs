using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.GitGraph.Model;

namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class GitGraphDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildSimpleGitGraph()
    {
        string graph = Mermaid
            .GitGraph()
            .Commit()
            .Commit()
            .Commit()
            .Build();

        var graphResult = await toolingFixture.ValidateDiagramAsync(graph);

        Assert.True(graphResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(graph, graphResult));
        Assert.Equal("gitGraph", toolingFixture.GetDiagramType(graphResult));
    }

    [Fact]
    public async Task CanBuildGitGraphWithBranches()
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

        var graphResult = await toolingFixture.ValidateDiagramAsync(graph);

        Assert.True(graphResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(graph, graphResult));
        Assert.Equal("gitGraph", toolingFixture.GetDiagramType(graphResult));
    }

    [Fact]
    public async Task CanBuildGitGraphWithBranchOrdering()
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

        var graphResult = await toolingFixture.ValidateDiagramAsync(graph);

        Assert.True(graphResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(graph, graphResult));
        Assert.Equal("gitGraph", toolingFixture.GetDiagramType(graphResult));
    }

    [Fact]
    public async Task CanBuildGitGraphWithAdditionalCommitProperties()
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

        var graphResult = await toolingFixture.ValidateDiagramAsync(graph);

        Assert.True(graphResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(graph, graphResult));
        Assert.Equal("gitGraph", toolingFixture.GetDiagramType(graphResult));
    }

    [Fact]
    public async Task CanBuildGitGraphWithAllSettings()
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

        var graphResult = await toolingFixture.ValidateDiagramAsync(graph);

        Assert.True(graphResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(graph, graphResult));
        Assert.Equal("gitGraph", toolingFixture.GetDiagramType(graphResult));
    }
}

public class GitGraphNoValidationNoSanitizationOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildSimpleGitGraph()
    {
        string graph = Mermaid
            .GitGraph(options: _options)
            .Commit()
            .Commit()
            .Commit()
            .Build();

        var graphResult = await toolingFixture.ValidateDiagramAsync(graph);

        Assert.True(graphResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(graph, graphResult));
        Assert.Equal("gitGraph", toolingFixture.GetDiagramType(graphResult));
    }

    [Fact]
    public async Task CanBuildGitGraphWithBranches()
    {
        string graph = Mermaid
            .GitGraph(options: _options)
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

        var graphResult = await toolingFixture.ValidateDiagramAsync(graph);

        Assert.True(graphResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(graph, graphResult));
        Assert.Equal("gitGraph", toolingFixture.GetDiagramType(graphResult));
    }

    [Fact]
    public async Task CanBuildGitGraphWithBranchOrdering()
    {
        string graph = Mermaid
            .GitGraph(options: _options)
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

        var graphResult = await toolingFixture.ValidateDiagramAsync(graph);

        Assert.True(graphResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(graph, graphResult));
        Assert.Equal("gitGraph", toolingFixture.GetDiagramType(graphResult));
    }

    [Fact]
    public async Task CanBuildGitGraphWithAdditionalCommitProperties()
    {
        string graph = Mermaid
            .GitGraph(options: _options)
            .Commit(id: "1", type: CommitType.Normal, tag: "v1.0.0")
            .Commit(id: "2", type: CommitType.Highlight, tag: "v1.0.1")
            .Branch("dev", out Branch dev)
            .Commit(id: "3", type: CommitType.Reverse, tag: "v1.0.2")
            .CheckoutMain()
            .Merge(dev, id: "4", type: CommitType.Normal, tag: "v1.0.3")
            .Build();

        var graphResult = await toolingFixture.ValidateDiagramAsync(graph);

        Assert.True(graphResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(graph, graphResult));
        Assert.Equal("gitGraph", toolingFixture.GetDiagramType(graphResult));
    }

    [Fact]
    public async Task CanBuildGitGraphWithAllSettings()
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
                vertical: true, options: _options)
            .Commit()
            .Build();

        var graphResult = await toolingFixture.ValidateDiagramAsync(graph);

        Assert.True(graphResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(graph, graphResult));
        Assert.Equal("gitGraph", toolingFixture.GetDiagramType(graphResult));
    }
}
