using FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.SequenceDiagram;

public class SequenceDiagramNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void AddNoteOver_DoesNotThrowIfMember1IsForeign()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var m1);

        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("bar", out var m2)
            .AddNoteOver(m1, m2, "Note")
            .Build();
    }

    [Fact]
    public void AddNoteOver_DoesNotThrowIfMember2IsForeign()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("bar", out var m2);

        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var m1)
            .AddNoteOver(m1, m2, "Note")
            .Build();
    }

    [Fact]
    public void AddNoteOver_DoesNotThrowIfNoteIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var m1)
            .AddMember("bar", out var m2)
            .AddNoteOver(m1, m2, " ")
            .Build();
    }

    [Fact]
    public void AddNoteRightOf_DoesNotThrowIfMemberIsForeign()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var m1);

        Mermaid
            .SequenceDiagram(options: _options)
            .AddNoteRightOf(m1, "Note")
            .Build();
    }

    [Fact]
    public void AddNoteRightOf_DoesNotThrowIfNoteIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var m1)
            .AddNoteRightOf(m1, " ")
            .Build();
    }

    [Fact]
    public void AddNoteLeftOf_DoesNotThrowIfMemberIsForeign()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var m1);

        Mermaid
            .SequenceDiagram(options: _options)
            .AddNoteLeftOf(m1, "Note")
            .Build();
    }

    [Fact]
    public void AddNoteLeftOf_DoesNotThrowIfNoteIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var m1)
            .AddNoteLeftOf(m1, " ")
            .Build();
    }

    [Fact]
    public void AddBox_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddBox(" ", out var _)
            .Build();
    }

    [Fact]
    public void AddLoop_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddLoop(" ", _ =>
            {
            })
            .Build();
    }

    [Fact]
    public void AddMember_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember(" ", out var _)
            .Build();
    }

    [Fact]
    public void AddMember_DoesNotThrowIfNameIsDuplicate()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var _)
            .AddMember("foo", out var _)
            .Build();
    }

    [Fact]
    public void AddMember_DoesNotThrowIfBoxIsForeign()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddBox("box", out var box);

        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var _, box: box)
            .Build();
    }

    [Fact]
    public void SendMessage_DoesNotThrowIfSenderIsForeign()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var sender);

        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("bar", out var recipient)
            .SendMessage(sender, recipient, "Message")
            .Build();
    }

    [Fact]
    public void SendMessage_DoesNotThrowIfRecipientIsForeign()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("bar", out var recipient);

        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var sender)
            .SendMessage(sender, recipient, "Message")
            .Build();
    }

    [Fact]
    public void SendMessage_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var sender)
            .AddMember("bar", out var recipient)
            .SendMessage(sender, recipient, " ")
            .Build();
    }

    [Fact]
    public void SendCreateMessage_DoesNotThrowIfSenderIsForeign()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var sender);

        Mermaid
            .SequenceDiagram(options: _options)
            .SendCreateMessage(sender, "baz", out var _, "Message")
            .Build();
    }

    [Fact]
    public void SendCreateMessage_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var sender)
            .SendCreateMessage(sender, " ", out var _, "Message")
            .Build();
    }

    [Fact]
    public void SendCreateMessage_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var sender)
            .SendCreateMessage(sender, "bar", out var _, " ")
            .Build();
    }

    [Fact]
    public void SendDestroyMessage_DoesNotThrowIfSenderIsForeign()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var sender);

        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("bar", out var recipient)
            .SendDestroyMessage(sender, recipient, DestructionTarget.Recipient, "Message")
            .Build();
    }

    [Fact]
    public void SendDestroyMessage_DoesNotThrowIfRecipientIsForeign()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("bar", out var recipient);

        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var sender)
            .SendDestroyMessage(sender, recipient, DestructionTarget.Recipient, "Message")
            .Build();
    }

    [Fact]
    public void SendDestroyMessage_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var sender)
            .AddMember("bar", out var recipient)
            .SendDestroyMessage(sender, recipient, DestructionTarget.Recipient, " ")
            .Build();
    }

    [Fact]
    public void Alternatives_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .Alternatives(
                ("foo", _ => { }
        ),
                (" ", _ => { }
        ),
                ("bar", _ => { }
        ))
            .Build();
    }

    [Fact]
    public void Parallels_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .Parallels(
                ("foo", _ => { }
        ),
                (" ", _ => { }
        ),
                ("bar", _ => { }
        ))
            .Build();
    }

    [Fact]
    public void Optional_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .Optional(" ", _ =>
            {
            })
            .Build();
    }

    [Fact]
    public void Critical_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .Critical(" ", _ =>
            {
            })
            .Build();
    }

    [Fact]
    public void Critical_DoesNotThrowIfOptionDescriptionIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .Critical(
                "foo",
                _ => { },
                ("foo", _ => { }
        ),
                (" ", _ => { }
        ),
                ("bar", _ => { }
        ))
            .Build();
    }

    [Fact]
    public void Break_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .Break(" ", _ =>
            {
            })
            .Build();
    }

    [Fact]
    public void Comment_DoesNotThrowIfTextIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .Comment(" ")
            .Build();
    }

    [Fact]
    public void AddLink_DoesNotThrowIfMemberIsForeign()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var member);

        Mermaid
            .SequenceDiagram(options: _options)
            .AddLink(member, "title", "uri")
            .Build();
    }

    [Fact]
    public void AddLink_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var member)
            .AddLink(member, " ", "uri")
            .Build();
    }

    [Fact]
    public void AddLink_DoesNotThrowIfUriIsWhiteSpace()
    {
        Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("foo", out var member)
            .AddLink(member, "title", " ")
            .Build();
    }
}
