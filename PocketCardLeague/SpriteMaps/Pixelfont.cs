using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class PixelfontSpriteAtlas : SpriteAtlas
{
    public PixelfontSpriteAtlas() : base()
    {
        Atlas = [new("Pixelfont")
        {
            Name = "Pixelfont",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:c7ca00689bd613c7edaec800abdc5cd1:a85dab7bbf0d75a6f2f07c0f26337683:dc2d6dd7659f6daffb5f1e6ebbb3dd89$",
            SpriteAtlas =
            {
                new("0", false, 9, 51, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("1", false, 1, 52, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("2", false, 9, 62, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("3", false, 1, 63, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("4", false, 9, 73, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("5", false, 1, 74, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("6", false, 9, 84, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("7", false, 1, 85, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("8", false, 9, 95, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("9", false, 1, 96, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("_a", false, 9, 106, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("_b", false, 1, 107, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("_c", false, 9, 117, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("_d", false, 1, 118, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("_e", false, 9, 128, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("_f", false, 1, 129, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("_g", false, 1, 1, 6, 11, 7, 11, 0.5833333333333334f, 0.5f),
                new("_h", false, 9, 139, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("_i", false, 10, 377, 4, 9, 7, 11, 0.875f, 0.6111111111111112f),
                new("_j", false, 9, 1, 6, 11, 7, 11, 0.5833333333333334f, 0.5f),
                new("_k", false, 1, 140, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("_l", false, 10, 337, 5, 9, 7, 11, 0.7f, 0.6111111111111112f),
                new("_m", false, 1, 338, 7, 9, 7, 11, 0.5f, 0.6111111111111112f),
                new("_n", false, 9, 150, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("_o", false, 1, 151, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("_p", false, 1, 14, 6, 11, 7, 11, 0.5833333333333334f, 0.5f),
                new("_q", false, 9, 14, 6, 11, 7, 11, 0.5833333333333334f, 0.5f),
                new("_r", false, 9, 161, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("_s", false, 1, 162, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("_t", false, 9, 172, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("_u", false, 1, 173, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("_v", false, 9, 183, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("_w", false, 1, 349, 7, 9, 7, 11, 0.5f, 0.6111111111111112f),
                new("_x", false, 1, 184, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("_y", false, 1, 27, 6, 11, 7, 11, 0.5833333333333334f, 0.5f),
                new("_z", false, 9, 194, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("a", false, 1, 195, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("asterisk", false, 1, 412, 6, 6, 7, 11, 0.5833333333333334f, 0.9166666666666666f),
                new("b", false, 9, 205, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("backward_slash", false, 1, 206, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("c", false, 9, 216, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("colon", false, 10, 399, 4, 8, 7, 11, 0.875f, 0.6875f),
                new("comma", true, 9, 412, 6, 4, 7, 11, 0.875f, 0.25f),
                new("d", false, 1, 217, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("dot", true, 10, 365, 5, 4, 7, 11, 0.875f, 0.3f),
                new("double_quote", true, 10, 357, 5, 6, 7, 11, 0.5833333333333334f, 1.1f),
                new("e", false, 9, 227, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("equals_sign", false, 1, 403, 7, 7, 7, 11, 0.5f, 0.7857142857142857f),
                new("exclaim", false, 10, 388, 4, 9, 7, 11, 0.875f, 0.6111111111111112f),
                new("f", false, 1, 228, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("forward_slash", false, 9, 238, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("g", false, 1, 239, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("h", false, 9, 249, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("i", false, 1, 250, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("j", false, 9, 260, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("k", false, 1, 261, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("l", false, 9, 271, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("m", false, 1, 360, 7, 9, 7, 11, 0.5f, 0.6111111111111112f),
                new("minus", true, 10, 348, 5, 7, 7, 11, 0.5f, 0.9f),
                new("n", false, 1, 272, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("o", false, 9, 51, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("p", false, 9, 282, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("percentage", false, 1, 371, 7, 9, 7, 11, 0.5f, 0.6111111111111112f),
                new("plus", false, 1, 393, 7, 8, 7, 11, 0.5f, 0.6875f),
                new("q", false, 9, 27, 6, 10, 7, 11, 0.5833333333333334f, 0.55f),
                new("question", false, 1, 283, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("r", false, 9, 293, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("round_bracket_close", false, 9, 39, 6, 10, 7, 11, 0.5833333333333334f, 0.55f),
                new("round_bracket_open", false, 1, 40, 6, 10, 7, 11, 0.5833333333333334f, 0.55f),
                new("s", false, 1, 294, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("single_quote", true, 10, 371, 5, 4, 7, 11, 0.875f, 1.1f),
                new("t", false, 9, 304, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("u", false, 1, 305, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("v", false, 9, 315, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("w", false, 1, 382, 7, 9, 7, 11, 0.5f, 0.6111111111111112f),
                new("x", false, 1, 316, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("y", false, 9, 326, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),
                new("z", false, 1, 327, 6, 9, 7, 11, 0.5833333333333334f, 0.6111111111111112f),

            }
        }];
    }

    public const string _0 = "0";
    public const string _1 = "1";
    public const string _2 = "2";
    public const string _3 = "3";
    public const string _4 = "4";
    public const string _5 = "5";
    public const string _6 = "6";
    public const string _7 = "7";
    public const string _8 = "8";
    public const string _9 = "9";
    public const string _a = "_a";
    public const string _b = "_b";
    public const string _c = "_c";
    public const string _d = "_d";
    public const string _e = "_e";
    public const string _f = "_f";
    public const string _g = "_g";
    public const string _h = "_h";
    public const string _i = "_i";
    public const string _j = "_j";
    public const string _k = "_k";
    public const string _l = "_l";
    public const string _m = "_m";
    public const string _n = "_n";
    public const string _o = "_o";
    public const string _p = "_p";
    public const string _q = "_q";
    public const string _r = "_r";
    public const string _s = "_s";
    public const string _t = "_t";
    public const string _u = "_u";
    public const string _v = "_v";
    public const string _w = "_w";
    public const string _x = "_x";
    public const string _y = "_y";
    public const string _z = "_z";
    public const string A = "a";
    public const string Asterisk = "asterisk";
    public const string B = "b";
    public const string Backward_slash = "backward_slash";
    public const string C = "c";
    public const string Colon = "colon";
    public const string Comma = "comma";
    public const string D = "d";
    public const string Dot = "dot";
    public const string Double_quote = "double_quote";
    public const string E = "e";
    public const string Equals_sign = "equals_sign";
    public const string Exclaim = "exclaim";
    public const string F = "f";
    public const string Forward_slash = "forward_slash";
    public const string G = "g";
    public const string H = "h";
    public const string I = "i";
    public const string J = "j";
    public const string K = "k";
    public const string L = "l";
    public const string M = "m";
    public const string Minus = "minus";
    public const string N = "n";
    public const string O = "o";
    public const string P = "p";
    public const string Percentage = "percentage";
    public const string Plus = "plus";
    public const string Q = "q";
    public const string Question = "question";
    public const string R = "r";
    public const string Round_bracket_close = "round_bracket_close";
    public const string Round_bracket_open = "round_bracket_open";
    public const string S = "s";
    public const string Single_quote = "single_quote";
    public const string T = "t";
    public const string U = "u";
    public const string V = "v";
    public const string W = "w";
    public const string X = "x";
    public const string Y = "y";
    public const string Z = "z";

}

