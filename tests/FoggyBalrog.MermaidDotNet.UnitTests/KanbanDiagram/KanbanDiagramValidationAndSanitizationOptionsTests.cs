using FoggyBalrog.MermaidDotNet.KanbanDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.KanbanDiagram;

public class KanbanDiagramValidationAndSanitizationOptionsTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = true, SanitizeInputs = true };

    [Fact]
    public void SanitizeInputs()
    {
        string diagram = Mermaid
            .KanbanDiagram(options: _options)
            .AddColumn("col[1]", x => x.AddTask("task[1]", assigned: "al'ce\\", ticket: "TK,{1}", priority: Priority.High))
            .Build();

        Assert.Contains("column0[col#91;1#93;]", diagram);
        Assert.Contains("task00[task#91;1#93;]", diagram);
        Assert.Contains("assigned: 'al#39;ce#92;'", diagram);
        Assert.Contains("ticket: TK#44;#123;1#125;", diagram);
    }
}
