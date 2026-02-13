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
    private Button _saveBtn = null!;
    private Panel _pokemonFilterBar = null!;
    private Panel _berryFilterBar = null!;
    private Panel _browserArea = null!;
    private Panel _pileArea = null!;

    // Components (code-built)
    private CardPageBrowser _browser = null!;
    private DeckPile _deckPile = null!;

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
        }

        // Bind XML components
        var btnBack = TryBind<SpriteButton>("btn_back");
        if (btnBack is not null)
        {
            btnBack.OnClicked += () => SceneManager.SetActiveScene(SceneType.Decks, new SlideTransition(SlideDirection.Left));
            btnBack.OnHoveredEnter += () => btnBack.Opacity = 1f;
            btnBack.OnHoveredExit += () => btnBack.Opacity = 0.8f;
        }

        _deckNameInput = Bind<InputField>("deck_name_input");
        _deckNameInput.Text = _editingDeck.DeckName;
        _deckNameInput.OnTextChanged += text => _editingDeck.DeckName = text;

        _tabPokemon = Bind<Button>("tab_pokemon");
        _tabPokemon.OnClicked += () => SwitchTab(true);

        _tabBerry = Bind<Button>("tab_berry");
        _tabBerry.OnClicked += () => SwitchTab(false);

        _saveBtn = Bind<Button>("save_btn");
        _saveBtn.OnClicked += SaveDeck;

        // Bind filter bars
        _pokemonFilterBar = Bind<Panel>("pokemon_filter_bar");
        _berryFilterBar = Bind<Panel>("berry_filter_bar");

        // Bind layout areas
        _browserArea = Bind<Panel>("browser_area");
        _pileArea = Bind<Panel>("pile_area");

        // Bind and populate Pokemon filters
        BindPokemonFilters();

        // Bind and populate Berry filters
        BindBerryFilters();

        // Build dynamic components using XML-defined positions
        BuildDynamicUI();
    }

    private void BindPokemonFilters()
    {
        // Type filter
        _filterType = Bind<Dropdown>("filter_type");
        var typeItems = new List<string> { "All Types" };
        typeItems.AddRange(Enum.GetValues<PokemonType>().Where(t => t != PokemonType.UNK).Select(t => t.ToString()));
        _filterType.Items = typeItems;
        _filterType.SelectedIndex = 0;
        _filterType.OnSelectionChanged += _ => ApplyFiltersAndRefresh();

        // Generation filter
        _filterGen = Bind<Dropdown>("filter_gen");
        var genItems = new List<string> { "All Gens" };
        genItems.AddRange(Enumerable.Range(1, 9).Select(g => $"Gen {g}"));
        _filterGen.Items = genItems;
        _filterGen.SelectedIndex = 0;
        _filterGen.OnSelectionChanged += _ => ApplyFiltersAndRefresh();

        // Checkboxes
        _filterMega = Bind<Checkbox>("filter_mega");
        _filterMega.OnCheckedChanged += () => ApplyFiltersAndRefresh();

        _filterGmax = Bind<Checkbox>("filter_gmax");
        _filterGmax.OnCheckedChanged += () => ApplyFiltersAndRefresh();

        _filterShiny = Bind<Checkbox>("filter_shiny");
        _filterShiny.OnCheckedChanged += () => ApplyFiltersAndRefresh();

        // Card level filter
        _filterCardLevel = Bind<Dropdown>("filter_lvl");
        var lvlItems = new List<string> { "All Levels" };
        lvlItems.AddRange(Enumerable.Range(1, 10).Select(l => $"Lv. {l}"));
        _filterCardLevel.Items = lvlItems;
        _filterCardLevel.SelectedIndex = 0;
        _filterCardLevel.OnSelectionChanged += _ => ApplyFiltersAndRefresh();

        // Innate power filter
        _filterInnatePower = Bind<Dropdown>("filter_innate");
        var innateItems = new List<string> { "All Innate" };
        innateItems.AddRange(Enumerable.Range(0, 6).Select(i => $"Innate {i}"));
        _filterInnatePower.Items = innateItems;
        _filterInnatePower.SelectedIndex = 0;
        _filterInnatePower.OnSelectionChanged += _ => ApplyFiltersAndRefresh();

        // Berry cost total filter
        _filterCostTotal = Bind<Dropdown>("filter_cost_total");
        var costTotalItems = new List<string> { "Any Cost" };
        costTotalItems.AddRange(Enumerable.Range(1, 6).Select(c => $"{c} Cost"));
        _filterCostTotal.Items = costTotalItems;
        _filterCostTotal.SelectedIndex = 0;
        _filterCostTotal.OnSelectionChanged += _ => ApplyFiltersAndRefresh();

        // Berry cost color filter
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

        // Refresh when returning to this scene
        var save = GameStateManager.ActiveSave;
        if (save.EditingDeck is not null)
            _editingDeck = save.EditingDeck;

        ApplyFiltersAndRefresh();
    }

    private void BuildDynamicUI()
    {
        // Card page browser — use XML-defined browser_area for position/size
        var browserPos = _browserArea.Position;
        var browserSize = _browserArea.Size;

        _browser = new CardPageBrowser("deck_browser")
        {
            Position = browserPos,
            Size = browserSize,
            Columns = 6,
            Rows = 3,
            CardScale = new Vector2(4, 4),
            CardSpacing = new Vector2(20, 20),
        };
        _browser.OnCardClicked += OnBrowserCardClicked;
        _browser.BuildLayout();
        AddComponent(_browser);

        // Deck pile — use XML-defined pile_area for position/size
        var pilePos = _pileArea.Position;
        var pileSize = _pileArea.Size;

        _deckPile = new DeckPile("deck_pile")
        {
            Position = pilePos,
            Size = pileSize,
        };
        _deckPile.OnCardClicked += OnPileCardClicked;
        _deckPile.BuildLayout();
        AddComponent(_deckPile);

        // Initial load
        ApplyFiltersAndRefresh();
    }

    private void SwitchTab(bool isMainDeck)
    {
        if (_isMainDeck == isMainDeck)
            return;

        _isMainDeck = isMainDeck;

        // Toggle filter bar visibility
        _pokemonFilterBar.Show = _isMainDeck;
        _berryFilterBar.Show = !_isMainDeck;

        // Update tab visuals
        if (_isMainDeck)
        {
            _tabPokemon.Background = new Color(80, 90, 120);
            _tabPokemon.TextColor = Color.White;
            _tabPokemon.Border = new Border(2, new Color(120, 140, 180));
            _tabBerry.Background = new Color(45, 50, 62);
            _tabBerry.TextColor = new Color(170, 170, 180);
            _tabBerry.Border = new Border(2, new Color(60, 65, 75));
        }
        else
        {
            _tabBerry.Background = new Color(80, 90, 120);
            _tabBerry.TextColor = Color.White;
            _tabBerry.Border = new Border(2, new Color(120, 140, 180));
            _tabPokemon.Background = new Color(45, 50, 62);
            _tabPokemon.TextColor = new Color(170, 170, 180);
            _tabPokemon.Border = new Border(2, new Color(60, 65, 75));
        }

        // Update deck pile header
        _deckPile.SetHeader(_isMainDeck ? "Pokemon Deck" : "Berry Deck");

        // Refresh deck pile cards for current mode
        RefreshDeckPile();

        // Refresh browser
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
        return save.PokemonCards.Where(c => !deckCardIds.Contains(c.Id)).ToList();
    }

    private List<BerryCardComponent> GetAvailableBerryCards()
    {
        var save = GameStateManager.ActiveSave;
        var deckCardIds = new HashSet<Guid>(_editingDeck.SideDeck.Select(c => c.Id));
        return save.BerryCards.Where(c => !deckCardIds.Contains(c.Id)).ToList();
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
            _deckPile.DeckCards = _editingDeck.MainDeck.Cast<CardComponent>().ToList();
            _deckPile.SetHeader("Pokemon Deck");
        }
        else
        {
            _deckPile.DeckCards = _editingDeck.SideDeck.Cast<CardComponent>().ToList();
            _deckPile.SetHeader("Berry Deck");
        }

        if (_isMainDeck && _editingDeck.FaceCard is not null)
            _deckPile.FeaturedCard = _editingDeck.FaceCard;

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

        ApplyFiltersAndRefresh();
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

        ApplyFiltersAndRefresh();
    }

    private void SaveDeck()
    {
        var save = GameStateManager.ActiveSave;

        _editingDeck.DeckName = _deckNameInput.Text;

        if (!save.Decks.Any(d => d.Id == _editingDeck.Id))
            save.Decks.Add(_editingDeck);

        save.EditingDeck = null;
        GameStateManager.Save(save);

        SceneManager.SetActiveScene(SceneType.Decks, new SlideTransition(SlideDirection.Left));
    }

    public override void Update(GameTime gameTime)
    {
        var pressedKeys = ControlState.GetPressedKeys();
        if (pressedKeys.Contains(Keys.Escape))
        {
            SceneManager.SetActiveScene(SceneType.Decks, new SlideTransition(SlideDirection.Left));
            return;
        }

        base.Update(gameTime);
    }
}
