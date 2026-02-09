using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Components;

/// <summary>
/// A dropdown list component for selecting from a list of items.
/// </summary>
public class Dropdown : BaseComponent
{
    private int _hoveredIndex = -1;

    /// <summary>
    /// The list of items to display in the dropdown.
    /// </summary>
    public List<string> Items { get; set; } = [];

    /// <summary>
    /// The currently selected index. -1 if nothing is selected.
    /// </summary>
    public int SelectedIndex
    {
        get;
        set
        {
            if (value >= -1 && value < Items.Count && SelectedIndex != value)
            {
                field = value;
                OnSelectionChanged?.Invoke(SelectedIndex);
            }
        }
    }

    /// <summary>
    /// The currently selected item, or null if nothing is selected.
    /// </summary>
    public string? SelectedItem => SelectedIndex >= 0 && SelectedIndex < Items.Count ? Items[SelectedIndex] : null;

    /// <summary>
    /// Whether the dropdown is enabled and can be interacted with.
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Whether the dropdown list is currently expanded.
    /// </summary>
    public bool IsExpanded { get; private set; } = false;

    /// <summary>
    /// Closes the dropdown if it's expanded.
    /// </summary>
    public void Close() => IsExpanded = false;

    /// <summary>
    /// Font for rendering text.
    /// </summary>
    public SpriteFont? Font { get; set; }

    /// <summary>
    /// Placeholder text when nothing is selected.
    /// </summary>
    public string Placeholder { get; set; } = "Select...";

    // Appearance - Main button
    /// <summary>
    /// Background color of the dropdown button.
    /// </summary>
    public Color Background { get; set; } = new Color(50, 50, 50);

    /// <summary>
    /// Background color when hovered.
    /// </summary>
    public Color BackgroundHovered { get; set; } = new Color(70, 70, 70);

    /// <summary>
    /// Text color.
    /// </summary>
    public Color TextColor { get; set; } = Color.White;

    /// <summary>
    /// Placeholder text color.
    /// </summary>
    public Color PlaceholderColor { get; set; } = Color.Gray;

    /// <summary>
    /// Border around the dropdown button.
    /// </summary>
    public Border? Border { get; set; } = new Border(1, Color.Gray);

    /// <summary>
    /// Border when focused/expanded.
    /// </summary>
    public Border? FocusedBorder { get; set; } = new Border(2, Color.CornflowerBlue);

    /// <summary>
    /// Padding inside the dropdown button.
    /// </summary>
    public int Padding { get; set; } = 8;

    // Appearance - Dropdown list
    /// <summary>
    /// Background color of the dropdown list.
    /// </summary>
    public Color ListBackground { get; set; } = new Color(40, 40, 40);

    /// <summary>
    /// Background color of a hovered item.
    /// </summary>
    public Color ItemHoveredBackground { get; set; } = new Color(70, 100, 150);

    /// <summary>
    /// Background color of the selected item in the list.
    /// </summary>
    public Color ItemSelectedBackground { get; set; } = new Color(50, 80, 120);

    /// <summary>
    /// Border around the dropdown list.
    /// </summary>
    public Border? ListBorder { get; set; } = new Border(1, Color.Gray);

    /// <summary>
    /// Maximum number of visible items before scrolling. 0 = show all.
    /// </summary>
    public int MaxVisibleItems { get; set; } = 5;

    /// <summary>
    /// Tint applied when disabled.
    /// </summary>
    public Color DisabledTint { get; set; } = new Color(128, 128, 128);

    // Arrow indicator
    /// <summary>
    /// Width of the arrow indicator area.
    /// </summary>
    public int ArrowWidth { get; set; } = 24;

    /// <summary>
    /// Size of the arrow triangle.
    /// </summary>
    public int ArrowSize { get; set; } = 6;

    /// <summary>
    /// Color of the arrow indicator.
    /// </summary>
    public Color ArrowColor { get; set; } = Color.White;

    /// <summary>
    /// Pixel size for the arrow triangle. Higher values make a chunkier, pixel-art style triangle.
    /// </summary>
    public int TrianglePixelSize { get; set; } = 1;

    /// <summary>
    /// Width of the scrollbar for long lists.
    /// </summary>
    public int ScrollbarWidth { get; set; } = 4;

    // Scroll state for long lists
    private int _scrollOffset = 0;

    // Events
    /// <summary>
    /// Fired when the selection changes. Parameter is the new selected index.
    /// </summary>
    public event Action<int>? OnSelectionChanged;

    public Dropdown(string? name = null, Anchor? position = null, Vector2? size = null) : base(name, position, size)
    {
        // Default size
        if (size == null)
        {
            Size = new Vector2(200, 32);
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (!IsEnabled)
        {
            Hovered = false;
            Pressed = false;
            IsExpanded = false;
            _hoveredIndex = -1;
            return;
        }

        var pos = Position.GetVector2();
        var mousePos = ControlState.GetRawMousePosition(); // Use raw position, not offset

        // Main button area
        var buttonRect = new Rectangle((int)pos.X, (int)pos.Y, (int)Size.X, (int)Size.Y);
        var mouseInButton = buttonRect.Contains(mousePos);

        // Dropdown list area (if expanded)
        var listRect = GetListRect();
        var mouseInList = IsExpanded && listRect.Contains(mousePos);

        // Register the list area as an input blocker when expanded
        // This prevents other components from receiving input in this area
        if (IsExpanded)
            OverlayManager.RegisterInputBlocker(listRect);

        // Update hovered item index
        _hoveredIndex = -1;
        if (mouseInList && Font != null)
        {
            var itemHeight = GetItemHeight();
            var relativeY = mousePos.Y - listRect.Y;
            var hoveredIdx = (int)(relativeY / itemHeight) + _scrollOffset;

            if (hoveredIdx >= 0 && hoveredIdx < Items.Count)
            {
                _hoveredIndex = hoveredIdx;
            }
        }

        // Handle mouse input
        if (ControlState.GetPressedMouseButtons().Length > 0)
        {
            if (mouseInButton)
            {
                // Toggle expanded state
                IsExpanded = !IsExpanded;
                if (IsExpanded)
                {
                    // Reset scroll when opening
                    _scrollOffset = 0;
                    // Scroll to show selected item if possible
                    if (SelectedIndex >= 0)
                    {
                        EnsureItemVisible(SelectedIndex);
                    }
                }
            }
            else if (mouseInList && _hoveredIndex >= 0)
            {
                // Select the clicked item
                SelectedIndex = _hoveredIndex;
                IsExpanded = false;
            }
            else if (IsExpanded)
            {
                // Click outside - close dropdown
                IsExpanded = false;
            }
        }

        // Handle scroll wheel when list is expanded and mouse is over it
        if (IsExpanded && mouseInList)
        {
            var scrollDelta = ControlState.GetScrollWheelDelta();
            if (scrollDelta != 0)
            {
                var maxScroll = Math.Max(0, Items.Count - GetVisibleItemCount());
                _scrollOffset = Math.Clamp(_scrollOffset - scrollDelta / 120, 0, maxScroll);
            }
        }

        Hovered = mouseInButton || mouseInList;

        // Request cursor type
        if (Hovered)
            ControlState.RequestCursor(CursorType.Pointer);

        // Don't call base.Update() to avoid default click handling
    }

    private Rectangle GetListRect()
    {
        var pos = Position.GetVector2();
        var itemHeight = GetItemHeight();
        var visibleCount = GetVisibleItemCount();
        var listHeight = visibleCount * itemHeight;

        return new Rectangle(
            (int)pos.X,
            (int)(pos.Y + Size.Y),
            (int)Size.X,
            listHeight
        );
    }

    private int GetItemHeight()
    {
        if (Font == null) return 24;
        return Font.LineSpacing + Padding;
    }

    private int GetVisibleItemCount()
    {
        if (MaxVisibleItems <= 0 || Items.Count <= MaxVisibleItems)
            return Items.Count;
        return MaxVisibleItems;
    }

    private void EnsureItemVisible(int index)
    {
        var visibleCount = GetVisibleItemCount();
        if (index < _scrollOffset)
        {
            _scrollOffset = index;
        }
        else if (index >= _scrollOffset + visibleCount)
        {
            _scrollOffset = index - visibleCount + 1;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var pos = Position.GetVector2();

        // Draw main button
        DrawButton(spriteBatch, pos);

        // Register dropdown list to be drawn as an overlay (on top of everything)
        if (IsExpanded)
        {
            // Capture pos for the lambda
            var capturedPos = pos;
            OverlayManager.RegisterOverlayDraw(sb => DrawList(sb, capturedPos));
        }

        base.Draw(spriteBatch);
    }

    private void DrawButton(SpriteBatch spriteBatch, Vector2 pos)
    {
        var buttonRect = new Rectangle((int)pos.X, (int)pos.Y, (int)Size.X, (int)Size.Y);

        // Background
        Color bgColor;
        if (!IsEnabled)
            bgColor = DisabledTint;
        else if (Hovered || IsExpanded)
            bgColor = BackgroundHovered;
        else
            bgColor = Background;

        spriteBatch.Draw(RendererHelper.WhitePixel, buttonRect, bgColor);

        // Border
        var border = IsExpanded ? FocusedBorder : Border;
        if (border != null)
        {
            RendererHelper.Draw(spriteBatch, border, pos, Size, Vector2.One);
        }

        // Text
        if (Font != null)
        {
            var text = SelectedItem ?? Placeholder;
            var textColor = IsEnabled
                ? (SelectedItem != null ? TextColor : PlaceholderColor)
                : DisabledTint;

            var textPos = new Vector2(
                pos.X + Padding,
                pos.Y + (Size.Y - Font.LineSpacing) / 2
            );

            // Clip text if too long
            var maxTextWidth = Size.X - Padding * 2 - ArrowWidth;
            var textToDraw = text;
            while (Font.MeasureString(textToDraw).X > maxTextWidth && textToDraw.Length > 1)
            {
                textToDraw = textToDraw[..^1];
            }
            if (textToDraw != text && textToDraw.Length > 2)
            {
                textToDraw = textToDraw[..^2] + "..";
            }

            spriteBatch.DrawString(Font, textToDraw, textPos, textColor);
        }

        // Arrow indicator
        DrawArrow(spriteBatch, pos);
    }

    private void DrawArrow(SpriteBatch spriteBatch, Vector2 pos)
    {
        var arrowColor = IsEnabled ? ArrowColor : DisabledTint;
        var arrowX = pos.X + Size.X - ArrowWidth / 2;
        var arrowY = pos.Y + Size.Y / 2;
        var offset = ArrowSize / 3;

        if (IsExpanded)
            DrawTriangle(spriteBatch, arrowX, arrowY + offset, ArrowSize, true, arrowColor);
        else
            DrawTriangle(spriteBatch, arrowX, arrowY - offset, ArrowSize, false, arrowColor);
    }

    private void DrawTriangle(SpriteBatch spriteBatch, float centerX, float centerY, int size, bool pointingUp, Color color)
    {
        var px = Math.Max(1, TrianglePixelSize);
        var halfSize = size / 2;
        var steps = halfSize / px;

        if (pointingUp)
        {
            for (int i = 0; i <= steps; i++)
            {
                var width = (steps - i) * 2 * px + px;
                var rect = new Rectangle(
                    (int)(centerX - width / 2),
                    (int)(centerY - halfSize + i * px),
                    width,
                    px
                );
                spriteBatch.Draw(RendererHelper.WhitePixel, rect, color);
            }
        }
        else
        {
            for (int i = 0; i <= steps; i++)
            {
                var width = (steps - i) * 2 * px + px;
                var rect = new Rectangle(
                    (int)(centerX - width / 2),
                    (int)(centerY + halfSize - i * px),
                    width,
                    px
                );
                spriteBatch.Draw(RendererHelper.WhitePixel, rect, color);
            }
        }
    }

    private void DrawList(SpriteBatch spriteBatch, Vector2 pos)
    {
        var listRect = GetListRect();
        var itemHeight = GetItemHeight();
        var visibleCount = GetVisibleItemCount();

        // List background
        spriteBatch.Draw(RendererHelper.WhitePixel, listRect, ListBackground);

        // Draw items
        for (int i = 0; i < visibleCount && i + _scrollOffset < Items.Count; i++)
        {
            var itemIndex = i + _scrollOffset;
            var itemRect = new Rectangle(
                listRect.X,
                listRect.Y + i * itemHeight,
                listRect.Width,
                itemHeight
            );

            // Item background
            Color itemBg;
            if (itemIndex == _hoveredIndex)
                itemBg = ItemHoveredBackground;
            else if (itemIndex == SelectedIndex)
                itemBg = ItemSelectedBackground;
            else
                itemBg = ListBackground;

            if (itemBg != ListBackground)
            {
                spriteBatch.Draw(RendererHelper.WhitePixel, itemRect, itemBg);
            }

            // Item text
            if (Font != null)
            {
                var textPos = new Vector2(
                    itemRect.X + Padding,
                    itemRect.Y + (itemHeight - Font.LineSpacing) / 2
                );
                spriteBatch.DrawString(Font, Items[itemIndex], textPos, TextColor);
            }
        }

        // Draw scroll indicator if there are more items
        if (Items.Count > visibleCount)
        {
            DrawScrollIndicator(spriteBatch, listRect, visibleCount);
        }

        // List border
        if (ListBorder != null)
        {
            RendererHelper.Draw(spriteBatch, ListBorder, new Vector2(listRect.X, listRect.Y),
                new Vector2(listRect.Width, listRect.Height), Vector2.One);
        }
    }

    private void DrawScrollIndicator(SpriteBatch spriteBatch, Rectangle listRect, int visibleCount)
    {
        var margin = Math.Max(2, ScrollbarWidth / 2);
        var maxScroll = Items.Count - visibleCount;
        var scrollRatio = maxScroll > 0 ? (float)_scrollOffset / maxScroll : 0f;

        var trackHeight = listRect.Height - margin * 2;
        var thumbHeight = Math.Max(ScrollbarWidth * 5, trackHeight * visibleCount / Items.Count);
        var thumbY = listRect.Y + margin + (int)((trackHeight - thumbHeight) * scrollRatio);

        var thumbRect = new Rectangle(
            listRect.Right - ScrollbarWidth - margin,
            thumbY,
            ScrollbarWidth,
            thumbHeight
        );

        spriteBatch.Draw(RendererHelper.WhitePixel, thumbRect, new Color(100, 100, 100, 180));
    }
}
