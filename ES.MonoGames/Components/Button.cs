using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Components;

public class Button(string? name = null, string text = "Button", SpriteFont? font = null, Anchor? position = null, Vector2? size = null, bool centered = false) : BaseComponent(name, position, size)
{
    public Border Border { get; set; } = new Border();
    public string Text { get; set; } = text;
    public Color Background { get; set; } = Color.Gray;
    public Color TextColor { get; set; } = Color.White;
    public Border TextBorder { get; set; } = new Border(1, Color.Black);
    public int Padding { get; set; } = 10;
    public bool Centered { get; set; } = centered;
    public bool QuickDraw { get; set; } = false;
    public int PressDepth { get; set; } = 0;

    public SpriteFont? Font { get; set; } = font;

    // BitmapLabel-based text rendering
    public string? FontFamily { get; set; }
    public float BitmapFontSize { get; set; } = 16;

    private BitmapLabel? _bitmapLabel;

    private float _lengthCache = -1f;
    private float _heightCache = -1f;
    private string _cachedText = "";

    private void EnsureTextMeasured()
    {
        // Re-measure if font exists and text changed or never measured
        if (Font is not null && (_lengthCache < 0f || _cachedText != Text))
        {
            _lengthCache = Font.MeasureString(Text).X;
            _heightCache = Font.MeasureString(Text).Y;
            _cachedText = Text;
        }
    }

    private Vector2 GetTextPosition()
    {
        EnsureTextMeasured();

        var pos = Position.GetVector2();
        if (Centered && _lengthCache >= 0f)
        {
            pos.X += (Size.X - _lengthCache) / 2;
            pos.Y += (Size.Y - _heightCache) / 2;
        }
        else
        {
            pos.X += Padding;
            pos.Y += Padding;
        }
        return pos;
    }


    public override void Draw(SpriteBatch spriteBatch)
    {
        if (Background.A > 0)
        {
            spriteBatch.Draw(
                RendererHelper.WhitePixel,
                new Rectangle(
                    (int)Position.GetVector2().X,
                    (int)Position.GetVector2().Y,
                    (int)Size.X,
                    (int)Size.Y
                ),
                Background
            );
        }

        RendererHelper.Draw(spriteBatch, Border, Position.GetVector2(), Size, Scale);

        if (FontFamily is not null)
        {
            // BitmapLabel path
            if (_bitmapLabel is null)
            {
                _bitmapLabel = new BitmapLabel();
            }

            _bitmapLabel.Text = Text;
            _bitmapLabel.FontFamily = FontFamily;
            _bitmapLabel.FontSize = BitmapFontSize;
            _bitmapLabel.TextColor = TextColor;
            _bitmapLabel.Border = TextBorder;

            if (Centered)
            {
                _bitmapLabel.Alignment = TextAlignment.Center;
                _bitmapLabel.MaxWidth = (int)Size.X;
                var labelPos = Position.GetVector2();
                labelPos.Y += (Size.Y - BitmapFontSize) / 2;
                if (Pressed && PressDepth > 0)
                    labelPos.Y += PressDepth;
                _bitmapLabel.Position = new Anchor(labelPos);
            }
            else
            {
                _bitmapLabel.Alignment = TextAlignment.Left;
                _bitmapLabel.MaxWidth = 0;
                var labelPos = Position.GetVector2();
                labelPos.X += Padding;
                labelPos.Y += (Size.Y - BitmapFontSize) / 2;
                if (Pressed && PressDepth > 0)
                    labelPos.Y += PressDepth;
                _bitmapLabel.Position = new Anchor(labelPos);
            }

            _bitmapLabel.Draw(spriteBatch);
        }
        else if (Font is not null)
        {
            var textPos = GetTextPosition();
            if (Pressed && PressDepth > 0)
                textPos.Y += PressDepth;

            if (QuickDraw)
                RendererHelper.DrawOutlinedStringFast(spriteBatch, Font, Text, textPos, TextColor, TextBorder);
            else
                RendererHelper.DrawOutlinedString(spriteBatch, Font, Text, textPos, TextColor, TextBorder);
        }

        base.Draw(spriteBatch);
    }
}
