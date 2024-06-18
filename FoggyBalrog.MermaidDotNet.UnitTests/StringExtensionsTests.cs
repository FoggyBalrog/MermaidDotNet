namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("", 0, "")]
    [InlineData("", 1, "")]
    [InlineData("foo", 0, "")]
    [InlineData("foo", 1, "foo")]
    [InlineData("foo", 2, "foofoo")]
    [InlineData("foo", 3, "foofoofoo")]
    public void Repeat(string input, uint count, string expected)
    {
        // Act
        string output = input.Repeat(count);

        // Assert
        Assert.Equal(expected, output);
    }
}
