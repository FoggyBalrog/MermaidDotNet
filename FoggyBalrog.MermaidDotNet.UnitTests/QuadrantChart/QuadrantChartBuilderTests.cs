namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class QuadrantChartBuilderTests
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
}
