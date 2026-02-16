using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class CardPartsSpriteAtlas : SpriteAtlas
{
    public CardPartsSpriteAtlas() : base()
    {
        Atlas = [new("CardParts")
        {
            Name = "CardParts",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:38a3f9cc4d143aa8fdd54a4d6025b373:155e5c4ed0d0179ef760dc190bd9cea4:5d15933ead00d54287f078d18191a377$",
            SpriteAtlas =
            {
                new("back_berry", false, 1, 176, 57, 80, 57, 80, 0.5f, 0.5f),
                new("back_mon", false, 1, 258, 57, 80, 57, 80, 0.5f, 0.5f),
                new("berry_dark", false, 11, 1, 14, 4, 14, 4, 0.5f, 0.5f),
                new("berry_effect_atk", false, 1, 75, 51, 9, 51, 9, 0.5f, 0.5f),
                new("berry_effect_def", false, 1, 86, 51, 9, 51, 9, 0.5f, 0.5f),
                new("berry_effect_empty", false, 1, 97, 51, 9, 51, 9, 0.5f, 0.5f),
                new("berry_effect_heal", false, 1, 108, 51, 9, 51, 9, 0.5f, 0.5f),
                new("berry_effect_jam", false, 1, 119, 51, 9, 51, 9, 0.5f, 0.5f),
                new("berry_effect_multi", false, 1, 130, 51, 9, 51, 9, 0.5f, 0.5f),
                new("berry_effect_return", false, 1, 141, 51, 9, 51, 9, 0.5f, 0.5f),
                new("berry_light", false, 27, 1, 14, 4, 14, 4, 0.5f, 0.5f),
                new("card_berry", false, 1, 340, 57, 80, 57, 80, 0.5f, 0.5f),
                new("card_common", false, 1, 422, 57, 80, 57, 80, 0.5f, 0.5f),
                new("card_rare", false, 1, 504, 57, 80, 57, 80, 0.5f, 0.5f),
                new("innate_minus", false, 1, 1, 3, 3, 3, 3, 0.5f, 0.5f),
                new("innate_plus", false, 6, 1, 3, 3, 3, 3, 0.5f, 0.5f),
                new("level_0_back", false, 1, 9, 51, 4, 51, 4, 0.5f, 0.5f),
                new("level_1_back", false, 1, 15, 51, 4, 51, 4, 0.5f, 0.5f),
                new("level_2_back", false, 1, 21, 51, 4, 51, 4, 0.5f, 0.5f),
                new("level_3_back", false, 1, 27, 51, 4, 51, 4, 0.5f, 0.5f),
                new("level_4_back", false, 1, 33, 51, 4, 51, 4, 0.5f, 0.5f),
                new("level_5_back", false, 1, 39, 51, 4, 51, 4, 0.5f, 0.5f),
                new("level_6_back", false, 1, 45, 51, 4, 51, 4, 0.5f, 0.5f),
                new("level_7_back", false, 1, 51, 51, 4, 51, 4, 0.5f, 0.5f),
                new("level_8_back", false, 1, 57, 51, 4, 51, 4, 0.5f, 0.5f),
                new("level_9_back", false, 1, 63, 51, 4, 51, 4, 0.5f, 0.5f),
                new("level_10_back", false, 1, 69, 51, 4, 51, 4, 0.5f, 0.5f),
                new("shiny", false, 43, 1, 7, 6, 7, 6, 0.5f, 0.5f),
                new("slot_delete", false, 1, 586, 57, 80, 57, 80, 0.5f, 0.5f),
                new("slot_discard", false, 1, 668, 57, 80, 57, 80, 0.5f, 0.5f),
                new("slot_filled", false, 1, 750, 57, 80, 57, 80, 0.5f, 0.5f),
                new("slot_hollow", false, 1, 832, 57, 80, 57, 80, 0.5f, 0.5f),
                new("type_1_back", false, 1, 152, 53, 10, 53, 10, 0.5f, 0.5f),
                new("type_2_back", false, 1, 164, 53, 10, 53, 10, 0.5f, 0.5f),

            }
        }];
    }

    public static TextureResult Back_berry => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("back_berry")!;
    public static TextureResult Back_mon => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("back_mon")!;
    public static TextureResult Berry_dark => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("berry_dark")!;
    public static TextureResult Berry_effect_atk => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("berry_effect_atk")!;
    public static TextureResult Berry_effect_def => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("berry_effect_def")!;
    public static TextureResult Berry_effect_empty => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("berry_effect_empty")!;
    public static TextureResult Berry_effect_heal => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("berry_effect_heal")!;
    public static TextureResult Berry_effect_jam => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("berry_effect_jam")!;
    public static TextureResult Berry_effect_multi => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("berry_effect_multi")!;
    public static TextureResult Berry_effect_return => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("berry_effect_return")!;
    public static TextureResult Berry_light => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("berry_light")!;
    public static TextureResult Card_berry => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("card_berry")!;
    public static TextureResult Card_common => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("card_common")!;
    public static TextureResult Card_rare => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("card_rare")!;
    public static TextureResult Innate_minus => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("innate_minus")!;
    public static TextureResult Innate_plus => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("innate_plus")!;
    public static TextureResult Level_0_back => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("level_0_back")!;
    public static TextureResult Level_1_back => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("level_1_back")!;
    public static TextureResult Level_2_back => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("level_2_back")!;
    public static TextureResult Level_3_back => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("level_3_back")!;
    public static TextureResult Level_4_back => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("level_4_back")!;
    public static TextureResult Level_5_back => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("level_5_back")!;
    public static TextureResult Level_6_back => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("level_6_back")!;
    public static TextureResult Level_7_back => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("level_7_back")!;
    public static TextureResult Level_8_back => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("level_8_back")!;
    public static TextureResult Level_9_back => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("level_9_back")!;
    public static TextureResult Level_10_back => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("level_10_back")!;
    public static TextureResult Shiny => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("shiny")!;
    public static TextureResult Slot_delete => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("slot_delete")!;
    public static TextureResult Slot_discard => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("slot_discard")!;
    public static TextureResult Slot_filled => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("slot_filled")!;
    public static TextureResult Slot_hollow => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("slot_hollow")!;
    public static TextureResult Type_1_back => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("type_1_back")!;
    public static TextureResult Type_2_back => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("type_2_back")!;

}