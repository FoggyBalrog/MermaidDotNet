using FoggyBalrog.MermaidDotNet.BlockDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class BlockDiagramDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildSimpleDiagram()
    {
        string diagram = Mermaid
            .BlockDiagram()
            .AddBlock("foo", out var foo)
            .AddSpace()
            .AddBlock("bar", out var bar)
            .AddSpace()
            .AddBlock("baz", out var baz)
            .AddLink(foo, bar, "qux")
            .AddLink(bar, baz)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("block", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildComplexDiagram()
    {
        Block? e = null;

        string diagram = Mermaid
            .BlockDiagram(columns: 4)
            .AddBlock("A", out var a)
            .AddBlock("B", out var b)
            .AddBlock("C", out var c)
            .AddBlock("D", out var d)
            .AddCompositeBlock(composite =>
            {
                composite
                    .AddBlock("E", out e)
                    .AddBlock("F", out var f)
                    .AddCompositeBlock(nested =>
                    {
                        nested
                            .AddBlock("G", out var g)
                            .AddBlock("H", out var h)
                            .AddLink(g, h, "links G to H");
                    }, columns: 1)
                    .AddLink(e, f, "links E to F")
                    .AddLink(c, f, "links C to F");
            }, columns: 2, width: 5)
            .AddBlock("I", out var i)
            .AddLink(a, b)
            .AddLink(e!, i)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("block", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithAllShapes()
    {
        string diagram = Mermaid
            .BlockDiagram()
            .AddBlock("A", out var a, shape: BlockShape.Rectangle)
            .AddBlock("B", out var b, shape: BlockShape.RoundEdges)
            .AddBlock("C", out var c, shape: BlockShape.Stadium)
            .AddBlock("D", out var d, shape: BlockShape.Subroutine)
            .AddBlock("E", out var e, shape: BlockShape.Cylindrical)
            .AddBlock("F", out var f, shape: BlockShape.Circle)
            .AddBlock("G", out var g, shape: BlockShape.DoubleCircle)
            .AddBlock("H", out var h, shape: BlockShape.Asymmetric)
            .AddBlock("I", out var i, shape: BlockShape.Rhombus)
            .AddBlock("J", out var j, shape: BlockShape.Hexagon)
            .AddBlock("K", out var k, shape: BlockShape.Parallelogram)
            .AddBlock("L", out var l, shape: BlockShape.ParallelogramAlt)
            .AddBlock("M", out var m, shape: BlockShape.Trapezoid)
            .AddBlock("N", out var n, shape: BlockShape.TrapezoidAlt)
            .AddBlock("O", out var o, shape: BlockShape.RightArrow)
            .AddBlock("P", out var p, shape: BlockShape.LeftArrow)
            .AddBlock("Q", out var q, shape: BlockShape.UpArrow)
            .AddBlock("R", out var r, shape: BlockShape.DownArrow)
            .AddBlock("S", out var s, shape: BlockShape.XArrow)
            .AddBlock("T", out var t, shape: BlockShape.YArrow)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("block", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithStyles()
    {
        string diagram = Mermaid
            .BlockDiagram()
            .AddBlock("A", out var a)
            .AddBlock("B", out var b)
            .StyleBlock(a, "fill:#f96,stroke:#333,stroke-width:4px")
            .StyleBlock(b, "fill:#bbf,stroke:#f66,stroke-width:2px")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("block", toolingFixture.GetDiagramType(diagramResult));
    }
}

public class BlockDiagramNoValidationNoSanitizationOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildSimpleDiagram()
    {
        string diagram = Mermaid
            .BlockDiagram(options: _options)
            .AddBlock("foo", out var foo)
            .AddSpace()
            .AddBlock("bar", out var bar)
            .AddSpace()
            .AddBlock("baz", out var baz)
            .AddLink(foo, bar, "qux")
            .AddLink(bar, baz)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("block", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildComplexDiagram()
    {
        Block? e = null;

        string diagram = Mermaid
            .BlockDiagram(columns: 4, options: _options)
            .AddBlock("A", out var a)
            .AddBlock("B", out var b)
            .AddBlock("C", out var c)
            .AddBlock("D", out var d)
            .AddCompositeBlock(composite =>
            {
                composite
                    .AddBlock("E", out e)
                    .AddBlock("F", out var f)
                    .AddCompositeBlock(nested =>
                    {
                        nested
                            .AddBlock("G", out var g)
                            .AddBlock("H", out var h)
                            .AddLink(g, h, "links G to H");
                    }, columns: 1)
                    .AddLink(e, f, "links E to F")
                    .AddLink(c, f, "links C to F");
            }, columns: 2, width: 5)
            .AddBlock("I", out var i)
            .AddLink(a, b)
            .AddLink(e!, i)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("block", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithAllShapes()
    {
        string diagram = Mermaid
            .BlockDiagram(options: _options)
            .AddBlock("A", out var a, shape: BlockShape.Rectangle)
            .AddBlock("B", out var b, shape: BlockShape.RoundEdges)
            .AddBlock("C", out var c, shape: BlockShape.Stadium)
            .AddBlock("D", out var d, shape: BlockShape.Subroutine)
            .AddBlock("E", out var e, shape: BlockShape.Cylindrical)
            .AddBlock("F", out var f, shape: BlockShape.Circle)
            .AddBlock("G", out var g, shape: BlockShape.DoubleCircle)
            .AddBlock("H", out var h, shape: BlockShape.Asymmetric)
            .AddBlock("I", out var i, shape: BlockShape.Rhombus)
            .AddBlock("J", out var j, shape: BlockShape.Hexagon)
            .AddBlock("K", out var k, shape: BlockShape.Parallelogram)
            .AddBlock("L", out var l, shape: BlockShape.ParallelogramAlt)
            .AddBlock("M", out var m, shape: BlockShape.Trapezoid)
            .AddBlock("N", out var n, shape: BlockShape.TrapezoidAlt)
            .AddBlock("O", out var o, shape: BlockShape.RightArrow)
            .AddBlock("P", out var p, shape: BlockShape.LeftArrow)
            .AddBlock("Q", out var q, shape: BlockShape.UpArrow)
            .AddBlock("R", out var r, shape: BlockShape.DownArrow)
            .AddBlock("S", out var s, shape: BlockShape.XArrow)
            .AddBlock("T", out var t, shape: BlockShape.YArrow)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("block", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithStyles()
    {
        string diagram = Mermaid
            .BlockDiagram(options: _options)
            .AddBlock("A", out var a)
            .AddBlock("B", out var b)
            .StyleBlock(a, "fill:#f96,stroke:#333,stroke-width:4px")
            .StyleBlock(b, "fill:#bbf,stroke:#f66,stroke-width:2px")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("block", toolingFixture.GetDiagramType(diagramResult));
    }
}
