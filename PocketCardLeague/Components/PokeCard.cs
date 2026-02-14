using PocketCardLeague.Consts;
using PocketCardLeague.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PocketCardLeague.Components;

public class PokeCard
{
    public PokeCard() { }

    public PokeCard(PokeDexEntry dexEntry, int level = 1, int innatePower = 0, string? nickname = null)
    {
        BasePokemonId = dexEntry.SpriteIdentifier;
        _basePokemon = dexEntry;
        Level = level;
        InnatePower = innatePower;
        Nickname = nickname;
    }

    public string BasePokemonId { get; set; } = string.Empty;

    private PokeDexEntry? _basePokemon;

    public PokeDexEntry BasePokemon
    {
        get => _basePokemon ??= PokeDex.Entries.Find(e => e.SpriteIdentifier == BasePokemonId) ?? new PokeDexEntry();
        set
        {
            _basePokemon = value;
            BasePokemonId = value.SpriteIdentifier;
        }
    }

    public int Level { get; set; } = 1;
    public int InnatePower { get; set; }
    public string? Nickname { get; set; }
    public List<BerryEnergyType> Cost { get; set; } = [BerryEnergyType.Green, BerryEnergyType.Green, BerryEnergyType.Void];
    public List<string> Glyphs { get; set; } = [];
    public int MaxHP { get; set; }
    public int HP { get; set; }
    public int Def { get; set; }
    public int Atk { get; set; }
}
