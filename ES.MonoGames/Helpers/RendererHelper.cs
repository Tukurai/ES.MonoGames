using Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helpers;

public static class RendererHelper
{
    private static Texture2D _whitePixel = null!;

    public static Texture2D WhitePixel { get => _whitePixel; }

    /// <summary>
    /// Must be called once during startup.
    /// </summary>
    public static void Initialize(GraphicsDevice graphicsDevice)
    {
        _whitePixel = new Texture2D(graphicsDevice, 1, 1);
        _whitePixel.SetData([Color.White]);
    }

    public static void Draw(SpriteBatch spriteBatch, Border border, Vector2 position, Vector2 size, Vector2 scale)
    {
        if (border is null || border.Thickness <= 0 || border.Color.A == 0)
            return;

        var t = border.Thickness;
        var scaledSize = size * scale;

        // Top
        spriteBatch.Draw(WhitePixel, new Rectangle((int)position.X, (int)position.Y, (int)scaledSize.X, t), border.Color);
        // Bottom
        spriteBatch.Draw(WhitePixel, new Rectangle((int)position.X, (int)(position.Y + scaledSize.Y - t), (int)scaledSize.X, t), border.Color);
        // Left
        spriteBatch.Draw(WhitePixel, new Rectangle((int)position.X, (int)position.Y, t, (int)scaledSize.Y), border.Color);
        // Right
        spriteBatch.Draw(WhitePixel, new Rectangle((int)(position.X + scaledSize.X - t), (int)position.Y, t, (int)scaledSize.Y), border.Color);
    }

    public static void DrawOutlinedString(SpriteBatch spriteBatch, SpriteFont font, string text, Vector2 position, Color textColor, Border border)
    {
        if (border is not null && border.Thickness > 0 && border.Color.A > 0)
        {
            for (int x = -border.Thickness; x <= border.Thickness; x++)
            {
                for (int y = -border.Thickness; y <= border.Thickness; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    // optional: circular outline instead of square
                    if (x * x + y * y > border.Thickness * border.Thickness)
                        continue;

                    spriteBatch.DrawString(
                        font,
                        text,
                        position + new Vector2(x, y),
                        border.Color
                    );
                }
            }
        }

        spriteBatch.DrawString(font, text, position, textColor);
    }

    public static void DrawOutlinedStringFast(SpriteBatch spriteBatch, SpriteFont font, string text, Vector2 position, Color textColor, Border border)
    {
        if (border is not null && border.Thickness > 0 && border.Color.A > 0)
        {
            var offsets = new[]
            {
                new Vector2(-border.Thickness, 0),
                new Vector2(border.Thickness, 0),
                new Vector2(0, -border.Thickness),
                new Vector2(0, border.Thickness),
                new Vector2(-border.Thickness, -border.Thickness),
                new Vector2(border.Thickness, -border.Thickness),
                new Vector2(-border.Thickness, border.Thickness),
                new Vector2(border.Thickness, border.Thickness)
            };

            foreach (var offset in offsets)
                spriteBatch.DrawString(font, text, position + offset, border.Color);
        }

        spriteBatch.DrawString(font, text, position, textColor);
    }

}
