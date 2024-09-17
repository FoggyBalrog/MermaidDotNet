namespace FoggyBalrog.MermaidDotNet.UnitTests.GitGraph;

public class GitGraphSafeModeValidationTests
{
    [Fact]
    public void GitGraphBuilder_ThrowsIfTitleIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid.GitGraph(" ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void Commit_ThrowsIfIdIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GitGraph("Title")
                .Commit(" ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void Commit_ThrowsIfTagIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GitGraph("Title")
                .Commit(tag: " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void Branch_ThrowsIfNameIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GitGraph("Title")
                .Branch(" ", out var _);
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void Checkout_ThrowIfBranchIsForeign()
    {
        Mermaid
            .GitGraph("Title")
            .Branch("foo", out var foo);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GitGraph("Title")
                .Checkout(foo);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void Merge_ThrowsIfBranchIsForeign()
    {
        Mermaid
            .GitGraph("Title")
            .Branch("foo", out var foo);

        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GitGraph("Title")
                .Merge(foo);
        });

        Assert.Equal(MermaidExceptionReason.ForeignItem, exception.Reason);
    }

    [Fact]
    public void Merge_ThrowsIfIdIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GitGraph("Title")
                .Branch("foo", out var foo)
                .Merge(foo, " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void Merge_ThrowsIfTagIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .GitGraph("Title")
                .Branch("foo", out var foo)
                .Merge(foo, tag: " ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }
}
