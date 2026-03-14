using FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.SequenceDiagram;

public class SequenceDiagramValidationAndSanitizationOptionsTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = true, SanitizeInputs = true };

    [Fact]
    public void SanitizeInputs()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("Alice\r\n-<>:;,\n#", out var a, MemberType.Participant)
            .AddLink(a, "foo;\nbar#baz@", "https://foo.com/?query=this is a test")
            .SendCreateMessage(a, "Bob-<>:;,#", out var b, "foo;\nbar#")
            .SendMessage(a, b, "foo;bar#")
            .Build();

        Assert.Contains("Alice<br/>#45;#60;#62;#58;#59;#44;<br/>#35;", diagram);
        Assert.Contains("foo#59;<br/>bar#35;baz#64;", diagram);
        Assert.Contains("https://foo.com/?query=this%20is%20a%20test", diagram);
        Assert.Contains("Bob#45;#60;#62;#58;#59;#44;#35;", diagram);
        Assert.Contains("foo#59;bar#35;", diagram);
    }
}
