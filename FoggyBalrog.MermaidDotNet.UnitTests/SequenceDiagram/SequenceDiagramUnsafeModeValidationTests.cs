using FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.SequenceDiagram;

public class SequenceDiagramUnsafeModeValidationTests
{
    [Fact]
    public void AddNoteOver_DoesNotThrowIfMember1IsForeign()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var m1);

        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("bar", out var m2)
            .AddNoteOver(m1, m2, "Note")
            .Build();
    }

    [Fact]
    public void AddNoteOver_DoesNotThrowIfMember2IsForeign()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("bar", out var m2);

        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var m1)
            .AddNoteOver(m1, m2, "Note")
            .Build();
    }

    [Fact]
    public void AddNoteOver_DoesNotThrowIfNoteIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var m1)
            .AddActor("bar", out var m2)
            .AddNoteOver(m1, m2, " ")
            .Build();
    }

    [Fact]
    public void AddNoteRightOf_DoesNotThrowIfMemberIsForeign()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var m1);

        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddNoteRightOf(m1, "Note")
            .Build();
    }

    [Fact]
    public void AddNoteRightOf_DoesNotThrowIfNoteIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var m1)
            .AddNoteRightOf(m1, " ")
            .Build();
    }

    [Fact]
    public void AddNoteLeftOf_DoesNotThrowIfMemberIsForeign()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var m1);

        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddNoteLeftOf(m1, "Note")
            .Build();
    }

    [Fact]
    public void AddNoteLeftOf_DoesNotThrowIfNoteIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var m1)
            .AddNoteLeftOf(m1, " ")
            .Build();
    }

    [Fact]
    public void AddBox_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddBox(" ", out var _)
            .Build();
    }

    [Fact]
    public void AddLoop_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddLoop(" ", _ =>
            {
            })
            .Build();
    }

    [Fact]
    public void AddMember_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddMember(" ", MemberType.Actor, out var _)
            .Build();
    }

    [Fact]
    public void AddMember_DoesNotThrowIfNameIsDuplicate()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddMember("foo", MemberType.Actor, out var _)
            .AddMember("foo", MemberType.Actor, out var _)
            .Build();
    }

    [Fact]
    public void AddMember_DoesNotThrowIfBoxIsForeign()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddBox("box", out var box);

        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddMember("foo", MemberType.Actor, out var _, box)
            .Build();
    }

    [Fact]
    public void SendMessage_DoesNotThrowIfSenderIsForeign()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var sender);

        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("bar", out var recipient)
            .SendMessage(sender, recipient, "Message")
            .Build();
    }

    [Fact]
    public void SendMessage_DoesNotThrowIfRecipientIsForeign()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("bar", out var recipient);

        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var sender)
            .SendMessage(sender, recipient, "Message")
            .Build();
    }

    [Fact]
    public void SendMessage_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var sender)
            .AddActor("bar", out var recipient)
            .SendMessage(sender, recipient, " ")
            .Build();
    }

    [Fact]
    public void SendCreateMessage_DoesNotThrowIfSenderIsForeign()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var sender);

        Mermaid
            .Unsafe
            .SequenceDiagram()
            .SendCreateMessage(sender, "baz", MemberType.Actor, out var _, "Message")
            .Build();
    }

    [Fact]
    public void SendCreateMessage_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var sender)
            .SendCreateMessage(sender, " ", MemberType.Actor, out var _, "Message")
            .Build();
    }

    [Fact]
    public void SendCreateMessage_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var sender)
            .SendCreateMessage(sender, "bar", MemberType.Actor, out var _, " ")
            .Build();
    }

    [Fact]
    public void SendDestroyMessage_DoesNotThrowIfSenderIsForeign()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var sender);

        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("bar", out var recipient)
            .SendDestroyMessage(sender, recipient, DestructionTarget.Recipient, "Message")
            .Build();
    }

    [Fact]
    public void SendDestroyMessage_DoesNotThrowIfRecipientIsForeign()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("bar", out var recipient);

        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var sender)
            .SendDestroyMessage(sender, recipient, DestructionTarget.Recipient, "Message")
            .Build();
    }

    [Fact]
    public void SendDestroyMessage_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var sender)
            .AddActor("bar", out var recipient)
            .SendDestroyMessage(sender, recipient, DestructionTarget.Recipient, " ")
            .Build();
    }

    [Fact]
    public void Alternatives_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
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
            .Unsafe
            .SequenceDiagram()
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
            .Unsafe
            .SequenceDiagram()
            .Optional(" ", _ =>
            {
            })
            .Build();
    }

    [Fact]
    public void Critical_DoesNotThrowIfDescriptionIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .Critical(" ", _ =>
            {
            })
            .Build();
    }

    [Fact]
    public void Critical_DoesNotThrowIfOptionDescriptionIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
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
            .Unsafe
            .SequenceDiagram()
            .Break(" ", _ =>
            {
            })
            .Build();
    }

    [Fact]
    public void Comment_DoesNotThrowIfTextIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .Comment(" ")
            .Build();
    }

    [Fact]
    public void AddLink_DoesNotThrowIfMemberIsForeign()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var member);

        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddLink(member, "title", "uri")
            .Build();
    }

    [Fact]
    public void AddLink_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var member)
            .AddLink(member, " ", "uri")
            .Build();
    }

    [Fact]
    public void AddLink_DoesNotThrowIfUriIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddActor("foo", out var member)
            .AddLink(member, "title", " ")
            .Build();
    }
}
