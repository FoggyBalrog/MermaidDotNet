using FoggyBalrog.MermaidDotNet.ClassDiagram.Model;

namespace FoggyBalrog.MermaidDotNet.UnitTests.ClassDiagram;

public class ClassDiagramBuilderUnsafeModeBuildTests
{
    [Fact]
    public void CanBuildSimpleClassDiagramWithTitle()
    {
        string diagram = Mermaid
            .Unsafe
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

        Assert.Equal(@"---
title: Simple Class Diagram
---
classDiagram
    class Animal {
        +int Age
        +Breathe()
        +Eat(Food food)* void
    }
    class Dog {
        +Bark(int times, int volume) Sound
    }
    Animal <|-- Dog : A dog is an animal", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildSimpleClassDiagramWithoutTitle()
    {
        string diagram = Mermaid
            .Unsafe
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

        Assert.Equal(@"classDiagram
    class Animal {
        +int Age
        +Breathe()
        +Eat(Food food)* void
    }
    class Dog {
        +Bark(int times, int volume) Sound
    }
    Animal <|-- Dog : A dog is an animal", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildClassDiagramWithGenericTypes()
    {
        string diagram = Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("c", out Class c)
            .AddProperty(c, "List<List<int>>", "MyList")
            .AddMethod(c, "List<List<int>>", "GetList", parameters: [
                ("List<List<int>>", "list")
            ])
            .Build();

        Assert.Equal(@"classDiagram
    class c {
        +List~List~int~~ MyList
        +GetList(List~List~int~~ list) List~List~int~~
    }", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildClassDiagramWithAllLinkStyles()
    {
        string diagram = Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("c1", out Class c1)
            .AddClass("c2", out Class c2)
            .AddClass("c3", out Class c3)
            .AddClass("c4", out Class c4)
            .AddRelationship(c1, c2, RelationshipType.Inheritance, linkStyle: LinkStyle.Solid)
            .AddRelationship(c3, c4, RelationshipType.Inheritance, linkStyle: LinkStyle.Dashed)
            .Build();

        Assert.Equal(@"classDiagram
    c1 <|-- c2
    c3 <|.. c4", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildClassDiagramWithAllSingleWayRelationships()
    {
        string diagram = Mermaid
            .Unsafe
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

        Assert.Equal(@"classDiagram
    c1 <|-- c2
    c3 *-- c4
    c5 o-- c6
    c7 <-- c8
    c9 -- c10", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildClassDiagramWithAllTwoWayRelationships()
    {
        string diagram = Mermaid
            .Unsafe
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

        Assert.Equal(@"classDiagram
    c1 <|--|> c2
    c1 <|--* c3
    c1 <|--o c4
    c1 <|--> c5
    c1 <|-- c6
    c7 *--|> c8
    c7 *--* c9
    c7 *--o c10
    c7 *--> c11
    c7 *-- c12
    c13 o--|> c14
    c13 o--* c15
    c13 o--o c16
    c13 o--> c17
    c13 o-- c18
    c19 <--|> c20
    c19 <--* c21
    c19 <--o c22
    c19 <--> c23
    c19 <-- c24
    c25 --|> c26
    c25 --* c27
    c25 --o c28
    c25 --> c29
    c25 -- c30", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildClassDiagramWithAllCardinalities()
    {
        string diagram = Mermaid
            .Unsafe
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

        Assert.Equal(@"classDiagram
    c1 ""1"" <|-- c2
    c3 ""0..1"" <|-- c4
    c5 ""0..*"" <|-- c6
    c7 ""1..*"" <|-- c8
    c9 ""*"" <|-- c10
    c11 ""42"" <|-- c12
    c13 ""n"" <|-- c14
    c15 ""42..99"" <|-- c16
    c17 ""a..b"" <|-- c18
    c19 <|--""1""  c20
    c21 <|--""0..1""  c22
    c23 <|--""0..*""  c24
    c25 <|--""1..*""  c26
    c27 <|--""*""  c28
    c29 <|--""42""  c30
    c31 <|--""n""  c32
    c33 <|--""42..99""  c34
    c35 <|--""a..b""  c36
    c37 ""1"" <|--""1""  c38
    c39 ""0..1"" <|--""0..1""  c40
    c41 ""0..*"" <|--""0..*""  c42
    c43 ""1..*"" <|--""1..*""  c44
    c45 ""*"" <|--""*""  c46
    c47 ""42"" <|--""42""  c48
    c49 ""n"" <|--""n""  c50
    c51 ""42..99"" *--""42..99""  c52
    c53 ""a..b"" o--""a..b""  c54", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildClassDiagramsWithAllDirections()
    {
        string diagram1 = Mermaid
            .Unsafe
            .ClassDiagram("Top to Bottom", ClassDiagramDirection.TopToBottom)
            .AddClass("c1", out Class d1c1)
            .AddClass("c2", out Class d1c2)
            .AddRelationship(d1c1, d1c2, RelationshipType.Inheritance)
            .Build();

        string diagram2 = Mermaid
            .Unsafe
            .ClassDiagram("Bottom to Top", ClassDiagramDirection.BottomToTop)
            .AddClass("c1", out Class d2c1)
            .AddClass("c2", out Class d2c2)
            .AddRelationship(d2c1, d2c2, RelationshipType.Inheritance)
            .Build();

        string diagram3 = Mermaid
            .Unsafe
            .ClassDiagram("Left to Right", ClassDiagramDirection.LeftToRight)
            .AddClass("c1", out Class d3c1)
            .AddClass("c2", out Class d3c2)
            .AddRelationship(d3c1, d3c2, RelationshipType.Inheritance)
            .Build();

        string diagram4 = Mermaid
            .Unsafe
            .ClassDiagram("Right to Left", ClassDiagramDirection.RightToLeft)
            .AddClass("c1", out Class d4c1)
            .AddClass("c2", out Class d4c2)
            .AddRelationship(d4c1, d4c2, RelationshipType.Inheritance)
            .Build();

        Assert.Equal(@"---
title: Top to Bottom
---
classDiagram
    direction TB
    c1 <|-- c2", diagram1, ignoreLineEndingDifferences: true);
        Assert.Equal(@"---
title: Bottom to Top
---
classDiagram
    direction BT
    c1 <|-- c2", diagram2, ignoreLineEndingDifferences: true);
        Assert.Equal(@"---
title: Left to Right
---
classDiagram
    direction LR
    c1 <|-- c2", diagram3, ignoreLineEndingDifferences: true);
        Assert.Equal(@"---
title: Right to Left
---
classDiagram
    direction RL
    c1 <|-- c2", diagram4, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildClassDiagramWithClickBinding()
    {
        string diagram = Mermaid
            .Unsafe
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

        Assert.Equal(@"classDiagram
    c1 <|-- c2
    c3 <|-- c4
    click c1 call callback()
    click c2 call callback() ""tooltip""
    click c3 href ""https://example.com""
    click c4 href ""https://example.com"" ""tooltip""", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildClassDiagramWithNotes()
    {
        string diagram = Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("c1", out Class c1)
            .AddNote("General note")
            .AddNote("Specific note", c1)
            .Build();

        Assert.Equal(@"classDiagram
    note ""General note""
    note for c1 ""Specific note""
    class c1", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildClassDiagramWithCustomStyle()
    {
        string diagram = Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("c1", out Class c1)
            .AddClass("c2", out Class c2)
            .AddClass("c3", out Class c3)
            .StyleWithRawCss(c1, "fill:#f9f,stroke:#333,stroke-width:4px")
            .StyleWithCssClass("styleClass", c2, c3)
            .Build();

        Assert.Equal(@"classDiagram
    class c1
    class c2
    class c3
    style c1 fill:#f9f,stroke:#333,stroke-width:4px
    cssClass ""c2,c3"" styleClass", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildClassDiagramWithNamespaces()
    {
        string diagram = Mermaid
            .Unsafe
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

        Assert.Equal(@"classDiagram
    class c2
    namespace ns1 {
        class c3
        class c4
    }
    class c5
    namespace ns2 {
        class c6
        class c7
    }
    c3 <|-- c4
    c6 <|-- c7
    c1 <|-- c7", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildClassDiagramWithClassLabels()
    {
        string diagram = Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("c1", out Class c1, "Hello World!")
            .AddClass("c2", out Class _, "Hello World!")
            .AddProperty(c1, "int", "Age")
            .AddMethod(c1, null, "Breathe")
            .Build();

        Assert.Equal(@"classDiagram
    class c1[""Hello World!""] {
        +int Age
        +Breathe()
    }
    class c2[""Hello World!""]", diagram, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void CanBuildClassDiagramWithClassAnnotations()
    {
        string diagram = Mermaid
            .Unsafe
            .ClassDiagram()
            .AddClass("c1", out Class c1, annotation: "foo")
            .AddClass("c2", out Class _, annotation: "bar")
            .AddProperty(c1, "int", "Age")
            .AddMethod(c1, null, "Breathe")
            .Build();

        Assert.Equal(@"classDiagram
    class c1 {
        <<foo>>
        +int Age
        +Breathe()
    }
    class c2 {
        <<bar>>
    }", diagram, ignoreLineEndingDifferences: true);
    }
}
