using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helpers;

public enum SlideDirection
{
    Left,
    Right,
    Up,
    Down
}

public class SlideTransition(SlideDirection direction, float duration = 0.4f) : SceneTransition(duration)
{
    public SlideDirection Direction { get; } = direction;
    public bool SharedBackground { get; set; }

    public override void Draw(SpriteBatch spriteBatch, GraphicsDevice device,
        RenderTarget2D oldTarget, RenderTarget2D newTarget, Rectangle destRect)
    {
        var eased = EaseInOut(Progress);
        var width = destRect.Width;
        var height = destRect.Height;

        // Calculate offsets based on direction
        var (oldOffsetX, oldOffsetY, newOffsetX, newOffsetY) = Direction switch
        {
            SlideDirection.Left => (
                (int)(-width * eased), 0,
                (int)(width * (1f - eased)), 0),
            SlideDirection.Right => (
                (int)(width * eased), 0,
                (int)(-width * (1f - eased)), 0),
            SlideDirection.Up => (
                0, (int)(-height * eased),
                0, (int)(height * (1f - eased))),
            SlideDirection.Down => (
                0, (int)(height * eased),
                0, (int)(-height * (1f - eased))),
            _ => (0, 0, 0, 0)
        };

        var oldRect = new Rectangle(
            destRect.X + oldOffsetX,
            destRect.Y + oldOffsetY,
            destRect.Width,
            destRect.Height);

        var newRect = new Rectangle(
            destRect.X + newOffsetX,
            destRect.Y + newOffsetY,
            destRect.Width,
            destRect.Height);

        spriteBatch.Draw(oldTarget, oldRect, Color.White);
        spriteBatch.Draw(newTarget, newRect, Color.White);
    }
}
