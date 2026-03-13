namespace FoggyBalrog.MermaidDotNet.UnitTests;

public class ValidationTests
{
    [Fact]
    public void ThrowIfContainsInvalidCharacters_DoesNotThrowForEscapedCharacterCodeWhenHashIsInvalid()
    {
        string value = "foo #123; bar";

        value.ThrowIfContainsInvalidCharacters(['#']);
    }

    [Theory]
    [InlineData("foo:bar", new char[] { ':' })]
    [InlineData("foo#bar", new char[] { '#' })]
    [InlineData("foo:bar#baz", new char[] { ':', '>' })]
    public void ThrowIfContainsInvalidCharacters_ThrowsForInvalidCharacters(string input, char[] invalidCharacters)
    {
        MermaidException exception = Assert.Throws<MermaidException>(() =>
        {
            input.ThrowIfContainsInvalidCharacters(invalidCharacters);
        });

        Assert.Equal(MermaidExceptionReason.InvalidCharacter, exception.Reason);
    }

    [Fact]
    public void ThrowIfContainsInvalidCharacters_ThrowsForFirstInvalidCharacterOnly()
    {
        string value = "foo:#bar";

        MermaidException exception = Assert.Throws<MermaidException>(() =>
        {
            value.ThrowIfContainsInvalidCharacters([':', '#']);
        });

        Assert.Contains("':'", exception.Message);
        Assert.DoesNotContain("'#'", exception.Message);
    }

    [Fact]
    public void ThrowIfContainsInvalidCharacters_DoesNotThrowForLineBreakTagWhenOnlyRawLineBreaksAreInvalid()
    {
        string value = "foo<br/>bar";

        value.ThrowIfContainsInvalidCharacters(['\r', '\n']);
    }

    [Fact]
    public void ThrowIfContainsInvalidCharacters_ThrowsForRawLineFeedWhenLineBreakIsInvalid()
    {
        string value = "foo\nbar";

        MermaidException exception = Assert.Throws<MermaidException>(() =>
        {
            value.ThrowIfContainsInvalidCharacters(['\n']);
        });

        Assert.Equal(MermaidExceptionReason.InvalidCharacter, exception.Reason);
        Assert.Contains(@"'\n'", exception.Message);
    }
}
