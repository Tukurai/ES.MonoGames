using Components;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PocketCardLeague.Consts;
using PocketCardLeague.Enums;
using PocketCardLeague.Helpers;
using PocketCardLeague.SpriteMaps;
using System.Collections.Generic;

namespace PocketCardLeague.Components;

public class BerryCard : Card
{
    public List<BerryEnergyType> BerryTypes { get; set; } = [BerryEnergyType.Green];
    public List<BerryEffectType> BerryEffects { get; set; } = [BerryEffectType.None];
    public string? BerrySpriteId { get; set; } = null;

    public BerryCard() : base()
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
                AddOverlay(berrySprite, new Vector2(
                    (areaX + (area.AreaWidth - spriteW) / 2f) * s.X,
                    (area.Y + (area.AreaHeight - spriteH) / 2f) * s.Y
                ));
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
                AddOverlay(dotSprite, new Vector2(
                    (dotsX + i * (dotW + layout.BerryDots.Spacing)) * s.X,
                    layout.BerryDots.Y * s.Y
                ));
            }
        }

        // Name label
        if (!string.IsNullOrEmpty(CardName))
        {
            var nameLayout = layout.NameLabel;
            var label = new BitmapLabel($"berry_name_{CardName}")
            {
                Text = CardName,
                FontFamily = Fonts.M3x6,
                FontSize = (int)nameLayout.FontSize,
                TextColor = Color.White,
                Scale = s,
            };
            var estimatedWidth = CardName.Length * nameLayout.FontSize * 0.5f;
            var labelX = AlignX(nameLayout.X, (float)estimatedWidth, nameLayout.Align);
            AddOverlay(label, new Vector2(labelX * s.X, nameLayout.Y * s.Y));
        }
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
