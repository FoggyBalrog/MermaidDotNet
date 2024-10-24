using System.Drawing;
using FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.SequenceDiagram;

public class SequenceDiagramUnsafeModeBuilderTests
{
    [Fact]
    public void CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .Build();

        Assert.Equal("sequenceDiagram", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithOnlyMembers()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddMember("Alice", MemberType.Participant, out _)
            .AddMember("Bob", MemberType.Actor, out _)
            .AddParticipant("Charlie", out _)
            .AddActor("David", out _)
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Alice
    actor Bob
    participant Charlie
    actor David", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildSimpleDiagram()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddMember("Alice", MemberType.Participant, out Member m1)
            .AddMember("Bob", MemberType.Participant, out Member m2)
            .SendMessage(m1, m2, $"Hello {m2.Name}!")
            .SendMessage(m2, m1, $"Hello {m1.Name}!", LineType.Dotted)
            .AddMember("Charlie", MemberType.Actor, out Member m3)
            .SendMessage(m1, m2, $"How are you {m2.Name}?", arrowType: ArrowType.Open)
            .SendMessage(m1, m3, $"How are you {m3.Name}?", arrowType: ArrowType.None)
            .SendMessage(m3, m1, $"I'm fine, thank you {m1.Name}!")
            .SendMessage(m2, m1, $"I'm fine, thank you {m1.Name}!", LineType.Dotted, ArrowType.Cross)
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Alice
    participant Bob
    actor Charlie
    Alice ->> Bob: Hello Bob!
    Bob -->> Alice: Hello Alice!
    Alice -) Bob: How are you Bob?
    Alice -> Charlie: How are you Charlie?
    Charlie ->> Alice: I'm fine, thank you Alice!
    Bob --x Alice: I'm fine, thank you Alice!", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithNotes()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddParticipant("Alice", out Member a)
            .AddParticipant("Bob", out Member b)
            .AddParticipant("Charlie", out Member c)
            .AddNoteOver(a, b, "This is a note")
            .AddNoteRightOf(c, "This is another note")
            .SendMessage(a, b, $"Hello {b.Name}!")
            .AddNoteOver(a, c, "This is a note")
            .SendMessage(b, c, $"Hello {c.Name}!")
            .AddNoteLeftOf(b, "This is another note")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Alice
    participant Bob
    participant Charlie
    note over Alice, Bob: This is a note
    note right of Charlie: This is another note
    Alice ->> Bob: Hello Bob!
    note over Alice, Charlie: This is a note
    Bob ->> Charlie: Hello Charlie!
    note left of Bob: This is another note", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithBoxes()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddBox("Box1", out Box box1, Color.Aquamarine)
            .AddBox("Box2", out Box box2, Color.FromArgb(70, 55, 56, 57))
            .AddBox("Box3", out Box box3)
            .AddParticipant("Alice", out Member a, box1)
            .AddParticipant("Bob", out Member b, box1)
            .AddParticipant("Charlie", out Member c, box2)
            .AddParticipant("David", out Member d, box3)
            .AddParticipant("Eve", out Member e)
            .SendMessage(a, b, $"Hello {b.Name}!")
            .SendMessage(b, c, $"Hello {c.Name}!")
            .SendMessage(c, d, $"Hello {d.Name}!")
            .SendMessage(d, e, $"Hello {e.Name}!")
            .SendMessage(e, a, $"Hello {a.Name}!")
            .Build();

        Assert.Equal(@"sequenceDiagram
    box Aquamarine Box1
    participant Alice
    participant Bob
    end
    box rgba(55, 56, 57, 0.27) Box2
    participant Charlie
    end
    box Transparent Box3
    participant David
    end
    participant Eve
    Alice ->> Bob: Hello Bob!
    Bob ->> Charlie: Hello Charlie!
    Charlie ->> David: Hello David!
    David ->> Eve: Hello Eve!
    Eve ->> Alice: Hello Alice!", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithMemberCreationAndDestruction()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddParticipant("Alice", out Member a)
            .AddParticipant("Bob", out Member b)
            .SendMessage(a, b, $"Hello {b.Name}, how are you?")
            .SendMessage(b, a, "Fine, thank you. And you?")
            .SendCreateMessage(a, "Carl", MemberType.Participant, out Member c, "Hi Carl!")
            .SendCreateMessage(c, "Donald", MemberType.Actor, out _, "Hi!")
            .SendDestroyMessage(a, c, DestructionTarget.Recipient, "We are too many", arrowType: ArrowType.Cross)
            .SendDestroyMessage(b, a, DestructionTarget.Sender, "I agree")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Alice
    participant Bob
    Alice ->> Bob: Hello Bob, how are you?
    Bob ->> Alice: Fine, thank you. And you?
    create participant Carl
    Alice ->> Carl: Hi Carl!
    create actor Donald
    Carl ->> Donald: Hi!
    destroy Carl
    Alice -x Carl: We are too many
    destroy Bob
    Bob ->> Alice: I agree", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithActivation()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddParticipant("Alice", out Member a)
            .AddParticipant("John", out Member j)
            .SendMessage(a, j, "Hello John, how are you?", activationType: ActivationType.Activate)
            .SendMessage(a, j, "John, can you hear me?", activationType: ActivationType.Activate)
            .SendMessage(j, a, "Hi Alice, I can hear you!", activationType: ActivationType.Deactivate)
            .SendMessage(j, a, "I feel great!", activationType: ActivationType.Deactivate)
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Alice
    participant John
    Alice ->>+ John: Hello John, how are you?
    Alice ->>+ John: John, can you hear me?
    John ->>- Alice: Hi Alice, I can hear you!
    John ->>- Alice: I feel great!", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithLoops()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddParticipant("Alice", out Member a)
            .AddParticipant("Bob", out Member b)
            .SendMessage(a, b, "Hello Bob!")
            .AddLoop("Every minute", builder => builder
                .SendMessage(b, a, "Hello Alice!")
                .AddNoteRightOf(b, "Note")
                .SendMessage(b, a, "Can you hear me?"))
            .SendMessage(a, b, "Yes, I can hear you!")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Alice
    participant Bob
    Alice ->> Bob: Hello Bob!
    loop Every minute
        Bob ->> Alice: Hello Alice!
        note right of Bob: Note
        Bob ->> Alice: Can you hear me?
    end
    Alice ->> Bob: Yes, I can hear you!", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithMultipleAlternatives()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddParticipant("Alice", out Member a)
            .AddParticipant("Bob", out Member b)
            .SendMessage(a, b, "Hello Bob!")
            .Alternatives(
                ("Bob is happy", builder => builder
                .SendMessage(b, a, "Hello Alice!")
                    .SendMessage(b, a, "Can you hear me?")
                    .Alternatives(
                        ("Alice is happy", builder => builder
                            .SendMessage(a, b, "Yes, I can hear you!")),
                        ("Alice is sad", builder => builder
                            .SendMessage(a, b, "No, I can't hear you!")))),
                ("Bob is sad", builder => builder
                    .SendMessage(b, a, "Hello Alice.")
                    .Alternatives(
                        ("Alice is happy", builder => builder
                            .SendMessage(a, b, "Sorry to hear that.")),
                        ("Alice is sad", builder => builder
                            .SendMessage(a, b, "Me too.")))))
            .SendMessage(a, b, "Bye")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Alice
    participant Bob
    Alice ->> Bob: Hello Bob!
    alt Bob is happy
        Bob ->> Alice: Hello Alice!
        Bob ->> Alice: Can you hear me?
        alt Alice is happy
            Alice ->> Bob: Yes, I can hear you!
        else Alice is sad
            Alice ->> Bob: No, I can't hear you!
        end
    else Bob is sad
        Bob ->> Alice: Hello Alice.
        alt Alice is happy
            Alice ->> Bob: Sorry to hear that.
        else Alice is sad
            Alice ->> Bob: Me too.
        end
    end
    Alice ->> Bob: Bye", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithSingleAlternative()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddParticipant("Alice", out Member a)
            .AddParticipant("Bob", out Member b)
            .SendMessage(a, b, "Hello Bob!")
            .Alternatives(
                ("Bob is happy", builder => builder
                    .SendMessage(b, a, "Hello Alice!")
                    .SendMessage(b, a, "Can you hear me?")))
            .SendMessage(a, b, "Bye")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Alice
    participant Bob
    Alice ->> Bob: Hello Bob!
    alt Bob is happy
        Bob ->> Alice: Hello Alice!
        Bob ->> Alice: Can you hear me?
    end
    Alice ->> Bob: Bye", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithZeroAlternatives()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddParticipant("Alice", out Member a)
            .AddParticipant("Bob", out Member b)
            .SendMessage(a, b, "Hello Bob!")
            .Alternatives()
            .SendMessage(a, b, "Bye")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Alice
    participant Bob
    Alice ->> Bob: Hello Bob!
    Alice ->> Bob: Bye", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithOptional()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddParticipant("Alice", out Member a)
            .AddParticipant("Bob", out Member b)
            .SendMessage(a, b, "Hello Bob!")
            .Optional("Bob is happy", builder => builder
                .SendMessage(b, a, "Hello Alice!")
                .SendMessage(b, a, "Can you hear me?"))
            .SendMessage(a, b, "Bye")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Alice
    participant Bob
    Alice ->> Bob: Hello Bob!
    opt Bob is happy
        Bob ->> Alice: Hello Alice!
        Bob ->> Alice: Can you hear me?
    end
    Alice ->> Bob: Bye", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithMultipleParallels()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddParticipant("Alice", out Member a)
            .AddParticipant("Bob", out Member b)
            .AddParticipant("Charlie", out Member c)
            .AddParticipant("David", out Member d)
            .AddParticipant("Eve", out Member e)
            .Parallels(
                ("Alice to Bob", builder => builder
                    .SendMessage(a, b, "Hello Bob!")
                    .SendMessage(b, a, "Hello Alice!")),
                ("Eve to David", builder => builder
                    .SendMessage(e, d, "Hello David!")
                    .SendMessage(d, e, "Hello Eve!")),
                ("Bob to Eve", builder => builder
                    .SendMessage(b, e, "Hello Eve!")
                    .SendMessage(e, b, "Hello Bob!")),
                ("Alice to Charlie", builder => builder
                    .SendMessage(a, c, "Hello Charlie!")
                    .SendMessage(c, a, "Hello Alice!")
                    .Parallels(
                        ("Charlie to Bob", builder => builder
                            .SendMessage(c, b, "Talked to Alice")),
                        ("Charlie to Eve", builder => builder
                            .SendMessage(c, e, "Talked to Alice")))))
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Alice
    participant Bob
    participant Charlie
    participant David
    participant Eve
    par Alice to Bob
        Alice ->> Bob: Hello Bob!
        Bob ->> Alice: Hello Alice!
    and Eve to David
        Eve ->> David: Hello David!
        David ->> Eve: Hello Eve!
    and Bob to Eve
        Bob ->> Eve: Hello Eve!
        Eve ->> Bob: Hello Bob!
    and Alice to Charlie
        Alice ->> Charlie: Hello Charlie!
        Charlie ->> Alice: Hello Alice!
        par Charlie to Bob
            Charlie ->> Bob: Talked to Alice
        and Charlie to Eve
            Charlie ->> Eve: Talked to Alice
        end
    end", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithSingleParallel()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddParticipant("Alice", out Member a)
            .AddParticipant("Bob", out Member b)
            .AddParticipant("Charlie", out Member c)
            .Parallels(
                ("Alice to Bob", builder => builder
                    .SendMessage(a, b, "Hello Bob!")
                    .SendMessage(b, a, "Hello Alice!")))
            .SendMessage(a, c, "Hello Charlie!")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Alice
    participant Bob
    participant Charlie
    par Alice to Bob
        Alice ->> Bob: Hello Bob!
        Bob ->> Alice: Hello Alice!
    end
    Alice ->> Charlie: Hello Charlie!", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithZeroParallels()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddParticipant("Alice", out Member a)
            .AddParticipant("Bob", out Member b)
            .Parallels()
            .SendMessage(a, b, "Hello Bob!")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Alice
    participant Bob
    Alice ->> Bob: Hello Bob!", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithCriticalAndMultipleOptions()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddParticipant("Service", out Member s)
            .AddParticipant("DB 1", out Member db1)
            .AddParticipant("DB 2", out Member db2)
            .Critical("Connect to DB1", builder => builder
                .SendMessage(s, db1, "Connect", LineType.Dotted, ArrowType.None)
                .Critical("Connect to DB2", builder => builder
                    .SendMessage(s, db2, "Connect", LineType.Dotted, ArrowType.None),
                    ("Network error", builder => builder
                        .SendMessage(s, db1, "Disconnect", LineType.Dotted, ArrowType.Cross)
                        .SendMessage(s, s, "Log error", LineType.Dotted, ArrowType.None)),
                    ("Network timeout", builder => builder
                        .SendMessage(s, db1, "Disconnect", LineType.Dotted, ArrowType.Cross)
                        .SendMessage(s, s, "Log timeout", LineType.Dotted, ArrowType.None))),
                ("Network error", builder => builder
                        .SendMessage(s, s, "Log error", LineType.Dotted, ArrowType.None)),
                    ("Network timeout", builder => builder
                        .SendMessage(s, s, "Log timeout", LineType.Dotted, ArrowType.None)))
            .Build();


        Assert.Equal(@"sequenceDiagram
    participant Service
    participant DB 1
    participant DB 2
    critical Connect to DB1
        Service --> DB 1: Connect
        critical Connect to DB2
            Service --> DB 2: Connect
        option Network error
            Service --x DB 1: Disconnect
            Service --> Service: Log error
        option Network timeout
            Service --x DB 1: Disconnect
            Service --> Service: Log timeout
        end
    option Network error
        Service --> Service: Log error
    option Network timeout
        Service --> Service: Log timeout
    end", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithCriticalAndNoOptions()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddParticipant("Service", out Member s)
            .AddParticipant("DB", out Member db1)
            .Critical("Connect to DB", builder => builder
                .SendMessage(s, db1, "Connect", LineType.Dotted, ArrowType.None))
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Service
    participant DB
    critical Connect to DB
        Service --> DB: Connect
    end", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithBreak()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddParticipant("Alice", out Member a)
            .AddParticipant("Bob", out Member b)
            .SendMessage(a, b, "Hello!")
            .Break("Something happens", builder => builder
                .SendMessage(a, b, "Bye!"))
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Alice
    participant Bob
    Alice ->> Bob: Hello!
    break Something happens
        Alice ->> Bob: Bye!
    end", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithRectangles()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddParticipant("Alice", out Member a)
            .AddParticipant("Bob", out Member b)
            .AddRectangle(Color.AliceBlue, builder => builder
                .SendMessage(a, b, "Hello Bob!")
                .SendMessage(b, a, "Hello Alice!"))
            .AddRectangle(Color.DarkSeaGreen, builder => builder
                .SendMessage(a, b, "Can you hear me?")
                .SendMessage(b, a, "Yes, I can hear you."))
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Alice
    participant Bob
    rect AliceBlue
        Alice ->> Bob: Hello Bob!
        Bob ->> Alice: Hello Alice!
    end
    rect DarkSeaGreen
        Alice ->> Bob: Can you hear me?
        Bob ->> Alice: Yes, I can hear you.
    end", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithComments()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddParticipant("Alice", out Member a)
            .AddParticipant("Bob", out Member b)
            .Comment("Alice is greeting Bob")
            .SendMessage(a, b, "Hello Bob!")
            .Comment("Bob is greeting Alice")
            .SendMessage(b, a, "Hello Alice!")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Alice
    participant Bob
    %% Alice is greeting Bob
    Alice ->> Bob: Hello Bob!
    %% Bob is greeting Alice
    Bob ->> Alice: Hello Alice!", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithAutoNumber()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram(autonumber: true)
            .AddMember("Alice", MemberType.Participant, out Member m1)
            .AddMember("Bob", MemberType.Participant, out Member m2)
            .SendMessage(m1, m2, $"Hello {m2.Name}!")
            .SendMessage(m2, m1, $"Hello {m1.Name}!")
            .Build();

        Assert.Equal(@"sequenceDiagram
    autonumber
    participant Alice
    participant Bob
    Alice ->> Bob: Hello Bob!
    Bob ->> Alice: Hello Alice!", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithLinks()
    {
        string diagram = Mermaid
            .Unsafe
            .SequenceDiagram()
            .AddMember("Alice", MemberType.Participant, out Member a)
            .AddMember("Bob", MemberType.Participant, out Member b)
            .AddLink(a, "Dashboard", "https://dashboard.contoso.com/alice")
            .AddLink(a, "Wiki", "https://wiki.contoso.com/alice")
            .AddLink(b, "Dashboard", "https://dashboard.contoso.com/bob")
            .AddLink(b, "Wiki", "https://wiki.contoso.com/bob")
            .SendMessage(a, b, $"Hello {b.Name}!")
            .SendMessage(b, a, $"Hello {a.Name}!")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant Alice
    participant Bob
    link Alice: Dashboard @ https://dashboard.contoso.com/alice
    link Alice: Wiki @ https://wiki.contoso.com/alice
    link Bob: Dashboard @ https://dashboard.contoso.com/bob
    link Bob: Wiki @ https://wiki.contoso.com/bob
    Alice ->> Bob: Hello Bob!
    Bob ->> Alice: Hello Alice!", diagram, ignoreLineEndingDifferences: true);
    }
}