using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Components;

/// <summary>
/// A SubScene is a panel-like component that can be used as a "page" within a SubSceneContainer.
/// It supports transitions when switching between SubScenes.
/// </summary>
public class SubScene : BaseComponent
{
    /// <summary>
    /// Background color of this SubScene.
    /// </summary>
    public Color Background { get; set; } = Color.Transparent;

    /// <summary>
    /// Border around this SubScene.
    /// </summary>
    public Border Border { get; set; } = new Border();

    /// <summary>
    /// Whether this SubScene is currently active (visible).
    /// Managed by the parent SubSceneContainer.
    /// </summary>
    public bool IsActive { get; internal set; } = false;

    public SubScene(string? name = null) : base(name) { }

    public override void Update(GameTime gameTime)
    {
        // Only update if active
        if (!IsActive)
            return;

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var pos = Position.GetVector2();

        // Draw background
        if (Background.A > 0)
        {
            spriteBatch.Draw(
                RendererHelper.WhitePixel,
                new Rectangle((int)pos.X, (int)pos.Y, (int)Size.X, (int)Size.Y),
                ApplyOpacity(Background)
            );
        }

        // Draw border
        if (Border.Thickness > 0)
        {
            var border = EffectiveOpacity < 1f
                ? new Border(Border.Thickness, ApplyOpacity(Border.Color))
                : Border;
            RendererHelper.Draw(spriteBatch, border, pos, Size, Scale);
        }

        // Draw children
        base.Draw(spriteBatch);
    }

    /// <summary>
    /// Draw only the content (children) without background - used during transitions.
    /// </summary>
    internal void DrawContent(SpriteBatch spriteBatch)
    {
        foreach (var child in Children)
            child.Draw(spriteBatch);
    }

    /// <summary>
    /// Draw only the background - used during transitions.
    /// </summary>
    internal void DrawBackground(SpriteBatch spriteBatch)
    {
        var pos = Position.GetVector2();

        if (Background.A > 0)
        {
            spriteBatch.Draw(
                RendererHelper.WhitePixel,
                new Rectangle((int)pos.X, (int)pos.Y, (int)Size.X, (int)Size.Y),
                ApplyOpacity(Background)
            );
        }
    }
}
