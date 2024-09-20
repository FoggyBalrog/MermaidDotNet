using System.Text;
using FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.ClassDiagram;

/// <summary>
/// A builder for creating class diagrams.
/// </summary>
public class ClassDiagramBuilder
{
    private readonly string? _title;
    private readonly List<IClassDiagramItem> _items = [];
    private readonly List<Relationship> _relationships = [];
    private readonly List<Note> _notes = [];
    private readonly List<IStyle> _style = [];
    private readonly ClassDiagramDirection? _direction;
    private readonly bool _isSafe;

    internal ClassDiagramBuilder(string? title, ClassDiagramDirection? direction, bool isSafe)
    {
        if (isSafe)
        {
            title.ThrowIfWhiteSpace();
        }

        _title = title;
        _direction = direction;
        _isSafe = isSafe;
    }

    /// <summary>
    /// Adds a class to the diagram.
    /// </summary>
    /// <param name="name">The name of the class.</param>
    /// <param name="class">The class object that is created.</param>
    /// <param name="label">An optional label for the class.</param>
    /// <param name="annotation">An optional annotation for the class.</param>
    /// <returns>The current <see cref="ClassDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="label"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="annotation"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public ClassDiagramBuilder AddClass(string name, out Class @class, string? label = null, string? annotation = null)
    {
        if (_isSafe)
        {
            name.ThrowIfWhiteSpace();
            label.ThrowIfWhiteSpace();
            annotation.ThrowIfWhiteSpace();
        }

        @class = new Class(name, label, annotation, null);
        _items.Add(@class);

        return this;
    }

    /// <summary>
    /// Adds a namespace to the diagram, in which classes and relationships can be added. The namespace will be closed automatically when <paramref name="action"/> is finished.
    /// </summary>
    /// <param name="name">The name of the namespace.</param>
    /// <param name="action">An action that will be executed to add classes and relationships to the namespace.</param>
    /// <returns>The current <see cref="ClassDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public ClassDiagramBuilder AddNamespace(string name, Action<ClassDiagramBuilder> action)
    {
        if (_isSafe)
        {
            name.ThrowIfWhiteSpace();
        }

        _items.Add(new NamespaceStart(name));
        action(this);
        _items.Add(new NamespaceEnd());

        return this;
    }

    /// <summary>
    /// Adds a property to a class.
    /// </summary>
    /// <param name="class">The class to add the property to.</param>
    /// <param name="type">The type of the property.</param>
    /// <param name="name">The name of the property.</param>
    /// <returns>The current <see cref="ClassDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="class"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="type"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public ClassDiagramBuilder AddProperty(Class @class, string type, string name)
    {
        if (_isSafe)
        {
            @class.ThrowIfForeignTo(_items);
            type.ThrowIfWhiteSpace();
            name.ThrowIfWhiteSpace();
        }

        @class.AddProperty(new Property(type, name));
        return this;
    }

    /// <summary>
    /// Adds a method to a class.
    /// </summary>
    /// <param name="class">The class to add the method to.</param>
    /// <param name="returnType">The return type of the method.</param>
    /// <param name="name">The name of the method.</param>
    /// <param name="visibility">The visibility of the method.</param>
    /// <param name="parameters">Optional parameters for the method.</param>
    /// <returns>The current <see cref="ClassDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="class"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="returnType"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="name"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when a type or name in <paramref name="parameters"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public ClassDiagramBuilder AddMethod(Class @class, string? returnType, string name, Visibilities visibility = Visibilities.Public, (string type, string name)[]? parameters = null)
    {
        if (_isSafe)
        {
            @class.ThrowIfForeignTo(_items);
            returnType.ThrowIfWhiteSpace();
            name.ThrowIfWhiteSpace();

            foreach (var (type, parameterName) in parameters ?? [])
            {
                type.ThrowIfWhiteSpace();
                parameterName.ThrowIfWhiteSpace();
            }
        }

        @class.AddMethod(new Method(returnType, name, visibility, parameters?.Select(p => new Parameter(p.type, p.name)).ToList() ?? []));
        return this;
    }

    /// <summary>
    /// Adds a relationship between two classes.
    /// </summary>
    /// <param name="from">The class that the relationship starts from.</param>
    /// <param name="to">The class that the relationship goes to.</param>
    /// <param name="fromRelationshipType">The type of the relationship relative to the <paramref name="from"/> class.</param>
    /// <param name="fromCardinality">The cardinality of the relationship relative to the <paramref name="from"/> class.</param>
    /// <param name="toRelationshipType">The type of the relationship relative to the <paramref name="to"/> class.</param>
    /// <param name="toCardinality">The cardinality of the relationship relative to the <paramref name="to"/> class.</param>
    /// <param name="linkStyle">The style of the link between the classes.</param>
    /// <param name="label">An optional label for the relationship.</param>
    /// <returns>The current <see cref="ClassDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="from"/> class is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="to"/> class is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="label"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public ClassDiagramBuilder AddRelationship(
        Class from,
        Class to,
        RelationshipType fromRelationshipType,
        Cardinality? fromCardinality = null,
        RelationshipType toRelationshipType = RelationshipType.Unspecified,
        Cardinality? toCardinality = null,
        LinkStyle linkStyle = LinkStyle.Solid,
        string? label = null)
    {
        if (_isSafe)
        {
            @from.ThrowIfForeignTo(_items);
            to.ThrowIfForeignTo(_items);
            label.ThrowIfWhiteSpace();
        }

        _relationships.Add(new Relationship(from, to, fromRelationshipType, fromCardinality, toRelationshipType, toCardinality, linkStyle, label));
        return this;
    }

    /// <summary>
    /// Adds a note to the diagram.
    /// </summary>
    /// <param name="text">The text of the note.</param>
    /// <param name="class">An optional class that the note is for.</param>
    /// <returns>The current <see cref="ClassDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="text"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="class"/> is not <c>null</c> and is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    public ClassDiagramBuilder AddNote(string text, Class? @class = null)
    {
        if (_isSafe)
        {
            text.ThrowIfWhiteSpace();
            @class?.ThrowIfForeignTo(_items);
        }

        _notes.Add(new Note(text, @class));
        return this;
    }

    /// <summary>
    /// Adds a callback to a class that will be executed when clicked on the rendered diagram.
    /// </summary>
    /// <param name="class">The class to add the callback to.</param>
    /// <param name="functionName">The name of the function to call when the class is clicked.</param>
    /// <param name="tooltip">An optional tooltip for the callback.</param>
    /// <returns>The current <see cref="ClassDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="class"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="functionName"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="tooltip"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public ClassDiagramBuilder AddCallback(Class @class, string functionName, string? tooltip = null)
    {
        if (_isSafe)
        {
            @class.ThrowIfForeignTo(_items);
            functionName.ThrowIfWhiteSpace();
            tooltip.ThrowIfWhiteSpace();
        }

        @class.ClickBinding = new ClassCallback(functionName, tooltip);
        return this;
    }

    /// <summary>
    /// Adds a hyperlink to a class that will be opened when clicked on the rendered diagram.
    /// </summary>
    /// <param name="class">The class to add the hyperlink to.</param>
    /// <param name="uri">The URI to open when the class is clicked.</param>
    /// <param name="tooltip">An optional tooltip for the hyperlink.</param>
    /// <returns>The current <see cref="ClassDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="class"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="uri"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="tooltip"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public ClassDiagramBuilder AddHyperlink(Class @class, string uri, string? tooltip = null)
    {
        if (_isSafe)
        {
            @class.ThrowIfForeignTo(_items);
            uri.ThrowIfWhiteSpace();
            tooltip.ThrowIfWhiteSpace();
        }

        @class.ClickBinding = new ClassHyperlink(uri, tooltip);
        return this;
    }

    /// <summary>
    /// Adds a style to a class using raw CSS.
    /// </summary>
    /// <param name="class">The class to add the style to.</param>
    /// <param name="css">The raw CSS to apply to the class.</param>
    /// <returns>The current <see cref="ClassDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="class"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="css"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    public ClassDiagramBuilder StyleWithRawCss(Class @class, string css)
    {
        if (_isSafe)
        {
            @class.ThrowIfForeignTo(_items);
            css.ThrowIfWhiteSpace();
        }

        _style.Add(new RawCssStyle(@class, css));
        return this;
    }

    /// <summary>
    /// Adds a style to one or more classes using a CSS class.
    /// </summary>
    /// <param name="cssClass">The CSS class to apply to the classes.</param>
    /// <param name="classes">The classes to apply the CSS class to.</param>
    /// <returns>The current <see cref="ClassDiagramBuilder"/> instance.</returns>
    /// <exception cref="MermaidException">Thrown when <paramref name="cssClass"/> is whitespace, with the reason <see cref="MermaidExceptionReason.WhiteSpace"/>.</exception>
    /// <exception cref="MermaidException">Thrown when <paramref name="class"/>es collection is empty, with the reason <see cref="MermaidExceptionReason.EmptyCollection"/>.</exception>
    /// <exception cref="MermaidException">Thrown when any of <paramref name="classes"/> is not part of the diagram, with the reason <see cref="MermaidExceptionReason.ForeignItem"/>.</exception>
    public ClassDiagramBuilder StyleWithCssClass(string cssClass, params Class[] classes)
    {
        if (_isSafe)
        {
            cssClass.ThrowIfWhiteSpace();
            classes.ThrowIfEmpty();
            classes.ForEach(c => c.ThrowIfForeignTo(_items));
        }

        _style.Add(new CssClassStyle(cssClass, classes));
        return this;
    }

    /// <summary>
    /// Builds the Mermaid code for the class diagram.
    /// </summary>
    /// <returns>The Mermaid code for the class diagram.</returns>
    public string Build()
    {
        var builder = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(_title))
        {
            builder.AppendLine("---");
            builder.AppendLine($"title: {_title}");
            builder.AppendLine("---");
        }

        builder.AppendLine("classDiagram");

        if (_direction is not null)
        {
            string directionString = SymbolMaps.Direction[_direction.Value];
            builder.AppendLine($"{Shared.Indent}direction {directionString}");
        }

        foreach (Note? note in _notes)
        {
            string noteClassString = note.Class is null ? "" : $" for {note.Class.Name}";
            builder.AppendLine($"{Shared.Indent}note{noteClassString} \"{note.Text}\"");
        }

        bool isInNamespace = false;

        foreach (IClassDiagramItem? item in _items)
        {
            switch (item)
            {
                case Class @class:
                    BuildClass(builder, @class, isInNamespace);
                    break;

                case NamespaceStart namespaceStart:
                    builder.AppendLine($"{Shared.Indent}namespace {namespaceStart.Name} {{");
                    isInNamespace = true;
                    break;

                case NamespaceEnd:
                    builder.AppendLine($"{Shared.Indent}}}");
                    isInNamespace = false;
                    break;
            }
        }

        foreach (Relationship? relationship in _relationships)
        {
            BuildRelationship(builder, relationship);
        }

        foreach (Class? @class in _items.Where(i => i is Class { ClickBinding: not null }).Cast<Class>())
        {
            switch (@class.ClickBinding)
            {
                case ClassCallback classCallback:
                    builder.AppendLine($"{Shared.Indent}click {@class.Name} call {classCallback.FunctionName}(){(classCallback.Tooltip is not null ? $" \"{classCallback.Tooltip}\"" : string.Empty)}");
                    break;

                case ClassHyperlink classHyperlink:
                    builder.AppendLine($"{Shared.Indent}click {@class.Name} href \"{classHyperlink.Uri}\"{(classHyperlink.Tooltip is not null ? $" \"{classHyperlink.Tooltip}\"" : string.Empty)}");
                    break;
            }
        }

        foreach (IStyle? style in _style)
        {
            switch (style)
            {
                case RawCssStyle rawCssStyle:
                    builder.AppendLine($"{Shared.Indent}style {rawCssStyle.Class.Name} {rawCssStyle.Css}");
                    break;

                case CssClassStyle cssClassStyle:
                    builder.AppendLine($"{Shared.Indent}cssClass \"{string.Join(",", cssClassStyle.Classes.Select(c => c.Name))}\" {cssClassStyle.CssClass}");
                    break;
            }
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }

    private static void BuildRelationship(StringBuilder builder, Relationship relationship)
    {
        string label = relationship.Label is null ? "" : $" : {relationship.Label}";
        string fromArrow = SymbolMaps.Relationship[relationship.FromRelationshipType].From;
        string toArrow = SymbolMaps.Relationship[relationship.ToRelationshipType].To;
        string link = SymbolMaps.Link[relationship.LinkStyle];
        string fromCardinality = GetCardinalityString(relationship.FromCardinality);
        string toCardinality = GetCardinalityString(relationship.ToCardinality);

        builder.AppendLine($"{Shared.Indent}{relationship.From.Name} {fromCardinality}{fromArrow}{link}{toArrow}{toCardinality} {relationship.To.Name}{label}");
    }

    private void BuildClass(StringBuilder builder, Class @class, bool isInNamespace)
    {
        string externalIndent = isInNamespace ? Shared.Indent.Repeat(2) : Shared.Indent;
        string internalIndent = isInNamespace ? Shared.Indent.Repeat(3) : Shared.Indent.Repeat(2);

        // Ignore empty classes that are part of a relationship or in a namespace (to limit output verbosity)
        if (!isInNamespace
            && @class.Label is null
            && !@class.Properties.Any()
            && !@class.Methods.Any()
            && _relationships.Exists(r => r.From == @class || r.To == @class))
        {
            return;
        }

        string classLabel = @class.Label is null ? "" : $"[\"{@class.Label}\"]";
        builder.Append($"{externalIndent}class {@class.Name}{classLabel}");

        if (@class.Properties.Any() || @class.Methods.Any() || @class.Annotation is not null)
        {
            builder.AppendLine(" {");
        }
        else
        {
            builder.AppendLine();
        }

        if (@class.Annotation is not null)
        {
            builder.AppendLine($"{internalIndent}<<{@class.Annotation}>>");
        }

        foreach (Property? property in @class.Properties)
        {
            builder.AppendLine($"{internalIndent}+{EscapeGenerics(property.Type)} {property.Name}");
        }

        foreach (Method? method in @class.Methods)
        {

            (string visibilityPrefix, string visibilitySuffix) = GetVisibilityPrefixAndSuffix(method);

            string returnTypeString = method.ReturnType is null ? "" : $" {EscapeGenerics(method.ReturnType)}";
            string parametersString = method.Parameters.Any() ? string.Join(", ", method.Parameters.Select(p => $"{EscapeGenerics(p.Type)} {p.Name}")) : "";

            builder.AppendLine($"{internalIndent}{visibilityPrefix}{method.Name}({parametersString}){visibilitySuffix}{returnTypeString}");
        }

        if (@class.Properties.Any() || @class.Methods.Any() || @class.Annotation is not null)
        {
            builder.AppendLine($"{externalIndent}}}");
        }
    }

    private static (string prefix, string sufffix) GetVisibilityPrefixAndSuffix(Method method)
    {
        var prefixBuilder = new StringBuilder();
        var suffixBuilder = new StringBuilder();

        foreach (var (prefix, suffix) in SymbolMaps.Visibility.Where(map => method.Visibility.HasFlag(map.Key)).Select(map => map.Value))
        {
            prefixBuilder.Append(prefix);
            suffixBuilder.Append(suffix);
        }

        return (prefixBuilder.ToString(), suffixBuilder.ToString());
    }

    private static string GetCardinalityString(Cardinality? cardinality)
    {
        if (cardinality is null)
        {
            return "";
        }

        if (cardinality.From == cardinality.To)
        {
            return $"\"{cardinality.From}\" ";
        }

        return $"\"{cardinality.From}..{cardinality.To}\" ";
    }

    private static string EscapeGenerics(string input)
    {
        return input.Replace("<", "~").Replace(">", "~");
    }
}
