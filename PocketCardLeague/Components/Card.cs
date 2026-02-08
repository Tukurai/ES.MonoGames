using Components;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PocketCardLeague.Components;

public class Card : Panel
{
    public string CardName { get; set; } = string.Empty;
    [JsonIgnore] public TextureResult? BackSprite { get; set; }
    [JsonIgnore] public TextureResult? FrontSprite { get; set; }
    [JsonIgnore] public TextureResult? Image { get; set; }
    [JsonIgnore] public bool FaceUp { get; set; } = true;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}
