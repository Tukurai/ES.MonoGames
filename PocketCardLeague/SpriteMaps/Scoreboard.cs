using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class ScoreboardSpriteAtlas : SpriteAtlas
{
    public ScoreboardSpriteAtlas() : base()
    {
        Atlas = [new("Scoreboard")
        {
            Name = "Scoreboard",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:8f6d35996ff01e9db69866a9eb415ba9:d5587f899f49df4a830de15e3c8c5a27:fcf33cac9c5ec04e50a1ac7aa8220b5c$",
            SpriteAtlas =
            {
                new("0", false, 0, 0, 8, 13, 8, 13, 0.5f, 0.5f),
                new("1", false, 0, 13, 8, 13, 8, 13, 0.5f, 0.5f),
                new("2", false, 0, 26, 8, 13, 8, 13, 0.5f, 0.5f),
                new("3", false, 0, 39, 8, 13, 8, 13, 0.5f, 0.5f),
                new("4", false, 0, 52, 8, 13, 8, 13, 0.5f, 0.5f),
                new("5", false, 0, 65, 8, 13, 8, 13, 0.5f, 0.5f),
                new("6", false, 0, 78, 8, 13, 8, 13, 0.5f, 0.5f),
                new("7", false, 0, 91, 8, 13, 8, 13, 0.5f, 0.5f),
                new("8", false, 0, 104, 8, 13, 8, 13, 0.5f, 0.5f),
                new("9", false, 0, 117, 8, 13, 8, 13, 0.5f, 0.5f),
                new("slash", false, 0, 130, 8, 13, 8, 13, 0.5f, 0.5f),

            }
        }];
    }

    public static TextureResult _0 => ContentHelper.GetTextureResult<ScoreboardSpriteAtlas>("0")!;
    public static TextureResult _1 => ContentHelper.GetTextureResult<ScoreboardSpriteAtlas>("1")!;
    public static TextureResult _2 => ContentHelper.GetTextureResult<ScoreboardSpriteAtlas>("2")!;
    public static TextureResult _3 => ContentHelper.GetTextureResult<ScoreboardSpriteAtlas>("3")!;
    public static TextureResult _4 => ContentHelper.GetTextureResult<ScoreboardSpriteAtlas>("4")!;
    public static TextureResult _5 => ContentHelper.GetTextureResult<ScoreboardSpriteAtlas>("5")!;
    public static TextureResult _6 => ContentHelper.GetTextureResult<ScoreboardSpriteAtlas>("6")!;
    public static TextureResult _7 => ContentHelper.GetTextureResult<ScoreboardSpriteAtlas>("7")!;
    public static TextureResult _8 => ContentHelper.GetTextureResult<ScoreboardSpriteAtlas>("8")!;
    public static TextureResult _9 => ContentHelper.GetTextureResult<ScoreboardSpriteAtlas>("9")!;
    public static TextureResult Slash => ContentHelper.GetTextureResult<ScoreboardSpriteAtlas>("slash")!;

}