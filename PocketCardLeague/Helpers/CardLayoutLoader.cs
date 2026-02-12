using Helpers;
using System;
using System.IO;
using System.Xml.Linq;

namespace PocketCardLeague.Helpers;

// Shared sub-records for layout element positioning
public record ElementLayout(float X, float Y, string Align = "Left");
public record SpacedLayout(float X, float Y, float Spacing, string Align = "Left");
public record AreaLayout(float X, float Y, float AreaWidth, float AreaHeight, string Align = "Left");
public record LabelLayout(float X, float Y, float FontSize, string Align = "Left");

public record PokemonCardLayout(
    int Width, int Height,
    string FrontSprite, string BackSprite,
    AreaLayout PokemonSprite,
    ElementLayout LevelBar,
    SpacedLayout Types,
    SpacedLayout Costs,
    SpacedLayout Glyphs,
    LabelLayout NameLabel,
    LabelLayout DexIdLabel,
    LabelLayout LevelLabel,
    LabelLayout HpLabel,
    LabelLayout AtkLabel,
    LabelLayout DefLabel);

public record BerryCardLayout(
    int Width, int Height,
    string FrontSprite, string BackSprite,
    AreaLayout BerrySprite,
    SpacedLayout BerryDots,
    LabelLayout NameLabel);

public record DeckBoxLayout(
    float FaceCardScale, float FaceCardBobDistance, float FaceCardOffsetY, float FaceCardAreaHeight,
    float NameLabelFontSize, float NameLabelMaxWidth, float NameLabelY,
    float LevelLabelFontSize, float LevelLabelBorderWidth, float LevelLabelX, float LevelLabelY,
    float TypesIconScale, float TypesSpacing, float TypesMarginX, float TypesMarginBottom,
    float CostsDotScale, float CostsSpacing, float CostsGapAboveTypes, float CostsMarginX);

public static class CardLayoutLoader
{
    private static readonly string LayoutPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Content", "Scenes", "Templates", "CardLayouts.xml");

    public static PokemonCardLayout PokemonLayout { get; private set; } = DefaultPokemonLayout;
    public static BerryCardLayout BerryLayout { get; private set; } = DefaultBerryLayout;
    public static DeckBoxLayout DeckBoxLayout { get; private set; } = DefaultDeckBoxLayout;

    private static PokemonCardLayout DefaultPokemonLayout => new(
        57, 80, "CardParts.card_common", "CardParts.back_mon",
        new AreaLayout(3, 7, 51, 40),
        new ElementLayout(3, 2),
        new SpacedLayout(2, 55, 1),
        new SpacedLayout(2, 74, 1),
        new SpacedLayout(2, 55, 1, "Right"),
        new LabelLayout(3, 48, 8),
        new LabelLayout(3, 48, 8, "Right"),
        new LabelLayout(3, 54, 8),
        new LabelLayout(3, 54, 8, "Right"),
        new LabelLayout(3, 60, 8),
        new LabelLayout(3, 60, 8, "Right"));

    private static BerryCardLayout DefaultBerryLayout => new(
        57, 80, "CardParts.card_berry", "CardParts.back_berry",
        new AreaLayout(3, 5, 51, 40),
        new SpacedLayout(3, 50, 1),
        new LabelLayout(3, 58, 8));

    private static DeckBoxLayout DefaultDeckBoxLayout => new(
        4f, 8f, 20f, 200f,
        36f, 228f, 320f,
        32f, 2f, 12f, 8f,
        4f, 4f, 12f, 12f,
        4f, 4f, 8f, 12f);

    public static void Initialize()
    {
        Load();
        HotReloadManager.OnFileChanged += OnFileChanged;
    }

    private static void OnFileChanged(string filePath)
    {
        if (filePath.EndsWith("CardLayouts.xml", StringComparison.OrdinalIgnoreCase))
            Load();
    }

    private static void Load()
    {
        if (!File.Exists(LayoutPath))
        {
            System.Diagnostics.Debug.WriteLine($"CardLayoutLoader: File not found: {LayoutPath}");
            return;
        }

        try
        {
            var xml = File.ReadAllText(LayoutPath);
            var root = XElement.Parse(xml);

            var pokemon = root.Element("PokemonCard");
            if (pokemon is not null)
                PokemonLayout = ParsePokemonLayout(pokemon);

            var berry = root.Element("BerryCard");
            if (berry is not null)
                BerryLayout = ParseBerryLayout(berry);

            var deckBox = root.Element("DeckBox");
            if (deckBox is not null)
                DeckBoxLayout = ParseDeckBoxLayout(deckBox);

            System.Diagnostics.Debug.WriteLine("CardLayoutLoader: Loaded successfully.");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"CardLayoutLoader: Failed to load: {ex.Message}");
        }
    }

    // --- Shared sub-record parsers ---

    private static ElementLayout ParseElementLayout(XElement? el, float defX, float defY, string defAlign = "Left")
    {
        if (el is null) return new(defX, defY, defAlign);
        return new(
            SceneLoader.ParseFloat(el.Attribute("X")?.Value) ?? defX,
            SceneLoader.ParseFloat(el.Attribute("Y")?.Value) ?? defY,
            el.Attribute("Align")?.Value ?? defAlign);
    }

    private static SpacedLayout ParseSpacedLayout(XElement? el, float defX, float defY, float defSpacing, string defAlign = "Left")
    {
        if (el is null) return new(defX, defY, defSpacing, defAlign);
        return new(
            SceneLoader.ParseFloat(el.Attribute("X")?.Value) ?? defX,
            SceneLoader.ParseFloat(el.Attribute("Y")?.Value) ?? defY,
            SceneLoader.ParseFloat(el.Attribute("Spacing")?.Value) ?? defSpacing,
            el.Attribute("Align")?.Value ?? defAlign);
    }

    private static AreaLayout ParseAreaLayout(XElement? el, float defX, float defY, float defW, float defH, string defAlign = "Left")
    {
        if (el is null) return new(defX, defY, defW, defH, defAlign);
        return new(
            SceneLoader.ParseFloat(el.Attribute("X")?.Value) ?? defX,
            SceneLoader.ParseFloat(el.Attribute("Y")?.Value) ?? defY,
            SceneLoader.ParseFloat(el.Attribute("AreaWidth")?.Value) ?? defW,
            SceneLoader.ParseFloat(el.Attribute("AreaHeight")?.Value) ?? defH,
            el.Attribute("Align")?.Value ?? defAlign);
    }

    private static LabelLayout ParseLabelLayout(XElement? el, float defX, float defY, float defFontSize, string defAlign = "Left")
    {
        if (el is null) return new(defX, defY, defFontSize, defAlign);
        return new(
            SceneLoader.ParseFloat(el.Attribute("X")?.Value) ?? defX,
            SceneLoader.ParseFloat(el.Attribute("Y")?.Value) ?? defY,
            SceneLoader.ParseFloat(el.Attribute("FontSize")?.Value) ?? defFontSize,
            el.Attribute("Align")?.Value ?? defAlign);
    }

    // --- Card layout parsers ---

    private static PokemonCardLayout ParsePokemonLayout(XElement el)
    {
        return new PokemonCardLayout(
            Width: SceneLoader.ParseInt(el.Attribute("Width")?.Value) ?? 57,
            Height: SceneLoader.ParseInt(el.Attribute("Height")?.Value) ?? 80,
            FrontSprite: el.Attribute("FrontSprite")?.Value ?? "CardParts.card_common",
            BackSprite: el.Attribute("BackSprite")?.Value ?? "CardParts.back_mon",
            PokemonSprite: ParseAreaLayout(el.Element("PokemonSprite"), 3, 7, 51, 40),
            LevelBar: ParseElementLayout(el.Element("LevelBar"), 3, 2),
            Types: ParseSpacedLayout(el.Element("Types"), 2, 55, 1),
            Costs: ParseSpacedLayout(el.Element("Costs"), 2, 74, 1),
            Glyphs: ParseSpacedLayout(el.Element("Glyphs"), 2, 55, 1, "Right"),
            NameLabel: ParseLabelLayout(el.Element("NameLabel"), 3, 48, 8),
            DexIdLabel: ParseLabelLayout(el.Element("DexIdLabel"), 3, 48, 8, "Right"),
            LevelLabel: ParseLabelLayout(el.Element("LevelLabel"), 3, 54, 8),
            HpLabel: ParseLabelLayout(el.Element("HpLabel"), 3, 54, 8, "Right"),
            AtkLabel: ParseLabelLayout(el.Element("AtkLabel"), 3, 60, 8),
            DefLabel: ParseLabelLayout(el.Element("DefLabel"), 3, 60, 8, "Right"));
    }

    private static BerryCardLayout ParseBerryLayout(XElement el)
    {
        return new BerryCardLayout(
            Width: SceneLoader.ParseInt(el.Attribute("Width")?.Value) ?? 57,
            Height: SceneLoader.ParseInt(el.Attribute("Height")?.Value) ?? 80,
            FrontSprite: el.Attribute("FrontSprite")?.Value ?? "CardParts.card_berry",
            BackSprite: el.Attribute("BackSprite")?.Value ?? "CardParts.back_berry",
            BerrySprite: ParseAreaLayout(el.Element("BerrySprite"), 3, 5, 51, 40),
            BerryDots: ParseSpacedLayout(el.Element("BerryDots"), 3, 50, 1),
            NameLabel: ParseLabelLayout(el.Element("NameLabel"), 3, 58, 8));
    }

    private static DeckBoxLayout ParseDeckBoxLayout(XElement el)
    {
        var face = el.Element("FaceCard");
        var name = el.Element("NameLabel");
        var level = el.Element("LevelLabel");
        var types = el.Element("Types");
        var costs = el.Element("Costs");

        return new DeckBoxLayout(
            FaceCardScale: SceneLoader.ParseFloat(face?.Attribute("SpriteScale")?.Value) ?? 4f,
            FaceCardBobDistance: SceneLoader.ParseFloat(face?.Attribute("BobDistance")?.Value) ?? 8f,
            FaceCardOffsetY: SceneLoader.ParseFloat(face?.Attribute("OffsetY")?.Value) ?? 20f,
            FaceCardAreaHeight: SceneLoader.ParseFloat(face?.Attribute("AreaHeight")?.Value) ?? 200f,
            NameLabelFontSize: SceneLoader.ParseFloat(name?.Attribute("FontSize")?.Value) ?? 36f,
            NameLabelMaxWidth: SceneLoader.ParseFloat(name?.Attribute("MaxWidth")?.Value) ?? 228f,
            NameLabelY: SceneLoader.ParseFloat(name?.Attribute("Y")?.Value) ?? 320f,
            LevelLabelFontSize: SceneLoader.ParseFloat(level?.Attribute("FontSize")?.Value) ?? 32f,
            LevelLabelBorderWidth: SceneLoader.ParseFloat(level?.Attribute("BorderWidth")?.Value) ?? 2f,
            LevelLabelX: SceneLoader.ParseFloat(level?.Attribute("X")?.Value) ?? 12f,
            LevelLabelY: SceneLoader.ParseFloat(level?.Attribute("Y")?.Value) ?? 8f,
            TypesIconScale: SceneLoader.ParseFloat(types?.Attribute("IconScale")?.Value) ?? 4f,
            TypesSpacing: SceneLoader.ParseFloat(types?.Attribute("Spacing")?.Value) ?? 4f,
            TypesMarginX: SceneLoader.ParseFloat(types?.Attribute("MarginX")?.Value) ?? 12f,
            TypesMarginBottom: SceneLoader.ParseFloat(types?.Attribute("MarginBottom")?.Value) ?? 12f,
            CostsDotScale: SceneLoader.ParseFloat(costs?.Attribute("DotScale")?.Value) ?? 4f,
            CostsSpacing: SceneLoader.ParseFloat(costs?.Attribute("Spacing")?.Value) ?? 4f,
            CostsGapAboveTypes: SceneLoader.ParseFloat(costs?.Attribute("GapAboveTypes")?.Value) ?? 8f,
            CostsMarginX: SceneLoader.ParseFloat(costs?.Attribute("MarginX")?.Value) ?? 12f
        );
    }
}
