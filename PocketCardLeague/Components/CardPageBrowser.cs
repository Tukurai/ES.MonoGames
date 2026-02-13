using Components;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PocketCardLeague.Consts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketCardLeague.Components;

/// <summary>
/// A reusable paginated card browser that displays cards in a grid with page navigation.
/// Cards are grouped by PokeDex ID (or card name for berries); each slot cycles through variants.
/// </summary>
public class CardPageBrowser : Panel
{
    public int Columns { get; set; } = 6;
    public int Rows { get; set; } = 3;
    public int CardsPerPage => Columns * Rows;
    public Vector2 CardScale { get; set; } = new(4, 4);
    public Vector2 CardSpacing { get; set; } = new(20, 20);

    public int CurrentPage
    {
        get;
        set
        {
            field = Math.Clamp(value, 0, Math.Max(0, TotalPages - 1));
            RefreshPage();
        }
    }

    public int TotalPages => _cardGroups.Count == 0 ? 1 : (int)Math.Ceiling((double)_cardGroups.Count / CardsPerPage);

    public event Action<CardComponent>? OnCardClicked;

    private List<List<CardComponent>> _cardGroups = [];
    private readonly Dictionary<int, int> _variantIndex = [];
    private readonly List<BaseComponent> _gridChildren = [];

    private BitmapLabel? _pageLabel;
    private SpriteButton? _btnPrev;
    private SpriteButton? _btnNext;
    private Slider? _pageSlider;
    private Panel? _gridPanel;

    public CardPageBrowser(string name = "card_page_browser") : base(name)
    {
        Background = Color.Transparent;
    }

    /// <summary>
    /// Call after setting Size, CardScale, etc. to build the internal UI.
    /// </summary>
    public void BuildLayout()
    {
        Children.Clear();
        _gridChildren.Clear();

        var cardW = 57 * CardScale.X;
        var cardH = 80 * CardScale.Y;
        var gridW = Columns * (cardW + CardSpacing.X) - CardSpacing.X;
        var gridH = Rows * (cardH + CardSpacing.Y) - CardSpacing.Y;

        // Grid panel
        _gridPanel = new Panel("browser_grid")
        {
            Position = new Anchor(new Vector2(0, 0), Position),
            Size = new Vector2(gridW, gridH),
            Background = Color.Transparent,
        };
        Children.Add(_gridPanel);

        // Navigation bar below the grid
        var navY = gridH + 20;

        // Previous page button
        var arrowResult = ContentHelper.GetTextureResult<SpriteMaps.ArrowsSpriteAtlas>("Arrow_left");
        var arrowActiveResult = ContentHelper.GetTextureResult<SpriteMaps.ArrowsSpriteAtlas>("Arrow_left_active");
        if (arrowResult is not null)
        {
            _btnPrev = new SpriteButton("browser_prev")
            {
                Position = new Anchor(new Vector2(0, navY), Position),
                Scale = new Vector2(3, 3),
                Opacity = 0.8f,
            };
            _btnPrev.SetNormalSprite(arrowResult);
            if (arrowActiveResult is not null)
                _btnPrev.SetPressedSprite(arrowActiveResult);
            _btnPrev.OnClicked += () => { if (CurrentPage > 0) CurrentPage--; };
            _btnPrev.OnHoveredEnter += () => _btnPrev.Opacity = 1f;
            _btnPrev.OnHoveredExit += () => _btnPrev.Opacity = 0.8f;
            Children.Add(_btnPrev);
        }

        // Next page button
        var arrowRightResult = ContentHelper.GetTextureResult<SpriteMaps.ArrowsSpriteAtlas>("Arrow_right");
        var arrowRightActiveResult = ContentHelper.GetTextureResult<SpriteMaps.ArrowsSpriteAtlas>("Arrow_right_active");
        if (arrowRightResult is not null)
        {
            _btnNext = new SpriteButton("browser_next")
            {
                Position = new Anchor(new Vector2(gridW - 80, navY), Position),
                Scale = new Vector2(3, 3),
                Opacity = 0.8f,
            };
            _btnNext.SetNormalSprite(arrowRightResult);
            if (arrowRightActiveResult is not null)
                _btnNext.SetPressedSprite(arrowRightActiveResult);
            _btnNext.OnClicked += () => { if (CurrentPage < TotalPages - 1) CurrentPage++; };
            _btnNext.OnHoveredEnter += () => _btnNext.Opacity = 1f;
            _btnNext.OnHoveredExit += () => _btnNext.Opacity = 0.8f;
            Children.Add(_btnNext);
        }

        // Page slider in the middle
        _pageSlider = new Slider("browser_slider")
        {
            Position = new Anchor(new Vector2(100, navY + 10), Position),
            Size = new Vector2(gridW - 280, 30),
            MinValue = 0,
            MaxValue = Math.Max(0, TotalPages - 1),
            Value = 0,
            TrackColor = new Color(60, 65, 75),
            TrackFillColor = new Color(100, 120, 160),
            ThumbColor = new Color(140, 160, 200),
            ThumbHoveredColor = new Color(170, 190, 230),
            ThumbSize = new Vector2(20, 30),
        };
        _pageSlider.OnValueChanged += v =>
        {
            if (v != CurrentPage)
                CurrentPage = v;
        };
        Children.Add(_pageSlider);

        // Page label
        _pageLabel = new BitmapLabel("browser_page_label")
        {
            Text = "Page 1 / 1",
            FontFamily = Fonts.M6x11,
            FontSize = 28,
            TextColor = Color.White,
            Border = new Border(2, new Color(25, 25, 25, 170)),
            Position = new Anchor(new Vector2(gridW - 170, navY + 4), Position),
        };
        Children.Add(_pageLabel);

        RefreshPage();
    }

    /// <summary>
    /// Sets the cards to display, grouped by Pokedex ID (pokemon) or card name (berry).
    /// Resets to page 0.
    /// </summary>
    public void SetCards(List<CardComponent> cards)
    {
        // Group cards: pokemon by dex id, berries by card name
        _cardGroups = cards
            .GroupBy(c => c is PokemonCardComponent pc
                ? (pc.Card?.BasePokemon?.Id.ToString() ?? pc.CardName)
                : c.CardName)
            .Select(g => g.ToList())
            .OrderBy(g =>
            {
                if (g[0] is PokemonCardComponent pc)
                    return pc.Card?.BasePokemon?.Id ?? 0;
                return 0;
            })
            .ToList<List<CardComponent>>();

        _variantIndex.Clear();

        if (_pageSlider is not null)
            _pageSlider.MaxValue = Math.Max(0, TotalPages - 1);

        CurrentPage = 0;
    }

    /// <summary>
    /// Rebuilds the visible card grid for the current page.
    /// </summary>
    public void RefreshPage()
    {
        if (_gridPanel is null)
            return;

        // Remove old grid children
        foreach (var child in _gridChildren)
            _gridPanel.Children.Remove(child);
        _gridChildren.Clear();

        var cardW = 57 * CardScale.X;
        var cardH = 80 * CardScale.Y;
        var startIndex = CurrentPage * CardsPerPage;

        for (int i = 0; i < CardsPerPage; i++)
        {
            var groupIndex = startIndex + i;
            if (groupIndex >= _cardGroups.Count)
                break;

            var group = _cardGroups[groupIndex];
            if (!_variantIndex.TryGetValue(groupIndex, out var vi))
                vi = 0;
            vi = Math.Clamp(vi, 0, group.Count - 1);
            _variantIndex[groupIndex] = vi;

            var card = group[vi];
            var col = i % Columns;
            var row = i / Columns;

            card.Scale = CardScale;
            card.Position = new Anchor(
                new Vector2(col * (cardW + CardSpacing.X), row * (cardH + CardSpacing.Y)),
                _gridPanel.Position);

            // Rebuild visuals at the new scale
            card.BuildVisuals();

            // Wire click event (remove previous to avoid duplicates)
            var capturedCard = card;
            card.OnClicked += () => OnCardClicked?.Invoke(capturedCard);

            _gridChildren.Add(card);
            _gridPanel.Children.Add(card);

            // If the group has multiple variants, add small navigation arrows
            if (group.Count > 1)
            {
                var capturedGroupIndex = groupIndex;
                var variantLabel = new BitmapLabel($"variant_{groupIndex}")
                {
                    Text = $"{vi + 1}/{group.Count}",
                    FontFamily = Fonts.M3x6,
                    FontSize = 20,
                    TextColor = new Color(200, 200, 200),
                    Border = new Border(2, new Color(25, 25, 25, 200)),
                    Position = new Anchor(
                        new Vector2(col * (cardW + CardSpacing.X), row * (cardH + CardSpacing.Y) + cardH - 24),
                        _gridPanel.Position),
                };
                _gridChildren.Add(variantLabel);
                _gridPanel.Children.Add(variantLabel);

                // Left variant arrow
                var leftBtn = new Button($"var_left_{groupIndex}", "<", Fonts.M3Default)
                {
                    TextColor = Color.White,
                    Background = new Color(40, 45, 55, 180),
                    Padding = 2,
                    Size = new Vector2(24, 24),
                    Position = new Anchor(
                        new Vector2(col * (cardW + CardSpacing.X) - 2, row * (cardH + CardSpacing.Y) + cardH / 2 - 12),
                        _gridPanel.Position),
                };
                leftBtn.OnClicked += () =>
                {
                    var ci = _variantIndex.GetValueOrDefault(capturedGroupIndex, 0);
                    _variantIndex[capturedGroupIndex] = (ci - 1 + _cardGroups[capturedGroupIndex].Count) % _cardGroups[capturedGroupIndex].Count;
                    RefreshPage();
                };
                _gridChildren.Add(leftBtn);
                _gridPanel.Children.Add(leftBtn);

                // Right variant arrow
                var rightBtn = new Button($"var_right_{groupIndex}", ">", Fonts.M3Default)
                {
                    TextColor = Color.White,
                    Background = new Color(40, 45, 55, 180),
                    Padding = 2,
                    Size = new Vector2(24, 24),
                    Position = new Anchor(
                        new Vector2(col * (cardW + CardSpacing.X) + cardW - 22, row * (cardH + CardSpacing.Y) + cardH / 2 - 12),
                        _gridPanel.Position),
                };
                rightBtn.OnClicked += () =>
                {
                    var ci = _variantIndex.GetValueOrDefault(capturedGroupIndex, 0);
                    _variantIndex[capturedGroupIndex] = (ci + 1) % _cardGroups[capturedGroupIndex].Count;
                    RefreshPage();
                };
                _gridChildren.Add(rightBtn);
                _gridPanel.Children.Add(rightBtn);
            }
        }

        // Update page label and slider
        if (_pageLabel is not null)
            _pageLabel.Text = $"Page {CurrentPage + 1} / {TotalPages}";

        if (_pageSlider is not null)
        {
            _pageSlider.MaxValue = Math.Max(0, TotalPages - 1);
            if (_pageSlider.Value != CurrentPage)
                _pageSlider.Value = CurrentPage;
        }

        UpdateNavButtonState();
    }

    private void UpdateNavButtonState()
    {
        if (_btnPrev is not null)
            _btnPrev.IsEnabled = CurrentPage > 0;
        if (_btnNext is not null)
            _btnNext.IsEnabled = CurrentPage < TotalPages - 1;
    }
}
