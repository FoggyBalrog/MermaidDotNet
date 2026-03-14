namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class PieChartDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildEmptyPieChart()
    {
        string pieChart = Mermaid
            .PieChart()
            .Build();

        var pieChartResult = await toolingFixture.ValidateDiagramAsync(pieChart);

        Assert.True(pieChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(pieChart, pieChartResult));
        Assert.Equal("pie", toolingFixture.GetDiagramType(pieChartResult));
    }

    [Fact]
    public async Task CanBuildEmptyPieChartWithTitle()
    {
        string pieChart = Mermaid
            .PieChart(title: "Title")
            .Build();

        var pieChartResult = await toolingFixture.ValidateDiagramAsync(pieChart);

        Assert.True(pieChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(pieChart, pieChartResult));
        Assert.Equal("pie", toolingFixture.GetDiagramType(pieChartResult));
    }

    [Fact]
    public async Task CanBuildEmptyPieChartWithDisplayValuesOnLegend()
    {
        string pieChart = Mermaid
            .PieChart(displayValuesOnLegend: true)
            .Build();

        var pieChartResult = await toolingFixture.ValidateDiagramAsync(pieChart);

        Assert.True(pieChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(pieChart, pieChartResult));
        Assert.Equal("pie", toolingFixture.GetDiagramType(pieChartResult));
    }

    [Fact]
    public async Task CanBuildEmptyPieChartWithTitleAndDisplayValuesOnLegend()
    {
        string pieChart = Mermaid
            .PieChart(displayValuesOnLegend: true, title: "Title")
            .Build();

        var pieChartResult = await toolingFixture.ValidateDiagramAsync(pieChart);

        Assert.True(pieChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(pieChart, pieChartResult));
        Assert.Equal("pie", toolingFixture.GetDiagramType(pieChartResult));
    }

    [Fact]
    public async Task CanBuildPieChartWithSingleDataSet()
    {
        string pieChart = Mermaid
            .PieChart()
            .AddDataSet("Label", 42.7)
            .Build();

        var pieChartResult = await toolingFixture.ValidateDiagramAsync(pieChart);

        Assert.True(pieChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(pieChart, pieChartResult));
        Assert.Equal("pie", toolingFixture.GetDiagramType(pieChartResult));
    }

    [Fact]
    public async Task CanBuildPieChartWithMultipleDataSets()
    {
        string pieChart = Mermaid
            .PieChart()
            .AddDataSet("Label1", 42.7)
            .AddDataSet("Label2", 57.3)
            .Build();

        var pieChartResult = await toolingFixture.ValidateDiagramAsync(pieChart);

        Assert.True(pieChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(pieChart, pieChartResult));
        Assert.Equal("pie", toolingFixture.GetDiagramType(pieChartResult));
    }

    [Fact]
    public async Task CanBuildFullPieChart()
    {
        string pieChart = Mermaid
            .PieChart(displayValuesOnLegend: true, title: "Title")
            .AddDataSet("Label1", 42.7)
            .AddDataSet("Label2", 57.3)
            .Build();

        var pieChartResult = await toolingFixture.ValidateDiagramAsync(pieChart);

        Assert.True(pieChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(pieChart, pieChartResult));
        Assert.Equal("pie", toolingFixture.GetDiagramType(pieChartResult));
    }
}

public class PieChartNoValidationNoSanitizationOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildEmptyPieChart()
    {
        string pieChart = Mermaid
            .PieChart(options: _options)
            .Build();

        var pieChartResult = await toolingFixture.ValidateDiagramAsync(pieChart);

        Assert.True(pieChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(pieChart, pieChartResult));
        Assert.Equal("pie", toolingFixture.GetDiagramType(pieChartResult));
    }

    [Fact]
    public async Task CanBuildEmptyPieChartWithTitle()
    {
        string pieChart = Mermaid
            .PieChart(title: "Title", options: _options)
            .Build();

        var pieChartResult = await toolingFixture.ValidateDiagramAsync(pieChart);

        Assert.True(pieChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(pieChart, pieChartResult));
        Assert.Equal("pie", toolingFixture.GetDiagramType(pieChartResult));
    }

    [Fact]
    public async Task CanBuildEmptyPieChartWithDisplayValuesOnLegend()
    {
        string pieChart = Mermaid
            .PieChart(displayValuesOnLegend: true, options: _options)
            .Build();

        var pieChartResult = await toolingFixture.ValidateDiagramAsync(pieChart);

        Assert.True(pieChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(pieChart, pieChartResult));
        Assert.Equal("pie", toolingFixture.GetDiagramType(pieChartResult));
    }

    [Fact]
    public async Task CanBuildEmptyPieChartWithTitleAndDisplayValuesOnLegend()
    {
        string pieChart = Mermaid
            .PieChart(displayValuesOnLegend: true, title: "Title", options: _options)
            .Build();

        var pieChartResult = await toolingFixture.ValidateDiagramAsync(pieChart);

        Assert.True(pieChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(pieChart, pieChartResult));
        Assert.Equal("pie", toolingFixture.GetDiagramType(pieChartResult));
    }

    [Fact]
    public async Task CanBuildPieChartWithSingleDataSet()
    {
        string pieChart = Mermaid
            .PieChart(options: _options)
            .AddDataSet("Label", 42.7)
            .Build();

        var pieChartResult = await toolingFixture.ValidateDiagramAsync(pieChart);

        Assert.True(pieChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(pieChart, pieChartResult));
        Assert.Equal("pie", toolingFixture.GetDiagramType(pieChartResult));
    }

    [Fact]
    public async Task CanBuildPieChartWithMultipleDataSets()
    {
        string pieChart = Mermaid
            .PieChart(options: _options)
            .AddDataSet("Label1", 42.7)
            .AddDataSet("Label2", 57.3)
            .Build();

        var pieChartResult = await toolingFixture.ValidateDiagramAsync(pieChart);

        Assert.True(pieChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(pieChart, pieChartResult));
        Assert.Equal("pie", toolingFixture.GetDiagramType(pieChartResult));
    }

    [Fact]
    public async Task CanBuildFullPieChart()
    {
        string pieChart = Mermaid
            .PieChart(displayValuesOnLegend: true, title: "Title", options: _options)
            .AddDataSet("Label1", 42.7)
            .AddDataSet("Label2", 57.3)
            .Build();

        var pieChartResult = await toolingFixture.ValidateDiagramAsync(pieChart);

        Assert.True(pieChartResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(pieChart, pieChartResult));
        Assert.Equal("pie", toolingFixture.GetDiagramType(pieChartResult));
    }
}
