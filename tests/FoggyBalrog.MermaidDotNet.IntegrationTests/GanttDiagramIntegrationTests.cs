using System.Globalization;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;
using FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class GanttDiagramDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildGanttDiagramWithAllTaskKinds()
    {
        string diagram = Mermaid
            .GanttDiagram()
            .AddTask("Foo", Date("2024-05-01"), Date("2024-05-05"), out GanttTask t1)
            .AddTask("Bar", Date("2024-05-08"), TimeSpan.FromSeconds(90), out GanttTask t2)
            .AddTask("Baz", t1, Date("2024-05-09"), out GanttTask _)
            .AddTask("Qux", t1, TimeSpan.FromDays(2), out GanttTask _)
            .AddTask("Quux", Date("2024-05-04"), t2, out GanttTask _)
            .AddTask("Corge", t1, t2, out GanttTask _)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("gantt", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildGanttDiagramWithSections()
    {
        string diagram = Mermaid
            .GanttDiagram()
            .AddTask("Foo", Date("2024-05-01"), Date("2024-05-05"), out GanttTask t1)
            .AddSection("Section 1")
            .AddTask("Bar", Date("2024-05-08"), TimeSpan.FromDays(3), out GanttTask t2)
            .AddTask("Baz", t1, Date("2024-05-09"), out GanttTask _)
            .AddTask("Qux", t1, TimeSpan.FromDays(2), out GanttTask _)
            .AddSection("Section 2")
            .AddTask("Quux", Date("2024-05-04"), t2, out GanttTask _)
            .AddTask("Corge", t1, t2, out GanttTask _)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("gantt", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildGanttDiagramWithAllTagsCombinations()
    {
        string diagram = Mermaid
            .GanttDiagram()
            .AddTask("Task 1", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Active)
            .AddTask("Task 2", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Done)
            .AddTask("Task 3", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Critical)
            .AddTask("Task 4", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Milestone)
            .AddTask("Task 5", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Active | TaskTags.Done)
            .AddTask("Task 6", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Active | TaskTags.Critical)
            .AddTask("Task 7", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Active | TaskTags.Milestone)
            .AddTask("Task 8", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Done | TaskTags.Critical)
            .AddTask("Task 9", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Done | TaskTags.Milestone)
            .AddTask("Task 10", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Critical | TaskTags.Milestone)
            .AddTask("Task 11", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Active | TaskTags.Done | TaskTags.Critical)
            .AddTask("Task 12", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Active | TaskTags.Done | TaskTags.Milestone)
            .AddTask("Task 13", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Active | TaskTags.Critical | TaskTags.Milestone)
            .AddTask("Task 14", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Done | TaskTags.Critical | TaskTags.Milestone)
            .AddTask("Task 15", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Active | TaskTags.Done | TaskTags.Critical | TaskTags.Milestone)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("gantt", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildGanttDiagramWithExcludes()
    {
        string diagram = Mermaid
            .GanttDiagram()
            .ExcludeMonday()
            .ExcludeTuesday()
            .ExcludeWednesday()
            .ExcludeThursday()
            .ExcludeFriday()
            .ExcludeSaturday()
            .ExcludeSunday()
            .ExcludeWeekends()
            .ExcludeDates(Date("2024-05-01"), Date("2024-05-05"))
            .AddTask("Foo", Date("2024-04-30"), Date("2024-05-06"), out GanttTask _)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("gantt", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildGanttDiagramWithConfiguration()
    {
        var config = new MermaidConfig
        {
            Gantt = new GanttDiagramConfig
            {
                AxisFormat = "%d-%m",
                DisplayMode = DisplayMode.Compact,
                TickInterval = "1week",
                Weekday = Weekday.Monday
            }
        };

        string diagram1 = Mermaid
            .GanttDiagram(
                title: "My Gantt",
                config: config,
                hideTodayMarker: true,
                dateFormat: "DD-MM-YYYY")
            .AddTask("Foo", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _)
            .Build();

        string diagram2 = Mermaid
            .GanttDiagram(
                title: "My Gantt",
                config: config,
                hideTodayMarker: false,
                todayMarkerCss: "stroke: #d3d3d3; stroke-width: 2px;",
                dateFormat: "DD-MM-YYYY")
            .AddTask("Foo", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _)
            .Build();

        var diagram1Result = await toolingFixture.ValidateDiagramAsync(diagram1);

        Assert.True(diagram1Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram1, diagram1Result));
        Assert.Equal("gantt", toolingFixture.GetDiagramType(diagram1Result));

        var diagram2Result = await toolingFixture.ValidateDiagramAsync(diagram2);

        Assert.True(diagram2Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram2, diagram2Result));
        Assert.Equal("gantt", toolingFixture.GetDiagramType(diagram2Result));
    }

    [Fact]
    public async Task CanBuildGanttDiagramWithClickBindings()
    {
        string diagram = Mermaid
            .GanttDiagram()
            .AddTask("Foo", Date("2024-05-01"), Date("2024-05-05"), out GanttTask t1)
            .AddTask("Bar", Date("2024-05-08"), Date("2024-05-12"), out GanttTask t2)
            .AddHyperlink(t1, "https://example.com")
            .AddCallback(t2, "myFunction")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("gantt", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildGanttDiagramWithVerticalMarker()
    {
        string diagram = Mermaid
            .GanttDiagram()
            .AddTask("Foo", Date("2024-05-01"), Date("2024-05-05"), out GanttTask t1)
            .AddTask("Bar", Date("2024-05-08"), Date("2024-05-12"), out GanttTask t2)
            .AddVerticalMarker("Milestone 1", Date("2024-05-03"))
            .AddVerticalMarker("Milestone 2", Date("2024-05-10"), TimeSpan.FromDays(1))
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("gantt", toolingFixture.GetDiagramType(diagramResult));
    }

    private static DateTimeOffset Date(string date)
    {
        return DateTimeOffset.Parse(date, CultureInfo.InvariantCulture);
    }
}

public class GanttDiagramNoValidationNoSanitizationOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildGanttDiagramWithAllTaskKinds()
    {
        string diagram = Mermaid
            .GanttDiagram(options: _options)
            .AddTask("Foo", Date("2024-05-01"), Date("2024-05-05"), out GanttTask t1)
            .AddTask("Bar", Date("2024-05-08"), TimeSpan.FromDays(3), out GanttTask t2)
            .AddTask("Baz", t1, Date("2024-05-09"), out GanttTask _)
            .AddTask("Qux", t1, TimeSpan.FromDays(2), out GanttTask _)
            .AddTask("Quux", Date("2024-05-04"), t2, out GanttTask _)
            .AddTask("Corge", t1, t2, out GanttTask _)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("gantt", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildGanttDiagramWithSections()
    {
        string diagram = Mermaid
            .GanttDiagram(options: _options)
            .AddTask("Foo", Date("2024-05-01"), Date("2024-05-05"), out GanttTask t1)
            .AddSection("Section 1")
            .AddTask("Bar", Date("2024-05-08"), TimeSpan.FromDays(3), out GanttTask t2)
            .AddTask("Baz", t1, Date("2024-05-09"), out GanttTask _)
            .AddTask("Qux", t1, TimeSpan.FromDays(2), out GanttTask _)
            .AddSection("Section 2")
            .AddTask("Quux", Date("2024-05-04"), t2, out GanttTask _)
            .AddTask("Corge", t1, t2, out GanttTask _)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("gantt", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildGanttDiagramWithAllTagsCombinations()
    {
        string diagram = Mermaid
            .GanttDiagram(options: _options)
            .AddTask("Task 1", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Active)
            .AddTask("Task 2", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Done)
            .AddTask("Task 3", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Critical)
            .AddTask("Task 4", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Milestone)
            .AddTask("Task 5", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Active | TaskTags.Done)
            .AddTask("Task 6", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Active | TaskTags.Critical)
            .AddTask("Task 7", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Active | TaskTags.Milestone)
            .AddTask("Task 8", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Done | TaskTags.Critical)
            .AddTask("Task 9", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Done | TaskTags.Milestone)
            .AddTask("Task 10", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Critical | TaskTags.Milestone)
            .AddTask("Task 11", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Active | TaskTags.Done | TaskTags.Critical)
            .AddTask("Task 12", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Active | TaskTags.Done | TaskTags.Milestone)
            .AddTask("Task 13", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Active | TaskTags.Critical | TaskTags.Milestone)
            .AddTask("Task 14", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Done | TaskTags.Critical | TaskTags.Milestone)
            .AddTask("Task 15", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _, TaskTags.Active | TaskTags.Done | TaskTags.Critical | TaskTags.Milestone)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("gantt", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildGanttDiagramWithExcludes()
    {
        string diagram = Mermaid
            .GanttDiagram(options: _options)
            .ExcludeMonday()
            .ExcludeTuesday()
            .ExcludeWednesday()
            .ExcludeThursday()
            .ExcludeFriday()
            .ExcludeSaturday()
            .ExcludeSunday()
            .ExcludeWeekends()
            .ExcludeDates(Date("2024-05-01"), Date("2024-05-05"))
            .AddTask("Foo", Date("2024-04-30"), Date("2024-05-06"), out GanttTask _)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("gantt", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildGanttDiagramWithConfiguration()
    {
        var config = new MermaidConfig
        {
            Gantt = new GanttDiagramConfig
            {
                AxisFormat = "%d-%m",
                DisplayMode = DisplayMode.Compact,
                TickInterval = "1week",
                Weekday = Weekday.Monday
            }
        };

        string diagram = Mermaid
            .GanttDiagram(
                title: "My Gantt",
                config: config,
                hideTodayMarker: true,
                todayMarkerCss: "stroke: #d3d3d3; stroke-width: 2px;",
                dateFormat: "DD-MM-YYYY", options: _options)
            .AddTask("Foo", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("gantt", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildGanttDiagramWithClickBindings()
    {
        string diagram = Mermaid
            .GanttDiagram(options: _options)
            .AddTask("Foo", Date("2024-05-01"), Date("2024-05-05"), out GanttTask t1)
            .AddTask("Bar", Date("2024-05-08"), Date("2024-05-12"), out GanttTask t2)
            .AddHyperlink(t1, "https://example.com")
            .AddCallback(t2, "myFunction")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("gantt", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildGanttDiagramWithVerticalMarker()
    {
        string diagram = Mermaid
            .GanttDiagram(options: _options)
            .AddTask("Foo", Date("2024-05-01"), Date("2024-05-05"), out GanttTask t1)
            .AddTask("Bar", Date("2024-05-08"), Date("2024-05-12"), out GanttTask t2)
            .AddVerticalMarker("Milestone 1", Date("2024-05-03"))
            .AddVerticalMarker("Milestone 2", Date("2024-05-10"), TimeSpan.FromDays(1))
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("gantt", toolingFixture.GetDiagramType(diagramResult));
    }

    private static DateTimeOffset Date(string date)
    {
        return DateTimeOffset.Parse(date, CultureInfo.InvariantCulture);
    }
}
