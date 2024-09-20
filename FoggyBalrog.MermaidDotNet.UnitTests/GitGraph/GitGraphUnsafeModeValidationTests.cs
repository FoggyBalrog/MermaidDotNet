namespace FoggyBalrog.MermaidDotNet.UnitTests.GitGraph;

public class GitGraphUnsafeModeValidationTests
{
    [Fact]
    public void GitGraphBuilder_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GitGraph(" ")
            .Build();
    }

    [Fact]
    public void Commit_DoesNotThrowIfIdIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GitGraph("Title")
            .Commit(" ")
            .Build();
    }

    [Fact]
    public void Commit_DoesNotThrowIfTagIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GitGraph("Title")
            .Commit(tag: " ")
            .Build();
    }

    [Fact]
    public void Branch_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GitGraph("Title")
            .Branch(" ", out var _)
            .Build();
    }

    [Fact]
    public void Checkout_ThrowIfBranchIsForeign()
    {
        Mermaid
            .Unsafe
            .GitGraph("Title")
            .Branch("foo", out var foo);

        Mermaid
            .Unsafe
            .GitGraph("Title")
            .Checkout(foo)
            .Build();
    }

    [Fact]
    public void Merge_DoesNotThrowIfBranchIsForeign()
    {
        Mermaid
            .Unsafe
            .GitGraph("Title")
            .Branch("foo", out var foo);

        Mermaid
            .Unsafe
            .GitGraph("Title")
            .Merge(foo)
            .Build();
    }

    [Fact]
    public void Merge_DoesNotThrowIfIdIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GitGraph("Title")
            .Branch("foo", out var foo)
            .Merge(foo, " ")
            .Build();
    }

    [Fact]
    public void Merge_DoesNotThrowIfTagIsWhiteSpace()
    {
        Mermaid
            .Unsafe
            .GitGraph("Title")
            .Branch("foo", out var foo)
            .Merge(foo, tag: " ")
            .Build();
    }
}
