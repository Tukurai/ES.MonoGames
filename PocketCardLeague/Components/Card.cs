using Components;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCardLeague.Components;

public class Card : Panel
{
    public string CardName { get; set; } = string.Empty;
    public TextureResult? BackSprite { get; set; }
    public TextureResult? FrontSprite { get; set; }
    public TextureResult? Image { get; set; }
    public bool FaceUp { get; set; } = true;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}
