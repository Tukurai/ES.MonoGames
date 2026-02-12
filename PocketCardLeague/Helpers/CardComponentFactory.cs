using Components;
using Helpers;
using PocketCardLeague.Components;
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

    internal static PokemonCard ParsePokemonCardData(XElement element)
    {
        var card = new PokemonCard();

        var cardName = element.Attribute("CardName")?.Value;
        if (!string.IsNullOrEmpty(cardName))
            card.CardName = cardName;

        var dexId = SceneLoader.ParseInt(element.Attribute("DexId")?.Value);
        if (dexId.HasValue)
            card.DexId = dexId.Value;

        var level = SceneLoader.ParseInt(element.Attribute("Level")?.Value);
        if (level.HasValue)
            card.Level = level.Value;

        card.Type1 = SceneLoader.ParseAttribute<PokemonType>(element, "Type1", PokemonType.Normal);

        var type2Str = element.Attribute("Type2")?.Value;
        if (!string.IsNullOrEmpty(type2Str) && Enum.TryParse<PokemonType>(type2Str, true, out var type2))
            card.Type2 = type2;

        var costStr = element.Attribute("Cost")?.Value;
        if (!string.IsNullOrEmpty(costStr))
            card.Cost = costStr.Split(',')
                .Select(s => Enum.TryParse<BerryEnergyType>(s.Trim(), true, out var c) ? c : BerryEnergyType.Void)
                .ToList();

        var faceUp = SceneLoader.ParseBool(element.Attribute("FaceUp")?.Value);
        if (faceUp.HasValue)
            card.FaceUp = faceUp.Value;

        var innatePower = SceneLoader.ParseInt(element.Attribute("InnatePower")?.Value);
        if (innatePower.HasValue)
            card.InnatePower = innatePower.Value;

        var nickname = element.Attribute("Nickname")?.Value;
        if (!string.IsNullOrEmpty(nickname))
            card.Nickname = nickname;

        var hp = SceneLoader.ParseInt(element.Attribute("HP")?.Value);
        if (hp.HasValue)
            card.HP = hp.Value;

        var atk = SceneLoader.ParseInt(element.Attribute("Atk")?.Value);
        if (atk.HasValue)
            card.Atk = atk.Value;

        var def = SceneLoader.ParseInt(element.Attribute("Def")?.Value);
        if (def.HasValue)
            card.Def = def.Value;

        var glyphsStr = element.Attribute("Glyphs")?.Value;
        if (!string.IsNullOrEmpty(glyphsStr))
            card.Glyphs = glyphsStr.Split(',').Select(s => s.Trim()).ToList();

        return card;
    }

    internal static BerryCard ParseBerryCardData(XElement element)
    {
        var card = new BerryCard();

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
