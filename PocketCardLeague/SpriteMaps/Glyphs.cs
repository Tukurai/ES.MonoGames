using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class GlyphsSpriteAtlas : SpriteAtlas
{
    public GlyphsSpriteAtlas() : base()
    {
        Atlas = [new("Glyphs")
        {
            Name = "Glyphs",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:bd2bc857eaa7c3ad7b9b203a96801361:b5b26caa6670a4f9b84803a921c0fa5f:dd4b0303f1bf8770480cbc0fb8d7d378$",
            SpriteAtlas =
            {
                new("adaptability", false, 1, 1, 12, 11, 12, 11, 0.5f, 0.5f),
                new("berserk", false, 15, 1, 12, 11, 12, 11, 0.5f, 0.5f),
                new("bulwark", false, 1, 14, 12, 11, 12, 11, 0.5f, 0.5f),
                new("counter", false, 15, 14, 12, 11, 12, 11, 0.5f, 0.5f),
                new("courage", false, 1, 27, 12, 11, 12, 11, 0.5f, 0.5f),
                new("curse", false, 15, 27, 12, 11, 12, 11, 0.5f, 0.5f),
                new("debilitate", false, 1, 40, 12, 11, 12, 11, 0.5f, 0.5f),
                new("emtpy", false, 15, 40, 12, 11, 12, 11, 0.5f, 0.5f),
                new("fork", false, 1, 53, 12, 11, 12, 11, 0.5f, 0.5f),
                new("harvest", false, 15, 53, 12, 11, 12, 11, 0.5f, 0.5f),
                new("imposter", false, 1, 66, 12, 11, 12, 11, 0.5f, 0.5f),
                new("lucky", false, 15, 66, 12, 11, 12, 11, 0.5f, 0.5f),
                new("medic", false, 1, 79, 12, 11, 12, 11, 0.5f, 0.5f),
                new("memento", false, 15, 79, 12, 11, 12, 11, 0.5f, 0.5f),
                new("mist", false, 1, 92, 12, 11, 12, 11, 0.5f, 0.5f),
                new("piercing", false, 15, 92, 12, 11, 12, 11, 0.5f, 0.5f),
                new("recovery", false, 1, 105, 12, 11, 12, 11, 0.5f, 0.5f),
                new("ruthless", false, 15, 105, 12, 11, 12, 11, 0.5f, 0.5f),
                new("setback", false, 1, 118, 12, 11, 12, 11, 0.5f, 0.5f),
                new("shield", false, 15, 118, 12, 11, 12, 11, 0.5f, 0.5f),
                new("sleepy", false, 1, 131, 12, 11, 12, 11, 0.5f, 0.5f),
                new("tenacity", false, 15, 131, 12, 11, 12, 11, 0.5f, 0.5f),
                new("vampire", false, 1, 144, 12, 11, 12, 11, 0.5f, 0.5f),

            }
        }];
    }

    public static TextureResult Adaptability => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("adaptability")!;
    public static TextureResult Berserk => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("berserk")!;
    public static TextureResult Bulwark => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("bulwark")!;
    public static TextureResult Counter => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("counter")!;
    public static TextureResult Courage => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("courage")!;
    public static TextureResult Curse => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("curse")!;
    public static TextureResult Debilitate => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("debilitate")!;
    public static TextureResult Emtpy => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("emtpy")!;
    public static TextureResult Fork => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("fork")!;
    public static TextureResult Harvest => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("harvest")!;
    public static TextureResult Imposter => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("imposter")!;
    public static TextureResult Lucky => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("lucky")!;
    public static TextureResult Medic => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("medic")!;
    public static TextureResult Memento => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("memento")!;
    public static TextureResult Mist => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("mist")!;
    public static TextureResult Piercing => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("piercing")!;
    public static TextureResult Recovery => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("recovery")!;
    public static TextureResult Ruthless => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("ruthless")!;
    public static TextureResult Setback => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("setback")!;
    public static TextureResult Shield => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("shield")!;
    public static TextureResult Sleepy => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("sleepy")!;
    public static TextureResult Tenacity => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("tenacity")!;
    public static TextureResult Vampire => ContentHelper.GetTextureResult<GlyphsSpriteAtlas>("vampire")!;

}