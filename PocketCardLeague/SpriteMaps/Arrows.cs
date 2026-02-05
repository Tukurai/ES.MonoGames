using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class ArrowsSpriteAtlas : SpriteAtlas
{
    public ArrowsSpriteAtlas() : base()
    {
        Atlas = [new("Arrows")
        {
            Name = "Arrows",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:835a7da2322dc6f3ab879ba5d6e93c00:9852e130f998e5475cd977621a766be9:8c78d36d3d74cf1ad05bd2a7267f9a3e$",
            SpriteAtlas =
            {
                new("arrow_down", false, 48, 0, 14, 8, 14, 14, 0.5f, 0.875f),
                new("arrow_down_active", false, 48, 8, 14, 8, 14, 14, 0.5f, 0.875f),
                new("arrow_down_hover", false, 62, 0, 14, 8, 14, 14, 0.5f, 0.875f),
                new("arrow_down_small", false, 105, 0, 8, 5, 14, 14, 0.875f, 1.4f),
                new("arrow_down_small_active", false, 105, 5, 8, 5, 14, 14, 0.875f, 1.4f),
                new("arrow_down_small_hover", false, 105, 10, 8, 5, 14, 14, 0.875f, 1.4f),
                new("arrow_left", false, 0, 0, 8, 14, 14, 14, 0.875f, 0.5f),
                new("arrow_left_active", false, 8, 0, 8, 14, 14, 14, 0.875f, 0.5f),
                new("arrow_left_hover", false, 16, 0, 8, 14, 14, 14, 0.875f, 0.5f),
                new("arrow_left_small", false, 90, 0, 5, 8, 14, 14, 1.4f, 0.875f),
                new("arrow_left_small_active", false, 90, 8, 5, 8, 14, 14, 1.4f, 0.875f),
                new("arrow_left_small_hover", false, 95, 0, 5, 8, 14, 14, 1.4f, 0.875f),
                new("arrow_right", false, 24, 0, 8, 14, 14, 14, 0.875f, 0.5f),
                new("arrow_right_active", false, 32, 0, 8, 14, 14, 14, 0.875f, 0.5f),
                new("arrow_right_hover", false, 40, 0, 8, 14, 14, 14, 0.875f, 0.5f),
                new("arrow_right_small", false, 95, 8, 5, 8, 14, 14, 1.4f, 0.875f),
                new("arrow_right_small_active", false, 100, 0, 5, 8, 14, 14, 1.4f, 0.875f),
                new("arrow_right_small_hover", false, 100, 8, 5, 8, 14, 14, 1.4f, 0.875f),
                new("arrow_up", false, 62, 8, 14, 8, 14, 14, 0.5f, 0.875f),
                new("arrow_up_active", false, 76, 0, 14, 8, 14, 14, 0.5f, 0.875f),
                new("arrow_up_howver", false, 76, 8, 14, 8, 14, 14, 0.5f, 0.875f),
                new("arrow_up_small", false, 113, 0, 8, 5, 14, 14, 0.875f, 1.4f),
                new("arrow_up_small_active", false, 113, 5, 8, 5, 14, 14, 0.875f, 1.4f),
                new("arrow_up_small_hover", false, 113, 10, 8, 5, 14, 14, 0.875f, 1.4f),

            }
        }];
    }

    public static TextureResult Arrow_down => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_down")!;
    public static TextureResult Arrow_down_active => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_down_active")!;
    public static TextureResult Arrow_down_hover => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_down_hover")!;
    public static TextureResult Arrow_down_small => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_down_small")!;
    public static TextureResult Arrow_down_small_active => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_down_small_active")!;
    public static TextureResult Arrow_down_small_hover => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_down_small_hover")!;
    public static TextureResult Arrow_left => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_left")!;
    public static TextureResult Arrow_left_active => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_left_active")!;
    public static TextureResult Arrow_left_hover => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_left_hover")!;
    public static TextureResult Arrow_left_small => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_left_small")!;
    public static TextureResult Arrow_left_small_active => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_left_small_active")!;
    public static TextureResult Arrow_left_small_hover => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_left_small_hover")!;
    public static TextureResult Arrow_right => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_right")!;
    public static TextureResult Arrow_right_active => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_right_active")!;
    public static TextureResult Arrow_right_hover => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_right_hover")!;
    public static TextureResult Arrow_right_small => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_right_small")!;
    public static TextureResult Arrow_right_small_active => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_right_small_active")!;
    public static TextureResult Arrow_right_small_hover => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_right_small_hover")!;
    public static TextureResult Arrow_up => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_up")!;
    public static TextureResult Arrow_up_active => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_up_active")!;
    public static TextureResult Arrow_up_howver => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_up_howver")!;
    public static TextureResult Arrow_up_small => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_up_small")!;
    public static TextureResult Arrow_up_small_active => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_up_small_active")!;
    public static TextureResult Arrow_up_small_hover => ContentHelper.GetTextureResult<ArrowsSpriteAtlas>("arrow_up_small_hover")!;

}