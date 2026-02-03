using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Components;

/// <summary>
/// A button component that uses sprites for different visual states.
/// Supports Normal, Hovered, Pressed, and Disabled states with fallback logic.
/// </summary>
public class SpriteButton(string? name = null, Anchor? position = null, Vector2? size = null) : BaseComponent(name, position, size)
{
    // Normal state sprite
    private Texture2D? _normalTexture;
    private Rectangle? _normalSourceRect;

    // Hovered state sprite
    private Texture2D? _hoveredTexture;
    private Rectangle? _hoveredSourceRect;

    // Pressed state sprite
    private Texture2D? _pressedTexture;
    private Rectangle? _pressedSourceRect;

    // Disabled state sprite
    private Texture2D? _disabledTexture;
    private Rectangle? _disabledSourceRect;

    /// <summary>
    /// The texture to display in normal state.
    /// </summary>
    public Texture2D? NormalTexture
    {
        get => _normalTexture;
        set
        {
            _normalTexture = value;
            UpdateSizeFromSprite();
        }
    }

    /// <summary>
    /// Source rectangle for normal state sprite (for atlas usage).
    /// </summary>
    public Rectangle? NormalSourceRect
    {
        get => _normalSourceRect;
        set
        {
            _normalSourceRect = value;
            UpdateSizeFromSprite();
        }
    }

    /// <summary>
    /// The texture to display when hovered.
    /// </summary>
    public Texture2D? HoveredTexture
    {
        get => _hoveredTexture;
        set => _hoveredTexture = value;
    }

    /// <summary>
    /// Source rectangle for hovered state sprite.
    /// </summary>
    public Rectangle? HoveredSourceRect
    {
        get => _hoveredSourceRect;
        set => _hoveredSourceRect = value;
    }

    /// <summary>
    /// The texture to display when pressed.
    /// </summary>
    public Texture2D? PressedTexture
    {
        get => _pressedTexture;
        set => _pressedTexture = value;
    }

    /// <summary>
    /// Source rectangle for pressed state sprite.
    /// </summary>
    public Rectangle? PressedSourceRect
    {
        get => _pressedSourceRect;
        set => _pressedSourceRect = value;
    }

    /// <summary>
    /// The texture to display when disabled.
    /// </summary>
    public Texture2D? DisabledTexture
    {
        get => _disabledTexture;
        set => _disabledTexture = value;
    }

    /// <summary>
    /// Source rectangle for disabled state sprite.
    /// </summary>
    public Rectangle? DisabledSourceRect
    {
        get => _disabledSourceRect;
        set => _disabledSourceRect = value;
    }

    /// <summary>
    /// Whether the button is enabled and can be interacted with.
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Tint color applied to the sprite. Default is White (no tint).
    /// </summary>
    public Color Tint { get; set; } = Color.White;

    /// <summary>
    /// Tint color applied when the button is disabled.
    /// </summary>
    public Color DisabledTint { get; set; } = new Color(128, 128, 128);

    /// <summary>
    /// Optional text to display on the button.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Font for the text overlay.
    /// </summary>
    public SpriteFont? Font { get; set; }

    /// <summary>
    /// Color of the text overlay.
    /// </summary>
    public Color TextColor { get; set; } = Color.White;

    /// <summary>
    /// Border around the text (for outline effect).
    /// </summary>
    public Border? TextBorder { get; set; }

    /// <summary>
    /// Sprite effects (flip horizontal/vertical).
    /// </summary>
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;

    /// <summary>
    /// Layer depth for draw order.
    /// </summary>
    public float LayerDepth { get; set; } = 0f;

    /// <summary>
    /// Sets the normal state sprite from a TextureResult (from a SpriteAtlas).
    /// </summary>
    public void SetNormalSprite(TextureResult? result)
    {
        if (result == null)
        {
            NormalTexture = null;
            NormalSourceRect = null;
            return;
        }

        NormalTexture = result.Texture;
        NormalSourceRect = new Rectangle(
            result.AtlasEntry.FrameX,
            result.AtlasEntry.FrameY,
            result.AtlasEntry.FrameWidth,
            result.AtlasEntry.FrameHeight
        );
    }

    /// <summary>
    /// Sets the hovered state sprite from a TextureResult.
    /// </summary>
    public void SetHoveredSprite(TextureResult? result)
    {
        if (result == null)
        {
            HoveredTexture = null;
            HoveredSourceRect = null;
            return;
        }

        HoveredTexture = result.Texture;
        HoveredSourceRect = new Rectangle(
            result.AtlasEntry.FrameX,
            result.AtlasEntry.FrameY,
            result.AtlasEntry.FrameWidth,
            result.AtlasEntry.FrameHeight
        );
    }

    /// <summary>
    /// Sets the pressed state sprite from a TextureResult.
    /// </summary>
    public void SetPressedSprite(TextureResult? result)
    {
        if (result == null)
        {
            PressedTexture = null;
            PressedSourceRect = null;
            return;
        }

        PressedTexture = result.Texture;
        PressedSourceRect = new Rectangle(
            result.AtlasEntry.FrameX,
            result.AtlasEntry.FrameY,
            result.AtlasEntry.FrameWidth,
            result.AtlasEntry.FrameHeight
        );
    }

    /// <summary>
    /// Sets the disabled state sprite from a TextureResult.
    /// </summary>
    public void SetDisabledSprite(TextureResult? result)
    {
        if (result == null)
        {
            DisabledTexture = null;
            DisabledSourceRect = null;
            return;
        }

        DisabledTexture = result.Texture;
        DisabledSourceRect = new Rectangle(
            result.AtlasEntry.FrameX,
            result.AtlasEntry.FrameY,
            result.AtlasEntry.FrameWidth,
            result.AtlasEntry.FrameHeight
        ); 
    }

    private void UpdateSizeFromSprite()
    {
        if (_normalSourceRect.HasValue)
        {
            Size = new Vector2(_normalSourceRect.Value.Width, _normalSourceRect.Value.Height);
        }
        else if (_normalTexture != null)
        {
            Size = new Vector2(_normalTexture.Width, _normalTexture.Height);
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (!IsEnabled)
        {
            // Clear hover/press states when disabled
            Hovered = false;
            Pressed = false;
            return; // Skip base.Update() to prevent interaction
        }

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var (texture, sourceRect, tint) = GetCurrentSprite();

        if (texture != null)
        {
            var pos = Position.GetVector2();

            spriteBatch.Draw(
                texture,
                pos,
                sourceRect,
                tint,
                Rotation,
                Vector2.Zero,
                Scale,
                Effects,
                LayerDepth
            );
        }

        // Draw text overlay if configured
        if (Text != null && Font != null)
        {
            DrawTextOverlay(spriteBatch);
        }

        base.Draw(spriteBatch);
    }

    private (Texture2D? texture, Rectangle? sourceRect, Color tint) GetCurrentSprite()
    {
        if (!IsEnabled)
        {
            // Disabled state: use disabled sprite or fall back to normal with disabled tint
            return (
                _disabledTexture ?? _normalTexture,
                _disabledSourceRect ?? _normalSourceRect,
                DisabledTint
            );
        }

        if (Pressed)
        {
            // Pressed state: pressed -> hovered -> normal
            if (_pressedTexture != null)
                return (_pressedTexture, _pressedSourceRect, Tint);
            if (_hoveredTexture != null)
                return (_hoveredTexture, _hoveredSourceRect, Tint);
            return (_normalTexture, _normalSourceRect, Tint);
        }

        if (Hovered)
        {
            // Hovered state: hovered -> normal
            if (_hoveredTexture != null)
                return (_hoveredTexture, _hoveredSourceRect, Tint);
            return (_normalTexture, _normalSourceRect, Tint);
        }

        // Normal state
        return (_normalTexture, _normalSourceRect, Tint);
    }

    private void DrawTextOverlay(SpriteBatch spriteBatch)
    {
        if (Text == null || Font == null) return;

        var pos = Position.GetVector2();
        var textSize = Font.MeasureString(Text);

        // Center text on the button
        var textPos = new Vector2(
            pos.X + (Size.X * Scale.X - textSize.X) / 2,
            pos.Y + (Size.Y * Scale.Y - textSize.Y) / 2
        );

        var textColor = IsEnabled ? TextColor : DisabledTint;

        if (TextBorder != null && TextBorder.Thickness > 0)
        {
            RendererHelper.DrawOutlinedString(spriteBatch, Font, Text, textPos, textColor, TextBorder);
        }
        else
        {
            spriteBatch.DrawString(Font, Text, textPos, textColor);
        }
    }
}
