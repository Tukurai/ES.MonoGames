using System;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Components;

/// <summary>
/// A checkbox component that can be toggled on/off.
/// Supports both sprite-based rendering and simple box rendering with checkmark.
/// </summary>
public class Checkbox : BaseComponent
{
    // Checked state sprites
    private Texture2D? _checkedTexture;
    private Rectangle? _checkedSourceRect;

    // Unchecked state sprites
    private Texture2D? _uncheckedTexture;
    private Rectangle? _uncheckedSourceRect;


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
        get => _checkedTexture;
        set
        {
            _checkedTexture = value;
            UpdateSizeFromSprite();
        }
    }

    public Rectangle? CheckedSourceRect
    {
        get => _checkedSourceRect;
        set
        {
            _checkedSourceRect = value;
            UpdateSizeFromSprite();
        }
    }

    public Texture2D? UncheckedTexture
    {
        get => _uncheckedTexture;
        set
        {
            _uncheckedTexture = value;
            UpdateSizeFromSprite();
        }
    }

    public Rectangle? UncheckedSourceRect
    {
        get => _uncheckedSourceRect;
        set
        {
            _uncheckedSourceRect = value;
            UpdateSizeFromSprite();
        }
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

    // Label properties
    /// <summary>
    /// Optional label text displayed to the right of the checkbox.
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// Font for the label.
    /// </summary>
    public SpriteFont? Font { get; set; }

    /// <summary>
    /// Color of the label text.
    /// </summary>
    public Color LabelColor { get; set; } = Color.White;

    /// <summary>
    /// Space between the checkbox and the label in pixels.
    /// </summary>
    public int LabelSpacing { get; set; } = 8;

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
        if (result == null)
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
        if (result == null)
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
        if (result == null)
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
        if (result == null)
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
        if (_uncheckedSourceRect.HasValue)
        {
            Size = new Vector2(_uncheckedSourceRect.Value.Width, _uncheckedSourceRect.Value.Height);
        }
        else if (_uncheckedTexture != null)
        {
            Size = new Vector2(_uncheckedTexture.Width, _uncheckedTexture.Height);
        }
        else if (_checkedSourceRect.HasValue)
        {
            Size = new Vector2(_checkedSourceRect.Value.Width, _checkedSourceRect.Value.Height);
        }
        else if (_checkedTexture != null)
        {
            Size = new Vector2(_checkedTexture.Width, _checkedTexture.Height);
        }
    }

    private bool HasSprites()
    {
        return _checkedTexture != null || _uncheckedTexture != null;
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
        {
            Size = new Vector2(BoxSize, BoxSize);
        }

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var pos = Position.GetVector2();

        if (HasSprites())
        {
            DrawSpriteMode(spriteBatch, pos);
        }
        else
        {
            DrawBoxMode(spriteBatch, pos);
        }

        // Draw label if set
        if (Label != null && Font != null)
        {
            var checkboxWidth = HasSprites() ? Size.X : BoxSize;
            var checkboxHeight = HasSprites() ? Size.Y : BoxSize;

            var labelX = pos.X + checkboxWidth + LabelSpacing;
            var labelY = pos.Y + checkboxHeight / 2 - Font.LineSpacing / 2;

            spriteBatch.DrawString(
                Font,
                Label,
                new Vector2(labelX, labelY),
                IsEnabled ? LabelColor : DisabledTint
            );
        }

        base.Draw(spriteBatch);
    }

    private void DrawSpriteMode(SpriteBatch spriteBatch, Vector2 pos)
    {
        var (texture, sourceRect, tint) = GetCurrentSprite();

        if (texture != null)
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
                    CheckedDisabledTexture ?? _checkedTexture,
                    CheckedDisabledSourceRect ?? _checkedSourceRect,
                    CheckedDisabledTexture != null ? Tint : DisabledTint
                );
            }
            else
            {
                return (
                    UncheckedDisabledTexture ?? _uncheckedTexture,
                    UncheckedDisabledSourceRect ?? _uncheckedSourceRect,
                    UncheckedDisabledTexture != null ? Tint : DisabledTint
                );
            }
        }

        if (IsChecked)
        {
            return (_checkedTexture, _checkedSourceRect, Tint);
        }
        else
        {
            return (_uncheckedTexture, _uncheckedSourceRect, Tint);
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
        var padding = 4;

        // Draw a simple checkmark using two thick lines
        // Line 1: from bottom-left area going up-right to center-bottom
        // Line 2: from center-bottom going up-right to top-right area

        var startX = (int)pos.X + padding;
        var startY = (int)pos.Y + BoxSize / 2;

        var midX = (int)pos.X + BoxSize / 3 + padding;
        var midY = (int)pos.Y + BoxSize - padding - 2;

        var endX = (int)pos.X + BoxSize - padding;
        var endY = (int)pos.Y + padding + 2;

        // Draw checkmark as filled rectangles (simplified approach)
        // For a better checkmark, you'd use line drawing or a texture

        // Short leg of checkmark (going down-right)
        DrawThickLine(spriteBatch, startX, startY, midX, midY, 3, color);

        // Long leg of checkmark (going up-right)
        DrawThickLine(spriteBatch, midX, midY, endX, endY, 3, color);
    }

    private void DrawThickLine(SpriteBatch spriteBatch, int x1, int y1, int x2, int y2, int thickness, Color color)
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
            new Vector2(0, thickness / 2f),
            new Vector2(length, thickness),
            SpriteEffects.None,
            0
        );
    }
}
