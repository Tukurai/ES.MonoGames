using Microsoft.Xna.Framework;
using Components;
using Helpers;
using PocketCardLeague.Enums;

namespace PocketCardLeague.Scenes;

public class TitleScene() : Scene<SceneType>(SceneType.Title)
{
    public override void Initialize()
    {
        BackgroundColor = Color.Black;

        var title = new Label("title", "Pocket Card League", ContentHelper.LoadFont("TitleFont"), new Anchor(new Vector2(0, 80), null), true, 800)
        {
            Border = new Border(2, Color.Black)
        };

        var continueAction = new Label("continue", "Press SPACE to continue!", ContentHelper.LoadFont("DefaultFont"), new Anchor(new Vector2(0, 320), null), true, 800)
        {
            Border = new Border(2, Color.Black)
        };

        continueAction.OnHoveredEnter += () => continueAction.Color = Color.Yellow;
        continueAction.OnHoveredExit += () => continueAction.Color = Color.White;

        var button = new Button("button", "Options", ContentHelper.LoadFont("DefaultFont"), new Anchor(new Vector2(340, 400), null), new Vector2(120, 40), true)
        {
            Background = Color.Green,
            Border = new Border(2, Color.Black),
            TextBorder = new Border(2, Color.Black)
        };

        button.OnHoveredEnter += () => button.Background = Color.LightGreen;
        button.OnHoveredExit += () => button.Background = Color.Green;
        button.OnClicked += () => SceneManager.SetActiveScene(SceneType.Options);

        AddComponent(title);
        AddComponent(continueAction);
        AddComponent(button);

        base.Initialize();
    }
}
