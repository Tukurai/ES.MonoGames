using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class PixelCursorsSpriteAtlas : SpriteAtlas
{
    public PixelCursorsSpriteAtlas() : base()
    {
        Atlas = [new("PixelCursors")
        {
            Name = "PixelCursors",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:7777f33dd44ee8901205ec832fd6b4f7:f53cedd9eb94c398cd4cd03b67d7fa40:3df0380123d43ceeb53831ca7924fe2f$",
            SpriteAtlas =
            {
                new("arrow", false, 25, 1, 9, 10, 10, 10, 0.5555555555555556f, 0.5f),
                new("grabber", false, 1, 1, 10, 10, 10, 10, 0.5f, 0.5f),
                new("pointer", false, 13, 1, 10, 10, 10, 10, 0.5f, 0.5f),

            }
        }];
    }

    public const string Arrow = "arrow";
    public const string Grabber = "grabber";
    public const string Pointer = "pointer";

}

