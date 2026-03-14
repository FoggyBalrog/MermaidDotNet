using System.Drawing;
using FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class SequenceDiagramDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithAllMembers()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildSimpleDiagram()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithNotes()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithBoxes()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithMemberCreationAndDestruction()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithActivation()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithLoops()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithMultipleAlternatives()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithSingleAlternative()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithZeroAlternatives()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .SendMessage(a, b, "Hello Bob!")
            .Alternatives()
            .SendMessage(a, b, "Bye")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithOptional()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithMultipleParallels()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithSingleParallel()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithZeroParallels()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .Parallels()
            .SendMessage(a, b, "Hello Bob!")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithCriticalAndMultipleOptions()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithCriticalAndNoOptions()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Service", out Member s)
            .AddMember("DB", out Member db1)
            .Critical("Connect to DB", builder => builder
                .SendMessage(s, db1, "Connect", LineType.Dotted, ArrowType.None))
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithBreak()
    {
        string diagram = Mermaid
            .SequenceDiagram()
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .SendMessage(a, b, "Hello!")
            .Break("Something happens", builder => builder
                .SendMessage(a, b, "Bye!"))
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithRectangles()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithComments()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithAutoNumber()
    {
        string diagram = Mermaid
            .SequenceDiagram(autonumber: true)
            .AddMember("Alice", out Member m1)
            .AddMember("Bob", out Member m2)
            .SendMessage(m1, m2, $"Hello {m2.Name}!")
            .SendMessage(m2, m1, $"Hello {m1.Name}!")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithLinks()
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }
}

public class SequenceDiagramNoValidationNoSanitizationOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildEmptyDiagram()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithAllMembers()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("Alice", out _, MemberType.Participant)
            .AddMember("Bob", out _, MemberType.Actor)
            .AddMember("Charlie", out _, MemberType.Boundary)
            .AddMember("David", out _, MemberType.Control)
            .AddMember("Eve", out _, MemberType.Entity)
            .AddMember("Frank", out _, MemberType.Database)
            .AddMember("Grace", out _, MemberType.Collections)
            .AddMember("Heidi", out _, MemberType.Queue)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildSimpleDiagram()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithNotes()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithBoxes()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithMemberCreationAndDestruction()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .SendMessage(a, b, $"Hello {b.Name}, how are you?")
            .SendMessage(b, a, "Fine, thank you. And you?")
            .SendCreateMessage(a, "Carl", out Member c, "Hi Carl!")
            .SendCreateMessage(c, "Donald", out _, "Hi!", MemberType.Actor)
            .SendDestroyMessage(a, c, DestructionTarget.Recipient, "We are too many", arrowType: ArrowType.Cross)
            .SendDestroyMessage(b, a, DestructionTarget.Sender, "I agree")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithActivation()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("Alice", out Member a)
            .AddMember("John", out Member j)
            .SendMessage(a, j, "Hello John, how are you?", activationType: ActivationType.Activate)
            .SendMessage(a, j, "John, can you hear me?", activationType: ActivationType.Activate)
            .SendMessage(j, a, "Hi Alice, I can hear you!", activationType: ActivationType.Deactivate)
            .SendMessage(j, a, "I feel great!", activationType: ActivationType.Deactivate)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithLoops()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .SendMessage(a, b, "Hello Bob!")
            .AddLoop("Every minute", builder => builder
                .SendMessage(b, a, "Hello Alice!")
                .AddNoteRightOf(b, "Note")
                .SendMessage(b, a, "Can you hear me?"))
            .SendMessage(a, b, "Yes, I can hear you!")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithMultipleAlternatives()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithSingleAlternative()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .SendMessage(a, b, "Hello Bob!")
            .Alternatives(
                ("Bob is happy", builder => builder
                    .SendMessage(b, a, "Hello Alice!")
                    .SendMessage(b, a, "Can you hear me?")))
            .SendMessage(a, b, "Bye")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithZeroAlternatives()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .SendMessage(a, b, "Hello Bob!")
            .Alternatives()
            .SendMessage(a, b, "Bye")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithOptional()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .SendMessage(a, b, "Hello Bob!")
            .Optional("Bob is happy", builder => builder
                .SendMessage(b, a, "Hello Alice!")
                .SendMessage(b, a, "Can you hear me?"))
            .SendMessage(a, b, "Bye")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithMultipleParallels()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithSingleParallel()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .AddMember("Charlie", out Member c)
            .Parallels(
                ("Alice to Bob", builder => builder
                    .SendMessage(a, b, "Hello Bob!")
                    .SendMessage(b, a, "Hello Alice!")))
            .SendMessage(a, c, "Hello Charlie!")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithZeroParallels()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .Parallels()
            .SendMessage(a, b, "Hello Bob!")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithCriticalAndMultipleOptions()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
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

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithCriticalAndNoOptions()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("Service", out Member s)
            .AddMember("DB", out Member db1)
            .Critical("Connect to DB", builder => builder
                .SendMessage(s, db1, "Connect", LineType.Dotted, ArrowType.None))
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithBreak()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .SendMessage(a, b, "Hello!")
            .Break("Something happens", builder => builder
                .SendMessage(a, b, "Bye!"))
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithRectangles()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .AddRectangle(Color.AliceBlue, builder => builder
                .SendMessage(a, b, "Hello Bob!")
                .SendMessage(b, a, "Hello Alice!"))
            .AddRectangle(Color.DarkSeaGreen, builder => builder
                .SendMessage(a, b, "Can you hear me?")
                .SendMessage(b, a, "Yes, I can hear you."))
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithComments()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .Comment("Alice is greeting Bob")
            .SendMessage(a, b, "Hello Bob!")
            .Comment("Bob is greeting Alice")
            .SendMessage(b, a, "Hello Alice!")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithAutoNumber()
    {
        string diagram = Mermaid
            .SequenceDiagram(autonumber: true, options: _options)
            .AddMember("Alice", out Member m1)
            .AddMember("Bob", out Member m2)
            .SendMessage(m1, m2, $"Hello {m2.Name}!")
            .SendMessage(m2, m1, $"Hello {m1.Name}!")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildDiagramWithLinks()
    {
        string diagram = Mermaid
            .SequenceDiagram(options: _options)
            .AddMember("Alice", out Member a)
            .AddMember("Bob", out Member b)
            .AddLink(a, "Dashboard", "https://dashboard.contoso.com/alice")
            .AddLink(a, "Wiki", "https://wiki.contoso.com/alice")
            .AddLink(b, "Dashboard", "https://dashboard.contoso.com/bob")
            .AddLink(b, "Wiki", "https://wiki.contoso.com/bob")
            .SendMessage(a, b, $"Hello {b.Name}!")
            .SendMessage(b, a, $"Hello {a.Name}!")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("sequence", toolingFixture.GetDiagramType(diagramResult));
    }
}
