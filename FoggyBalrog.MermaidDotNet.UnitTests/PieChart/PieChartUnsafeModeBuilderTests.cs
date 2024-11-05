﻿namespace FoggyBalrog.MermaidDotNet.UnitTests.PieChart;

public class PieChartUnsafeModeBuilderTests
{
    [Fact]
    public void CanBuildEmptyPieChart()
    {
        string pieChart = Mermaid
            .Unsafe
            .PieChart()
            .Build();

        Assert.Equal("pie", pieChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildEmptyPieChartWithTitle()
    {
        string pieChart = Mermaid
            .Unsafe
            .PieChart(title: "Title")
            .Build();

        Assert.Equal(@"---
title: Title
---
pie", pieChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildEmptyPieChartWithDisplayValuesOnLegend()
    {
        string pieChart = Mermaid
            .Unsafe
            .PieChart(displayValuesOnLegend: true)
            .Build();

        Assert.Equal("pie showData", pieChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildEmptyPieChartWithTitleAndDisplayValuesOnLegend()
    {
        string pieChart = Mermaid
            .Unsafe
            .PieChart(displayValuesOnLegend: true, title: "Title")
            .Build();

        Assert.Equal(@"---
title: Title
---
pie showData", pieChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildPieChartWithSingleDataSet()
    {
        string pieChart = Mermaid
            .Unsafe
            .PieChart()
            .AddDataSet("Label", 42.7)
            .Build();

        Assert.Equal(@"pie
    ""Label"" : 42.7", pieChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildPieChartWithMultipleDataSets()
    {
        string pieChart = Mermaid
            .Unsafe
            .PieChart()
            .AddDataSet("Label1", 42.7)
            .AddDataSet("Label2", 57.3)
            .Build();

        Assert.Equal(@"pie
    ""Label1"" : 42.7
    ""Label2"" : 57.3", pieChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildFullPieChart()
    {
        string pieChart = Mermaid
            .Unsafe
            .PieChart(displayValuesOnLegend: true, title: "Title")
            .AddDataSet("Label1", 42.7)
            .AddDataSet("Label2", 57.3)
            .Build();

        Assert.Equal(@"---
title: Title
---
pie showData
    ""Label1"" : 42.7
    ""Label2"" : 57.3", pieChart, ignoreLineEndingDifferences: true);
    }
}
