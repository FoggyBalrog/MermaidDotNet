namespace FoggyBalrog.MermaidDotNet;

public class MermaidException : Exception
{
    private MermaidException(
        MermaidExceptionReason reason,
        string message) : base(message)
    {
        Reason = reason;
    }

    public MermaidExceptionReason Reason { get; }

    internal static MermaidException ForeignItem(string itemName)
    {
        return new MermaidException(MermaidExceptionReason.ForeignItem, $"Item '{itemName}' must belong to the current diagram.");
    }

    internal static MermaidException ForeignItemInCollection(string itemsName, int index)
    {
        return new MermaidException(MermaidExceptionReason.ForeignItem, $"Item at index {index} in collection '{itemsName}' must belong to the current diagram.");
    }

    internal static MermaidException WhiteSpace(string itemName)
    {
        return new MermaidException(MermaidExceptionReason.WhiteSpace, $"Item '{itemName}' cannot be whitespace.");
    }

    internal static MermaidException Empty(string itemName)
    {
        return new MermaidException(MermaidExceptionReason.EmptyCollection, $"Collection '{itemName}' cannot be empty.");
    }

    internal static MermaidException Duplicate(string valueName)
    {
        return new MermaidException(MermaidExceptionReason.DuplicateValue, $"Item '{valueName}' is a duplicate.");
    }

    internal static MermaidException StrictlyNegative<T>(string valueName, T value)
    {
        return new MermaidException(MermaidExceptionReason.StrictlyNegative, $"Value '{valueName}' must be greater than or equal to zero (actual: {value}).");
    }

    internal static MermaidException OutOfRange<T>(string valueName, T value, T min, T max) where T : struct, IComparable<T>
    {
        return new MermaidException(MermaidExceptionReason.OutOfRange, $"Value '{valueName}' must be between {min} and {max} (actual: {value}).");
    }

    internal static Exception WhiteSpaceInCollection(string itemsName, int index)
    {
        return new MermaidException(MermaidExceptionReason.WhiteSpace, $"Item at index {index} in collection '{itemsName}' cannot be whitespace.");
    }

    internal static Exception InvalidConfiguration(string comment)
    {
        return new MermaidException(MermaidExceptionReason.InvalidConfiguration, $"Invalid configuration: {comment}");
    }
}
