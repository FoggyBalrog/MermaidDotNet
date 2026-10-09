using FoggyBalrog.MermaidDotNet.EntityRelationshipDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class MermaidErCompatibilityTests
{
    public static TheoryData<string, bool, string> MultilineLabels => new()
    {
        { "places\norders", true, "places<br/>orders" },
        { "places\r\norders", true, "places<br/>orders" },
        { "places<br/>orders", true, "places<br/>orders" },
        { "places<br/>orders", false, "places<br/>orders" },
        { "places<br />orders", true, "places<br />orders" },
        { "places<br />orders", false, "places<br />orders" }
    };

    [Theory]
    [MemberData(nameof(MultilineLabels))]
    public void MultilineLabels_Strict11_0RejectsWithoutMutation(string label, bool sanitizeInputs, string expectedLabel)
    {
        foreach (bool validateInputs in new[] { false, true })
        {
            var options = new MermaidDotNetOptions
            {
                TargetMermaidVersion = MermaidVersion.V11_0,
                SanitizeInputs = sanitizeInputs,
                ValidateInputs = validateInputs
            };
            var builder = Mermaid.EntityRelationshipDiagram(options: options)
                .AddEntity("Customer", out var customer)
                .AddEntity("Order", out var order)
                .AddRelationship(Cardinality.ExactlyOne, customer, Cardinality.ZeroOrMore, order, "before");
            string before = builder.Build();

            var exception = Assert.Throws<MermaidException>(() => builder.AddRelationship(
                Cardinality.ExactlyOne, customer, Cardinality.ZeroOrMore, order, label));

            Assert.DoesNotContain(expectedLabel, builder.Build());
            Assert.Equal(MermaidExceptionReason.IncompatibleVersion, exception.Reason);
            Assert.Contains("ER multiline relationship labels", exception.Message);
            Assert.Contains("Required version range: >= 11.1", exception.Message);
            Assert.Equal(before, builder.Build());

            builder.AddRelationship(Cardinality.ExactlyOne, customer, Cardinality.ZeroOrMore, order, "after");
            string expected = Mermaid.EntityRelationshipDiagram(options: options)
                .AddEntity("Customer", out var expectedCustomer)
                .AddEntity("Order", out var expectedOrder)
                .AddRelationship(Cardinality.ExactlyOne, expectedCustomer, Cardinality.ZeroOrMore, expectedOrder, "before")
                .AddRelationship(Cardinality.ExactlyOne, expectedCustomer, Cardinality.ZeroOrMore, expectedOrder, "after")
                .Build();
            Assert.Equal(expected, builder.Build());
        }
    }

    [Theory]
    [MemberData(nameof(MultilineLabels))]
    public void MultilineLabels_ExactAndHigherVersionsAndUncheckedAllow(string label, bool sanitizeInputs, string expectedLabel)
    {
        foreach (bool validateInputs in new[] { false, true })
        {
            foreach (var (target, mode) in new[]
            {
                (MermaidVersion.V11_1, MermaidCompatibilityMode.Strict),
                (new MermaidVersion(11, 1, 1), MermaidCompatibilityMode.Strict),
                (new MermaidVersion(12, 0), MermaidCompatibilityMode.Strict),
                (MermaidVersion.V11_0, MermaidCompatibilityMode.Unchecked)
            })
            {
                var options = new MermaidDotNetOptions
                {
                    TargetMermaidVersion = target,
                    CompatibilityMode = mode,
                    SanitizeInputs = sanitizeInputs,
                    ValidateInputs = validateInputs
                };
                string diagram = Mermaid.EntityRelationshipDiagram(options: options)
                    .AddEntity("Customer", out var customer)
                    .AddEntity("Order", out var order)
                    .AddRelationship(Cardinality.ExactlyOne, customer, Cardinality.ZeroOrMore, order, label)
                    .Build();

                Assert.Contains($"Customer ||--o{{ Order : \"{expectedLabel}\"", diagram);
            }
        }
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public void OrdinaryLabels_StrictBaselineAllows(bool validateInputs, bool sanitizeInputs)
    {
        string diagram = Mermaid.EntityRelationshipDiagram(options: new()
            {
                TargetMermaidVersion = MermaidVersion.V11_0,
                ValidateInputs = validateInputs,
                SanitizeInputs = sanitizeInputs
            })
            .AddEntity("Customer", out var customer)
            .AddEntity("Order", out var order)
            .AddRelationship(Cardinality.ExactlyOne, customer, Cardinality.ZeroOrMore, order, "places orders")
            .Build();

        Assert.Contains("Customer ||--o{ Order : \"places orders\"", diagram);
    }

    [Theory]
    [InlineData(MermaidCompatibilityMode.Strict)]
    [InlineData(MermaidCompatibilityMode.Unchecked)]
    public void CompatibilityMode_DoesNotDisableInputValidation(MermaidCompatibilityMode mode)
    {
        var builder = Mermaid.EntityRelationshipDiagram(options: new()
            {
                TargetMermaidVersion = MermaidVersion.V11_0,
                CompatibilityMode = mode
            })
            .AddEntity("Customer", out var customer)
            .AddEntity("Order", out var order);
        Mermaid.EntityRelationshipDiagram().AddEntity("Foreign", out var foreign);
        string before = builder.Build();

        var whitespace = Assert.Throws<MermaidException>(() => builder.AddRelationship(
            Cardinality.ExactlyOne, customer, Cardinality.ZeroOrMore, order, " "));
        Assert.Equal(MermaidExceptionReason.WhiteSpace, whitespace.Reason);
        var foreignEntity = Assert.Throws<MermaidException>(() => builder.AddRelationship(
            Cardinality.ExactlyOne, foreign, Cardinality.ZeroOrMore, order, "places"));
        Assert.Equal(MermaidExceptionReason.ForeignItem, foreignEntity.Reason);
        Assert.Equal(before, builder.Build());
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void NullLabel_WithoutSanitizationRetainsExistingBehavior(bool validateInputs)
    {
        var builder = Mermaid.EntityRelationshipDiagram(options: new()
            {
                TargetMermaidVersion = MermaidVersion.V11_0,
                ValidateInputs = validateInputs
            })
            .AddEntity("Customer", out var customer)
            .AddEntity("Order", out var order);
        string before = builder.Build();
        Action add = () => builder.AddRelationship(
            Cardinality.ExactlyOne, customer, Cardinality.ZeroOrMore, order, null!);

        if (validateInputs)
        {
            Assert.Throws<NullReferenceException>(add);
            Assert.Equal(before, builder.Build());
        }
        else
        {
            add();
            Assert.Contains("Customer ||--o{ Order : \"\"", builder.Build());
        }
    }
}
