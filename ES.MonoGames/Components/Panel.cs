using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json.Serialization;

namespace Components;

public class Panel(string? name = null) : BaseComponent(name)
{
    [JsonIgnore] public Color Background { get; set; } = Color.Transparent;
    [JsonIgnore] public Border Border { get; set; } = new Border();

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

        base.Draw(spriteBatch);
    }
}
