using System;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Components;

/// <summary>
/// A slider component for selecting integer values within a range.
/// Snaps to whole number values.
/// </summary>
public class Slider : BaseComponent
{
    private bool _isDragging = false;

    /// <summary>
    /// Minimum value of the slider.
    /// </summary>
    public int MinValue { get; set; } = 0;

    /// <summary>
    /// Maximum value of the slider.
    /// </summary>
    public int MaxValue { get; set; } = 100;

    /// <summary>
    /// Current value of the slider. Clamped to MinValue-MaxValue range.
    /// </summary>
    public int Value
    {
        get;
        set
        {
            var clamped = Math.Clamp(value, MinValue, MaxValue);
            if (field != clamped)
            {
                field = clamped;
                OnValueChanged?.Invoke(field);
            }
        }
    }

    /// <summary>
    /// Whether the slider is enabled and can be interacted with.
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    // Track appearance
    /// <summary>
    /// Height of the track in pixels.
    /// </summary>
    public int TrackHeight { get; set; } = 6;

    /// <summary>
    /// Background color of the track.
    /// </summary>
    public Color TrackColor { get; set; } = new Color(60, 60, 60);

    /// <summary>
    /// Color of the filled portion of the track (from min to current value).
    /// </summary>
    public Color TrackFillColor { get; set; } = Color.CornflowerBlue;

    /// <summary>
    /// Border around the track.
    /// </summary>
    public Border? TrackBorder { get; set; } = new Border(1, Color.Gray);

    // Thumb appearance
    /// <summary>
    /// Width of the thumb in pixels.
    /// </summary>
    public int ThumbWidth { get; set; } = 16;

    /// <summary>
    /// Height of the thumb in pixels.
    /// </summary>
    public int ThumbHeight { get; set; } = 20;

    /// <summary>
    /// Color of the thumb.
    /// </summary>
    public Color ThumbColor { get; set; } = Color.White;

    /// <summary>
    /// Color of the thumb when hovered.
    /// </summary>
    public Color ThumbHoveredColor { get; set; } = new Color(220, 220, 220);

    /// <summary>
    /// Color of the thumb when being dragged.
    /// </summary>
    public Color ThumbDraggedColor { get; set; } = new Color(200, 200, 200);

    /// <summary>
    /// Border around the thumb.
    /// </summary>
    public Border? ThumbBorder { get; set; } = new Border(1, Color.Black);

    /// <summary>
    /// Tint applied when disabled.
    /// </summary>
    public Color DisabledTint { get; set; } = new Color(128, 128, 128);

    // Label
    /// <summary>
    /// Optional label text displayed to the left of the slider.
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// Font for the label and value display.
    /// </summary>
    public SpriteFont? Font { get; set; }

    /// <summary>
    /// Color of the label text.
    /// </summary>
    public Color LabelColor { get; set; } = Color.White;

    /// <summary>
    /// Whether to show the current value next to the slider.
    /// </summary>
    public bool ShowValue { get; set; } = true;

    /// <summary>
    /// Format string for displaying the value. Use {0} for the value.
    /// </summary>
    public string ValueFormat { get; set; } = "{0}";

    /// <summary>
    /// Space between label and slider track.
    /// </summary>
    public int LabelSpacing { get; set; } = 8;

    // Events
    /// <summary>
    /// Fired when the value changes.
    /// </summary>
    public event Action<int>? OnValueChanged;

    // Internal state
    private bool _thumbHovered = false;

    public Slider(string? name = null, Anchor? position = null, Vector2? size = null) : base(name, position, size)
    {
        // Default size
        if (size == null)
        {
            Size = new Vector2(200, 20);
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (!IsEnabled)
        {
            Hovered = false;
            Pressed = false;
            _isDragging = false;
            _thumbHovered = false;
            return;
        }

        var pos = Position.GetVector2();
        var mousePos = ControlState.GetMousePosition();
        var trackRect = GetTrackRect();
        var thumbRect = GetThumbRect();

        // Check if mouse is over thumb
        _thumbHovered = thumbRect.Contains(mousePos);

        // Check if mouse is over track area (for click-to-set)
        var sliderArea = new Rectangle(
            trackRect.X - ThumbWidth / 2,
            (int)pos.Y,
            trackRect.Width + ThumbWidth,
            (int)Size.Y
        );
        var mouseInSlider = sliderArea.Contains(mousePos);

        // Start dragging on mouse press
        if (ControlState.GetPressedMouseButtons().Length > 0)
        {
            if (_thumbHovered || mouseInSlider)
            {
                _isDragging = true;
                // Immediately set value on click
                SetValueFromMouseX(mousePos.X);
            }
        }

        // Continue dragging
        if (_isDragging)
        {
            if (ControlState.GetHeldMouseButtons().Length > 0)
            {
                SetValueFromMouseX(mousePos.X);
            }
            else
            {
                _isDragging = false;
            }
        }

        // Update hover state for the whole component
        Hovered = mouseInSlider || _thumbHovered;

        // Request cursor type
        if (_isDragging)
            ControlState.RequestCursor(CursorType.Grab);
        else if (Hovered)
            ControlState.RequestCursor(CursorType.Pointer);

        // Don't call base.Update() to avoid default click handling
    }

    private void SetValueFromMouseX(int mouseX)
    {
        var trackRect = GetTrackRect();
        var usableWidth = trackRect.Width;

        // Calculate position relative to track
        var relativeX = mouseX - trackRect.X;
        var ratio = Math.Clamp((float)relativeX / usableWidth, 0f, 1f);

        // Calculate and snap to nearest integer value
        var range = MaxValue - MinValue;
        var newValue = MinValue + (int)Math.Round(ratio * range);

        Value = newValue;
    }

    private Rectangle GetTrackRect()
    {
        var pos = Position.GetVector2();
        var trackY = (int)(pos.Y + (Size.Y - TrackHeight) / 2);

        return new Rectangle(
            (int)pos.X + ThumbWidth / 2,
            trackY,
            (int)Size.X - ThumbWidth,
            TrackHeight
        );
    }

    private Rectangle GetThumbRect()
    {
        var pos = Position.GetVector2();
        var trackRect = GetTrackRect();

        // Calculate thumb position based on value
        var range = MaxValue - MinValue;
        var ratio = range > 0 ? (float)(Value - MinValue) / range : 0f;
        var thumbX = trackRect.X + (int)(ratio * trackRect.Width) - ThumbWidth / 2;
        var thumbY = (int)(pos.Y + (Size.Y - ThumbHeight) / 2);

        return new Rectangle(thumbX, thumbY, ThumbWidth, ThumbHeight);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var pos = Position.GetVector2();
        var trackRect = GetTrackRect();
        var thumbRect = GetThumbRect();

        // Draw track background
        var trackColor = IsEnabled ? TrackColor : DisabledTint;
        spriteBatch.Draw(RendererHelper.WhitePixel, trackRect, trackColor);

        // Draw filled portion of track
        if (Value > MinValue)
        {
            var range = MaxValue - MinValue;
            var ratio = range > 0 ? (float)(Value - MinValue) / range : 0f;
            var fillWidth = (int)(trackRect.Width * ratio);

            var fillRect = new Rectangle(trackRect.X, trackRect.Y, fillWidth, trackRect.Height);
            var fillColor = IsEnabled ? TrackFillColor : DisabledTint;
            spriteBatch.Draw(RendererHelper.WhitePixel, fillRect, fillColor);
        }

        // Draw track border
        if (TrackBorder != null)
        {
            RendererHelper.Draw(spriteBatch, TrackBorder, new Vector2(trackRect.X, trackRect.Y),
                new Vector2(trackRect.Width, trackRect.Height), Vector2.One);
        }

        // Draw thumb
        Color currentThumbColor;
        if (!IsEnabled)
            currentThumbColor = DisabledTint;
        else if (_isDragging)
            currentThumbColor = ThumbDraggedColor;
        else if (_thumbHovered)
            currentThumbColor = ThumbHoveredColor;
        else
            currentThumbColor = ThumbColor;

        spriteBatch.Draw(RendererHelper.WhitePixel, thumbRect, currentThumbColor);

        // Draw thumb border
        if (ThumbBorder != null)
        {
            RendererHelper.Draw(spriteBatch, ThumbBorder, new Vector2(thumbRect.X, thumbRect.Y),
                new Vector2(thumbRect.Width, thumbRect.Height), Vector2.One);
        }

        // Draw value text
        if (ShowValue && Font != null)
        {
            var valueText = string.Format(ValueFormat, Value);
            var textSize = Font.MeasureString(valueText);
            var textX = pos.X + Size.X + LabelSpacing;
            var textY = pos.Y + (Size.Y - textSize.Y) / 2;

            spriteBatch.DrawString(Font, valueText, new Vector2(textX, textY),
                IsEnabled ? LabelColor : DisabledTint);
        }

        base.Draw(spriteBatch);
    }
}
