using Components;
using Helpers;
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
