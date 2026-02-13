using Microsoft.Xna.Framework;
using Components;
using Helpers;
using PocketCardLeague.Enums;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace PocketCardLeague.Scenes;

public class CardDebugScene() : XmlScene<SceneType>(SceneType.CardDebug)
{
    protected override string XmlFileName => "CardDebugScene.xml";

    protected override void OnXmlLoaded()
    {
    }

    public override void Update(GameTime gameTime)
    {
        var pressedKeys = ControlState.GetPressedKeys();
        var heldKeys = ControlState.GetHeldKeys();
        bool altHeld = heldKeys.Contains(Keys.LeftAlt) || heldKeys.Contains(Keys.RightAlt);

        if (!altHeld && pressedKeys.Contains(Keys.End))
        {
            SceneManager.SetActiveScene(SceneType.Title, new FadeTransition());
            return;
        }

        base.Update(gameTime);
    }
}
