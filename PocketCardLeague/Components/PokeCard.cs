using PocketCardLeague.Consts;
using PocketCardLeague.Enums;
using System.Collections.Generic;

namespace PocketCardLeague.Components;

public class PokeCard (PokeDexEntry dexEntry, int level = 1, int innatePower = 0, string? nickname = null)
{
    public PokeDexEntry BasePokemon { get; set; } = dexEntry;
    public int Level { get; set; } = level;
    public int InnatePower { get; set; } = innatePower;
    public string? Nickname { get; set; } = nickname;
    public List<BerryEnergyType> Cost { get; set; } = [BerryEnergyType.Green, BerryEnergyType.Green, BerryEnergyType.Void];
    public List<string> Glyphs { get; set; } = [];
    public int MaxHP { get; set; }
    public int HP { get; set; }
    public int Def { get; set; }
    public int Atk { get; set; }
}
