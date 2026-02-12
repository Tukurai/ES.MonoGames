using Components;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PocketCardLeague.Enums;
using PocketCardLeague.Helpers;
using PocketCardLeague.SpriteMaps;
using System;
using System.Collections.Generic;

namespace PocketCardLeague.Components;

public class PokemonCardComponent : CardComponent
{
    public int Level { get; set; } = 1;
    public int InnatePower { get; set; } = 0;
    public string? Nickname { get; set; } = null;
    public int DexId { get; set; } = 0;
    public PokemonType Type1 { get; set; } = PokemonType.Normal;
    public PokemonType? Type2 { get; set; } = null;
    public PokeDexEntry? BasePokemon { get; set; } = null;
    public int HP { get; set; } = 0;
    public int Atk { get; set; } = 0;
    public int Def { get; set; } = 0;
    public List<string> Glyphs { get; set; } = [];

    public List<BerryEnergyType> Cost { get; set; } = [BerryEnergyType.Green, BerryEnergyType.Green, BerryEnergyType.Void];

    [JsonIgnore]
    public List<PokemonType> Types => Type2 is null ? [Type1] : [Type1, Type2.Value];

    [JsonIgnore]
    public string? SpriteIdentifier
    {
        get
        {
            if (BasePokemon is not null)
                return BasePokemon.SpriteIdentifier;
            if (DexId > 0)
                return PokeDex.Entries.Find(e => e.Id == DexId)?.SpriteIdentifier;
            return null;
        }
    }

    public override void BuildVisuals()
    {
        base.BuildVisuals();

        var layout = CardLayoutLoader.PokemonLayout;
        Size = new Vector2(layout.Width, layout.Height);

        FrontSprite = SceneLoader.ParseSprite(layout.FrontSprite);
        BackSprite = SceneLoader.ParseSprite(layout.BackSprite);

        if (!FaceUp) return;

        var s = Scale;

        // Pokemon image sprite centered in the sprite area
        var spriteId = SpriteIdentifier;
        if (spriteId is not null)
        {
            var result = ContentHelper.GetTextureResult<PokemonSpriteAtlas>(spriteId);
            if (result is not null)
            {
                Image = result;
                var pokemonSprite = new Sprite($"card_pokemon_{CardName}")
                {
                    Scale = s,
                };
                pokemonSprite.SetFromAtlas(result);
                pokemonSprite.Origin = Vector2.Zero;

                var area = layout.PokemonSprite;
                var spriteW = result.AtlasEntry.FrameWidth;
                var spriteH = result.AtlasEntry.FrameHeight;
                var areaX = AlignX(area.X, area.AreaWidth, area.Align);
                AddOverlay(pokemonSprite, new Vector2(
                    (areaX + (area.AreaWidth - spriteW) / 2f) * s.X,
                    (area.Y + (area.AreaHeight - spriteH) / 2f) * s.Y
                ));
            }
        }

        // Level bar
        var lvlIndex = Math.Clamp(Level, 0, 10);
        var lvlName = $"level_{lvlIndex}_back";
        var lvlResult = ContentHelper.GetTextureResult<CardPartsSpriteAtlas>(lvlName);
        if (lvlResult is not null)
        {
            var lvlSprite = new Sprite($"card_level_{CardName}")
            {
                Scale = s,
            };
            lvlSprite.SetFromAtlas(lvlResult);

            float lvlW, lvlH;
            if (lvlResult.AtlasEntry.Rotated)
            {
                lvlSprite.Rotation = MathF.PI / 2f;
                lvlSprite.Origin = new Vector2(0, lvlResult.AtlasEntry.FrameHeight);
                lvlW = lvlResult.AtlasEntry.FrameHeight;
                lvlH = lvlResult.AtlasEntry.FrameWidth;
            }
            else
            {
                lvlSprite.Origin = Vector2.Zero;
                lvlW = lvlResult.AtlasEntry.FrameWidth;
                lvlH = lvlResult.AtlasEntry.FrameHeight;
            }

            var lvlX = AlignX(layout.LevelBar.X, lvlW, layout.LevelBar.Align);
            AddOverlay(lvlSprite, new Vector2(lvlX * s.X, layout.LevelBar.Y * s.Y));
        }

        // Type icons — vertical stack
        for (int i = 0; i < Types.Count; i++)
        {
            var typeName = Types[i].ToString().ToLowerInvariant();
            var typeResult = ContentHelper.GetTextureResult<TypesSpriteAtlas>(typeName);
            if (typeResult is not null)
            {
                var typeSprite = new Sprite($"card_type_{i}_{CardName}")
                {
                    Scale = s,
                };
                typeSprite.SetFromAtlas(typeResult);
                typeSprite.Origin = Vector2.Zero;

                var typeW = typeResult.AtlasEntry.FrameWidth;
                var typeH = typeResult.AtlasEntry.FrameHeight;
                var typeX = AlignX(layout.Types.X, typeW, layout.Types.Align);
                AddOverlay(typeSprite, new Vector2(
                    typeX * s.X,
                    (layout.Types.Y + i * (typeH + layout.Types.Spacing)) * s.Y
                ));
            }
        }

        // Cost dots — small berry dots, left-aligned
        for (int i = 0; i < Cost.Count; i++)
        {
            var berryName = "berry_" + Cost[i].ToString().ToLowerInvariant() + "_small";
            var berryResult = ContentHelper.GetTextureResult<BerriesSpriteAtlas>(berryName);
            if (berryResult is not null)
            {
                var costSprite = new Sprite($"card_cost_{i}_{CardName}")
                {
                    Scale = s,
                };
                costSprite.SetFromAtlas(berryResult);
                costSprite.Origin = Vector2.Zero;

                var dotW = berryResult.AtlasEntry.FrameWidth;
                var costsX = AlignX(layout.Costs.X, dotW, layout.Costs.Align);
                AddOverlay(costSprite, new Vector2(
                    (costsX + i * (dotW + layout.Costs.Spacing)) * s.X,
                    layout.Costs.Y * s.Y
                ));
            }
        }

        // Glyphs — vertical stack, opposite side of types
        for (int i = 0; i < Glyphs.Count; i++)
        {
            var glyphName = Glyphs[i].ToLowerInvariant();
            var glyphResult = ContentHelper.GetTextureResult<GlyphsSpriteAtlas>(glyphName);
            if (glyphResult is not null)
            {
                var glyphSprite = new Sprite($"card_glyph_{i}_{CardName}")
                {
                    Scale = s,
                };
                glyphSprite.SetFromAtlas(glyphResult);
                glyphSprite.Origin = Vector2.Zero;

                var glyphW = glyphResult.AtlasEntry.FrameWidth;
                var glyphH = glyphResult.AtlasEntry.FrameHeight;
                var glyphX = AlignX(layout.Glyphs.X, glyphW, layout.Glyphs.Align);
                AddOverlay(glyphSprite, new Vector2(
                    glyphX * s.X,
                    (layout.Glyphs.Y + i * (glyphH + layout.Glyphs.Spacing)) * s.Y
                ));
            }
        }

        // Labels
        AddCardLabel($"card_name_{CardName}", CardName, layout.NameLabel, s);
        if (DexId > 0)
            AddCardLabel($"card_dex_{CardName}", $"{DexId}", layout.DexIdLabel, s);
        AddCardLabel($"card_lv_{CardName}", $"Lv.{Level}", layout.LevelLabel, s);
        if (HP > 0)
            AddCardLabel($"card_hp_{CardName}", $"{HP}", layout.HpLabel, s);
        if (Atk > 0)
            AddCardLabel($"card_atk_{CardName}", $"{Atk}", layout.AtkLabel, s);
        if (Def > 0)
            AddCardLabel($"card_def_{CardName}", $"{Def}", layout.DefLabel, s);
    }

    private void AddCardLabel(string name, string text, LabelLayout layout, Vector2 scale)
    {
        var label = new BitmapLabel(name)
        {
            Text = text,
            FontFamily = Fonts.M3x6,
            FontSize = (int)layout.FontSize,
            TextColor = Color.White,
            Border = new Border(4, Color.Black),
            Scale = scale,
        };
        // Estimate label width for alignment (FontSize * chars * ~0.5 ratio for m3x6)
        var estimatedWidth = text.Length * layout.FontSize * 0.5f;
        var labelX = AlignX(layout.X, (float)estimatedWidth, layout.Align);
        AddOverlay(label, new Vector2(labelX * scale.X, layout.Y * scale.Y));
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
