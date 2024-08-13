namespace FoggyBalrog.MermaidDotNet.UnitTests.TimelineDiagram;

public class TimelineDiagramBuilderTests
{
    [Fact]
    public void CanBuildEmptyTimelineDiagram()
    {
        string diagram = Mermaid
            .TimelineDiagram()
            .Build();

        Assert.Equal("timeline", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildEmptyTimelineDiagramWithTitle()
    {
        string diagram = Mermaid
            .TimelineDiagram(title: "Title")
            .Build();

        Assert.Equal(@"timeline
    title Title", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildSimpleTimelineDiagram()
    {
        string diagram = Mermaid
            .TimelineDiagram("Some title")
            .AddEvents("2021", "Event 1", "Event 2")
            .AddEvents("2022", "Event 3")
            .AddEvents("2023", "Event 4", "Event 5", "Event 6")
            .Build();

        Assert.Equal(@"timeline
    title Some title
    2021 : Event 1 : Event 2
    2022 : Event 3
    2023 : Event 4 : Event 5 : Event 6", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildTimelineDiagramWithSections()
    {
        string diagram = Mermaid
            .TimelineDiagram("Some title")
            .AddSection("Section 1")
            .AddEvents("2021", "Event 1", "Event 2")
            .AddEvents("2022", "Event 3")
            .AddSection("Section 2")
            .AddEvents("2023", "Event 4", "Event 5", "Event 6")
            .Build();

        Assert.Equal(@"timeline
    title Some title
    section Section 1
        2021 : Event 1 : Event 2
        2022 : Event 3
    section Section 2
        2023 : Event 4 : Event 5 : Event 6", diagram, ignoreLineEndingDifferences: true);
    }
}
