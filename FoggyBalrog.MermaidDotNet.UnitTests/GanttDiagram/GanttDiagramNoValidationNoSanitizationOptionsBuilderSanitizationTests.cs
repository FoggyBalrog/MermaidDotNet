using System.Globalization;
using FoggyBalrog.MermaidDotNet.GanttDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.GanttDiagram;

public class GanttDiagramNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .GanttDiagram(options: _options)
            .AddTask("Task:1", Date("2024-05-01"), Date("2024-05-02"), out GanttTask t1)
            .AddTask("Task:2", Date("2024-05-03"), Date("2024-05-04"), out GanttTask t2)
            .AddSection("Section:1")
            .AddVerticalMarker("Mark:1", Date("2024-05-05"))
            .AddCallback(t1, "fn name")
            .AddHyperlink(t2, "https://example.com/a b")
            .Build();

        Assert.Contains("Task:1: task1", diagram);
        Assert.Contains("Task:2: task2", diagram);
        Assert.Contains("section Section:1", diagram);
        Assert.Contains("Mark:1: vert", diagram);
        Assert.Contains("click task1 call fn name()", diagram);
        Assert.Contains("https://example.com/a b", diagram);
        Assert.DoesNotContain("#58;", diagram);
    }

    private static DateTimeOffset Date(string date)
    {
        return DateTimeOffset.Parse(date, CultureInfo.InvariantCulture);
    }
}
