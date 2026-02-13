using Microsoft.Xna.Framework;
using PocketCardLeague.Enums;
using System.Collections.Generic;

namespace PocketCardLeague.Consts;

public static class TypeColors
{
    private static readonly Dictionary<PokemonType, Color> Colors = new()
    {
        { PokemonType.Normal,   new Color(168, 168, 120) },
        { PokemonType.Fighting, new Color(192, 48, 40) },
        { PokemonType.Flying,   new Color(168, 144, 240) },
        { PokemonType.Poison,   new Color(160, 64, 160) },
        { PokemonType.Ground,   new Color(224, 192, 104) },
        { PokemonType.Rock,     new Color(184, 160, 56) },
        { PokemonType.Bug,      new Color(168, 184, 32) },
        { PokemonType.Ghost,    new Color(112, 88, 152) },
        { PokemonType.Steel,    new Color(184, 184, 208) },
        { PokemonType.Fire,     new Color(240, 128, 48) },
        { PokemonType.Water,    new Color(104, 144, 240) },
        { PokemonType.Grass,    new Color(120, 200, 80) },
        { PokemonType.Electric, new Color(248, 208, 48) },
        { PokemonType.Psychic,  new Color(248, 88, 136) },
        { PokemonType.Ice,      new Color(152, 216, 216) },
        { PokemonType.Dragon,   new Color(112, 56, 248) },
        { PokemonType.Dark,     new Color(112, 88, 72) },
        { PokemonType.Fairy,    new Color(238, 153, 172) },
        { PokemonType.UNK,      new Color(104, 160, 144) },
    };

    public static Color Get(PokemonType type)
    {
        if (Colors.TryGetValue(type, out var color))
            return color;
        return Color.White;
    }
}
