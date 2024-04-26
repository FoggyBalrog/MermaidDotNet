using System.Text;
using FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.ClassDiagram;

public class ClassDiagramBuilder
{
    private const string _doubleIndent = "        ";
    private const string _singleIndent = "    ";
    private readonly string? _title;
    private readonly List<IClassDiagramItem> _items = [];
    private readonly List<Relationship> _relationships = [];
    private readonly List<Note> _notes = [];
    private readonly List<IStyle> _style = [];
    private readonly ClassDiagramDirection? _direction;

    internal ClassDiagramBuilder(string? title)
    {
        _title = title;
    }

    public ClassDiagramBuilder(string? title, ClassDiagramDirection? direction) : this(title)
    {
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

    public ClassDiagramBuilder AddProperty(Class animal, string type, string name)
    {
        animal.AddProperty(new Property(type, name));
        return this;
    }

    public ClassDiagramBuilder AddMethod(Class animal, string? returnType, string name, Visibilities visibility = Visibilities.Public, (string type, string name)[]? parameters = null)
    {
        animal.AddMethod(new Method(returnType, name, visibility, parameters?.Select(p => new Parameter(p.type, p.name)).ToList() ?? []));
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
        @class.ClickBindind = new ClassCallback(functionName, tooltip);
        return this;
    }

    public ClassDiagramBuilder AddHyperlink(Class @class, string uri, string? tooltip = null)
    {
        @class.ClickBindind = new ClassHyperlink(uri, tooltip);
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
            string directionString = _direction switch
            {
                ClassDiagramDirection.TopToBottom => "TB",
                ClassDiagramDirection.BottomToTop => "BT",
                ClassDiagramDirection.LeftToRight => "LR",
                ClassDiagramDirection.RightToLeft => "RL",
                _ => throw new InvalidOperationException($"Unknown direction: {_direction}")
            };
            builder.AppendLine($"{_singleIndent}direction {directionString}");
        }

        foreach (var note in _notes)
        {
            string noteClassString = note.Class is null ? "" : $" for {note.Class.Name}";
            builder.AppendLine($"{_singleIndent}note{noteClassString} \"{note.Text}\"");
        }

        bool isInNamespace = false;

        foreach (var item in _items)
        {
            switch (item)
            {
                case Class @class:
                    BuildClass(builder, @class, isInNamespace);
                    break;

                case NamespaceStart namespaceStart:
                    builder.AppendLine($"{_singleIndent}namespace {namespaceStart.Name} {{");
                    isInNamespace = true;
                    break;

                case NamespaceEnd _:
                    builder.AppendLine($"{_singleIndent}}}");
                    isInNamespace = false;
                    break;
            }
        }

        foreach (var relationship in _relationships)
        {
            string label = relationship.Label is null ? "" : $" : {relationship.Label}";
            string fromArrow = relationship.FromRelationshipType switch
            {
                RelationshipType.Inheritance => "<|",
                RelationshipType.Composition => "*",
                RelationshipType.Aggregation => "o",
                RelationshipType.Association => "<",
                RelationshipType.Unspecified => "",
                _ => throw new InvalidOperationException($"Unknown \"from\" relationship type: {relationship.FromRelationshipType}")
            };
            string toArrow = relationship.ToRelationshipType switch
            {
                RelationshipType.Inheritance => "|>",
                RelationshipType.Composition => "*",
                RelationshipType.Aggregation => "o",
                RelationshipType.Association => ">",
                RelationshipType.Unspecified => "",
                _ => throw new InvalidOperationException($"Unknown \"to\" relationship type: {relationship.ToRelationshipType}")
            };
            string link = relationship.LinkStyle switch
            {
                LinkStyle.Solid => "--",
                LinkStyle.Dashed => "..",
                _ => throw new InvalidOperationException($"Unknown link style: {relationship.LinkStyle}")
            };
            string fromCardinality = GetCardinalityString(relationship.FromCardinality);
            string toCardinality = GetCardinalityString(relationship.ToCardinality);

            builder.AppendLine($"{_singleIndent}{@relationship.From.Name} {fromCardinality}{fromArrow}{link}{toArrow}{toCardinality} {@relationship.To.Name}{label}");
        }

        foreach (var @class in _items.Where(i => i is Class c && c.ClickBindind is not null).Cast<Class>())
        {
            switch (@class.ClickBindind)
            {
                case ClassCallback classCallback:
                    builder.AppendLine($"{_singleIndent}click {@class.Name} call {classCallback.FunctionName}(){(classCallback.Tooltip is not null ? $" \"{classCallback.Tooltip}\"" : string.Empty)}");
                    break;

                case ClassHyperlink classHyperlink:
                    builder.AppendLine($"{_singleIndent}click {@class.Name} href \"{classHyperlink.Uri}\"{(classHyperlink.Tooltip is not null ? $" \"{classHyperlink.Tooltip}\"" : string.Empty)}");
                    break;
            }
        }

        foreach (var style in _style)
        {
            switch (style)
            {
                case RawCssStyle rawCssStyle:
                    builder.AppendLine($"{_singleIndent}style {rawCssStyle.Class.Name} {rawCssStyle.Css}");
                    break;

                case CssClassStyle cssClassStyle:
                    builder.AppendLine($"{_singleIndent}cssClass \"{string.Join(",", cssClassStyle.Classes.Select(c => c.Name))}\" {cssClassStyle.CssClass}");
                    break;
            }
        }

        // Remove the last newline
        builder.Length -= Environment.NewLine.Length;

        return builder.ToString();
    }

    private void BuildClass(StringBuilder builder, Class @class, bool isInNamespace)
    {
        var singleIndent = isInNamespace ? _doubleIndent : _singleIndent;
        var doubleIndent = isInNamespace ? _doubleIndent + _singleIndent : _doubleIndent;

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
        builder.Append($"{singleIndent}class {@class.Name}{classLabel}");

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
            builder.AppendLine($"{doubleIndent}<<{@class.Annotation}>>");
        }

        foreach (var property in @class.Properties)
        {
            builder.AppendLine($"{doubleIndent}+{EscapeGenerics(property.Type)} {property.Name}");
        }

        foreach (var method in @class.Methods)
        {
            string visibilitPrefix = "";
            string visibilitySuffix = "";

            if (method.Visibility.HasFlag(Visibilities.Public))
            {
                visibilitPrefix += "+";
            }
            if (method.Visibility.HasFlag(Visibilities.Private))
            {
                visibilitPrefix += "-";
            }
            if (method.Visibility.HasFlag(Visibilities.Protected))
            {
                visibilitPrefix += "#";
            }
            if (method.Visibility.HasFlag(Visibilities.Internal))
            {
                visibilitPrefix += "~";
            }
            if (method.Visibility.HasFlag(Visibilities.Abstract))
            {
                visibilitySuffix += "*";
            }
            if (method.Visibility.HasFlag(Visibilities.Static))
            {
                visibilitySuffix += "$";
            }

            string returnTypeString = method.ReturnType is null ? "" : $" {EscapeGenerics(method.ReturnType)}";
            string parametersString = method.Parameters.Any() ? string.Join(", ", method.Parameters.Select(p => $"{EscapeGenerics(p.Type)} {p.Name}")) : "";

            builder.AppendLine($"{doubleIndent}{visibilitPrefix}{method.Name}({parametersString}){visibilitySuffix}{returnTypeString}");
        }

        if (@class.Properties.Any() || @class.Methods.Any() || @class.Annotation is not null)
        {
            builder.AppendLine($"{singleIndent}}}");
        }
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
