using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class PixelCursorsSpriteAtlas : SpriteAtlas
{
    public PixelCursorsSpriteAtlas() : base()
    {
        Atlas = [new("PixelCursors")
        {
            Name = "PixelCursors",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:437928496ac87adacd4a12f26e5bb195:f53cedd9eb94c398cd4cd03b67d7fa40:3df0380123d43ceeb53831ca7924fe2f$",
            SpriteAtlas =
            {
                new("arrow", false, 20, 0, 9, 10, 10, 10, 0.5555555555555556f, 0.5f),
                new("grabber", false, 0, 0, 10, 10, 10, 10, 0.5f, 0.5f),
                new("pointer", false, 10, 0, 10, 10, 10, 10, 0.5f, 0.5f),

            }
        }];
    }

    public static TextureResult Arrow => ContentHelper.GetTextureResult<PixelCursorsSpriteAtlas>("arrow")!;
    public static TextureResult Grabber => ContentHelper.GetTextureResult<PixelCursorsSpriteAtlas>("grabber")!;
    public static TextureResult Pointer => ContentHelper.GetTextureResult<PixelCursorsSpriteAtlas>("pointer")!;

}