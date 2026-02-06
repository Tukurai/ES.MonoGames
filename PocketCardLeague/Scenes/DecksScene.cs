using Microsoft.Xna.Framework;
using Components;
using Helpers;
using PocketCardLeague.Enums;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace PocketCardLeague.Scenes;

public class DecksScene() : XmlScene<SceneType>(SceneType.Decks)
{
    protected override string XmlFileName => "DecksScene.xml";

    protected override void OnXmlLoaded()
    {
        // Bind events to components defined in XML
        var btnLeft = Bind<SpriteButton>("btn_left");
        btnLeft.OnClicked += () => SceneManager.SetActiveScene(SceneType.Main, new SlideTransition(SlideDirection.Right));
        btnLeft.OnHoveredEnter += () => btnLeft.Opacity = 1f;
        btnLeft.OnHoveredExit += () => btnLeft.Opacity = 0.8f;

        var btnDown = Bind<SpriteButton>("btn_down");
        btnDown.OnClicked += () => SceneManager.SetActiveScene(SceneType.Cards, new SlideTransition(SlideDirection.Up));
        btnDown.OnHoveredEnter += () => btnDown.Opacity = 1f;
        btnDown.OnHoveredExit += () => btnDown.Opacity = 0.8f;
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
