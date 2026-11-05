using FoggyBalrog.MermaidDotNet.KanbanDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.KanbanDiagram;

public class KanbanDiagramNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .KanbanDiagram(options: _options)
            .AddColumn("col[1]", x => x.AddTask("task[1]", assigned: "al'ce\\", ticket: "TK,{1}", priority: Priority.High))
            .Build();

        Assert.Contains("column0[col[1]]", diagram);
        Assert.Contains("task00[task[1]]", diagram);
        Assert.Contains("assigned: 'al'ce\\'", diagram);
        Assert.Contains("ticket: TK,{1}", diagram);
        Assert.DoesNotContain("#91;", diagram);
    }
}
