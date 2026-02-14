using Microsoft.Xna.Framework;
using Components;
using Helpers;
using PocketCardLeague.Enums;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace PocketCardLeague.Scenes;

public class CardsScene() : XmlScene<SceneType>(SceneType.Cards)
{
    protected override string XmlFileName => "CardsScene.xml";

    protected override void OnXmlLoaded()
    {
        // Bind events to components defined in XML
        var btnUp = Bind<SpriteButton>("btn_up");
        btnUp.OnClicked += () => SceneManager.SetActiveScene(SceneType.Decks, new SlideTransition(SlideDirection.Down));
        btnUp.OnHoveredEnter += () => btnUp.Opacity = 1f;
        btnUp.OnHoveredExit += () => btnUp.Opacity = 0.8f;

        var btnLeft = Bind<SpriteButton>("btn_left");
        btnLeft.OnClicked += () => SceneManager.SetActiveScene(SceneType.BoosterDebug, new SlideTransition(SlideDirection.Right));
        btnLeft.OnHoveredEnter += () => btnLeft.Opacity = 1f;
        btnLeft.OnHoveredExit += () => btnLeft.Opacity = 0.8f;
    }

    public override void Update(GameTime gameTime)
    {
        var pressedKeys = ControlState.GetPressedKeys();
        if (pressedKeys.Contains(Keys.Up))
        {
            SceneManager.SetActiveScene(SceneType.Decks, new SlideTransition(SlideDirection.Down));
            return;
        }

        if (pressedKeys.Contains(Keys.Left))
        {
            SceneManager.SetActiveScene(SceneType.BoosterDebug, new SlideTransition(SlideDirection.Right));
            return;
        }

        base.Update(gameTime);
    }
}
