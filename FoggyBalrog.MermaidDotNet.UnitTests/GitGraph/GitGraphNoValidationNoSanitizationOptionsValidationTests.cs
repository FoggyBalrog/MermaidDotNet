namespace FoggyBalrog.MermaidDotNet.UnitTests.GitGraph;

public class GitGraphNoValidationNoSanitizationOptionsValidationTests
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public void GitGraphBuilder_DoesNotThrowIfTitleIsWhiteSpace()
    {
        Mermaid
            .GitGraph(" ", options: _options)
            .Build();
    }

    [Fact]
    public void Commit_DoesNotThrowIfIdIsWhiteSpace()
    {
        Mermaid
            .GitGraph("Title", options: _options)
            .Commit(" ")
            .Build();
    }

    [Fact]
    public void Commit_DoesNotThrowIfTagIsWhiteSpace()
    {
        Mermaid
            .GitGraph("Title", options: _options)
            .Commit(tag: " ")
            .Build();
    }

    [Fact]
    public void Branch_DoesNotThrowIfNameIsWhiteSpace()
    {
        Mermaid
            .GitGraph("Title", options: _options)
            .Branch(" ", out var _)
            .Build();
    }

    [Fact]
    public void Checkout_ThrowIfBranchIsForeign()
    {
        Mermaid
            .GitGraph("Title", options: _options)
            .Branch("foo", out var foo);

        Mermaid
            .GitGraph("Title", options: _options)
            .Checkout(foo)
            .Build();
    }

    [Fact]
    public void Merge_DoesNotThrowIfBranchIsForeign()
    {
        Mermaid
            .GitGraph("Title", options: _options)
            .Branch("foo", out var foo);

        Mermaid
            .GitGraph("Title", options: _options)
            .Merge(foo)
            .Build();
    }

    [Fact]
    public void Merge_DoesNotThrowIfIdIsWhiteSpace()
    {
        Mermaid
            .GitGraph("Title", options: _options)
            .Branch("foo", out var foo)
            .Merge(foo, " ")
            .Build();
    }

    [Fact]
    public void Merge_DoesNotThrowIfTagIsWhiteSpace()
    {
        Mermaid
            .GitGraph("Title", options: _options)
            .Branch("foo", out var foo)
            .Merge(foo, tag: " ")
            .Build();
    }
}
