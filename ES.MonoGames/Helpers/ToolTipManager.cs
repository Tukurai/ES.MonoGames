using Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helpers;

public static class ToolTipManager
{
    public static SpriteFont? DefaultFont { get; set; }

    private static ToolTip? _activeToolTip;
    private const int CursorOffset = 16;

    public static void Request(ToolTip tooltip)
    {
        _activeToolTip = tooltip;
    }

    public static void Clear()
    {
        _activeToolTip = null;
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        if (_activeToolTip is null)
            return;

        var font = _activeToolTip.Font ?? DefaultFont;
        if (font is null)
            return;

        var cursor = ControlState.GetRawMousePosition();
        var textSize = font.MeasureString(_activeToolTip.Text);
        var padding = _activeToolTip.Padding;
        var boxWidth = (int)textSize.X + padding * 2;
        var boxHeight = (int)textSize.Y + padding * 2;

        // Try placing to the right of cursor
        var x = cursor.X + CursorOffset;

        // If it overflows the right edge, place to the left
        if (x + boxWidth > ScaleManager.VirtualWidth)
            x = cursor.X - CursorOffset - boxWidth;

        // Clamp Y to keep on screen
        var y = cursor.Y;
        if (y + boxHeight > ScaleManager.VirtualHeight)
            y = ScaleManager.VirtualHeight - boxHeight;
        if (y < 0)
            y = 0;

        // Draw background
        var bgRect = new Rectangle(x, y, boxWidth, boxHeight);
        spriteBatch.Draw(RendererHelper.WhitePixel, bgRect, _activeToolTip.Background);

        // Draw text
        var textPos = new Vector2(x + padding, y + padding);
        spriteBatch.DrawString(font, _activeToolTip.Text, textPos, _activeToolTip.TextColor);
    }
}
