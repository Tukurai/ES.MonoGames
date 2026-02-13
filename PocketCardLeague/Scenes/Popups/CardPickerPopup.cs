using Components;
using Helpers;
using Microsoft.Xna.Framework;
using PocketCardLeague.Components;
using PocketCardLeague.Consts;
using PocketCardLeague.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketCardLeague.Scenes.Popups;

public class CardPickerPopup : Popup
{
    private readonly string _prompt;
    private readonly List<CardComponent> _allCards;
    private CardComponent? _selectedCard;
    private CardPageBrowser? _browser;
    private Panel? _filterBarPanel;

    // Filters
    private Dropdown? _filterType;
    private Dropdown? _filterGen;

    public CardPickerPopup(string prompt, List<CardComponent> cards) : base("card_picker_popup")
    {
        _prompt = prompt;
        _allCards = cards;
        Title = "Pick a Card";
        ContentSize = new Vector2(1000, 700);
    }

    protected override void BuildContent(Panel contentPanel)
    {
        var promptLabel = new BitmapLabel("prompt_label")
        {
            Text = _prompt,
            FontFamily = "m3x6.ttf",
            FontSize = 20,
            TextColor = Color.White,
            Position = new Anchor(new Vector2(16, 48), contentPanel.Position),
        };
        contentPanel.Children.Add(promptLabel);

        // Filter bar
        _filterBarPanel = new Panel("picker_filter_bar")
        {
            Position = new Anchor(new Vector2(16, 80), contentPanel.Position),
            Size = new Vector2(ContentSize.X - 32, 44),
            Background = Color.Transparent,
        };
        contentPanel.Children.Add(_filterBarPanel);
        BuildFilters();

        // Card browser
        _browser = new CardPageBrowser("picker_browser")
        {
            Position = new Anchor(new Vector2(16, 130), contentPanel.Position),
            Size = new Vector2(ContentSize.X - 32, 500),
            Columns = 4,
            Rows = 2,
            CardScale = new Vector2(3, 3),
            CardSpacing = new Vector2(16, 16),
        };
        _browser.OnCardClicked += card =>
        {
            _selectedCard = card;
        };
        _browser.BuildLayout();
        _browser.SetCards(_allCards);
        contentPanel.Children.Add(_browser);
    }

    private void BuildFilters()
    {
        if (_filterBarPanel is null)
            return;

        float x = 0;
        var dropdownSpacing = 12;

        // Type filter
        var typeItems = new List<string> { "All Types" };
        typeItems.AddRange(Enum.GetValues<PokemonType>().Where(t => t != PokemonType.UNK).Select(t => t.ToString()));
        _filterType = new Dropdown("picker_filter_type")
        {
            Position = new Anchor(new Vector2(x, 4), _filterBarPanel.Position),
            Size = new Vector2(140, 32),
            Items = typeItems,
            SelectedIndex = 0,
            Background = new Color(40, 45, 55),
            BackgroundHovered = new Color(50, 55, 68),
            TextColor = Color.White,
            Border = new Border(2, new Color(60, 65, 75)),
            FocusedBorder = new Border(2, new Color(100, 120, 160)),
            ListBackground = new Color(35, 38, 48),
            ItemHoveredBackground = new Color(60, 68, 85),
            ListBorder = new Border(2, new Color(70, 75, 90)),
            Padding = 6,
            MaxVisibleItems = 8,
        };
        _filterType.OnSelectionChanged += _ => ApplyFilters();
        _filterBarPanel.Children.Add(_filterType);
        x += 140 + dropdownSpacing;

        // Generation filter
        var genItems = new List<string> { "All Gens" };
        genItems.AddRange(Enumerable.Range(1, 9).Select(g => $"Gen {g}"));
        _filterGen = new Dropdown("picker_filter_gen")
        {
            Position = new Anchor(new Vector2(x, 4), _filterBarPanel.Position),
            Size = new Vector2(110, 32),
            Items = genItems,
            SelectedIndex = 0,
            Background = new Color(40, 45, 55),
            BackgroundHovered = new Color(50, 55, 68),
            TextColor = Color.White,
            Border = new Border(2, new Color(60, 65, 75)),
            FocusedBorder = new Border(2, new Color(100, 120, 160)),
            ListBackground = new Color(35, 38, 48),
            ItemHoveredBackground = new Color(60, 68, 85),
            ListBorder = new Border(2, new Color(70, 75, 90)),
            Padding = 6,
            MaxVisibleItems = 8,
        };
        _filterGen.OnSelectionChanged += _ => ApplyFilters();
        _filterBarPanel.Children.Add(_filterGen);
    }

    private void ApplyFilters()
    {
        var filtered = _allCards.AsEnumerable();

        if (_filterType is not null && _filterType.SelectedIndex > 0)
        {
            var selectedType = Enum.GetValues<PokemonType>().Where(t => t != PokemonType.UNK).ElementAtOrDefault(_filterType.SelectedIndex - 1);
            filtered = filtered.Where(c => c is PokemonCardComponent pc && pc.Card?.BasePokemon?.Types.Contains(selectedType) == true);
        }

        if (_filterGen is not null && _filterGen.SelectedIndex > 0)
        {
            var gen = _filterGen.SelectedIndex;
            filtered = filtered.Where(c => c is PokemonCardComponent pc && pc.Card?.BasePokemon?.Generation == gen);
        }

        _browser?.SetCards(filtered.ToList());
    }

    protected override PopupResult BuildResult(bool confirmed)
    {
        return new PopupResult
        {
            Confirmed = confirmed,
            Value = _selectedCard,
        };
    }
}
