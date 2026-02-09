using PocketCardLeague.Consts;

namespace PocketCardLeague.Components;

public class PokeCard (PokeDexEntry dexEntry, int level = 1, int innatePower = 0, string? nickname = null)
{
    public PokeDexEntry BasePokemon { get; set; } = dexEntry;
    public int Level { get; set; } = level;
    public int InnatePower { get; set; } = innatePower;
    public string? Nickname { get; set; } = nickname;
}
