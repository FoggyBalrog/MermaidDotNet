using FoggyBalrog.MermaidDotNet.StateDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.StateDiagram;

public class StateDiagramSafeModeBuilderTests
{
    [Fact]
    public void CanBuildSimpleStateDiagramWithTitle()
    {
        string diagram = Mermaid
            .StateDiagram("My title")
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddTransitionFromStart(s1)
            .AddStateTransition(s1, s2)
            .AddTransitionToEnd(s2)
            .Build();

        Assert.Equal(@"---
title: My title
---
stateDiagram-v2
    s1 : State 1
    s2 : State 2
    [*] --> s1
    s1 --> s2
    s2 --> [*]", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildSimpleStateDiagramWithoutTitle()
    {
        string diagram = Mermaid
            .StateDiagram()
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddTransitionFromStart(s1)
            .AddStateTransition(s1, s2)
            .AddTransitionToEnd(s2)
            .Build();

        Assert.Equal(@"stateDiagram-v2
    s1 : State 1
    s2 : State 2
    [*] --> s1
    s1 --> s2
    s2 --> [*]", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildStateDiagramWithDirection()
    {
        string diagram = Mermaid
            .StateDiagram(direction: StateDiagramDirection.RightToLeft)
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddTransitionFromStart(s1)
            .AddStateTransition(s1, s2)
            .AddTransitionToEnd(s2)
            .Build();

        Assert.Equal(@"stateDiagram-v2
    direction RL
    s1 : State 1
    s2 : State 2
    [*] --> s1
    s1 --> s2
    s2 --> [*]", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildStateDiagramWithTransitionDescriptions()
    {
        string diagram = Mermaid
            .StateDiagram()
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddTransitionFromStart(s1, "foo")
            .AddStateTransition(s1, s2, "bar")
            .AddTransitionToEnd(s2, "baz")
            .Build();

        Assert.Equal(@"stateDiagram-v2
    s1 : State 1
    s2 : State 2
    [*] --> s1 : foo
    s1 --> s2 : bar
    s2 --> [*] : baz", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildStateDiagramWithCompositeStates()
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

        Assert.Equal(@"stateDiagram-v2
    s1 : State 1
    state ""Composite 1"" as s2 {
        s3 : State 2
        s4 : State 3
        s3 --> s4
        s1 --> s3
        s4 --> [*]
        state ""Composite 2"" as s8 {
            s9 : State 2a
            s10 : State 2b
            s9 --> s10
            s3 --> s9
            s1 --> s10
            s10 --> [*]
        }
    }
    s17 : State 4
    [*] --> s2
    s1 --> s2
    s2 --> s17
    s17 --> [*]", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildStateDiagramWithChoiceStates()
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

        Assert.Equal(@"stateDiagram-v2
    s1 : State 1
    state s2 <<choice>>
    s3 : State 2
    s4 : State 3
    s1 --> s2
    s2 --> s3 : foo
    s2 --> s4 : bar
    s3 --> [*]
    s4 --> [*]", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildStateDiagramWithForkAndJoinStates()
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

        Assert.Equal(@"stateDiagram-v2
    s1 : State 1
    state s2 <<fork>>
    s3 : State 2
    s4 : State 3
    state s5 <<join>>
    s6 : State 4
    s1 --> s2
    s2 --> s3
    s2 --> s4
    s3 --> s5
    s4 --> s5
    s5 --> s6
    s6 --> [*]", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildStateDiagramWithNotes()
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

        Assert.Equal(@"stateDiagram-v2
    s1 : State 1
    s2 : State 2
    note right of s1
        foo
    end note
    note left of s2
        bar
    end note
    [*] --> s1
    s1 --> s2
    s2 --> [*]", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildStateDiagramWithConcurrency()
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

        Assert.Equal(@"stateDiagram-v2
    state ""Active"" as s1 {
        s2 : State 2
        s3 : State 3
        [*] --> s2
        s2 --> s3
        s3 --> [*]
        --
        s8 : State 4
        s9 : State 5
        [*] --> s8
        s8 --> s9
        s9 --> [*]
        --
        s14 : State 6
        s15 : State 7
        [*] --> s14
        s14 --> s15
        s15 --> [*]
    }
    [*] --> s1
    s1 --> [*]", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildStateDiagramWithCustomStyles()
    {
        string diagram = Mermaid
            .StateDiagram()
            .AddState("State 1", out State s1)
            .AddState("State 2", out State s2)
            .AddState("State 3", out State s3)
            .StyleWithRawCss(s1, "fill:#f00,color:white,font-weight:bold,stroke-width:2px,stroke:yellow")
            .StyleWithCssClass("foo", s2, s3)
            .AddTransitionFromStart(s1)
            .AddStateTransition(s1, s2)
            .AddStateTransition(s2, s3)
            .AddTransitionToEnd(s3)
            .Build();

        Assert.Equal(@"stateDiagram-v2
    s1 : State 1
    s2 : State 2
    s3 : State 3
    classDef s1 fill:#f00,color:white,font-weight:bold,stroke-width:2px,stroke:yellow
    class s2,s3 foo
    [*] --> s1
    s1 --> s2
    s2 --> s3
    s3 --> [*]", diagram, ignoreLineEndingDifferences: true);
    }
}
