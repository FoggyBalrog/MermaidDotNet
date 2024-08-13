namespace FoggyBalrog.MermaidDotNet.UnitTests.UserJourneyDiagram;

public class UserJourneyDiagramValidationTests
{
    [Fact]
    public void UserJourneyDiagramBuilder_ThrowsIfTitleIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .UserJourneyDiagram(" ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddTask_ThrowsIfDescriptionIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .UserJourneyDiagram("Title")
                .AddTask(" ", 1, "Actor 1", "Actor 2", "Actor 3");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddTask_ThrowsIfAnyActorIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .UserJourneyDiagram("Title")
                .AddTask("Description", 1, "Actor 1", " ", "Actor 3");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }

    [Fact]
    public void AddSection_ThrowsIfDescriptionIsWhiteSpace()
    {
        var exception = Assert.Throws<MermaidException>(() =>
        {
            Mermaid
                .UserJourneyDiagram("Title")
                .AddSection(" ");
        });

        Assert.Equal(MermaidExceptionReason.WhiteSpace, exception.Reason);
    }
}
