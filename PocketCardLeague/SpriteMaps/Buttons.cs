using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class ButtonsSpriteAtlas : SpriteAtlas
{
    public ButtonsSpriteAtlas() : base()
    {
        Atlas = [new("Buttons")
        {
            Name = "Buttons",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:015bb7030b8b0308a26e087a3194b2da:0f0d23db5230666f73b410b7d1e30e65:a68c7291fd959d506666c9fd9a7121a4$",
            SpriteAtlas =
            {
                new("arrow_down", false, 0, 640, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_down_active", false, 14, 640, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_down_hover", false, 0, 654, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_down_small", false, 14, 654, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_down_small_active", false, 0, 668, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_down_small_hover", false, 14, 668, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_left", false, 0, 682, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_left_active", false, 14, 682, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_left_hover", false, 0, 696, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_left_small", false, 14, 696, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_left_small_active", false, 0, 710, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_left_small_hover", false, 14, 710, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_right", false, 0, 724, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_right_active", false, 14, 724, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_right_hover", false, 0, 738, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_right_small", false, 14, 738, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_right_small_active", false, 0, 752, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_right_small_hover", false, 14, 752, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_up", false, 0, 766, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_up_active", false, 14, 766, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_up_howver", false, 0, 780, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_up_small", false, 14, 780, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_up_small_active", false, 0, 794, 14, 14, 14, 14, 0.5f, 0.5f),
                new("arrow_up_small_hover", false, 14, 794, 14, 14, 14, 14, 0.5f, 0.5f),
                new("blue_pass_large", false, 0, 0, 31, 24, 31, 24, 0.5f, 0.5f),
                new("blue_pass_large_active", false, 0, 24, 31, 24, 31, 24, 0.5f, 0.5f),
                new("blue_up_large", false, 0, 48, 31, 24, 31, 24, 0.5f, 0.5f),
                new("blue_up_large_active", false, 0, 72, 31, 24, 31, 24, 0.5f, 0.5f),
                new("cycle", false, 0, 288, 16, 16, 16, 16, 0.5f, 0.5f),
                new("cycle_active", false, 16, 288, 16, 16, 16, 16, 0.5f, 0.5f),
                new("deck", false, 0, 304, 16, 16, 16, 16, 0.5f, 0.5f),
                new("deck_active", false, 16, 304, 16, 16, 16, 16, 0.5f, 0.5f),
                new("discord_large", false, 0, 96, 31, 24, 31, 24, 0.5f, 0.5f),
                new("discord_large_active", false, 0, 120, 31, 24, 31, 24, 0.5f, 0.5f),
                new("double_back", false, 0, 320, 16, 16, 16, 16, 0.5f, 0.5f),
                new("double_back_active", false, 16, 320, 16, 16, 16, 16, 0.5f, 0.5f),
                new("double_back_negative", false, 0, 336, 16, 16, 16, 16, 0.5f, 0.5f),
                new("double_forward", false, 16, 336, 16, 16, 16, 16, 0.5f, 0.5f),
                new("double_forward_active", false, 0, 352, 16, 16, 16, 16, 0.5f, 0.5f),
                new("double_forward_negative", false, 16, 352, 16, 16, 16, 16, 0.5f, 0.5f),
                new("five", false, 0, 368, 16, 16, 16, 16, 0.5f, 0.5f),
                new("five_active", false, 16, 368, 16, 16, 16, 16, 0.5f, 0.5f),
                new("four", false, 0, 384, 16, 16, 16, 16, 0.5f, 0.5f),
                new("four_active", false, 16, 384, 16, 16, 16, 16, 0.5f, 0.5f),
                new("green_check", false, 0, 400, 16, 16, 16, 16, 0.5f, 0.5f),
                new("green_check_active", false, 16, 400, 16, 16, 16, 16, 0.5f, 0.5f),
                new("green_check_large", false, 0, 144, 31, 24, 31, 24, 0.5f, 0.5f),
                new("green_check_large_active", false, 0, 168, 31, 24, 31, 24, 0.5f, 0.5f),
                new("green_down", false, 0, 416, 16, 16, 16, 16, 0.5f, 0.5f),
                new("green_down_active", false, 16, 416, 16, 16, 16, 16, 0.5f, 0.5f),
                new("grey_up_large", false, 0, 192, 31, 24, 31, 24, 0.5f, 0.5f),
                new("grey_up_large_active", false, 0, 216, 31, 24, 31, 24, 0.5f, 0.5f),
                new("one", false, 0, 432, 16, 16, 16, 16, 0.5f, 0.5f),
                new("one_active", false, 16, 432, 16, 16, 16, 16, 0.5f, 0.5f),
                new("questionmark", false, 0, 448, 16, 16, 16, 16, 0.5f, 0.5f),
                new("questionmark_active", false, 16, 448, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_cycle", false, 0, 464, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_cycle_active", false, 16, 464, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_dots_large", false, 0, 240, 31, 24, 31, 24, 0.5f, 0.5f),
                new("red_dots_large_active", false, 0, 264, 31, 24, 31, 24, 0.5f, 0.5f),
                new("red_exclaim", false, 0, 480, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_exclaim_active", false, 16, 480, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_out", false, 0, 496, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_out_active", false, 16, 496, 16, 16, 16, 16, 0.5f, 0.5f),
                new("sword", false, 0, 512, 16, 16, 16, 16, 0.5f, 0.5f),
                new("sword_active", false, 16, 512, 16, 16, 16, 16, 0.5f, 0.5f),
                new("three", false, 0, 528, 16, 16, 16, 16, 0.5f, 0.5f),
                new("three_active", false, 16, 528, 16, 16, 16, 16, 0.5f, 0.5f),
                new("toggle_atk_off", false, 0, 560, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_atk_on", false, 15, 560, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_def_off", false, 0, 576, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_def_on", false, 15, 576, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_dex_off", false, 0, 592, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_dex_on", false, 15, 592, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_innate_off", false, 0, 608, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_innate_on", false, 15, 608, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_level_off", false, 0, 624, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_level_on", false, 15, 624, 15, 16, 15, 16, 0.5f, 0.5f),
                new("two", false, 0, 544, 16, 16, 16, 16, 0.5f, 0.5f),
                new("two_active", false, 16, 544, 16, 16, 16, 16, 0.5f, 0.5f),

            }
        }];
    }

    public const string Arrow_down = "arrow_down";
    public const string Arrow_down_active = "arrow_down_active";
    public const string Arrow_down_hover = "arrow_down_hover";
    public const string Arrow_down_small = "arrow_down_small";
    public const string Arrow_down_small_active = "arrow_down_small_active";
    public const string Arrow_down_small_hover = "arrow_down_small_hover";
    public const string Arrow_left = "arrow_left";
    public const string Arrow_left_active = "arrow_left_active";
    public const string Arrow_left_hover = "arrow_left_hover";
    public const string Arrow_left_small = "arrow_left_small";
    public const string Arrow_left_small_active = "arrow_left_small_active";
    public const string Arrow_left_small_hover = "arrow_left_small_hover";
    public const string Arrow_right = "arrow_right";
    public const string Arrow_right_active = "arrow_right_active";
    public const string Arrow_right_hover = "arrow_right_hover";
    public const string Arrow_right_small = "arrow_right_small";
    public const string Arrow_right_small_active = "arrow_right_small_active";
    public const string Arrow_right_small_hover = "arrow_right_small_hover";
    public const string Arrow_up = "arrow_up";
    public const string Arrow_up_active = "arrow_up_active";
    public const string Arrow_up_howver = "arrow_up_howver";
    public const string Arrow_up_small = "arrow_up_small";
    public const string Arrow_up_small_active = "arrow_up_small_active";
    public const string Arrow_up_small_hover = "arrow_up_small_hover";
    public const string Blue_pass_large = "blue_pass_large";
    public const string Blue_pass_large_active = "blue_pass_large_active";
    public const string Blue_up_large = "blue_up_large";
    public const string Blue_up_large_active = "blue_up_large_active";
    public const string Cycle = "cycle";
    public const string Cycle_active = "cycle_active";
    public const string Deck = "deck";
    public const string Deck_active = "deck_active";
    public const string Discord_large = "discord_large";
    public const string Discord_large_active = "discord_large_active";
    public const string Double_back = "double_back";
    public const string Double_back_active = "double_back_active";
    public const string Double_back_negative = "double_back_negative";
    public const string Double_forward = "double_forward";
    public const string Double_forward_active = "double_forward_active";
    public const string Double_forward_negative = "double_forward_negative";
    public const string Five = "five";
    public const string Five_active = "five_active";
    public const string Four = "four";
    public const string Four_active = "four_active";
    public const string Green_check = "green_check";
    public const string Green_check_active = "green_check_active";
    public const string Green_check_large = "green_check_large";
    public const string Green_check_large_active = "green_check_large_active";
    public const string Green_down = "green_down";
    public const string Green_down_active = "green_down_active";
    public const string Grey_up_large = "grey_up_large";
    public const string Grey_up_large_active = "grey_up_large_active";
    public const string One = "one";
    public const string One_active = "one_active";
    public const string Questionmark = "questionmark";
    public const string Questionmark_active = "questionmark_active";
    public const string Red_cycle = "red_cycle";
    public const string Red_cycle_active = "red_cycle_active";
    public const string Red_dots_large = "red_dots_large";
    public const string Red_dots_large_active = "red_dots_large_active";
    public const string Red_exclaim = "red_exclaim";
    public const string Red_exclaim_active = "red_exclaim_active";
    public const string Red_out = "red_out";
    public const string Red_out_active = "red_out_active";
    public const string Sword = "sword";
    public const string Sword_active = "sword_active";
    public const string Three = "three";
    public const string Three_active = "three_active";
    public const string Toggle_atk_off = "toggle_atk_off";
    public const string Toggle_atk_on = "toggle_atk_on";
    public const string Toggle_def_off = "toggle_def_off";
    public const string Toggle_def_on = "toggle_def_on";
    public const string Toggle_dex_off = "toggle_dex_off";
    public const string Toggle_dex_on = "toggle_dex_on";
    public const string Toggle_innate_off = "toggle_innate_off";
    public const string Toggle_innate_on = "toggle_innate_on";
    public const string Toggle_level_off = "toggle_level_off";
    public const string Toggle_level_on = "toggle_level_on";
    public const string Two = "two";
    public const string Two_active = "two_active";

}

