using System.Collections;
using System.Reflection;
using static FoggyBalrog.MermaidDotNet.UnitTests.CompatibilityTestHelpers;

namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class MermaidSyntaxCompatibilityTests
{
    private static readonly DateTimeOffset Date = new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void VerticalMarkers_HonorBoundaryWithAndWithoutOffset(bool offset)
    {
        foreach (var (options, allowed) in Cases(7))
        {
            var builder = Mermaid.GanttDiagram(options: options);
            string before = builder.Build();
            Action add = () => builder.AddVerticalMarker("Marker", Date, offset ? TimeSpan.FromDays(1) : null);
            if (allowed)
            {
                add();
                Assert.Contains("Marker: vert, vert1, 2026-01-01", builder.Build());
            }
            else
            {
                AssertIncompatible(add);
                Assert.Equal(before, builder.Build());
                builder.AddTask("Task", Date, TimeSpan.FromDays(1), out _);
                Assert.DoesNotContain("Marker", builder.Build());
                Assert.Contains("Task", builder.Build());
            }
        }
    }

    [Theory]
    [InlineData(null)]
    [InlineData("Tooltip")]
    public void StateHyperlinks_HonorBoundaryWithAndWithoutTooltip(string? tooltip)
    {
        foreach (var (options, allowed) in Cases(7))
        {
            var builder = Mermaid.StateDiagram(options: options).AddState("State", out var state);
            string before = builder.Build();
            Action add = () => builder.AddStateLink(state, "https://example.com", tooltip);
            if (allowed)
            {
                add();
                Assert.Contains(tooltip is null ? "click s1 href \"https://example.com\"" : "click s1 \"https://example.com\" \"Tooltip\"", builder.Build());
            }
            else
            {
                AssertIncompatible(add);
                Assert.Equal(before, builder.Build());
                builder.AddTransitionFromStart(state);
                Assert.DoesNotContain("click", builder.Build());
                Assert.Contains("[*] --> s1", builder.Build());
            }
        }
    }

    [Theory]
    [InlineData(null)]
    [InlineData("Field")]
    public void RelativePacketFields_HaveAnIndependentMethodAndDiagramBoundary(string? description)
    {
        foreach (var (options, allowed) in Cases(7))
        {
            var builder = Mermaid.PacketDiagram(options: options).AddFieldWithEnd(7, "Header");
            if (allowed)
            {
                builder.AddFieldWithBits(8, description);
                if (options.CompatibilityMode == MermaidCompatibilityMode.Strict && options.TargetMermaidVersion < MermaidVersion.V11_9)
                    AssertIncompatible(() => builder.Build());
                else
                    Assert.Contains($"+8: \"{description}\"", builder.Build());
            }
            else
            {
                // The bare packet header is itself unavailable at this target, so inspect the collection
                // rather than bypassing the builder's immutable compatibility snapshot to call Build.
                var field = builder.GetType().GetField("_fields", BindingFlags.Instance | BindingFlags.NonPublic);
                Assert.NotNull(field);
                var fields = Assert.IsAssignableFrom<ICollection>(field.GetValue(builder));
                Assert.Single(fields);
                AssertIncompatible(() => builder.AddFieldWithBits(8, description));
                Assert.Single(fields);
                builder.AddFieldWithEnd(15, "Next");
                Assert.Equal(2, fields.Count);
            }
        }

        string diagram = Mermaid.PacketDiagram(options: new() { TargetMermaidVersion = MermaidVersion.V11_9 })
            .AddFieldWithEnd(7, "Header").AddFieldWithBits(8, description).AddFieldWithEnd(23, "Next").Build();
        Assert.Contains($"+8: \"{description}\"", diagram);
        Assert.Contains("16-23: \"Next\"", diagram);
    }

    [Fact]
    public void DottedNamespaces_HonorBoundaryAndDoNotInvokeRejectedCallback()
    {
        foreach (var (options, allowed) in Cases(3))
        {
            var builder = Mermaid.ClassDiagram(options: options).AddClass("Before", out _);
            string before = builder.Build();
            bool invoked = false;
            Action add = () => builder.AddNamespace("Company.Product", ns =>
            {
                invoked = true;
                ns.AddClass("Inside", out _);
            });
            if (allowed)
            {
                add();
                Assert.True(invoked);
                Assert.Contains("namespace Company.Product {", builder.Build());
                Assert.Contains("class Inside", builder.Build());
            }
            else
            {
                AssertIncompatible(add);
                Assert.False(invoked);
                Assert.Equal(before, builder.Build());
                builder.AddNamespace("Plain", ns => ns.AddClass("After", out _));
                Assert.Contains("namespace Plain", builder.Build());
                Assert.DoesNotContain("Company", builder.Build());
            }
        }
    }

    [Fact]
    public void NestedNamespaces_HonorFutureBoundaryWithoutMutatingRejectedInnerNamespace()
    {
        foreach (var (options, allowed) in Cases(15))
        {
            var builder = Mermaid.ClassDiagram(options: options);
            bool invoked = false;
            builder.AddNamespace("Outer", outer =>
            {
                Action add = () => outer.AddNamespace("Inner", inner =>
                {
                    invoked = true;
                    inner.AddClass("Inside", out _);
                });
                if (allowed) add();
                else AssertIncompatible(add);
                outer.AddClass("After", out _);
            });
            Assert.Equal(allowed, invoked);
            string diagram = builder.Build();
            Assert.Contains("namespace Outer", diagram);
            Assert.Contains("class After", diagram);
            if (allowed) Assert.Contains("namespace Inner", diagram);
            else
            {
                Assert.DoesNotContain("Inner", diagram);
                Assert.Equal(Mermaid.ClassDiagram(options: options)
                    .AddNamespace("Outer", outer => outer.AddClass("After", out _)).Build(), diagram);
            }
            // Closing a namespace must restore the depth, not gate a later sibling as nested.
            builder.AddNamespace("Sibling", sibling => sibling.AddClass("SiblingClass", out _));
            Assert.Contains("namespace Sibling", builder.Build());
        }
    }

    [Fact]
    public void NamespaceCallbackFailure_ClosesScopeAndRestoresDepth()
    {
        var builder = Mermaid.ClassDiagram();
        var failure = new InvalidOperationException("Callback failed.");

        var exception = Assert.Throws<InvalidOperationException>(() => builder.AddNamespace("First", ns =>
        {
            ns.AddClass("BeforeFailure", out _);
            throw failure;
        }));

        Assert.Same(failure, exception);
        builder.AddNamespace("Sibling", ns =>
        {
            // The failed callback must not leave this sibling nested or disable nested checks.
            AssertIncompatible(() => ns.AddNamespace("Nested", _ => { }));
            ns.AddClass("AfterFailure", out _);
        });

        string expected = Mermaid.ClassDiagram()
            .AddNamespace("First", ns => ns.AddClass("BeforeFailure", out _))
            .AddNamespace("Sibling", ns => ns.AddClass("AfterFailure", out _))
            .Build();
        Assert.Equal(expected, builder.Build());
    }

    [Fact]
    public void RejectedNestedNamespace_UnwindsOuterScopeAndAllowsSiblings()
    {
        var builder = Mermaid.ClassDiagram();
        bool invoked = false;

        AssertIncompatible(() => builder.AddNamespace("Outer", outer =>
        {
            outer.AddClass("BeforeRejection", out _);
            outer.AddNamespace("Inner", _ => invoked = true);
        }));

        Assert.False(invoked);
        builder.AddNamespace("Sibling", ns => ns.AddClass("AfterRejection", out _));

        string expected = Mermaid.ClassDiagram()
            .AddNamespace("Outer", ns => ns.AddClass("BeforeRejection", out _))
            .AddNamespace("Sibling", ns => ns.AddClass("AfterRejection", out _))
            .Build();
        Assert.Equal(expected, builder.Build());
    }

    [Fact]
    public void DefaultStrictOptions_RejectNewSyntaxButAllowDottedAndSiblingNamespaces()
    {
        AssertIncompatible(() => Mermaid.GanttDiagram().AddVerticalMarker("Marker", Date));
        var state = Mermaid.StateDiagram().AddState("State", out var item);
        AssertIncompatible(() => state.AddStateLink(item, "https://example.com"));
        AssertIncompatible(() => Mermaid.PacketDiagram().AddFieldWithBits(8));
        var classes = Mermaid.ClassDiagram();
        classes.AddNamespace("Company.Product", outer =>
        {
            AssertIncompatible(() => outer.AddNamespace("Inner", _ => { }));
            outer.AddClass("Item", out _);
        });
        classes.AddNamespace("Sibling", _ => { });
        Assert.Contains("namespace Company.Product", classes.Build());
    }

    [Fact]
    public void UncheckedSyntax_StillValidatesInputs()
    {
        var options = new MermaidDotNetOptions { CompatibilityMode = MermaidCompatibilityMode.Unchecked };
        Assert.Equal(MermaidExceptionReason.WhiteSpace,
            Assert.Throws<MermaidException>(() => Mermaid.GanttDiagram(options: options).AddVerticalMarker(" ", Date)).Reason);
        Assert.Equal(MermaidExceptionReason.StrictlyNegative,
            Assert.Throws<MermaidException>(() => Mermaid.PacketDiagram(options: options).AddFieldWithBits(-1)).Reason);
        var state = Mermaid.StateDiagram(options: options).AddState("State", out var item);
        Assert.Equal(MermaidExceptionReason.WhiteSpace,
            Assert.Throws<MermaidException>(() => state.AddStateLink(item, " ")).Reason);
        Assert.Equal(MermaidExceptionReason.WhiteSpace,
            Assert.Throws<MermaidException>(() => Mermaid.ClassDiagram(options: options).AddNamespace(" ", _ => { })).Reason);
    }
}
