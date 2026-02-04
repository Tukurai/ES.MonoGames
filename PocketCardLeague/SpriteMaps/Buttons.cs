using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class ButtonsSpriteAtlas : SpriteAtlas
{
    public ButtonsSpriteAtlas() : base()
    {
        Atlas = [new("Buttons")
        {
            Name = "Buttons",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:46fec0ce4b568d862c88900b6b138fbe:0f0d23db5230666f73b410b7d1e30e65:a68c7291fd959d506666c9fd9a7121a4$",
            SpriteAtlas =
            {
                new("arrow_down", false, 0, 12, 14, 9, 14, 14, 0.5f, 0.7777777777777778f),
                new("arrow_down_active", false, 14, 12, 14, 9, 14, 14, 0.5f, 0.7777777777777778f),
                new("arrow_down_hover", false, 0, 21, 14, 9, 14, 14, 0.5f, 0.7777777777777778f),
                new("arrow_down_small", false, 0, 0, 9, 6, 14, 14, 0.7777777777777778f, 1.1666666666666667f),
                new("arrow_down_small_active", false, 9, 0, 9, 6, 14, 14, 0.7777777777777778f, 1.1666666666666667f),
                new("arrow_down_small_hover", false, 18, 0, 9, 6, 14, 14, 0.7777777777777778f, 1.1666666666666667f),
                new("arrow_left", false, 0, 48, 9, 14, 14, 14, 0.7777777777777778f, 0.5f),
                new("arrow_left_active", false, 9, 48, 9, 14, 14, 14, 0.7777777777777778f, 0.5f),
                new("arrow_left_hover", false, 18, 48, 9, 14, 14, 14, 0.7777777777777778f, 0.5f),
                new("arrow_left_small", false, 14, 21, 6, 9, 14, 14, 1.1666666666666667f, 0.7777777777777778f),
                new("arrow_left_small_active", false, 20, 21, 6, 9, 14, 14, 1.1666666666666667f, 0.7777777777777778f),
                new("arrow_left_small_hover", false, 26, 21, 6, 9, 14, 14, 1.1666666666666667f, 0.7777777777777778f),
                new("arrow_right", false, 0, 62, 9, 14, 14, 14, 0.7777777777777778f, 0.5f),
                new("arrow_right_active", false, 9, 62, 9, 14, 14, 14, 0.7777777777777778f, 0.5f),
                new("arrow_right_hover", false, 18, 62, 9, 14, 14, 14, 0.7777777777777778f, 0.5f),
                new("arrow_right_small", false, 0, 30, 6, 9, 14, 14, 1.1666666666666667f, 0.7777777777777778f),
                new("arrow_right_small_active", false, 6, 30, 6, 9, 14, 14, 1.1666666666666667f, 0.7777777777777778f),
                new("arrow_right_small_hover", false, 12, 30, 6, 9, 14, 14, 1.1666666666666667f, 0.7777777777777778f),
                new("arrow_up", false, 18, 30, 14, 9, 14, 14, 0.5f, 0.7777777777777778f),
                new("arrow_up_active", false, 0, 39, 14, 9, 14, 14, 0.5f, 0.7777777777777778f),
                new("arrow_up_howver", false, 14, 39, 14, 9, 14, 14, 0.5f, 0.7777777777777778f),
                new("arrow_up_small", false, 0, 6, 9, 6, 14, 14, 0.7777777777777778f, 1.1666666666666667f),
                new("arrow_up_small_active", false, 9, 6, 9, 6, 14, 14, 0.7777777777777778f, 1.1666666666666667f),
                new("arrow_up_small_hover", false, 18, 6, 9, 6, 14, 14, 0.7777777777777778f, 1.1666666666666667f),
                new("blue_pass_large", false, 0, 556, 31, 24, 31, 24, 0.5f, 0.5f),
                new("blue_pass_large_active", false, 0, 418, 31, 23, 31, 24, 0.5f, 0.4782608695652174f),
                new("blue_up_large", false, 0, 580, 31, 24, 31, 24, 0.5f, 0.5f),
                new("blue_up_large_active", false, 0, 441, 31, 23, 31, 24, 0.5f, 0.4782608695652174f),
                new("cycle", false, 16, 226, 16, 16, 16, 16, 0.5f, 0.5f),
                new("cycle_active", false, 0, 76, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("deck", false, 0, 241, 16, 16, 16, 16, 0.5f, 0.5f),
                new("deck_active", false, 16, 76, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("discord_large", false, 0, 604, 31, 24, 31, 24, 0.5f, 0.5f),
                new("discord_large_active", false, 0, 464, 31, 23, 31, 24, 0.5f, 0.4782608695652174f),
                new("double_back", false, 16, 242, 16, 16, 16, 16, 0.5f, 0.5f),
                new("double_back_active", false, 0, 91, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("double_back_negative", false, 0, 257, 16, 16, 16, 16, 0.5f, 0.5f),
                new("double_forward", false, 16, 258, 16, 16, 16, 16, 0.5f, 0.5f),
                new("double_forward_active", false, 16, 91, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("double_forward_negative", false, 0, 273, 16, 16, 16, 16, 0.5f, 0.5f),
                new("five", false, 16, 274, 16, 16, 16, 16, 0.5f, 0.5f),
                new("five_active", false, 0, 106, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("four", false, 0, 289, 16, 16, 16, 16, 0.5f, 0.5f),
                new("four_active", false, 16, 106, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("green_check", false, 16, 290, 16, 16, 16, 16, 0.5f, 0.5f),
                new("green_check_active", false, 0, 121, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("green_check_large", false, 0, 628, 31, 24, 31, 24, 0.5f, 0.5f),
                new("green_check_large_active", false, 0, 487, 31, 23, 31, 24, 0.5f, 0.4782608695652174f),
                new("green_down", false, 0, 305, 16, 16, 16, 16, 0.5f, 0.5f),
                new("green_down_active", false, 16, 121, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("grey_up_large", false, 0, 652, 31, 24, 31, 24, 0.5f, 0.5f),
                new("grey_up_large_active", false, 0, 510, 31, 23, 31, 24, 0.5f, 0.4782608695652174f),
                new("one", false, 16, 306, 16, 16, 16, 16, 0.5f, 0.5f),
                new("one_active", false, 0, 136, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("questionmark", false, 0, 321, 16, 16, 16, 16, 0.5f, 0.5f),
                new("questionmark_active", false, 16, 136, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("red_cycle", false, 16, 322, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_cycle_active", false, 0, 151, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("red_dots_large", false, 0, 676, 31, 24, 31, 24, 0.5f, 0.5f),
                new("red_dots_large_active", false, 0, 533, 31, 23, 31, 24, 0.5f, 0.4782608695652174f),
                new("red_exclaim", false, 0, 337, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_exclaim_active", false, 16, 151, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("red_out", false, 16, 338, 16, 16, 16, 16, 0.5f, 0.5f),
                new("red_out_active", false, 0, 166, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("sword", false, 0, 353, 16, 16, 16, 16, 0.5f, 0.5f),
                new("sword_active", false, 16, 166, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("three", false, 16, 354, 16, 16, 16, 16, 0.5f, 0.5f),
                new("three_active", false, 0, 181, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),
                new("toggle_atk_off", false, 0, 369, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_atk_on", false, 16, 181, 15, 15, 15, 16, 0.5f, 0.4666666666666667f),
                new("toggle_def_off", false, 15, 370, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_def_on", false, 0, 196, 15, 15, 15, 16, 0.5f, 0.4666666666666667f),
                new("toggle_dex_off", false, 0, 385, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_dex_on", false, 15, 196, 15, 15, 15, 16, 0.5f, 0.4666666666666667f),
                new("toggle_innate_off", false, 15, 386, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_innate_on", false, 0, 211, 15, 15, 15, 16, 0.5f, 0.4666666666666667f),
                new("toggle_level_off", false, 0, 401, 15, 16, 15, 16, 0.5f, 0.5f),
                new("toggle_level_on", false, 15, 211, 15, 15, 15, 16, 0.5f, 0.4666666666666667f),
                new("two", false, 15, 402, 16, 16, 16, 16, 0.5f, 0.5f),
                new("two_active", false, 0, 226, 16, 15, 16, 16, 0.5f, 0.4666666666666667f),

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

