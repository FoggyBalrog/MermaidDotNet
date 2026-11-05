using FoggyBalrog.MermaidDotNet.StateDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.StateDiagram;

public class StateDiagramNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .StateDiagram(options: _options)
            .AddState("S:\"", out State s1)
            .AddState("T", out State s2)
            .AddStateTransition(s1, s2, "D:")
            .AddStateLink(s1, "https://example.com/\"", "tip\"")
            .AddNote(s1, NotePosition.Right, "N")
            .Build();

        Assert.Contains("s1 : S:\"", diagram);
        Assert.Contains("s1 --> s2 : D:", diagram);
        Assert.Contains("click s1 \"https://example.com/\"\" \"tip\"\"", diagram);
        Assert.DoesNotContain("#58;", diagram);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("S:\"")]
    public void CompositeStateWithDirection_DoesntProcessDescription(string description)
    {
        string diagram = Mermaid
            .StateDiagram(options: _options)
            .AddCompositeState(description, out State _, _ =>
            {
            }, StateDiagramDirection.LeftToRight)
            .Build();

        Assert.Equal($@"stateDiagram-v2
    state ""{description}"" as s1 {{
        direction LR
    }}", diagram, ignoreLineEndingDifferences: true);
    }
}
