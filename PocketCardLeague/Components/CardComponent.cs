using Components;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PocketCardLeague.Components;

public class CardComponent : Panel
{
    public string CardName { get; set; } = string.Empty;
    [JsonIgnore] public TextureResult? BackSprite { get; set; }
    [JsonIgnore] public TextureResult? FrontSprite { get; set; }
    [JsonIgnore] public TextureResult? Image { get; set; }
    [JsonIgnore] public bool FaceUp { get; set; } = true;

    /// <summary>
    /// When true, BuildVisuals is called automatically on the first Update.
    /// Used by XML factories where Scale/Position are set after construction.
    /// </summary>
    [JsonIgnore] public bool AutoBuild { get; set; } = false;

    private bool _needsBuild = false;
    private readonly List<(BaseComponent Component, Vector2 Offset)> _overlays = [];

    public Card(string? name = null) : base(name)
    {
        Size = new Vector2(57, 80);
        OnPositionChanged += RepositionOverlays;
    }

    public override void Update(GameTime gameTime)
    {
        if (AutoBuild && !_needsBuild)
        {
            _needsBuild = true;
            AutoBuild = false;
            BuildVisuals();
        }

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var pos = Position.GetVector2();
        var sprite = FaceUp ? FrontSprite : BackSprite;

        if (sprite is not null)
        {
            var entry = sprite.AtlasEntry;
            var sourceRect = new Rectangle(entry.FrameX, entry.FrameY, entry.FrameWidth, entry.FrameHeight);

            if (entry.Rotated)
            {
                var origin = new Vector2(0, entry.FrameHeight);
                spriteBatch.Draw(sprite.Texture, pos, sourceRect, ApplyOpacity(Color.White),
                    MathF.PI / 2f, origin, Scale, SpriteEffects.None, 0f);
            }
            else
                spriteBatch.Draw(sprite.Texture, pos, sourceRect, ApplyOpacity(Color.White),
                    0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }

        base.Draw(spriteBatch);
    }

    /// <summary>
    /// Builds all visual overlay children from current card data.
    /// Call this after setting card properties.
    /// </summary>
    public virtual void BuildVisuals()
    {
        foreach (var (component, _) in _overlays)
            Children.Remove(component);
        _overlays.Clear();
    }

    /// <summary>
    /// Calculates X position based on alignment.
    /// Left: X is pixels from left edge. Right: X is pixels from right edge.
    /// </summary>
    protected float AlignX(float x, float elementWidth, string align)
    {
        if (align.Equals("Right", StringComparison.OrdinalIgnoreCase))
            return Size.X - x - elementWidth;
        return x;
    }

    protected void AddOverlay(BaseComponent component, Vector2 offset)
    {
        component.Position = new Anchor(offset, Position);
        _overlays.Add((component, offset));
        Children.Add(component);
    }

    private void RepositionOverlays()
    {
        foreach (var (component, offset) in _overlays)
            component.Position = new Anchor(offset, Position);
    }
}
