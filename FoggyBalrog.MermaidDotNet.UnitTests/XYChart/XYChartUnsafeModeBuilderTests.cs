using FoggyBalrog.MermaidDotNet.XYChart.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.XYChart;

public class XYChartUnsafeModeBuilderTests
{
    [Fact]
    public void CanBuildDiagram()
    {
        string diagram = Mermaid
            .Unsafe
            .XYChart()
            .Build();

        Assert.Equal("xychart", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildSimpleDiagramWithTitleAndOrientation()
    {
        string diagram = Mermaid
            .Unsafe
            .XYChart(title: "My title", orientation: XYChartOrientation.Horizontal)
            .AddBarSeries([1, 2, 3, 4])
            .AddLineSeries([4, 3, 2, 1])
            .AddBarSeries([5, 6, 7, 8])
            .AddLineSeries([8, 7, 6, 5])
            .Build();

        Assert.Equal(@"---
title: My title
---
xychart horizontal
bar [1, 2, 3, 4]
line [4, 3, 2, 1]
bar [5, 6, 7, 8]
line [8, 7, 6, 5]", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithTitledAxes()
    {
        string diagram = Mermaid
            .Unsafe
            .XYChart()
            .WithTitledXAxis("foo")
            .WithTitledYAxis("bar")
            .AddBarSeries([1, 2, 3, 4])
            .Build();

        Assert.Equal(@"xychart
x-axis ""foo""
y-axis ""bar""
bar [1, 2, 3, 4]", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithNumericAxis()
    {
        string diagram = Mermaid
            .Unsafe
            .XYChart()
            .WithNumericXAxis(0, 10, "foo")
            .WithNumericYAxis(-5, 5, "bar")
            .AddBarSeries([1, 2, 3, 4])
            .Build();

        Assert.Equal(@"xychart
x-axis ""foo"" 0 --> 10
y-axis ""bar"" -5 --> 5
bar [1, 2, 3, 4]", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithCategoricalAxis()
    {
        string diagram = Mermaid
            .Unsafe
            .XYChart()
            .WithCategoricalXAxis(["A", "B", "C", "D"], "foo")
            .AddBarSeries([1, 2, 3, 4])
            .Build();

        Assert.Equal(@"xychart
x-axis ""foo"" [""A"", ""B"", ""C"", ""D""]
bar [1, 2, 3, 4]", diagram, ignoreLineEndingDifferences: true);
    }
}