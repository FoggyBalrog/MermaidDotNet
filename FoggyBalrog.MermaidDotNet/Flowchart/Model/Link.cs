﻿namespace FoggyBalrog.MermaidDotNet.Flowchart.Model;

/// <summary>
/// Represents a link between one or more flowchart items to another one or more flowchart items.
/// </summary>
public record Link : IFlowItem
{
    internal Link(
        ILinkable[] from,
        ILinkable[] to,
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

    /// <summary>
    /// The flowchart items from which the link originates.
    /// </summary>
    public ILinkable[] From { get; }

    /// <summary>
    /// The flowchart items to which the link goes.
    /// </summary>
    public ILinkable[] To { get; }

    /// <summary>
    /// An optional text to display on the link.
    /// </summary>
    public string? Text { get; }

    /// <summary>
    /// The style of the line.
    /// </summary>
    public LinkLineStyle LineStyle { get; }

    /// <summary>
    /// The ending of the link.
    /// </summary>
    public LinkEnding Ending { get; }

    /// <summary>
    /// Whether the link is multidirectional or not.
    /// </summary>
    public bool Multidirectional { get; }

    /// <summary>
    /// The extra length of the link.
    /// </summary>
    public int ExtraLength { get; }
}
