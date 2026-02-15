using Microsoft.Xna.Framework;
using Components;
using Helpers;
using PocketCardLeague.Enums;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace PocketCardLeague.Scenes;

public class MainScene() : XmlScene<SceneType>(SceneType.Main)
{
    protected override string XmlFileName => "MainScene.xml";

    protected override void OnXmlLoaded()
    {
        // Bind events to components defined in XML
        var btnLeft = Bind<SpriteButton>("btn_left");
        btnLeft.OnClicked += () => SceneManager.SetActiveScene(SceneType.Options, new SlideTransition(SlideDirection.Right));
        btnLeft.OnHoveredEnter += () => btnLeft.Opacity = 1f;
        btnLeft.OnHoveredExit += () => btnLeft.Opacity = 0.8f;

        var btnRight = Bind<SpriteButton>("btn_right");
        btnRight.OnClicked += () => SceneManager.SetActiveScene(SceneType.Decks, new SlideTransition(SlideDirection.Left));
        btnRight.OnHoveredEnter += () => btnRight.Opacity = 1f;
        btnRight.OnHoveredExit += () => btnRight.Opacity = 0.8f;

        var btnDown = Bind<SpriteButton>("btn_down");
        btnDown.OnClicked += () => SceneManager.SetActiveScene(SceneType.BoosterDebug, new SlideTransition(SlideDirection.Up));
        btnDown.OnHoveredEnter += () => btnDown.Opacity = 1f;
        btnDown.OnHoveredExit += () => btnDown.Opacity = 0.8f;
    }

    public override void Update(GameTime gameTime)
    {
        var pressedKeys = ControlState.GetPressedKeys();
        if (pressedKeys.Contains(Keys.Left))
        {
            SceneManager.SetActiveScene(SceneType.Options, new SlideTransition(SlideDirection.Right));
            return;
        }

        if (pressedKeys.Contains(Keys.Right))
        {
            SceneManager.SetActiveScene(SceneType.Decks, new SlideTransition(SlideDirection.Left));
            return;
        }

        if (pressedKeys.Contains(Keys.Down))
        {
            SceneManager.SetActiveScene(SceneType.BoosterDebug, new SlideTransition(SlideDirection.Up));
            return;
        }

        base.Update(gameTime);
    }
}
