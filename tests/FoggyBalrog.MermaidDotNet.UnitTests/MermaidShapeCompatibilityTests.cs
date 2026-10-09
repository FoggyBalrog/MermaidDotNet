using FoggyBalrog.MermaidDotNet.Flowchart.Model;
using FoggyBalrog.MermaidDotNet.SequenceDiagram.Model;
using static FoggyBalrog.MermaidDotNet.UnitTests.CompatibilityTestHelpers;

namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class MermaidShapeCompatibilityTests
{
    public static IEnumerable<object[]> ExpandedShapes => Enum.GetValues<ExpandedNodeShape>().Select(shape => new object[] { shape });
    public static IEnumerable<object[]> Curves => Enum.GetValues<CurveStyle>().SelectMany(curve => new[] { new object[] { curve, false }, new object[] { curve, true } });
    public static IEnumerable<object[]> ParticipantShapes => Enum.GetValues<MemberType>()
        .Where(type => type is not MemberType.Actor and not MemberType.Participant)
        .SelectMany(type => new[] { new object[] { type, false, false }, new object[] { type, false, true }, new object[] { type, true, false } });

    [Theory]
    [MemberData(nameof(ExpandedShapes))]
    public void ExpandedShape_EveryValueHonorsBoundaryAndDoesNotMutateOnRejection(ExpandedNodeShape shape)
    {
        foreach (var (options, allowed) in Cases(3))
        {
            var builder = Mermaid.Flowchart(options: options).AddNode("Before", out _);
            string before = builder.Build();
            if (allowed)
            {
                builder.AddNodeWithExpandedShape("Expanded", out var node, shape);
                Assert.Equal(shape, node.ExpandedShape);
                Assert.Contains("shape:", builder.Build());
                Assert.Contains("Expanded", builder.Build());
            }
            else
            {
                AssertIncompatible(() => builder.AddNodeWithExpandedShape("Expanded", out _, shape));
                Assert.Equal(before, builder.Build());
                builder.AddNode("After", out var after);
                Assert.Equal("id2", after.Id);
            }
        }
    }

    [Theory]
    [MemberData(nameof(Curves))]
    public void EdgeCurve_EveryValueAndBothApisHonorBoundaryAndPreserveLinkIds(CurveStyle curve, bool chain)
    {
        foreach (var (options, allowed) in Cases(10))
        {
            var builder = Mermaid.Flowchart(options: options).AddNode("From", out var from).AddNode("To", out var to);
            string before = builder.Build();
            Action add = () =>
            {
                if (chain) builder.AddLinkChain([from], [to], out _, curveStyle: curve);
                else builder.AddLink(from, to, out _, curveStyle: curve);
            };
            if (allowed)
            {
                add();
                Assert.Contains("e0@", builder.Build());
                Assert.Contains("curve:", builder.Build());
            }
            else
            {
                AssertIncompatible(add);
                Assert.Equal(before, builder.Build());
                builder.AddLink(from, to, out var link);
                Assert.Equal(0, link.Id);
                Assert.DoesNotContain("curve:", builder.Build());
            }
        }
    }

    [Theory]
    [MemberData(nameof(ParticipantShapes))]
    public void SequenceShapes_AllTypesAndDeclarationPathsHonorBoundary(MemberType type, bool create, bool boxed)
    {
        foreach (var (options, allowed) in Cases(13))
        {
            var builder = Mermaid.SequenceDiagram(options: options).AddMember("Sender", out var sender);
            Box? box = null;
            if (boxed) builder.AddBox("Group", out box);
            string before = builder.Build();
            Action add = () =>
            {
                if (create) builder.SendCreateMessage(sender, "Display Name", out _, "Create", type);
                else builder.AddMember("Display Name", out _, type, box);
            };
            if (allowed)
            {
                add();
                string declaration = $"participant m1@{{ \"type\" : \"{type.ToString().ToLowerInvariant()}\" }} as Display Name";
                Assert.Contains(create ? "create " + declaration : declaration, builder.Build());
                if (create) Assert.Contains("m0 ->> m1: Create", builder.Build());
                else builder.AddMember("Next", out _);
            }
            else
            {
                AssertIncompatible(add);
                Assert.Equal(before, builder.Build());
                builder.AddMember("Display Name", out var next);
                Assert.Equal("m1", next.Id);
                Assert.Contains("participant m1 as Display Name", builder.Build());
            }
        }
    }

    [Theory]
    [InlineData(MemberType.Actor)]
    [InlineData(MemberType.Participant)]
    public void OrdinarySequenceMembersAndCreateMessagesRemainAvailableAtBaseline(MemberType type)
    {
        var builder = Mermaid.SequenceDiagram(options: new() { TargetMermaidVersion = MermaidVersion.V11_0 })
            .AddMember("Sender", out var sender, type)
            .SendCreateMessage(sender, "Recipient", out _, "Create", type);
        string keyword = type == MemberType.Actor ? "actor" : "participant";
        Assert.Contains($"{keyword} m0 as Sender", builder.Build());
        Assert.Contains($"create {keyword} m1 as Recipient", builder.Build());
        Assert.DoesNotContain("@{", builder.Build());
    }

    [Fact]
    public void OrdinaryNodesAndLinksRemainAvailableAtBaseline()
    {
        var builder = Mermaid.Flowchart(options: new() { TargetMermaidVersion = MermaidVersion.V11_0 })
            .AddNode("From", out var from).AddNode("To", out var to)
            .AddLink(from, to, out _, curveStyle: null)
            .AddLinkChain([from], [to], out _, curveStyle: null);
        Assert.DoesNotContain("curve:", builder.Build());
    }

    [Fact]
    public void DefaultStrictOptions_AllowExpandedShapesButRejectCurvesAndParticipantShapes()
    {
        var flow = Mermaid.Flowchart().AddNodeWithExpandedShape("From", out var from, ExpandedNodeShape.Rect).AddNode("To", out var to);
        Assert.Contains("shape: rect", flow.Build());
        AssertIncompatible(() => flow.AddLink(from, to, out _, curveStyle: CurveStyle.Linear));
        AssertIncompatible(() => flow.AddLinkChain([from], [to], out _, curveStyle: CurveStyle.Linear));
        var sequence = Mermaid.SequenceDiagram().AddMember("Sender", out var sender);
        AssertIncompatible(() => sequence.AddMember("Recipient", out _, MemberType.Database));
        AssertIncompatible(() => sequence.SendCreateMessage(sender, "Recipient", out _, "Create", MemberType.Database));
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void UncheckedMode_DoesNotDisableShapeAndCurveInputValidation(bool create)
    {
        var options = new MermaidDotNetOptions { CompatibilityMode = MermaidCompatibilityMode.Unchecked };
        var flow = Mermaid.Flowchart(options: options).AddNode("From", out var from).AddNode("To", out var to);
        var curveError = Assert.Throws<MermaidException>(() => flow.AddLink(from, to, out _, curveStyle: CurveStyle.Linear, extraLength: -1));
        Assert.Equal(MermaidExceptionReason.StrictlyNegative, curveError.Reason);
        var chainError = Assert.Throws<MermaidException>(() => flow.AddLinkChain([from], [to], out _, curveStyle: CurveStyle.Linear, extraLength: -1));
        Assert.Equal(MermaidExceptionReason.StrictlyNegative, chainError.Reason);
        var shapeError = Assert.Throws<MermaidException>(() => flow.AddNodeWithExpandedShape(" ", out _, ExpandedNodeShape.Rect));
        Assert.Equal(MermaidExceptionReason.WhiteSpace, shapeError.Reason);
        var sequence = Mermaid.SequenceDiagram(options: options).AddMember("Sender", out var sender);
        var memberError = Assert.Throws<MermaidException>(() =>
        {
            if (create) sequence.SendCreateMessage(sender, " ", out _, "Create", MemberType.Queue);
            else sequence.AddMember(" ", out _, MemberType.Queue);
        });
        Assert.Equal(MermaidExceptionReason.WhiteSpace, memberError.Reason);
    }

    [Fact]
    public void FeatureGates_UseSnapshottedOptionsRatherThanCallerMutation()
    {
        var options = new MermaidDotNetOptions();
        var builder = Mermaid.SequenceDiagram(options: options);
        options.TargetMermaidVersion = MermaidVersion.V11_13;
        options.CompatibilityMode = MermaidCompatibilityMode.Unchecked;
        AssertIncompatible(() => builder.AddMember("Database", out _, MemberType.Database));

        options.CompatibilityMode = MermaidCompatibilityMode.Strict;
        var supported = Mermaid.SequenceDiagram(options: options);
        options.TargetMermaidVersion = MermaidVersion.V11_0;
        supported.AddMember("Database", out _, MemberType.Database);
        Assert.Contains("database", supported.Build());
    }

}
