using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class ButtonsSpriteAtlas : SpriteAtlas
{
    public ButtonsSpriteAtlas() : base()
    {
        Atlas = [new("Buttons")
        {
            Name = "Buttons",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:7d16bde0e7c360bc8900b2e9bc4903d9:b4f02e79120f1d99cd6752230eaef903:a68c7291fd959d506666c9fd9a7121a4$",
            SpriteAtlas =
            {
                new("blue_pass_large", false, 0, 0, 31, 24, 31, 24, 0.5f, 0.5f),
                new("blue_pass_large_active", false, 0, 24, 31, 24, 31, 24, 0.5f, 0.5f),
                new("blue_up_large", false, 0, 48, 31, 24, 31, 24, 0.5f, 0.5f),
                new("blue_up_large_active", false, 0, 72, 31, 24, 31, 24, 0.5f, 0.5f),
                new("cycle", false, 0, 96, 16, 16, 16, 16, 0.5f, 0.5f),
                new("cycle_active", false, 16, 96, 16, 16, 16, 16, 0.5f, 0.5f),
                new("deck", false, 0, 112, 16, 16, 16, 16, 0.5f, 0.5f),
                new("deck_active", false, 16, 112, 16, 16, 16, 16, 0.5f, 0.5f),
                new("discord_large", false, 0, 128, 31, 24, 31, 24, 0.5f, 0.5f),
                new("discord_large_active", false, 0, 152, 31, 24, 31, 24, 0.5f, 0.5f),
                new("double_back", false, 0, 176, 16, 16, 16, 16, 0.5f, 0.5f),
                new("double_back_active", false, 16, 176, 16, 16, 16, 16, 0.5f, 0.5f),
                new("double_back_negative", false, 0, 192, 16, 16, 16, 16, 0.5f, 0.5f),
                new("double_forward", false, 16, 192, 16, 16, 16, 16, 0.5f, 0.5f),
                new("double_forward_active", false, 0, 208, 16, 16, 16, 16, 0.5f, 0.5f),
                new("double_forward_negative", false, 16, 208, 16, 16, 16, 16, 0.5f, 0.5f),
                new("five", false, 0, 224, 16, 16, 16, 16, 0.5f, 0.5f),
                new("five_active", false, 16, 224, 16, 16, 16, 16, 0.5f, 0.5f),
                new("four", false, 0, 240, 16, 16, 16, 16, 0.5f, 0.5f),
                new("four_active", false, 16, 240, 16, 16, 16, 16, 0.5f, 0.5f),
                new("g", false, 0, 256, 15, 16, 15, 16, 0.5f, 0.5f),
                new("g_active", false, 15, 256, 15, 16, 15, 16, 0.5f, 0.5f),
                new("green_check", false, 0, 272, 16, 16, 16, 16, 0.5f, 0.5f),
                new("green_check_active", false, 16, 272, 16, 16, 16, 16, 0.5f, 0.5f),
                new("green_check_large", false, 0, 288, 31, 24, 31, 24, 0.5f, 0.5f),
                new("green_check_large_active", false, 0, 312, 31, 24, 31, 24, 0.5f, 0.5f),
                new("green_down", false, 0, 336, 16, 16, 16, 16, 0.5f, 0.5f),
                new("green_down_active", false, 16, 336, 16, 16, 16, 16, 0.5f, 0.5f),
                new("grey_up_large", false, 0, 352, 31, 24, 31, 24, 0.5f, 0.5f),
                new("grey_up_large_active", false, 0, 376, 31, 24, 31, 24, 0.5f, 0.5f),
                new("m", false, 0, 400, 15, 16, 15, 16, 0.5f, 0.5f),
                new("m_active", false, 15, 400, 15, 16, 15, 16, 0.5f, 0.5f),
                new("one", false, 0, 416, 16, 16, 16, 16, 0.5f, 0.5f),
                new("one_active", false, 16, 416, 16, 16, 16, 16, 0.5f, 0.5f),
                new("questionmark", false, 0, 432, 16, 16, 16, 16, 0.5f, 0.5f),
                new("questionmark_active", false, 16, 432, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_cycle", false, 0, 448, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_cycle_active", false, 16, 448, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_dots_large", false, 0, 464, 31, 24, 31, 24, 0.5f, 0.5f),
                new("red_dots_large_active", false, 0, 488, 31, 24, 31, 24, 0.5f, 0.5f),
                new("red_exclaim", false, 0, 512, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_exclaim_active", false, 16, 512, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_out", false, 0, 528, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_out_active", false, 16, 528, 16, 16, 16, 16, 0.5f, 0.5f),
                new("shiny", false, 0, 544, 15, 16, 15, 16, 0.5f, 0.5f),
                new("shiny_active", false, 15, 544, 15, 16, 15, 16, 0.5f, 0.5f),
                new("sword", false, 0, 560, 16, 16, 16, 16, 0.5f, 0.5f),
                new("sword_active", false, 16, 560, 16, 16, 16, 16, 0.5f, 0.5f),
                new("three", false, 0, 576, 16, 16, 16, 16, 0.5f, 0.5f),
                new("three_active", false, 16, 576, 16, 16, 16, 16, 0.5f, 0.5f),
                new("toggle_atk_off", false, 0, 592, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_atk_on", false, 15, 592, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_def_off", false, 0, 608, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_def_on", false, 15, 608, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_dex_off", false, 0, 624, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_dex_on", false, 15, 624, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_innate_off", false, 0, 640, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_innate_on", false, 15, 640, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_level_off", false, 0, 656, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_level_on", false, 15, 656, 15, 16, 15, 16, 0.5f, 0.5f),
                new("top", false, 0, 672, 7, 9, 7, 9, 0.5f, 0.5f),
                new("top_active", false, 7, 672, 7, 9, 7, 9, 0.5f, 0.5f),
                new("two", false, 14, 672, 16, 16, 16, 16, 0.5f, 0.5f),
                new("two_active", false, 0, 688, 16, 16, 16, 16, 0.5f, 0.5f),

            }
        }];
    }

    public static TextureResult Blue_pass_large => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("blue_pass_large")!;
    public static TextureResult Blue_pass_large_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("blue_pass_large_active")!;
    public static TextureResult Blue_up_large => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("blue_up_large")!;
    public static TextureResult Blue_up_large_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("blue_up_large_active")!;
    public static TextureResult Cycle => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("cycle")!;
    public static TextureResult Cycle_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("cycle_active")!;
    public static TextureResult Deck => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("deck")!;
    public static TextureResult Deck_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("deck_active")!;
    public static TextureResult Discord_large => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("discord_large")!;
    public static TextureResult Discord_large_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("discord_large_active")!;
    public static TextureResult Double_back => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("double_back")!;
    public static TextureResult Double_back_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("double_back_active")!;
    public static TextureResult Double_back_negative => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("double_back_negative")!;
    public static TextureResult Double_forward => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("double_forward")!;
    public static TextureResult Double_forward_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("double_forward_active")!;
    public static TextureResult Double_forward_negative => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("double_forward_negative")!;
    public static TextureResult Five => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("five")!;
    public static TextureResult Five_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("five_active")!;
    public static TextureResult Four => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("four")!;
    public static TextureResult Four_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("four_active")!;
    public static TextureResult G => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("g")!;
    public static TextureResult G_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("g_active")!;
    public static TextureResult Green_check => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("green_check")!;
    public static TextureResult Green_check_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("green_check_active")!;
    public static TextureResult Green_check_large => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("green_check_large")!;
    public static TextureResult Green_check_large_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("green_check_large_active")!;
    public static TextureResult Green_down => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("green_down")!;
    public static TextureResult Green_down_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("green_down_active")!;
    public static TextureResult Grey_up_large => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("grey_up_large")!;
    public static TextureResult Grey_up_large_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("grey_up_large_active")!;
    public static TextureResult M => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("m")!;
    public static TextureResult M_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("m_active")!;
    public static TextureResult One => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("one")!;
    public static TextureResult One_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("one_active")!;
    public static TextureResult Questionmark => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("questionmark")!;
    public static TextureResult Questionmark_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("questionmark_active")!;
    public static TextureResult Red_cycle => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("red_cycle")!;
    public static TextureResult Red_cycle_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("red_cycle_active")!;
    public static TextureResult Red_dots_large => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("red_dots_large")!;
    public static TextureResult Red_dots_large_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("red_dots_large_active")!;
    public static TextureResult Red_exclaim => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("red_exclaim")!;
    public static TextureResult Red_exclaim_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("red_exclaim_active")!;
    public static TextureResult Red_out => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("red_out")!;
    public static TextureResult Red_out_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("red_out_active")!;
    public static TextureResult Shiny => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("shiny")!;
    public static TextureResult Shiny_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("shiny_active")!;
    public static TextureResult Sword => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("sword")!;
    public static TextureResult Sword_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("sword_active")!;
    public static TextureResult Three => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("three")!;
    public static TextureResult Three_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("three_active")!;
    public static TextureResult Toggle_atk_off => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("toggle_atk_off")!;
    public static TextureResult Toggle_atk_on => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("toggle_atk_on")!;
    public static TextureResult Toggle_def_off => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("toggle_def_off")!;
    public static TextureResult Toggle_def_on => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("toggle_def_on")!;
    public static TextureResult Toggle_dex_off => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("toggle_dex_off")!;
    public static TextureResult Toggle_dex_on => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("toggle_dex_on")!;
    public static TextureResult Toggle_innate_off => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("toggle_innate_off")!;
    public static TextureResult Toggle_innate_on => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("toggle_innate_on")!;
    public static TextureResult Toggle_level_off => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("toggle_level_off")!;
    public static TextureResult Toggle_level_on => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("toggle_level_on")!;
    public static TextureResult Top => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("top")!;
    public static TextureResult Top_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("top_active")!;
    public static TextureResult Two => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("two")!;
    public static TextureResult Two_active => ContentHelper.GetTextureResult<ButtonsSpriteAtlas>("two_active")!;

}