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
        AddComponent(settingsContinue);
        AddComponent(button);

        base.Initialize();
    }
}
