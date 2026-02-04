using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class PixelfontSpriteAtlas : SpriteAtlas
{
    public PixelfontSpriteAtlas() : base()
    {
        Atlas = [new("Pixelfont")
        {
            Name = "Pixelfont",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:7727fe5e841ce828cdb8e582cb85aed7:a85dab7bbf0d75a6f2f07c0f26337683:dc2d6dd7659f6daffb5f1e6ebbb3dd89$",
            SpriteAtlas =
            {
                new("0", false, 0, 0, 5, 11, 7, 11, 0.7f, 0.5f),
                new("1", false, 5, 0, 5, 11, 7, 11, 0.7f, 0.5f),
                new("2", false, 10, 0, 5, 11, 7, 11, 0.7f, 0.5f),
                new("3", false, 15, 0, 5, 11, 7, 11, 0.7f, 0.5f),
                new("4", false, 20, 0, 5, 11, 7, 11, 0.7f, 0.5f),
                new("5", false, 25, 0, 5, 11, 7, 11, 0.7f, 0.5f),
                new("6", false, 0, 11, 5, 11, 7, 11, 0.7f, 0.5f),
                new("7", false, 5, 11, 5, 11, 7, 11, 0.7f, 0.5f),
                new("8", false, 10, 11, 5, 11, 7, 11, 0.7f, 0.5f),
                new("9", false, 15, 11, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_a", false, 20, 11, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_b", false, 25, 11, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_c", false, 0, 22, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_d", false, 5, 22, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_e", false, 10, 22, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_f", false, 15, 22, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_g", false, 20, 22, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_h", false, 25, 22, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_i", false, 0, 33, 3, 11, 7, 11, 1.1666666666666667f, 0.5f),
                new("_j", false, 3, 33, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_k", false, 8, 33, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_l", false, 13, 33, 4, 11, 7, 11, 0.875f, 0.5f),
                new("_m", false, 17, 33, 7, 11, 7, 11, 0.5f, 0.5f),
                new("_n", false, 24, 33, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_o", false, 0, 44, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_p", false, 5, 44, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_q", false, 10, 44, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_r", false, 15, 44, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_s", false, 20, 44, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_t", false, 25, 44, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_u", false, 0, 55, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_v", false, 5, 55, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_w", false, 10, 55, 7, 11, 7, 11, 0.5f, 0.5f),
                new("_x", false, 17, 55, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_y", false, 22, 55, 5, 11, 7, 11, 0.7f, 0.5f),
                new("_z", false, 27, 55, 5, 11, 7, 11, 0.7f, 0.5f),
                new("a", false, 0, 66, 5, 11, 7, 11, 0.7f, 0.5f),
                new("asterisk", false, 5, 66, 5, 11, 7, 11, 0.7f, 0.5f),
                new("b", false, 10, 66, 5, 11, 7, 11, 0.7f, 0.5f),
                new("backward_slash", false, 15, 66, 5, 11, 7, 11, 0.7f, 0.5f),
                new("c", false, 20, 66, 5, 11, 7, 11, 0.7f, 0.5f),
                new("colon", false, 25, 66, 3, 11, 7, 11, 1.1666666666666667f, 0.5f),
                new("comma", false, 28, 66, 3, 11, 7, 11, 1.1666666666666667f, 0.5f),
                new("d", false, 0, 77, 5, 11, 7, 11, 0.7f, 0.5f),
                new("dot", false, 5, 77, 3, 11, 7, 11, 1.1666666666666667f, 0.5f),
                new("double_quote", false, 8, 77, 5, 11, 7, 11, 0.7f, 0.5f),
                new("e", false, 13, 77, 5, 11, 7, 11, 0.7f, 0.5f),
                new("equals_sign", false, 18, 77, 7, 11, 7, 11, 0.5f, 0.5f),
                new("exclaim", false, 25, 77, 3, 11, 7, 11, 1.1666666666666667f, 0.5f),
                new("f", false, 0, 88, 5, 11, 7, 11, 0.7f, 0.5f),
                new("forward_slash", false, 5, 88, 5, 11, 7, 11, 0.7f, 0.5f),
                new("g", false, 10, 88, 5, 11, 7, 11, 0.7f, 0.5f),
                new("h", false, 15, 88, 5, 11, 7, 11, 0.7f, 0.5f),
                new("i", false, 20, 88, 5, 11, 7, 11, 0.7f, 0.5f),
                new("j", false, 25, 88, 5, 11, 7, 11, 0.7f, 0.5f),
                new("k", false, 0, 99, 5, 11, 7, 11, 0.7f, 0.5f),
                new("l", false, 5, 99, 5, 11, 7, 11, 0.7f, 0.5f),
                new("m", false, 10, 99, 7, 11, 7, 11, 0.5f, 0.5f),
                new("minus", false, 17, 99, 7, 11, 7, 11, 0.5f, 0.5f),
                new("n", false, 24, 99, 5, 11, 7, 11, 0.7f, 0.5f),
                new("o", false, 0, 0, 5, 11, 7, 11, 0.7f, 0.5f),
                new("p", false, 0, 110, 5, 11, 7, 11, 0.7f, 0.5f),
                new("percentage", false, 5, 110, 7, 11, 7, 11, 0.5f, 0.5f),
                new("plus", false, 12, 110, 7, 11, 7, 11, 0.5f, 0.5f),
                new("q", false, 19, 110, 5, 11, 7, 11, 0.7f, 0.5f),
                new("question", false, 24, 110, 5, 11, 7, 11, 0.7f, 0.5f),
                new("r", false, 0, 121, 5, 11, 7, 11, 0.7f, 0.5f),
                new("round_bracket_close", false, 5, 121, 5, 11, 7, 11, 0.7f, 0.5f),
                new("round_bracket_open", false, 10, 121, 5, 11, 7, 11, 0.7f, 0.5f),
                new("s", false, 15, 121, 5, 11, 7, 11, 0.7f, 0.5f),
                new("single_quote", false, 20, 121, 3, 11, 7, 11, 1.1666666666666667f, 0.5f),
                new("t", false, 23, 121, 5, 11, 7, 11, 0.7f, 0.5f),
                new("u", false, 0, 132, 5, 11, 7, 11, 0.7f, 0.5f),
                new("v", false, 5, 132, 5, 11, 7, 11, 0.7f, 0.5f),
                new("w", false, 10, 132, 7, 11, 7, 11, 0.5f, 0.5f),
                new("x", false, 17, 132, 5, 11, 7, 11, 0.7f, 0.5f),
                new("y", false, 22, 132, 5, 11, 7, 11, 0.7f, 0.5f),
                new("z", false, 27, 132, 5, 11, 7, 11, 0.7f, 0.5f),

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

