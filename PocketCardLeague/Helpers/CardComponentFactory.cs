using Components;
using Helpers;
using Microsoft.Xna.Framework;
using PocketCardLeague.Components;
using PocketCardLeague.Consts;
using PocketCardLeague.Enums;
using System;
using System.Linq;
using System.Xml.Linq;

namespace PocketCardLeague.Helpers;

public static class CardComponentFactory
{
    public static void Register()
    {
        SceneLoader.RegisterComponentFactory("PokemonCard", CreatePokemonCard);
        SceneLoader.RegisterComponentFactory("BerryCard", CreateBerryCard);
        SceneLoader.RegisterComponentFactory("Deck", CreateDeck);
        SceneLoader.RegisterComponentFactory("Popup", CreatePopup);
        SceneLoader.RegisterComponentFactory("CardPageBrowser", CreateCardPageBrowser);
        SceneLoader.RegisterComponentFactory("DeckPile", CreateDeckPile);
    }

    private static BaseComponent? CreateCardPageBrowser(XElement element, IScene scene)
    {
        var browser = new CardPageBrowser();

        // Panel properties
        var background = SceneLoader.ParseColor(element.Attribute("Background")?.Value);
        if (background.HasValue)
            browser.Background = background.Value;

        var border = SceneLoader.ParseBorder(element);
        if (border is not null)
            browser.Border = border;

        // Grid layout
        var columns = SceneLoader.ParseInt(element.Attribute("Columns")?.Value);
        if (columns.HasValue)
            browser.Columns = columns.Value;

        var rows = SceneLoader.ParseInt(element.Attribute("Rows")?.Value);
        if (rows.HasValue)
            browser.Rows = rows.Value;

        var cardScale = SceneLoader.ParseVector2(element.Attribute("CardScale")?.Value);
        if (cardScale.HasValue)
            browser.CardScale = cardScale.Value;

        var cardSpacing = SceneLoader.ParseVector2(element.Attribute("CardSpacing")?.Value);
        if (cardSpacing.HasValue)
            browser.CardSpacing = cardSpacing.Value;

        // Navigation layout
        var navGap = SceneLoader.ParseFloat(element.Attribute("NavGap")?.Value);
        if (navGap.HasValue)
            browser.NavGap = navGap.Value;

        var arrowScale = SceneLoader.ParseVector2(element.Attribute("ArrowScale")?.Value);
        if (arrowScale.HasValue)
            browser.ArrowScale = arrowScale.Value;

        var arrowOpacity = SceneLoader.ParseFloat(element.Attribute("ArrowOpacity")?.Value);
        if (arrowOpacity.HasValue)
            browser.ArrowOpacity = arrowOpacity.Value;

        var sliderOffset = SceneLoader.ParseVector2(element.Attribute("SliderOffset")?.Value);
        if (sliderOffset.HasValue)
            browser.SliderOffset = sliderOffset.Value;

        var sliderSize = SceneLoader.ParseVector2(element.Attribute("SliderSize")?.Value);
        if (sliderSize.HasValue)
            browser.SliderSize = sliderSize.Value;

        // Slider appearance
        var sliderTrackColor = SceneLoader.ParseColor(element.Attribute("SliderTrackColor")?.Value);
        if (sliderTrackColor.HasValue)
            browser.SliderTrackColor = sliderTrackColor.Value;

        var sliderTrackFillColor = SceneLoader.ParseColor(element.Attribute("SliderTrackFillColor")?.Value);
        if (sliderTrackFillColor.HasValue)
            browser.SliderTrackFillColor = sliderTrackFillColor.Value;

        var sliderThumbColor = SceneLoader.ParseColor(element.Attribute("SliderThumbColor")?.Value);
        if (sliderThumbColor.HasValue)
            browser.SliderThumbColor = sliderThumbColor.Value;

        var sliderThumbHoveredColor = SceneLoader.ParseColor(element.Attribute("SliderThumbHoveredColor")?.Value);
        if (sliderThumbHoveredColor.HasValue)
            browser.SliderThumbHoveredColor = sliderThumbHoveredColor.Value;

        var sliderThumbSize = SceneLoader.ParseVector2(element.Attribute("SliderThumbSize")?.Value);
        if (sliderThumbSize.HasValue)
            browser.SliderThumbSize = sliderThumbSize.Value;

        // Page label
        var pageLabelFont = element.Attribute("PageLabelFont")?.Value;
        if (!string.IsNullOrEmpty(pageLabelFont))
            browser.PageLabelFont = pageLabelFont;

        var pageLabelFontSize = SceneLoader.ParseInt(element.Attribute("PageLabelFontSize")?.Value);
        if (pageLabelFontSize.HasValue)
            browser.PageLabelFontSize = pageLabelFontSize.Value;

        var pageLabelColor = SceneLoader.ParseColor(element.Attribute("PageLabelColor")?.Value);
        if (pageLabelColor.HasValue)
            browser.PageLabelColor = pageLabelColor.Value;

        var pageLabelBorder = SceneLoader.ParseBorder(element, "PageLabelBorder");
        if (pageLabelBorder is not null)
            browser.PageLabelBorder = pageLabelBorder;

        // Variant label
        var variantLabelFont = element.Attribute("VariantLabelFont")?.Value;
        if (!string.IsNullOrEmpty(variantLabelFont))
            browser.VariantLabelFont = variantLabelFont;

        var variantLabelFontSize = SceneLoader.ParseInt(element.Attribute("VariantLabelFontSize")?.Value);
        if (variantLabelFontSize.HasValue)
            browser.VariantLabelFontSize = variantLabelFontSize.Value;

        var variantLabelColor = SceneLoader.ParseColor(element.Attribute("VariantLabelColor")?.Value);
        if (variantLabelColor.HasValue)
            browser.VariantLabelColor = variantLabelColor.Value;

        var variantLabelBorder = SceneLoader.ParseBorder(element, "VariantLabelBorder");
        if (variantLabelBorder is not null)
            browser.VariantLabelBorder = variantLabelBorder;

        // Variant buttons
        var variantBtnColor = SceneLoader.ParseColor(element.Attribute("VariantBtnColor")?.Value);
        if (variantBtnColor.HasValue)
            browser.VariantBtnColor = variantBtnColor.Value;

        var variantBtnBackground = SceneLoader.ParseColor(element.Attribute("VariantBtnBackground")?.Value);
        if (variantBtnBackground.HasValue)
            browser.VariantBtnBackground = variantBtnBackground.Value;

        var variantBtnSize = SceneLoader.ParseVector2(element.Attribute("VariantBtnSize")?.Value);
        if (variantBtnSize.HasValue)
            browser.VariantBtnSize = variantBtnSize.Value;

        var variantBtnPadding = SceneLoader.ParseInt(element.Attribute("VariantBtnPadding")?.Value);
        if (variantBtnPadding.HasValue)
            browser.VariantBtnPadding = variantBtnPadding.Value;

        return browser;
    }

    private static BaseComponent? CreateDeckPile(XElement element, IScene scene)
    {
        var pile = new DeckPile();

        // Panel properties
        var background = SceneLoader.ParseColor(element.Attribute("Background")?.Value);
        if (background.HasValue)
            pile.Background = background.Value;

        var border = SceneLoader.ParseBorder(element);
        if (border is not null)
            pile.Border = border;

        // Header
        var headerText = element.Attribute("HeaderText")?.Value;
        if (!string.IsNullOrEmpty(headerText))
            pile.HeaderText = headerText;

        var headerFont = element.Attribute("HeaderFont")?.Value;
        if (!string.IsNullOrEmpty(headerFont))
            pile.HeaderFont = headerFont;

        var headerFontSize = SceneLoader.ParseInt(element.Attribute("HeaderFontSize")?.Value);
        if (headerFontSize.HasValue)
            pile.HeaderFontSize = headerFontSize.Value;

        var headerColor = SceneLoader.ParseColor(element.Attribute("HeaderColor")?.Value);
        if (headerColor.HasValue)
            pile.HeaderColor = headerColor.Value;

        var headerOffsetY = SceneLoader.ParseFloat(element.Attribute("HeaderOffsetY")?.Value);
        if (headerOffsetY.HasValue)
            pile.HeaderOffsetY = headerOffsetY.Value;

        // Count label
        var countFont = element.Attribute("CountFont")?.Value;
        if (!string.IsNullOrEmpty(countFont))
            pile.CountFont = countFont;

        var countFontSize = SceneLoader.ParseInt(element.Attribute("CountFontSize")?.Value);
        if (countFontSize.HasValue)
            pile.CountFontSize = countFontSize.Value;

        var countColor = SceneLoader.ParseColor(element.Attribute("CountColor")?.Value);
        if (countColor.HasValue)
            pile.CountColor = countColor.Value;

        var countOffsetY = SceneLoader.ParseFloat(element.Attribute("CountOffsetY")?.Value);
        if (countOffsetY.HasValue)
            pile.CountOffsetY = countOffsetY.Value;

        // Scroll panel
        var scrollPanelBg = SceneLoader.ParseColor(element.Attribute("ScrollPanelBg")?.Value);
        if (scrollPanelBg.HasValue)
            pile.ScrollPanelBg = scrollPanelBg.Value;

        var scrollPanelBorder = SceneLoader.ParseBorder(element, "ScrollPanelBorder");
        if (scrollPanelBorder is not null)
            pile.ScrollPanelBorder = scrollPanelBorder;

        var scrollSpeed = SceneLoader.ParseFloat(element.Attribute("ScrollSpeed")?.Value);
        if (scrollSpeed.HasValue)
            pile.ScrollSpeed = scrollSpeed.Value;

        var scrollbarWidth = SceneLoader.ParseInt(element.Attribute("ScrollbarWidth")?.Value);
        if (scrollbarWidth.HasValue)
            pile.ScrollbarWidth = scrollbarWidth.Value;

        var scrollbarPadding = SceneLoader.ParseInt(element.Attribute("ScrollbarPadding")?.Value);
        if (scrollbarPadding.HasValue)
            pile.ScrollbarPadding = scrollbarPadding.Value;

        var scrollbarThumb = SceneLoader.ParseColor(element.Attribute("ScrollbarThumb")?.Value);
        if (scrollbarThumb.HasValue)
            pile.ScrollbarThumb = scrollbarThumb.Value;

        var scrollbarThumbHovered = SceneLoader.ParseColor(element.Attribute("ScrollbarThumbHovered")?.Value);
        if (scrollbarThumbHovered.HasValue)
            pile.ScrollbarThumbHovered = scrollbarThumbHovered.Value;

        var scrollTopOffset = SceneLoader.ParseFloat(element.Attribute("ScrollTopOffset")?.Value);
        if (scrollTopOffset.HasValue)
            pile.ScrollTopOffset = scrollTopOffset.Value;

        // Featured card area
        var featuredBg = SceneLoader.ParseColor(element.Attribute("FeaturedBg")?.Value);
        if (featuredBg.HasValue)
            pile.FeaturedBg = featuredBg.Value;

        var featuredBorder = SceneLoader.ParseBorder(element, "FeaturedBorder");
        if (featuredBorder is not null)
            pile.FeaturedBorder = featuredBorder;

        var featuredScale = SceneLoader.ParseFloat(element.Attribute("FeaturedScale")?.Value);
        if (featuredScale.HasValue)
            pile.FeaturedScale = featuredScale.Value;

        var featuredLabelFont = element.Attribute("FeaturedLabelFont")?.Value;
        if (!string.IsNullOrEmpty(featuredLabelFont))
            pile.FeaturedLabelFont = featuredLabelFont;

        var featuredLabelFontSize = SceneLoader.ParseInt(element.Attribute("FeaturedLabelFontSize")?.Value);
        if (featuredLabelFontSize.HasValue)
            pile.FeaturedLabelFontSize = featuredLabelFontSize.Value;

        var featuredLabelColor = SceneLoader.ParseColor(element.Attribute("FeaturedLabelColor")?.Value);
        if (featuredLabelColor.HasValue)
            pile.FeaturedLabelColor = featuredLabelColor.Value;

        // Card list items
        var cardListScale = SceneLoader.ParseFloat(element.Attribute("CardListScale")?.Value);
        if (cardListScale.HasValue)
            pile.CardListScale = cardListScale.Value;

        var cardItemHeight = SceneLoader.ParseFloat(element.Attribute("CardItemHeight")?.Value);
        if (cardItemHeight.HasValue)
            pile.CardItemHeight = cardItemHeight.Value;

        var cardItemSpacing = SceneLoader.ParseFloat(element.Attribute("CardItemSpacing")?.Value);
        if (cardItemSpacing.HasValue)
            pile.CardItemSpacing = cardItemSpacing.Value;

        var cardItemBg = SceneLoader.ParseColor(element.Attribute("CardItemBg")?.Value);
        if (cardItemBg.HasValue)
            pile.CardItemBg = cardItemBg.Value;

        var cardItemHoverBg = SceneLoader.ParseColor(element.Attribute("CardItemHoverBg")?.Value);
        if (cardItemHoverBg.HasValue)
            pile.CardItemHoverBg = cardItemHoverBg.Value;

        var cardItemBorder = SceneLoader.ParseBorder(element, "CardItemBorder");
        if (cardItemBorder is not null)
            pile.CardItemBorder = cardItemBorder;

        var cardItemFont = element.Attribute("CardItemFont")?.Value;
        if (!string.IsNullOrEmpty(cardItemFont))
            pile.CardItemFont = cardItemFont;

        var cardItemFontSize = SceneLoader.ParseInt(element.Attribute("CardItemFontSize")?.Value);
        if (cardItemFontSize.HasValue)
            pile.CardItemFontSize = cardItemFontSize.Value;

        var cardItemColor = SceneLoader.ParseColor(element.Attribute("CardItemColor")?.Value);
        if (cardItemColor.HasValue)
            pile.CardItemColor = cardItemColor.Value;

        return pile;
    }

    private static BaseComponent? CreatePopup(XElement element, IScene scene)
    {
        var title = element.Attribute("Title")?.Value ?? "Popup";
        var width = SceneLoader.ParseInt(element.Attribute("Width")?.Value) ?? 600;
        var height = SceneLoader.ParseInt(element.Attribute("Height")?.Value) ?? 400;

        var popup = new Popup
        {
            Title = title,
            ContentSize = new Vector2(width, height),
        };

        var okText = element.Attribute("OkText")?.Value;
        if (!string.IsNullOrEmpty(okText))
            popup.OkText = okText;

        var cancelText = element.Attribute("CancelText")?.Value;
        if (!string.IsNullOrEmpty(cancelText))
            popup.CancelText = cancelText;

        return popup;
    }

    private static BaseComponent? CreatePokemonCard(XElement element, IScene scene)
    {
        var card = ParsePokemonCardData(element);
        card.AutoBuild = true;
        return card;
    }

    private static BaseComponent? CreateBerryCard(XElement element, IScene scene)
    {
        var card = ParseBerryCardData(element);
        card.AutoBuild = true;
        return card;
    }

    private static BaseComponent? CreateDeck(XElement element, IScene scene)
    {
        var deckName = element.Attribute("DeckName")?.Value ?? "Deck";
        var deck = new Deck(deckName);

        // Parse FaceCard sub-element
        var faceCardEl = element.Element("FaceCard");
        if (faceCardEl is not null)
            deck.FaceCard = ParsePokemonCardData(faceCardEl);

        // Parse MainDeck sub-element with PokemonCard children
        var mainDeckEl = element.Element("MainDeck");
        if (mainDeckEl is not null)
            foreach (var cardEl in mainDeckEl.Elements("PokemonCard"))
                deck.MainDeck.Add(ParsePokemonCardData(cardEl));

        // Parse SideDeck sub-element with BerryCard children
        var sideDeckEl = element.Element("SideDeck");
        if (sideDeckEl is not null)
            foreach (var cardEl in sideDeckEl.Elements("BerryCard"))
                deck.SideDeck.Add(ParseBerryCardData(cardEl));

        deck.AutoBuild = true;
        return deck;
    }

    internal static PokemonCardComponent ParsePokemonCardData(XElement element)
    {
        var cardComponent = new PokemonCardComponent();

        // Look up PokeDexEntry by identity string (SpriteIdentifier format)
        var spriteId = element.Attribute("SpriteId")?.Value;
        PokeDexEntry? dexEntry = null;
        if (!string.IsNullOrEmpty(spriteId))
            dexEntry = PokeDex.Entries.Find(e => e.SpriteIdentifier == spriteId);
        dexEntry ??= new PokeDexEntry();

        var level = SceneLoader.ParseInt(element.Attribute("Level")?.Value) ?? 1;
        var innatePower = SceneLoader.ParseInt(element.Attribute("InnatePower")?.Value) ?? 0;
        var nickname = element.Attribute("Nickname")?.Value;

        var card = new PokeCard(dexEntry, level, innatePower, nickname)
        {
            HP = dexEntry.Hp,
            MaxHP = dexEntry.Hp,
            Atk = dexEntry.Attack,
            Def = dexEntry.Defense,
        };

        var costStr = element.Attribute("Cost")?.Value;
        if (!string.IsNullOrEmpty(costStr))
            card.Cost = costStr.Split(',')
                .Select(s => Enum.TryParse<BerryEnergyType>(s.Trim(), true, out var c) ? c : BerryEnergyType.Void)
                .ToList();

        var glyphsStr = element.Attribute("Glyphs")?.Value;
        if (!string.IsNullOrEmpty(glyphsStr))
            card.Glyphs = [.. glyphsStr.Split(',').Select(s => s.Trim())];

        cardComponent.Card = card;
        cardComponent.CardName = nickname ?? dexEntry.Name;

        var faceUp = SceneLoader.ParseBool(element.Attribute("FaceUp")?.Value);
        if (faceUp.HasValue)
            cardComponent.FaceUp = faceUp.Value;

        return cardComponent;
    }

    internal static BerryCardComponent ParseBerryCardData(XElement element)
    {
        var card = new BerryCardComponent();

        var cardName = element.Attribute("CardName")?.Value;
        if (!string.IsNullOrEmpty(cardName))
            card.CardName = cardName;

        var berryTypesStr = element.Attribute("BerryTypes")?.Value;
        if (!string.IsNullOrEmpty(berryTypesStr))
            card.BerryTypes = berryTypesStr.Split(',')
                .Select(s => Enum.TryParse<BerryEnergyType>(s.Trim(), true, out var c) ? c : BerryEnergyType.Green)
                .ToList();

        var faceUp = SceneLoader.ParseBool(element.Attribute("FaceUp")?.Value);
        if (faceUp.HasValue)
            card.FaceUp = faceUp.Value;

        var berrySpriteId = element.Attribute("BerrySpriteId")?.Value;
        if (!string.IsNullOrEmpty(berrySpriteId))
            card.BerrySpriteId = berrySpriteId;

        return card;
    }
}
