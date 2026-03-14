using FoggyBalrog.MermaidDotNet.StateDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class StateDiagramDefaultOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildSimpleStateDiagramWithTitle()
    {
        string diagram = Mermaid
            .StateDiagram("My title")
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddTransitionFromStart(s1)
            .AddStateTransition(s1, s2)
            .AddTransitionToEnd(s2)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildSimpleStateDiagramWithoutTitle()
    {
        string diagram = Mermaid
            .StateDiagram()
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddTransitionFromStart(s1)
            .AddStateTransition(s1, s2)
            .AddTransitionToEnd(s2)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithLinks()
    {
        string diagram = Mermaid
            .StateDiagram()
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddStateLink(s1, "https://example.com/state1")
            .AddStateLink(s2, "https://example.com/state2", "State 2 Tooltip")
            .AddTransitionFromStart(s1)
            .AddStateTransition(s1, s2)
            .AddTransitionToEnd(s2)
            .Build();
        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithDirection()
    {
        string diagram = Mermaid
            .StateDiagram(direction: StateDiagramDirection.RightToLeft)
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddTransitionFromStart(s1)
            .AddStateTransition(s1, s2)
            .AddTransitionToEnd(s2)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithTransitionDescriptions()
    {
        string diagram = Mermaid
            .StateDiagram()
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddTransitionFromStart(s1, "foo")
            .AddStateTransition(s1, s2, "bar")
            .AddTransitionToEnd(s2, "baz")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithCompositeStates()
    {
        string diagram = Mermaid
            .StateDiagram()
            .AddState("State 1", out State s1)
            .AddCompositeState("Composite 1", out State c1, builder => builder
                .AddState("State 2", out State s2)
                .AddState("State 3", out State s3)
                .AddStateTransition(s2, s3)
                .AddStateTransition(s1, s2)
                .AddTransitionToEnd(s3)
                .AddCompositeState("Composite 2", out State _, builder2 => builder2
                    .AddState("State 2a", out State s2a)
                    .AddState("State 2b", out State s2b)
                    .AddStateTransition(s2a, s2b)
                    .AddStateTransition(s2, s2a)
                    .AddStateTransition(s1, s2b)
                    .AddTransitionToEnd(s2b)))
            .AddState("State 4", out State s4)
            .AddTransitionFromStart(c1)
            .AddStateTransition(s1, c1)
            .AddStateTransition(c1, s4)
            .AddTransitionToEnd(s4)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithChoiceStates()
    {
        string diagram = Mermaid
            .StateDiagram()
            .AddState("State 1", out State s1)
            .AddChoiceState(out State c1)
            .AddState("State 2", out State s2)
            .AddState("State 3", out State s3)
            .AddStateTransition(s1, c1)
            .AddStateTransition(c1, s2, "foo")
            .AddStateTransition(c1, s3, "bar")
            .AddTransitionToEnd(s2)
            .AddTransitionToEnd(s3)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithForkAndJoinStates()
    {
        string diagram = Mermaid
            .StateDiagram()
            .AddState("State 1", out State s1)
            .AddForkState(out State f1)
            .AddState("State 2", out State s2)
            .AddState("State 3", out State s3)
            .AddJoinState(out State j1)
            .AddState("State 4", out State s4)
            .AddStateTransition(s1, f1)
            .AddStateTransition(f1, s2)
            .AddStateTransition(f1, s3)
            .AddStateTransition(s2, j1)
            .AddStateTransition(s3, j1)
            .AddStateTransition(j1, s4)
            .AddTransitionToEnd(s4)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithNotes()
    {
        string diagram = Mermaid
            .StateDiagram()
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddNote(s1, NotePosition.Right, "foo")
            .AddNote(s2, NotePosition.Left, "bar")
            .AddTransitionFromStart(s1)
            .AddStateTransition(s1, s2)
            .AddTransitionToEnd(s2)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithConcurrency()
    {
        string diagram = Mermaid
            .StateDiagram()
            .AddConcurrency("Active", out State c1,
                builder1 => builder1
                .AddState("State 2", out State s2)
                .AddState("State 3", out State s3)
                .AddTransitionFromStart(s2)
                .AddStateTransition(s2, s3)
                .AddTransitionToEnd(s3),
                builder2 => builder2
                .AddState("State 4", out State s4)
                .AddState("State 5", out State s5)
                .AddTransitionFromStart(s4)
                .AddStateTransition(s4, s5)
                .AddTransitionToEnd(s5),
                builder3 => builder3
                .AddState("State 6", out State s6)
                .AddState("State 7", out State s7)
                .AddTransitionFromStart(s6)
                .AddStateTransition(s6, s7)
                .AddTransitionToEnd(s7))
            .AddTransitionFromStart(c1)
            .AddTransitionToEnd(c1)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithCustomStyles()
    {
        string diagram = Mermaid
            .StateDiagram()
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddState("State 3", out State s3)
            .DefineCssClass("foo", "fill:#f00,color:white,font-weight:bold,stroke-width:2px,stroke:yellow", out var fooCssClass)
            .StyleWithCssClass(fooCssClass, s2, s3)
            .AddTransitionFromStart(s1)
            .AddStateTransition(s1, s2)
            .AddStateTransition(s2, s3)
            .AddTransitionToEnd(s3)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }
}

public class StateDiagramNoValidationNoSanitizationOptionsBuilderIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildSimpleStateDiagramWithTitle()
    {
        string diagram = Mermaid
            .StateDiagram("My title", options: _options)
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddTransitionFromStart(s1)
            .AddStateTransition(s1, s2)
            .AddTransitionToEnd(s2)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildSimpleStateDiagramWithoutTitle()
    {
        string diagram = Mermaid
            .StateDiagram(options: _options)
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddTransitionFromStart(s1)
            .AddStateTransition(s1, s2)
            .AddTransitionToEnd(s2)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithLinks()
    {
        string diagram = Mermaid
            .StateDiagram(options: _options)
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddStateLink(s1, "https://example.com/state1")
            .AddStateLink(s2, "https://example.com/state2", "State 2 Tooltip")
            .AddTransitionFromStart(s1)
            .AddStateTransition(s1, s2)
            .AddTransitionToEnd(s2)
            .Build();
        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithDirection()
    {
        string diagram = Mermaid
            .StateDiagram(direction: StateDiagramDirection.RightToLeft, options: _options)
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddTransitionFromStart(s1)
            .AddStateTransition(s1, s2)
            .AddTransitionToEnd(s2)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithTransitionDescriptions()
    {
        string diagram = Mermaid
            .StateDiagram(options: _options)
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddTransitionFromStart(s1, "foo")
            .AddStateTransition(s1, s2, "bar")
            .AddTransitionToEnd(s2, "baz")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithCompositeStates()
    {
        string diagram = Mermaid
            .StateDiagram(options: _options)
            .AddState("State 1", out State s1)
            .AddCompositeState("Composite 1", out State c1, builder => builder
                .AddState("State 2", out State s2)
                .AddState("State 3", out State s3)
                .AddStateTransition(s2, s3)
                .AddStateTransition(s1, s2)
                .AddTransitionToEnd(s3)
                .AddCompositeState("Composite 2", out State _, builder2 => builder2
                    .AddState("State 2a", out State s2a)
                    .AddState("State 2b", out State s2b)
                    .AddStateTransition(s2a, s2b)
                    .AddStateTransition(s2, s2a)
                    .AddStateTransition(s1, s2b)
                    .AddTransitionToEnd(s2b)))
            .AddState("State 4", out State s4)
            .AddTransitionFromStart(c1)
            .AddStateTransition(s1, c1)
            .AddStateTransition(c1, s4)
            .AddTransitionToEnd(s4)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithChoiceStates()
    {
        string diagram = Mermaid
            .StateDiagram(options: _options)
            .AddState("State 1", out State s1)
            .AddChoiceState(out State c1)
            .AddState("State 2", out State s2)
            .AddState("State 3", out State s3)
            .AddStateTransition(s1, c1)
            .AddStateTransition(c1, s2, "foo")
            .AddStateTransition(c1, s3, "bar")
            .AddTransitionToEnd(s2)
            .AddTransitionToEnd(s3)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithForkAndJoinStates()
    {
        string diagram = Mermaid
            .StateDiagram(options: _options)
            .AddState("State 1", out State s1)
            .AddForkState(out State f1)
            .AddState("State 2", out State s2)
            .AddState("State 3", out State s3)
            .AddJoinState(out State j1)
            .AddState("State 4", out State s4)
            .AddStateTransition(s1, f1)
            .AddStateTransition(f1, s2)
            .AddStateTransition(f1, s3)
            .AddStateTransition(s2, j1)
            .AddStateTransition(s3, j1)
            .AddStateTransition(j1, s4)
            .AddTransitionToEnd(s4)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithNotes()
    {
        string diagram = Mermaid
            .StateDiagram(options: _options)
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddNote(s1, NotePosition.Right, "foo")
            .AddNote(s2, NotePosition.Left, "bar")
            .AddTransitionFromStart(s1)
            .AddStateTransition(s1, s2)
            .AddTransitionToEnd(s2)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithConcurrency()
    {
        string diagram = Mermaid
            .StateDiagram(options: _options)
            .AddConcurrency("Active", out State c1,
                builder1 => builder1
                .AddState("State 2", out State s2)
                .AddState("State 3", out State s3)
                .AddTransitionFromStart(s2)
                .AddStateTransition(s2, s3)
                .AddTransitionToEnd(s3),
                builder2 => builder2
                .AddState("State 4", out State s4)
                .AddState("State 5", out State s5)
                .AddTransitionFromStart(s4)
                .AddStateTransition(s4, s5)
                .AddTransitionToEnd(s5),
                builder3 => builder3
                .AddState("State 6", out State s6)
                .AddState("State 7", out State s7)
                .AddTransitionFromStart(s6)
                .AddStateTransition(s6, s7)
                .AddTransitionToEnd(s7))
            .AddTransitionFromStart(c1)
            .AddTransitionToEnd(c1)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildStateDiagramWithCustomStyles()
    {
        string diagram = Mermaid
            .StateDiagram(options: _options)
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddState("State 3", out State s3)
            .DefineCssClass("foo", "fill:#f00,color:white,font-weight:bold,stroke-width:2px,stroke:yellow", out var fooCssClass)
            .StyleWithCssClass(fooCssClass, s2, s3)
            .AddTransitionFromStart(s1)
            .AddStateTransition(s1, s2)
            .AddStateTransition(s2, s3)
            .AddTransitionToEnd(s3)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("stateDiagram", toolingFixture.GetDiagramType(diagramResult));
    }
}
