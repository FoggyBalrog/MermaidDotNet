using FoggyBalrog.MermaidDotNet.Configuration;
using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

namespace FoggyBalrog.MermaidDotNet.UnitTests.Configuration;

public class FrontmatterGeneratorTests

{
    [Fact]
    public void Generate_WhenOnlyNullValues_ReturnsEmptyString()
    {
        // Arrange
        var config = new MermaidConfig();

        // Act
        string result = FrontmatterGenerator.Generate(null, config);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void GenerateMermaidConfigFrontmatter_WhenOnlySomeConfigValues_ReturnsFrontmatter()
    {
        // Arrange
        var config = new MermaidConfig
        {
            Theme = Theme.Forest,
            ThemeVariables = new Dictionary<string, string>
            {
                { "primaryColor", "#ff0000" },
                { "secondaryColor", "#00ff00" }
            }
        };

        // Act
        string result = FrontmatterGenerator.Generate(null, config);

        // Assert
        Assert.Equal(@"---
config:
  theme: forest
  themeVariables:
    primaryColor: '#ff0000'
    secondaryColor: '#00ff00'
---
", result, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void GenerateMermaidConfigFrontmatter_WhenOnlyFullConfig_ReturnsFrontmatter()
    {
        // Arrange
        var config = MermaidConfigTestBuilder.Build();

        // Act
        string result = FrontmatterGenerator.Generate(null, config);

        // Assert
        Assert.Equal(@"---
config:
  altFontFamily: Arial
  arrowMarkerAbsolute: true
  class:
    titleTopMargin: 70
    arrowMarkerAbsolute: true
    dividerMargin: 20
    padding: 40
    textHeight: 60
    defaultRenderer: elk
    nodeSpacing: 30
    rankSpacing: 50
    diagramPadding: 10
    htmlLabels: true
    useWidth: 80
    useMaxWidth: true
  darkMode: true
  deterministicIDSeed: seed
  deterministicIds: true
  dompurifyConfig:
    Foo: bar
    Baz: qux
  elk:
    mergeEdges: true
    nodePlacementStrategy: NETWORK_SIMPLEX
    cycleBreakingStrategy: DEPTH_FIRST
  er:
    titleTopMargin: 60
    diagramPadding: 10
    layoutDirection: TB
    minEntityWidth: 50
    minEntityHeight: 40
    entityPadding: 20
    stroke: '#00ff00'
    fill: '#ff0000'
    fontSize: 30
    useWidth: 70
    useMaxWidth: true
  flowchart:
    titleTopMargin: 70
    subGraphTitleMargin:
      top: 60
      bottom: 50
    arrowMarkerAbsolute: true
    diagramPadding: 10
    htmlLabels: true
    nodeSpacing: 20
    rankSpacing: 40
    curve: linear
    padding: 30
    defaultRenderer: elk
    wrappingWidth: 80
    useWidth: 90
    useMaxWidth: true
  fontFamily: Garamond
  fontSize: 20
  forceLegacyMathML: true
  gantt:
    titleTopMargin: 80
    barHeight: 10
    barGap: 20
    topPadding: 90
    rightPadding: 70
    leftPadding: 50
    gridLineStartPadding: 40
    fontSize: 30
    sectionFontSize: 20px
    numberSectionStyles: 60
    axisFormat: YYYY-MM-DD
    tickInterval: 1 day
    topAxis: true
    displayMode: compact
    weekday: wednesday
    useWidth: 100
    useMaxWidth: true
  gitGraph:
    titleTopMargin: 70
    diagramPadding: 10
    nodeLabel:
      width: 40
      height: 30
      x: 50
      y: 60
    mainBranchName: main
    mainBranchOrder: 20
    showCommitLabel: true
    showBranches: true
    rotateCommitLabel: true
    parallelCommits: true
    arrowMarkerAbsolute: true
    useWidth: 80
    useMaxWidth: true
  handDrawnSeed: 42
  htmlLabels: true
  journey:
    diagramMarginX: 50
    diagramMarginY: 60
    leftMargin: 80
    width: 120
    height: 70
    boxMargin: 30
    boxTextMargin: 40
    noteMargin: 100
    messageMargin: 90
    messageAlign: center
    bottomMarginAdj: 20
    rightAngles: true
    taskFontSize: 20px
    taskFontFamily: Arial
    taskMargin: 110
    activationWidth: 10
    textPlacement: top
    actorColours:
      - '#ff0000'
      - '#00ff00'
    sectionFills:
      - '#ff0000'
      - '#00ff00'
    sectionColours:
      - '#ff0000'
      - '#00ff00'
    titleColor: red
    titleFontFamily: Verdana
    titleFontSize: 24px
    useWidth: 130
    useMaxWidth: true
  layout: Layout
  legacyMathML: true
  logLevel: debug
  look: handDrawn
  markdownAutoWrap: true
  maxEdges: 10
  maxTextSize: 30
  mindmap:
    padding: 20
    maxNodeWidth: 10
    useWidth: 30
    useMaxWidth: true
  pie:
    textPosition: 0.42
    useWidth: 10
    useMaxWidth: true
  quadrantChart:
    chartWidth: 20
    chartHeight: 10
    titleFontSize: 110
    titlePadding: 120
    quadrantPadding: 90
    xAxisLabelPadding: 140
    yAxisLabelPadding: 160
    xAxisLabelFontSize: 130
    yAxisLabelFontSize: 150
    quadrantLabelFontSize: 80
    quadrantTextTopPadding: 100
    pointTextPadding: 50
    pointLabelFontSize: 30
    pointRadius: 40
    xAxisPosition: top
    yAxisPosition: right
    quadrantInternalBorderStrokeWidth: 70
    quadrantExternalBorderStrokeWidth: 60
    useWidth: 170
    useMaxWidth: true
  requirement:
    rect_fill: '#00ff00'
    text_color: '#0000ff'
    rect_border_size: 2px
    rect_border_color: '#ff0000'
    rect_min_width: 50
    rect_min_height: 40
    fontSize: 10
    rect_padding: 60
    line_height: 20
    useWidth: 70
    useMaxWidth: true
  secure:
    - foo
    - bar
  securityLevel: strict
  sequence:
    arrowMarkerAbsolute: true
    hideUnusedParticipants: true
    activationWidth: 10
    diagramMarginX: 70
    diagramMarginY: 80
    actorMargin: 30
    width: 140
    height: 90
    boxMargin: 50
    boxTextMargin: 60
    noteMargin: 130
    messageMargin: 120
    messageAlign: center
    mirrorActors: true
    forceMenus: true
    bottomMarginAdj: 40
    rightAngles: true
    showSequenceNumbers: true
    actorFontSize: 20px
    actorFontFamily: Arial
    actorFontWeight: bold
    noteFontSize: 20px
    noteFontFamily: Arial
    noteFontWeight: bold
    noteAlign: center
    messageFontSize: 20px
    messageFontFamily: Arial
    messageFontWeight: bold
    wrap: true
    wrapPadding: 150
    labelBoxWidth: 110
    labelBoxHeight: 100
    useWidth: 160
    useMaxWidth: true
  startOnLoad: true
  state:
    titleTopMargin: 170
    arrowMarkerAbsolute: true
    dividerMargin: 20
    sizeUnit: 140
    padding: 110
    textHeight: 150
    titleShift: 160
    noteMargin: 100
    nodeSpacing: 90
    rankSpacing: 130
    forkWidth: 60
    forkHeight: 50
    miniPadding: 80
    fontSizeFactor: 40
    fontSize: 30
    labelHeight: 70
    edgeLengthFactor: 0.5
    compositTitleSize: 10
    radius: 120
    defaultRenderer: elk
    useWidth: 180
    useMaxWidth: true
  suppressErrorRendering: true
  theme: default
  themeCSS: theme css
  themeVariables:
    primaryColor: '#ff0000'
    secondaryColor: '#00ff00'
  timeline:
    diagramMarginX: 70
    diagramMarginY: 80
    leftMargin: 100
    width: 150
    height: 90
    padding: 130
    boxMargin: 40
    boxTextMargin: 50
    noteMargin: 120
    messageMargin: 110
    messageAlign: center
    bottomMarginAdj: 60
    rightAngles: true
    taskFontSize: 20px
    taskFontFamily: Arial
    taskMargin: 140
    activationWidth: 10
    textPlacement: top
    actorColours:
      - '#ff0000'
      - '#00ff00'
    sectionFills:
      - '#ff0000'
      - '#00ff00'
    sectionColours:
      - '#ff0000'
      - '#00ff00'
    disableMulticolor: true
    useWidth: 160
    useMaxWidth: true
  wrap: true
  xyChart:
    width: 800
    height: 600
    titlePadding: 15
    titleFontSize: 25
    showTitle: true
    xAxis:
      showLabel: true
      labelFontSize: 12
      labelPadding: 4
      showTitle: true
      titleFontSize: 14
      titlePadding: 6
      showTick: true
      tickLength: 6
      tickWidth: 3
      showAxisLine: true
      axisLineWidth: 1
    yAxis:
      showLabel: false
      labelFontSize: 10
      labelPadding: 2
      showTitle: false
      titleFontSize: 12
      titlePadding: 4
      showTick: false
      tickLength: 4
      tickWidth: 1
      showAxisLine: false
      axisLineWidth: 3
    chartOrientation: horizontal
    plotReservedSpacePercent: 60
    showDataLabel: true
    useWidth: 900
    useMaxWidth: false
---
", result, ignoreLineEndingDifferences: true);
    }
}
