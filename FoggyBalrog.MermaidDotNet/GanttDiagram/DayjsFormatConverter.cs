using System.Globalization;

namespace FoggyBalrog.MermaidDotNet.GanttDiagram;

internal static class DayjsFormatConverter
{
    private static readonly (double factor, string suffix)[] _timespan_units =
    [
        (365d * 24 * 60 * 60 * 1000, "y"),
        (30d  * 24 * 60 * 60 * 1000, "M"),
        (7d   * 24 * 60 * 60 * 1000, "w"),
        (24d  * 60 * 60 * 1000,      "d"),
        (60d  * 60 * 1000,           "h"),
        (60d  * 1000,                "m"),
        (1000d,                      "s"),
        (1d,                         "ms")
    ];

    private const double _epsilon = 1e-9;

    public static string FormatDateTimeOffset(DateTimeOffset date, string dayjsFormat)
    {
        string format = dayjsFormat
            .Replace("YYYY", "yyyy")
            .Replace("YY", "yy")
            .Replace("Do", "dd")
            .Replace("DD", "dd")
            .Replace("D", "d")
            .Replace("SSS", "fff")
            .Replace("SS", "ff")
            .Replace("S", "f")
            .Replace("ZZ", "<no-colon-offset>")
            .Replace("Z", "zzz")
            .Replace("A", "tt")
            .Replace("a", "<lowercase-ampm>");

        // If format is a 1 character standard date format, add % to interpret it as a custom format.
        if (format.Length == 1 && "dfFghHKmMOoRrstyz:/".Contains(format))
        {
            format = "%" + format;
        }

        if (dayjsFormat.Contains("Do"))
        {
            int day = date.Day;
            string suffix = day switch
            {
                1 or 21 or 31 => "st",
                2 or 22 => "nd",
                3 or 23 => "rd",
                _ => "th"
            };
            format = format.Replace("dd", $"d'{suffix}'");
        }

        string unixSeconds = (date.ToUnixTimeMilliseconds() / 1000d).ToString(CultureInfo.InvariantCulture);

        format = format
            .Replace("X", $"'{unixSeconds}'")
            .Replace("x", $"'{date.ToUnixTimeMilliseconds()}'")
            .Replace("<no-colon-offset>", date.ToString("zzz", CultureInfo.InvariantCulture).Replace(":", ""))
            .Replace("<lowercase-ampm>", $"'{date.ToString("tt", CultureInfo.InvariantCulture).ToLowerInvariant()}'");

        return date.ToString(format, CultureInfo.InvariantCulture);
    }

    public static string FormatTimeSpan(TimeSpan span)
    {
        if (span == TimeSpan.Zero)
        {
            return "0ms";
        }

        double ms = Math.Abs(span.TotalMilliseconds);

        string sign = span < TimeSpan.Zero ? "-" : "";

        foreach (var (factor, suffix) in _timespan_units)
        {
            if (ms >= factor)
            {
                double value = ms / factor;
                string text = Math.Abs(value % 1) < _epsilon
                    ? ((long)value).ToString(CultureInfo.InvariantCulture)
                    : value.ToString("0.##", CultureInfo.InvariantCulture);
                return sign + text + suffix;
            }
        }

        return sign + "0ms";
    }
}
