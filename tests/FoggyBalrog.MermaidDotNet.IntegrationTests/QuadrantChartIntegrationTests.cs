namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class QuadrantChartDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildEmptyQuadrantChart()
    {
        string quadrantChart = Mermaid
            .QuadrantChart()
            .Build();

        var quadrantChartResult = await toolingFixture.ValidateDiagramAsync(quadrantChart);

        Assert.True(quadrantChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(quadrantChart, quadrantChartResult));
        Assert.Equal("quadrantChart", toolingFixture.GetDiagramType(quadrantChartResult));
    }

    [Fact]
    public async Task CanBuildSimpleQuadrantChart()
    {
        string quadrantChart = Mermaid
            .QuadrantChart()
            .AddPoint("A", 0.1, 0.2)
            .AddPoint("B", 0.3, 0.4)
            .Build();

        var quadrantChartResult = await toolingFixture.ValidateDiagramAsync(quadrantChart);

        Assert.True(quadrantChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(quadrantChart, quadrantChartResult));
        Assert.Equal("quadrantChart", toolingFixture.GetDiagramType(quadrantChartResult));
    }

    [Fact]
    public async Task CanBuildQuadrantChartWithTitleAndQuadrantLabels()
    {
        string quadrantChart = Mermaid
            .QuadrantChart(
                title: "Some title",
                quadrant1: "Quadrant 1",
                quadrant2: "Quadrant 2",
                quadrant3: "Quadrant 3",
                quadrant4: "Quadrant 4")
            .AddPoint("A", 0.1, 0.2)
            .AddPoint("B", 0.3, 0.4)
            .Build();

        var quadrantChartResult = await toolingFixture.ValidateDiagramAsync(quadrantChart);

        Assert.True(quadrantChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(quadrantChart, quadrantChartResult));
        Assert.Equal("quadrantChart", toolingFixture.GetDiagramType(quadrantChartResult));
    }

    [Fact]
    public async Task CanBuildQuadrantChartWithDoubleAxisLabels()
    {
        string quadrantChart = Mermaid
            .QuadrantChart()
            .SetXAxisLabel("Left", "Right")
            .SetYAxisLabel("Bottom", "Top")
            .AddPoint("A", 0.1, 0.2)
            .AddPoint("B", 0.3, 0.4)
            .Build();

        var quadrantChartResult = await toolingFixture.ValidateDiagramAsync(quadrantChart);

        Assert.True(quadrantChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(quadrantChart, quadrantChartResult));
        Assert.Equal("quadrantChart", toolingFixture.GetDiagramType(quadrantChartResult));
    }

    [Fact]
    public async Task CanBuildQuadrantChartWithSingleAxisLabels()
    {
        string quadrantChart = Mermaid
            .QuadrantChart()
            .SetXAxisLabel("Left")
            .SetYAxisLabel("Bottom")
            .AddPoint("A", 0.1, 0.2)
            .AddPoint("B", 0.3, 0.4)
            .Build();

        var quadrantChartResult = await toolingFixture.ValidateDiagramAsync(quadrantChart);

        Assert.True(quadrantChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(quadrantChart, quadrantChartResult));
        Assert.Equal("quadrantChart", toolingFixture.GetDiagramType(quadrantChartResult));
    }

    [Fact]
    public async Task CanBuildQuadrantChartWithAllLabels()
    {
        string quadrantChart = Mermaid
            .QuadrantChart(
                title: "Some title",
                quadrant1: "Quadrant 1",
                quadrant2: "Quadrant 2",
                quadrant3: "Quadrant 3",
                quadrant4: "Quadrant 4")
            .SetXAxisLabel("Left", "Right")
            .SetYAxisLabel("Bottom", "Top")
            .AddPoint("A", 0.1, 0.2)
            .AddPoint("B", 0.3, 0.4)
            .Build();

        var quadrantChartResult = await toolingFixture.ValidateDiagramAsync(quadrantChart);

        Assert.True(quadrantChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(quadrantChart, quadrantChartResult));
        Assert.Equal("quadrantChart", toolingFixture.GetDiagramType(quadrantChartResult));
    }

    [Fact]
    public async Task CanBuildQuadrantChartWithPointStyling()
    {
        string quadrantChart = Mermaid
            .QuadrantChart()
            .DefineCssClass("foo", "color: #ff0000", out var foo)
            .AddPoint("A", 0.1, 0.2, "radius: 25")
            .AddPoint("B", 0.3, 0.4, "radius: 1", foo)
            .AddPoint("C", 0.5, 0.6, cssClass: foo)
            .Build();

        var quadrantChartResult = await toolingFixture.ValidateDiagramAsync(quadrantChart);

        Assert.True(quadrantChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(quadrantChart, quadrantChartResult));
        Assert.Equal("quadrantChart", toolingFixture.GetDiagramType(quadrantChartResult));
    }
}

public class QuadrantChartNoValidationNoSanitizationOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildEmptyQuadrantChart()
    {
        string quadrantChart = Mermaid
            .QuadrantChart(options: _options)
            .Build();

        var quadrantChartResult = await toolingFixture.ValidateDiagramAsync(quadrantChart);

        Assert.True(quadrantChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(quadrantChart, quadrantChartResult));
        Assert.Equal("quadrantChart", toolingFixture.GetDiagramType(quadrantChartResult));
    }

    [Fact]
    public async Task CanBuildSimpleQuadrantChart()
    {
        string quadrantChart = Mermaid
            .QuadrantChart(options: _options)
            .AddPoint("A", 0.1, 0.2)
            .AddPoint("B", 0.3, 0.4)
            .Build();

        var quadrantChartResult = await toolingFixture.ValidateDiagramAsync(quadrantChart);

        Assert.True(quadrantChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(quadrantChart, quadrantChartResult));
        Assert.Equal("quadrantChart", toolingFixture.GetDiagramType(quadrantChartResult));
    }

    [Fact]
    public async Task CanBuildQuadrantChartWithTitleAndQuadrantLabels()
    {
        string quadrantChart = Mermaid
            .QuadrantChart(
                title: "Some title",
                quadrant1: "Quadrant 1",
                quadrant2: "Quadrant 2",
                quadrant3: "Quadrant 3",
                quadrant4: "Quadrant 4", options: _options)
            .AddPoint("A", 0.1, 0.2)
            .AddPoint("B", 0.3, 0.4)
            .Build();

        var quadrantChartResult = await toolingFixture.ValidateDiagramAsync(quadrantChart);

        Assert.True(quadrantChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(quadrantChart, quadrantChartResult));
        Assert.Equal("quadrantChart", toolingFixture.GetDiagramType(quadrantChartResult));
    }

    [Fact]
    public async Task CanBuildQuadrantChartWithDoubleAxisLabels()
    {
        string quadrantChart = Mermaid
            .QuadrantChart(options: _options)
            .SetXAxisLabel("Left", "Right")
            .SetYAxisLabel("Bottom", "Top")
            .AddPoint("A", 0.1, 0.2)
            .AddPoint("B", 0.3, 0.4)
            .Build();

        var quadrantChartResult = await toolingFixture.ValidateDiagramAsync(quadrantChart);

        Assert.True(quadrantChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(quadrantChart, quadrantChartResult));
        Assert.Equal("quadrantChart", toolingFixture.GetDiagramType(quadrantChartResult));
    }

    [Fact]
    public async Task CanBuildQuadrantChartWithSingleAxisLabels()
    {
        string quadrantChart = Mermaid
            .QuadrantChart(options: _options)
            .SetXAxisLabel("Left")
            .SetYAxisLabel("Bottom")
            .AddPoint("A", 0.1, 0.2)
            .AddPoint("B", 0.3, 0.4)
            .Build();

        var quadrantChartResult = await toolingFixture.ValidateDiagramAsync(quadrantChart);

        Assert.True(quadrantChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(quadrantChart, quadrantChartResult));
        Assert.Equal("quadrantChart", toolingFixture.GetDiagramType(quadrantChartResult));
    }

    [Fact]
    public async Task CanBuildQuadrantChartWithAllLabels()
    {
        string quadrantChart = Mermaid
            .QuadrantChart(
                title: "Some title",
                quadrant1: "Quadrant 1",
                quadrant2: "Quadrant 2",
                quadrant3: "Quadrant 3",
                quadrant4: "Quadrant 4", options: _options)
            .SetXAxisLabel("Left", "Right")
            .SetYAxisLabel("Bottom", "Top")
            .AddPoint("A", 0.1, 0.2)
            .AddPoint("B", 0.3, 0.4)
            .Build();

        var quadrantChartResult = await toolingFixture.ValidateDiagramAsync(quadrantChart);

        Assert.True(quadrantChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(quadrantChart, quadrantChartResult));
        Assert.Equal("quadrantChart", toolingFixture.GetDiagramType(quadrantChartResult));
    }

    [Fact]
    public async Task CanBuildQuadrantChartWithPointStyling()
    {
        string quadrantChart = Mermaid
            .QuadrantChart(options: _options)
            .DefineCssClass("foo", "color: #ff0000", out var foo)
            .AddPoint("A", 0.1, 0.2, "radius: 25")
            .AddPoint("B", 0.3, 0.4, "radius: 1", foo)
            .AddPoint("C", 0.5, 0.6, cssClass: foo)
            .Build();

        var quadrantChartResult = await toolingFixture.ValidateDiagramAsync(quadrantChart);

        Assert.True(quadrantChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(quadrantChart, quadrantChartResult));
        Assert.Equal("quadrantChart", toolingFixture.GetDiagramType(quadrantChartResult));
    }
}
