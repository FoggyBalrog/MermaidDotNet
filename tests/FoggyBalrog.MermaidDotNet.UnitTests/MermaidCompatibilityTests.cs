namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class MermaidCompatibilityTests
{
    [Fact]
    public void Options_UseExpectedDefaults()
    {
        var options = new MermaidDotNetOptions();

        Assert.True(options.ValidateInputs);
        Assert.False(options.SanitizeInputs);
        Assert.Equal(MermaidVersion.V11_4, options.TargetMermaidVersion);
        Assert.Equal(MermaidCompatibilityMode.Strict, options.CompatibilityMode);
    }

    [Fact]
    public void Options_WithCopy_PreservesValuesAndAllowsIndependentMutation()
    {
        var options = new MermaidDotNetOptions
        {
            ValidateInputs = false,
            SanitizeInputs = true,
            TargetMermaidVersion = MermaidVersion.V12_0,
            CompatibilityMode = MermaidCompatibilityMode.Unchecked
        };

        var copy = options with { };

        Assert.NotSame(options, copy);
        Assert.Equal(options, copy);

        copy.ValidateInputs = true;
        copy.SanitizeInputs = false;
        copy.TargetMermaidVersion = MermaidVersion.V11_4;
        copy.CompatibilityMode = MermaidCompatibilityMode.Strict;

        Assert.False(options.ValidateInputs);
        Assert.True(options.SanitizeInputs);
        Assert.Equal(MermaidVersion.V12_0, options.TargetMermaidVersion);
        Assert.Equal(MermaidCompatibilityMode.Unchecked, options.CompatibilityMode);
        Assert.NotEqual(options, copy);
    }

    [Fact]
    public void SupportRange_IsIndependentOfVersionPresets()
    {
        Assert.Equal(MermaidVersion.V11_0, MermaidCompatibility.MinimumSupportedVersion);
        Assert.Equal(MermaidVersion.V11_13, MermaidCompatibility.LatestTestedVersion);
        Assert.True(MermaidVersion.V11_17 > MermaidCompatibility.LatestTestedVersion);
        Assert.True(MermaidVersion.V12_0 > MermaidCompatibility.LatestTestedVersion);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Snapshot_RejectsUnknownCompatibilityModeRegardlessOfInputValidation(bool validateInputs)
    {
        var options = new MermaidDotNetOptions
        {
            ValidateInputs = validateInputs,
            CompatibilityMode = (MermaidCompatibilityMode)42
        };

        MermaidException exception = Assert.Throws<MermaidException>(() => MermaidDotNetOptions.Snapshot(options));

        Assert.Equal(MermaidExceptionReason.InvalidConfiguration, exception.Reason);
    }

    [Theory]
    [InlineData("11.13.1")]
    [InlineData("11.15")]
    [InlineData("12.0")]
    [InlineData("99.0")]
    public void StrictMode_AcceptsFutureTargets(string target)
    {
        var options = new MermaidDotNetOptions { TargetMermaidVersion = MermaidVersion.Parse(target) };

        Assert.Equal("flowchart TB", Mermaid.Flowchart(options: options).Build());
    }

    [Fact]
    public void IncompatibleVersion_ExceptionIncludesAllSpecifiedBounds()
    {
        MermaidException exception = MermaidException.IncompatibleVersion(
            "Test feature",
            MermaidVersion.V12_0,
            MermaidVersion.V11_7,
            maximumVersion: MermaidVersion.V11_13,
            removedInVersion: MermaidVersion.V11_10);

        Assert.Equal(MermaidExceptionReason.IncompatibleVersion, exception.Reason);
        Assert.Contains(
            "Feature 'Test feature' is not compatible with target Mermaid 12.0. Required version range: >= 11.7 and <= 11.13 and < 11.10.",
            exception.Message);
    }

    [Fact]
    public void UncheckedMode_DoesNotDisableOrdinaryValidation()
    {
        var options = new MermaidDotNetOptions
        {
            TargetMermaidVersion = MermaidVersion.V12_0,
            CompatibilityMode = MermaidCompatibilityMode.Unchecked
        };

        MermaidException exception = Assert.Throws<MermaidException>(() =>
            Mermaid.PieChart(options: options).AddDataSet("Slice", -1));

        Assert.Equal(MermaidExceptionReason.StrictlyNegative, exception.Reason);
        Assert.Contains("must be greater than or equal to zero", exception.Message);
    }

    [Fact]
    public void UncheckedMode_DoesNotDisableSanitizationBeforeValidation()
    {
        var options = new MermaidDotNetOptions
        {
            SanitizeInputs = true,
            TargetMermaidVersion = MermaidVersion.V12_0,
            CompatibilityMode = MermaidCompatibilityMode.Unchecked
        };

        string diagram = Mermaid.PieChart(options: options).AddDataSet("A\"B", 1).Build();

        Assert.Contains("A#34;B", diagram);
    }
}
