using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Helpers;

namespace Components;

public class Label(string? name = null, string text = "", SpriteFont? font = null, Anchor? position = null, bool center = false, int maxWidth = 0) : BaseComponent(name, position)
{
    public string Text { get; set; } = text;
    public Color Color { get; set; } = Color.White;
    public Border Border { get; set; } = new Border();
    public bool QuickDraw { get; set; } = false;
    public bool Center { get; set; } = center;
    public int MaxWidth { get; set; } = maxWidth;

    private SpriteFont? Font { get; set; } = font;

    private float _lengthCache = -1f;
    private float _heightCache = -1f;

    public override void Initialize()
    {
        if (_lengthCache < 0f && _heightCache < 0f)
        {
            if (Font is not null)
            {
                var measure = Font.MeasureString(Text);
                _lengthCache = measure.X;
                _heightCache = measure.Y;
                Size = new Vector2(_lengthCache, _heightCache);

                if (Center)
                {
                    Position.TransformPosition(new Vector2(
                        (MaxWidth / 2) - (_lengthCache / 2),
                        0
                    ));
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
        if (Font is not null)
            if (QuickDraw)
                RendererHelper.DrawOutlinedStringFast(spriteBatch, Font, Text, Position.GetVector2(), Color, Border);
            else
                RendererHelper.DrawOutlinedString(spriteBatch, Font, Text, Position.GetVector2(), Color, Border);

        base.Draw(spriteBatch);
    }
}
