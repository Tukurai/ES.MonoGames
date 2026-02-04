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
    /// <summary>
    /// The texture to display in normal state.
    /// </summary>
    public Texture2D? NormalTexture { get; set { field = value; UpdateSizeFromSprite(); } }

    /// <summary>
    /// Source rectangle for normal state sprite (for atlas usage).
    /// </summary>
    public Rectangle? NormalSourceRect { get; set { field = value; UpdateSizeFromSprite(); } }

    /// <summary>
    /// The texture to display when hovered.
    /// </summary>
    public Texture2D? HoveredTexture { get; set; }

    /// <summary>
    /// Source rectangle for hovered state sprite.
    /// </summary>
    public Rectangle? HoveredSourceRect { get; set; }

    /// <summary>
    /// The texture to display when pressed.
    /// </summary>
    public Texture2D? PressedTexture { get; set; }
    /// <summary>
    /// Source rectangle for pressed state sprite.
    /// </summary>
    public Rectangle? PressedSourceRect { get; set; }

    /// <summary>
    /// The texture to display when disabled.
    /// </summary>
    public Texture2D? DisabledTexture { get; set; }

    /// <summary>
    /// Source rectangle for disabled state sprite.
    /// </summary>
    public Rectangle? DisabledSourceRect { get; set; }

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
        if (result is null)
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
        if (result is null)
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
        if (result is null)
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
        if (result is null)
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
        if (NormalSourceRect.HasValue)
            Size = new Vector2(NormalSourceRect.Value.Width, NormalSourceRect.Value.Height);
        else if (NormalTexture is not null)
            Size = new Vector2(NormalTexture.Width, NormalTexture.Height);
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

        if (Hovered)
            ControlState.RequestCursor(CursorType.Pointer);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var (texture, sourceRect, tint) = GetCurrentSprite();

        if (texture is not null)
        {
            var pos = Position.GetVector2();

            spriteBatch.Draw(
                texture,
                pos,
                sourceRect,
                ApplyOpacity(tint),
                Rotation,
                Vector2.Zero,
                Scale,
                Effects,
                LayerDepth
            );
        }

        // Draw text overlay if configured
        if (Text is not null && Font is not null)
            DrawTextOverlay(spriteBatch);

        base.Draw(spriteBatch);
    }

    private (Texture2D? texture, Rectangle? sourceRect, Color tint) GetCurrentSprite()
    {
        if (!IsEnabled)
        {
            // Disabled state: use disabled sprite or fall back to normal with disabled tint
            return (
                DisabledTexture ?? NormalTexture,
                DisabledSourceRect ?? NormalSourceRect,
                DisabledTint
            );
        }

        if (Pressed)
        {
            // Pressed state: pressed -> hovered -> normal
            if (PressedTexture is not null)
                return (PressedTexture, PressedSourceRect, Tint);
            if (HoveredTexture is not null)
                return (HoveredTexture, HoveredSourceRect, Tint);
            return (NormalTexture, NormalSourceRect, Tint);
        }

        if (Hovered)
        {
            // Hovered state: hovered -> normal
            if (HoveredTexture is not null)
                return (HoveredTexture, HoveredSourceRect, Tint);
            return (NormalTexture, NormalSourceRect, Tint);
        }

        // Normal state
        return (NormalTexture, NormalSourceRect, Tint);
    }

    private void DrawTextOverlay(SpriteBatch spriteBatch)
    {
        if (Text is null || Font is null) return;

        var pos = Position.GetVector2();
        var textSize = Font.MeasureString(Text);

        // Center text on the button
        var textPos = new Vector2(
            pos.X + (Size.X * Scale.X - textSize.X) / 2,
            pos.Y + (Size.Y * Scale.Y - textSize.Y) / 2
        );

        var textColor = ApplyOpacity(IsEnabled ? TextColor : DisabledTint);

        if (TextBorder is not null && TextBorder.Thickness > 0)
        {
            var border = EffectiveOpacity < 1f
                ? new Border(TextBorder.Thickness, ApplyOpacity(TextBorder.Color))
                : TextBorder;
            RendererHelper.DrawOutlinedString(spriteBatch, Font, Text, textPos, textColor, border);
        }
        else
            spriteBatch.DrawString(Font, Text, textPos, textColor);
    }
}
