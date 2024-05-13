namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class QuadrantChartBuilderTests
{
    [Fact]
    public void CanBuildEmptyQuadrantChart()
    {
        var quadrantChart = Mermaid
            .QuadrantChart()
            .Build();

        Assert.Equal(@"quadrantChart", quadrantChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildSimpleQuadrantChart()
    {
        var quadrantChart = Mermaid
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
        var quadrantChart = Mermaid
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
        var quadrantChart = Mermaid
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
        var quadrantChart = Mermaid
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
        var quadrantChart = Mermaid
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
    public void ThrowsWhenCoordinatesAreOutOfRange()
    {
        Assert.Throws<InvalidOperationException>(() => Mermaid.QuadrantChart().AddPoint("A", -0.1, 0.2));
        Assert.Throws<InvalidOperationException>(() => Mermaid.QuadrantChart().AddPoint("A", 0.1, -0.2));
        Assert.Throws<InvalidOperationException>(() => Mermaid.QuadrantChart().AddPoint("A", 1.1, 0.2));
        Assert.Throws<InvalidOperationException>(() => Mermaid.QuadrantChart().AddPoint("A", 0.1, 1.2));
    }
}
