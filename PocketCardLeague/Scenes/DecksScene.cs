using Microsoft.Xna.Framework;
using Components;
using Helpers;
using PocketCardLeague.Enums;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using PocketCardLeague.Components;
using PocketCardLeague.Helpers;

namespace PocketCardLeague.Scenes;

public class DecksScene() : XmlScene<SceneType>(SceneType.Decks)
{
    protected override string XmlFileName => "DecksScene.xml";

    private const int DeckBoxWidth = 228;
    private const int DeckBoxHeight = 312;
    private const int DeckSpacing = 20;
    private const int DeckPadding = 20;
    private const int DecksPerRow = 4;

    private static readonly Color DefaultDeckBackground = new(50, 55, 65);
    private static readonly Color SelectedDeckBackground = new(65, 80, 110);
    private static readonly Color SelectedDeckBorderColor = new(120, 160, 220);

    protected override void OnXmlLoaded()
    {
        var btnLeft = Bind<SpriteButton>("btn_left");
        btnLeft.OnClicked += () => SceneManager.SetActiveScene(SceneType.Main, new SlideTransition(SlideDirection.Right));
        btnLeft.OnHoveredEnter += () => btnLeft.Opacity = 1f;
        btnLeft.OnHoveredExit += () => btnLeft.Opacity = 0.8f;

        var btnDown = Bind<SpriteButton>("btn_down");
        btnDown.OnClicked += () => SceneManager.SetActiveScene(SceneType.Cards, new SlideTransition(SlideDirection.Up));
        btnDown.OnHoveredEnter += () => btnDown.Opacity = 1f;
        btnDown.OnHoveredExit += () => btnDown.Opacity = 0.8f;

        var btnNewDeck = Bind<Button>("new_deck_button");
        btnNewDeck.OnClicked += () =>
        {
            GameStateManager.ActiveSave.EditingDeck = null;
            SceneManager.SetActiveScene(SceneType.DeckBuilder, new SlideTransition(SlideDirection.Down));
        };

        RefreshDecks();
    }

    public override void Start()
    {
        base.Start();
        RefreshDecks();
    }

    private void RefreshDecks()
    {
        var decksList = Bind<ScrollPanel>("decks_container");
        decksList.Children.Clear();

        // Empty "new deck" placeholder — first in the list
        var emptyDeck = CreateEmptyDeckButton(0);
        decksList.Children.Add(emptyDeck);

        // Build deck boxes from save data
        var decks = GameStateManager.ActiveSave.Decks;
        var activeDeck = GameStateManager.ActiveSave.ActiveDeck;

        for (int i = 0; i < decks.Count; i++)
        {
            var deck = decks[i];
            var isActive = activeDeck is not null && deck.Id == activeDeck.Id;

            deck.Background = isActive ? SelectedDeckBackground : DefaultDeckBackground;
            deck.Border = isActive ? new Border(4, SelectedDeckBorderColor) : new Border(4, Color.Gray);

            var capturedDeck = deck;
            deck.OnClicked += () =>
            {
                GameStateManager.ActiveSave.ActiveDeck = capturedDeck;
                GameStateManager.ActiveSave.EditingDeck = capturedDeck;
                GameStateManager.Save(GameStateManager.ActiveSave);
                SceneManager.SetActiveScene(SceneType.DeckBuilder, new SlideTransition(SlideDirection.Down));
            };

            PositionDeckInGrid(deck, i + 1); // +1 because empty deck is index 0
            deck.BuildVisuals();
            decksList.Children.Add(deck);
        }

        // Update content size based on total items
        var totalItems = 1 + decks.Count;
        var rows = (totalItems + DecksPerRow - 1) / DecksPerRow;
        decksList.ContentSize = new Vector2(
            decksList.ContentSize.X,
            DeckPadding * 2 + rows * (DeckBoxHeight + DeckSpacing) - DeckSpacing);
    }

    private void PositionDeckInGrid(BaseComponent component, int index)
    {
        var col = index % DecksPerRow;
        var row = index / DecksPerRow;
        var x = DeckPadding + col * (DeckBoxWidth + DeckSpacing);
        var y = DeckPadding + row * (DeckBoxHeight + DeckSpacing);

        component.Position = new Anchor(new Vector2(x, y), Bind<ScrollPanel>("decks_container").Position);
        component.Size = new Vector2(DeckBoxWidth, DeckBoxHeight);
    }

    private Panel CreateEmptyDeckButton(int index)
    {
        var panel = new Panel("empty_deck")
        {
            Background = new Color(60, 65, 75),
        };
        panel.Border = new Border(4, Color.Gray);

        var plusLabel = new BitmapLabel("empty_deck_label")
        {
            Text = "+",
            FontFamily = Consts.Fonts.M6x11,
            FontSize = 72,
            TextColor = Color.White,
            Alignment = TextAlignment.Center,
            MaxWidth = DeckBoxWidth,
        };

        var nameLabel = new BitmapLabel("empty_deck_name")
        {
            Text = "New Deck",
            FontFamily = Consts.Fonts.M6x11,
            FontSize = 36,
            TextColor = Color.White,
            Alignment = TextAlignment.Center,
            MaxWidth = DeckBoxWidth,
        };

        PositionDeckInGrid(panel, index);

        plusLabel.Position = new Anchor(new Vector2(0, 120), panel.Position);
        nameLabel.Position = new Anchor(new Vector2(0, 320), panel.Position);

        panel.Children.Add(plusLabel);
        panel.Children.Add(nameLabel);

        panel.OnClicked += () =>
        {
            GameStateManager.ActiveSave.EditingDeck = null;
            SceneManager.SetActiveScene(SceneType.DeckBuilder, new SlideTransition(SlideDirection.Right));
        };

        return panel;
    }

    public override void Update(GameTime gameTime)
    {
        var pressedKeys = ControlState.GetPressedKeys();
        if (pressedKeys.Contains(Keys.Left))
        {
            SceneManager.SetActiveScene(SceneType.Main, new SlideTransition(SlideDirection.Right));
            return;
        }

        if (pressedKeys.Contains(Keys.Down))
        {
            SceneManager.SetActiveScene(SceneType.Cards, new SlideTransition(SlideDirection.Up));
            return;
        }

        base.Update(gameTime);
    }
}
