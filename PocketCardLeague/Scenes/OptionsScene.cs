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

        var settingsContinue = new Label("text_label", "Fiddle diddle!", ContentHelper.LoadFont("DefaultFont"), new Anchor(new Vector2(0, 320), null), true, 800)
        {
            Border = new Border(2, Color.Black),
        };

        settingsContinue.OnHoveredEnter += () => settingsContinue.Color = Color.Yellow;
        settingsContinue.OnHoveredExit += () => settingsContinue.Color = Color.White;

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
        AddComponent(settingsContinue);
        AddComponent(button);

        base.Initialize();
    }
}
