namespace FoggyBalrog.MermaidDotNet.UnitTests.UserJourneyDiagram;

public class UserJourneyDiagramUnsafeModeBuilderTests
{
    [Fact]
    public void CanBuildSimpleDiagramWithoutTitle()
    {
        string diagram = Mermaid
            .Unsafe
            .UserJourneyDiagram()
            .AddTask("Task 1", 1, "Actor 1", "Actor 2")
            .AddTask("Task 2", 2)
            .AddSection("Section 1")
            .AddTask("Task 3", 3)
            .AddTask("Task 4", 4, "Actor 3")
            .AddSection("Section 2")
            .AddTask("Task 5", 5, "Actor 1", "Actor 3")
            .AddTask("Task 6", 6, "Actor 2")
            .Build();

        Assert.Equal(@"journey
    Task 1: 1: Actor 1, Actor 2
    Task 2: 2
    section Section 1
        Task 3: 3
        Task 4: 4: Actor 3
    section Section 2
        Task 5: 5: Actor 1, Actor 3
        Task 6: 6: Actor 2", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildSimpleDiagramWithTitle()
    {
        string diagram = Mermaid
            .Unsafe
            .UserJourneyDiagram("My title")
            .AddTask("Task 1", 1, "Actor 1", "Actor 2")
            .AddTask("Task 2", 2)
            .AddSection("Section 1")
            .AddTask("Task 3", 3)
            .AddTask("Task 4", 4, "Actor 3")
            .AddSection("Section 2")
            .AddTask("Task 5", 5, "Actor 1", "Actor 3")
            .AddTask("Task 6", 6, "Actor 2")
            .Build();

        Assert.Equal(@"journey
    title My title
    Task 1: 1: Actor 1, Actor 2
    Task 2: 2
    section Section 1
        Task 3: 3
        Task 4: 4: Actor 3
    section Section 2
        Task 5: 5: Actor 1, Actor 3
        Task 6: 6: Actor 2", diagram, ignoreLineEndingDifferences: true);
    }
}
