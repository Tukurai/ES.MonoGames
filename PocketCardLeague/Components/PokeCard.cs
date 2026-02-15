using PocketCardLeague.Consts;
using PocketCardLeague.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Serialization;

namespace PocketCardLeague.Components;

public class PokeCard(string? basePokemonId, int level = 1, int innatePower = 1, string? nickname = null)
{
    const int MaxInnate = 4;
    const int BaseInnate = 1;

    const double AtkDivisor = 12.0;
    const double DefDivisor = 28.0;
    const double HpDivisor = 6.0;
    const double InnateScalar = 0.42;

    public string? BasePokemonId
    {
        get; set
        {
            field = value;

            if (!string.IsNullOrEmpty(value))
                _basePokemon = PokeDex.Entries.Find(e => e.SpriteIdentifier == value);
            _basePokemon ??= new PokeDexEntry();
        }
    } = basePokemonId;

    private PokeDexEntry? _basePokemon;

    [JsonIgnore]
    public PokeDexEntry BasePokemon
    {
        get => _basePokemon ??= PokeDex.Entries.Find(e => e.SpriteIdentifier == BasePokemonId) ?? new PokeDexEntry();
        set
        {
            _basePokemon = value;
            BasePokemonId = value.SpriteIdentifier;
        }
    }

    public int Level { get; set; } = level;
    public int InnatePower { get; set; } = innatePower;
    public string? Nickname { get; set; } = nickname;
    public List<BerryEnergyType> Cost { get; set; } = [BerryEnergyType.Green, BerryEnergyType.Green, BerryEnergyType.Void];
    public List<string> Glyphs { get; set; } = [];

    [JsonIgnore]
    public int HP { get; set; } // Current HP
    [JsonIgnore]
    public int MaxHp { get { return (int)Math.Round(UntributedHp + TributedHp); } }
    [JsonIgnore]
    public int Atk { get { return (int)Math.Round(UntributedAtk + TributedAtk); } }
    [JsonIgnore]
    public int Def { get { return (int)Math.Round(UntributedDef + TributedDef); } }

    // Calculate subtotal of the 6 main stats
    [JsonIgnore]
    public int BaseAtk { get { return BasePokemon.Attack + BasePokemon.SpecialAttack; } }
    [JsonIgnore]
    public int BaseDef { get { return BasePokemon.Defense + BasePokemon.SpecialDefense; } }
    [JsonIgnore]
    public int BaseHp { get { return BasePokemon.Hp; } }

    // Card stat calculation
    [JsonIgnore]
    private double UntributedAtk { get { return Math.Sqrt(BaseAtk) * Level / AtkDivisor; } }
    [JsonIgnore]
    private double TributedAtk { get { return CalcTributeStat(UntributedAtk); } }

    [JsonIgnore]
    private double UntributedDef { get { return Math.Sqrt(BaseDef) * Level / DefDivisor; } }
    [JsonIgnore]
    private double TributedDef { get { return CalcTributeStat(UntributedDef); } }

    [JsonIgnore]
    private double UntributedHp { get { return (Math.Sqrt(BaseHp <= 20 ? 1 : BaseHp) * Level / HpDivisor) + 1; } }
    [JsonIgnore]
    private double TributedHp { get { return CalcTributeStat(UntributedHp); } }

    private double CalcTributeStat(double untributedStat)
        => untributedStat * ((InnatePower - BaseInnate) / (double)(MaxInnate - BaseInnate)) * InnateScalar;
}
