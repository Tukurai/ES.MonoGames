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

public class DeckBuilderScene() : XmlScene<SceneType>(SceneType.DeckBuilder)
{
    protected override string XmlFileName => "DeckBuilderScene.xml";

    // State
    private bool _isMainDeck = true;
    private Deck _editingDeck = null!;

    // Components (XML-bound)
    private InputField _deckNameInput = null!;
    private Button _tabPokemon = null!;
    private Button _tabBerry = null!;
    private Panel _pokemonFilterBar = null!;
    private Panel _berryFilterBar = null!;

    // Components (XML-bound custom)
    private CardPageBrowser _browser = null!;
    private DeckPile _deckPile = null!;

    // Tab styles captured from XML initial state
    private (Color Background, Color TextColor, Border? Border) _tabActiveStyle;
    private (Color Background, Color TextColor, Border? Border) _tabInactiveStyle;

    // Filters (Pokemon mode — XML-bound)
    private Dropdown _filterType = null!;
    private Dropdown _filterGen = null!;
    private Checkbox _filterMega = null!;
    private Checkbox _filterGmax = null!;
    private Checkbox _filterShiny = null!;
    private Dropdown _filterCardLevel = null!;
    private Dropdown _filterInnatePower = null!;
    private Dropdown _filterCostTotal = null!;
    private Dropdown _filterCostColor = null!;

    // Filters (Berry mode — XML-bound)
    private Dropdown _filterBerryColor = null!;

    protected override void OnBeforeReload()
    {
        _browser?.ClearCardHandlers();
    }

    protected override void OnXmlLoaded()
    {
        // Load or create editing deck
        var save = GameStateManager.ActiveSave;
        if (save.EditingDeck is not null)
            _editingDeck = save.EditingDeck;
        else
        {
            _editingDeck = new Deck("New Deck");
            save.EditingDeck = _editingDeck;
            // Auto-add to decks list
            if (!save.Decks.Any(d => d.Id == _editingDeck.Id))
                save.Decks.Add(_editingDeck);
        }

        // Bind XML components
        var btnBack = TryBind<Button>("btn_back");
        if (btnBack is not null)
        {
            btnBack.OnClicked += () =>
            {
                AutoSave();
                SceneManager.SetActiveScene(SceneType.Decks, new SlideTransition(SlideDirection.Up));
            };
        }

        var btnDelete = TryBind<Button>("btn_delete");
        if (btnDelete is not null)
            btnDelete.OnClicked += DeleteDeck;

        _deckNameInput = Bind<InputField>("deck_name_input");
        _deckNameInput.Text = _editingDeck.DeckName;
        _deckNameInput.OnTextChanged += text => _editingDeck.DeckName = text;

        _tabPokemon = Bind<Button>("tab_pokemon");
        _tabPokemon.OnClicked += () => SwitchTab(true);

        _tabBerry = Bind<Button>("tab_berry");
        _tabBerry.OnClicked += () => SwitchTab(false);

        // Capture tab styles from XML initial state (pokemon tab is active by default)
        _tabActiveStyle = (_tabPokemon.Background, _tabPokemon.TextColor, _tabPokemon.Border);
        _tabInactiveStyle = (_tabBerry.Background, _tabBerry.TextColor, _tabBerry.Border);

        // Bind filter bars
        _pokemonFilterBar = Bind<Panel>("pokemon_filter_bar");
        _berryFilterBar = Bind<Panel>("berry_filter_bar");

        // Bind custom components from XML
        _browser = Bind<CardPageBrowser>("card_browser");
        _browser.OnCardClicked += OnBrowserCardClicked;
        _browser.BuildLayout();

        _deckPile = Bind<DeckPile>("deck_pile");
        _deckPile.OnCardClicked += OnPileCardClicked;
        _deckPile.OnFaceCardChanged += OnFaceCardChanged;
        _deckPile.BuildLayout();

        // Bind and populate Pokemon filters
        BindPokemonFilters();

        // Bind and populate Berry filters
        BindBerryFilters();

        // Initial load
        ApplyFiltersAndRefresh();
    }

    private void BindPokemonFilters()
    {
        _filterType = Bind<Dropdown>("filter_type");
        var typeItems = new List<string> { "All Types" };
        typeItems.AddRange(Enum.GetValues<PokemonType>().Where(t => t != PokemonType.UNK).Select(t => t.ToString()));
        _filterType.Items = typeItems;
        _filterType.SelectedIndex = 0;
        _filterType.OnSelectionChanged += _ => ApplyFiltersAndRefresh();

        _filterGen = Bind<Dropdown>("filter_gen");
        var genItems = new List<string> { "All Gens" };
        genItems.AddRange(Enumerable.Range(1, 9).Select(g => $"Gen {g}"));
        _filterGen.Items = genItems;
        _filterGen.SelectedIndex = 0;
        _filterGen.OnSelectionChanged += _ => ApplyFiltersAndRefresh();

        _filterMega = Bind<Checkbox>("filter_mega");
        _filterMega.OnCheckedChanged += () => ApplyFiltersAndRefresh();

        _filterGmax = Bind<Checkbox>("filter_gmax");
        _filterGmax.OnCheckedChanged += () => ApplyFiltersAndRefresh();

        _filterShiny = Bind<Checkbox>("filter_shiny");
        _filterShiny.OnCheckedChanged += () => ApplyFiltersAndRefresh();

        _filterCardLevel = Bind<Dropdown>("filter_lvl");
        var lvlItems = new List<string> { "All Levels" };
        lvlItems.AddRange(Enumerable.Range(1, 10).Select(l => $"Lv. {l}"));
        _filterCardLevel.Items = lvlItems;
        _filterCardLevel.SelectedIndex = 0;
        _filterCardLevel.OnSelectionChanged += _ => ApplyFiltersAndRefresh();

        _filterInnatePower = Bind<Dropdown>("filter_innate");
        var innateItems = new List<string> { "All Innate" };
        innateItems.AddRange(Enumerable.Range(0, 6).Select(i => $"Innate {i}"));
        _filterInnatePower.Items = innateItems;
        _filterInnatePower.SelectedIndex = 0;
        _filterInnatePower.OnSelectionChanged += _ => ApplyFiltersAndRefresh();

        _filterCostTotal = Bind<Dropdown>("filter_cost_total");
        var costTotalItems = new List<string> { "Any Cost" };
        costTotalItems.AddRange(Enumerable.Range(1, 6).Select(c => $"{c} Cost"));
        _filterCostTotal.Items = costTotalItems;
        _filterCostTotal.SelectedIndex = 0;
        _filterCostTotal.OnSelectionChanged += _ => ApplyFiltersAndRefresh();

        _filterCostColor = Bind<Dropdown>("filter_cost_color");
        var costColorItems = new List<string> { "Any Color" };
        costColorItems.AddRange(Enum.GetValues<BerryEnergyType>().Select(b => b.ToString()));
        _filterCostColor.Items = costColorItems;
        _filterCostColor.SelectedIndex = 0;
        _filterCostColor.OnSelectionChanged += _ => ApplyFiltersAndRefresh();
    }

    private void BindBerryFilters()
    {
        _filterBerryColor = Bind<Dropdown>("filter_berry_color");
        var colorItems = new List<string> { "All Colors" };
        colorItems.AddRange(Enum.GetValues<BerryEnergyType>().Select(b => b.ToString()));
        _filterBerryColor.Items = colorItems;
        _filterBerryColor.SelectedIndex = 0;
        _filterBerryColor.OnSelectionChanged += _ => ApplyFiltersAndRefresh();
    }

    public override void Start()
    {
        base.Start();

        var save = GameStateManager.ActiveSave;
        if (save.EditingDeck is not null)
        {
            _editingDeck = save.EditingDeck;
            _deckNameInput.Text = _editingDeck.DeckName;
        }

        ApplyFiltersAndRefresh();
    }

    private void SwitchTab(bool isMainDeck)
    {
        if (_isMainDeck == isMainDeck)
            return;

        _isMainDeck = isMainDeck;

        _pokemonFilterBar.Show = _isMainDeck;
        _berryFilterBar.Show = !_isMainDeck;

        if (_isMainDeck)
        {
            _tabPokemon.Background = _tabActiveStyle.Background;
            _tabPokemon.TextColor = _tabActiveStyle.TextColor;
            _tabPokemon.Border = _tabActiveStyle.Border;
            _tabBerry.Background = _tabInactiveStyle.Background;
            _tabBerry.TextColor = _tabInactiveStyle.TextColor;
            _tabBerry.Border = _tabInactiveStyle.Border;
        }
        else
        {
            _tabBerry.Background = _tabActiveStyle.Background;
            _tabBerry.TextColor = _tabActiveStyle.TextColor;
            _tabBerry.Border = _tabActiveStyle.Border;
            _tabPokemon.Background = _tabInactiveStyle.Background;
            _tabPokemon.TextColor = _tabInactiveStyle.TextColor;
            _tabPokemon.Border = _tabInactiveStyle.Border;
        }

        _deckPile.SetHeader(_isMainDeck ? "Main Deck" : "Side Deck");
        RefreshDeckPile();
        ApplyFiltersAndRefresh();
    }

    private void ApplyFiltersAndRefresh()
    {
        if (_isMainDeck)
        {
            var available = GetAvailablePokemonCards();
            var filtered = ApplyPokemonFilters(available);
            _browser.SetCards(filtered.Cast<CardComponent>().ToList());
        }
        else
        {
            var available = GetAvailableBerryCards();
            var filtered = ApplyBerryFilters(available);
            _browser.SetCards(filtered.Cast<CardComponent>().ToList());
        }

        RefreshDeckPile();
    }

    private List<PokemonCardComponent> GetAvailablePokemonCards()
    {
        var save = GameStateManager.ActiveSave;
        var deckCardIds = new HashSet<Guid>(_editingDeck.MainDeck.Select(c => c.Id));
        return [.. save.PokemonCards.Where(c => !deckCardIds.Contains(c.Id))];
    }

    private List<BerryCardComponent> GetAvailableBerryCards()
    {
        var save = GameStateManager.ActiveSave;
        var deckCardIds = new HashSet<Guid>(_editingDeck.SideDeck.Select(c => c.Id));
        return [.. save.BerryCards.Where(c => !deckCardIds.Contains(c.Id))];
    }

    private List<PokemonCardComponent> ApplyPokemonFilters(List<PokemonCardComponent> cards)
    {
        var result = cards.AsEnumerable();

        if (_filterType.SelectedIndex > 0)
        {
            var selectedType = Enum.GetValues<PokemonType>().Where(t => t != PokemonType.UNK).ElementAtOrDefault(_filterType.SelectedIndex - 1);
            result = result.Where(c => c.Card?.BasePokemon?.Types.Contains(selectedType) == true);
        }

        if (_filterGen.SelectedIndex > 0)
        {
            var gen = _filterGen.SelectedIndex;
            result = result.Where(c => c.Card?.BasePokemon?.Generation == gen);
        }

        if (_filterMega.IsChecked)
            result = result.Where(c => c.Card?.BasePokemon?.Mega == true);

        if (_filterGmax.IsChecked)
            result = result.Where(c => c.Card?.BasePokemon?.Gigantamax == true);

        if (_filterShiny.IsChecked)
            result = result.Where(c => c.Card?.BasePokemon?.Shiny == true);

        if (_filterCardLevel.SelectedIndex > 0)
        {
            var lvl = _filterCardLevel.SelectedIndex;
            result = result.Where(c => c.Card?.Level == lvl);
        }

        if (_filterInnatePower.SelectedIndex > 0)
        {
            var innate = _filterInnatePower.SelectedIndex - 1;
            result = result.Where(c => c.Card?.InnatePower == innate);
        }

        if (_filterCostTotal.SelectedIndex > 0)
        {
            var costCount = _filterCostTotal.SelectedIndex;
            result = result.Where(c => c.Card?.Cost.Count == costCount);
        }

        if (_filterCostColor.SelectedIndex > 0)
        {
            var costColor = Enum.GetValues<BerryEnergyType>().ElementAtOrDefault(_filterCostColor.SelectedIndex - 1);
            result = result.Where(c => c.Card?.Cost.Contains(costColor) == true);
        }

        return result.ToList();
    }

    private List<BerryCardComponent> ApplyBerryFilters(List<BerryCardComponent> cards)
    {
        var result = cards.AsEnumerable();

        if (_filterBerryColor.SelectedIndex > 0)
        {
            var color = Enum.GetValues<BerryEnergyType>().ElementAtOrDefault(_filterBerryColor.SelectedIndex - 1);
            result = result.Where(c => c.BerryTypes.Contains(color));
        }

        return result.ToList();
    }

    private void RefreshDeckPile()
    {
        if (_isMainDeck)
        {
            _deckPile.DeckCards = [.. _editingDeck.MainDeck.Cast<CardComponent>()];
            _deckPile.SetHeader("Main Deck");
        }
        else
        {
            _deckPile.DeckCards = [.. _editingDeck.SideDeck.Cast<CardComponent>()];
            _deckPile.SetHeader("Side Deck");
        }

        _deckPile.FaceCard = _editingDeck.FaceCard;
        _deckPile.RefreshCardList();
    }

    private void OnBrowserCardClicked(CardComponent card)
    {
        if (_isMainDeck && card is PokemonCardComponent pokemonCard)
        {
            _editingDeck.MainDeck.Add(pokemonCard);

            if (_editingDeck.FaceCard is null)
                _editingDeck.FaceCard = pokemonCard;
        }
        else if (!_isMainDeck && card is BerryCardComponent berryCard)
            _editingDeck.SideDeck.Add(berryCard);

        AutoSave();
        ApplyFiltersAndRefresh();
    }

    private void OnFaceCardChanged(CardComponent card)
    {
        if (_deckPile.FaceCard is PokemonCardComponent pc)
            _editingDeck.FaceCard = pc;
        else
            _editingDeck.FaceCard = _editingDeck.MainDeck.FirstOrDefault();

        AutoSave();
    }

    private void OnPileCardClicked(CardComponent card)
    {
        if (_isMainDeck && card is PokemonCardComponent pokemonCard)
        {
            _editingDeck.MainDeck.Remove(pokemonCard);

            if (_editingDeck.FaceCard?.Id == pokemonCard.Id)
                _editingDeck.FaceCard = _editingDeck.MainDeck.FirstOrDefault();
        }
        else if (!_isMainDeck && card is BerryCardComponent berryCard)
            _editingDeck.SideDeck.Remove(berryCard);

        AutoSave();
        ApplyFiltersAndRefresh();
    }

    private void AutoSave()
    {
        _editingDeck.DeckName = _deckNameInput.Text;
        GameStateManager.Save(GameStateManager.ActiveSave);
    }

    private void DeleteDeck()
    {
        var save = GameStateManager.ActiveSave;

        // Can't delete the only deck
        if (save.Decks.Count <= 1)
            return;

        // Return all cards to the pool (they're already in PokemonCards/BerryCards, just remove from deck)
        _editingDeck.MainDeck.Clear();
        _editingDeck.SideDeck.Clear();
        _editingDeck.FaceCard = null;

        save.Decks.Remove(_editingDeck);

        // If this was the active deck, pick another
        if (save.ActiveDeck?.Id == _editingDeck.Id)
            save.ActiveDeck = save.Decks.FirstOrDefault();

        save.EditingDeck = null;
        GameStateManager.Save(save);

        SceneManager.SetActiveScene(SceneType.Decks, new SlideTransition(SlideDirection.Up));
    }

    public override void Update(GameTime gameTime)
    {
        var pressedKeys = ControlState.GetPressedKeys();
        if (pressedKeys.Contains(Keys.Escape))
        {
            AutoSave();
            SceneManager.SetActiveScene(SceneType.Decks, new SlideTransition(SlideDirection.Up));
            return;
        }

        base.Update(gameTime);
    }
}
