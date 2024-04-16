namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class PieChartBuilderTests
{
    [Fact]
    public void CanBuildEmptyPieChart()
    {
        var pieChart = Mermaid
            .PieChart()
            .Build();

        Assert.Equal(@"pie", pieChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildEmptyPieChartWithTitle()
    {
        var pieChart = Mermaid
            .PieChart(title: "Title")
            .Build();

        Assert.Equal(@"pie title Title", pieChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildEmptyPieChartWithDisplayValuesOnLegend()
    {
        var pieChart = Mermaid
            .PieChart(displayValuesOnLegend: true)
            .Build();

        Assert.Equal(@"pie showData", pieChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildEmptyPieChartWithTitleAndDisplayValuesOnLegend()
    {
        var pieChart = Mermaid
            .PieChart(displayValuesOnLegend: true, title: "Title")
            .Build();

        Assert.Equal(@"pie showData title Title", pieChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildPieChartWithSingleDataSet()
    {
        var pieChart = Mermaid
            .PieChart()
            .AddDataSet("Label", 42.7)
            .Build();

        Assert.Equal(@"pie
    ""Label"" : 42.7", pieChart, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildPieChartWithMultipleDataSets()
    {
        var pieChart = Mermaid
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
        var pieChart = Mermaid
            .PieChart(displayValuesOnLegend: true, title: "Title")
            .AddDataSet("Label1", 42.7)
            .AddDataSet("Label2", 57.3)
            .Build();

        Assert.Equal(@"pie showData title Title
    ""Label1"" : 42.7
    ""Label2"" : 57.3", pieChart, ignoreLineEndingDifferences: true);
    }
}
