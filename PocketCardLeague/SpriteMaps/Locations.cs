using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class LocationsSpriteAtlas : SpriteAtlas
{
    public LocationsSpriteAtlas() : base()
    {
        Atlas = [new("Locations")
        {
            Name = "Locations",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:19326e4c3e20f4e7ca50fd4f5c7a091e:5f8eaba45675cec2d1bd83d031abf09a:3e2ac14ac5837f2394c3b535b7539b60$",
            SpriteAtlas =
            {
                new("border", false, 0, 0, 244, 116, 244, 116, 0.5f, 0.5f),
                new("cave_1", false, 244, 0, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_2", false, 244, 112, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_3", false, 0, 116, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_4", false, 240, 224, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_5", false, 0, 228, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_6", false, 240, 336, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_7", false, 0, 340, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_8", false, 240, 448, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_9", false, 0, 452, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_10", false, 240, 560, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_11", false, 0, 564, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_12", false, 240, 672, 240, 112, 240, 112, 0.5f, 0.5f),
                new("city_1", false, 0, 676, 240, 112, 240, 112, 0.5f, 0.5f),
                new("forest_1", false, 240, 784, 240, 112, 240, 112, 0.5f, 0.5f),
                new("forest_2", false, 0, 788, 240, 112, 240, 112, 0.5f, 0.5f),
                new("hideout_1", false, 240, 896, 240, 112, 240, 112, 0.5f, 0.5f),
                new("hideout_2", false, 0, 900, 240, 112, 240, 112, 0.5f, 0.5f),
                new("hideout_3", false, 240, 1008, 240, 112, 240, 112, 0.5f, 0.5f),
                new("hideout_4", false, 0, 1012, 240, 112, 240, 112, 0.5f, 0.5f),
                new("locations-7", false, 240, 1120, 240, 112, 240, 112, 0.5f, 0.5f),
                new("locations-12", false, 0, 1124, 240, 112, 240, 112, 0.5f, 0.5f),

            }
        }];
    }

    public static TextureResult Border => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("border")!;
    public static TextureResult Cave_1 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("cave_1")!;
    public static TextureResult Cave_2 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("cave_2")!;
    public static TextureResult Cave_3 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("cave_3")!;
    public static TextureResult Cave_4 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("cave_4")!;
    public static TextureResult Cave_5 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("cave_5")!;
    public static TextureResult Cave_6 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("cave_6")!;
    public static TextureResult Cave_7 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("cave_7")!;
    public static TextureResult Cave_8 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("cave_8")!;
    public static TextureResult Cave_9 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("cave_9")!;
    public static TextureResult Cave_10 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("cave_10")!;
    public static TextureResult Cave_11 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("cave_11")!;
    public static TextureResult Cave_12 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("cave_12")!;
    public static TextureResult City_1 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("city_1")!;
    public static TextureResult Forest_1 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("forest_1")!;
    public static TextureResult Forest_2 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("forest_2")!;
    public static TextureResult Hideout_1 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("hideout_1")!;
    public static TextureResult Hideout_2 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("hideout_2")!;
    public static TextureResult Hideout_3 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("hideout_3")!;
    public static TextureResult Hideout_4 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("hideout_4")!;
    public static TextureResult Locations_7 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("locations-7")!;
    public static TextureResult Locations_12 => ContentHelper.GetTextureResult<LocationsSpriteAtlas>("locations-12")!;

}