using FoggyBalrog.MermaidDotNet.MindMap.Model;

namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class MindMapDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildSimpleMindMap()
    {
        string mindMap = Mermaid
            .MindMap("Root")
            .Build();

        var mindMapResult = await toolingFixture.ValidateDiagramAsync(mindMap);

        Assert.True(mindMapResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(mindMap, mindMapResult));
        Assert.Equal("mindmap", toolingFixture.GetDiagramType(mindMapResult));
    }

    [Fact]
    public async Task CanBuildComplexMindMap()
    {
        string mindMap = Mermaid
            .MindMap("Root")
            .AddNode("Node 1", out Node node1)
            .AddNode("Node 2", out Node node2, node1)
            .AddNode("Node 3", out Node node3, node1)
            .AddNode("Node 4", out Node _, node2)
            .AddNode("Node 5", out Node _, node2)
            .AddNode("Node 6", out Node _, node3)
            .AddNode("Node 7", out Node _, node3)
            .Build();

        var mindMapResult = await toolingFixture.ValidateDiagramAsync(mindMap);

        Assert.True(mindMapResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(mindMap, mindMapResult));
        Assert.Equal("mindmap", toolingFixture.GetDiagramType(mindMapResult));
    }

    [Fact]
    public async Task CanBuildMindMapWithDifferentShapes()
    {
        string mindMap = Mermaid
            .MindMap("Root", rootShape: NodeShape.Hexagon)
            .AddNode("Node 1", out Node node1, shape: NodeShape.Square)
            .AddNode("Node 2", out Node node2, shape: NodeShape.RoundedSquare, parent: node1)
            .AddNode("Node 3", out Node _, shape: NodeShape.Circle, parent: node1)
            .AddNode("Node 4", out Node _, shape: NodeShape.Bang, parent: node2)
            .AddNode("Node 5", out Node _, shape: NodeShape.Cloud, parent: node2)
            .Build();

        var mindMapResult = await toolingFixture.ValidateDiagramAsync(mindMap);

        Assert.True(mindMapResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(mindMap, mindMapResult));
        Assert.Equal("mindmap", toolingFixture.GetDiagramType(mindMapResult));
    }

    [Fact]
    public async Task CanBuildMindMapWithIconAndClasses()
    {
        string mindMap = Mermaid
            .MindMap("Root", rootIcon: "fa fa-home", rootClasses: ["class1", "class2"])
            .AddNode("Node 1", out Node node1, icon: "fa fa-book", classes: ["class3", "class4"])
            .AddNode("Node 2", out Node _, icon: "fa fa-hat-wizard", classes: ["class5", "class6"], parent: node1)
            .Build();

        var mindMapResult = await toolingFixture.ValidateDiagramAsync(mindMap);

        Assert.True(mindMapResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(mindMap, mindMapResult));
        Assert.Equal("mindmap", toolingFixture.GetDiagramType(mindMapResult));
    }

    [Fact]
    public async Task CanBuildMindMapWithMarkdown()
    {
        string mindMap = Mermaid
            .MindMap("Root", rootIsMarkdown: true, rootShape: NodeShape.Square)
            .AddNode("Node 1", out Node node1, isMarkdown: true, shape: NodeShape.Square)
            .AddNode("Node 2", out Node _, parent: node1, isMarkdown: true, shape: NodeShape.Square)
            .Build();

        var mindMapResult = await toolingFixture.ValidateDiagramAsync(mindMap);

        Assert.True(mindMapResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(mindMap, mindMapResult));
        Assert.Equal("mindmap", toolingFixture.GetDiagramType(mindMapResult));
    }
}

public class MindMapNoValidationNoSanitizationOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildSimpleMindMap()
    {
        string mindMap = Mermaid
            .MindMap("Root", options: _options)
            .Build();

        var mindMapResult = await toolingFixture.ValidateDiagramAsync(mindMap);

        Assert.True(mindMapResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(mindMap, mindMapResult));
        Assert.Equal("mindmap", toolingFixture.GetDiagramType(mindMapResult));
    }

    [Fact]
    public async Task CanBuildComplexMindMap()
    {
        string mindMap = Mermaid
            .MindMap("Root", options: _options)
            .AddNode("Node 1", out Node node1)
            .AddNode("Node 2", out Node node2, node1)
            .AddNode("Node 3", out Node node3, node1)
            .AddNode("Node 4", out Node _, node2)
            .AddNode("Node 5", out Node _, node2)
            .AddNode("Node 6", out Node _, node3)
            .AddNode("Node 7", out Node _, node3)
            .Build();

        var mindMapResult = await toolingFixture.ValidateDiagramAsync(mindMap);

        Assert.True(mindMapResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(mindMap, mindMapResult));
        Assert.Equal("mindmap", toolingFixture.GetDiagramType(mindMapResult));
    }

    [Fact]
    public async Task CanBuildMindMapWithDifferentShapes()
    {
        string mindMap = Mermaid
            .MindMap("Root", rootShape: NodeShape.Hexagon, options: _options)
            .AddNode("Node 1", out Node node1, shape: NodeShape.Square)
            .AddNode("Node 2", out Node node2, shape: NodeShape.RoundedSquare, parent: node1)
            .AddNode("Node 3", out Node _, shape: NodeShape.Circle, parent: node1)
            .AddNode("Node 4", out Node _, shape: NodeShape.Bang, parent: node2)
            .AddNode("Node 5", out Node _, shape: NodeShape.Cloud, parent: node2)
            .Build();

        var mindMapResult = await toolingFixture.ValidateDiagramAsync(mindMap);

        Assert.True(mindMapResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(mindMap, mindMapResult));
        Assert.Equal("mindmap", toolingFixture.GetDiagramType(mindMapResult));
    }

    [Fact]
    public async Task CanBuildMindMapWithIconAndClasses()
    {
        string mindMap = Mermaid
            .MindMap("Root", rootIcon: "fa fa-home", rootClasses: ["class1", "class2"], options: _options)
            .AddNode("Node 1", out Node node1, icon: "fa fa-book", classes: ["class3", "class4"])
            .AddNode("Node 2", out Node _, icon: "fa fa-hat-wizard", classes: ["class5", "class6"], parent: node1)
            .Build();

        var mindMapResult = await toolingFixture.ValidateDiagramAsync(mindMap);

        Assert.True(mindMapResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(mindMap, mindMapResult));
        Assert.Equal("mindmap", toolingFixture.GetDiagramType(mindMapResult));
    }

    [Fact]
    public async Task CanBuildMindMapWithMarkdown()
    {
        string mindMap = Mermaid
            .MindMap("Root", rootIsMarkdown: true, rootShape: NodeShape.Square, options: _options)
            .AddNode("Node 1", out Node node1, isMarkdown: true, shape: NodeShape.Square)
            .AddNode("Node 2", out Node _, parent: node1, isMarkdown: true, shape: NodeShape.Square)
            .Build();

        var mindMapResult = await toolingFixture.ValidateDiagramAsync(mindMap);

        Assert.True(mindMapResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(mindMap, mindMapResult));
        Assert.Equal("mindmap", toolingFixture.GetDiagramType(mindMapResult));
    }
}
