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
using System.Text.Json.Serialization;

namespace PocketCardLeague.Components;

public class PokemonCardComponent : CardComponent
{
    public PokeCard? Card { get; set; }
    public bool ShowCurrentHp { get; set; } = false;

    [JsonIgnore]
    public string? SpriteIdentifier
    {
        get
        {
            if (Card?.BasePokemon is not null)
                return Card.BasePokemon.SpriteIdentifier;
            if (Card?.BasePokemon.Id > 0)
                return PokeDex.Entries.Find(e => e.Id == Card.BasePokemon.Id)?.SpriteIdentifier;
            return null;
        }
    }

    public override void BuildVisuals()
    {
        base.BuildVisuals();

        var layout = CardLayoutLoader.PokemonLayout;
        Size = new Vector2(layout.Width, layout.Height);
        Tint = Card?.InnatePower == 4 ? TypeColors.Get(Card.BasePokemon.Types[0]) : Color.White;

        FrontSprite = SceneLoader.ParseSprite(layout.FrontSprite);
        BackSprite = SceneLoader.ParseSprite(layout.BackSprite);

        if (!FaceUp) return;

        var s = Scale;
        var layers = new List<(BaseComponent Component, Vector2 Offset, float Layer)>();

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
                layers.Add((pokemonSprite, new Vector2(
                    (areaX + (area.AreaWidth - spriteW) / 2f) * s.X,
                    (area.Y + (area.AreaHeight - spriteH) / 2f) * s.Y
                ), area.Layer));
            }
        }

        // Level bar
        var lvlIndex = Math.Clamp(Card?.Level ?? 0, 0, 10);
        var lvlName = $"level_{lvlIndex}_back";
        var lvlResult = ContentHelper.GetTextureResult<CardPartsSpriteAtlas>(lvlName);
        if (lvlResult is not null)
        {
            var lvlSprite = new Sprite($"card_level_{CardName}")
            {
                Scale = s,
            };
            lvlSprite.Tint = Tint;
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
            layers.Add((lvlSprite, new Vector2(lvlX * s.X, layout.LevelBar.Y * s.Y), layout.LevelBar.Layer));
        }

        // Type icons — vertical stack
        for (int i = 0; i < Card?.BasePokemon.Types.Count; i++)
        {
            var typeName = Card.BasePokemon.Types[i].ToString().ToLowerInvariant();
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
                layers.Add((typeSprite, new Vector2(
                    typeX * s.X,
                    (layout.Types.Y + i * (typeH + layout.Types.Spacing)) * s.Y
                ), layout.Types.Layer));
            }
        }

        // Cost dots — small berry dots, left-aligned
        for (int i = 0; i < Card?.Cost.Count; i++)
        {
            var berryName = "berry_" + Card?.Cost[i].ToString().ToLowerInvariant() + "_small";
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
                layers.Add((costSprite, new Vector2(
                    (costsX + i * (dotW + layout.Costs.Spacing)) * s.X,
                    layout.Costs.Y * s.Y
                ), layout.Costs.Layer));
            }
        }

        // Glyphs — vertical stack, opposite side of types
        for (int i = 0; i < Card?.Glyphs.Count; i++)
        {
            var glyphName = Card.Glyphs[i].ToLowerInvariant();
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
                layers.Add((glyphSprite, new Vector2(
                    glyphX * s.X,
                    (layout.Glyphs.Y + i * (glyphH + layout.Glyphs.Spacing)) * s.Y
                ), layout.Glyphs.Layer));
            }
        }

        // Type background bars (overlaid, behind name)
        var type1Color = TypeColors.Get(Card?.BasePokemon.Type1 ?? PokemonType.Normal);
        var type2Color = Card?.BasePokemon.Type2 is not null
            ? TypeColors.Get(Card.BasePokemon.Type2.Value)
            : Color.White;
        AddTypeBack("type_2_back", type2Color, layout.TypeBacks, s, layers, false);
        AddTypeBack("type_1_back", type1Color, layout.TypeBacks, s, layers, true);

        // Custom overlays from XML
        foreach (var overlay in layout.Overlays)
        {
            if (string.IsNullOrEmpty(overlay.Sprite))
                continue;
            var overlayResult = SceneLoader.ParseSprite(overlay.Sprite);
            if (overlayResult is not null)
            {
                var overlaySprite = new Sprite($"card_overlay_{overlay.Sprite}_{CardName}")
                {
                    Scale = new Vector2(overlay.ScaleX * s.X, overlay.ScaleY * s.Y),
                    Tint = Tint * overlay.Opacity,
                };
                overlaySprite.SetFromAtlas(overlayResult);
                overlaySprite.Origin = Vector2.Zero;

                var overlayW = overlayResult.AtlasEntry.FrameWidth * overlay.ScaleX;
                var overlayX = AlignX(overlay.X, (float)overlayW, overlay.Align);
                layers.Add((overlaySprite, new Vector2(overlayX * s.X, overlay.Y * s.Y), overlay.Layer));
            }
        }

        // Innate decoration — show innate_plus or innate_minus sprites centered
        var innateCount = (Card?.InnatePower ?? 1) - 1;
        if (innateCount != 0)
        {
            var innateName = innateCount > 0 ? "innate_plus" : "innate_minus";
            var innateResult = ContentHelper.GetTextureResult<CardPartsSpriteAtlas>(innateName);
            if (innateResult is not null)
            {
                var absCount = Math.Abs(innateCount);
                var innateLayout = layout.InnateDecoration;
                var spriteW = innateResult.AtlasEntry.FrameWidth;
                var totalW = absCount * spriteW + (absCount - 1) * innateLayout.Spacing;
                var startX = AlignX(innateLayout.X, totalW, innateLayout.Align);

                for (int i = 0; i < absCount; i++)
                {
                    var innateSprite = new Sprite($"card_innate_{i}_{CardName}")
                    {
                        Scale = s,
                        Tint = Tint,
                    };
                    innateSprite.SetFromAtlas(innateResult);
                    innateSprite.Origin = Vector2.Zero;

                    var x = startX + i * (spriteW + innateLayout.Spacing);
                    layers.Add((innateSprite, new Vector2(x * s.X, innateLayout.Y * s.Y), innateLayout.Layer));
                }
            }
        }

        // Shiny decoration — show shiny sprite if pokemon is shiny
        if (Card?.BasePokemon?.Shiny == true)
        {
            var shinyResult = ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("shiny");
            if (shinyResult is not null)
            {
                var shinyLayout = layout.ShinyDecoration;
                var shinySprite = new Sprite($"card_shiny_{CardName}")
                {
                    Scale = s,
                };
                shinySprite.SetFromAtlas(shinyResult);
                shinySprite.Origin = Vector2.Zero;

                var shinyW = shinyResult.AtlasEntry.FrameWidth;
                var shinyX = AlignX(shinyLayout.X, shinyW, shinyLayout.Align);
                layers.Add((shinySprite, new Vector2(shinyX * s.X, shinyLayout.Y * s.Y), shinyLayout.Layer));
            }
        }

        // Labels
        AddCardLabel($"card_name_{CardName}", CardName, layout.NameLabel, s, layers);
        if (Card?.BasePokemon.Id > 0)
            AddCardLabel($"card_dex_{CardName}", $"{Card.BasePokemon.Id}", layout.DexIdLabel, s, layers);
        AddCardLabel($"card_lv_{CardName}", $"lv {Card?.Level}", layout.LevelLabel, s, layers);
        if (ShowCurrentHp && Card?.MaxHp is not null)
            AddCardLabel($"card_hp_{CardName}", $"{Card?.HP}", layout.HpLabel, s, layers);
        else
            AddCardLabel($"card_hp_{CardName}", $"{Card?.MaxHp}", layout.HpLabel, s, layers);
        if (Card?.Atk is not null)
            AddCardLabel($"card_atk_{CardName}", $"{Card?.Atk}", layout.AtkLabel, s, layers);
        if (Card?.Def is not null)
            AddCardLabel($"card_def_{CardName}", $"{Card?.Def}", layout.DefLabel, s, layers);

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

        var sprite = new Sprite($"card_{spriteName}_{CardName}")
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
