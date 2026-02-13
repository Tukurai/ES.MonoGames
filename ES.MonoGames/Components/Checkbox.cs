using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Components;

/// <summary>
/// A checkbox component that can be toggled on/off.
/// Supports both sprite-based rendering and simple box rendering with checkmark.
/// </summary>
public class Checkbox : BaseComponent
{
    /// <summary>
    /// Whether the checkbox is checked.
    /// </summary>
    public bool IsChecked { get; set; } = false;

    /// <summary>
    /// Whether the checkbox is enabled and can be interacted with.
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    // Sprite properties
    public Texture2D? CheckedTexture
    {
        get;
        set { field = value; UpdateSizeFromSprite(); }
    }

    public Rectangle? CheckedSourceRect
    {
        get;
        set { field = value; UpdateSizeFromSprite(); }
    }

    public Texture2D? UncheckedTexture
    {
        get;
        set { field = value; UpdateSizeFromSprite(); }
    }

    public Rectangle? UncheckedSourceRect
    {
        get;
        set { field = value; UpdateSizeFromSprite(); }
    }

    public Texture2D? CheckedDisabledTexture { get; set; } = null;
    public Rectangle? CheckedDisabledSourceRect { get; set; } = null;

    public Texture2D? UncheckedDisabledTexture { get; set; } = null;
    public Rectangle? UncheckedDisabledSourceRect { get; set; } = null;

    // Simple box mode properties
    /// <summary>
    /// Background color of the checkbox box (simple mode).
    /// </summary>
    public Color BoxBackground { get; set; } = Color.White;

    /// <summary>
    /// Border color of the checkbox box (simple mode).
    /// </summary>
    public Color BoxBorderColor { get; set; } = Color.Black;

    /// <summary>
    /// Color of the checkmark (simple mode).
    /// </summary>
    public Color CheckmarkColor { get; set; } = Color.Black;

    /// <summary>
    /// Size of the checkbox box in pixels (simple mode).
    /// </summary>
    public int BoxSize { get; set; } = 20;

    /// <summary>
    /// Thickness of the box border (simple mode).
    /// </summary>
    public int BoxBorderThickness { get; set; } = 2;

    // Rendering properties
    /// <summary>
    /// Tint color applied to sprites.
    /// </summary>
    public Color Tint { get; set; } = Color.White;

    /// <summary>
    /// Tint color applied when disabled.
    /// </summary>
    public Color DisabledTint { get; set; } = new Color(128, 128, 128);

    /// <summary>
    /// Sprite effects (flip horizontal/vertical).
    /// </summary>
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;

    /// <summary>
    /// Layer depth for draw order.
    /// </summary>
    public float LayerDepth { get; set; } = 0f;

    // Events
    /// <summary>
    /// Fired when the checked state changes.
    /// </summary>
    public event Action? OnCheckedChanged;

    /// <summary>
    /// Fired when the checkbox becomes checked.
    /// </summary>
    public event Action? OnChecked;

    /// <summary>
    /// Fired when the checkbox becomes unchecked.
    /// </summary>
    public event Action? OnUnchecked;

    public Checkbox(string? name = null, Anchor? position = null) : base(name, position)
    {
        // Set default size for box mode
        Size = new Vector2(BoxSize, BoxSize);

        // Hook into click event for toggling
        OnClicked += Toggle;
    }

    /// <summary>
    /// Sets the checked state sprite from a TextureResult.
    /// </summary>
    public void SetCheckedSprite(TextureResult? result)
    {
        if (result is null)
        {
            CheckedTexture = null;
            CheckedSourceRect = null;
            return;
        }

        CheckedTexture = result.Texture;
        CheckedSourceRect = new Rectangle(
            result.AtlasEntry.FrameX,
            result.AtlasEntry.FrameY,
            result.AtlasEntry.FrameWidth,
            result.AtlasEntry.FrameHeight
        );
    }

    /// <summary>
    /// Sets the unchecked state sprite from a TextureResult.
    /// </summary>
    public void SetUncheckedSprite(TextureResult? result)
    {
        if (result is null)
        {
            UncheckedTexture = null;
            UncheckedSourceRect = null;
            return;
        }

        UncheckedTexture = result.Texture;
        UncheckedSourceRect = new Rectangle(
            result.AtlasEntry.FrameX,
            result.AtlasEntry.FrameY,
            result.AtlasEntry.FrameWidth,
            result.AtlasEntry.FrameHeight
        );
    }

    /// <summary>
    /// Sets the checked disabled state sprite from a TextureResult.
    /// </summary>
    public void SetCheckedDisabledSprite(TextureResult? result)
    {
        if (result is null)
        {
            CheckedDisabledTexture = null;
            CheckedDisabledSourceRect = null;
            return;
        }

        CheckedDisabledTexture = result.Texture;
        CheckedDisabledSourceRect = new Rectangle(
            result.AtlasEntry.FrameX,
            result.AtlasEntry.FrameY,
            result.AtlasEntry.FrameWidth,
            result.AtlasEntry.FrameHeight
        );
    }

    /// <summary>
    /// Sets the unchecked disabled state sprite from a TextureResult.
    /// </summary>
    public void SetUncheckedDisabledSprite(TextureResult? result)
    {
        if (result is null)
        {
            UncheckedDisabledTexture = null;
            UncheckedDisabledSourceRect = null;
            return;
        }

        UncheckedDisabledTexture = result.Texture;
        UncheckedDisabledSourceRect = new Rectangle(
            result.AtlasEntry.FrameX,
            result.AtlasEntry.FrameY,
            result.AtlasEntry.FrameWidth,
            result.AtlasEntry.FrameHeight
        );
    }

    /// <summary>
    /// Toggles the checked state.
    /// </summary>
    public void Toggle()
    {
        if (!IsEnabled) return;

        IsChecked = !IsChecked;
        OnCheckedChanged?.Invoke();

        if (IsChecked)
            OnChecked?.Invoke();
        else
            OnUnchecked?.Invoke();
    }

    private void UpdateSizeFromSprite()
    {
        // Prefer unchecked sprite for size, fall back to checked
        if (UncheckedSourceRect.HasValue)
        {
            Size = new Vector2(UncheckedSourceRect.Value.Width, UncheckedSourceRect.Value.Height);
        }
        else if (UncheckedTexture is not null)
        {
            Size = new Vector2(UncheckedTexture.Width, UncheckedTexture.Height);
        }
        else if (CheckedSourceRect.HasValue)
        {
            Size = new Vector2(CheckedSourceRect.Value.Width, CheckedSourceRect.Value.Height);
        }
        else if (CheckedTexture is not null)
        {
            Size = new Vector2(CheckedTexture.Width, CheckedTexture.Height);
        }
    }

    private bool HasSprites()
    {
        return CheckedTexture is not null || UncheckedTexture is not null;
    }

    public override void Update(GameTime gameTime)
    {
        if (!IsEnabled)
        {
            Hovered = false;
            Pressed = false;
            return;
        }

        // Update size for box mode (in case BoxSize changed)
        if (!HasSprites())
            Size = new Vector2(BoxSize, BoxSize);

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var pos = Position.GetVector2();

        if (HasSprites())
            DrawSpriteMode(spriteBatch, pos);
        else
            DrawBoxMode(spriteBatch, pos);

        base.Draw(spriteBatch);
    }

    private void DrawSpriteMode(SpriteBatch spriteBatch, Vector2 pos)
    {
        var (texture, sourceRect, tint) = GetCurrentSprite();

        if (texture is not null)
        {
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
    }

    private (Texture2D? texture, Rectangle? sourceRect, Color tint) GetCurrentSprite()
    {
        if (!IsEnabled)
        {
            if (IsChecked)
            {
                return (
                    CheckedDisabledTexture ?? CheckedTexture,
                    CheckedDisabledSourceRect ?? CheckedSourceRect,
                    CheckedDisabledTexture is not null ? Tint : DisabledTint
                );
            }
            else
            {
                return (
                    UncheckedDisabledTexture ?? UncheckedTexture,
                    UncheckedDisabledSourceRect ?? UncheckedSourceRect,
                    UncheckedDisabledTexture is not null ? Tint : DisabledTint
                );
            }
        }

        if (IsChecked)
        {
            return (CheckedTexture, CheckedSourceRect, Tint);
        }
        else
        {
            return (UncheckedTexture, UncheckedSourceRect, Tint);
        }
    }

    private void DrawBoxMode(SpriteBatch spriteBatch, Vector2 pos)
    {
        var boxRect = new Rectangle((int)pos.X, (int)pos.Y, BoxSize, BoxSize);

        // Draw box background
        spriteBatch.Draw(
            RendererHelper.WhitePixel,
            boxRect,
            IsEnabled ? BoxBackground : DisabledTint
        );

        // Draw border
        var border = new Border(BoxBorderThickness, BoxBorderColor);
        RendererHelper.Draw(spriteBatch, border, pos, new Vector2(BoxSize), Scale);

        // Draw checkmark if checked
        if (IsChecked)
        {
            DrawCheckmark(spriteBatch, pos);
        }
    }

    private void DrawCheckmark(SpriteBatch spriteBatch, Vector2 pos)
    {
        var color = IsEnabled ? CheckmarkColor : DisabledTint;
        var padding = Math.Max(2, BoxSize / 5);
        var thickness = Math.Max(2, BoxSize / 8);

        // Draw a simple checkmark using two thick lines
        // Line 1: from bottom-left area going up-right to center-bottom
        // Line 2: from center-bottom going up-right to top-right area

        var startX = (int)pos.X + padding;
        var startY = (int)pos.Y + BoxSize / 2;

        var midX = (int)pos.X + BoxSize / 3 + padding;
        var midY = (int)pos.Y + BoxSize - padding - thickness;

        var endX = (int)pos.X + BoxSize - padding;
        var endY = (int)pos.Y + padding + thickness;

        // Short leg of checkmark (going down-right)
        DrawThickLine(spriteBatch, startX, startY, midX, midY, thickness, color);

        // Long leg of checkmark (going up-right)
        DrawThickLine(spriteBatch, midX, midY, endX, endY, thickness, color);
    }

    private static void DrawThickLine(SpriteBatch spriteBatch, int x1, int y1, int x2, int y2, int thickness, Color color)
    {
        // Calculate line properties
        var dx = x2 - x1;
        var dy = y2 - y1;
        var length = (float)Math.Sqrt(dx * dx + dy * dy);
        var angle = (float)Math.Atan2(dy, dx);

        // Draw as rotated rectangle
        spriteBatch.Draw(
            RendererHelper.WhitePixel,
            new Vector2(x1, y1),
            null,
            color,
            angle,
            new Vector2(0, 0.5f),
            new Vector2(length, thickness),
            SpriteEffects.None,
            0
        );
    }
}
