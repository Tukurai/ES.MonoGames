using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Components;

/// <summary>
/// A component that renders a sprite from a texture or sprite atlas.
/// </summary>
public class Sprite(string? name = null, Anchor? position = null) : BaseComponent(name, position)
{
    /// <summary>
    /// The texture to render. If using a sprite atlas, this is the sprite sheet.
    /// </summary>
    public Texture2D? Texture { get; set { field = value; UpdateSize(); } }

    /// <summary>
    /// The source rectangle within the texture. If null, the entire texture is used.
    /// </summary>
    public Rectangle? SourceRectangle { get; set { field = value; UpdateSize(); } }

    /// <summary>
    /// The origin point for rotation and scaling. Default is top-left (0,0).
    /// </summary>
    public Vector2 Origin { get; set; } = Vector2.Zero;

    /// <summary>
    /// Tint color applied to the sprite. Default is White (no tint).
    /// </summary>
    public Color Tint { get; set; } = Color.White;

    /// <summary>
    /// Sprite effects (flip horizontal/vertical). Default is None.
    /// </summary>
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;

    /// <summary>
    /// Layer depth for draw order. Default is 0.
    /// </summary>
    public float LayerDepth { get; set; } = 0f;

    /// <summary>
    /// Sets the sprite from a TextureResult (from a SpriteAtlas).
    /// </summary>
    public void SetFromAtlas(TextureResult? result)
    {
        if (result is null)
        {
            Texture = null;
            SourceRectangle = null;
            return;
        }

        Texture = result.Texture;
        SourceRectangle = new Rectangle(
            result.AtlasEntry.FrameX,
            result.AtlasEntry.FrameY,
            result.AtlasEntry.FrameWidth,
            result.AtlasEntry.FrameHeight
        );

        // Set origin based on pivot
        Origin = new Vector2(
            result.AtlasEntry.FrameWidth * result.AtlasEntry.PivotX,
            result.AtlasEntry.FrameHeight * result.AtlasEntry.PivotY
        );
    }

    /// <summary>
    /// Centers the origin of the sprite.
    /// </summary>
    public void CenterOrigin()
    {
        if (SourceRectangle.HasValue)
            Origin = new Vector2(SourceRectangle.Value.Width / 2f, SourceRectangle.Value.Height / 2f);
        else if (Texture is not null)
            Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
    }

    private void UpdateSize()
    {
        if (SourceRectangle.HasValue)
            Size = new Vector2(SourceRectangle.Value.Width, SourceRectangle.Value.Height);
        else if (Texture is not null)
            Size = new Vector2(Texture.Width, Texture.Height);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (Texture is not null)
        {
            var pos = Position.GetVector2();

            spriteBatch.Draw(
                Texture,
                pos,
                SourceRectangle,
                Tint,
                Rotation,
                Origin,
                Scale,
                Effects,
                LayerDepth
            );
        }

        base.Draw(spriteBatch);
    }
}
