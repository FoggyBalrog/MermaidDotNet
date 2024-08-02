using FoggyBalrog.MermaidDotNet.GanttDiagram;

namespace FoggyBalrog.MermaidDotNet.UnitTests.GanttDiagram;

public class DateFormatConverterTests
{
    private readonly DateTimeOffset _referenceDate1 = new(2024, 01, 3, 08, 06, 07, 371, 954, TimeSpan.FromHours(-5));
    private readonly DateTimeOffset _referenceDate2 = new(2024, 10, 13, 14, 17, 23, 371, 954, TimeSpan.FromHours(14));

    [Theory]
    [InlineData("YY", "24", "24")]
    [InlineData("YYYY", "2024", "2024")]
    [InlineData("M", "1", "10")]
    [InlineData("MM", "01", "10")]
    [InlineData("MMM", "Jan", "Oct")]
    [InlineData("MMMM", "January", "October")]
    [InlineData("D", "3", "13")]
    [InlineData("DD", "03", "13")]
    [InlineData("H", "8", "14")]
    [InlineData("HH", "08", "14")]
    [InlineData("h", "8", "2")]
    [InlineData("hh", "08", "02")]
    [InlineData("m", "6", "17")]
    [InlineData("mm", "06", "17")]
    [InlineData("s", "7", "23")]
    [InlineData("ss", "07", "23")]
    [InlineData("S", "3", "3")]
    [InlineData("SS", "37", "37")]
    [InlineData("SSS", "371", "371")]
    [InlineData("Z", "-05:00", "+14:00")]
    [InlineData("ZZ", "-0500", "+1400")]
    [InlineData("A", "AM", "PM")]
    [InlineData("a", "am", "pm")]
    [InlineData("Do", "3rd", "13th")]
    [InlineData("X", "1704287167.371", "1728778643.371")]
    [InlineData("x", "1704287167371", "1728778643371")]
    public void ToDayjsFormat(string dayjsFormat, string expectedFormattedDate1, string expectedFormattedDate2)
    {
        string formattedDate1 = DateFormatConverter.ToDayjsFormat(_referenceDate1, dayjsFormat);
        string formattedDate2 = DateFormatConverter.ToDayjsFormat(_referenceDate2, dayjsFormat);

        Assert.Equal(expectedFormattedDate1, formattedDate1);
        Assert.Equal(expectedFormattedDate2, formattedDate2);
    }
}
