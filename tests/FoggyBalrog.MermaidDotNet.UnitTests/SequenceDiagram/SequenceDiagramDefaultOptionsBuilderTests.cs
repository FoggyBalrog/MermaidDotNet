using System.Drawing;
using FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.SequenceDiagram;

public class SequenceDiagramDefaultOptionsBuilderTests
{
    [Fact]
    public void CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .Build();

        Assert.Equal("sequenceDiagram", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithAllMembers()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out _, MemberType.Participant)
            .AddMember("Bob", out _, MemberType.Actor)
            .AddMember("Charlie", out _, MemberType.Boundary)
            .AddMember("David", out _, MemberType.Control)
            .AddMember("Eve", out _, MemberType.Entity)
            .AddMember("Frank", out _, MemberType.Database)
            .AddMember("Grace", out _, MemberType.Collections)
            .AddMember("Heidi", out _, MemberType.Queue)
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant m0 as Alice
    actor m1 as Bob
    participant m2 as Charlie@{ ""type"" : ""boundary"" }
    participant m3 as David@{ ""type"" : ""control"" }
    participant m4 as Eve@{ ""type"" : ""entity"" }
    participant m5 as Frank@{ ""type"" : ""database"" }
    participant m6 as Grace@{ ""type"" : ""collections"" }
    participant m7 as Heidi@{ ""type"" : ""queue"" }", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildSimpleDiagram()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member m1)
            .AddMember("Bob", out Member m2)
            .SendMessage(m1, m2, $"Hello {m2.Name}!")
            .SendMessage(m2, m1, $"Hello {m1.Name}!", LineType.Dotted)
            .AddMember("Charlie", out Member m3, MemberType.Actor)
            .SendMessage(m1, m2, $"How are you {m2.Name}?", arrowType: ArrowType.Open)
            .SendMessage(m1, m3, $"How are you {m3.Name}?", arrowType: ArrowType.None)
            .SendMessage(m3, m1, $"I'm fine, thank you {m1.Name}!")
            .SendMessage(m2, m1, $"I'm fine, thank you {m1.Name}!", LineType.Dotted, ArrowType.Cross)
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant m0 as Alice
    participant m1 as Bob
    actor m2 as Charlie
    m0 ->> m1: Hello Bob!
    m1 -->> m0: Hello Alice!
    m0 -) m1: How are you Bob?
    m0 -> m2: How are you Charlie?
    m2 ->> m0: I'm fine, thank you Alice!
    m1 --x m0: I'm fine, thank you Alice!", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithNotes()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .AddMember("Charlie", out Member c)
            .AddNoteOver(a, b, "This is a note")
            .AddNoteRightOf(c, "This is another note")
            .SendMessage(a, b, $"Hello {b.Name}!")
            .AddNoteOver(a, c, "This is a note")
            .SendMessage(b, c, $"Hello {c.Name}!")
            .AddNoteLeftOf(b, "This is another note")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant m0 as Alice
    participant m1 as Bob
    participant m2 as Charlie
    note over m0, m1: This is a note
    note right of m2: This is another note
    m0 ->> m1: Hello Bob!
    note over m0, m2: This is a note
    m1 ->> m2: Hello Charlie!
    note left of m1: This is another note", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithBoxes()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddBox("Box1", out Box box1, Color.Aquamarine)
            .AddBox("Box2", out Box box2, Color.FromArgb(70, 55, 56, 57))
            .AddBox("Box3", out Box box3)
            .AddMember("Alice", out Member a, box: box1)
            .AddMember("Bob", out Member b, box: box1)
            .AddMember("Charlie", out Member c, box: box2)
            .AddMember("David", out Member d, box: box3)
            .AddMember("Eve", out Member e)
            .SendMessage(a, b, $"Hello {b.Name}!")
            .SendMessage(b, c, $"Hello {c.Name}!")
            .SendMessage(c, d, $"Hello {d.Name}!")
            .SendMessage(d, e, $"Hello {e.Name}!")
            .SendMessage(e, a, $"Hello {a.Name}!")
            .Build();

        Assert.Equal(@"sequenceDiagram
    box Aquamarine Box1
    participant m0 as Alice
    participant m1 as Bob
    end
    box rgba(55, 56, 57, 0.27) Box2
    participant m2 as Charlie
    end
    box Transparent Box3
    participant m3 as David
    end
    participant m4 as Eve
    m0 ->> m1: Hello Bob!
    m1 ->> m2: Hello Charlie!
    m2 ->> m3: Hello David!
    m3 ->> m4: Hello Eve!
    m4 ->> m0: Hello Alice!", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithMemberCreationAndDestruction()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .SendMessage(a, b, $"Hello {b.Name}, how are you?")
            .SendMessage(b, a, "Fine, thank you. And you?")
            .SendCreateMessage(a, "Carl", out Member c, "Hi Carl!")
            .SendCreateMessage(c, "Donald", out _, "Hi!", MemberType.Actor)
            .SendDestroyMessage(a, c, DestructionTarget.Recipient, "We are too many", arrowType: ArrowType.Cross)
            .SendDestroyMessage(b, a, DestructionTarget.Sender, "I agree")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant m0 as Alice
    participant m1 as Bob
    m0 ->> m1: Hello Bob, how are you?
    m1 ->> m0: Fine, thank you. And you?
    create participant m2 as Carl
    m0 ->> m2: Hi Carl!
    create actor m3 as Donald
    m2 ->> m3: Hi!
    destroy m2
    m0 -x m2: We are too many
    destroy m1
    m1 ->> m0: I agree", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithActivation()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("John", out Member j)
            .SendMessage(a, j, "Hello John, how are you?", activationType: ActivationType.Activate)
            .SendMessage(a, j, "John, can you hear me?", activationType: ActivationType.Activate)
            .SendMessage(j, a, "Hi Alice, I can hear you!", activationType: ActivationType.Deactivate)
            .SendMessage(j, a, "I feel great!", activationType: ActivationType.Deactivate)
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant m0 as Alice
    participant m1 as John
    m0 ->>+ m1: Hello John, how are you?
    m0 ->>+ m1: John, can you hear me?
    m1 ->>- m0: Hi Alice, I can hear you!
    m1 ->>- m0: I feel great!", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithLoops()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .SendMessage(a, b, "Hello Bob!")
            .AddLoop("Every minute", builder => builder
                .SendMessage(b, a, "Hello Alice!")
                .AddNoteRightOf(b, "Note")
                .SendMessage(b, a, "Can you hear me?"))
            .SendMessage(a, b, "Yes, I can hear you!")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant m0 as Alice
    participant m1 as Bob
    m0 ->> m1: Hello Bob!
    loop Every minute
        m1 ->> m0: Hello Alice!
        note right of m1: Note
        m1 ->> m0: Can you hear me?
    end
    m0 ->> m1: Yes, I can hear you!", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithMultipleAlternatives()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
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
    participant m0 as Alice
    participant m1 as Bob
    m0 ->> m1: Hello Bob!
    alt Bob is happy
        m1 ->> m0: Hello Alice!
        m1 ->> m0: Can you hear me?
        alt Alice is happy
            m0 ->> m1: Yes, I can hear you!
        else Alice is sad
            m0 ->> m1: No, I can't hear you!
        end
    else Bob is sad
        m1 ->> m0: Hello Alice.
        alt Alice is happy
            m0 ->> m1: Sorry to hear that.
        else Alice is sad
            m0 ->> m1: Me too.
        end
    end
    m0 ->> m1: Bye", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithSingleAlternative()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .SendMessage(a, b, "Hello Bob!")
            .Alternatives(
                ("Bob is happy", builder => builder
                    .SendMessage(b, a, "Hello Alice!")
                    .SendMessage(b, a, "Can you hear me?")))
            .SendMessage(a, b, "Bye")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant m0 as Alice
    participant m1 as Bob
    m0 ->> m1: Hello Bob!
    alt Bob is happy
        m1 ->> m0: Hello Alice!
        m1 ->> m0: Can you hear me?
    end
    m0 ->> m1: Bye", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithZeroAlternatives()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .SendMessage(a, b, "Hello Bob!")
            .Alternatives()
            .SendMessage(a, b, "Bye")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant m0 as Alice
    participant m1 as Bob
    m0 ->> m1: Hello Bob!
    m0 ->> m1: Bye", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithOptional()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .SendMessage(a, b, "Hello Bob!")
            .Optional("Bob is happy", builder => builder
                .SendMessage(b, a, "Hello Alice!")
                .SendMessage(b, a, "Can you hear me?"))
            .SendMessage(a, b, "Bye")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant m0 as Alice
    participant m1 as Bob
    m0 ->> m1: Hello Bob!
    opt Bob is happy
        m1 ->> m0: Hello Alice!
        m1 ->> m0: Can you hear me?
    end
    m0 ->> m1: Bye", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithMultipleParallels()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .AddMember("Charlie", out Member c)
            .AddMember("David", out Member d)
            .AddMember("Eve", out Member e)
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
    participant m0 as Alice
    participant m1 as Bob
    participant m2 as Charlie
    participant m3 as David
    participant m4 as Eve
    par Alice to Bob
        m0 ->> m1: Hello Bob!
        m1 ->> m0: Hello Alice!
    and Eve to David
        m4 ->> m3: Hello David!
        m3 ->> m4: Hello Eve!
    and Bob to Eve
        m1 ->> m4: Hello Eve!
        m4 ->> m1: Hello Bob!
    and Alice to Charlie
        m0 ->> m2: Hello Charlie!
        m2 ->> m0: Hello Alice!
        par Charlie to Bob
            m2 ->> m1: Talked to Alice
        and Charlie to Eve
            m2 ->> m4: Talked to Alice
        end
    end", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithSingleParallel()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .AddMember("Charlie", out Member c)
            .Parallels(
                ("Alice to Bob", builder => builder
                    .SendMessage(a, b, "Hello Bob!")
                    .SendMessage(b, a, "Hello Alice!")))
            .SendMessage(a, c, "Hello Charlie!")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant m0 as Alice
    participant m1 as Bob
    participant m2 as Charlie
    par Alice to Bob
        m0 ->> m1: Hello Bob!
        m1 ->> m0: Hello Alice!
    end
    m0 ->> m2: Hello Charlie!", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithZeroParallels()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .Parallels()
            .SendMessage(a, b, "Hello Bob!")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant m0 as Alice
    participant m1 as Bob
    m0 ->> m1: Hello Bob!", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithCriticalAndMultipleOptions()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Service", out Member s)
            .AddMember("DB 1", out Member db1)
            .AddMember("DB 2", out Member db2)
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
    participant m0 as Service
    participant m1 as DB 1
    participant m2 as DB 2
    critical Connect to DB1
        m0 --> m1: Connect
        critical Connect to DB2
            m0 --> m2: Connect
        option Network error
            m0 --x m1: Disconnect
            m0 --> m0: Log error
        option Network timeout
            m0 --x m1: Disconnect
            m0 --> m0: Log timeout
        end
    option Network error
        m0 --> m0: Log error
    option Network timeout
        m0 --> m0: Log timeout
    end", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithCriticalAndNoOptions()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Service", out Member s)
            .AddMember("DB", out Member db1)
            .Critical("Connect to DB", builder => builder
                .SendMessage(s, db1, "Connect", LineType.Dotted, ArrowType.None))
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant m0 as Service
    participant m1 as DB
    critical Connect to DB
        m0 --> m1: Connect
    end", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithBreak()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .SendMessage(a, b, "Hello!")
            .Break("Something happens", builder => builder
                .SendMessage(a, b, "Bye!"))
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant m0 as Alice
    participant m1 as Bob
    m0 ->> m1: Hello!
    break Something happens
        m0 ->> m1: Bye!
    end", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithRectangles()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .AddRectangle(Color.AliceBlue, builder => builder
                .SendMessage(a, b, "Hello Bob!")
                .SendMessage(b, a, "Hello Alice!"))
            .AddRectangle(Color.DarkSeaGreen, builder => builder
                .SendMessage(a, b, "Can you hear me?")
                .SendMessage(b, a, "Yes, I can hear you."))
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant m0 as Alice
    participant m1 as Bob
    rect AliceBlue
        m0 ->> m1: Hello Bob!
        m1 ->> m0: Hello Alice!
    end
    rect DarkSeaGreen
        m0 ->> m1: Can you hear me?
        m1 ->> m0: Yes, I can hear you.
    end", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithComments()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .Comment("Alice is greeting Bob")
            .SendMessage(a, b, "Hello Bob!")
            .Comment("Bob is greeting Alice")
            .SendMessage(b, a, "Hello Alice!")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant m0 as Alice
    participant m1 as Bob
    %% Alice is greeting Bob
    m0 ->> m1: Hello Bob!
    %% Bob is greeting Alice
    m1 ->> m0: Hello Alice!", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithAutoNumber()
    {
        string diagram = Mermaid
            .SequenceDiagram(autonumber: true)
            .AddMember("Alice", out Member m1)
            .AddMember("Bob", out Member m2)
            .SendMessage(m1, m2, $"Hello {m2.Name}!")
            .SendMessage(m2, m1, $"Hello {m1.Name}!")
            .Build();

        Assert.Equal(@"sequenceDiagram
    autonumber
    participant m0 as Alice
    participant m1 as Bob
    m0 ->> m1: Hello Bob!
    m1 ->> m0: Hello Alice!", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildDiagramWithLinks()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .AddLink(a, "Dashboard", "https://dashboard.contoso.com/alice")
            .AddLink(a, "Wiki", "https://wiki.contoso.com/alice")
            .AddLink(b, "Dashboard", "https://dashboard.contoso.com/bob")
            .AddLink(b, "Wiki", "https://wiki.contoso.com/bob")
            .SendMessage(a, b, $"Hello {b.Name}!")
            .SendMessage(b, a, $"Hello {a.Name}!")
            .Build();

        Assert.Equal(@"sequenceDiagram
    participant m0 as Alice
    participant m1 as Bob
    link m0: Dashboard @ https://dashboard.contoso.com/alice
    link m0: Wiki @ https://wiki.contoso.com/alice
    link m1: Dashboard @ https://dashboard.contoso.com/bob
    link m1: Wiki @ https://wiki.contoso.com/bob
    m0 ->> m1: Hello Bob!
    m1 ->> m0: Hello Alice!", diagram, ignoreLineEndingDifferences: true);
    }
}