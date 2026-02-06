using Microsoft.Xna.Framework;
using Components;
using Helpers;
using PocketCardLeague.Enums;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace PocketCardLeague.Scenes;

public class DeckBuilderScene() : XmlScene<SceneType>(SceneType.DeckBuilder)
{
    protected override string XmlFileName => "DeckBuilderScene.xml";

    protected override void OnXmlLoaded()
    {
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}
