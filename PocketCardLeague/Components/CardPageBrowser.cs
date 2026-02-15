using Components;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PocketCardLeague.Consts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketCardLeague.Components;

/// <summary>
/// A reusable paginated card browser that displays cards in a grid with page navigation.
/// Cards are grouped by PokeDex ID (or card name for berries); each slot cycles through variants.
/// Structural children (browser_slider, browser_page_label) can be defined in XML as nested children.
/// </summary>
public class CardPageBrowser : Panel
{
    public int Columns { get; set; } = 6;
    public int Rows { get; set; } = 3;
    public int CardsPerPage => Columns * Rows;
    public Vector2 CardScale { get; set; } = new(4, 4);
    public Vector2 CardSpacing { get; set; } = new(20, 20);

    // Navigation layout
    public float NavGap { get; set; } = 20;
    public Vector2 ArrowScale { get; set; } = new(3, 3);
    public float ArrowOpacity { get; set; } = 0.8f;
    public Vector2 SliderOffset { get; set; } = new(100, 10);

    // Variant label (template for dynamic per-card variant indicators)
    public string VariantLabelFont { get; set; } = Fonts.M3x6;
    public int VariantLabelFontSize { get; set; } = 20;
    public Color VariantLabelColor { get; set; } = new(200, 200, 200);
    public Border? VariantLabelBorder { get; set; } = new Border(2, new Color(25, 25, 25, 200));

    // Variant buttons (SpriteButton arrow sprites)
    public string VariantPrevSprite { get; set; } = "Arrows.Arrow_left_small";
    public string VariantNextSprite { get; set; } = "Arrows.Arrow_right_small";
    public string VariantPrevActiveSprite { get; set; } = "Arrows.Arrow_left_small_active";
    public string VariantNextActiveSprite { get; set; } = "Arrows.Arrow_right_small_active";
    public Vector2 VariantBtnScale { get; set; } = new(3, 3);
    public Vector2 VariantBtnOffset { get; set; } = new(-2, 0);
    public Vector2 VariantLabelOffset { get; set; } = new(0, -24);

    // Variant name label (displayed on each card)
    public bool ShowVariantName { get; set; } = true;
    public string VariantNameFont { get; set; } = Fonts.M3x6;
    public int VariantNameFontSize { get; set; } = 16;
    public Color VariantNameColor { get; set; } = Color.White;
    public Border? VariantNameBorder { get; set; } = new Border(2, new Color(25, 25, 25, 200));
    public Vector2 VariantNameOffset { get; set; } = new(0, -12);

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
    private readonly Dictionary<CardComponent, Action> _clickHandlers = [];
    private readonly List<SpriteButton> _variantButtons = [];
    private readonly Dictionary<int, Rectangle> _cardAreas = [];

    private BitmapLabel? _pageLabel;
    private SpriteButton? _btnPrev;
    private SpriteButton? _btnNext;
    private Slider? _pageSlider;
    private Panel? _gridPanel;

    // Track which children were found from XML (don't override their Position/Size)
    private bool _prevFromXml;
    private bool _nextFromXml;
    private bool _sliderFromXml;
    private bool _pageLabelFromXml;

    public CardPageBrowser(string name = "card_page_browser") : base(name)
    {
        Background = Color.Transparent;
    }

    /// <summary>
    /// Unsubscribes all click handlers from card instances.
    /// Must be called before the browser is abandoned (e.g. on scene reload)
    /// to prevent stale handlers on persistent card objects.
    /// </summary>
    public void ClearCardHandlers()
    {
        foreach (var (card, handler) in _clickHandlers)
            card.OnClicked -= handler;
        _clickHandlers.Clear();
    }

    private T? FindChild<T>(string name) where T : BaseComponent
        => Children.OfType<T>().FirstOrDefault(c => c.Name == name);

    /// <summary>
    /// Call after setting Size, CardScale, etc. to build the internal UI.
    /// Finds named children (browser_prev, browser_next, browser_slider, browser_page_label)
    /// from XML or creates defaults. XML-found children keep their Position/Size.
    /// </summary>
    public void BuildLayout()
    {
        _gridChildren.Clear();

        // Remove previously created internal components (not XML children)
        if (_gridPanel is not null)
            Children.Remove(_gridPanel);
        if (_btnPrev is not null && !_prevFromXml)
            Children.Remove(_btnPrev);
        if (_btnNext is not null && !_nextFromXml)
            Children.Remove(_btnNext);

        var cardW = 57 * CardScale.X;
        var cardH = 80 * CardScale.Y;
        var gridW = Columns * (cardW + CardSpacing.X) - CardSpacing.X;
        var gridH = Rows * (cardH + CardSpacing.Y) - CardSpacing.Y;

        // Grid panel (always created internally)
        _gridPanel = new Panel("browser_grid")
        {
            Position = new Anchor(new Vector2(0, 0), Position),
            Size = new Vector2(gridW, gridH),
            Background = Color.Transparent,
        };
        Children.Add(_gridPanel);

        // Navigation bar below the grid
        var navY = gridH + NavGap;

        // Find or create previous page button
        _prevFromXml = false;
        _btnPrev = FindChild<SpriteButton>("browser_prev");
        if (_btnPrev is not null)
        {
            _prevFromXml = true;
        }
        else
        {
            var arrowResult = ContentHelper.GetTextureResult<SpriteMaps.ArrowsSpriteAtlas>("Arrow_left");
            var arrowActiveResult = ContentHelper.GetTextureResult<SpriteMaps.ArrowsSpriteAtlas>("Arrow_left_active");
            if (arrowResult is not null)
            {
                _btnPrev = new SpriteButton("browser_prev")
                {
                    Position = new Anchor(new Vector2(0, navY), Position),
                    Scale = ArrowScale,
                    Opacity = ArrowOpacity,
                };
                _btnPrev.SetNormalSprite(arrowResult);
                if (arrowActiveResult is not null)
                    _btnPrev.SetPressedSprite(arrowActiveResult);
                Children.Add(_btnPrev);
            }
        }
        if (_btnPrev is not null)
        {
            _btnPrev.OnClicked += () => { if (CurrentPage > 0) CurrentPage--; };
            _btnPrev.OnHoveredEnter += () => _btnPrev.Opacity = 1f;
            _btnPrev.OnHoveredExit += () => _btnPrev.Opacity = ArrowOpacity;
        }

        // Find or create next page button
        _nextFromXml = false;
        _btnNext = FindChild<SpriteButton>("browser_next");
        if (_btnNext is not null)
        {
            _nextFromXml = true;
        }
        else
        {
            var arrowRightResult = ContentHelper.GetTextureResult<SpriteMaps.ArrowsSpriteAtlas>("Arrow_right");
            var arrowRightActiveResult = ContentHelper.GetTextureResult<SpriteMaps.ArrowsSpriteAtlas>("Arrow_right_active");
            if (arrowRightResult is not null)
            {
                _btnNext = new SpriteButton("browser_next")
                {
                    Position = new Anchor(new Vector2(gridW - 80, navY), Position),
                    Scale = ArrowScale,
                    Opacity = ArrowOpacity,
                };
                _btnNext.SetNormalSprite(arrowRightResult);
                if (arrowRightActiveResult is not null)
                    _btnNext.SetPressedSprite(arrowRightActiveResult);
                Children.Add(_btnNext);
            }
        }
        if (_btnNext is not null)
        {
            _btnNext.OnClicked += () => { if (CurrentPage < TotalPages - 1) CurrentPage++; };
            _btnNext.OnHoveredEnter += () => _btnNext.Opacity = 1f;
            _btnNext.OnHoveredExit += () => _btnNext.Opacity = ArrowOpacity;
        }

        // Find or create page slider
        _sliderFromXml = false;
        _pageSlider = FindChild<Slider>("browser_slider");
        if (_pageSlider is not null)
        {
            _sliderFromXml = true;
        }
        else
        {
            _pageSlider = new Slider("browser_slider")
            {
                TrackColor = new Color(60, 65, 75),
                TrackFillColor = new Color(100, 120, 160),
                ThumbColor = new Color(140, 160, 200),
                ThumbHoveredColor = new Color(170, 190, 230),
                ThumbSize = new Vector2(20, 30),
            };
            Children.Add(_pageSlider);
        }

        if (!_sliderFromXml)
        {
            var sliderW = gridW - 280;
            _pageSlider.Position = new Anchor(new Vector2(SliderOffset.X, navY + SliderOffset.Y), Position);
            _pageSlider.Size = new Vector2(sliderW, 30);
        }
        _pageSlider.MinValue = 0;
        _pageSlider.MaxValue = Math.Max(0, TotalPages - 1);
        _pageSlider.Value = 0;
        _pageSlider.OnValueChanged += v =>
        {
            if (v != CurrentPage)
                CurrentPage = v;
        };

        // Find or create page label
        _pageLabelFromXml = false;
        _pageLabel = FindChild<BitmapLabel>("browser_page_label");
        if (_pageLabel is not null)
        {
            _pageLabelFromXml = true;
        }
        else
        {
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
        }

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
        _variantButtons.Clear();
        _cardAreas.Clear();

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

            // Track card area for scroll-to-browse
            var cardPos = card.Position.GetVector2();
            _cardAreas[groupIndex] = new Rectangle((int)cardPos.X, (int)cardPos.Y, (int)cardW, (int)cardH);

            // Wire click event — skip if a variant button is hovered (prevents unintended deck-add)
            var capturedCard = card;
            if (_clickHandlers.TryGetValue(card, out var oldHandler))
                card.OnClicked -= oldHandler;
            Action handler = () =>
            {
                if (_variantButtons.Any(b => b.Hovered))
                    return;
                OnCardClicked?.Invoke(capturedCard);
            };
            card.OnClicked += handler;
            _clickHandlers[card] = handler;

            _gridChildren.Add(card);
            _gridPanel.Children.Add(card);

            // Variant name label on each card
            if (ShowVariantName)
            {
                var nameLabel = new BitmapLabel($"varname_{groupIndex}")
                {
                    Text = card.CardName,
                    FontFamily = VariantNameFont,
                    FontSize = VariantNameFontSize,
                    TextColor = VariantNameColor,
                    Border = VariantNameBorder,
                    Position = new Anchor(
                        new Vector2(
                            col * (cardW + CardSpacing.X) + VariantNameOffset.X,
                            row * (cardH + CardSpacing.Y) + VariantNameOffset.Y),
                        _gridPanel.Position),
                };
                _gridChildren.Add(nameLabel);
                _gridPanel.Children.Add(nameLabel);
            }

            // If the group has multiple variants, add small navigation arrows
            if (group.Count > 1)
            {
                var capturedGroupIndex = groupIndex;
                var variantLabel = new BitmapLabel($"variant_{groupIndex}")
                {
                    Text = $"{vi + 1}/{group.Count}",
                    FontFamily = VariantLabelFont,
                    FontSize = VariantLabelFontSize,
                    TextColor = VariantLabelColor,
                    Border = VariantLabelBorder,
                    Position = new Anchor(
                        new Vector2(
                            col * (cardW + CardSpacing.X) + VariantLabelOffset.X,
                            row * (cardH + CardSpacing.Y) + cardH + VariantLabelOffset.Y),
                        _gridPanel.Position),
                };
                _gridChildren.Add(variantLabel);
                _gridPanel.Children.Add(variantLabel);

                // Left variant arrow (SpriteButton)
                var leftSprite = SceneLoader.ParseSprite(VariantPrevSprite);
                var leftActiveSprite = SceneLoader.ParseSprite(VariantPrevActiveSprite);
                if (leftSprite is not null)
                {
                    var leftBtn = new SpriteButton($"var_left_{groupIndex}")
                    {
                        Scale = VariantBtnScale,
                        Position = new Anchor(
                            new Vector2(
                                col * (cardW + CardSpacing.X) + VariantBtnOffset.X,
                                row * (cardH + CardSpacing.Y) + cardH / 2 - (leftSprite.AtlasEntry.FrameHeight * VariantBtnScale.Y / 2) + VariantBtnOffset.Y),
                            _gridPanel.Position),
                    };
                    leftBtn.SetNormalSprite(leftSprite);
                    if (leftActiveSprite is not null)
                        leftBtn.SetPressedSprite(leftActiveSprite);
                    leftBtn.OnClicked += () =>
                    {
                        var ci = _variantIndex.GetValueOrDefault(capturedGroupIndex, 0);
                        _variantIndex[capturedGroupIndex] = (ci - 1 + _cardGroups[capturedGroupIndex].Count) % _cardGroups[capturedGroupIndex].Count;
                        RefreshPage();
                    };
                    _variantButtons.Add(leftBtn);
                    _gridChildren.Add(leftBtn);
                    _gridPanel.Children.Add(leftBtn);
                }

                // Right variant arrow (SpriteButton)
                var rightSprite = SceneLoader.ParseSprite(VariantNextSprite);
                var rightActiveSprite = SceneLoader.ParseSprite(VariantNextActiveSprite);
                if (rightSprite is not null)
                {
                    var rightBtn = new SpriteButton($"var_right_{groupIndex}")
                    {
                        Scale = VariantBtnScale,
                        Position = new Anchor(
                            new Vector2(
                                col * (cardW + CardSpacing.X) + cardW - (rightSprite.AtlasEntry.FrameWidth * VariantBtnScale.X) - VariantBtnOffset.X,
                                row * (cardH + CardSpacing.Y) + cardH / 2 - (rightSprite.AtlasEntry.FrameHeight * VariantBtnScale.Y / 2) + VariantBtnOffset.Y),
                            _gridPanel.Position),
                    };
                    rightBtn.SetNormalSprite(rightSprite);
                    if (rightActiveSprite is not null)
                        rightBtn.SetPressedSprite(rightActiveSprite);
                    rightBtn.OnClicked += () =>
                    {
                        var ci = _variantIndex.GetValueOrDefault(capturedGroupIndex, 0);
                        _variantIndex[capturedGroupIndex] = (ci + 1) % _cardGroups[capturedGroupIndex].Count;
                        RefreshPage();
                    };
                    _variantButtons.Add(rightBtn);
                    _gridChildren.Add(rightBtn);
                    _gridPanel.Children.Add(rightBtn);
                }
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

    public override void Update(GameTime gameTime)
    {
        var pressed = ControlState.GetPressedKeys();
        if (pressed.Contains(Keys.Left) || pressed.Contains(Keys.A))
        {
            if (CurrentPage > 0) CurrentPage--;
        }
        else if (pressed.Contains(Keys.Right) || pressed.Contains(Keys.D))
        {
            if (CurrentPage < TotalPages - 1) CurrentPage++;
        }

        // Scroll wheel cycles variants when hovering a card with multiple variants
        var scrollDelta = ControlState.GetScrollWheelDelta();
        if (scrollDelta != 0)
        {
            foreach (var (groupIndex, area) in _cardAreas)
            {
                if (groupIndex < _cardGroups.Count
                    && _cardGroups[groupIndex].Count > 1
                    && ControlState.MouseInArea(area))
                {
                    var ci = _variantIndex.GetValueOrDefault(groupIndex, 0);
                    var count = _cardGroups[groupIndex].Count;
                    if (scrollDelta > 0)
                        _variantIndex[groupIndex] = (ci + 1) % count;
                    else
                        _variantIndex[groupIndex] = (ci - 1 + count) % count;
                    RefreshPage();
                    break;
                }
            }
        }

        base.Update(gameTime);
    }

    private void UpdateNavButtonState()
    {
        if (_btnPrev is not null)
            _btnPrev.IsEnabled = CurrentPage > 0;
        if (_btnNext is not null)
            _btnNext.IsEnabled = CurrentPage < TotalPages - 1;
    }
}
