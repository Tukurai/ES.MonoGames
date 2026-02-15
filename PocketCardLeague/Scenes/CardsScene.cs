using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Components;
using Helpers;
using PocketCardLeague.Components;
using PocketCardLeague.Enums;
using PocketCardLeague.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketCardLeague.Scenes;

public class CardsScene() : XmlScene<SceneType>(SceneType.Cards)
{
    protected override string XmlFileName => "CardsScene.xml";

    private bool _isPokemonTab = true;

    // Components (XML-bound)
    private Button _tabPokemon = null!;
    private Button _tabBerry = null!;
    private Panel _pokemonFilterBar = null!;
    private Panel _berryFilterBar = null!;
    private CardPageBrowser _browser = null!;

    // Tab styles captured from XML initial state
    private (Color Background, Color TextColor, Border? Border) _tabActiveStyle;
    private (Color Background, Color TextColor, Border? Border) _tabInactiveStyle;

    // Pokemon filters
    private Dropdown _filterType = null!;
    private Dropdown _filterGen = null!;
    private Checkbox _filterMega = null!;
    private Checkbox _filterGmax = null!;
    private Checkbox _filterShiny = null!;
    private Dropdown _filterCardLevel = null!;
    private Dropdown _filterInnatePower = null!;
    private Dropdown _filterCostTotal = null!;
    private Dropdown _filterCostColor = null!;

    // Berry filters
    private Dropdown _filterBerryColor = null!;

    protected override void OnBeforeReload()
    {
        _browser?.ClearCardHandlers();
    }

    protected override void OnXmlLoaded()
    {
        var btnUp = Bind<SpriteButton>("btn_up");
        btnUp.OnClicked += () => SceneManager.SetActiveScene(SceneType.Decks, new SlideTransition(SlideDirection.Down));
        btnUp.OnHoveredEnter += () => btnUp.Opacity = 1f;
        btnUp.OnHoveredExit += () => btnUp.Opacity = 0.8f;

        _tabPokemon = Bind<Button>("tab_pokemon");
        _tabPokemon.OnClicked += () => SwitchTab(true);

        _tabBerry = Bind<Button>("tab_berry");
        _tabBerry.OnClicked += () => SwitchTab(false);

        _tabActiveStyle = (_tabPokemon.Background, _tabPokemon.TextColor, _tabPokemon.Border);
        _tabInactiveStyle = (_tabBerry.Background, _tabBerry.TextColor, _tabBerry.Border);

        _pokemonFilterBar = Bind<Panel>("pokemon_filter_bar");
        _berryFilterBar = Bind<Panel>("berry_filter_bar");

        _browser = Bind<CardPageBrowser>("card_browser");
        _browser.BuildLayout();

        BindPokemonFilters();
        BindBerryFilters();
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
        ApplyFiltersAndRefresh();
    }

    private void SwitchTab(bool isPokemon)
    {
        if (_isPokemonTab == isPokemon)
            return;

        _isPokemonTab = isPokemon;

        _pokemonFilterBar.Show = _isPokemonTab;
        _berryFilterBar.Show = !_isPokemonTab;

        if (_isPokemonTab)
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

        ApplyFiltersAndRefresh();
    }

    private void ApplyFiltersAndRefresh()
    {
        var save = GameStateManager.ActiveSave;

        if (_isPokemonTab)
        {
            var cards = save.PokemonCards.ToList();
            var filtered = ApplyPokemonFilters(cards);
            _browser.SetCards(filtered.Cast<CardComponent>().ToList());
        }
        else
        {
            var cards = save.BerryCards.ToList();
            var filtered = ApplyBerryFilters(cards);
            _browser.SetCards(filtered.Cast<CardComponent>().ToList());
        }
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

    public override void Update(GameTime gameTime)
    {
        var pressedKeys = ControlState.GetPressedKeys();
        if (pressedKeys.Contains(Keys.Up))
        {
            SceneManager.SetActiveScene(SceneType.Decks, new SlideTransition(SlideDirection.Down));
            return;
        }

        base.Update(gameTime);
    }
}
