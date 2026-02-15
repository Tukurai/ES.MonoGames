using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace Components;

/// <summary>
/// A scrollable panel that can contain components larger than its visible area.
/// Supports mouse wheel scrolling, drag-to-scroll, and displays scrollbars.
/// </summary>
public class ScrollPanel(string? name = null) : Panel(name)
{
    // Content size - the total scrollable area
    private Vector2 _contentSize = Vector2.Zero;
    public Vector2 ContentSize
    {
        get => _contentSize;
        set
        {
            _contentSize = value;
            ClampScrollOffset();
        }
    }

    // Scroll offset
    private Vector2 _scrollOffset = Vector2.Zero;
    public Vector2 ScrollOffset
    {
        get => _scrollOffset;
        set
        {
            _scrollOffset = value;
            ClampScrollOffset();
        }
    }

    // Scrollbar appearance
    public Color ScrollbarBackground { get; set; } = new Color(40, 40, 40, 200);
    public Color ScrollbarThumb { get; set; } = new Color(100, 100, 100, 220);
    public Color ScrollbarThumbHovered { get; set; } = new Color(140, 140, 140, 255);
    public int ScrollbarWidth { get; set; } = 12;
    public int ScrollbarPadding { get; set; } = 2;
    public Border? ScrollbarTrackBorder { get; set; }
    public Border? ScrollbarThumbBorder { get; set; }

    // Scroll behavior
    public float ScrollSpeed { get; set; } = 40f; // Pixels per scroll wheel notch
    public bool EnableHorizontalScroll { get; set; } = true;
    public bool EnableVerticalScroll { get; set; } = true;
    public bool ShowScrollbars { get; set; } = true;
    public bool AutoHideScrollbars { get; set; } = true; // Only show when content exceeds view

    // Drag state
    private bool _isDragging = false;
    private Point _dragStart;
    private Vector2 _scrollOffsetAtDragStart;

    // Scrollbar hover state
    private bool _verticalThumbHovered = false;
    private bool _horizontalThumbHovered = false;
    private bool _isDraggingVerticalThumb = false;
    private bool _isDraggingHorizontalThumb = false;
    private float _thumbDragOffset = 0f;

    // Scissor state for clipping
    private static readonly RasterizerState _scissorRasterizer = new() { ScissorTestEnable = true };

    // Computed properties
    public bool CanScrollVertically => ContentSize.Y > Size.Y;
    public bool CanScrollHorizontally => ContentSize.X > Size.X;
    public float MaxScrollX => Math.Max(0, ContentSize.X - Size.X + (CanScrollVertically && ShowScrollbars ? ScrollbarWidth : 0));
    public float MaxScrollY => Math.Max(0, ContentSize.Y - Size.Y + (CanScrollHorizontally && ShowScrollbars ? ScrollbarWidth : 0));

    public override void Update(GameTime gameTime)
    {
        var pos = Position.GetVector2();
        // Use raw mouse position for ScrollPanel's own logic (screen space)
        var mousePos = ControlState.GetRawMousePosition();
        var panelRect = new Rectangle(pos.ToPoint(), Size.ToPoint());
        var mouseInPanel = panelRect.Contains(mousePos);
        var mouseDelta = ControlState.GetMouseDelta();

        // Calculate scrollbar rectangles
        var (verticalTrack, verticalThumb) = GetVerticalScrollbarRects();
        var (horizontalTrack, horizontalThumb) = GetHorizontalScrollbarRects();

        // Update hover state
        _verticalThumbHovered = verticalThumb.Contains(mousePos);
        _horizontalThumbHovered = horizontalThumb.Contains(mousePos);

        // Handle scrollbar thumb dragging
        if (ControlState.GetPressedMouseButtons().Length > 0)
        {
            if (_verticalThumbHovered && CanScrollVertically)
            {
                _isDraggingVerticalThumb = true;
                _thumbDragOffset = mousePos.Y - verticalThumb.Y;
            }
            else if (_horizontalThumbHovered && CanScrollHorizontally)
            {
                _isDraggingHorizontalThumb = true;
                _thumbDragOffset = mousePos.X - horizontalThumb.X;
            }
            else if (mouseInPanel && !verticalTrack.Contains(mousePos) && !horizontalTrack.Contains(mousePos))
            {
                // Start drag-to-scroll
                _isDragging = true;
                _dragStart = mousePos;
                _scrollOffsetAtDragStart = _scrollOffset;
            }
        }

        // Handle thumb dragging
        if (_isDraggingVerticalThumb)
        {
            if (ControlState.GetHeldMouseButtons().Length > 0)
            {
                // Account for padding at top and bottom
                var usableTrackHeight = verticalTrack.Height - ScrollbarPadding * 2 - verticalThumb.Height;
                if (usableTrackHeight > 0)
                {
                    var thumbY = mousePos.Y - _thumbDragOffset - verticalTrack.Y - ScrollbarPadding;
                    var scrollRatio = Math.Clamp(thumbY / usableTrackHeight, 0f, 1f);
                    _scrollOffset.Y = scrollRatio * MaxScrollY;
                }
            }
            else
            {
                _isDraggingVerticalThumb = false;
            }
        }

        if (_isDraggingHorizontalThumb)
        {
            if (ControlState.GetHeldMouseButtons().Length > 0)
            {
                // Account for padding at left and right
                var usableTrackWidth = horizontalTrack.Width - ScrollbarPadding * 2 - horizontalThumb.Width;
                if (usableTrackWidth > 0)
                {
                    var thumbX = mousePos.X - _thumbDragOffset - horizontalTrack.X - ScrollbarPadding;
                    var scrollRatio = Math.Clamp(thumbX / usableTrackWidth, 0f, 1f);
                    _scrollOffset.X = scrollRatio * MaxScrollX;
                }
            }
            else
            {
                _isDraggingHorizontalThumb = false;
            }
        }

        // Handle drag-to-scroll
        if (_isDragging)
        {
            if (ControlState.GetHeldMouseButtons().Length > 0)
            {
                var delta = new Vector2(
                    _dragStart.X - mousePos.X,
                    _dragStart.Y - mousePos.Y
                );

                if (EnableHorizontalScroll)
                    _scrollOffset.X = _scrollOffsetAtDragStart.X + delta.X;
                if (EnableVerticalScroll)
                    _scrollOffset.Y = _scrollOffsetAtDragStart.Y + delta.Y;

                ClampScrollOffset();
            }
            else
            {
                _isDragging = false;
            }
        }

        // Handle mouse wheel scrolling
        if (mouseInPanel)
        {
            var wheelDelta = ControlState.GetScrollWheelDelta();
            if (wheelDelta != 0 && EnableVerticalScroll)
            {
                _scrollOffset.Y -= wheelDelta / 120f * ScrollSpeed;
                ClampScrollOffset();
            }
        }

        // Request cursor type
        if (_isDragging || _isDraggingVerticalThumb || _isDraggingHorizontalThumb)
            ControlState.RequestCursor(CursorType.Grab);
        else if (_verticalThumbHovered || _horizontalThumbHovered)
            ControlState.RequestCursor(CursorType.Pointer);

        // Update children with offset position
        // Note: We don't call base.Update() here because we need to offset child positions
        UpdateChildrenWithOffset(gameTime);
    }

    private void UpdateChildrenWithOffset(GameTime gameTime)
    {
        // Set mouse offset so children's hit detection accounts for scroll position
        // The offset transforms screen-space mouse to content-space coordinates
        var previousOffset = ControlState.MouseOffset;
        ControlState.MouseOffset = _scrollOffset;

        foreach (var child in Children.ToArray())
        {
            child.Update(gameTime);
        }

        // Restore previous offset
        ControlState.MouseOffset = previousOffset;
    }

    private void ClampScrollOffset()
    {
        _scrollOffset.X = Math.Clamp(_scrollOffset.X, 0, MaxScrollX);
        _scrollOffset.Y = Math.Clamp(_scrollOffset.Y, 0, MaxScrollY);
    }

    private (Rectangle track, Rectangle thumb) GetVerticalScrollbarRects()
    {
        var pos = Position.GetVector2();
        var showHorizontal = CanScrollHorizontally && ShowScrollbars && (!AutoHideScrollbars || ContentSize.X > Size.X);
        var borderThickness = Border?.Thickness ?? 0;

        // Position scrollbar inside the border
        var trackX = (int)(pos.X + Size.X - ScrollbarWidth - borderThickness);
        var trackY = (int)pos.Y + borderThickness;
        var trackHeight = (int)Size.Y - (showHorizontal ? ScrollbarWidth : 0) - borderThickness * 2;

        var track = new Rectangle(trackX, trackY, ScrollbarWidth, trackHeight);

        if (!CanScrollVertically || MaxScrollY <= 0)
            return (track, Rectangle.Empty);

        // Account for padding at top and bottom of track for thumb movement
        var usableTrackHeight = trackHeight - ScrollbarPadding * 2;
        var viewRatio = Size.Y / ContentSize.Y;
        var thumbHeight = Math.Max(20, (int)(usableTrackHeight * viewRatio));
        var scrollRatio = _scrollOffset.Y / MaxScrollY;
        var thumbY = trackY + ScrollbarPadding + (int)((usableTrackHeight - thumbHeight) * scrollRatio);

        var thumb = new Rectangle(
            trackX + ScrollbarPadding,
            thumbY,
            ScrollbarWidth - ScrollbarPadding * 2,
            thumbHeight
        );

        return (track, thumb);
    }

    private (Rectangle track, Rectangle thumb) GetHorizontalScrollbarRects()
    {
        var pos = Position.GetVector2();
        var showVertical = CanScrollVertically && ShowScrollbars && (!AutoHideScrollbars || ContentSize.Y > Size.Y);
        var borderThickness = Border?.Thickness ?? 0;

        // Position scrollbar inside the border
        var trackX = (int)pos.X + borderThickness;
        var trackY = (int)(pos.Y + Size.Y - ScrollbarWidth - borderThickness);
        var trackWidth = (int)Size.X - (showVertical ? ScrollbarWidth : 0) - borderThickness * 2;

        var track = new Rectangle(trackX, trackY, trackWidth, ScrollbarWidth);

        if (!CanScrollHorizontally || MaxScrollX <= 0)
            return (track, Rectangle.Empty);

        // Account for padding at left and right of track for thumb movement
        var usableTrackWidth = trackWidth - ScrollbarPadding * 2;
        var viewRatio = Size.X / ContentSize.X;
        var thumbWidth = Math.Max(20, (int)(usableTrackWidth * viewRatio));
        var scrollRatio = _scrollOffset.X / MaxScrollX;
        var thumbX = trackX + ScrollbarPadding + (int)((usableTrackWidth - thumbWidth) * scrollRatio);

        var thumb = new Rectangle(
            thumbX,
            trackY + ScrollbarPadding,
            thumbWidth,
            ScrollbarWidth - ScrollbarPadding * 2
        );

        return (track, thumb);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var pos = Position.GetVector2();
        var graphicsDevice = spriteBatch.GraphicsDevice;

        // Draw panel background
        if (Background.A > 0)
        {
            spriteBatch.Draw(
                RendererHelper.WhitePixel,
                new Rectangle((int)pos.X, (int)pos.Y, (int)Size.X, (int)Size.Y),
                Background
            );
        }

        // Draw border
        RendererHelper.Draw(spriteBatch, Border, pos, Size, Scale);

        // Calculate content area (excluding scrollbars and border)
        var showVerticalScrollbar = CanScrollVertically && ShowScrollbars && (!AutoHideScrollbars || ContentSize.Y > Size.Y);
        var showHorizontalScrollbar = CanScrollHorizontally && ShowScrollbars && (!AutoHideScrollbars || ContentSize.X > Size.X);
        var borderThickness = Border?.Thickness ?? 0;

        var contentWidth = Size.X - (showVerticalScrollbar ? ScrollbarWidth : 0) - borderThickness * 2;
        var contentHeight = Size.Y - (showHorizontalScrollbar ? ScrollbarWidth : 0) - borderThickness * 2;

        // End current batch to apply scissor clipping for content
        spriteBatch.End();

        var previousScissor = graphicsDevice.ScissorRectangle;
        graphicsDevice.ScissorRectangle = new Rectangle(
            (int)pos.X + borderThickness,
            (int)pos.Y + borderThickness,
            (int)contentWidth,
            (int)contentHeight
        );

        // Create transform matrix for scroll offset
        var scrollTransform = Matrix.CreateTranslation(-_scrollOffset.X, -_scrollOffset.Y, 0);

        spriteBatch.Begin(
            samplerState: SamplerState.PointClamp,
            rasterizerState: _scissorRasterizer,
            transformMatrix: scrollTransform
        );

        // Draw children - the transform matrix handles the offset
        foreach (var child in Children.ToArray())
        {
            child.Draw(spriteBatch);
        }

        // End clipped batch
        spriteBatch.End();
        graphicsDevice.ScissorRectangle = previousScissor;

        // Resume normal drawing for scrollbars
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        // Draw scrollbars
        if (showVerticalScrollbar)
        {
            DrawVerticalScrollbar(spriteBatch);
        }

        if (showHorizontalScrollbar)
        {
            DrawHorizontalScrollbar(spriteBatch);
        }

        // Draw corner if both scrollbars are visible
        if (showVerticalScrollbar && showHorizontalScrollbar)
        {
            var cornerRect = new Rectangle(
                (int)(pos.X + Size.X - ScrollbarWidth - borderThickness),
                (int)(pos.Y + Size.Y - ScrollbarWidth - borderThickness),
                ScrollbarWidth,
                ScrollbarWidth
            );
            spriteBatch.Draw(RendererHelper.WhitePixel, cornerRect, ScrollbarBackground);
        }
    }

    private void DrawVerticalScrollbar(SpriteBatch spriteBatch)
    {
        var (track, thumb) = GetVerticalScrollbarRects();

        // Draw track
        spriteBatch.Draw(RendererHelper.WhitePixel, track, ScrollbarBackground);
        if (ScrollbarTrackBorder is not null)
            RendererHelper.Draw(spriteBatch, ScrollbarTrackBorder, new Vector2(track.X, track.Y), new Vector2(track.Width, track.Height), Vector2.One);

        // Draw thumb
        if (thumb != Rectangle.Empty)
        {
            var thumbColor = (_verticalThumbHovered || _isDraggingVerticalThumb)
                ? ScrollbarThumbHovered
                : ScrollbarThumb;
            spriteBatch.Draw(RendererHelper.WhitePixel, thumb, thumbColor);
            if (ScrollbarThumbBorder is not null)
                RendererHelper.Draw(spriteBatch, ScrollbarThumbBorder, new Vector2(thumb.X, thumb.Y), new Vector2(thumb.Width, thumb.Height), Vector2.One);
        }
    }

    private void DrawHorizontalScrollbar(SpriteBatch spriteBatch)
    {
        var (track, thumb) = GetHorizontalScrollbarRects();

        // Draw track
        spriteBatch.Draw(RendererHelper.WhitePixel, track, ScrollbarBackground);
        if (ScrollbarTrackBorder is not null)
            RendererHelper.Draw(spriteBatch, ScrollbarTrackBorder, new Vector2(track.X, track.Y), new Vector2(track.Width, track.Height), Vector2.One);

        // Draw thumb
        if (thumb != Rectangle.Empty)
        {
            var thumbColor = (_horizontalThumbHovered || _isDraggingHorizontalThumb)
                ? ScrollbarThumbHovered
                : ScrollbarThumb;
            spriteBatch.Draw(RendererHelper.WhitePixel, thumb, thumbColor);
            if (ScrollbarThumbBorder is not null)
                RendererHelper.Draw(spriteBatch, ScrollbarThumbBorder, new Vector2(thumb.X, thumb.Y), new Vector2(thumb.Width, thumb.Height), Vector2.One);
        }
    }

    /// <summary>
    /// Scrolls to ensure the specified rectangle is visible
    /// </summary>
    public void ScrollToView(Rectangle rect)
    {
        if (rect.X < _scrollOffset.X)
            _scrollOffset.X = rect.X;
        else if (rect.Right > _scrollOffset.X + Size.X)
            _scrollOffset.X = rect.Right - Size.X;

        if (rect.Y < _scrollOffset.Y)
            _scrollOffset.Y = rect.Y;
        else if (rect.Bottom > _scrollOffset.Y + Size.Y)
            _scrollOffset.Y = rect.Bottom - Size.Y;

        ClampScrollOffset();
    }

    /// <summary>
    /// Scrolls to the top-left corner
    /// </summary>
    public void ScrollToTop()
    {
        _scrollOffset = Vector2.Zero;
    }

    /// <summary>
    /// Scrolls to the bottom
    /// </summary>
    public void ScrollToBottom()
    {
        _scrollOffset.Y = MaxScrollY;
    }
}
