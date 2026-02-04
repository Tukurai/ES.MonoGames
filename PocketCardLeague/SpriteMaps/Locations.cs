using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class LocationsSpriteAtlas : SpriteAtlas
{
    public LocationsSpriteAtlas() : base()
    {
        Atlas = [new("Locations")
        {
            Name = "Locations",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:993043578a0dfe25faa6340cce4fe528:5f8eaba45675cec2d1bd83d031abf09a:3e2ac14ac5837f2394c3b535b7539b60$",
            SpriteAtlas =
            {
                new("border", false, 1, 1, 244, 116, 244, 116, 0.5f, 0.5f),
                new("cave_1", false, 247, 1, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_2", false, 247, 115, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_3", false, 1, 119, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_4", false, 243, 229, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_5", false, 1, 233, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_6", false, 243, 343, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_7", false, 1, 347, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_8", false, 243, 457, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_9", false, 1, 461, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_10", false, 243, 571, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_11", false, 1, 575, 240, 112, 240, 112, 0.5f, 0.5f),
                new("cave_12", false, 243, 685, 240, 112, 240, 112, 0.5f, 0.5f),
                new("city_1", false, 1, 689, 240, 112, 240, 112, 0.5f, 0.5f),
                new("forest_1", false, 243, 799, 240, 112, 240, 112, 0.5f, 0.5f),
                new("forest_2", false, 1, 803, 240, 112, 240, 112, 0.5f, 0.5f),
                new("hideout_1", false, 243, 913, 240, 112, 240, 112, 0.5f, 0.5f),
                new("hideout_2", false, 1, 917, 240, 112, 240, 112, 0.5f, 0.5f),
                new("hideout_3", false, 243, 1027, 240, 112, 240, 112, 0.5f, 0.5f),
                new("hideout_4", false, 1, 1031, 240, 112, 240, 112, 0.5f, 0.5f),
                new("locations-7", false, 243, 1141, 240, 112, 240, 112, 0.5f, 0.5f),
                new("locations-12", false, 1, 1145, 240, 112, 240, 112, 0.5f, 0.5f),

            }
        }];
    }

    public const string Border = "border";
    public const string Cave_1 = "cave_1";
    public const string Cave_2 = "cave_2";
    public const string Cave_3 = "cave_3";
    public const string Cave_4 = "cave_4";
    public const string Cave_5 = "cave_5";
    public const string Cave_6 = "cave_6";
    public const string Cave_7 = "cave_7";
    public const string Cave_8 = "cave_8";
    public const string Cave_9 = "cave_9";
    public const string Cave_10 = "cave_10";
    public const string Cave_11 = "cave_11";
    public const string Cave_12 = "cave_12";
    public const string City_1 = "city_1";
    public const string Forest_1 = "forest_1";
    public const string Forest_2 = "forest_2";
    public const string Hideout_1 = "hideout_1";
    public const string Hideout_2 = "hideout_2";
    public const string Hideout_3 = "hideout_3";
    public const string Hideout_4 = "hideout_4";
    public const string Locations_7 = "locations-7";
    public const string Locations_12 = "locations-12";

}

