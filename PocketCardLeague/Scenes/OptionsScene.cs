using Components;
using Helpers;
using Microsoft.Xna.Framework;
using PocketCardLeague.Enums;

namespace PocketCardLeague.Scenes;

public class OptionsScene() : Scene<SceneType>(SceneType.Options)
{
    public override void Initialize()
    {
        BackgroundColor = Color.Black;

        var title = new Label("title", "Settings", ContentHelper.LoadFont("TitleFont"), new Anchor(new Vector2(0, 80), null), true, 800)
        {
            Border = new Border(2, Color.Black),
        };

        // Player name input field example
        var nameLabel = new Label("name_label", "Player Name:", ContentHelper.LoadFont("DefaultFont"), new Anchor(new Vector2(200, 180), null))
        {
            Border = new Border(1, Color.Black),
        };

        var nameInput = new InputField(
            name: "name_input",
            placeholderText: "Enter your name...",
            font: ContentHelper.LoadFont("DefaultFont"),
            position: new Anchor(new Vector2(350, 175)),
            size: new Vector2(250, 32))
        {
            Background = new Color(40, 40, 40),
            Border = new Border(1, Color.Gray),
            FocusedBorder = new Border(2, Color.CornflowerBlue),
            TextColor = Color.White,
            PlaceholderColor = Color.DarkGray
        };

        nameInput.OnTextChanged += (text) => System.Diagnostics.Debug.WriteLine($"Name changed: {text}");
        nameInput.OnSubmit += () => System.Diagnostics.Debug.WriteLine($"Name submitted: {nameInput.Text}");

        // ScrollPanel example
        var scrollPanel = new ScrollPanel("scroll_example")
        {
            Position = new Anchor(new Vector2(50, 220)),
            Size = new Vector2(250, 200),
            ContentSize = new Vector2(250, 500), // Content is taller than view
            Background = new Color(30, 30, 30),
            Border = new Border(1, Color.Gray),
            ScrollbarThumb = Color.CornflowerBlue
        };

        // Add items to the scroll panel
        var font = ContentHelper.LoadFont("DefaultFont");
        for (int i = 0; i < 12; i++)
        {
            var itemLabel = new Label(
                $"scroll_item_{i}",
                $"Scroll Item {i + 1}",
                font,
                new Anchor(new Vector2(60, 230 + i * 40)))
            {
                Color = Color.White
            };
            itemLabel.OnHoveredEnter += () => itemLabel.Color = Color.Yellow;
            itemLabel.OnHoveredExit += () => itemLabel.Color = Color.White;
            scrollPanel.Children.Add(itemLabel);
        }

        var scrollLabel = new Label("scroll_label", "Scroll Panel Demo:", font, new Anchor(new Vector2(50, 195)))
        {
            Color = Color.Gray
        };

        var button = new Button("back_button", "Back", ContentHelper.LoadFont("DefaultFont"), new Anchor(new Vector2(340, 400), null), new Vector2(120, 40), true)
        {
            Background = Color.Green,
            Border = new Border(2, Color.Black),
            TextBorder = new Border(2, Color.Black)
        };

        button.OnHoveredEnter += () => button.Background = Color.LightGreen;
        button.OnHoveredExit += () => button.Background = Color.Green;
        button.OnClicked += () => SceneManager.SetActiveScene(SceneType.Title);

        AddComponent(title);
        AddComponent(nameLabel);
        AddComponent(nameInput);
        AddComponent(scrollLabel);
        AddComponent(scrollPanel);
        AddComponent(button);

        base.Initialize();
    }
}
