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

        // Variant label properties
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

        // Variant SpriteButton properties
        var variantPrevSprite = element.Attribute("VariantPrevSprite")?.Value;
        if (!string.IsNullOrEmpty(variantPrevSprite))
            browser.VariantPrevSprite = variantPrevSprite;

        var variantNextSprite = element.Attribute("VariantNextSprite")?.Value;
        if (!string.IsNullOrEmpty(variantNextSprite))
            browser.VariantNextSprite = variantNextSprite;

        var variantPrevActiveSprite = element.Attribute("VariantPrevActiveSprite")?.Value;
        if (!string.IsNullOrEmpty(variantPrevActiveSprite))
            browser.VariantPrevActiveSprite = variantPrevActiveSprite;

        var variantNextActiveSprite = element.Attribute("VariantNextActiveSprite")?.Value;
        if (!string.IsNullOrEmpty(variantNextActiveSprite))
            browser.VariantNextActiveSprite = variantNextActiveSprite;

        var variantBtnScale = SceneLoader.ParseVector2(element.Attribute("VariantBtnScale")?.Value);
        if (variantBtnScale.HasValue)
            browser.VariantBtnScale = variantBtnScale.Value;

        var variantBtnOffset = SceneLoader.ParseVector2(element.Attribute("VariantBtnOffset")?.Value);
        if (variantBtnOffset.HasValue)
            browser.VariantBtnOffset = variantBtnOffset.Value;

        var variantLabelOffset = SceneLoader.ParseVector2(element.Attribute("VariantLabelOffset")?.Value);
        if (variantLabelOffset.HasValue)
            browser.VariantLabelOffset = variantLabelOffset.Value;

        // Variant name label properties
        var showVariantName = SceneLoader.ParseBool(element.Attribute("ShowVariantName")?.Value);
        if (showVariantName.HasValue)
            browser.ShowVariantName = showVariantName.Value;

        var variantNameFont = element.Attribute("VariantNameFont")?.Value;
        if (!string.IsNullOrEmpty(variantNameFont))
            browser.VariantNameFont = variantNameFont;

        var variantNameFontSize = SceneLoader.ParseInt(element.Attribute("VariantNameFontSize")?.Value);
        if (variantNameFontSize.HasValue)
            browser.VariantNameFontSize = variantNameFontSize.Value;

        var variantNameColor = SceneLoader.ParseColor(element.Attribute("VariantNameColor")?.Value);
        if (variantNameColor.HasValue)
            browser.VariantNameColor = variantNameColor.Value;

        var variantNameBorder = SceneLoader.ParseBorder(element, "VariantNameBorder");
        if (variantNameBorder is not null)
            browser.VariantNameBorder = variantNameBorder;

        var variantNameOffset = SceneLoader.ParseVector2(element.Attribute("VariantNameOffset")?.Value);
        if (variantNameOffset.HasValue)
            browser.VariantNameOffset = variantNameOffset.Value;

        // Slider and page label are now nested children (Slider, BitmapLabel)
        // parsed automatically by SceneLoader's <Children> handling

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

        // Layout properties
        var scrollTopOffset = SceneLoader.ParseFloat(element.Attribute("ScrollTopOffset")?.Value);
        if (scrollTopOffset.HasValue)
            pile.ScrollTopOffset = scrollTopOffset.Value;

        // Card item template properties
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

        var cardListPadding = SceneLoader.ParseThickness(element.Attribute("CardListPadding")?.Value);
        if (cardListPadding.HasValue)
            pile.CardListPadding = cardListPadding.Value;

        // Face card checkbox properties
        var faceCardCheckedSprite = element.Attribute("FaceCardCheckedSprite")?.Value;
        if (!string.IsNullOrEmpty(faceCardCheckedSprite))
            pile.FaceCardCheckedSprite = faceCardCheckedSprite;

        var faceCardUncheckedSprite = element.Attribute("FaceCardUncheckedSprite")?.Value;
        if (!string.IsNullOrEmpty(faceCardUncheckedSprite))
            pile.FaceCardUncheckedSprite = faceCardUncheckedSprite;

        var faceCardCheckboxScale = SceneLoader.ParseVector2(element.Attribute("FaceCardCheckboxScale")?.Value);
        if (faceCardCheckboxScale.HasValue)
            pile.FaceCardCheckboxScale = faceCardCheckboxScale.Value;

        var faceCardCheckboxOffset = SceneLoader.ParseVector2(element.Attribute("FaceCardCheckboxOffset")?.Value);
        if (faceCardCheckboxOffset.HasValue)
            pile.FaceCardCheckboxOffset = faceCardCheckboxOffset.Value;

        // Structural children (pile_header, pile_count, pile_scroll, pile_featured)
        // are parsed automatically by SceneLoader's <Children> handling

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

        var level = SceneLoader.ParseInt(element.Attribute("Level")?.Value) ?? 1;
        var innatePower = SceneLoader.ParseInt(element.Attribute("InnatePower")?.Value) ?? 0;
        var nickname = element.Attribute("Nickname")?.Value;

        var card = new PokeCard(spriteId, level, innatePower, nickname);

        var costStr = element.Attribute("Cost")?.Value;
        if (!string.IsNullOrEmpty(costStr))
            card.Cost = costStr.Split(',')
                .Select(s => Enum.TryParse<BerryEnergyType>(s.Trim(), true, out var c) ? c : BerryEnergyType.Void)
                .ToList();

        var glyphsStr = element.Attribute("Glyphs")?.Value;
        if (!string.IsNullOrEmpty(glyphsStr))
            card.Glyphs = [.. glyphsStr.Split(',').Select(s => s.Trim())];

        cardComponent.Card = card;
        cardComponent.CardName = nickname ?? card.BasePokemon.Name;

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
