using Helpers;

namespace PocketCardLeague.SpriteMaps;

public class TypesSpriteAtlas : SpriteAtlas
{
    public TypesSpriteAtlas() : base()
    {
        Atlas = [new("Types")
        {
            Name = "Types",
            SmartUpdateKey = "$TexturePacker:SmartUpdate:89e60e1e7710d109b7ee8fdc4505f90e:54a0b8b75a28a62582c6eef2c1d0a51e:20c7e4ba065060dd53a64a9cbad01766$",
            SpriteAtlas =
            {
                new("bug", false, 1, 1, 12, 11, 12, 11, 0.5f, 0.5f),
                new("dark", false, 15, 1, 12, 11, 12, 11, 0.5f, 0.5f),
                new("dragon", false, 1, 14, 12, 11, 12, 11, 0.5f, 0.5f),
                new("electric", false, 15, 14, 12, 11, 12, 11, 0.5f, 0.5f),
                new("fairy", false, 1, 27, 12, 11, 12, 11, 0.5f, 0.5f),
                new("fighting", false, 15, 27, 12, 11, 12, 11, 0.5f, 0.5f),
                new("fire", false, 1, 40, 12, 11, 12, 11, 0.5f, 0.5f),
                new("flying", false, 15, 40, 12, 11, 12, 11, 0.5f, 0.5f),
                new("ghost", false, 1, 53, 12, 11, 12, 11, 0.5f, 0.5f),
                new("grass", false, 15, 53, 12, 11, 12, 11, 0.5f, 0.5f),
                new("ground", false, 1, 66, 12, 11, 12, 11, 0.5f, 0.5f),
                new("ice", false, 15, 66, 12, 11, 12, 11, 0.5f, 0.5f),
                new("normal", false, 1, 79, 12, 11, 12, 11, 0.5f, 0.5f),
                new("poison", false, 15, 79, 12, 11, 12, 11, 0.5f, 0.5f),
                new("psychic", false, 1, 92, 12, 11, 12, 11, 0.5f, 0.5f),
                new("rock", false, 15, 92, 12, 11, 12, 11, 0.5f, 0.5f),
                new("steel", false, 1, 105, 12, 11, 12, 11, 0.5f, 0.5f),
                new("water", false, 15, 105, 12, 11, 12, 11, 0.5f, 0.5f),

            }
        }];
    }

    public static TextureResult Bug => ContentHelper.GetTextureResult<TypesSpriteAtlas>("bug")!;
    public static TextureResult Dark => ContentHelper.GetTextureResult<TypesSpriteAtlas>("dark")!;
    public static TextureResult Dragon => ContentHelper.GetTextureResult<TypesSpriteAtlas>("dragon")!;
    public static TextureResult Electric => ContentHelper.GetTextureResult<TypesSpriteAtlas>("electric")!;
    public static TextureResult Fairy => ContentHelper.GetTextureResult<TypesSpriteAtlas>("fairy")!;
    public static TextureResult Fighting => ContentHelper.GetTextureResult<TypesSpriteAtlas>("fighting")!;
    public static TextureResult Fire => ContentHelper.GetTextureResult<TypesSpriteAtlas>("fire")!;
    public static TextureResult Flying => ContentHelper.GetTextureResult<TypesSpriteAtlas>("flying")!;
    public static TextureResult Ghost => ContentHelper.GetTextureResult<TypesSpriteAtlas>("ghost")!;
    public static TextureResult Grass => ContentHelper.GetTextureResult<TypesSpriteAtlas>("grass")!;
    public static TextureResult Ground => ContentHelper.GetTextureResult<TypesSpriteAtlas>("ground")!;
    public static TextureResult Ice => ContentHelper.GetTextureResult<TypesSpriteAtlas>("ice")!;
    public static TextureResult Normal => ContentHelper.GetTextureResult<TypesSpriteAtlas>("normal")!;
    public static TextureResult Poison => ContentHelper.GetTextureResult<TypesSpriteAtlas>("poison")!;
    public static TextureResult Psychic => ContentHelper.GetTextureResult<TypesSpriteAtlas>("psychic")!;
    public static TextureResult Rock => ContentHelper.GetTextureResult<TypesSpriteAtlas>("rock")!;
    public static TextureResult Steel => ContentHelper.GetTextureResult<TypesSpriteAtlas>("steel")!;
    public static TextureResult Water => ContentHelper.GetTextureResult<TypesSpriteAtlas>("water")!;

}