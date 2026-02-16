using PocketCardLeague.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PocketCardLeague.Consts;

public class PokeDexEntry
{
    public int Id { get; set; } = 0;
    public int FormId { get; set; } = 0;
    public int VariationId { get; set; } = 0;
    public string UniqueId => $"{Id}_{FormId}_{VariationId}_{(Gender.ToString().ToLowerInvariant())}";
    public string Name { get; set; } = string.Empty;
    public string FormName { get; set; } = string.Empty;
    public string VariationName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Shiny { get; set; } = false;
    public bool Secret { get; set; } = false;
    public bool Environment { get; set; } = false;
    public bool Legendary { get; set; } = false;
    public bool Gigantamax { get; set; } = false;
    public bool MegaEvolution { get; set; } = false;
    public GenderCode Gender { get; set; } = GenderCode.MF;
    public int VariantId { get; set; } = 0;
    public PokemonType Type1 { get; set; } = PokemonType.UNK;
    public PokemonType? Type2 { get; set; } = null;
    public List<PokemonType> Types => Type2 is null ? [Type1] : [Type1, Type2.Value];
    public int BaseStatTotal { get; set; } = 0;
    public int Hp { get; set; } = 0;
    public int Attack { get; set; } = 0;
    public int Defense { get; set; } = 0;
    public int SpecialAttack { get; set; } = 0;
    public int SpecialDefense { get; set; } = 0;
    public int Speed { get; set; } = 0;
    public int Generation { get; set; } = 1;


    [JsonIgnore]
    public string SpriteIdentifier => $"{Id:D4}_{FormId:D3}_{Gender.ToString().ToLower()}_{(Gigantamax ? 'g' : 'n')}_{VariantId:D8}_{(Shiny ? 'r' : 'n')}";
}

public static class PokeDex
{
    public static readonly List<PokeDexEntry> Entries = LoadEntries();

    private static List<PokeDexEntry> LoadEntries()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Content/Data/pokedex.json");
        if (!File.Exists(path)) return [];
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<PokeDexEntry>>(json) ?? [];
    }
}
