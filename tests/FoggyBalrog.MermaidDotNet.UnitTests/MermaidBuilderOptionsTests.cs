using System.Reflection;
using FoggyBalrog.MermaidDotNet.KanbanDiagram;

namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class MermaidBuilderOptionsTests
{
    private static readonly IReadOnlyDictionary<string, Func<MermaidDotNetOptions?, object>> _builders =
        new Dictionary<string, Func<MermaidDotNetOptions?, object>>
        {
            [nameof(Mermaid.BlockDiagram)] = options => Mermaid.BlockDiagram(options: options),
            [nameof(Mermaid.ClassDiagram)] = options => Mermaid.ClassDiagram(options: options),
            [nameof(Mermaid.EntityRelationshipDiagram)] = options => Mermaid.EntityRelationshipDiagram(options: options),
            [nameof(Mermaid.Flowchart)] = options => Mermaid.Flowchart(options: options),
            [nameof(Mermaid.GanttDiagram)] = options => Mermaid.GanttDiagram(options: options),
            [nameof(Mermaid.GitGraph)] = options => Mermaid.GitGraph(options: options),
            [nameof(Mermaid.KanbanDiagram)] = options => Mermaid.KanbanDiagram(options: options),
            [nameof(Mermaid.MindMap)] = options => Mermaid.MindMap(rootText: "Root", options: options),
            [nameof(Mermaid.PacketDiagram)] = options => Mermaid.PacketDiagram(options: options),
            [nameof(Mermaid.PieChart)] = options => Mermaid.PieChart(options: options),
            [nameof(Mermaid.QuadrantChart)] = options => Mermaid.QuadrantChart(options: options),
            [nameof(Mermaid.RequirementDiagram)] = options => Mermaid.RequirementDiagram(options: options),
            [nameof(Mermaid.SankeyDiagram)] = options => Mermaid.SankeyDiagram(options: options),
            [nameof(Mermaid.SequenceDiagram)] = options => Mermaid.SequenceDiagram(options: options),
            [nameof(Mermaid.StateDiagram)] = options => Mermaid.StateDiagram(options: options),
            [nameof(Mermaid.TimelineDiagram)] = options => Mermaid.TimelineDiagram(options: options),
            [nameof(Mermaid.UserJourneyDiagram)] = options => Mermaid.UserJourneyDiagram(options: options),
            [nameof(Mermaid.XYChart)] = options => Mermaid.XYChart(options: options),
            [nameof(KanbanDiagramColumnBuilder)] = options => new KanbanDiagramColumnBuilder("Column", options: options)
        };

    public static IEnumerable<object[]> BuilderNames => _builders.Keys.Select(name => new object[] { name });

    [Fact]
    public void BuilderDelegates_CoverEveryMermaidFactory()
    {
        string[] factoryNames = typeof(Mermaid)
            .GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(method => method.Name)
            .OrderBy(name => name)
            .ToArray();
        string[] coveredNames = _builders.Keys
            .Where(name => name != nameof(KanbanDiagramColumnBuilder))
            .OrderBy(name => name)
            .ToArray();

        Assert.Equal(factoryNames, coveredNames);
    }

    [Theory]
    [MemberData(nameof(BuilderNames))]
    public void Constructor_WithNullOptions_UsesIndependentDefaults(string builderName)
    {
        MermaidDotNetOptions first = GetOptions(_builders[builderName](null));
        MermaidDotNetOptions second = GetOptions(_builders[builderName](null));

        Assert.NotSame(first, second);
        AssertOptions(first, true, false, MermaidVersion.V11_4, MermaidCompatibilityMode.Strict);
        AssertOptions(second, true, false, MermaidVersion.V11_4, MermaidCompatibilityMode.Strict);
    }

    [Theory]
    [MemberData(nameof(BuilderNames))]
    public void Constructor_InStrictMode_AllowsFutureTargetsRegardlessOfInputValidation(string builderName)
    {
        MermaidVersion[] targets = [new(11, 13, 1), new(11, 14), new(12, 0)];

        foreach (bool validateInputs in new[] { false, true })
        {
            foreach (MermaidVersion target in targets)
            {
                var options = new MermaidDotNetOptions
                {
                    ValidateInputs = validateInputs,
                    TargetMermaidVersion = target,
                    CompatibilityMode = MermaidCompatibilityMode.Strict
                };

                object builder = _builders[builderName](options);

                AssertOptions(GetOptions(builder), validateInputs, false, target, MermaidCompatibilityMode.Strict);
            }
        }
    }

    [Theory]
    [MemberData(nameof(BuilderNames))]
    public void Constructor_InUncheckedMode_AllowsFutureTargets(string builderName)
    {
        MermaidVersion[] targets = [new(11, 13, 1), new(11, 14), new(12, 0)];

        foreach (bool validateInputs in new[] { false, true })
        {
            foreach (MermaidVersion target in targets)
            {
                var options = new MermaidDotNetOptions
                {
                    ValidateInputs = validateInputs,
                    TargetMermaidVersion = target,
                    CompatibilityMode = MermaidCompatibilityMode.Unchecked
                };

                object builder = _builders[builderName](options);

                AssertOptions(GetOptions(builder), validateInputs, false, target, MermaidCompatibilityMode.Unchecked);
            }
        }
    }

    [Theory]
    [MemberData(nameof(BuilderNames))]
    public void Constructor_RejectsTargetsBelowElevenInBothModesRegardlessOfInputValidation(string builderName)
    {
        MermaidVersion[] targets = [new(0, 0), new(10, 99, 99)];
        MermaidCompatibilityMode[] modes = [MermaidCompatibilityMode.Strict, MermaidCompatibilityMode.Unchecked];

        foreach (MermaidCompatibilityMode mode in modes)
        {
            foreach (bool validateInputs in new[] { false, true })
            {
                foreach (MermaidVersion target in targets)
                {
                    var options = new MermaidDotNetOptions
                    {
                        ValidateInputs = validateInputs,
                        TargetMermaidVersion = target,
                        CompatibilityMode = mode
                    };

                    MermaidException exception = Assert.Throws<MermaidException>(() => _builders[builderName](options));

                    AssertIncompatibleVersion(exception, target);
                }
            }
        }
    }

    [Theory]
    [MemberData(nameof(BuilderNames))]
    public void Constructor_AcceptsInclusiveSupportedBoundariesInBothModes(string builderName)
    {
        MermaidVersion[] targets = [MermaidVersion.V11_0, MermaidVersion.V11_4, MermaidVersion.V11_13];
        MermaidCompatibilityMode[] modes = [MermaidCompatibilityMode.Strict, MermaidCompatibilityMode.Unchecked];

        foreach (MermaidCompatibilityMode mode in modes)
        {
            foreach (MermaidVersion target in targets)
            {
                var options = new MermaidDotNetOptions
                {
                    TargetMermaidVersion = target,
                    CompatibilityMode = mode
                };

                object builder = _builders[builderName](options);

                AssertOptions(GetOptions(builder), true, false, target, mode);
            }
        }
    }

    [Theory]
    [MemberData(nameof(BuilderNames))]
    public void Constructor_SnapshotsAllFourOptionsAndAllowsCallerReuse(string builderName)
    {
        foreach (bool useDefaults in new[] { false, true })
        {
            MermaidVersion target = useDefaults ? MermaidVersion.V11_4 : new MermaidVersion(12, 0);
            MermaidCompatibilityMode mode = useDefaults ? MermaidCompatibilityMode.Strict : MermaidCompatibilityMode.Unchecked;
            var options = new MermaidDotNetOptions
            {
                ValidateInputs = useDefaults,
                SanitizeInputs = !useDefaults,
                TargetMermaidVersion = target,
                CompatibilityMode = mode
            };
            object firstBuilder = _builders[builderName](options);

            options.ValidateInputs = !useDefaults;
            options.SanitizeInputs = useDefaults;
            options.TargetMermaidVersion = MermaidVersion.V11_0;
            options.CompatibilityMode = useDefaults ? MermaidCompatibilityMode.Unchecked : MermaidCompatibilityMode.Strict;
            object secondBuilder = _builders[builderName](options);

            MermaidDotNetOptions first = GetOptions(firstBuilder);
            MermaidDotNetOptions second = GetOptions(secondBuilder);
            Assert.NotSame(options, first);
            Assert.NotSame(options, second);
            Assert.NotSame(first, second);
            AssertOptions(first, useDefaults, !useDefaults, target, mode);
            AssertOptions(second, !useDefaults, useDefaults, MermaidVersion.V11_0, options.CompatibilityMode);
        }
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void PieChart_RetainsInputValidationAfterCallerMutation(bool validateInputs)
    {
        var options = new MermaidDotNetOptions { ValidateInputs = validateInputs };
        var builder = Mermaid.PieChart(options: options);
        options.ValidateInputs = !validateInputs;

        if (validateInputs)
        {
            MermaidException exception = Assert.Throws<MermaidException>(() => builder.AddDataSet("Slice", -1));
            Assert.Equal(MermaidExceptionReason.StrictlyNegative, exception.Reason);
        }
        else
        {
            builder.AddDataSet("Slice", -1);
            Assert.Contains("\"Slice\" : -1", builder.Build());
        }
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void PieChart_RetainsSanitizationAndUncheckedFutureTargetAfterCallerMutation(bool sanitizeInputs)
    {
        var options = new MermaidDotNetOptions
        {
            ValidateInputs = false,
            SanitizeInputs = sanitizeInputs,
            TargetMermaidVersion = new MermaidVersion(12, 0),
            CompatibilityMode = MermaidCompatibilityMode.Unchecked
        };
        var builder = Mermaid.PieChart(options: options);
        options.ValidateInputs = true;
        options.SanitizeInputs = !sanitizeInputs;
        options.TargetMermaidVersion = new MermaidVersion(10, 0);
        options.CompatibilityMode = MermaidCompatibilityMode.Strict;

        string diagram = builder.AddDataSet("A\"B", 1).Build();

        Assert.Contains(sanitizeInputs ? "A#34;B" : "A\"B", diagram);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void KanbanColumn_SnapshotsParentOptionsAndIgnoresCallerMutation(bool validateInputs)
    {
        var options = new MermaidDotNetOptions
        {
            ValidateInputs = validateInputs,
            SanitizeInputs = true,
            TargetMermaidVersion = new MermaidVersion(12, 0),
            CompatibilityMode = MermaidCompatibilityMode.Unchecked
        };
        var builder = Mermaid.KanbanDiagram(options: options);
        options.ValidateInputs = !validateInputs;
        options.SanitizeInputs = false;
        options.TargetMermaidVersion = new MermaidVersion(10, 0);
        options.CompatibilityMode = MermaidCompatibilityMode.Strict;

        builder.AddColumn("Column", column =>
        {
            MermaidDotNetOptions snapshot = GetOptions(column);
            Assert.NotSame(GetOptions(builder), snapshot);
            AssertOptions(snapshot, validateInputs, true, new MermaidVersion(12, 0), MermaidCompatibilityMode.Unchecked);
            column.AddTask("task[1]");

            if (validateInputs)
            {
                MermaidException exception = Assert.Throws<MermaidException>(() => column.AddTask(" "));
                Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
            }
            else
            {
                column.AddTask(" ");
            }
        });

        Assert.Contains("task00[task#91;1#93;]", builder.Build());
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void CompositeBlock_SnapshotsParentOptionsAndIgnoresCallerMutation(bool validateInputs)
    {
        var options = new MermaidDotNetOptions
        {
            ValidateInputs = validateInputs,
            SanitizeInputs = true,
            TargetMermaidVersion = new MermaidVersion(12, 0),
            CompatibilityMode = MermaidCompatibilityMode.Unchecked
        };
        var builder = Mermaid.BlockDiagram(options: options);
        options.ValidateInputs = !validateInputs;
        options.SanitizeInputs = false;
        options.TargetMermaidVersion = new MermaidVersion(10, 0);
        options.CompatibilityMode = MermaidCompatibilityMode.Strict;

        builder.AddCompositeBlock(composite =>
        {
            MermaidDotNetOptions snapshot = GetOptions(composite);
            Assert.NotSame(GetOptions(builder), snapshot);
            AssertOptions(snapshot, validateInputs, true, new MermaidVersion(12, 0), MermaidCompatibilityMode.Unchecked);
            composite.AddBlock("A\"B", out _);

            if (validateInputs)
            {
                MermaidException exception = Assert.Throws<MermaidException>(() => composite.AddBlock("Invalid", out _, width: -1));
                Assert.Equal(MermaidExceptionReason.StrictlyNegative, exception.Reason);
            }
            else
            {
                composite.AddBlock("Unchecked", out _, width: -1);
            }
        });

        Assert.Contains("A#34;B", builder.Build());
    }

    private static MermaidDotNetOptions GetOptions(object builder)
    {
        FieldInfo? field = builder.GetType().GetField("_options", BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.NotNull(field);
        Assert.True(field.IsInitOnly);
        return Assert.IsType<MermaidDotNetOptions>(field.GetValue(builder));
    }

    private static void AssertOptions(
        MermaidDotNetOptions options,
        bool validateInputs,
        bool sanitizeInputs,
        MermaidVersion target,
        MermaidCompatibilityMode mode)
    {
        Assert.Equal(validateInputs, options.ValidateInputs);
        Assert.Equal(sanitizeInputs, options.SanitizeInputs);
        Assert.Equal(target, options.TargetMermaidVersion);
        Assert.Equal(mode, options.CompatibilityMode);
    }

    private static void AssertIncompatibleVersion(MermaidException exception, MermaidVersion target)
    {
        Assert.Equal(MermaidExceptionReason.IncompatibleVersion, exception.Reason);
        string range = $">= {MermaidCompatibility.MinimumSupportedVersion}";

        Assert.Contains(
            $"Feature 'MermaidDotNet support' is not compatible with target Mermaid {target}. Required version range: {range}.",
            exception.Message);
    }
}
