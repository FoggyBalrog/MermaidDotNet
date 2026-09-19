using FoggyBalrog.MermaidDotNet.XYChart.Model;

namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class XYChartDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildDiagram()
    {
        string diagram = Mermaid
            .XYChart(options: new MermaidDotNetOptions { TargetMermaidVersion = MermaidVersion.V11_10 })
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("xychart", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildSimpleDiagramWithTitleAndOrientation()
    {
        string diagram = Mermaid
            .XYChart(title: "My title", orientation: XYChartOrientation.Horizontal, options: new MermaidDotNetOptions { TargetMermaidVersion = MermaidVersion.V11_10 })
            .AddBarSeries([1, 2, 3, 4])
                    .AddLineSeries([4, 3, 2, 1])
                    .AddBarSeries([5, 6, 7, 8])
                    .AddLineSeries([8, 7, 6, 5])
                    .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("xychart", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithTitledAxes()
    {
        string diagram = Mermaid
            .XYChart(options: new MermaidDotNetOptions { TargetMermaidVersion = MermaidVersion.V11_10 })
            .WithTitledXAxis("foo")
            .WithTitledYAxis("bar")
            .AddBarSeries([1, 2, 3, 4])
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("xychart", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithNumericAxis()
    {
        string diagram = Mermaid
            .XYChart(options: new MermaidDotNetOptions { TargetMermaidVersion = MermaidVersion.V11_10 })
            .WithNumericXAxis(0, 10, "foo")
            .WithNumericYAxis(-5, 5, "bar")
            .AddBarSeries([1, 2, 3, 4])
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("xychart", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithCategoricalAxis()
    {
        string diagram = Mermaid
            .XYChart(options: new MermaidDotNetOptions { TargetMermaidVersion = MermaidVersion.V11_10 })
            .WithCategoricalXAxis(["A", "B", "C", "D"], "foo")
            .AddBarSeries([1, 2, 3, 4])
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("xychart", toolingFixture.GetDiagramType(diagramResult));
    }
}

public class XYChartNoValidationNoSanitizationOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildDiagram()
    {
        string diagram = Mermaid
            .XYChart(options: _options with { TargetMermaidVersion = MermaidVersion.V11_10 })
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("xychart", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildSimpleDiagramWithTitleAndOrientation()
    {
        string diagram = Mermaid
            .XYChart(title: "My title", orientation: XYChartOrientation.Horizontal, options: _options with { TargetMermaidVersion = MermaidVersion.V11_10 })
            .AddBarSeries([1, 2, 3, 4])
            .AddLineSeries([4, 3, 2, 1])
            .AddBarSeries([5, 6, 7, 8])
            .AddLineSeries([8, 7, 6, 5])
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("xychart", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithTitledAxes()
    {
        string diagram = Mermaid
            .XYChart(options: _options with { TargetMermaidVersion = MermaidVersion.V11_10 })
            .WithTitledXAxis("foo")
            .WithTitledYAxis("bar")
            .AddBarSeries([1, 2, 3, 4])
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("xychart", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithNumericAxis()
    {
        string diagram = Mermaid
            .XYChart(options: _options with { TargetMermaidVersion = MermaidVersion.V11_10 })
            .WithNumericXAxis(0, 10, "foo")
            .WithNumericYAxis(-5, 5, "bar")
            .AddBarSeries([1, 2, 3, 4])
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("xychart", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithCategoricalAxis()
    {
        string diagram = Mermaid
            .XYChart(options: _options with { TargetMermaidVersion = MermaidVersion.V11_10 })
            .WithCategoricalXAxis(["A", "B", "C", "D"], "foo")
            .AddBarSeries([1, 2, 3, 4])
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("xychart", toolingFixture.GetDiagramType(diagramResult));
    }
}
