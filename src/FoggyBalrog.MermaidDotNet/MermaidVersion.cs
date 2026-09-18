using System.Globalization;
using Semver;

namespace FoggyBalrog.MermaidDotNet;

/// <summary>
/// Represents a Mermaid version independently of library support for that version.
/// </summary>
public readonly record struct MermaidVersion : IComparable<MermaidVersion>, IComparable
{
    private static readonly SemVersion _zero = new(0, 0, 0);
    private readonly SemVersion? _version;

    // A default struct has no backing instance, but still represents version 0.0.
    private SemVersion Version => _version ?? _zero;

    public int Major => (int)Version.Major;

    public int Minor => (int)Version.Minor;

    public int Patch => (int)Version.Patch;

    public MermaidVersion(int major, int minor, int patch = 0)
        : this(new SemVersion(major, minor, patch))
    {
    }

    private MermaidVersion(SemVersion version) => _version = version;

    public static MermaidVersion V11_0 { get; } = new(11, 0);

    public static MermaidVersion V11_1 { get; } = new(11, 1);

    public static MermaidVersion V11_2 { get; } = new(11, 2);

    public static MermaidVersion V11_3 { get; } = new(11, 3);

    public static MermaidVersion V11_4 { get; } = new(11, 4);

    public static MermaidVersion V11_5 { get; } = new(11, 5);

    public static MermaidVersion V11_6 { get; } = new(11, 6);

    public static MermaidVersion V11_7 { get; } = new(11, 7);

    public static MermaidVersion V11_8 { get; } = new(11, 8);

    public static MermaidVersion V11_9 { get; } = new(11, 9);

    public static MermaidVersion V11_10 { get; } = new(11, 10);

    public static MermaidVersion V11_11 { get; } = new(11, 11);

    public static MermaidVersion V11_12 { get; } = new(11, 12);

    public static MermaidVersion V11_13 { get; } = new(11, 13);

    public static MermaidVersion V11_14 { get; } = new(11, 14);

    public static MermaidVersion V11_15 { get; } = new(11, 15);

    public static MermaidVersion V11_16 { get; } = new(11, 16);

    public static MermaidVersion V11_17 { get; } = new(11, 17);

    public static MermaidVersion V12_0 { get; } = new(12, 0);

    public bool Equals(MermaidVersion other) => Version.Equals(other.Version);

    public override int GetHashCode() => Version.GetHashCode();

    public int CompareTo(MermaidVersion other) => Version.ComparePrecedenceTo(other.Version);

    public int CompareTo(object? obj)
    {
        if (obj is null)
        {
            return 1;
        }

        if (obj is MermaidVersion other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Object must be a MermaidVersion.", nameof(obj));
    }

    public static bool operator <(MermaidVersion left, MermaidVersion right) => left.CompareTo(right) < 0;

    public static bool operator <=(MermaidVersion left, MermaidVersion right) => left.CompareTo(right) <= 0;

    public static bool operator >(MermaidVersion left, MermaidVersion right) => left.CompareTo(right) > 0;

    public static bool operator >=(MermaidVersion left, MermaidVersion right) => left.CompareTo(right) >= 0;

    /// <summary>
    /// Parses numeric major.minor or major.minor.patch components without signs or whitespace,
    /// up to 1,024 characters.
    /// </summary>
    /// <exception cref="FormatException">The input is malformed, exceeds 1,024 characters, or a component exceeds <see cref="int.MaxValue"/>.</exception>
    public static MermaidVersion Parse(string value)
    {
        if (!TryParse(value, out MermaidVersion version))
        {
            throw new FormatException("Expected a numeric major.minor or major.minor.patch version of at most 1024 characters with components between 0 and Int32.MaxValue.");
        }

        return version;
    }

    public static bool TryParse(string? value, out MermaidVersion version)
    {
        version = default;

        if (!SemVersion.TryParse(value, SemVersionStyles.OptionalPatch | SemVersionStyles.AllowLeadingZeros, out var parsed)
            || parsed.IsPrerelease
            || parsed.Metadata.Length != 0
            || parsed.Major > int.MaxValue
            || parsed.Minor > int.MaxValue
            || parsed.Patch > int.MaxValue)
        {
            return false;
        }

        version = new MermaidVersion(parsed);
        return true;
    }

    public override string ToString()
    {
        string version = Major.ToString(CultureInfo.InvariantCulture) + "." + Minor.ToString(CultureInfo.InvariantCulture);
        return Patch == 0 ? version : version + "." + Patch.ToString(CultureInfo.InvariantCulture);
    }
}
