using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Components;

/// <summary>
/// A component that renders a sprite from a texture or sprite atlas.
/// </summary>
public class Sprite(string? name = null, Anchor? position = null) : BaseComponent(name, position)
{
    private Texture2D? _texture;
    private Rectangle? _sourceRectangle;
    private Vector2 _origin = Vector2.Zero;

    /// <summary>
    /// The texture to render. If using a sprite atlas, this is the sprite sheet.
    /// </summary>
    public Texture2D? Texture
    {
        get => _texture;
        set
        {
            _texture = value;
            UpdateSize();
        }
    }

    /// <summary>
    /// The source rectangle within the texture. If null, the entire texture is used.
    /// </summary>
    public Rectangle? SourceRectangle
    {
        get => _sourceRectangle;
        set
        {
            _sourceRectangle = value;
            UpdateSize();
        }
    }

    /// <summary>
    /// The origin point for rotation and scaling. Default is top-left (0,0).
    /// </summary>
    public Vector2 Origin
    {
        get => _origin;
        set => _origin = value;
    }

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
        if (result == null)
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
        _origin = new Vector2(
            result.AtlasEntry.FrameWidth * result.AtlasEntry.PivotX,
            result.AtlasEntry.FrameHeight * result.AtlasEntry.PivotY
        );
    }

    /// <summary>
    /// Centers the origin of the sprite.
    /// </summary>
    public void CenterOrigin()
    {
        if (_sourceRectangle.HasValue)
        {
            _origin = new Vector2(_sourceRectangle.Value.Width / 2f, _sourceRectangle.Value.Height / 2f);
        }
        else if (_texture != null)
        {
            _origin = new Vector2(_texture.Width / 2f, _texture.Height / 2f);
        }
    }

    private void UpdateSize()
    {
        if (_sourceRectangle.HasValue)
        {
            Size = new Vector2(_sourceRectangle.Value.Width, _sourceRectangle.Value.Height);
        }
        else if (_texture != null)
        {
            Size = new Vector2(_texture.Width, _texture.Height);
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (Texture != null)
        {
            var pos = Position.GetVector2();

            spriteBatch.Draw(
                Texture,
                pos,
                SourceRectangle,
                Tint,
                Rotation,
                _origin,
                Scale,
                Effects,
                LayerDepth
            );
        }

        base.Draw(spriteBatch);
    }
}
