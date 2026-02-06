using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class BerriesSpriteAtlas : SpriteAtlas
{
    public BerriesSpriteAtlas() : base()
    {
        Atlas = [new("Berries")
        {
            Name = "Berries",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:6fdcee4dcd76a12cf41ee520b946ddc3:41e0237f5cfb1ca62d848191241d106a:beb890e8d1bb2feb19d6aa5fcbe0421b$",
            SpriteAtlas =
            {
                new("aguav", false, 130, 27, 21, 24, 24, 24, 0.47619047619047616f, 0.5f),
                new("apicot", false, 344, 1, 19, 20, 24, 24, 0.5263157894736842f, 0.5f),
                new("aspear", false, 201, 76, 22, 22, 24, 24, 0.5f, 0.5f),
                new("babiri", false, 297, 49, 21, 21, 24, 24, 0.5238095238095238f, 0.47619047619047616f),
                new("belue", false, 131, 1, 23, 23, 24, 24, 0.4782608695652174f, 0.5217391304347826f),
                new("berry_blue", false, 1, 111, 5, 5, 5, 5, 0.5f, 0.5f),
                new("berry_blue_small", false, 1, 118, 5, 5, 5, 5, 0.5f, 0.5f),
                new("berry_green", false, 8, 111, 5, 5, 5, 5, 0.5f, 0.5f),
                new("berry_green_small", false, 8, 118, 5, 5, 5, 5, 0.5f, 0.5f),
                new("berry_purple", false, 15, 111, 5, 5, 5, 5, 0.5f, 0.5f),
                new("berry_purple_small", false, 15, 118, 5, 5, 5, 5, 0.5f, 0.5f),
                new("berry_red", false, 22, 111, 5, 5, 5, 5, 0.5f, 0.5f),
                new("berry_red_small", false, 22, 118, 5, 5, 5, 5, 0.5f, 0.5f),
                new("berry_white", false, 29, 111, 5, 5, 5, 5, 0.5f, 0.5f),
                new("berry_white_small", false, 29, 118, 5, 5, 5, 5, 0.5f, 0.5f),
                new("berry_yellow", false, 36, 111, 5, 5, 5, 5, 0.5f, 0.5f),
                new("berry_yellow_small", false, 36, 118, 5, 5, 5, 5, 0.5f, 0.5f),
                new("bluk", false, 154, 78, 22, 23, 24, 24, 0.5f, 0.4782608695652174f),
                new("charti", false, 363, 46, 19, 19, 24, 24, 0.5263157894736842f, 0.47368421052631576f),
                new("cheri", false, 1, 85, 24, 24, 24, 24, 0.5f, 0.5f),
                new("chesto", false, 320, 48, 20, 21, 24, 24, 0.5f, 0.47619047619047616f),
                new("chilan", false, 275, 1, 21, 22, 24, 24, 0.5238095238095238f, 0.5f),
                new("chople", false, 255, 98, 21, 22, 24, 24, 0.47619047619047616f, 0.5f),
                new("coba", false, 319, 25, 21, 21, 24, 24, 0.5238095238095238f, 0.47619047619047616f),
                new("colbur", false, 202, 51, 22, 22, 24, 24, 0.5f, 0.5f),
                new("cornn", true, 106, 1, 23, 24, 24, 24, 0.5f, 0.4782608695652174f),
                new("custap", false, 224, 26, 22, 22, 24, 24, 0.5f, 0.5f),
                new("durin", false, 55, 27, 23, 24, 24, 24, 0.5217391304347826f, 0.5f),
                new("enigma", false, 227, 1, 22, 22, 24, 24, 0.5f, 0.5f),
                new("figy", true, 57, 105, 24, 22, 24, 24, 0.5f, 0.5f),
                new("ganlon", false, 207, 100, 22, 22, 24, 24, 0.5f, 0.5f),
                new("grepa", false, 135, 105, 23, 22, 24, 24, 0.4782608695652174f, 0.5f),
                new("haban", false, 225, 75, 22, 22, 24, 24, 0.5f, 0.5f),
                new("hondew", false, 160, 103, 22, 23, 24, 24, 0.5f, 0.4782608695652174f),
                new("iapapa", false, 55, 53, 23, 24, 24, 24, 0.4782608695652174f, 0.5f),
                new("jaboca", false, 226, 50, 22, 22, 24, 24, 0.5f, 0.5f),
                new("jam_blue_green", false, 43, 111, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_blue_green_small", false, 43, 118, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_blue_purple", false, 50, 111, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_blue_purple_small", false, 50, 118, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_blue_red", false, 255, 122, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_blue_red_small", false, 262, 122, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_blue_white", false, 269, 122, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_blue_white_small", false, 276, 122, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_blue_yellow", false, 283, 121, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_blue_yellow_small", false, 290, 121, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_green_blue", false, 297, 121, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_green_blue_small", false, 304, 120, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_green_purple", false, 311, 120, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_green_purple_small", false, 318, 120, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_green_red", false, 325, 118, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_green_red_small", false, 332, 118, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_green_white", false, 339, 118, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_green_white_small", false, 346, 117, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_green_yellow", false, 353, 117, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_green_yellow_small", false, 360, 117, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_purple_blue", false, 365, 1, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_purple_blue_small", false, 365, 8, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_purple_green", false, 365, 15, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_purple_green_small", false, 372, 1, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_purple_red", false, 372, 8, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_purple_red_small", false, 372, 15, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_purple_white", false, 379, 1, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_purple_white_small", false, 379, 8, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_purple_yellow", false, 379, 15, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_purple_yellow_small", false, 363, 67, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_red_blue", false, 363, 74, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_red_blue_small", false, 363, 81, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_red_green", false, 367, 88, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_red_green_small", false, 370, 67, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_red_purple", false, 370, 74, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_red_purple_small", false, 370, 81, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_red_white", false, 367, 95, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_red_white_small", false, 367, 102, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_red_yellow", false, 367, 109, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_red_yellow_small", false, 367, 116, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_white_blue", false, 374, 88, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_white_blue_small", false, 377, 67, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_white_green", false, 377, 74, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_white_green_small", false, 377, 81, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_white_purple", false, 374, 95, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_white_purple_small", false, 374, 102, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_white_red", false, 374, 109, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_white_red_small", false, 374, 116, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_white_yellow", false, 381, 88, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_white_yellow_small", false, 381, 95, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_yellow_blue", false, 381, 102, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_yellow_blue_small", false, 381, 109, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_yellow_green", false, 381, 116, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_yellow_green_small", false, 384, 44, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_yellow_purple", false, 385, 22, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_yellow_purple_small", false, 385, 29, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_yellow_red", false, 385, 36, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_yellow_red_small", false, 386, 1, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_yellow_white", false, 386, 8, 5, 5, 5, 5, 0.5f, 0.5f),
                new("jam_yellow_white_small", false, 386, 15, 5, 5, 5, 5, 0.5f, 0.5f),
                new("kasib", false, 27, 85, 24, 24, 24, 24, 0.5f, 0.5f),
                new("kebia", false, 321, 1, 21, 21, 24, 24, 0.5238095238095238f, 0.47619047619047616f),
                new("kee", true, 105, 53, 23, 24, 24, 24, 0.5f, 0.5217391304347826f),
                new("kelpsy", false, 79, 79, 23, 24, 24, 24, 0.4782608695652174f, 0.5f),
                new("lansat", false, 178, 76, 21, 23, 24, 24, 0.5238095238095238f, 0.5217391304347826f),
                new("leppa", true, 129, 79, 23, 24, 24, 24, 0.5f, 0.4782608695652174f),
                new("liechi", true, 296, 25, 21, 22, 24, 24, 0.5f, 0.47619047619047616f),
                new("lum", true, 342, 46, 19, 20, 24, 24, 0.5f, 0.47368421052631576f),
                new("mago", false, 323, 95, 20, 21, 24, 24, 0.5f, 0.47619047619047616f),
                new("magost", false, 341, 71, 20, 21, 24, 24, 0.5f, 0.47619047619047616f),
                new("maranga", false, 80, 27, 23, 24, 24, 24, 0.5217391304347826f, 0.5f),
                new("micle", false, 342, 24, 20, 20, 24, 24, 0.5f, 0.5f),
                new("nanab", false, 1, 1, 26, 26, 34, 34, 0.46153846153846156f, 0.5f),
                new("nanab_classic", false, 29, 1, 24, 24, 24, 24, 0.5f, 0.5f),
                new("nanab_gold", false, 1, 29, 26, 26, 34, 34, 0.46153846153846156f, 0.5f),
                new("nanab_silver", false, 1, 57, 26, 26, 34, 34, 0.46153846153846156f, 0.5f),
                new("nomel", false, 248, 25, 22, 22, 24, 24, 0.5f, 0.5f),
                new("occa", false, 364, 23, 19, 19, 24, 24, 0.5263157894736842f, 0.47368421052631576f),
                new("oran", true, 345, 94, 20, 21, 24, 24, 0.47619047619047616f, 0.5f),
                new("pamtre", true, 83, 105, 24, 22, 24, 24, 0.5f, 0.5f),
                new("passho", true, 296, 73, 20, 22, 24, 24, 0.5f, 0.5f),
                new("payapa", false, 29, 27, 24, 24, 24, 24, 0.5f, 0.5f),
                new("pecha", false, 251, 1, 22, 22, 24, 24, 0.5f, 0.5f),
                new("persim", false, 231, 99, 22, 22, 24, 24, 0.5f, 0.5f),
                new("petaya", false, 153, 26, 22, 23, 24, 24, 0.5f, 0.4782608695652174f),
                new("pinap", false, 81, 1, 23, 24, 24, 24, 0.5217391304347826f, 0.5f),
                new("pinap_gold", false, 80, 53, 23, 24, 34, 34, 0.5217391304347826f, 0.5f),
                new("pinap_silver", false, 104, 79, 23, 24, 34, 34, 0.5217391304347826f, 0.5f),
                new("pomeg", false, 179, 51, 21, 23, 24, 24, 0.47619047619047616f, 0.4782608695652174f),
                new("qualot", false, 300, 97, 21, 21, 24, 24, 0.47619047619047616f, 0.47619047619047616f),
                new("rabuta", false, 29, 53, 24, 24, 24, 24, 0.5f, 0.5f),
                new("rawst", true, 180, 1, 22, 23, 24, 24, 0.4782608695652174f, 0.5f),
                new("razz", false, 201, 26, 21, 23, 34, 34, 0.47619047619047616f, 0.5217391304347826f),
                new("razz_classic", false, 249, 74, 22, 22, 24, 24, 0.5f, 0.5f),
                new("razz_gold", false, 204, 1, 21, 23, 34, 34, 0.47619047619047616f, 0.5217391304347826f),
                new("razz_silver", false, 184, 101, 21, 23, 34, 34, 0.47619047619047616f, 0.5217391304347826f),
                new("rindo", false, 273, 73, 21, 22, 24, 24, 0.5238095238095238f, 0.5f),
                new("roseli", false, 318, 72, 21, 21, 24, 24, 0.47619047619047616f, 0.47619047619047616f),
                new("rowap", false, 130, 53, 23, 23, 24, 24, 0.5217391304347826f, 0.4782608695652174f),
                new("salac", false, 53, 79, 24, 24, 24, 24, 0.5f, 0.5f),
                new("shuca", false, 274, 49, 21, 22, 24, 24, 0.5238095238095238f, 0.5f),
                new("sitrus", false, 278, 97, 20, 22, 24, 24, 0.5f, 0.5f),
                new("spelon", false, 156, 1, 22, 23, 24, 24, 0.5f, 0.5217391304347826f),
                new("starf", false, 250, 49, 22, 22, 24, 24, 0.5f, 0.5f),
                new("tamato", false, 155, 51, 22, 23, 24, 24, 0.5f, 0.4782608695652174f),
                new("tanga", false, 272, 25, 22, 22, 24, 24, 0.5f, 0.5f),
                new("wacan", true, 298, 1, 21, 22, 24, 24, 0.5f, 0.5238095238095238f),
                new("watmel", false, 55, 1, 24, 24, 24, 24, 0.5f, 0.5f),
                new("wepear", true, 109, 105, 24, 22, 24, 24, 0.5f, 0.5f),
                new("wiki", false, 105, 27, 23, 24, 24, 24, 0.4782608695652174f, 0.5f),
                new("yache", false, 177, 26, 22, 23, 24, 24, 0.5f, 0.4782608695652174f),

            }
        }];
    }

    public static TextureResult Aguav => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("aguav")!;
    public static TextureResult Apicot => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("apicot")!;
    public static TextureResult Aspear => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("aspear")!;
    public static TextureResult Babiri => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("babiri")!;
    public static TextureResult Belue => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("belue")!;
    public static TextureResult Berry_blue => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("berry_blue")!;
    public static TextureResult Berry_blue_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("berry_blue_small")!;
    public static TextureResult Berry_green => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("berry_green")!;
    public static TextureResult Berry_green_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("berry_green_small")!;
    public static TextureResult Berry_purple => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("berry_purple")!;
    public static TextureResult Berry_purple_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("berry_purple_small")!;
    public static TextureResult Berry_red => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("berry_red")!;
    public static TextureResult Berry_red_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("berry_red_small")!;
    public static TextureResult Berry_white => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("berry_white")!;
    public static TextureResult Berry_white_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("berry_white_small")!;
    public static TextureResult Berry_yellow => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("berry_yellow")!;
    public static TextureResult Berry_yellow_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("berry_yellow_small")!;
    public static TextureResult Bluk => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("bluk")!;
    public static TextureResult Charti => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("charti")!;
    public static TextureResult Cheri => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("cheri")!;
    public static TextureResult Chesto => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("chesto")!;
    public static TextureResult Chilan => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("chilan")!;
    public static TextureResult Chople => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("chople")!;
    public static TextureResult Coba => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("coba")!;
    public static TextureResult Colbur => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("colbur")!;
    public static TextureResult Cornn => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("cornn")!;
    public static TextureResult Custap => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("custap")!;
    public static TextureResult Durin => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("durin")!;
    public static TextureResult Enigma => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("enigma")!;
    public static TextureResult Figy => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("figy")!;
    public static TextureResult Ganlon => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("ganlon")!;
    public static TextureResult Grepa => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("grepa")!;
    public static TextureResult Haban => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("haban")!;
    public static TextureResult Hondew => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("hondew")!;
    public static TextureResult Iapapa => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("iapapa")!;
    public static TextureResult Jaboca => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jaboca")!;
    public static TextureResult Jam_blue_green => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_blue_green")!;
    public static TextureResult Jam_blue_green_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_blue_green_small")!;
    public static TextureResult Jam_blue_purple => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_blue_purple")!;
    public static TextureResult Jam_blue_purple_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_blue_purple_small")!;
    public static TextureResult Jam_blue_red => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_blue_red")!;
    public static TextureResult Jam_blue_red_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_blue_red_small")!;
    public static TextureResult Jam_blue_white => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_blue_white")!;
    public static TextureResult Jam_blue_white_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_blue_white_small")!;
    public static TextureResult Jam_blue_yellow => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_blue_yellow")!;
    public static TextureResult Jam_blue_yellow_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_blue_yellow_small")!;
    public static TextureResult Jam_green_blue => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_green_blue")!;
    public static TextureResult Jam_green_blue_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_green_blue_small")!;
    public static TextureResult Jam_green_purple => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_green_purple")!;
    public static TextureResult Jam_green_purple_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_green_purple_small")!;
    public static TextureResult Jam_green_red => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_green_red")!;
    public static TextureResult Jam_green_red_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_green_red_small")!;
    public static TextureResult Jam_green_white => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_green_white")!;
    public static TextureResult Jam_green_white_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_green_white_small")!;
    public static TextureResult Jam_green_yellow => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_green_yellow")!;
    public static TextureResult Jam_green_yellow_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_green_yellow_small")!;
    public static TextureResult Jam_purple_blue => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_purple_blue")!;
    public static TextureResult Jam_purple_blue_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_purple_blue_small")!;
    public static TextureResult Jam_purple_green => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_purple_green")!;
    public static TextureResult Jam_purple_green_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_purple_green_small")!;
    public static TextureResult Jam_purple_red => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_purple_red")!;
    public static TextureResult Jam_purple_red_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_purple_red_small")!;
    public static TextureResult Jam_purple_white => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_purple_white")!;
    public static TextureResult Jam_purple_white_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_purple_white_small")!;
    public static TextureResult Jam_purple_yellow => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_purple_yellow")!;
    public static TextureResult Jam_purple_yellow_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_purple_yellow_small")!;
    public static TextureResult Jam_red_blue => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_red_blue")!;
    public static TextureResult Jam_red_blue_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_red_blue_small")!;
    public static TextureResult Jam_red_green => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_red_green")!;
    public static TextureResult Jam_red_green_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_red_green_small")!;
    public static TextureResult Jam_red_purple => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_red_purple")!;
    public static TextureResult Jam_red_purple_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_red_purple_small")!;
    public static TextureResult Jam_red_white => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_red_white")!;
    public static TextureResult Jam_red_white_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_red_white_small")!;
    public static TextureResult Jam_red_yellow => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_red_yellow")!;
    public static TextureResult Jam_red_yellow_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_red_yellow_small")!;
    public static TextureResult Jam_white_blue => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_white_blue")!;
    public static TextureResult Jam_white_blue_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_white_blue_small")!;
    public static TextureResult Jam_white_green => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_white_green")!;
    public static TextureResult Jam_white_green_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_white_green_small")!;
    public static TextureResult Jam_white_purple => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_white_purple")!;
    public static TextureResult Jam_white_purple_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_white_purple_small")!;
    public static TextureResult Jam_white_red => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_white_red")!;
    public static TextureResult Jam_white_red_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_white_red_small")!;
    public static TextureResult Jam_white_yellow => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_white_yellow")!;
    public static TextureResult Jam_white_yellow_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_white_yellow_small")!;
    public static TextureResult Jam_yellow_blue => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_yellow_blue")!;
    public static TextureResult Jam_yellow_blue_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_yellow_blue_small")!;
    public static TextureResult Jam_yellow_green => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_yellow_green")!;
    public static TextureResult Jam_yellow_green_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_yellow_green_small")!;
    public static TextureResult Jam_yellow_purple => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_yellow_purple")!;
    public static TextureResult Jam_yellow_purple_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_yellow_purple_small")!;
    public static TextureResult Jam_yellow_red => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_yellow_red")!;
    public static TextureResult Jam_yellow_red_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_yellow_red_small")!;
    public static TextureResult Jam_yellow_white => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_yellow_white")!;
    public static TextureResult Jam_yellow_white_small => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("jam_yellow_white_small")!;
    public static TextureResult Kasib => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("kasib")!;
    public static TextureResult Kebia => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("kebia")!;
    public static TextureResult Kee => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("kee")!;
    public static TextureResult Kelpsy => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("kelpsy")!;
    public static TextureResult Lansat => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("lansat")!;
    public static TextureResult Leppa => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("leppa")!;
    public static TextureResult Liechi => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("liechi")!;
    public static TextureResult Lum => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("lum")!;
    public static TextureResult Mago => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("mago")!;
    public static TextureResult Magost => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("magost")!;
    public static TextureResult Maranga => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("maranga")!;
    public static TextureResult Micle => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("micle")!;
    public static TextureResult Nanab => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("nanab")!;
    public static TextureResult Nanab_classic => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("nanab_classic")!;
    public static TextureResult Nanab_gold => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("nanab_gold")!;
    public static TextureResult Nanab_silver => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("nanab_silver")!;
    public static TextureResult Nomel => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("nomel")!;
    public static TextureResult Occa => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("occa")!;
    public static TextureResult Oran => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("oran")!;
    public static TextureResult Pamtre => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("pamtre")!;
    public static TextureResult Passho => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("passho")!;
    public static TextureResult Payapa => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("payapa")!;
    public static TextureResult Pecha => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("pecha")!;
    public static TextureResult Persim => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("persim")!;
    public static TextureResult Petaya => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("petaya")!;
    public static TextureResult Pinap => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("pinap")!;
    public static TextureResult Pinap_gold => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("pinap_gold")!;
    public static TextureResult Pinap_silver => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("pinap_silver")!;
    public static TextureResult Pomeg => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("pomeg")!;
    public static TextureResult Qualot => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("qualot")!;
    public static TextureResult Rabuta => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("rabuta")!;
    public static TextureResult Rawst => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("rawst")!;
    public static TextureResult Razz => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("razz")!;
    public static TextureResult Razz_classic => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("razz_classic")!;
    public static TextureResult Razz_gold => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("razz_gold")!;
    public static TextureResult Razz_silver => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("razz_silver")!;
    public static TextureResult Rindo => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("rindo")!;
    public static TextureResult Roseli => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("roseli")!;
    public static TextureResult Rowap => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("rowap")!;
    public static TextureResult Salac => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("salac")!;
    public static TextureResult Shuca => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("shuca")!;
    public static TextureResult Sitrus => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("sitrus")!;
    public static TextureResult Spelon => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("spelon")!;
    public static TextureResult Starf => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("starf")!;
    public static TextureResult Tamato => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("tamato")!;
    public static TextureResult Tanga => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("tanga")!;
    public static TextureResult Wacan => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("wacan")!;
    public static TextureResult Watmel => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("watmel")!;
    public static TextureResult Wepear => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("wepear")!;
    public static TextureResult Wiki => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("wiki")!;
    public static TextureResult Yache => ContentHelper.GetTextureResult<BerriesSpriteAtlas>("yache")!;

}