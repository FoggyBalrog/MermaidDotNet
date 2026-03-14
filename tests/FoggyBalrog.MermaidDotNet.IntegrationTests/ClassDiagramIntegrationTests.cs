using FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.IntegrationTests;

public class ClassDiagramBuilderDefaultOptionsBuildIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    [Fact]
    public async Task CanBuildSimpleClassDiagramWithTitle()
    {
        string diagram = Mermaid
            .ClassDiagram("Simple Class Diagram")
            .AddClass("Animal", out Class animal)
            .AddClass("Dog", out Class dog)
            .AddProperty(animal, "int", "Age")
            .AddMethod(animal, null, "Breathe")
            .AddMethod(animal, "void", "Eat", Visibilities.Public | Visibilities.Abstract,
            [
                ("Food", "food")
            ])
            .AddMethod(dog, "Sound", "Bark", parameters: [
                ("int", "times"),
("int", "volume")
            ])
            .AddRelationship(animal, dog, RelationshipType.Inheritance, label: "A dog is an animal")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildSimpleClassDiagramWithoutTitle()
    {
        string diagram = Mermaid
            .ClassDiagram()
            .AddClass("Animal", out Class animal)
            .AddClass("Dog", out Class dog)
            .AddProperty(animal, "int", "Age")
            .AddMethod(animal, null, "Breathe")
            .AddMethod(animal, "void", "Eat", Visibilities.Public | Visibilities.Abstract,
                        [
                ("Food", "food")
            ])
            .AddMethod(dog, "Sound", "Bark", parameters: [
                ("int", "times"),
        ("int", "volume")
            ])
            .AddRelationship(animal, dog, RelationshipType.Inheritance, label: "A dog is an animal")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithGenericTypes()
    {
        string diagram = Mermaid
            .ClassDiagram()
            .AddClass("c", out Class c)
            .AddProperty(c, "List<List<int>>", "MyList")
            .AddMethod(c, "List<List<int>>", "GetList", parameters: [
                ("List<List<int>>", "list")
            ])
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithAllLinkStyles()
    {
        string diagram = Mermaid
            .ClassDiagram()
            .AddClass("c1", out Class c1)
            .AddClass("c2", out Class c2)
            .AddClass("c3", out Class c3)
            .AddClass("c4", out Class c4)
            .AddRelationship(c1, c2, RelationshipType.Inheritance, linkStyle: LinkStyle.Solid)
            .AddRelationship(c3, c4, RelationshipType.Inheritance, linkStyle: LinkStyle.Dashed)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithAllSingleWayRelationships()
    {
        string diagram = Mermaid
            .ClassDiagram()
            .AddClass("c1", out Class c1)
            .AddClass("c2", out Class c2)
            .AddClass("c3", out Class c3)
            .AddClass("c4", out Class c4)
            .AddClass("c5", out Class c5)
            .AddClass("c6", out Class c6)
            .AddClass("c7", out Class c7)
            .AddClass("c8", out Class c8)
            .AddClass("c9", out Class c9)
            .AddClass("c10", out Class c10)
            .AddRelationship(c1, c2, RelationshipType.Inheritance)
            .AddRelationship(c3, c4, RelationshipType.Composition)
            .AddRelationship(c5, c6, RelationshipType.Aggregation)
            .AddRelationship(c7, c8, RelationshipType.Association)
            .AddRelationship(c9, c10, RelationshipType.Unspecified)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithAllTwoWayRelationships()
    {
        string diagram = Mermaid
            .ClassDiagram()
            .AddClass("c1", out Class c1)
            .AddClass("c2", out Class c2)
            .AddClass("c3", out Class c3)
            .AddClass("c4", out Class c4)
            .AddClass("c5", out Class c5)
            .AddClass("c6", out Class c6)
            .AddClass("c7", out Class c7)
            .AddClass("c8", out Class c8)
            .AddClass("c9", out Class c9)
            .AddClass("c10", out Class c10)
            .AddClass("c11", out Class c11)
            .AddClass("c12", out Class c12)
            .AddClass("c13", out Class c13)
            .AddClass("c14", out Class c14)
            .AddClass("c15", out Class c15)
            .AddClass("c16", out Class c16)
            .AddClass("c17", out Class c17)
            .AddClass("c18", out Class c18)
            .AddClass("c19", out Class c19)
            .AddClass("c20", out Class c20)
            .AddClass("c21", out Class c21)
            .AddClass("c22", out Class c22)
            .AddClass("c23", out Class c23)
            .AddClass("c24", out Class c24)
            .AddClass("c25", out Class c25)
            .AddClass("c26", out Class c26)
            .AddClass("c27", out Class c27)
            .AddClass("c28", out Class c28)
            .AddClass("c29", out Class c29)
            .AddClass("c30", out Class c30)
            .AddRelationship(c1, c2, RelationshipType.Inheritance, toRelationshipType: RelationshipType.Inheritance)
            .AddRelationship(c1, c3, RelationshipType.Inheritance, toRelationshipType: RelationshipType.Composition)
            .AddRelationship(c1, c4, RelationshipType.Inheritance, toRelationshipType: RelationshipType.Aggregation)
            .AddRelationship(c1, c5, RelationshipType.Inheritance, toRelationshipType: RelationshipType.Association)
            .AddRelationship(c1, c6, RelationshipType.Inheritance, toRelationshipType: RelationshipType.Unspecified)
            .AddRelationship(c7, c8, RelationshipType.Composition, toRelationshipType: RelationshipType.Inheritance)
            .AddRelationship(c7, c9, RelationshipType.Composition, toRelationshipType: RelationshipType.Composition)
            .AddRelationship(c7, c10, RelationshipType.Composition, toRelationshipType: RelationshipType.Aggregation)
            .AddRelationship(c7, c11, RelationshipType.Composition, toRelationshipType: RelationshipType.Association)
            .AddRelationship(c7, c12, RelationshipType.Composition, toRelationshipType: RelationshipType.Unspecified)
            .AddRelationship(c13, c14, RelationshipType.Aggregation, toRelationshipType: RelationshipType.Inheritance)
            .AddRelationship(c13, c15, RelationshipType.Aggregation, toRelationshipType: RelationshipType.Composition)
            .AddRelationship(c13, c16, RelationshipType.Aggregation, toRelationshipType: RelationshipType.Aggregation)
            .AddRelationship(c13, c17, RelationshipType.Aggregation, toRelationshipType: RelationshipType.Association)
            .AddRelationship(c13, c18, RelationshipType.Aggregation, toRelationshipType: RelationshipType.Unspecified)
            .AddRelationship(c19, c20, RelationshipType.Association, toRelationshipType: RelationshipType.Inheritance)
            .AddRelationship(c19, c21, RelationshipType.Association, toRelationshipType: RelationshipType.Composition)
            .AddRelationship(c19, c22, RelationshipType.Association, toRelationshipType: RelationshipType.Aggregation)
            .AddRelationship(c19, c23, RelationshipType.Association, toRelationshipType: RelationshipType.Association)
            .AddRelationship(c19, c24, RelationshipType.Association, toRelationshipType: RelationshipType.Unspecified)
            .AddRelationship(c25, c26, RelationshipType.Unspecified, toRelationshipType: RelationshipType.Inheritance)
            .AddRelationship(c25, c27, RelationshipType.Unspecified, toRelationshipType: RelationshipType.Composition)
            .AddRelationship(c25, c28, RelationshipType.Unspecified, toRelationshipType: RelationshipType.Aggregation)
            .AddRelationship(c25, c29, RelationshipType.Unspecified, toRelationshipType: RelationshipType.Association)
            .AddRelationship(c25, c30, RelationshipType.Unspecified, toRelationshipType: RelationshipType.Unspecified)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithAllCardinalities()
    {
        string diagram = Mermaid
            .ClassDiagram()
            .AddClass("c1", out Class c1)
            .AddClass("c2", out Class c2)
                    .AddClass("c3", out Class c3)
                    .AddClass("c4", out Class c4)
                    .AddClass("c5", out Class c5)
                    .AddClass("c6", out Class c6)
                    .AddClass("c7", out Class c7)
                    .AddClass("c8", out Class c8)
                    .AddClass("c9", out Class c9)
                    .AddClass("c10", out Class c10)
                    .AddClass("c11", out Class c11)
                    .AddClass("c12", out Class c12)
                    .AddClass("c13", out Class c13)
                    .AddClass("c14", out Class c14)
                    .AddClass("c15", out Class c15)
                    .AddClass("c16", out Class c16)
                    .AddClass("c17", out Class c17)
                    .AddClass("c18", out Class c18)
                    .AddClass("c19", out Class c19)
                    .AddClass("c20", out Class c20)
                    .AddClass("c21", out Class c21)
                    .AddClass("c22", out Class c22)
                    .AddClass("c23", out Class c23)
                    .AddClass("c24", out Class c24)
                    .AddClass("c25", out Class c25)
                    .AddClass("c26", out Class c26)
                    .AddClass("c27", out Class c27)
                    .AddClass("c28", out Class c28)
                    .AddClass("c29", out Class c29)
                    .AddClass("c30", out Class c30)
                    .AddClass("c31", out Class c31)
                    .AddClass("c32", out Class c32)
                    .AddClass("c33", out Class c33)
                    .AddClass("c34", out Class c34)
                    .AddClass("c35", out Class c35)
                    .AddClass("c36", out Class c36)
                    .AddClass("c37", out Class c37)
                    .AddClass("c38", out Class c38)
                    .AddClass("c39", out Class c39)
                    .AddClass("c40", out Class c40)
                    .AddClass("c41", out Class c41)
                    .AddClass("c42", out Class c42)
                    .AddClass("c43", out Class c43)
                    .AddClass("c44", out Class c44)
                    .AddClass("c45", out Class c45)
                    .AddClass("c46", out Class c46)
                    .AddClass("c47", out Class c47)
                    .AddClass("c48", out Class c48)
                    .AddClass("c49", out Class c49)
                    .AddClass("c50", out Class c50)
                    .AddClass("c51", out Class c51)
                    .AddClass("c52", out Class c52)
                    .AddClass("c53", out Class c53)
                    .AddClass("c54", out Class c54)
                    .AddRelationship(c1, c2, RelationshipType.Inheritance, fromCardinality: Cardinality.One)
                    .AddRelationship(c3, c4, RelationshipType.Inheritance, fromCardinality: Cardinality.ZeroOrOne)
                    .AddRelationship(c5, c6, RelationshipType.Inheritance, fromCardinality: Cardinality.ZeroOrMore)
                    .AddRelationship(c7, c8, RelationshipType.Inheritance, fromCardinality: Cardinality.OneOrMore)
                    .AddRelationship(c9, c10, RelationshipType.Inheritance, fromCardinality: Cardinality.Any)
                    .AddRelationship(c11, c12, RelationshipType.Inheritance, fromCardinality: Cardinality.Exactly(42))
                    .AddRelationship(c13, c14, RelationshipType.Inheritance, fromCardinality: Cardinality.Exactly("n"))
                    .AddRelationship(c15, c16, RelationshipType.Inheritance, fromCardinality: Cardinality.Range(42, 99))
                    .AddRelationship(c17, c18, RelationshipType.Inheritance, fromCardinality: Cardinality.Range("a", "b"))
                    .AddRelationship(c19, c20, RelationshipType.Inheritance, toCardinality: Cardinality.One)
                    .AddRelationship(c21, c22, RelationshipType.Inheritance, toCardinality: Cardinality.ZeroOrOne)
                    .AddRelationship(c23, c24, RelationshipType.Inheritance, toCardinality: Cardinality.ZeroOrMore)
                    .AddRelationship(c25, c26, RelationshipType.Inheritance, toCardinality: Cardinality.OneOrMore)
                    .AddRelationship(c27, c28, RelationshipType.Inheritance, toCardinality: Cardinality.Any)
                    .AddRelationship(c29, c30, RelationshipType.Inheritance, toCardinality: Cardinality.Exactly(42))
                    .AddRelationship(c31, c32, RelationshipType.Inheritance, toCardinality: Cardinality.Exactly("n"))
                    .AddRelationship(c33, c34, RelationshipType.Inheritance, toCardinality: Cardinality.Range(42, 99))
                    .AddRelationship(c35, c36, RelationshipType.Inheritance, toCardinality: Cardinality.Range("a", "b"))
                    .AddRelationship(c37, c38, RelationshipType.Inheritance, fromCardinality: Cardinality.One, toCardinality: Cardinality.One)
                    .AddRelationship(c39, c40, RelationshipType.Inheritance, fromCardinality: Cardinality.ZeroOrOne, toCardinality: Cardinality.ZeroOrOne)
                    .AddRelationship(c41, c42, RelationshipType.Inheritance, fromCardinality: Cardinality.ZeroOrMore, toCardinality: Cardinality.ZeroOrMore)
                    .AddRelationship(c43, c44, RelationshipType.Inheritance, fromCardinality: Cardinality.OneOrMore, toCardinality: Cardinality.OneOrMore)
                    .AddRelationship(c45, c46, RelationshipType.Inheritance, fromCardinality: Cardinality.Any, toCardinality: Cardinality.Any)
                    .AddRelationship(c47, c48, RelationshipType.Inheritance, fromCardinality: Cardinality.Exactly(42), toCardinality: Cardinality.Exactly(42))
                    .AddRelationship(c49, c50, RelationshipType.Inheritance, fromCardinality: Cardinality.Exactly("n"), toCardinality: Cardinality.Exactly("n"))
                    .AddRelationship(c51, c52, RelationshipType.Composition, fromCardinality: Cardinality.Range(42, 99), toCardinality: Cardinality.Range(42, 99))
                    .AddRelationship(c53, c54, RelationshipType.Aggregation, fromCardinality: Cardinality.Range("a", "b"), toCardinality: Cardinality.Range("a", "b"))
                    .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramsWithAllDirections()
    {
        string diagram1 = Mermaid
            .ClassDiagram("Top to Bottom", direction: ClassDiagramDirection.TopToBottom)
            .AddClass("c1", out Class d1c1)
            .AddClass("c2", out Class d1c2)
            .AddRelationship(d1c1, d1c2, RelationshipType.Inheritance)
            .Build();

        string diagram2 = Mermaid
            .ClassDiagram("Bottom to Top", direction: ClassDiagramDirection.BottomToTop)
            .AddClass("c1", out Class d2c1)
            .AddClass("c2", out Class d2c2)
            .AddRelationship(d2c1, d2c2, RelationshipType.Inheritance)
            .Build();

        string diagram3 = Mermaid
            .ClassDiagram("Left to Right", direction: ClassDiagramDirection.LeftToRight)
            .AddClass("c1", out Class d3c1)
            .AddClass("c2", out Class d3c2)
            .AddRelationship(d3c1, d3c2, RelationshipType.Inheritance)
            .Build();

        string diagram4 = Mermaid
            .ClassDiagram("Right to Left", direction: ClassDiagramDirection.RightToLeft)
            .AddClass("c1", out Class d4c1)
            .AddClass("c2", out Class d4c2)
            .AddRelationship(d4c1, d4c2, RelationshipType.Inheritance)
            .Build();

        var diagram1Result = await toolingFixture.ValidateDiagramAsync(diagram1);

        Assert.True(diagram1Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram1, diagram1Result));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagram1Result));

        var diagram2Result = await toolingFixture.ValidateDiagramAsync(diagram2);

        Assert.True(diagram2Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram2, diagram2Result));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagram2Result));

        var diagram3Result = await toolingFixture.ValidateDiagramAsync(diagram3);

        Assert.True(diagram3Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram3, diagram3Result));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagram3Result));

        var diagram4Result = await toolingFixture.ValidateDiagramAsync(diagram4);

        Assert.True(diagram4Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram4, diagram4Result));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagram4Result));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithClickBinding()
    {
        string diagram = Mermaid
            .ClassDiagram()
            .AddClass("c1", out Class c1)
            .AddClass("c2", out Class c2)
            .AddClass("c3", out Class c3)
            .AddClass("c4", out Class c4)
            .AddCallback(c1, "callback")
            .AddCallback(c2, "callback", "tooltip")
            .AddHyperlink(c3, "https://example.com")
            .AddHyperlink(c4, "https://example.com", "tooltip")
            .AddRelationship(c1, c2, RelationshipType.Inheritance)
            .AddRelationship(c3, c4, RelationshipType.Inheritance)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithNotes()
    {
        string diagram = Mermaid
            .ClassDiagram()
            .AddClass("c1", out Class c1)
            .AddNote("General note")
            .AddNote("Specific note", c1)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithCustomStyle()
    {
        string diagram = Mermaid
            .ClassDiagram()
            .AddClass("c1", out Class c1)
            .AddClass("c2", out Class c2)
            .AddClass("c3", out Class c3)
            .StyleWithRawCss(c1, "fill:#f9f,stroke:#333,stroke-width:4px")
            .StyleWithCssClass("styleClass", c2, c3)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithNamespaces()
    {
        string diagram = Mermaid
            .ClassDiagram()
            .AddClass("c1", out Class c1)
            .AddClass("c2", out Class _)
            .AddNamespace("ns1", builder => builder
                .AddClass("c3", out Class c3)
                .AddClass("c4", out Class c4)
                .AddRelationship(c3, c4, RelationshipType.Inheritance))
            .AddClass("c5", out Class _)
            .AddNamespace("ns2", builder => builder
                .AddClass("c6", out Class c6)
                .AddClass("c7", out Class c7)
                .AddRelationship(c6, c7, RelationshipType.Inheritance)
                .AddRelationship(c1, c7, RelationshipType.Inheritance))
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithClassLabels()
    {
        string diagram = Mermaid
            .ClassDiagram()
            .AddClass("c1", out Class c1, "Hello World!")
            .AddClass("c2", out Class _, "Hello World!")
            .AddProperty(c1, "int", "Age")
            .AddMethod(c1, null, "Breathe")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithClassAnnotations()
    {
        string diagram = Mermaid
            .ClassDiagram()
            .AddClass("c1", out Class c1, annotation: "foo")
            .AddClass("c2", out Class _, annotation: "bar")
            .AddProperty(c1, "int", "Age")
            .AddMethod(c1, null, "Breathe")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }
}

public class ClassDiagramBuilderNoValidationNoSanitizationOptionsBuildIntegrationTests(MermaidToolingFixture toolingFixture) : IClassFixture<MermaidToolingFixture>
{
    private static readonly MermaidDotNetOptions _options = new() { ValidateInputs = false, SanitizeInputs = false };

    [Fact]
    public async Task CanBuildSimpleClassDiagramWithTitle()
    {
        string diagram = Mermaid
            .ClassDiagram("Simple Class Diagram", options: _options)
            .AddClass("Animal", out Class animal)
            .AddClass("Dog", out Class dog)
            .AddProperty(animal, "int", "Age")
            .AddMethod(animal, null, "Breathe")
            .AddMethod(animal, "void", "Eat", Visibilities.Public | Visibilities.Abstract,
            [
                ("Food", "food")
            ])
            .AddMethod(dog, "Sound", "Bark", parameters: [
                ("int", "times"),
("int", "volume")
            ])
            .AddRelationship(animal, dog, RelationshipType.Inheritance, label: "A dog is an animal")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildSimpleClassDiagramWithoutTitle()
    {
        string diagram = Mermaid
            .ClassDiagram(options: _options)
            .AddClass("Animal", out Class animal)
            .AddClass("Dog", out Class dog)
            .AddProperty(animal, "int", "Age")
            .AddMethod(animal, null, "Breathe")
            .AddMethod(animal, "void", "Eat", Visibilities.Public | Visibilities.Abstract,
                        [
                ("Food", "food")
            ])
            .AddMethod(dog, "Sound", "Bark", parameters: [
                ("int", "times"),
        ("int", "volume")
            ])
            .AddRelationship(animal, dog, RelationshipType.Inheritance, label: "A dog is an animal")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithGenericTypes()
    {
        string diagram = Mermaid
            .ClassDiagram(options: _options)
            .AddClass("c", out Class c)
            .AddProperty(c, "List<List<int>>", "MyList")
            .AddMethod(c, "List<List<int>>", "GetList", parameters: [
                ("List<List<int>>", "list")
            ])
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithAllLinkStyles()
    {
        string diagram = Mermaid
            .ClassDiagram(options: _options)
            .AddClass("c1", out Class c1)
            .AddClass("c2", out Class c2)
            .AddClass("c3", out Class c3)
            .AddClass("c4", out Class c4)
            .AddRelationship(c1, c2, RelationshipType.Inheritance, linkStyle: LinkStyle.Solid)
            .AddRelationship(c3, c4, RelationshipType.Inheritance, linkStyle: LinkStyle.Dashed)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithAllSingleWayRelationships()
    {
        string diagram = Mermaid
            .ClassDiagram(options: _options)
            .AddClass("c1", out Class c1)
            .AddClass("c2", out Class c2)
            .AddClass("c3", out Class c3)
            .AddClass("c4", out Class c4)
            .AddClass("c5", out Class c5)
            .AddClass("c6", out Class c6)
            .AddClass("c7", out Class c7)
            .AddClass("c8", out Class c8)
            .AddClass("c9", out Class c9)
            .AddClass("c10", out Class c10)
            .AddRelationship(c1, c2, RelationshipType.Inheritance)
            .AddRelationship(c3, c4, RelationshipType.Composition)
            .AddRelationship(c5, c6, RelationshipType.Aggregation)
            .AddRelationship(c7, c8, RelationshipType.Association)
            .AddRelationship(c9, c10, RelationshipType.Unspecified)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithAllTwoWayRelationships()
    {
        string diagram = Mermaid
            .ClassDiagram(options: _options)
            .AddClass("c1", out Class c1)
            .AddClass("c2", out Class c2)
            .AddClass("c3", out Class c3)
            .AddClass("c4", out Class c4)
            .AddClass("c5", out Class c5)
            .AddClass("c6", out Class c6)
            .AddClass("c7", out Class c7)
            .AddClass("c8", out Class c8)
            .AddClass("c9", out Class c9)
            .AddClass("c10", out Class c10)
            .AddClass("c11", out Class c11)
            .AddClass("c12", out Class c12)
            .AddClass("c13", out Class c13)
            .AddClass("c14", out Class c14)
            .AddClass("c15", out Class c15)
            .AddClass("c16", out Class c16)
            .AddClass("c17", out Class c17)
            .AddClass("c18", out Class c18)
            .AddClass("c19", out Class c19)
            .AddClass("c20", out Class c20)
            .AddClass("c21", out Class c21)
            .AddClass("c22", out Class c22)
            .AddClass("c23", out Class c23)
            .AddClass("c24", out Class c24)
            .AddClass("c25", out Class c25)
            .AddClass("c26", out Class c26)
            .AddClass("c27", out Class c27)
            .AddClass("c28", out Class c28)
            .AddClass("c29", out Class c29)
            .AddClass("c30", out Class c30)
            .AddRelationship(c1, c2, RelationshipType.Inheritance, toRelationshipType: RelationshipType.Inheritance)
            .AddRelationship(c1, c3, RelationshipType.Inheritance, toRelationshipType: RelationshipType.Composition)
            .AddRelationship(c1, c4, RelationshipType.Inheritance, toRelationshipType: RelationshipType.Aggregation)
            .AddRelationship(c1, c5, RelationshipType.Inheritance, toRelationshipType: RelationshipType.Association)
            .AddRelationship(c1, c6, RelationshipType.Inheritance, toRelationshipType: RelationshipType.Unspecified)
            .AddRelationship(c7, c8, RelationshipType.Composition, toRelationshipType: RelationshipType.Inheritance)
            .AddRelationship(c7, c9, RelationshipType.Composition, toRelationshipType: RelationshipType.Composition)
            .AddRelationship(c7, c10, RelationshipType.Composition, toRelationshipType: RelationshipType.Aggregation)
            .AddRelationship(c7, c11, RelationshipType.Composition, toRelationshipType: RelationshipType.Association)
            .AddRelationship(c7, c12, RelationshipType.Composition, toRelationshipType: RelationshipType.Unspecified)
            .AddRelationship(c13, c14, RelationshipType.Aggregation, toRelationshipType: RelationshipType.Inheritance)
            .AddRelationship(c13, c15, RelationshipType.Aggregation, toRelationshipType: RelationshipType.Composition)
            .AddRelationship(c13, c16, RelationshipType.Aggregation, toRelationshipType: RelationshipType.Aggregation)
            .AddRelationship(c13, c17, RelationshipType.Aggregation, toRelationshipType: RelationshipType.Association)
            .AddRelationship(c13, c18, RelationshipType.Aggregation, toRelationshipType: RelationshipType.Unspecified)
            .AddRelationship(c19, c20, RelationshipType.Association, toRelationshipType: RelationshipType.Inheritance)
            .AddRelationship(c19, c21, RelationshipType.Association, toRelationshipType: RelationshipType.Composition)
            .AddRelationship(c19, c22, RelationshipType.Association, toRelationshipType: RelationshipType.Aggregation)
            .AddRelationship(c19, c23, RelationshipType.Association, toRelationshipType: RelationshipType.Association)
            .AddRelationship(c19, c24, RelationshipType.Association, toRelationshipType: RelationshipType.Unspecified)
            .AddRelationship(c25, c26, RelationshipType.Unspecified, toRelationshipType: RelationshipType.Inheritance)
            .AddRelationship(c25, c27, RelationshipType.Unspecified, toRelationshipType: RelationshipType.Composition)
            .AddRelationship(c25, c28, RelationshipType.Unspecified, toRelationshipType: RelationshipType.Aggregation)
            .AddRelationship(c25, c29, RelationshipType.Unspecified, toRelationshipType: RelationshipType.Association)
            .AddRelationship(c25, c30, RelationshipType.Unspecified, toRelationshipType: RelationshipType.Unspecified)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithAllCardinalities()
    {
        string diagram = Mermaid
            .ClassDiagram(options: _options)
            .AddClass("c1", out Class c1)
            .AddClass("c2", out Class c2)
                    .AddClass("c3", out Class c3)
                    .AddClass("c4", out Class c4)
                    .AddClass("c5", out Class c5)
                    .AddClass("c6", out Class c6)
                    .AddClass("c7", out Class c7)
                    .AddClass("c8", out Class c8)
                    .AddClass("c9", out Class c9)
                    .AddClass("c10", out Class c10)
                    .AddClass("c11", out Class c11)
                    .AddClass("c12", out Class c12)
                    .AddClass("c13", out Class c13)
                    .AddClass("c14", out Class c14)
                    .AddClass("c15", out Class c15)
                    .AddClass("c16", out Class c16)
                    .AddClass("c17", out Class c17)
                    .AddClass("c18", out Class c18)
                    .AddClass("c19", out Class c19)
                    .AddClass("c20", out Class c20)
                    .AddClass("c21", out Class c21)
                    .AddClass("c22", out Class c22)
                    .AddClass("c23", out Class c23)
                    .AddClass("c24", out Class c24)
                    .AddClass("c25", out Class c25)
                    .AddClass("c26", out Class c26)
                    .AddClass("c27", out Class c27)
                    .AddClass("c28", out Class c28)
                    .AddClass("c29", out Class c29)
                    .AddClass("c30", out Class c30)
                    .AddClass("c31", out Class c31)
                    .AddClass("c32", out Class c32)
                    .AddClass("c33", out Class c33)
                    .AddClass("c34", out Class c34)
                    .AddClass("c35", out Class c35)
                    .AddClass("c36", out Class c36)
                    .AddClass("c37", out Class c37)
                    .AddClass("c38", out Class c38)
                    .AddClass("c39", out Class c39)
                    .AddClass("c40", out Class c40)
                    .AddClass("c41", out Class c41)
                    .AddClass("c42", out Class c42)
                    .AddClass("c43", out Class c43)
                    .AddClass("c44", out Class c44)
                    .AddClass("c45", out Class c45)
                    .AddClass("c46", out Class c46)
                    .AddClass("c47", out Class c47)
                    .AddClass("c48", out Class c48)
                    .AddClass("c49", out Class c49)
                    .AddClass("c50", out Class c50)
                    .AddClass("c51", out Class c51)
                    .AddClass("c52", out Class c52)
                    .AddClass("c53", out Class c53)
                    .AddClass("c54", out Class c54)
                    .AddRelationship(c1, c2, RelationshipType.Inheritance, fromCardinality: Cardinality.One)
                    .AddRelationship(c3, c4, RelationshipType.Inheritance, fromCardinality: Cardinality.ZeroOrOne)
                    .AddRelationship(c5, c6, RelationshipType.Inheritance, fromCardinality: Cardinality.ZeroOrMore)
                    .AddRelationship(c7, c8, RelationshipType.Inheritance, fromCardinality: Cardinality.OneOrMore)
                    .AddRelationship(c9, c10, RelationshipType.Inheritance, fromCardinality: Cardinality.Any)
                    .AddRelationship(c11, c12, RelationshipType.Inheritance, fromCardinality: Cardinality.Exactly(42))
                    .AddRelationship(c13, c14, RelationshipType.Inheritance, fromCardinality: Cardinality.Exactly("n"))
                    .AddRelationship(c15, c16, RelationshipType.Inheritance, fromCardinality: Cardinality.Range(42, 99))
                    .AddRelationship(c17, c18, RelationshipType.Inheritance, fromCardinality: Cardinality.Range("a", "b"))
                    .AddRelationship(c19, c20, RelationshipType.Inheritance, toCardinality: Cardinality.One)
                    .AddRelationship(c21, c22, RelationshipType.Inheritance, toCardinality: Cardinality.ZeroOrOne)
                    .AddRelationship(c23, c24, RelationshipType.Inheritance, toCardinality: Cardinality.ZeroOrMore)
                    .AddRelationship(c25, c26, RelationshipType.Inheritance, toCardinality: Cardinality.OneOrMore)
                    .AddRelationship(c27, c28, RelationshipType.Inheritance, toCardinality: Cardinality.Any)
                    .AddRelationship(c29, c30, RelationshipType.Inheritance, toCardinality: Cardinality.Exactly(42))
                    .AddRelationship(c31, c32, RelationshipType.Inheritance, toCardinality: Cardinality.Exactly("n"))
                    .AddRelationship(c33, c34, RelationshipType.Inheritance, toCardinality: Cardinality.Range(42, 99))
                    .AddRelationship(c35, c36, RelationshipType.Inheritance, toCardinality: Cardinality.Range("a", "b"))
                    .AddRelationship(c37, c38, RelationshipType.Inheritance, fromCardinality: Cardinality.One, toCardinality: Cardinality.One)
                    .AddRelationship(c39, c40, RelationshipType.Inheritance, fromCardinality: Cardinality.ZeroOrOne, toCardinality: Cardinality.ZeroOrOne)
                    .AddRelationship(c41, c42, RelationshipType.Inheritance, fromCardinality: Cardinality.ZeroOrMore, toCardinality: Cardinality.ZeroOrMore)
                    .AddRelationship(c43, c44, RelationshipType.Inheritance, fromCardinality: Cardinality.OneOrMore, toCardinality: Cardinality.OneOrMore)
                    .AddRelationship(c45, c46, RelationshipType.Inheritance, fromCardinality: Cardinality.Any, toCardinality: Cardinality.Any)
                    .AddRelationship(c47, c48, RelationshipType.Inheritance, fromCardinality: Cardinality.Exactly(42), toCardinality: Cardinality.Exactly(42))
                    .AddRelationship(c49, c50, RelationshipType.Inheritance, fromCardinality: Cardinality.Exactly("n"), toCardinality: Cardinality.Exactly("n"))
                    .AddRelationship(c51, c52, RelationshipType.Composition, fromCardinality: Cardinality.Range(42, 99), toCardinality: Cardinality.Range(42, 99))
                    .AddRelationship(c53, c54, RelationshipType.Aggregation, fromCardinality: Cardinality.Range("a", "b"), toCardinality: Cardinality.Range("a", "b"))
                    .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramsWithAllDirections()
    {
        string diagram1 = Mermaid
            .ClassDiagram("Top to Bottom", direction: ClassDiagramDirection.TopToBottom, options: _options)
            .AddClass("c1", out Class d1c1)
            .AddClass("c2", out Class d1c2)
            .AddRelationship(d1c1, d1c2, RelationshipType.Inheritance)
            .Build();

        string diagram2 = Mermaid
            .ClassDiagram("Bottom to Top", direction: ClassDiagramDirection.BottomToTop, options: _options)
            .AddClass("c1", out Class d2c1)
            .AddClass("c2", out Class d2c2)
            .AddRelationship(d2c1, d2c2, RelationshipType.Inheritance)
            .Build();

        string diagram3 = Mermaid
            .ClassDiagram("Left to Right", direction: ClassDiagramDirection.LeftToRight, options: _options)
            .AddClass("c1", out Class d3c1)
            .AddClass("c2", out Class d3c2)
            .AddRelationship(d3c1, d3c2, RelationshipType.Inheritance)
            .Build();

        string diagram4 = Mermaid
            .ClassDiagram("Right to Left", direction: ClassDiagramDirection.RightToLeft, options: _options)
            .AddClass("c1", out Class d4c1)
            .AddClass("c2", out Class d4c2)
            .AddRelationship(d4c1, d4c2, RelationshipType.Inheritance)
            .Build();

        var diagram1Result = await toolingFixture.ValidateDiagramAsync(diagram1);

        Assert.True(diagram1Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram1, diagram1Result));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagram1Result));

        var diagram2Result = await toolingFixture.ValidateDiagramAsync(diagram2);

        Assert.True(diagram2Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram2, diagram2Result));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagram2Result));

        var diagram3Result = await toolingFixture.ValidateDiagramAsync(diagram3);

        Assert.True(diagram3Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram3, diagram3Result));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagram3Result));

        var diagram4Result = await toolingFixture.ValidateDiagramAsync(diagram4);

        Assert.True(diagram4Result.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram4, diagram4Result));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagram4Result));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithClickBinding()
    {
        string diagram = Mermaid
            .ClassDiagram(options: _options)
            .AddClass("c1", out Class c1)
            .AddClass("c2", out Class c2)
            .AddClass("c3", out Class c3)
            .AddClass("c4", out Class c4)
            .AddCallback(c1, "callback")
            .AddCallback(c2, "callback", "tooltip")
            .AddHyperlink(c3, "https://example.com")
            .AddHyperlink(c4, "https://example.com", "tooltip")
            .AddRelationship(c1, c2, RelationshipType.Inheritance)
            .AddRelationship(c3, c4, RelationshipType.Inheritance)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithNotes()
    {
        string diagram = Mermaid
            .ClassDiagram(options: _options)
            .AddClass("c1", out Class c1)
            .AddNote("General note")
            .AddNote("Specific note", c1)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithCustomStyle()
    {
        string diagram = Mermaid
            .ClassDiagram(options: _options)
            .AddClass("c1", out Class c1)
            .AddClass("c2", out Class c2)
            .AddClass("c3", out Class c3)
            .StyleWithRawCss(c1, "fill:#f9f,stroke:#333,stroke-width:4px")
            .StyleWithCssClass("styleClass", c2, c3)
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithNamespaces()
    {
        string diagram = Mermaid
            .ClassDiagram(options: _options)
            .AddClass("c1", out Class c1)
            .AddClass("c2", out Class _)
            .AddNamespace("ns1", builder => builder
                .AddClass("c3", out Class c3)
                .AddClass("c4", out Class c4)
                .AddRelationship(c3, c4, RelationshipType.Inheritance))
            .AddClass("c5", out Class _)
            .AddNamespace("ns2", builder => builder
                .AddClass("c6", out Class c6)
                .AddClass("c7", out Class c7)
                .AddRelationship(c6, c7, RelationshipType.Inheritance)
                .AddRelationship(c1, c7, RelationshipType.Inheritance))
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithClassLabels()
    {
        string diagram = Mermaid
            .ClassDiagram(options: _options)
            .AddClass("c1", out Class c1, "Hello World!")
            .AddClass("c2", out Class _, "Hello World!")
            .AddProperty(c1, "int", "Age")
            .AddMethod(c1, null, "Breathe")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }

    [Fact]
    public async Task CanBuildClassDiagramWithClassAnnotations()
    {
        string diagram = Mermaid
            .ClassDiagram(options: _options)
            .AddClass("c1", out Class c1, annotation: "foo")
            .AddClass("c2", out Class _, annotation: "bar")
            .AddProperty(c1, "int", "Age")
            .AddMethod(c1, null, "Breathe")
            .Build();

        var diagramResult = await toolingFixture.ValidateDiagramAsync(diagram);

        Assert.True(diagramResult.ExitCode == 0, toolingFixture.FormatParsingErrorMessage(diagram, diagramResult));
        Assert.Equal("class", toolingFixture.GetDiagramType(diagramResult));
    }
}
