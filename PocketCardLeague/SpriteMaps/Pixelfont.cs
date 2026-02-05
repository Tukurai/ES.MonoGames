using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class PixelfontSpriteAtlas : SpriteAtlas
{
    public PixelfontSpriteAtlas() : base()
    {
        Atlas = [new("Pixelfont")
        {
            Name = "Pixelfont",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:a97a1613e3fe1a15b41c8c1ed4ca6ae0:a85dab7bbf0d75a6f2f07c0f26337683:dc2d6dd7659f6daffb5f1e6ebbb3dd89$",
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

    public static TextureResult _0 => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("0")!;
    public static TextureResult _1 => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("1")!;
    public static TextureResult _2 => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("2")!;
    public static TextureResult _3 => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("3")!;
    public static TextureResult _4 => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("4")!;
    public static TextureResult _5 => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("5")!;
    public static TextureResult _6 => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("6")!;
    public static TextureResult _7 => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("7")!;
    public static TextureResult _8 => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("8")!;
    public static TextureResult _9 => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("9")!;
    public static TextureResult _a => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_a")!;
    public static TextureResult _b => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_b")!;
    public static TextureResult _c => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_c")!;
    public static TextureResult _d => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_d")!;
    public static TextureResult _e => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_e")!;
    public static TextureResult _f => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_f")!;
    public static TextureResult _g => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_g")!;
    public static TextureResult _h => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_h")!;
    public static TextureResult _i => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_i")!;
    public static TextureResult _j => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_j")!;
    public static TextureResult _k => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_k")!;
    public static TextureResult _l => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_l")!;
    public static TextureResult _m => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_m")!;
    public static TextureResult _n => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_n")!;
    public static TextureResult _o => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_o")!;
    public static TextureResult _p => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_p")!;
    public static TextureResult _q => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_q")!;
    public static TextureResult _r => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_r")!;
    public static TextureResult _s => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_s")!;
    public static TextureResult _t => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_t")!;
    public static TextureResult _u => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_u")!;
    public static TextureResult _v => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_v")!;
    public static TextureResult _w => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_w")!;
    public static TextureResult _x => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_x")!;
    public static TextureResult _y => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_y")!;
    public static TextureResult _z => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("_z")!;
    public static TextureResult A => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("a")!;
    public static TextureResult Asterisk => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("asterisk")!;
    public static TextureResult B => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("b")!;
    public static TextureResult Backward_slash => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("backward_slash")!;
    public static TextureResult C => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("c")!;
    public static TextureResult Colon => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("colon")!;
    public static TextureResult Comma => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("comma")!;
    public static TextureResult D => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("d")!;
    public static TextureResult Dot => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("dot")!;
    public static TextureResult Double_quote => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("double_quote")!;
    public static TextureResult E => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("e")!;
    public static TextureResult Equals_sign => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("equals_sign")!;
    public static TextureResult Exclaim => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("exclaim")!;
    public static TextureResult F => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("f")!;
    public static TextureResult Forward_slash => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("forward_slash")!;
    public static TextureResult G => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("g")!;
    public static TextureResult H => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("h")!;
    public static TextureResult I => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("i")!;
    public static TextureResult J => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("j")!;
    public static TextureResult K => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("k")!;
    public static TextureResult L => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("l")!;
    public static TextureResult M => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("m")!;
    public static TextureResult Minus => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("minus")!;
    public static TextureResult N => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("n")!;
    public static TextureResult O => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("o")!;
    public static TextureResult P => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("p")!;
    public static TextureResult Percentage => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("percentage")!;
    public static TextureResult Plus => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("plus")!;
    public static TextureResult Q => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("q")!;
    public static TextureResult Question => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("question")!;
    public static TextureResult R => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("r")!;
    public static TextureResult Round_bracket_close => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("round_bracket_close")!;
    public static TextureResult Round_bracket_open => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("round_bracket_open")!;
    public static TextureResult S => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("s")!;
    public static TextureResult Single_quote => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("single_quote")!;
    public static TextureResult T => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("t")!;
    public static TextureResult U => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("u")!;
    public static TextureResult V => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("v")!;
    public static TextureResult W => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("w")!;
    public static TextureResult X => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("x")!;
    public static TextureResult Y => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("y")!;
    public static TextureResult Z => ContentHelper.GetTextureResult<PixelfontSpriteAtlas>("z")!;

}