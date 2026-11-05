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
    [InlineData("11.6.99", false)]
    [InlineData("11.7", true)]
    [InlineData("11.7.1", true)]
    public void RequireVersion_UsesInclusiveIntroductionBoundary(string target, bool compatible)
    {
        var options = new MermaidDotNetOptions { TargetMermaidVersion = MermaidVersion.Parse(target) };

        if (compatible)
        {
            MermaidCompatibility.RequireVersion(options, "Test feature", MermaidVersion.V11_7);
        }
        else
        {
            MermaidException exception = Assert.Throws<MermaidException>(() =>
                MermaidCompatibility.RequireVersion(options, "Test feature", MermaidVersion.V11_7));

            Assert.Equal(MermaidExceptionReason.IncompatibleVersion, exception.Reason);
            Assert.Equal(
                $"Feature 'Test feature' is not compatible with target Mermaid {target}. Required version range: >= 11.7.",
                exception.Message);
        }
    }

    [Theory]
    [InlineData("11.9.99", true)]
    [InlineData("11.10", false)]
    [InlineData("11.10.1", false)]
    public void RequireVersion_UsesExclusiveRemovalBoundary(string target, bool compatible)
    {
        var options = new MermaidDotNetOptions { TargetMermaidVersion = MermaidVersion.Parse(target) };

        if (compatible)
        {
            MermaidCompatibility.RequireVersion(options, "Test feature", MermaidVersion.V11_0, MermaidVersion.V11_10);
        }
        else
        {
            MermaidException exception = Assert.Throws<MermaidException>(() =>
                MermaidCompatibility.RequireVersion(options, "Test feature", MermaidVersion.V11_0, MermaidVersion.V11_10));

            Assert.Equal(MermaidExceptionReason.IncompatibleVersion, exception.Reason);
            Assert.Equal(
                $"Feature 'Test feature' is not compatible with target Mermaid {target}. Required version range: >= 11.0 and < 11.10.",
                exception.Message);
        }
    }

    [Theory]
    [InlineData("11.4")]
    [InlineData("11.10")]
    [InlineData("12.0")]
    public void RequireVersion_InUncheckedMode_IgnoresIntroductionAndRemoval(string target)
    {
        var options = new MermaidDotNetOptions
        {
            TargetMermaidVersion = MermaidVersion.Parse(target),
            CompatibilityMode = MermaidCompatibilityMode.Unchecked
        };

        MermaidCompatibility.RequireVersion(options, "Test feature", MermaidVersion.V11_7, MermaidVersion.V11_10);
    }

    [Fact]
    public void RequireVersion_InStrictMode_StillChecksWhenInputValidationIsDisabled()
    {
        var options = new MermaidDotNetOptions { ValidateInputs = false };

        MermaidException exception = Assert.Throws<MermaidException>(() =>
            MermaidCompatibility.RequireVersion(options, "Test feature", MermaidVersion.V11_7));

        Assert.Equal(MermaidExceptionReason.IncompatibleVersion, exception.Reason);
        Assert.Equal(
            "Feature 'Test feature' is not compatible with target Mermaid 11.4. Required version range: >= 11.7.",
            exception.Message);
    }

    [Fact]
    public void UnsupportedTarget_ExceptionIncludesRequiredRange()
    {
        var options = new MermaidDotNetOptions { TargetMermaidVersion = MermaidVersion.V12_0 };

        MermaidException exception = Assert.Throws<MermaidException>(() => Mermaid.Flowchart(options: options));

        Assert.Equal(MermaidExceptionReason.IncompatibleVersion, exception.Reason);
        Assert.Equal(
            "Feature 'MermaidDotNet support' is not compatible with target Mermaid 12.0. Required version range: >= 11.0 and <= 11.13.",
            exception.Message);
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
        Assert.Equal(
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
