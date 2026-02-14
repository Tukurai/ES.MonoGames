using Components;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PocketCardLeague.Consts;
using PocketCardLeague.SpriteMaps;
using System;
using System.Collections.Generic;

namespace PocketCardLeague.Components;

/// <summary>
/// A scrollable deck pile panel showing cards in a vertical list with a featured card display.
/// Used in the DeckBuilder to show the current deck contents.
/// </summary>
public class DeckPile : Panel
{
    public List<CardComponent> DeckCards { get; set; } = [];
    public CardComponent? FeaturedCard { get; set; }

    public event Action<CardComponent>? OnCardClicked;
    public event Action<CardComponent>? OnCardFeatured;

    // Header
    public string HeaderText { get; set; } = "Deck";
    public string HeaderFont { get; set; } = Fonts.M6x11;
    public int HeaderFontSize { get; set; } = 28;
    public Color HeaderColor { get; set; } = Color.White;
    public float HeaderOffsetY { get; set; } = 8;

    // Count label
    public string CountFont { get; set; } = Fonts.M3x6;
    public int CountFontSize { get; set; } = 20;
    public Color CountColor { get; set; } = new(170, 170, 180);
    public float CountOffsetY { get; set; } = 38;

    // Scroll panel
    public Color ScrollPanelBg { get; set; } = new(30, 33, 42);
    public Border? ScrollPanelBorder { get; set; } = new Border(2, new Color(55, 60, 70));
    public float ScrollSpeed { get; set; } = 40;
    public int ScrollbarWidth { get; set; } = 10;
    public int ScrollbarPadding { get; set; } = 2;
    public Color ScrollbarThumb { get; set; } = new(80, 90, 110);
    public Color ScrollbarThumbHovered { get; set; } = new(110, 120, 145);
    public float ScrollTopOffset { get; set; } = 60;

    // Featured card area
    public Color FeaturedBg { get; set; } = new(30, 33, 42);
    public Border? FeaturedBorder { get; set; } = new Border(2, new Color(55, 60, 70));
    public float FeaturedScale { get; set; } = 3.5f;
    public string FeaturedLabelFont { get; set; } = Fonts.M3x6;
    public int FeaturedLabelFontSize { get; set; } = 18;
    public Color FeaturedLabelColor { get; set; } = new(170, 170, 180);

    // Card list items
    public float CardListScale { get; set; } = 2f;
    public float CardItemHeight { get; set; } = 40;
    public float CardItemSpacing { get; set; } = 4;
    public Color CardItemBg { get; set; } = new(45, 50, 62);
    public Color CardItemHoverBg { get; set; } = new(60, 68, 85);
    public Border? CardItemBorder { get; set; } = new Border(1, new Color(70, 75, 90));
    public string CardItemFont { get; set; } = Fonts.M3x6;
    public int CardItemFontSize { get; set; } = 20;
    public Color CardItemColor { get; set; } = Color.White;

    private ScrollPanel? _scrollPanel;
    private Panel? _featuredPanel;
    private BitmapLabel? _countLabel;
    private BitmapLabel? _headerLabel;
    private readonly List<BaseComponent> _cardItems = [];
    private readonly List<(BaseComponent Component, Vector2 Offset)> _featuredOverlays = [];

    public DeckPile(string name = "deck_pile") : base(name)
    {
        Background = new Color(35, 38, 48);
        Border = new Border(3, new Color(70, 75, 90));
    }

    /// <summary>
    /// Call after setting Size and Position to build the internal layout.
    /// </summary>
    public void BuildLayout()
    {
        Children.Clear();
        _cardItems.Clear();
        _featuredOverlays.Clear();

        var pileW = Size.X;

        // Header label
        _headerLabel = new BitmapLabel("pile_header")
        {
            Text = HeaderText,
            FontFamily = HeaderFont,
            FontSize = HeaderFontSize,
            TextColor = HeaderColor,
            Alignment = TextAlignment.Center,
            MaxWidth = (int)pileW,
            Position = new Anchor(new Vector2(0, HeaderOffsetY), Position),
        };
        Children.Add(_headerLabel);

        // Card count
        _countLabel = new BitmapLabel("pile_count")
        {
            Text = "0 cards",
            FontFamily = CountFont,
            FontSize = CountFontSize,
            TextColor = CountColor,
            Alignment = TextAlignment.Center,
            MaxWidth = (int)pileW,
            Position = new Anchor(new Vector2(0, CountOffsetY), Position),
        };
        Children.Add(_countLabel);

        // Scrollable card list
        var featuredH = 80 * FeaturedScale + 30;
        var scrollH = Size.Y - ScrollTopOffset - featuredH - 10;

        _scrollPanel = new ScrollPanel("pile_scroll")
        {
            Position = new Anchor(new Vector2(8, ScrollTopOffset), Position),
            Size = new Vector2(pileW - 16, scrollH),
            Background = ScrollPanelBg,
            Border = ScrollPanelBorder,
            ScrollSpeed = ScrollSpeed,
            ScrollbarWidth = ScrollbarWidth,
            ScrollbarPadding = ScrollbarPadding,
            ScrollbarThumb = ScrollbarThumb,
            ScrollbarThumbHovered = ScrollbarThumbHovered,
        };
        Children.Add(_scrollPanel);

        // Featured card area at bottom
        var featuredY = ScrollTopOffset + scrollH + 8;
        _featuredPanel = new Panel("pile_featured")
        {
            Position = new Anchor(new Vector2(8, featuredY), Position),
            Size = new Vector2(pileW - 16, featuredH),
            Background = FeaturedBg,
            Border = FeaturedBorder,
        };
        Children.Add(_featuredPanel);

        RefreshCardList();
    }

    /// <summary>
    /// Rebuilds the card list and featured card display from current DeckCards.
    /// </summary>
    public void RefreshCardList()
    {
        if (_scrollPanel is null)
            return;

        // Clear previous items
        foreach (var item in _cardItems)
            _scrollPanel.Children.Remove(item);
        _cardItems.Clear();

        // Build list items
        for (int i = 0; i < DeckCards.Count; i++)
        {
            var card = DeckCards[i];
            var y = i * (CardItemHeight + CardItemSpacing);
            var itemBg = CardItemBg;
            var itemHoverBg = CardItemHoverBg;

            var itemPanel = new Panel($"pile_item_{i}")
            {
                Position = new Anchor(new Vector2(4, y + 4), _scrollPanel.Position),
                Size = new Vector2(_scrollPanel.Size.X - 24, CardItemHeight),
                Background = itemBg,
                Border = CardItemBorder,
            };

            // Card name label
            var nameText = card.CardName;
            if (card is PokemonCardComponent pc && pc.Card is not null)
                nameText = $"Lv.{pc.Card.Level} {pc.CardName}";

            var nameLabel = new BitmapLabel($"pile_name_{i}")
            {
                Text = nameText,
                FontFamily = CardItemFont,
                FontSize = CardItemFontSize,
                TextColor = CardItemColor,
                Position = new Anchor(new Vector2(8, 10), itemPanel.Position),
            };
            itemPanel.Children.Add(nameLabel);

            // Wire click to remove from deck
            var capturedCard = card;
            itemPanel.OnClicked += () => OnCardClicked?.Invoke(capturedCard);

            // Hover effect
            itemPanel.OnHoveredEnter += () => itemPanel.Background = itemHoverBg;
            itemPanel.OnHoveredExit += () => itemPanel.Background = itemBg;

            // Right-click to feature
            itemPanel.OnPressed += () =>
            {
                var pressed = ControlState.GetPressedMouseButtons();
                if (Array.Exists(pressed, b => b == MouseButton.Right))
                {
                    FeaturedCard = capturedCard;
                    OnCardFeatured?.Invoke(capturedCard);
                    RefreshFeatured();
                }
            };

            _cardItems.Add(itemPanel);
            _scrollPanel.Children.Add(itemPanel);
        }

        // Update scroll content size
        var totalH = DeckCards.Count * (CardItemHeight + CardItemSpacing) + 8;
        _scrollPanel.ContentSize = new Vector2(_scrollPanel.ContentSize.X, totalH);

        // Update count label
        if (_countLabel is not null)
            _countLabel.Text = $"{DeckCards.Count} cards";

        RefreshFeatured();
    }

    /// <summary>
    /// Refreshes the featured card display area.
    /// </summary>
    private void RefreshFeatured()
    {
        if (_featuredPanel is null)
            return;

        // Clear previous featured overlays
        foreach (var (component, _) in _featuredOverlays)
            _featuredPanel.Children.Remove(component);
        _featuredOverlays.Clear();

        if (FeaturedCard is null && DeckCards.Count > 0)
            FeaturedCard = DeckCards[0];

        if (FeaturedCard is null)
            return;

        // Show a featured label
        var label = new BitmapLabel("featured_label")
        {
            Text = $"Featured: {FeaturedCard.CardName}",
            FontFamily = FeaturedLabelFont,
            FontSize = FeaturedLabelFontSize,
            TextColor = FeaturedLabelColor,
            Position = new Anchor(new Vector2(8, 4), _featuredPanel.Position),
        };
        _featuredOverlays.Add((label, new Vector2(8, 4)));
        _featuredPanel.Children.Add(label);

        // Show the card sprite if it's a Pokemon card
        if (FeaturedCard is PokemonCardComponent featuredPc && featuredPc.SpriteIdentifier is not null)
        {
            var result = ContentHelper.GetTextureResult<PokemonSpriteAtlas>(featuredPc.Card?.BasePokemon.SpriteIdentifier ?? "");
            if (result is not null)
            {
                var sprite = new Sprite("featured_sprite")
                {
                    Scale = new Vector2(FeaturedScale, FeaturedScale),
                };
                sprite.SetFromAtlas(result);
                sprite.Origin = Vector2.Zero;

                var spriteW = result.AtlasEntry.FrameWidth * FeaturedScale;
                var offset = new Vector2((_featuredPanel.Size.X - spriteW) / 2f, 22);
                sprite.Position = new Anchor(offset, _featuredPanel.Position);
                _featuredOverlays.Add((sprite, offset));
                _featuredPanel.Children.Add(sprite);
            }
        }
    }

    /// <summary>
    /// Updates the header text (e.g. "Pokemon Deck" or "Berry Deck").
    /// </summary>
    public void SetHeader(string text)
    {
        if (_headerLabel is not null)
            _headerLabel.Text = text;
    }
}
