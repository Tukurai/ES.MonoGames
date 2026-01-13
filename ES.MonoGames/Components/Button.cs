using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Helpers;

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

    private SpriteFont? Font { get; set; } = font;

    private float _lengthCache = -1f;
    private float _heightCache = -1f;
    private Vector2 _textPositionCache = Vector2.Zero;

    public override void Initialize()
    {
        if (_lengthCache < 0f && _heightCache < 0f)
        {
            if (Font is not null)
            {
                _lengthCache = Font.MeasureString(Text).X;
                _heightCache = Font.MeasureString(Text).Y;

                // Calculate text position based on centering and padding
                _textPositionCache = Position.GetVector2();
                if (Centered)
                {
                    _textPositionCache.X += (Size.X - _lengthCache) / 2;
                    _textPositionCache.Y += (Size.Y - _heightCache) / 2;
                }
                else
                {
                    _textPositionCache.X += Padding;
                    _textPositionCache.Y += Padding;
                }
            }
        }

        base.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
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

        if (Font is not null)
            if (QuickDraw)
                RendererHelper.DrawOutlinedStringFast(spriteBatch, Font, Text, _textPositionCache, TextColor, TextBorder);
            else
                RendererHelper.DrawOutlinedString(spriteBatch, Font, Text, _textPositionCache, TextColor, TextBorder);

        base.Draw(spriteBatch);
    }
}
