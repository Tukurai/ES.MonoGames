using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class CardPartsSpriteAtlas : SpriteAtlas
{
    public CardPartsSpriteAtlas() : base()
    {
        Atlas = [new("CardParts")
        {
            Name = "CardParts",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:a878ff5b60cd8dc18ad4772d3f424256:088faca42b8c7296e170555cc63ba9ec:5d15933ead00d54287f078d18191a377$",
            SpriteAtlas =
            {
                new("back_berry", false, 1, 1, 57, 80, 57, 80, 0.5f, 0.5f),
                new("back_mon", false, 1, 83, 57, 80, 57, 80, 0.5f, 0.5f),
                new("berry_effect_atk", false, 178, 95, 51, 9, 51, 9, 0.5f, 0.5f),
                new("berry_effect_def", false, 178, 106, 51, 9, 51, 9, 0.5f, 0.5f),
                new("berry_effect_empty", false, 178, 117, 51, 9, 51, 9, 0.5f, 0.5f),
                new("berry_effect_heal", false, 178, 128, 51, 9, 51, 9, 0.5f, 0.5f),
                new("berry_effect_jam", false, 178, 139, 51, 9, 51, 9, 0.5f, 0.5f),
                new("berry_effect_multi", false, 178, 150, 51, 9, 51, 9, 0.5f, 0.5f),
                new("berry_effect_return", false, 178, 161, 51, 9, 51, 9, 0.5f, 0.5f),
                new("card_berry", false, 60, 1, 57, 80, 57, 80, 0.5f, 0.5f),
                new("card_common", false, 60, 83, 57, 80, 57, 80, 0.5f, 0.5f),
                new("card_rare", true, 1, 165, 80, 57, 57, 80, 0.5f, 0.5f),
                new("level_0_back", true, 177, 172, 4, 51, 51, 4, 0.5f, 0.5f),
                new("level_1_back", true, 183, 172, 4, 51, 51, 4, 0.5f, 0.5f),
                new("level_2_back", true, 189, 172, 4, 51, 51, 4, 0.5f, 0.5f),
                new("level_3_back", true, 195, 172, 4, 51, 51, 4, 0.5f, 0.5f),
                new("level_4_back", true, 201, 172, 4, 51, 51, 4, 0.5f, 0.5f),
                new("level_5_back", true, 207, 172, 4, 51, 51, 4, 0.5f, 0.5f),
                new("level_6_back", true, 213, 172, 4, 51, 51, 4, 0.5f, 0.5f),
                new("level_7_back", true, 219, 172, 4, 51, 51, 4, 0.5f, 0.5f),
                new("level_8_back", true, 225, 172, 4, 51, 51, 4, 0.5f, 0.5f),
                new("level_9_back", true, 231, 95, 4, 51, 51, 4, 0.5f, 0.5f),
                new("level_10_back", true, 231, 148, 4, 51, 51, 4, 0.5f, 0.5f),
                new("slot_delete", false, 119, 1, 57, 80, 57, 80, 0.5f, 0.5f),
                new("slot_discard", false, 119, 83, 57, 80, 57, 80, 0.5f, 0.5f),
                new("slot_filled", true, 83, 165, 80, 57, 57, 80, 0.5f, 0.5f),
                new("slot_hollow", false, 178, 1, 57, 80, 57, 80, 0.5f, 0.5f),
                new("type_1_back", true, 165, 165, 10, 53, 53, 10, 0.5f, 0.5f),
                new("type_2_back", false, 178, 83, 53, 10, 53, 10, 0.5f, 0.5f),

            }
        }];
    }

    public static TextureResult Back_berry => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("back_berry")!;
    public static TextureResult Back_mon => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("back_mon")!;
    public static TextureResult Berry_effect_atk => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("berry_effect_atk")!;
    public static TextureResult Berry_effect_def => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("berry_effect_def")!;
    public static TextureResult Berry_effect_empty => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("berry_effect_empty")!;
    public static TextureResult Berry_effect_heal => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("berry_effect_heal")!;
    public static TextureResult Berry_effect_jam => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("berry_effect_jam")!;
    public static TextureResult Berry_effect_multi => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("berry_effect_multi")!;
    public static TextureResult Berry_effect_return => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("berry_effect_return")!;
    public static TextureResult Card_berry => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("card_berry")!;
    public static TextureResult Card_common => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("card_common")!;
    public static TextureResult Card_rare => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("card_rare")!;
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
    public static TextureResult Slot_delete => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("slot_delete")!;
    public static TextureResult Slot_discard => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("slot_discard")!;
    public static TextureResult Slot_filled => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("slot_filled")!;
    public static TextureResult Slot_hollow => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("slot_hollow")!;
    public static TextureResult Type_1_back => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("type_1_back")!;
    public static TextureResult Type_2_back => ContentHelper.GetTextureResult<CardPartsSpriteAtlas>("type_2_back")!;

}