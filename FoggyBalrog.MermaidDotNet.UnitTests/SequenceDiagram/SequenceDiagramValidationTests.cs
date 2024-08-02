using FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.SequenceDiagram;

public class SequenceDiagramValidationTests
{
    [Fact]
    public void AddNoteOver_ThrowsIfMember1IsForeign()
    {
        Mermaid
            .SequenceDiagram()
            .AddActor("foo", out var m1);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddActor("bar", out var m2)
                .AddNoteOver(m1, m2, "Note");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddNoteOver_ThrowsIfMember2IsForeign()
    {
        Mermaid
            .SequenceDiagram()
            .AddActor("bar", out var m2);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddActor("foo", out var m1)
                .AddNoteOver(m1, m2, "Note");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddNoteOver_ThrowsIfNoteIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddActor("foo", out var m1)
                .AddActor("bar", out var m2)
                .AddNoteOver(m1, m2, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddNoteRightOf_ThrowsIfMemberIsForeign()
    {
        Mermaid
            .SequenceDiagram()
            .AddActor("foo", out var m1);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddNoteRightOf(m1, "Note");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddNoteRightOf_ThrowsIfNoteIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddActor("foo", out var m1)
                .AddNoteRightOf(m1, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddNoteLeftOf_ThrowsIfMemberIsForeign()
    {
        Mermaid
            .SequenceDiagram()
            .AddActor("foo", out var m1);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddNoteLeftOf(m1, "Note");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddNoteLeftOf_ThrowsIfNoteIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddActor("foo", out var m1)
                .AddNoteLeftOf(m1, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddBox_ThrowsIfNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddBox(" ", out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddLoop_ThrowsIfDescriptionIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddLoop(" ", _ =>
                {
                });
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddMember_ThrowsIfNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddMember(" ", MemberType.Actor, out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddMember_ThrowsIfNameIsDuplicate()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddMember("foo", MemberType.Actor, out var _)
                .AddMember("foo", MemberType.Actor, out var _);
        });

        Assert.Equal(MermaidExceptionReason.DuplicateValue, exception.Reason);
    }

    [Fact]
    public void AddMember_ThrowsIfBoxIsForeign()
    {
        Mermaid
            .SequenceDiagram()
            .AddBox("box", out var box);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddMember("foo", MemberType.Actor, out var _, box);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void SendMessage_ThrowsIfSenderIsForeign()
    {
        Mermaid
            .SequenceDiagram()
            .AddActor("foo", out var sender);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddActor("bar", out var recipient)
                .SendMessage(sender, recipient, "Message");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void SendMessage_ThrowsIfRecipientIsForeign()
    {
        Mermaid
            .SequenceDiagram()
            .AddActor("bar", out var recipient);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddActor("foo", out var sender)
                .SendMessage(sender, recipient, "Message");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void SendMessage_ThrowsIfDescriptionIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddActor("foo", out var sender)
                .AddActor("bar", out var recipient)
                .SendMessage(sender, recipient, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void SendCreateMessage_ThrowsIfSenderIsForeign()
    {
        Mermaid
            .SequenceDiagram()
            .AddActor("foo", out var sender);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .SendCreateMessage(sender, "baz", MemberType.Actor, out var _, "Message");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void SendCreateMessage_ThrowsIfNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddActor("foo", out var sender)
                .SendCreateMessage(sender, " ", MemberType.Actor, out var _, "Message");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void SendCreateMessage_ThrowsIfDescriptionIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddActor("foo", out var sender)
                .SendCreateMessage(sender, "bar", MemberType.Actor, out var _, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void SendDestroyMessage_ThrowsIfSenderIsForeign()
    {
        Mermaid
            .SequenceDiagram()
            .AddActor("foo", out var sender);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddActor("bar", out var recipient)
                .SendDestroyMessage(sender, recipient, DestructionTarget.Recipient, "Message");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void SendDestroyMessage_ThrowsIfRecipientIsForeign()
    {
        Mermaid
            .SequenceDiagram()
            .AddActor("bar", out var recipient);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddActor("foo", out var sender)
                .SendDestroyMessage(sender, recipient, DestructionTarget.Recipient, "Message");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void SendDestroyMessage_ThrowsIfDescriptionIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddActor("foo", out var sender)
                .AddActor("bar", out var recipient)
                .SendDestroyMessage(sender, recipient, DestructionTarget.Recipient, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void Alternatives_ThrowsIfDescriptionIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .Alternatives(
                    ("foo", _ => { }
            ),
                    (" ", _ => { }
            ),
                    ("bar", _ => { }
            ));
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void Parallels_ThrowsIfDescriptionIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .Parallels(
                    ("foo", _ => { }
            ),
                    (" ", _ => { }
            ),
                    ("bar", _ => { }
            ));
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void Optional_ThrowsIfDescriptionIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .Optional(" ", _ =>
                {
                });
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    // Critical(string description, Action<SequenceDiagramBuilder> action, params (string description, Action<SequenceDiagramBuilder> action)[] options)
    [Fact]
    public void Critical_ThrowsIfDescriptionIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .Critical(" ", _ =>
                {
                });
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void Critical_ThrowsIfOptionDescriptionIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .Critical(
                    "foo",
                    _ => { },
                    ("foo", _ => { }
            ),
                    (" ", _ => { }
            ),
                    ("bar", _ => { }
            ));
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void Break_ThrowsIfDescriptionIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .Break(" ", _ =>
                {
                });
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void Comment_ThrowsIfTextIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .Comment(" ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddLink_ThrowsIfMemberIsForeign()
    {
        Mermaid
            .SequenceDiagram()
            .AddActor("foo", out var member);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddLink(member, "title", "uri");
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void AddLink_ThrowsIfTitleIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddActor("foo", out var member)
                .AddLink(member, " ", "uri");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddLink_ThrowsIfUriIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .SequenceDiagram()
                .AddActor("foo", out var member)
                .AddLink(member, "title", " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }
}
