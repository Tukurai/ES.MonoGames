using Components;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PocketCardLeague.Consts;
using PocketCardLeague.Enums;
using PocketCardLeague.Helpers;
using PocketCardLeague.SpriteMaps;
using System;
using System.Collections.Generic;

namespace PocketCardLeague.Components;

public class BerryCardComponent : CardComponent
{
    public List<BerryEnergyType> BerryTypes { get; set; } = [BerryEnergyType.Green];
    public List<BerryEffectType> BerryEffects { get; set; } = [BerryEffectType.None];
    public string? BerrySpriteId { get; set; } = null;

    public BerryCardComponent() : base()
    {
    }

    public override void BuildVisuals()
    {
        base.BuildVisuals();

        var layout = CardLayoutLoader.BerryLayout;
        Size = new Vector2(layout.Width, layout.Height);

        FrontSprite = SceneLoader.ParseSprite(layout.FrontSprite);
        BackSprite = SceneLoader.ParseSprite(layout.BackSprite);

        if (!FaceUp) return;

        var s = Scale;
        var layers = new List<(BaseComponent Component, Vector2 Offset, float Layer)>();

        // Berry image sprite centered in the sprite area
        if (BerrySpriteId is not null)
        {
            var result = ContentHelper.GetTextureResult<BerriesSpriteAtlas>(BerrySpriteId);
            if (result is not null)
            {
                var berrySprite = new Sprite($"berry_img_{CardName}")
                {
                    Scale = s,
                };
                berrySprite.SetFromAtlas(result);
                berrySprite.Origin = Vector2.Zero;

                var area = layout.BerrySprite;
                var spriteW = result.AtlasEntry.FrameWidth;
                var spriteH = result.AtlasEntry.FrameHeight;
                var areaX = AlignX(area.X, area.AreaWidth, area.Align);
                layers.Add((berrySprite, new Vector2(
                    (areaX + (area.AreaWidth - spriteW) / 2f) * s.X,
                    (area.Y + (area.AreaHeight - spriteH) / 2f) * s.Y
                ), area.Layer));
            }
        }

        // Berry energy type dots — small, with alignment
        for (int i = 0; i < BerryTypes.Count; i++)
        {
            var berryName = "berry_" + BerryTypes[i].ToString().ToLowerInvariant() + "_small";
            var berryResult = ContentHelper.GetTextureResult<BerriesSpriteAtlas>(berryName);
            if (berryResult is not null)
            {
                var dotSprite = new Sprite($"berry_dot_{i}_{CardName}")
                {
                    Scale = s,
                };
                dotSprite.SetFromAtlas(berryResult);
                dotSprite.Origin = Vector2.Zero;

                var dotW = berryResult.AtlasEntry.FrameWidth;
                var dotsX = AlignX(layout.BerryDots.X, dotW, layout.BerryDots.Align);
                layers.Add((dotSprite, new Vector2(
                    (dotsX + i * (dotW + layout.BerryDots.Spacing)) * s.X,
                    layout.BerryDots.Y * s.Y
                ), layout.BerryDots.Layer));
            }
        }

        // Type background bars (overlaid, white for berry cards)
        AddTypeBack("type_2_back", Color.White, layout.TypeBacks, s, layers, false);
        AddTypeBack("type_1_back", Color.White, layout.TypeBacks, s, layers, true);

        // Custom overlays from XML
        foreach (var overlay in layout.Overlays)
        {
            if (string.IsNullOrEmpty(overlay.Sprite))
                continue;
            var overlayResult = SceneLoader.ParseSprite(overlay.Sprite);
            if (overlayResult is not null)
            {
                var overlaySprite = new Sprite($"berry_overlay_{overlay.Sprite}_{CardName}")
                {
                    Scale = new Vector2(overlay.ScaleX * s.X, overlay.ScaleY * s.Y),
                    Tint = Color.White * overlay.Opacity,
                };
                overlaySprite.SetFromAtlas(overlayResult);
                overlaySprite.Origin = Vector2.Zero;

                var overlayW = overlayResult.AtlasEntry.FrameWidth * overlay.ScaleX;
                var overlayX = AlignX(overlay.X, (float)overlayW, overlay.Align);
                layers.Add((overlaySprite, new Vector2(overlayX * s.X, overlay.Y * s.Y), overlay.Layer));
            }
        }

        // Name label
        if (!string.IsNullOrEmpty(CardName))
            AddCardLabel($"berry_name_{CardName}", CardName, layout.NameLabel, s, layers);

        // Sort by layer and commit
        layers.Sort((a, b) => a.Layer.CompareTo(b.Layer));
        foreach (var (component, offset, _) in layers)
            AddOverlay(component, offset);
    }

    private void AddTypeBack(string spriteName, Color tint, TypeBackLayout layout,
        Vector2 scale, List<(BaseComponent, Vector2, float)> layers, bool flip)
    {
        var result = ContentHelper.GetTextureResult<CardPartsSpriteAtlas>(spriteName);
        if (result is null) return;

        var sprite = new Sprite($"berry_{spriteName}_{CardName}")
        {
            Scale = scale,
            Tint = tint * layout.Opacity,
        };
        sprite.SetFromAtlas(result);

        if (result.AtlasEntry.Rotated)
        {
            sprite.Rotation = MathF.PI / 2f;
            sprite.Origin = new Vector2(0, result.AtlasEntry.FrameHeight);
        }
        else
            sprite.Origin = Vector2.Zero;

        if (flip)
            sprite.Effects = result.AtlasEntry.Rotated
                ? SpriteEffects.FlipVertically
                : SpriteEffects.FlipHorizontally;

        layers.Add((sprite, new Vector2(layout.X * scale.X, layout.Y * scale.Y), layout.Layer));
    }

    private void AddCardLabel(string name, string text, LabelLayout layout, Vector2 scale,
        List<(BaseComponent, Vector2, float)> layers)
    {
        var label = new BitmapLabel(name)
        {
            Text = text,
            FontFamily = Fonts.M3x6,
            FontSize = (int)layout.FontSize,
            TextColor = layout.TextColor ?? Color.White,
            Border = new Border(layout.BorderWidth, layout.BorderColor ?? Color.Black),
            Scale = scale,
        };

        if (layout.Align.Equals("Center", StringComparison.OrdinalIgnoreCase))
        {
            label.Alignment = TextAlignment.Center;
            label.MaxWidth = (int)(Size.X * scale.X);
            layers.Add((label, new Vector2(0, layout.Y * scale.Y), layout.Layer));
        }
        else if (layout.Align.Equals("Right", StringComparison.OrdinalIgnoreCase))
        {
            label.Alignment = TextAlignment.Right;
            label.MaxWidth = (int)((Size.X - layout.X) * scale.X);
            layers.Add((label, new Vector2(0, layout.Y * scale.Y), layout.Layer));
        }
        else
            layers.Add((label, new Vector2(layout.X * scale.X, layout.Y * scale.Y), layout.Layer));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}
