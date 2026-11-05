using System.Globalization;

namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class MermaidVersionTests
{
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(1, 2, 3)]
    [InlineData(10, 9, 8)]
    [InlineData(11, 17, 1)]
    [InlineData(12, 0, 0)]
    [InlineData(int.MaxValue, int.MaxValue, int.MaxValue)]
    public void Constructor_PreservesNonnegativeComponents(int major, int minor, int patch)
    {
        var version = new MermaidVersion(major, minor, patch);

        Assert.Equal(major, version.Major);
        Assert.Equal(minor, version.Minor);
        Assert.Equal(patch, version.Patch);
    }

    [Fact]
    public void Constructor_DefaultsPatchToZero()
    {
        var version = new MermaidVersion(11, 17);

        Assert.Equal(11, version.Major);
        Assert.Equal(17, version.Minor);
        Assert.Equal(0, version.Patch);
        Assert.Equal(new MermaidVersion(11, 17, 0), version);
    }

    [Theory]
    [InlineData(-1, 0, 0, "major")]
    [InlineData(0, -1, 0, "minor")]
    [InlineData(0, 0, -1, "patch")]
    [InlineData(int.MinValue, 0, 0, "major")]
    [InlineData(0, int.MinValue, 0, "minor")]
    [InlineData(0, 0, int.MinValue, "patch")]
    public void Constructor_RejectsNegativeComponents(int major, int minor, int patch, string parameterName)
    {
        Assert.Throws<ArgumentOutOfRangeException>(parameterName, () => new MermaidVersion(major, minor, patch));
    }

    [Fact]
    public void Default_IsZeroVersion()
    {
        MermaidVersion version = default;

        var zero = new MermaidVersion(0, 0, 0);
        var parsedZero = MermaidVersion.Parse("00.00.00");

        Assert.Equal(0, version.Major);
        Assert.Equal(0, version.Minor);
        Assert.Equal(0, version.Patch);
        Assert.Equal(zero, version);
        Assert.Equal(new MermaidVersion(), version);
        Assert.Equal(parsedZero, version);
        Assert.True(version.Equals((object)zero));
        Assert.True(version == zero);
        Assert.False(version != parsedZero);
        Assert.Equal(zero.GetHashCode(), version.GetHashCode());
        Assert.Equal(parsedZero.GetHashCode(), version.GetHashCode());
        Assert.Equal(0, version.CompareTo(zero));
        Assert.Equal(0, zero.CompareTo(version));
        Assert.True(version <= zero);
        Assert.True(version >= zero);
        Assert.True(version < new MermaidVersion(0, 0, 1));
        Assert.Single(new HashSet<MermaidVersion> { version, zero, parsedZero });
        Assert.Equal("0.0", version.ToString());
    }

    [Theory]
    [InlineData(nameof(MermaidVersion.Major))]
    [InlineData(nameof(MermaidVersion.Minor))]
    [InlineData(nameof(MermaidVersion.Patch))]
    public void Components_AreGetOnly(string propertyName)
    {
        var property = typeof(MermaidVersion).GetProperty(propertyName);

        Assert.NotNull(property);
        Assert.Equal(typeof(int), property.PropertyType);
        Assert.NotNull(property.GetMethod);
        Assert.Null(property.SetMethod);
    }

    [Theory]
    [InlineData(11, 0, 0, 11, 0, 0, 0)]
    [InlineData(11, 17, 2, 11, 17, 2, 0)]
    [InlineData(0, 0, 0, 0, 0, 0, 0)]
    [InlineData(10, 99, 99, 11, 0, 0, -1)]
    [InlineData(12, 0, 0, 11, 17, 99, 1)]
    [InlineData(11, 9, 99, 11, 10, 0, -1)]
    [InlineData(11, 10, 0, 11, 9, 99, 1)]
    [InlineData(11, 17, 1, 11, 17, 2, -1)]
    [InlineData(11, 17, 2, 11, 17, 1, 1)]
    [InlineData(0, int.MaxValue, int.MaxValue, int.MaxValue, 0, 0, -1)]
    [InlineData(int.MaxValue, 0, 0, 0, int.MaxValue, int.MaxValue, 1)]
    [InlineData(11, 0, int.MaxValue, 11, int.MaxValue, 0, -1)]
    [InlineData(11, int.MaxValue, 0, 11, 0, int.MaxValue, 1)]
    [InlineData(11, 17, 0, 11, 17, int.MaxValue, -1)]
    [InlineData(11, 17, int.MaxValue, 11, 17, 0, 1)]
    public void Comparison_IsLexicographic(int leftMajor, int leftMinor, int leftPatch, int rightMajor, int rightMinor, int rightPatch, int expectedSign)
    {
        var left = new MermaidVersion(leftMajor, leftMinor, leftPatch);
        var right = new MermaidVersion(rightMajor, rightMinor, rightPatch);

        Assert.Equal(expectedSign, Math.Sign(left.CompareTo(right)));
        Assert.Equal(-expectedSign, Math.Sign(right.CompareTo(left)));
        Assert.Equal(expectedSign, Math.Sign(((IComparable<MermaidVersion>)left).CompareTo(right)));
        Assert.Equal(expectedSign, Math.Sign(((IComparable)left).CompareTo(right)));
        Assert.Equal(expectedSign < 0, left < right);
        Assert.Equal(expectedSign <= 0, left <= right);
        Assert.Equal(expectedSign > 0, left > right);
        Assert.Equal(expectedSign >= 0, left >= right);
        Assert.Equal(expectedSign == 0, left == right);
        Assert.Equal(expectedSign != 0, left != right);
    }

    [Fact]
    public void CompareTo_NullObject_ReturnsPositive()
    {
        IComparable version = default(MermaidVersion);

        Assert.True(version.CompareTo(null) > 0);
    }

    [Theory]
    [InlineData("11.0")]
    [InlineData(11)]
    public void CompareTo_OtherType_ThrowsArgumentException(object other)
    {
        IComparable version = MermaidVersion.V11_0;

        Assert.Throws<ArgumentException>("obj", () => version.CompareTo(other));
    }

    [Fact]
    public void Versions_CanBeSortedUsingDefaultComparer()
    {
        MermaidVersion[] versions = [new(12, 0), new(11, 10), new(11, 9, 1), new(10, 9), new(11, 9)];

        Array.Sort(versions);

        Assert.Equal(new MermaidVersion[] { new(10, 9), new(11, 9), new(11, 9, 1), new(11, 10), new(12, 0) }, versions);
    }

    [Fact]
    public void Equality_UsesAllComponentsAndProducesEqualHashes()
    {
        var version = new MermaidVersion(11, 17, 2);
        var equal = new MermaidVersion(11, 17, 2);

        Assert.True(version.Equals(equal));
        Assert.True(version.Equals((object)equal));
        Assert.True(((IEquatable<MermaidVersion>)version).Equals(equal));
        Assert.Equal(version.GetHashCode(), equal.GetHashCode());
        Assert.False(version.Equals(new MermaidVersion(12, 17, 2)));
        Assert.False(version.Equals(new MermaidVersion(11, 16, 2)));
        Assert.False(version.Equals(new MermaidVersion(11, 17, 3)));
        Assert.False(version.Equals(null));
        Assert.False(version.Equals("11.17.2"));

        var versions = new HashSet<MermaidVersion> { version, equal, new(11, 17, 3) };
        Assert.Equal(2, versions.Count);
        Assert.Contains(equal, versions);
    }

    [Theory]
    [InlineData("0.0", 0, 0, 0, "0.0")]
    [InlineData("0.0.0", 0, 0, 0, "0.0")]
    [InlineData("1.2.3", 1, 2, 3, "1.2.3")]
    [InlineData("10.9", 10, 9, 0, "10.9")]
    [InlineData("11.17", 11, 17, 0, "11.17")]
    [InlineData("11.17.0", 11, 17, 0, "11.17")]
    [InlineData("11.17.2", 11, 17, 2, "11.17.2")]
    [InlineData("12.0", 12, 0, 0, "12.0")]
    [InlineData("0011.0017.0002", 11, 17, 2, "11.17.2")]
    [InlineData("0011.0017", 11, 17, 0, "11.17")]
    [InlineData("2147483647.2147483647", int.MaxValue, int.MaxValue, 0, "2147483647.2147483647")]
    [InlineData("2147483647.2147483647.2147483647", int.MaxValue, int.MaxValue, int.MaxValue, "2147483647.2147483647.2147483647")]
    public void Parsing_AcceptsNumericComponentsAndFormattingIsCanonical(string input, int major, int minor, int patch, string formatted)
    {
        var expected = new MermaidVersion(major, minor, patch);

        Assert.Equal(expected, MermaidVersion.Parse(input));
        Assert.True(MermaidVersion.TryParse(input, out var result));
        Assert.Equal(expected, result);
        Assert.Equal(formatted, result.ToString());
        Assert.Equal(result, MermaidVersion.Parse(result.ToString()));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("11")]
    [InlineData(".")]
    [InlineData("11.")]
    [InlineData(".17")]
    [InlineData("11..1")]
    [InlineData("11.17.")]
    [InlineData(".11.17")]
    [InlineData("11.17.1.0")]
    [InlineData("11.17..1")]
    [InlineData("+11.17")]
    [InlineData("11.+17")]
    [InlineData("11.17.+1")]
    [InlineData("-11.17")]
    [InlineData("11.-17")]
    [InlineData("11.17.-1")]
    [InlineData("-0.0")]
    [InlineData(" 11.17")]
    [InlineData("11.17 ")]
    [InlineData("11. 17")]
    [InlineData("11 .17")]
    [InlineData("11.17. 1")]
    [InlineData("\t11.17")]
    [InlineData("11.17\n")]
    [InlineData("11.17\r\n")]
    [InlineData("11.\t17")]
    [InlineData("\u00a011.17")]
    [InlineData("11.17\u00a0")]
    [InlineData("11.17.1-beta")]
    [InlineData("11.17-beta")]
    [InlineData("11.17.1+build")]
    [InlineData("11.17+build")]
    [InlineData("11.17.1-beta+build")]
    [InlineData("11.17.0-0")]
    [InlineData("11.17.0+0")]
    [InlineData("v11.17")]
    [InlineData("11.17a")]
    [InlineData("11.17.1x")]
    [InlineData("11,17")]
    [InlineData("1,000.17")]
    [InlineData("1e1.17")]
    [InlineData("0xB.17")]
    [InlineData("١١.١٧")]
    [InlineData("１１.１７")]
    [InlineData("11.17\0")]
    [InlineData("11\0.17")]
    [InlineData("11.17.1\0")]
    [InlineData("2147483648.0")]
    [InlineData("11.2147483648")]
    [InlineData("11.17.2147483648")]
    [InlineData("999999999999999999999999999999.0")]
    [InlineData("11.999999999999999999999999999999")]
    [InlineData("11.17.999999999999999999999999999999")]
    public void Parsing_RejectsMalformedOrOverflowingInput(string input)
    {
        Assert.Throws<FormatException>(() => MermaidVersion.Parse(input));

        var result = MermaidVersion.V12_0;
        Assert.False(MermaidVersion.TryParse(input, out result));
        Assert.Equal(default(MermaidVersion), result);
    }

    [Fact]
    public void TryParse_HandlesNull()
    {
        var result = MermaidVersion.V12_0;
        Assert.False(MermaidVersion.TryParse(null, out result));
        Assert.Equal(default(MermaidVersion), result);
    }

    [Theory]
    [InlineData(1024, true)]
    [InlineData(1025, false)]
    public void Parsing_UsesSemverLengthLimit(int length, bool expectedSuccess)
    {
        string input = "1.2".PadLeft(length, '0');

        Assert.Equal(expectedSuccess, MermaidVersion.TryParse(input, out var result));

        if (expectedSuccess)
        {
            Assert.Equal(new MermaidVersion(1, 2), result);
            Assert.Equal(result, MermaidVersion.Parse(input));
        }
        else
        {
            Assert.Equal(default(MermaidVersion), result);
            Assert.Throws<FormatException>(() => MermaidVersion.Parse(input));
        }
    }

    [Theory]
    [InlineData("en-US")]
    [InlineData("fr-FR")]
    [InlineData("ar-SA")]
    [InlineData("tr-TR")]
    public void ParsingAndFormatting_AreCultureIndependent(string cultureName)
    {
        CultureInfo originalCulture = CultureInfo.CurrentCulture;
        CultureInfo originalUICulture = CultureInfo.CurrentUICulture;

        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(cultureName);

            Assert.Equal("1234.5678", new MermaidVersion(1234, 5678).ToString());
            Assert.Equal("1234.5678.9012", new MermaidVersion(1234, 5678, 9012).ToString());
            Assert.Equal(new MermaidVersion(1234, 5678), MermaidVersion.Parse("1234.5678"));
            Assert.Equal(new MermaidVersion(1234, 5678, 9012), MermaidVersion.Parse("1234.5678.9012"));
            Assert.True(MermaidVersion.TryParse("1234.5678.9012", out var parsed));
            Assert.Equal(new MermaidVersion(1234, 5678, 9012), parsed);
            Assert.False(MermaidVersion.TryParse("1234,5678", out _));
            Assert.False(MermaidVersion.TryParse("١١.١٧", out _));
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
            CultureInfo.CurrentUICulture = originalUICulture;
        }
    }

    [Theory]
    [InlineData(nameof(MermaidVersion.V11_0), 11, 0)]
    [InlineData(nameof(MermaidVersion.V11_1), 11, 1)]
    [InlineData(nameof(MermaidVersion.V11_2), 11, 2)]
    [InlineData(nameof(MermaidVersion.V11_3), 11, 3)]
    [InlineData(nameof(MermaidVersion.V11_4), 11, 4)]
    [InlineData(nameof(MermaidVersion.V11_5), 11, 5)]
    [InlineData(nameof(MermaidVersion.V11_6), 11, 6)]
    [InlineData(nameof(MermaidVersion.V11_7), 11, 7)]
    [InlineData(nameof(MermaidVersion.V11_8), 11, 8)]
    [InlineData(nameof(MermaidVersion.V11_9), 11, 9)]
    [InlineData(nameof(MermaidVersion.V11_10), 11, 10)]
    [InlineData(nameof(MermaidVersion.V11_11), 11, 11)]
    [InlineData(nameof(MermaidVersion.V11_12), 11, 12)]
    [InlineData(nameof(MermaidVersion.V11_13), 11, 13)]
    [InlineData(nameof(MermaidVersion.V11_14), 11, 14)]
    [InlineData(nameof(MermaidVersion.V11_15), 11, 15)]
    [InlineData(nameof(MermaidVersion.V11_16), 11, 16)]
    [InlineData(nameof(MermaidVersion.V11_17), 11, 17)]
    [InlineData(nameof(MermaidVersion.V12_0), 12, 0)]
    public void StaticVersions_AreGetOnlyPropertiesWithExpectedValues(string propertyName, int major, int minor)
    {
        var property = typeof(MermaidVersion).GetProperty(propertyName);

        Assert.NotNull(property);
        Assert.NotNull(property.GetMethod);
        Assert.True(property.GetMethod.IsStatic);
        Assert.Null(property.SetMethod);
        var version = Assert.IsType<MermaidVersion>(property.GetValue(null));
        Assert.Equal(new MermaidVersion(major, minor, 0), version);
    }
}
