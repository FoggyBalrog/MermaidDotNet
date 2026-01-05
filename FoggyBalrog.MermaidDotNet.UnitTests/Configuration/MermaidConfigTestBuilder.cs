using FoggyBalrog.MermaidDotNet.Configuration.Model;
using FoggyBalrog.MermaidDotNet.Configuration.Model.Enums;

namespace FoggyBalrog.MermaidDotNet.UnitTests.Configuration;

internal static class MermaidConfigTestBuilder
{
    public static MermaidConfig Build()
    {
        return new MermaidConfig
        {
            AltFontFamily = "Arial",
            ArrowMarkerAbsolute = true,
            Class = BuildClassDiagramConfig(),
            DarkMode = true,
            DeterministicIds = true,
            DeterministicIDSeed = "seed",
            DomPurifyConfig = new
            {
                Foo = "bar",
                Baz = "qux"
            },
            Elk = BuildElkConfig(),
            Er = BuildErDiagramConfig(),
            Flowchart = BuildFlowchartDiagramConfig(),
            FontFamily = "Garamond",
            FontSize = 20,
            ForceLegacyMathML = true,
            Gantt = BuildGanttDiagramConfig(),
            Git = BuildGitGraphDiagramConfig(),
            HandDrawnSeed = 42,
            HtmlLabels = true,
            Journey = BuildJourneyDiagramConfig(),
            Layout = "Layout",
            LegacyMathML = true,
            LogLevel = LogLevel.Debug,
            Look = Look.HandDrawn,
            MarkdownAutoWrap = true,
            MaxEdges = 10,
            MaxTextSize = 30,
            Mindmap = BuildMindmapDiagramConfig(),
            Pie = BuildPieDiagramConfig(),
            QuadrantChart = BuildQuadrantChartConfig(),
            Requirement = BuildRequirementDiagramConfig(),
            Sankey = BuildSankeyDiagramConfig(),
            Secure = ["foo", "bar"],
            SecurityLevel = SecurityLevel.Strict,
            Sequence = BuildSequenceDiagramConfig(),
            StartOnLoad = true,
            State = BuildStateDiagramConfig(),
            SuppressErrorRendering = true,
            Theme = Theme.Default,
            ThemeCSS = "theme css",
            ThemeVariables = new Dictionary<string, string>
            {
                { "primaryColor", "#ff0000" },
                { "secondaryColor", "#00ff00" }
            },
            Timeline = BuildTimelineDiagramConfig(),
            Wrap = true
        };
    }


    public static ClassDiagramConfig BuildClassDiagramConfig()
    {
        return new ClassDiagramConfig
        {
            ArrowMarkerAbsolute = true,
            DefaultRenderer = Rendered.Elk,
            DiagramPadding = 10,
            DividerMargin = 20,
            HtmlLabels = true,
            NodeSpacing = 30,
            Padding = 40,
            RankSpacing = 50,
            TextHeight = 60,
            TitleTopMargin = 70,

            // Properties from BaseDiagramConfig
            UseMaxWidth = true,
            UseWidth = 80
        };
    }

    public static ElkConfig BuildElkConfig()
    {
        return new ElkConfig
        {
            CycleBreakingStrategy = ElkCycleBreakingStrategy.DepthFirst,
            MergeEdges = true,
            NodePlacementStrategy = ElkNodePlacementStrategy.NetworkSimplex
        };
    }

    public static ErDiagramConfig BuildErDiagramConfig()
    {
        return new ErDiagramConfig
        {
            DiagramPadding = 10,
            EntityPadding = 20,
            Fill = "#ff0000",
            FontSize = 30,
            LayoutDirection = LayoutDirection.TopBottom,
            MinEntityHeight = 40,
            MinEntityWidth = 50,
            Stroke = "#00ff00",
            TitleTopMargin = 60,

            // Properties from BaseDiagramConfig
            UseMaxWidth = true,
            UseWidth = 70
        };
    }

    public static FlowchartDiagramConfig BuildFlowchartDiagramConfig()
    {
        return new FlowchartDiagramConfig
        {
            ArrowMarkerAbsolute = true,
            Curve = FlowchartDiagramCurve.Linear,
            DefaultRenderer = Rendered.Elk,
            DiagramPadding = 10,
            HtmlLabels = true,
            NodeSpacing = 20,
            Padding = 30,
            RankSpacing = 40,
            SubGraphTitleMargin = new()
            {
                Bottom = 50,
                Top = 60
            },
            TitleTopMargin = 70,
            WrappingWidth = 80,

            // Properties from BaseDiagramConfig
            UseMaxWidth = true,
            UseWidth = 90
        };
    }

    public static GanttDiagramConfig BuildGanttDiagramConfig()
    {
        return new GanttDiagramConfig
        {
            AxisFormat = "YYYY-MM-DD",
            BarHeight = 10,
            BarGap = 20,
            DisplayMode = DisplayMode.Compact,
            FontSize = 30,
            GridLineStartPadding = 40,
            LeftPadding = 50,
            NumberSectionStyles = 60,
            RightPadding = 70,
            SectionFontSize = "20px",
            TickInterval = "1 day",
            TitleTopMargin = 80,
            TopAxis = true,
            TopPadding = 90,
            Weekday = Weekday.Wednesday,

            // Properties from BaseDiagramConfig
            UseMaxWidth = true,
            UseWidth = 100
        };
    }

    public static GitGraphDiagramConfig BuildGitGraphDiagramConfig()
    {
        return new GitGraphDiagramConfig
        {
            ArrowMarkerAbsolute = true,
            DiagramPadding = 10,
            MainBranchName = "main",
            MainBranchOrder = 20,
            NodeLabel = new()
            {
                Height = 30,
                Width = 40,
                X = 50,
                Y = 60
            },
            ParallelCommits = true,
            RotateCommitLabel = true,
            ShowBranches = true,
            ShowCommitLabel = true,
            TitleTopMargin = 70,

            // Properties from BaseDiagramConfig
            UseMaxWidth = true,
            UseWidth = 80
        };
    }

    public static JourneyDiagramConfig BuildJourneyDiagramConfig()
    {
        return new JourneyDiagramConfig
        {
            ActivationWidth = 10,
            ActorColours = ["#ff0000", "#00ff00"],
            BottomMarginAdj = 20,
            BoxMargin = 30,
            BoxTextMargin = 40,
            DiagramMarginX = 50,
            DiagramMarginY = 60,
            Height = 70,
            LeftMargin = 80,
            MessageMargin = 90,
            MessageAlign = MessageAlign.Center,
            NoteMargin = 100,
            RightAngles = true,
            SectionColours = ["#ff0000", "#00ff00"],
            SectionFills = ["#ff0000", "#00ff00"],
            TaskFontFamily = "Arial",
            TaskFontSize = "20px",
            TaskMargin = 110,
            TextPlacement = "top",
            TitleColor = "red",
            TitleFontFamily = "Verdana",
            TitleFontSize = "24px",
            Width = 120,

            // Properties from BaseDiagramConfig
            UseMaxWidth = true,
            UseWidth = 130
        };
    }

    public static MindmapDiagramConfig BuildMindmapDiagramConfig()
    {
        return new MindmapDiagramConfig
        {
            MaxNodeWidth = 10,
            Padding = 20,

            // Properties from BaseDiagramConfig
            UseMaxWidth = true,
            UseWidth = 30
        };
    }

    public static PieDiagramConfig BuildPieDiagramConfig()
    {
        return new PieDiagramConfig
        {
            TextPosition = 0.42,

            // Properties from BaseDiagramConfig
            UseMaxWidth = true,
            UseWidth = 10
        };
    }

    public static QuadrantChartConfig BuildQuadrantChartConfig()
    {
        return new QuadrantChartConfig
        {
            ChartHeight = 10,
            ChartWidth = 20,
            PointLabelFontSize = 30,
            PointRadius = 40,
            PointTextPadding = 50,
            QuadrantExternalBorderStrokeWidth = 60,
            QuadrantInternalBorderStrokeWidth = 70,
            QuadrantLabelFontSize = 80,
            QuadrantPadding = 90,
            QuadrantTextTopPadding = 100,
            TitleFontSize = 110,
            TitlePadding = 120,
            XAxisLabelFontSize = 130,
            XAxisLabelPadding = 140,
            XAxisPosition = XAxisPosition.Top,
            YAxisLabelFontSize = 150,
            YAxisLabelPadding = 160,
            YAxisPosition = YAxisPosition.Right,

            // Properties from BaseDiagramConfig
            UseMaxWidth = true,
            UseWidth = 170
        };
    }

    public static RequirementDiagramConfig BuildRequirementDiagramConfig()
    {
        return new RequirementDiagramConfig
        {
            FontSize = 10,
            LineHeight = 20,
            RectBorderColor = "#ff0000",
            RectBorderSize = "2px",
            RectFill = "#00ff00",
            RectMinHeight = 40,
            RectMinWidth = 50,
            RectPadding = 60,
            TextColor = "#0000ff",

            // Properties from BaseDiagramConfig
            UseMaxWidth = true,
            UseWidth = 70
        };
    }

    public static SankeyDiagramConfig BuildSankeyDiagramConfig()
    {
        return new SankeyDiagramConfig
        {
            Height = 10,
            LinkColor = "#ff0000",
            NodeAlignment = "justify",
            Width = 20,

            // Properties from BaseDiagramConfig
            UseMaxWidth = true,
            UseWidth = 30
        };
    }

    public static SequenceDiagramConfig BuildSequenceDiagramConfig()
    {
        return new SequenceDiagramConfig
        {
            ActivationWidth = 10,
            ActorFontFamily = "Arial",
            ActorFontSize = "20px",
            ActorFontWeight = "bold",
            ActorMargin = 30,
            ArrowMarkerAbsolute = true,
            BottomMarginAdj = 40,
            BoxMargin = 50,
            BoxTextMargin = 60,
            DiagramMarginX = 70,
            DiagramMarginY = 80,
            ForceMenus = true,
            Height = 90,
            HideUnusedParticipants = true,
            LabelBoxHeight = 100,
            LabelBoxWidth = 110,
            MessageAlign = MessageAlign.Center,
            MessageFontFamily = "Arial",
            MessageFontSize = "20px",
            MessageFontWeight = "bold",
            MessageMargin = 120,
            MirrorActors = true,
            NoteAlign = NoteAlign.Center,
            NoteFontFamily = "Arial",
            NoteFontSize = "20px",
            NoteFontWeight = "bold",
            NoteMargin = 130,
            RightAngles = true,
            ShowSequenceNumbers = true,
            Width = 140,
            Wrap = true,
            WrapPadding = 150,

            // Properties from BaseDiagramConfig
            UseMaxWidth = true,
            UseWidth = 160
        };
    }

    public static StateDiagramConfig BuildStateDiagramConfig()
    {
        return new StateDiagramConfig
        {
            ArrowMarkerAbsolute = true,
            CompositTitleSize = 10,
            DefaultRenderer = Rendered.Elk,
            DividerMargin = 20,
            EdgeLengthFactor = "0.5",
            FontSize = 30,
            FontSizeFactor = 40,
            ForkHeight = 50,
            ForkWidth = 60,
            LabelHeight = 70,
            MiniPadding = 80,
            NodeSpacing = 90,
            NoteMargin = 100,
            Padding = 110,
            Radius = 120,
            RankSpacing = 130,
            SizeUnit = 140,
            TextHeight = 150,
            TitleShift = 160,
            TitleTopMargin = 170,

            // Properties from BaseDiagramConfig
            UseMaxWidth = true,
            UseWidth = 180
        };
    }

    public static TimelineDiagramConfig BuildTimelineDiagramConfig()
    {
        return new TimelineDiagramConfig
        {
            ActivationWidth = 10,
            ActorColours = ["#ff0000", "#00ff00"],
            BoxMargin = 40,
            BoxTextMargin = 50,
            BottomMarginAdj = 60,
            DiagramMarginX = 70,
            DiagramMarginY = 80,
            DisableMulticolor = true,
            Height = 90,
            LeftMargin = 100,
            MessageAlign = MessageAlign.Center,
            MessageMargin = 110,
            NoteMargin = 120,
            Padding = 130,
            RightAngles = true,
            SectionColours = ["#ff0000", "#00ff00"],
            SectionFills = ["#ff0000", "#00ff00"],
            TaskFontSize = "20px",
            TaskFontFamily = "Arial",
            TaskMargin = 140,
            TextPlacement = "top",
            Width = 150,

            // Properties from BaseDiagramConfig
            UseMaxWidth = true,
            UseWidth = 160
        };
    }
}