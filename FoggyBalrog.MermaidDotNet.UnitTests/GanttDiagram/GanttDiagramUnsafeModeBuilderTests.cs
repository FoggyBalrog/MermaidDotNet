using System.Globalization;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;
using FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.GanttDiagram;

public class GanttDiagramUnsafeModeBuilderTests
{
    [Fact]
    public void CanBuildGanttDiagramWithAllTaskKinds()
    {
        string diagram = Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("Foo", Date("2024-05-01"), Date("2024-05-05"), out GanttTask t1)
            .AddTask("Bar", Date("2024-05-08"), TimeSpan.FromDays(3), out GanttTask t2)
            .AddTask("Baz", t1, Date("2024-05-09"), out GanttTask _)
            .AddTask("Qux", t1, TimeSpan.FromDays(2), out GanttTask _)
            .AddTask("Quux", Date("2024-05-04"), t2, out GanttTask _)
            .AddTask("Corge", t1, t2, out GanttTask _)
            .Build();

        Assert.Equal(@"gantt
    dateFormat YYYY-MM-DD
    Foo: task1, 2024-05-01, 2024-05-05
    Bar: task2, 2024-05-08, 3d
    Baz: task3, after task1, 2024-05-09
    Qux: task4, after task1, 2d
    Quux: task5, 2024-05-04, until task2
    Corge: task6, after task1, until task2", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildGanttDiagramWithSections()
    {
        string diagram = Mermaid
            .Unsafe
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

        Assert.Equal(@"gantt
    dateFormat YYYY-MM-DD
    Foo: task1, 2024-05-01, 2024-05-05
    section Section 1
        Bar: task2, 2024-05-08, 3d
        Baz: task3, after task1, 2024-05-09
        Qux: task4, after task1, 2d
    section Section 2
        Quux: task5, 2024-05-04, until task2
        Corge: task6, after task1, until task2", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildGanttDiagramWithAllTagsCombinations()
    {
        string diagram = Mermaid
            .Unsafe
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

        Assert.Equal(@"gantt
    dateFormat YYYY-MM-DD
    Task 1: active, task1, 2024-05-01, 2024-05-05
    Task 2: done, task2, 2024-05-01, 2024-05-05
    Task 3: crit, task3, 2024-05-01, 2024-05-05
    Task 4: milestone, task4, 2024-05-01, 2024-05-05
    Task 5: active, done, task5, 2024-05-01, 2024-05-05
    Task 6: active, crit, task6, 2024-05-01, 2024-05-05
    Task 7: active, milestone, task7, 2024-05-01, 2024-05-05
    Task 8: done, crit, task8, 2024-05-01, 2024-05-05
    Task 9: done, milestone, task9, 2024-05-01, 2024-05-05
    Task 10: crit, milestone, task10, 2024-05-01, 2024-05-05
    Task 11: active, done, crit, task11, 2024-05-01, 2024-05-05
    Task 12: active, done, milestone, task12, 2024-05-01, 2024-05-05
    Task 13: active, crit, milestone, task13, 2024-05-01, 2024-05-05
    Task 14: done, crit, milestone, task14, 2024-05-01, 2024-05-05
    Task 15: active, done, crit, milestone, task15, 2024-05-01, 2024-05-05", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildGanttDiagramWithExcludes()
    {
        string diagram = Mermaid
            .Unsafe
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

        Assert.Equal(@"gantt
    dateFormat YYYY-MM-DD
    excludes monday, tuesday, wednesday, thursday, friday, saturday, sunday, weekends, 2024-05-01, 2024-05-05
    Foo: task1, 2024-04-30, 2024-05-06", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildGanttDiagramWithConfiguration()
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
            .Unsafe
            .GanttDiagram(
                title: "My Gantt",
                config: config,
                hideTodayMarker: true,
                todayMarkerCss: "stroke: #d3d3d3; stroke-width: 2px;",
                dateFormat: "DD-MM-YYYY")
            .AddTask("Foo", Date("2024-05-01"), Date("2024-05-05"), out GanttTask _)
            .Build();

        Assert.Equal(@"---
title: My Gantt
config:
  gantt:
    axisFormat: '%d-%m'
    tickInterval: 1week
    displayMode: compact
    weekday: monday
---
gantt
    dateFormat DD-MM-YYYY
    todayMarker off
    todayMarker stroke: #d3d3d3; stroke-width: 2px;
    Foo: task1, 01-05-2024, 05-05-2024", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildGanttDiagramWithClickBindings()
    {
        string diagram = Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("Foo", Date("2024-05-01"), Date("2024-05-05"), out GanttTask t1)
            .AddTask("Bar", Date("2024-05-08"), Date("2024-05-12"), out GanttTask t2)
            .AddHyperlink(t1, "https://example.com")
            .AddCallback(t2, "myFunction")
            .Build();

        Assert.Equal(@"gantt
    dateFormat YYYY-MM-DD
    Foo: task1, 2024-05-01, 2024-05-05
    click task1 href ""https://example.com""
    Bar: task2, 2024-05-08, 2024-05-12
    click task2 call myFunction()", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildGanttDiagramWithVerticalMarker()
    {
        string diagram = Mermaid
            .Unsafe
            .GanttDiagram()
            .AddTask("Foo", Date("2024-05-01"), Date("2024-05-05"), out GanttTask t1)
            .AddTask("Bar", Date("2024-05-08"), Date("2024-05-12"), out GanttTask t2)
            .AddVerticalMarker("Milestone 1", Date("2024-05-03"))
            .AddVerticalMarker("Milestone 2", Date("2024-05-10"), TimeSpan.FromDays(1))
            .Build();

        Assert.Equal(@"gantt
    dateFormat YYYY-MM-DD
    Foo: task1, 2024-05-01, 2024-05-05
    Bar: task2, 2024-05-08, 2024-05-12
    Milestone 1: vert, vert1, 2024-05-03, 0ms
    Milestone 2: vert, vert1, 2024-05-10, 1d", diagram, ignoreLineEndingDifferences: true);
    }

    private static DateTimeOffset Date(string date)
    {
        return DateTimeOffset.Parse(date, CultureInfo.InvariantCulture);
    }
}
