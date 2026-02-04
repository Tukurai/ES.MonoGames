using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helpers;

public class FadeTransition(float duration = 0.6f) : SceneTransition(duration)
{
    public override void Draw(SpriteBatch spriteBatch, GraphicsDevice device,
        RenderTarget2D oldTarget, RenderTarget2D newTarget, Rectangle destRect)
    {
        if (Progress < 0.5f)
        {
            // Phase 1: Draw old scene, fade to black
            var alpha = Progress * 2f; // 0 -> 1
            spriteBatch.Draw(oldTarget, destRect, Color.White);
            spriteBatch.Draw(RendererHelper.WhitePixel, destRect, Color.Black * alpha);
        }
        else
        {
            // Phase 2: Draw new scene, fade from black
            var alpha = 1f - (Progress - 0.5f) * 2f; // 1 -> 0
            spriteBatch.Draw(newTarget, destRect, Color.White);
            spriteBatch.Draw(RendererHelper.WhitePixel, destRect, Color.Black * alpha);
        }
    }
}
