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
/// A scrollable deck pile panel showing cards in a vertical list.
/// Each card item has a face card checkbox. Structural children
/// (pile_header, pile_count, pile_scroll) are defined in XML as nested children.
/// Card item styling uses flat template properties.
/// </summary>
public class DeckPile : Panel
{
    public List<CardComponent> DeckCards { get; set; } = [];

    public event Action<CardComponent>? OnCardClicked;
    public event Action<CardComponent>? OnFaceCardChanged;

    // Layout properties
    public float ScrollTopOffset { get; set; } = 60;
    public Thickness CardListPadding { get; set; } = new(4);

    // Card item template properties (for dynamically created list items)
    public float CardItemHeight { get; set; } = 40;
    public float CardItemSpacing { get; set; } = 4;
    public Color CardItemBg { get; set; } = new(45, 50, 62);
    public Color CardItemHoverBg { get; set; } = new(60, 68, 85);
    public Border? CardItemBorder { get; set; } = new Border(1, new Color(70, 75, 90));
    public string CardItemFont { get; set; } = Fonts.M3x6;
    public int CardItemFontSize { get; set; } = 20;
    public Color CardItemColor { get; set; } = Color.White;

    // Face card checkbox properties
    public string FaceCardCheckedSprite { get; set; } = "Buttons.top_active";
    public string FaceCardUncheckedSprite { get; set; } = "Buttons.top";
    public Vector2 FaceCardCheckboxScale { get; set; } = new(3, 3);
    public Vector2 FaceCardCheckboxOffset { get; set; } = new(-8, 4);

    // The currently selected face card
    public CardComponent? FaceCard { get; set; }

    private ScrollPanel? _scrollPanel;
    private BitmapLabel? _countLabel;
    private BitmapLabel? _headerLabel;
    private readonly List<BaseComponent> _cardItems = [];
    private readonly List<Checkbox> _faceCardCheckboxes = [];

    // Track which children were found from XML (don't override their Position/Size)
    private bool _headerFromXml;
    private bool _countFromXml;
    private bool _scrollFromXml;

    public DeckPile(string name = "deck_pile") : base(name)
    {
        Background = new Color(35, 38, 48);
        Border = new Border(3, new Color(70, 75, 90));
    }

    private T? FindChild<T>(string name) where T : BaseComponent
        => Children.OfType<T>().FirstOrDefault(c => c.Name == name);

    /// <summary>
    /// Call after setting Size and Position to build the internal layout.
    /// Finds named children from XML or creates defaults.
    /// </summary>
    public void BuildLayout()
    {
        _cardItems.Clear();
        _faceCardCheckboxes.Clear();

        var pileW = Size.X;

        // Find or create header label
        _headerFromXml = false;
        _headerLabel = FindChild<BitmapLabel>("pile_header");
        if (_headerLabel is not null)
        {
            _headerFromXml = true;
        }
        else
        {
            _headerLabel = new BitmapLabel("pile_header")
            {
                Text = "Deck",
                FontFamily = Fonts.M6x11,
                FontSize = 28,
                TextColor = Color.White,
                Alignment = TextAlignment.Center,
                Position = new Anchor(new Vector2(0, 8), Position),
            };
            Children.Add(_headerLabel);
        }
        _headerLabel.MaxWidth = (int)pileW;

        // Find or create count label
        _countFromXml = false;
        _countLabel = FindChild<BitmapLabel>("pile_count");
        if (_countLabel is not null)
        {
            _countFromXml = true;
        }
        else
        {
            _countLabel = new BitmapLabel("pile_count")
            {
                Text = "0 cards",
                FontFamily = Fonts.M3x6,
                FontSize = 20,
                TextColor = new Color(170, 170, 180),
                Alignment = TextAlignment.Center,
                Position = new Anchor(new Vector2(0, 38), Position),
            };
            Children.Add(_countLabel);
        }
        _countLabel.MaxWidth = (int)pileW;
        _countLabel.Text = "0 cards";

        // Calculate layout sizes
        var scrollH = Size.Y - ScrollTopOffset - 10;

        // Find or create scroll panel
        _scrollFromXml = false;
        _scrollPanel = FindChild<ScrollPanel>("pile_scroll");
        if (_scrollPanel is not null)
        {
            _scrollFromXml = true;
        }
        else
        {
            _scrollPanel = new ScrollPanel("pile_scroll")
            {
                Background = new Color(30, 33, 42),
                Border = new Border(2, new Color(55, 60, 70)),
                ScrollSpeed = 40,
                ScrollbarWidth = 10,
                ScrollbarPadding = 2,
                ScrollbarThumb = new Color(80, 90, 110),
                ScrollbarThumbHovered = new Color(110, 120, 145),
            };
            Children.Add(_scrollPanel);
        }
        if (!_scrollFromXml)
        {
            _scrollPanel.Position = new Anchor(new Vector2(8, ScrollTopOffset), Position);
            _scrollPanel.Size = new Vector2(pileW - 16, scrollH);
        }

        RefreshCardList();
    }

    /// <summary>
    /// Rebuilds the card list from current DeckCards.
    /// </summary>
    public void RefreshCardList()
    {
        if (_scrollPanel is null)
            return;

        // Clear previous items
        foreach (var item in _cardItems)
            _scrollPanel.Children.Remove(item);
        _cardItems.Clear();
        _faceCardCheckboxes.Clear();

        // Put the face card at the top of the list
        if (FaceCard is not null)
        {
            DeckCards.Remove(FaceCard);
            DeckCards.Insert(0, FaceCard);
        }

        // Build list items
        for (int i = 0; i < DeckCards.Count; i++)
        {
            var card = DeckCards[i];
            var y = CardListPadding.Top + i * (CardItemHeight + CardItemSpacing);
            var itemBg = CardItemBg;
            var itemHoverBg = CardItemHoverBg;
            var itemW = _scrollPanel.Size.X - CardListPadding.Left - CardListPadding.Right - _scrollPanel.ScrollbarWidth;

            var itemPanel = new Panel($"pile_item_{i}")
            {
                Position = new Anchor(new Vector2(CardListPadding.Left, y), _scrollPanel.Position),
                Size = new Vector2(itemW, CardItemHeight),
                Background = itemBg,
                Border = CardItemBorder,
            };

            // Card name label
            var nameText = card.CardName;
            if (card is PokemonCardComponent pc && pc.Card is not null)
                nameText = $"lv {pc.Card.Level} {pc.CardName}";

            var nameLabel = new BitmapLabel($"pile_name_{i}")
            {
                Text = nameText,
                FontFamily = CardItemFont,
                FontSize = CardItemFontSize,
                TextColor = CardItemColor,
                Position = new Anchor(new Vector2(8, 10), itemPanel.Position),
            };
            itemPanel.Children.Add(nameLabel);

            // Face card checkbox (only for pokemon cards)
            if (card is PokemonCardComponent)
            {
                var isFaceCard = FaceCard is not null && FaceCard.Id == card.Id;
                var checkbox = new Checkbox($"pile_face_{i}")
                {
                    IsChecked = isFaceCard,
                    Scale = FaceCardCheckboxScale,
                    Position = new Anchor(
                        new Vector2(itemW + FaceCardCheckboxOffset.X, FaceCardCheckboxOffset.Y),
                        itemPanel.Position),
                    Opacity = 0
                };

                var checkedSprite = SceneLoader.ParseSprite(FaceCardCheckedSprite);
                if (checkedSprite is not null)
                    checkbox.SetCheckedSprite(checkedSprite);

                var uncheckedSprite = SceneLoader.ParseSprite(FaceCardUncheckedSprite);
                if (uncheckedSprite is not null)
                    checkbox.SetUncheckedSprite(uncheckedSprite);

                var capturedCard = card;
                checkbox.OnChecked += () =>
                {
                    FaceCard = capturedCard;
                    // Uncheck all other checkboxes
                    foreach (var cb in _faceCardCheckboxes)
                        if (cb != checkbox && cb.IsChecked)
                            cb.IsChecked = false;

                    OnFaceCardChanged?.Invoke(capturedCard);
                    RefreshCardList();
                };
                checkbox.OnUnchecked += () =>
                {
                    // If this was the face card, clear it
                    if (FaceCard is not null && FaceCard.Id == capturedCard.Id)
                    {
                        FaceCard = null;
                        OnFaceCardChanged?.Invoke(capturedCard);
                    }
                };

                itemPanel.OnHoveredEnter += () => checkbox.Opacity = 1;
                itemPanel.OnHoveredExit += () => checkbox.Opacity = 0;

                _faceCardCheckboxes.Add(checkbox);
                itemPanel.Children.Add(checkbox);
            }

            // Wire click to remove from deck — skip if a face card checkbox is hovered
            var capturedCardForClick = card;
            itemPanel.OnClicked += () =>
            {
                if (_faceCardCheckboxes.Any(cb => cb.Hovered))
                    return;
                OnCardClicked?.Invoke(capturedCardForClick);
            };

            // Hover effect
            itemPanel.OnHoveredEnter += () => itemPanel.Background = itemHoverBg;
            itemPanel.OnHoveredExit += () => itemPanel.Background = itemBg;

            _cardItems.Add(itemPanel);
            _scrollPanel.Children.Add(itemPanel);
        }

        // Update scroll content size
        var totalH = CardListPadding.Top + DeckCards.Count * (CardItemHeight + CardItemSpacing) + CardListPadding.Bottom;
        _scrollPanel.ContentSize = new Vector2(_scrollPanel.ContentSize.X, totalH);

        // Update count label
        if (_countLabel is not null)
            _countLabel.Text = $"{DeckCards.Count} cards";
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
