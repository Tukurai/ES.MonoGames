using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ES.MonoGames.Components.Graphics;

public class Sprite
{
    public TextureRegion TextureRegion { get; set; }
    public Color Color { get; set; } = Color.White;
    public float Rotation { get; set; } = 0.0f;
    public Vector2 Scale { get; set; } = Vector2.One;
    public Vector2 Origin { get; set; } = Vector2.Zero;
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public float LayerDepth { get; set; } = 0.0f;
    public float Width => TextureRegion.Width * Scale.X;
    public float Height => TextureRegion.Height * Scale.Y;
    
    public Sprite() { }

    public Sprite(TextureRegion textureRegion)
    {
        TextureRegion = textureRegion;
    }

    public void CenterOrigin()
    {
        Origin = new Vector2(TextureRegion.Width, TextureRegion.Height) * 0.5f;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        TextureRegion.Draw(spriteBatch, position, Color, Rotation, Origin, Scale, SpriteEffects, LayerDepth);
    }
}