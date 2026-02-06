using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PocketCardLeague.Enums;
using PocketCardLeague.SpriteMaps;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCardLeague.Components;

public class BerryCard : Card
{
    public List<BerryType> BerryTypes { get; set; } = [BerryType.Green];
    public List<BerryEffectType> BerryEffects { get; set; } = [BerryEffectType.None];

    public BerryCard() : base()
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
