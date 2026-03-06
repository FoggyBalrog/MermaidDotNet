using FoggyBalrog.MermaidDotNet.StateDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.StateDiagram;

public class StateDiagramValidationAndSanitizationOptionsTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = true, SanitizeInputs = true };

    [Fact]
    public void SanitizeInputs()
    {
        string diagram = Mermaid
            .StateDiagram(options: _options)
            .AddState("S:\"", out State s1)
            .AddState("T", out State s2)
            .AddStateTransition(s1, s2, "D:")
            .AddStateLink(s1, "https://example.com/\"", "tip\"")
            .AddNote(s1, NotePosition.Right, "N")
            .Build();

        Assert.Contains("s1 : S#58;#34;", diagram);
        Assert.Contains("s1 --> s2 : D#58;", diagram);
        Assert.Contains("click s1 \"https://example.com/#34;\" \"tip#34;\"", diagram);
    }
}
