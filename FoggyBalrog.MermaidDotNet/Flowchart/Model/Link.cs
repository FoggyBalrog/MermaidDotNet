namespace FoggyBalrog.MermaidDotNet.Flowchart.Model;

public record Link : IFlowItem
{
    internal Link(
        ILinkable from,
        ILinkable to,
        string? text,
        LinkLineStyle lineStyle,
        LinkEnding ending,
        bool multidirectional,
        int extraLength)
    {
        From = from;
        To = to;
        Text = text;
        LineStyle = lineStyle;
        Ending = ending;
        Multidirectional = multidirectional;
        ExtraLength = extraLength;
    }

    public ILinkable From { get; }
    public ILinkable To { get; }
    public string? Text { get; }
    public LinkLineStyle LineStyle { get; }
    public LinkEnding Ending { get; }
    public bool Multidirectional { get; }
    public int ExtraLength { get; }
}
