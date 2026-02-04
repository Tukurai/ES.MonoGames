using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helpers;

public abstract class SceneTransition
{
    public float Duration { get; }
    public float Elapsed { get; private set; }
    public float Progress => Math.Clamp(Elapsed / Duration, 0f, 1f);
    public bool IsComplete => Elapsed >= Duration;

    protected SceneTransition(float duration)
        => Duration = duration;

    public void Update(GameTime gameTime)
        => Elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

    public abstract void Draw(SpriteBatch spriteBatch, GraphicsDevice device,
        RenderTarget2D oldTarget, RenderTarget2D newTarget, Rectangle destRect);

    public static float EaseInOut(float t)
        => t < 0.5f ? 4f * t * t * t : 1f - MathF.Pow(-2f * t + 2f, 3f) / 2f;
}
