using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PocketCardLeague.Enums;
using PocketCardLeague.SpriteMaps;
using System.Collections.Generic;

namespace PocketCardLeague.Components;

public class BerryCardComponent : CardComponent
{
    public List<BerryEnergyType> BerryTypes { get; set; } = [BerryEnergyType.Green];
    public List<BerryEffectType> BerryEffects { get; set; } = [BerryEffectType.None];

    public BerryCardComponent() : base()
    {
        BackSprite = CardPartsSpriteAtlas.Card_berry;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    } 
}
