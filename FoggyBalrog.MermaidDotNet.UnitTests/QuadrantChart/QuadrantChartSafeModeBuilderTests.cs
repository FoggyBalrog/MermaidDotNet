using FoggyBalrog.MermaidDotNet.QuadrantChart.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class QuadrantChartSafeModeBuilderTests
{
    [Fact]
    public void CanBuildEmptyQuadrantChart()
    {
        string quadrantChart = Mermaid
            .QuadrantChart()
            .Build();

        Assert.Equal("quadrantChart", quadrantChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildSimpleQuadrantChart()
    {
        string quadrantChart = Mermaid
            .QuadrantChart()
            .AddPoint("A", 0.1, 0.2)
            .AddPoint("B", 0.3, 0.4)
            .Build();

        Assert.Equal(@"quadrantChart
    A: [0.1, 0.2]
    B: [0.3, 0.4]", quadrantChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildQuadrantChartWithTitleAndQuadrantLabels()
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

        Assert.Equal(@"quadrantChart
    title Some title
    quadrant-1 Quadrant 1
    quadrant-2 Quadrant 2
    quadrant-3 Quadrant 3
    quadrant-4 Quadrant 4
    A: [0.1, 0.2]
    B: [0.3, 0.4]", quadrantChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildQuadrantChartWithDoubleAxisLabels()
    {
        string quadrantChart = Mermaid
            .QuadrantChart()
            .SetXAxisLabel("Left", "Right")
            .SetYAxisLabel("Bottom", "Top")
            .AddPoint("A", 0.1, 0.2)
            .AddPoint("B", 0.3, 0.4)
            .Build();

        Assert.Equal(@"quadrantChart
    x-axis Left --> Right
    y-axis Bottom --> Top
    A: [0.1, 0.2]
    B: [0.3, 0.4]", quadrantChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildQuadrantChartWithSingleAxisLabels()
    {
        string quadrantChart = Mermaid
            .QuadrantChart()
            .SetXAxisLabel("Left")
            .SetYAxisLabel("Bottom")
            .AddPoint("A", 0.1, 0.2)
            .AddPoint("B", 0.3, 0.4)
            .Build();

        Assert.Equal(@"quadrantChart
    x-axis Left
    y-axis Bottom
    A: [0.1, 0.2]
    B: [0.3, 0.4]", quadrantChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildQuadrantChartWithAllLabels()
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

        Assert.Equal(@"quadrantChart
    title Some title
    x-axis Left --> Right
    y-axis Bottom --> Top
    quadrant-1 Quadrant 1
    quadrant-2 Quadrant 2
    quadrant-3 Quadrant 3
    quadrant-4 Quadrant 4
    A: [0.1, 0.2]
    B: [0.3, 0.4]", quadrantChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildQuadrantChartWithStyleConfiguration()
    {
        var config = new StyleConfiguration
        {
            ChartConfigurations = new()
            {
                ChartWidth = 400,
                ChartHeight = 600
            },
            ThemeVariables = new()
            {
                Quadrant1Fill = "red",
                Quadrant2Fill = "green",
                Quadrant3Fill = "blue",
                Quadrant4Fill = "yellow"
            }
        };

        string quadrantChart = Mermaid
            .QuadrantChart(styleConfiguration: config)
            .AddPoint("A", 0.1, 0.2)
            .AddPoint("B", 0.3, 0.4)
            .Build();

        Assert.Equal(@"%%{init: {""quadrantChart"": {""chartWidth"": 400, ""chartHeight"": 600}, ""themeVariables"": {""quadrant1Fill"": ""red"", ""quadrant2Fill"": ""green"", ""quadrant3Fill"": ""blue"", ""quadrant4Fill"": ""yellow""}}}%%
quadrantChart
    A: [0.1, 0.2]
    B: [0.3, 0.4]", quadrantChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildQuadrantChartWithPointStyling()
    {
        string quadrantChart = Mermaid
            .QuadrantChart()
            .DefineCssClass("foo", "color: #ff0000", out var foo)
            .AddPoint("A", 0.1, 0.2, "radius: 25")
            .AddPoint("B", 0.3, 0.4, "radius: 1", foo)
            .AddPoint("C", 0.5, 0.6, cssClass: foo)
            .Build();

        Assert.Equal(@"quadrantChart
    A: [0.1, 0.2] radius: 25
    B:::foo: [0.3, 0.4] radius: 1
    C:::foo: [0.5, 0.6]
    classDef foo color: #ff0000", quadrantChart, ignoreLineEndingDifferences: true);
    }
}
