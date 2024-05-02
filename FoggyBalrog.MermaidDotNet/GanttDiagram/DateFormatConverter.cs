using System.Globalization;

namespace FoggyBalrog.MermaidDotNet.GanttDiagram;

internal static class DateFormatConverter
{
    public static string ToDayjsFormat(DateTimeOffset date, string dayjsFormat)
    {
        var format = dayjsFormat
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
            var day = date.Day;
            var suffix = day switch
            {
                1 or 21 or 31 => "st",
                2 or 22 => "nd",
                3 or 23 => "rd",
                _ => "th",
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
}
