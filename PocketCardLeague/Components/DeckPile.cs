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

    private ScrollPanel? _scrollPanel;
    private Panel? _featuredPanel;
    private BitmapLabel? _countLabel;
    private BitmapLabel? _headerLabel;
    private readonly List<BaseComponent> _cardItems = [];
    private readonly List<(BaseComponent Component, Vector2 Offset)> _featuredOverlays = [];

    private const float CardListScale = 2f;
    private const float CardItemHeight = 40;
    private const float CardItemSpacing = 4;
    private const float FeaturedScale = 3.5f;

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
            Text = "Deck",
            FontFamily = Fonts.M6x11,
            FontSize = 28,
            TextColor = Color.White,
            Alignment = TextAlignment.Center,
            MaxWidth = (int)pileW,
            Position = new Anchor(new Vector2(0, 8), Position),
        };
        Children.Add(_headerLabel);

        // Card count
        _countLabel = new BitmapLabel("pile_count")
        {
            Text = "0 cards",
            FontFamily = Fonts.M3x6,
            FontSize = 20,
            TextColor = new Color(170, 170, 180),
            Alignment = TextAlignment.Center,
            MaxWidth = (int)pileW,
            Position = new Anchor(new Vector2(0, 38), Position),
        };
        Children.Add(_countLabel);

        // Scrollable card list
        var scrollTop = 60f;
        var featuredH = 80 * FeaturedScale + 30;
        var scrollH = Size.Y - scrollTop - featuredH - 10;

        _scrollPanel = new ScrollPanel("pile_scroll")
        {
            Position = new Anchor(new Vector2(8, scrollTop), Position),
            Size = new Vector2(pileW - 16, scrollH),
            Background = new Color(30, 33, 42),
            Border = new Border(2, new Color(55, 60, 70)),
            ScrollSpeed = 40,
            ScrollbarWidth = 10,
            ScrollbarPadding = 2,
            ScrollbarThumb = new Color(80, 90, 110),
            ScrollbarThumbHovered = new Color(110, 120, 145),
        };
        Children.Add(_scrollPanel);

        // Featured card area at bottom
        var featuredY = scrollTop + scrollH + 8;
        _featuredPanel = new Panel("pile_featured")
        {
            Position = new Anchor(new Vector2(8, featuredY), Position),
            Size = new Vector2(pileW - 16, featuredH),
            Background = new Color(30, 33, 42),
            Border = new Border(2, new Color(55, 60, 70)),
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

            var itemPanel = new Panel($"pile_item_{i}")
            {
                Position = new Anchor(new Vector2(4, y + 4), _scrollPanel.Position),
                Size = new Vector2(_scrollPanel.Size.X - 24, CardItemHeight),
                Background = new Color(45, 50, 62),
                Border = new Border(1, new Color(70, 75, 90)),
            };

            // Card name label
            var nameText = card.CardName;
            if (card is PokemonCardComponent pc && pc.Card is not null)
                nameText = $"Lv.{pc.Card.Level} {pc.CardName}";

            var nameLabel = new BitmapLabel($"pile_name_{i}")
            {
                Text = nameText,
                FontFamily = Fonts.M3x6,
                FontSize = 20,
                TextColor = Color.White,
                Position = new Anchor(new Vector2(8, 10), itemPanel.Position),
            };
            itemPanel.Children.Add(nameLabel);

            // Wire click to remove from deck
            var capturedCard = card;
            itemPanel.OnClicked += () => OnCardClicked?.Invoke(capturedCard);

            // Hover effect
            itemPanel.OnHoveredEnter += () => itemPanel.Background = new Color(60, 68, 85);
            itemPanel.OnHoveredExit += () => itemPanel.Background = new Color(45, 50, 62);

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
            FontFamily = Fonts.M3x6,
            FontSize = 18,
            TextColor = new Color(170, 170, 180),
            Position = new Anchor(new Vector2(8, 4), _featuredPanel.Position),
        };
        _featuredOverlays.Add((label, new Vector2(8, 4)));
        _featuredPanel.Children.Add(label);

        // Show the card sprite if it's a Pokemon card
        if (FeaturedCard is PokemonCardComponent pc && pc.SpriteIdentifier is not null)
        {
            var result = ContentHelper.GetTextureResult<PokemonSpriteAtlas>(pc.Card?.BasePokemon.SpriteIdentifier ?? "");
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
