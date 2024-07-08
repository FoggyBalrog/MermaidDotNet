using System.Text;
using FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.ClassDiagram;

public class ClassDiagramBuilder
{
    private readonly string? _title;
    private readonly List<IClassDiagramItem> _items = [];
    private readonly List<Relationship> _relationships = [];
    private readonly List<Note> _notes = [];
    private readonly List<IStyle> _style = [];
    private readonly ClassDiagramDirection? _direction;

    internal ClassDiagramBuilder(string? title, ClassDiagramDirection? direction)
    {
        _title = title;
        _direction = direction;
    }

    public ClassDiagramBuilder AddClass(string name, out Class @class, string? label = null, string? annotation = null)
    {
        @class = new Class(name, label, annotation, null);
        _items.Add(@class);

        return this;
    }

    public ClassDiagramBuilder AddNamespace(string name, Action<ClassDiagramBuilder> action)
    {
        _items.Add(new NamespaceStart(name));
        action(this);
        _items.Add(new NamespaceEnd());

        return this;
    }

    public ClassDiagramBuilder AddProperty(Class @class, string type, string name)
    {
        @class.AddProperty(new Property(type, name));
        return this;
    }

    public ClassDiagramBuilder AddMethod(Class @class, string? returnType, string name, Visibilities visibility = Visibilities.Public, (string type, string name)[]? parameters = null)
    {
        @class.AddMethod(new Method(returnType, name, visibility, parameters?.Select(p => new Parameter(p.type, p.name)).ToList() ?? []));
        return this;
    }

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
        _relationships.Add(new Relationship(from, to, fromRelationshipType, fromCardinality, toRelationshipType, toCardinality, linkStyle, label));
        return this;
    }

    public ClassDiagramBuilder AddNote(string text, Class? @class = null)
    {
        _notes.Add(new Note(text, @class));
        return this;
    }

    public ClassDiagramBuilder AddCallback(Class @class, string functionName, string? tooltip = null)
    {
        @class.ClickBinding = new ClassCallback(functionName, tooltip);
        return this;
    }

    public ClassDiagramBuilder AddHyperlink(Class @class, string uri, string? tooltip = null)
    {
        @class.ClickBinding = new ClassHyperlink(uri, tooltip);
        return this;
    }

    public ClassDiagramBuilder StyleWithRawCss(Class @class, string css)
    {
        _style.Add(new RawCssStyle(@class, css));
        return this;
    }

    public ClassDiagramBuilder StyleWithCssClass(string cssClass, params Class[] classes)
    {
        _style.Add(new CssClassStyle(cssClass, classes));
        return this;
    }

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
