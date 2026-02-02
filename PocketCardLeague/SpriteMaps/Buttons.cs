using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class ButtonsSpriteAtlas : SpriteAtlas
{
    public ButtonsSpriteAtlas() : base()
    {
        Atlas = [new("Buttons")
        {
            Name = "Buttons",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:bde47748d35b10bbb86ae8b12d255cb1:0f0d23db5230666f73b410b7d1e30e65:a68c7291fd959d506666c9fd9a7121a4$",
            SpriteAtlas =
            {
                new("arrow_down", false, 0, 621, 14, 9, 14, 14, 0.5f, 0.7777777777777778f),
                new("arrow_down_active", false, 14, 621, 14, 9, 14, 14, 0.5f, 0.7777777777777778f),
                new("arrow_down_hover", false, 0, 630, 14, 9, 14, 14, 0.5f, 0.7777777777777778f),
                new("arrow_down_small", false, 0, 693, 9, 6, 14, 14, 0.7777777777777778f, 1.1666666666666667f),
                new("arrow_down_small_active", false, 9, 693, 9, 6, 14, 14, 0.7777777777777778f, 1.1666666666666667f),
                new("arrow_down_small_hover", false, 18, 675, 9, 6, 14, 14, 0.7777777777777778f, 1.1666666666666667f),
                new("arrow_left", true, 0, 648, 14, 9, 14, 14, 0.7777777777777778f, 0.5f),
                new("arrow_left_active", true, 14, 648, 14, 9, 14, 14, 0.7777777777777778f, 0.5f),
                new("arrow_left_hover", true, 0, 657, 14, 9, 14, 14, 0.7777777777777778f, 0.5f),
                new("arrow_left_small", false, 0, 675, 6, 9, 14, 14, 1.1666666666666667f, 0.7777777777777778f),
                new("arrow_left_small_active", false, 0, 684, 6, 9, 14, 14, 1.1666666666666667f, 0.7777777777777778f),
                new("arrow_left_small_hover", false, 6, 675, 6, 9, 14, 14, 1.1666666666666667f, 0.7777777777777778f),
                new("arrow_right", true, 14, 657, 14, 9, 14, 14, 0.7777777777777778f, 0.5f),
                new("arrow_right_active", true, 0, 666, 14, 9, 14, 14, 0.7777777777777778f, 0.5f),
                new("arrow_right_hover", true, 14, 666, 14, 9, 14, 14, 0.7777777777777778f, 0.5f),
                new("arrow_right_small", false, 6, 684, 6, 9, 14, 14, 1.1666666666666667f, 0.7777777777777778f),
                new("arrow_right_small_active", false, 12, 675, 6, 9, 14, 14, 1.1666666666666667f, 0.7777777777777778f),
                new("arrow_right_small_hover", false, 12, 684, 6, 9, 14, 14, 1.1666666666666667f, 0.7777777777777778f),
                new("arrow_up", false, 14, 630, 14, 9, 14, 14, 0.5f, 0.7777777777777778f),
                new("arrow_up_active", false, 0, 639, 14, 9, 14, 14, 0.5f, 0.7777777777777778f),
                new("arrow_up_howver", false, 14, 639, 14, 9, 14, 14, 0.5f, 0.7777777777777778f),
                new("arrow_up_small", false, 18, 681, 9, 6, 14, 14, 0.7777777777777778f, 1.1666666666666667f),
                new("arrow_up_small_active", true, 18, 687, 6, 9, 14, 14, 0.7777777777777778f, 1.1666666666666667f),
                new("arrow_up_small_hover", true, 24, 687, 6, 9, 14, 14, 0.7777777777777778f, 1.1666666666666667f),
                new("blue_pass_large", false, 0, 0, 31, 24, 31, 24, 0.5f, 0.5f),
                new("blue_pass_large_active", false, 0, 144, 31, 23, 31, 24, 0.5f, 0.4782608695652174f),
                new("blue_up_large", false, 0, 24, 31, 24, 31, 24, 0.5f, 0.5f),
                new("blue_up_large_active", false, 0, 167, 31, 23, 31, 24, 0.5f, 0.4782608695652174f),
                new("cycle", false, 0, 282, 16, 16, 16, 16, 0.5f, 0.5f),
                new("cycle_active", false, 0, 426, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("deck", false, 16, 282, 16, 16, 16, 16, 0.5f, 0.5f),
                new("deck_active", false, 16, 426, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("discord_large", false, 0, 48, 31, 24, 31, 24, 0.5f, 0.5f),
                new("discord_large_active", false, 0, 190, 31, 23, 31, 24, 0.5f, 0.4782608695652174f),
                new("double_back", false, 0, 298, 16, 16, 16, 16, 0.5f, 0.5f),
                new("double_back_active", false, 0, 441, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("double_back_negative", false, 16, 298, 16, 16, 16, 16, 0.5f, 0.5f),
                new("double_forward", false, 0, 314, 16, 16, 16, 16, 0.5f, 0.5f),
                new("double_forward_active", false, 16, 441, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("double_forward_negative", false, 16, 314, 16, 16, 16, 16, 0.5f, 0.5f),
                new("five", false, 0, 330, 16, 16, 16, 16, 0.5f, 0.5f),
                new("five_active", false, 0, 456, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("four", false, 16, 330, 16, 16, 16, 16, 0.5f, 0.5f),
                new("four_active", false, 16, 456, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("green_check", false, 0, 346, 16, 16, 16, 16, 0.5f, 0.5f),
                new("green_check_active", false, 0, 471, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("green_check_large", false, 0, 72, 31, 24, 31, 24, 0.5f, 0.5f),
                new("green_check_large_active", false, 0, 213, 31, 23, 31, 24, 0.5f, 0.4782608695652174f),
                new("green_down", false, 16, 346, 16, 16, 16, 16, 0.5f, 0.5f),
                new("green_down_active", false, 16, 471, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("grey_up_large", false, 0, 96, 31, 24, 31, 24, 0.5f, 0.5f),
                new("grey_up_large_active", false, 0, 236, 31, 23, 31, 24, 0.5f, 0.4782608695652174f),
                new("one", false, 0, 362, 16, 16, 16, 16, 0.5f, 0.5f),
                new("one_active", false, 0, 486, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("questionmark", false, 16, 362, 16, 16, 16, 16, 0.5f, 0.5f),
                new("questionmark_active", false, 16, 486, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("red_cycle", false, 0, 378, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_cycle_active", false, 0, 501, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("red_dots_large", false, 0, 120, 31, 24, 31, 24, 0.5f, 0.5f),
                new("red_dots_large_active", false, 0, 259, 31, 23, 31, 24, 0.5f, 0.4782608695652174f),
                new("red_exclaim", false, 16, 378, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_exclaim_active", false, 16, 501, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("red_out", false, 0, 394, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_out_active", false, 0, 516, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("sword", false, 16, 394, 16, 16, 16, 16, 0.5f, 0.5f),
                new("sword_active", false, 16, 516, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("three", false, 0, 410, 16, 16, 16, 16, 0.5f, 0.5f),
                new("three_active", false, 0, 531, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("toggle_atk_off", true, 0, 546, 16, 15, 15, 16, 0.5f, 0.5f),
                new("toggle_atk_on", false, 16, 576, 15, 15, 15, 16, 0.5f, 0.4666666666666667f),
                new("toggle_def_off", true, 16, 546, 16, 15, 15, 16, 0.5f, 0.5f),
                new("toggle_def_on", false, 0, 591, 15, 15, 15, 16, 0.5f, 0.4666666666666667f),
                new("toggle_dex_off", true, 0, 561, 16, 15, 15, 16, 0.5f, 0.5f),
                new("toggle_dex_on", false, 15, 591, 15, 15, 15, 16, 0.5f, 0.4666666666666667f),
                new("toggle_innate_off", true, 16, 561, 16, 15, 15, 16, 0.5f, 0.5f),
                new("toggle_innate_on", false, 0, 606, 15, 15, 15, 16, 0.5f, 0.4666666666666667f),
                new("toggle_level_off", true, 0, 576, 16, 15, 15, 16, 0.5f, 0.5f),
                new("toggle_level_on", false, 15, 606, 15, 15, 15, 16, 0.5f, 0.4666666666666667f),
                new("two", false, 16, 410, 16, 16, 16, 16, 0.5f, 0.5f),
                new("two_active", false, 16, 531, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),

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

