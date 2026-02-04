using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class ScoreboardSpriteAtlas : SpriteAtlas
{
    public ScoreboardSpriteAtlas() : base()
    {
        Atlas = [new("Scoreboard")
        {
            Name = "Scoreboard",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:c4c5a9d923e7ed0a4c8b18146974e784:d5587f899f49df4a830de15e3c8c5a27:fcf33cac9c5ec04e50a1ac7aa8220b5c$",
            SpriteAtlas =
            {
                new("0", false, 1, 1, 8, 13, 8, 13, 0.5f, 0.5f),
                new("1", false, 11, 1, 8, 13, 8, 13, 0.5f, 0.5f),
                new("2", false, 21, 1, 8, 13, 8, 13, 0.5f, 0.5f),
                new("3", false, 31, 1, 8, 13, 8, 13, 0.5f, 0.5f),
                new("4", false, 41, 1, 8, 13, 8, 13, 0.5f, 0.5f),
                new("5", false, 51, 1, 8, 13, 8, 13, 0.5f, 0.5f),
                new("6", false, 61, 1, 8, 13, 8, 13, 0.5f, 0.5f),
                new("7", false, 71, 1, 8, 13, 8, 13, 0.5f, 0.5f),
                new("8", false, 81, 1, 8, 13, 8, 13, 0.5f, 0.5f),
                new("9", false, 91, 1, 8, 13, 8, 13, 0.5f, 0.5f),
                new("slash", false, 101, 1, 8, 13, 8, 13, 0.5f, 0.5f),

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
    public const string Slash = "slash";

}

