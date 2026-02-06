using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class EventsSpriteAtlas : SpriteAtlas
{
    public EventsSpriteAtlas() : base()
    {
        Atlas = [new("Events")
        {
            Name = "Events",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:b078ae0a3b92357e1ec0fbdb93d222cf:a915a784e8cdade5571116518dee0669:d23092bab5c0f677d96f6c3c203cbd18$",
            SpriteAtlas =
            {
                new("add_card", false, 27, 240, 26, 27, 26, 27, 0.5f, 0.5f),
                new("battle", false, 27, 269, 26, 27, 26, 27, 0.5f, 0.5f),
                new("berry", false, 25, 186, 26, 25, 26, 27, 0.5f, 0.5f),
                new("booster_1", false, 1, 298, 25, 27, 26, 27, 0.48f, 0.5f),
                new("booster_2", false, 28, 298, 25, 27, 26, 27, 0.48f, 0.5f),
                new("booster_3", false, 1, 327, 25, 27, 26, 27, 0.48f, 0.5f),
                new("booster_4", false, 28, 327, 25, 27, 26, 27, 0.48f, 0.5f),
                new("booster_5", false, 1, 356, 25, 27, 26, 27, 0.48f, 0.5f),
                new("booster_6", false, 28, 356, 25, 27, 26, 27, 0.48f, 0.5f),
                new("booster_7", false, 1, 385, 25, 27, 26, 27, 0.48f, 0.5f),
                new("booster_8", false, 28, 385, 25, 27, 26, 27, 0.48f, 0.5f),
                new("booster_9", false, 1, 414, 25, 27, 26, 27, 0.48f, 0.5f),
                new("booster_10", false, 28, 414, 25, 27, 26, 27, 0.48f, 0.5f),
                new("border", false, 1, 530, 44, 44, 44, 44, 0.5f, 0.5f),
                new("campfire", false, 1, 113, 25, 22, 26, 27, 0.52f, 0.4772727272727273f),
                new("campfire_grey", false, 28, 113, 25, 22, 26, 27, 0.52f, 0.4772727272727273f),
                new("cashout", false, 1, 267, 24, 27, 26, 27, 0.5f, 0.5f),
                new("deck_fire_", false, 1, 443, 25, 27, 26, 27, 0.48f, 0.5f),
                new("deck_grass", false, 28, 443, 25, 27, 26, 27, 0.48f, 0.5f),
                new("deck_water", false, 1, 472, 25, 27, 26, 27, 0.48f, 0.5f),
                new("deglyph", false, 1, 1, 25, 20, 26, 27, 0.48f, 0.525f),
                new("deglyph_grey", false, 28, 1, 25, 20, 26, 27, 0.48f, 0.525f),
                new("evolve", false, 1, 213, 26, 25, 26, 27, 0.5f, 0.46f),
                new("evolve_grey", false, 29, 213, 26, 25, 26, 27, 0.5f, 0.46f),
                new("glyp_grey", false, 1, 23, 25, 20, 26, 27, 0.48f, 0.525f),
                new("glyph", false, 28, 23, 25, 20, 26, 27, 0.48f, 0.525f),
                new("gym", false, 28, 472, 25, 27, 26, 27, 0.48f, 0.5f),
                new("innate", false, 1, 67, 24, 21, 26, 27, 0.5f, 0.5f),
                new("innate_grey", false, 27, 67, 24, 21, 26, 27, 0.5f, 0.5f),
                new("innate_transfer", false, 1, 137, 25, 22, 26, 27, 0.48f, 0.4772727272727273f),
                new("innate_transfer_grey", false, 28, 137, 25, 22, 26, 27, 0.48f, 0.4772727272727273f),
                new("levelup", false, 1, 501, 26, 27, 26, 27, 0.5f, 0.5f),
                new("levelup_grey", false, 29, 501, 26, 27, 26, 27, 0.5f, 0.5f),
                new("mega_evolve", false, 1, 45, 20, 20, 26, 27, 0.5f, 0.525f),
                new("mega_evolve_grey", false, 23, 45, 20, 20, 26, 27, 0.5f, 0.525f),
                new("return", false, 1, 161, 20, 23, 26, 27, 0.5f, 0.5f),
                new("sacrifice_grey", false, 1, 90, 22, 21, 26, 27, 0.5f, 0.5f),
                new("select_card", false, 1, 240, 24, 25, 26, 27, 0.4583333333333333f, 0.5f),
                new("swap", false, 23, 161, 22, 23, 26, 27, 0.5f, 0.5434782608695652f),
                new("swap_grey", false, 1, 186, 22, 23, 26, 27, 0.5f, 0.5434782608695652f),
                new("tribute", false, 25, 90, 22, 21, 26, 27, 0.5f, 0.5f),

            }
        }];
    }

    public static TextureResult Add_card => ContentHelper.GetTextureResult<EventsSpriteAtlas>("add_card")!;
    public static TextureResult Battle => ContentHelper.GetTextureResult<EventsSpriteAtlas>("battle")!;
    public static TextureResult Berry => ContentHelper.GetTextureResult<EventsSpriteAtlas>("berry")!;
    public static TextureResult Booster_1 => ContentHelper.GetTextureResult<EventsSpriteAtlas>("booster_1")!;
    public static TextureResult Booster_2 => ContentHelper.GetTextureResult<EventsSpriteAtlas>("booster_2")!;
    public static TextureResult Booster_3 => ContentHelper.GetTextureResult<EventsSpriteAtlas>("booster_3")!;
    public static TextureResult Booster_4 => ContentHelper.GetTextureResult<EventsSpriteAtlas>("booster_4")!;
    public static TextureResult Booster_5 => ContentHelper.GetTextureResult<EventsSpriteAtlas>("booster_5")!;
    public static TextureResult Booster_6 => ContentHelper.GetTextureResult<EventsSpriteAtlas>("booster_6")!;
    public static TextureResult Booster_7 => ContentHelper.GetTextureResult<EventsSpriteAtlas>("booster_7")!;
    public static TextureResult Booster_8 => ContentHelper.GetTextureResult<EventsSpriteAtlas>("booster_8")!;
    public static TextureResult Booster_9 => ContentHelper.GetTextureResult<EventsSpriteAtlas>("booster_9")!;
    public static TextureResult Booster_10 => ContentHelper.GetTextureResult<EventsSpriteAtlas>("booster_10")!;
    public static TextureResult Border => ContentHelper.GetTextureResult<EventsSpriteAtlas>("border")!;
    public static TextureResult Campfire => ContentHelper.GetTextureResult<EventsSpriteAtlas>("campfire")!;
    public static TextureResult Campfire_grey => ContentHelper.GetTextureResult<EventsSpriteAtlas>("campfire_grey")!;
    public static TextureResult Cashout => ContentHelper.GetTextureResult<EventsSpriteAtlas>("cashout")!;
    public static TextureResult Deck_fire_ => ContentHelper.GetTextureResult<EventsSpriteAtlas>("deck_fire_")!;
    public static TextureResult Deck_grass => ContentHelper.GetTextureResult<EventsSpriteAtlas>("deck_grass")!;
    public static TextureResult Deck_water => ContentHelper.GetTextureResult<EventsSpriteAtlas>("deck_water")!;
    public static TextureResult Deglyph => ContentHelper.GetTextureResult<EventsSpriteAtlas>("deglyph")!;
    public static TextureResult Deglyph_grey => ContentHelper.GetTextureResult<EventsSpriteAtlas>("deglyph_grey")!;
    public static TextureResult Evolve => ContentHelper.GetTextureResult<EventsSpriteAtlas>("evolve")!;
    public static TextureResult Evolve_grey => ContentHelper.GetTextureResult<EventsSpriteAtlas>("evolve_grey")!;
    public static TextureResult Glyp_grey => ContentHelper.GetTextureResult<EventsSpriteAtlas>("glyp_grey")!;
    public static TextureResult Glyph => ContentHelper.GetTextureResult<EventsSpriteAtlas>("glyph")!;
    public static TextureResult Gym => ContentHelper.GetTextureResult<EventsSpriteAtlas>("gym")!;
    public static TextureResult Innate => ContentHelper.GetTextureResult<EventsSpriteAtlas>("innate")!;
    public static TextureResult Innate_grey => ContentHelper.GetTextureResult<EventsSpriteAtlas>("innate_grey")!;
    public static TextureResult Innate_transfer => ContentHelper.GetTextureResult<EventsSpriteAtlas>("innate_transfer")!;
    public static TextureResult Innate_transfer_grey => ContentHelper.GetTextureResult<EventsSpriteAtlas>("innate_transfer_grey")!;
    public static TextureResult Levelup => ContentHelper.GetTextureResult<EventsSpriteAtlas>("levelup")!;
    public static TextureResult Levelup_grey => ContentHelper.GetTextureResult<EventsSpriteAtlas>("levelup_grey")!;
    public static TextureResult Mega_evolve => ContentHelper.GetTextureResult<EventsSpriteAtlas>("mega_evolve")!;
    public static TextureResult Mega_evolve_grey => ContentHelper.GetTextureResult<EventsSpriteAtlas>("mega_evolve_grey")!;
    public static TextureResult Return => ContentHelper.GetTextureResult<EventsSpriteAtlas>("return")!;
    public static TextureResult Sacrifice_grey => ContentHelper.GetTextureResult<EventsSpriteAtlas>("sacrifice_grey")!;
    public static TextureResult Select_card => ContentHelper.GetTextureResult<EventsSpriteAtlas>("select_card")!;
    public static TextureResult Swap => ContentHelper.GetTextureResult<EventsSpriteAtlas>("swap")!;
    public static TextureResult Swap_grey => ContentHelper.GetTextureResult<EventsSpriteAtlas>("swap_grey")!;
    public static TextureResult Tribute => ContentHelper.GetTextureResult<EventsSpriteAtlas>("tribute")!;

}