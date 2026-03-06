using FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.SequenceDiagram;

public class SequenceDiagramNoValidationNoSanitizationOptionsBuilderSanitizationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void DoesntSanitizeInputs()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("Alice\r\n-<>:;,\n#", out var a, MemberType.Participant)
            .AddLink(a, "foo;bar#baz@", "https://foo.com/?query=this is a test")
            .SendCreateMessage(a, "Bob-<>:;,#", out var b, "foo;bar#")
            .SendMessage(a, b, "foo;bar#")
            .Build();

        Assert.Contains("Alice\r\n-<>:;,\n#", diagram);
        Assert.Contains("foo;bar#baz@", diagram);
        Assert.Contains("https://foo.com/?query=this is a test", diagram);
        Assert.Contains("Bob-<>:;,#", diagram);
        Assert.Contains("foo;bar#", diagram);
        Assert.DoesNotContain("#59;", diagram);
    }
}
