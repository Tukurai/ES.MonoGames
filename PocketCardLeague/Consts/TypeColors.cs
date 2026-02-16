using Microsoft.Xna.Framework;
using PocketCardLeague.Enums;
using SharpDX.Direct2D1.Effects;
using System.Collections.Generic;

namespace PocketCardLeague.Consts;

public static class TypeColors
{
    private static readonly Dictionary<PokemonType, Color> Colors = new()
    {
        { PokemonType.Normal, new Color(169,182,214) },
        { PokemonType.Grass, new Color(174,214,170) },
        { PokemonType.Fire, new Color(219,181,174) },
        { PokemonType.Water, new Color(170,191,214) },
        { PokemonType.Electric, new Color(214,207,170) },
        { PokemonType.Flying, new Color(170,177,214) },
        { PokemonType.Fighting, new Color(214,170,183) },
        { PokemonType.Psychic, new Color(214,170,171) },
        { PokemonType.Fairy, new Color(214,170,210) },
        { PokemonType.Ground, new Color(214,186,170) },
        { PokemonType.Rock, new Color(214,202,170) },
        { PokemonType.Bug, new Color(200,214,170) },
        { PokemonType.Poison, new Color(199,170,213) },
        { PokemonType.Ice, new Color(172,214,208) },
        { PokemonType.Dragon, new Color(173,196,214) },
        { PokemonType.Steel, new Color(175,205,214) },
        { PokemonType.Ghost, new Color(176,186,214) },
        { PokemonType.UNK, new Color(104, 160, 144) },
    };

    public static Color Get(PokemonType type)
    {
        if (Colors.TryGetValue(type, out var color))
            return color;
        return Color.White;
    }
}
