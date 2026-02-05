using System.Runtime.CompilerServices;

namespace FoggyBalrog.MermaidDotNet;

internal static class Validation
{
    public static void ThrowIfForeignTo<T>(this T item, ICollection<T> items, [CallerArgumentExpression(nameof(item))] string? itemName = null)
    {
        if (!items.Contains(item))
        {
            throw MermaidException.ForeignItem(itemName ?? "Unknown");
        }
    }

    public static void ThrowIfForeignToAll<T>(this T item, IEnumerable<T>[] itemCollections, [CallerArgumentExpression(nameof(item))] string? itemName = null)
    {
        if (!itemCollections.Any(c => c.Contains(item)))
        {
            throw MermaidException.ForeignItem(itemName ?? "Unknown");
        }
    }

    public static void ThrowIfAnyForeignTo<T>(this ICollection<T> items, ICollection<T> otherItems, [CallerArgumentExpression(nameof(items))] string? itemsName = null)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (!otherItems.Contains(items.ElementAt(i)))
            {
                throw MermaidException.ForeignItemInCollection(itemsName ?? "Unknown", i);
            }
        }
    }

    public static void ThrowIfWhiteSpace(this string? item, [CallerArgumentExpression(nameof(item))] string? itemName = null)
    {
        if (item is not null && string.IsNullOrWhiteSpace(item))
        {
            throw MermaidException.WhiteSpace(itemName ?? "Unknown");
        }
    }

    public static void ThrowIfAnyWhitespace(this IEnumerable<string?> items, [CallerArgumentExpression(nameof(items))] string? itemsName = null)
    {
        int index = 0;

        foreach (string? item in items)
        {
            if (item is not null && string.IsNullOrWhiteSpace(item))
            {
                throw MermaidException.WhiteSpaceInCollection(itemsName ?? "Unknown", index);
            }
            index++;
        }
    }

    public static void ThrowIfEmpty(this string item, [CallerArgumentExpression(nameof(item))] string? itemName = null)
    {
        if (item.Length == 0)
        {
            throw MermaidException.Empty(itemName ?? "Unknown");
        }
    }

    public static void ThrowIfEmpty<T>(this ICollection<T> items, [CallerArgumentExpression(nameof(items))] string? itemName = null)
    {
        if (items.Count == 0)
        {
            throw MermaidException.Empty(itemName ?? "Unknown");
        }
    }

    public static void ThrowIfDuplicate<T, TProperty>(this IEnumerable<T> items, TProperty value, Func<T, TProperty> propertyValueSelector, [CallerArgumentExpression(nameof(value))] string? valueName = null)
        where T : notnull
        where TProperty : notnull
    {
        if (items.Any(i => propertyValueSelector(i).Equals(value)))
        {
            throw MermaidException.Duplicate(valueName ?? "Unknown");
        }
    }

    public static void ThrowIfStrictlyNegative<T>(this T value, [CallerArgumentExpression(nameof(value))] string? valueName = null)
        where T : struct, IComparable<T>
    {
        if (value.CompareTo(default) < 0)
        {
            throw MermaidException.StrictlyNegative(valueName ?? "Unknown", value);
        }
    }

    public static void ThrowIfOutOfRange<T>(this T value, T min, T max, [CallerArgumentExpression(nameof(value))] string? valueName = null)
        where T : struct, IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
        {
            throw MermaidException.OutOfRange(valueName ?? "Unknown", value, min, max);
        }
    }
}