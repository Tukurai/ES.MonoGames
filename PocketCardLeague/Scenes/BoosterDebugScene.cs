using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Components;
using Helpers;
using PocketCardLeague.Components;
using PocketCardLeague.Consts;
using PocketCardLeague.Enums;
using PocketCardLeague.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketCardLeague.Scenes;

public class BoosterDebugScene() : XmlScene<SceneType>(SceneType.BoosterDebug)
{
    protected override string XmlFileName => "BoosterDebugScene.xml";

    private Dropdown _filterGen = null!;
    private Panel _cardArea = null!;
    private Label _statusLabel = null!;
    private readonly List<CardComponent> _displayedCards = [];
    private Action? _pendingAction;

    protected override void OnXmlLoaded()
    {
        var btnBack = TryBind<SpriteButton>("btn_back");
        if (btnBack is not null)
        {
            btnBack.OnClicked += () => SceneManager.SetActiveScene(SceneType.Cards, new SlideTransition(SlideDirection.Down));
            btnBack.OnHoveredEnter += () => btnBack.Opacity = 1f;
            btnBack.OnHoveredExit += () => btnBack.Opacity = 0.8f;
        }

        _filterGen = Bind<Dropdown>("filter_gen");
        var genItems = new List<string> { "All Gens" };
        var gens = PokeDex.Entries.Select(e => e.Generation).Distinct().OrderBy(g => g);
        genItems.AddRange(gens.Select(g => $"Gen {g}"));
        _filterGen.Items = genItems;
        _filterGen.SelectedIndex = 0;

        var btnGetPack = Bind<Button>("btn_get_pack");
        btnGetPack.OnClicked += () => _pendingAction = GeneratePokemonPack;

        var btnGetBerryPack = Bind<Button>("btn_get_berry_pack");
        btnGetBerryPack.OnClicked += () => _pendingAction = GenerateBerryPack;

        _cardArea = Bind<Panel>("card_area");
        _statusLabel = Bind<Label>("status_label");
    }

    private void ClearDisplayedCards()
    {
        foreach (var card in _displayedCards)
            RemoveComponent(card);
        _displayedCards.Clear();
    }

    private void GeneratePokemonPack()
    {
        ClearDisplayedCards();

        var entries = PokeDex.Entries
            .Where(e => !e.MegaEvolution && !e.Gigantamax && !e.Shiny);

        if (_filterGen.SelectedIndex > 0)
        {
            var gen = int.Parse(_filterGen.Items[_filterGen.SelectedIndex].Replace("Gen ", ""));
            entries = entries.Where(e => e.Generation == gen);
        }

        var pool = entries.ToList();
        if (pool.Count == 0)
        {
            _statusLabel.Text = "No Pokemon found for this generation.";
            return;
        }

        var picked = new List<PokeDexEntry>();
        for (int i = 0; i < 4; i++)
            picked.Add(pool[Random.Shared.Next(pool.Count)]);

        var anchor = _cardArea.Position.GetVector2();
        const float spacing = 380f;

        for (int i = 0; i < picked.Count; i++)
        {
            var entry = picked[i];
            var level = Random.Shared.Next(1, 6);
            var innate = Random.Shared.Next(0, 3);

            var card = new PokeCard(entry.SpriteIdentifier, level, innate)
            {
                Cost = GetCostFromType(entry.Type1),
            };

            var comp = new PokemonCardComponent
            {
                Card = card,
                CardName = entry.Name,
                Scale = new Vector2(4, 4),
                Position = new Anchor(new Vector2(anchor.X + i * spacing, anchor.Y)),
            };
            comp.BuildVisuals();

            comp.OnClicked += () => _pendingAction = () => CollectCard(comp);
            AddComponent(comp);
            _displayedCards.Add(comp);
        }

        _statusLabel.Text = "Click a card to add it to your collection!";
    }

    private void GenerateBerryPack()
    {
        ClearDisplayedCards();

        var pool = BerryDex.Entries;
        if (pool.Count == 0)
        {
            _statusLabel.Text = "No berries available.";
            return;
        }

        var picked = new List<BerryDexEntry>();
        for (int i = 0; i < 4; i++)
            picked.Add(pool[Random.Shared.Next(pool.Count)]);

        var anchor = _cardArea.Position.GetVector2();
        const float spacing = 380f;

        for (int i = 0; i < picked.Count; i++)
        {
            var entry = picked[i];

            var comp = new BerryCardComponent
            {
                CardName = entry.Name,
                BerryTypes = [entry.Type],
                BerrySpriteId = entry.SpriteIdentifier,
                Scale = new Vector2(4, 4),
                Position = new Anchor(new Vector2(anchor.X + i * spacing, anchor.Y)),
            };
            comp.BuildVisuals();

            comp.OnClicked += () => _pendingAction = () => CollectCard(comp);
            AddComponent(comp);
            _displayedCards.Add(comp);
        }

        _statusLabel.Text = "Click a card to add it to your collection!";
    }

    private void CollectCard(CardComponent card)
    {
        var save = GameStateManager.ActiveSave;

        if (card is PokemonCardComponent pokemon)
            save.PokemonCards.Add(pokemon);
        else if (card is BerryCardComponent berry)
            save.BerryCards.Add(berry);

        GameStateManager.Save(save);
        RemoveComponent(card);
        _displayedCards.Remove(card);

        _statusLabel.Text = $"Added {card.CardName} to collection!";
    }

    private static List<BerryEnergyType> GetCostFromType(PokemonType type) => type switch
    {
        PokemonType.Fire => [BerryEnergyType.Red, BerryEnergyType.Void],
        PokemonType.Water => [BerryEnergyType.Blue, BerryEnergyType.Void],
        PokemonType.Grass => [BerryEnergyType.Green, BerryEnergyType.Void],
        PokemonType.Electric => [BerryEnergyType.Yellow, BerryEnergyType.Void],
        PokemonType.Psychic => [BerryEnergyType.Purple, BerryEnergyType.Void],
        PokemonType.Ice => [BerryEnergyType.Blue, BerryEnergyType.Void],
        PokemonType.Dragon => [BerryEnergyType.Purple, BerryEnergyType.Void],
        PokemonType.Dark => [BerryEnergyType.Purple, BerryEnergyType.Void],
        PokemonType.Fairy => [BerryEnergyType.White, BerryEnergyType.Void],
        PokemonType.Ghost => [BerryEnergyType.Purple, BerryEnergyType.Void],
        PokemonType.Poison => [BerryEnergyType.Green, BerryEnergyType.Void],
        PokemonType.Fighting => [BerryEnergyType.Red, BerryEnergyType.Void],
        PokemonType.Ground => [BerryEnergyType.Yellow, BerryEnergyType.Void],
        PokemonType.Rock => [BerryEnergyType.Yellow, BerryEnergyType.Void],
        PokemonType.Steel => [BerryEnergyType.White, BerryEnergyType.Void],
        PokemonType.Bug => [BerryEnergyType.Green, BerryEnergyType.Void],
        PokemonType.Flying => [BerryEnergyType.Blue, BerryEnergyType.Void],
        _ => [BerryEnergyType.Void, BerryEnergyType.Void],
    };

    public override void Update(GameTime gameTime)
    {
        var pressedKeys = ControlState.GetPressedKeys();
        if (pressedKeys.Contains(Keys.Up))
        {
            SceneManager.SetActiveScene(SceneType.Main, new SlideTransition(SlideDirection.Down));
            return;
        }

        base.Update(gameTime);

        if (_pendingAction is not null)
        {
            var action = _pendingAction;
            _pendingAction = null;
            action();
        }
    }
}
