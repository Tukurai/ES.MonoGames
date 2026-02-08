using PocketCardLeague.Enums;
using SharpDX.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PocketCardLeague.Consts;

public class BerryDexEntry
{
    public int Id { get; set; } = 0;
    public BerryEnergyType Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Variant { get; set; } = null;
    public string Description { get; set; } = string.Empty;

    [JsonIgnore]
    public string SpriteIdentifier => $"{Name.Split(" ")[0].ToLower()}{(string.IsNullOrEmpty(Variant) ? "" : $"_{Variant.ToLower()}")}";
}

public static class BerryDex
{
    public static readonly List<BerryDexEntry> Entries = [
        new() { Id = 1,  Type = BerryEnergyType.Red, Name = "Cheri Berry",   Description = "Cures paralysis." },
        new() { Id = 2,  Type = BerryEnergyType.Red, Name = "Chesto Berry",  Description = "Cures sleep." },
        new() { Id = 3,  Type = BerryEnergyType.Red, Name = "Pecha Berry",   Description = "Cures poison." },
        new() { Id = 4,  Type = BerryEnergyType.Red, Name = "Rawst Berry",   Description = "Cures burn." },
        new() { Id = 5,  Type = BerryEnergyType.Red, Name = "Aspear Berry",  Description = "Cures freeze." },
        new() { Id = 6,  Type = BerryEnergyType.Red, Name = "Leppa Berry",   Description = "Restores 10 PP." },
        new() { Id = 7,  Type = BerryEnergyType.Red, Name = "Oran Berry",    Description = "Restores 10 HP." },
        new() { Id = 8,  Type = BerryEnergyType.Red, Name = "Persim Berry",  Description = "Cures confusion." },
        new() { Id = 9,  Type = BerryEnergyType.Red, Name = "Lum Berry",     Description = "Cures all non-volatile status conditions." },
        new() { Id = 10, Type = BerryEnergyType.Red, Name = "Sitrus Berry",  Description = "Restores HP when below half." },
        new() { Id = 11, Type = BerryEnergyType.Red, Name = "Figy Berry",    Description = "Restores HP but may cause confusion." },
        new() { Id = 12, Type = BerryEnergyType.Red, Name = "Wiki Berry",    Description = "Restores HP but may cause confusion." },
        new() { Id = 13, Type = BerryEnergyType.Red, Name = "Mago Berry",    Description = "Restores HP but may cause confusion." },
        new() { Id = 14, Type = BerryEnergyType.Red, Name = "Aguav Berry",   Description = "Restores HP but may cause confusion." },
        new() { Id = 15, Type = BerryEnergyType.Red, Name = "Iapapa Berry",  Description = "Restores HP but may cause confusion." },
        new() { Id = 16, Type = BerryEnergyType.Red, Name = "Razz Berry",    Description = "Makes wild Pokémon easier to catch." },
        new() { Id = 17, Type = BerryEnergyType.Red, Name = "Bluk Berry",    Description = "No battle effect." },
        new() { Id = 18, Type = BerryEnergyType.Red, Name = "Nanab Berry",   Description = "Makes wild Pokémon calmer." },
        new() { Id = 19, Type = BerryEnergyType.Red, Name = "Wepear Berry",  Description = "No battle effect." },
        new() { Id = 20, Type = BerryEnergyType.Red, Name = "Pinap Berry",   Description = "Improves rewards from wild Pokémon." },
        new() { Id = 21, Type = BerryEnergyType.Red, Name = "Pomeg Berry",   Description = "Lowers HP EVs and raises friendship." },
        new() { Id = 22, Type = BerryEnergyType.Red, Name = "Kelpsy Berry",  Description = "Lowers Attack EVs and raises friendship." },
        new() { Id = 23, Type = BerryEnergyType.Red, Name = "Qualot Berry",  Description = "Lowers Defense EVs and raises friendship." },
        new() { Id = 24, Type = BerryEnergyType.Red, Name = "Hondew Berry",  Description = "Lowers Special Attack EVs and raises friendship." },
        new() { Id = 25, Type = BerryEnergyType.Red, Name = "Grepa Berry",   Description = "Lowers Special Defense EVs and raises friendship." },
        new() { Id = 26, Type = BerryEnergyType.Red, Name = "Tamato Berry",  Description = "Lowers Speed EVs and raises friendship." },
        new() { Id = 27, Type = BerryEnergyType.Red, Name = "Cornn Berry",   Description = "No battle effect." },
        new() { Id = 28, Type = BerryEnergyType.Red, Name = "Magost Berry",  Description = "No battle effect." },
        new() { Id = 29, Type = BerryEnergyType.Red, Name = "Rabuta Berry",  Description = "No battle effect." },
        new() { Id = 30, Type = BerryEnergyType.Red, Name = "Nomel Berry",   Description = "No battle effect." },
        new() { Id = 31, Type = BerryEnergyType.Red, Name = "Spelon Berry",  Description = "No battle effect." },
        new() { Id = 32, Type = BerryEnergyType.Red, Name = "Pamtre Berry",  Description = "No battle effect." },
        new() { Id = 33, Type = BerryEnergyType.Red, Name = "Watmel Berry",  Description = "No battle effect." },
        new() { Id = 34, Type = BerryEnergyType.Red, Name = "Durin Berry",   Description = "No battle effect." },
        new() { Id = 35, Type = BerryEnergyType.Red, Name = "Belue Berry",   Description = "No battle effect." },
        new() { Id = 36, Type = BerryEnergyType.Red, Name = "Occa Berry",    Description = "Reduces damage from a super effective attack." },
        new() { Id = 37, Type = BerryEnergyType.Red, Name = "Passho Berry",  Description = "Reduces damage from a super effective attack." },
        new() { Id = 38, Type = BerryEnergyType.Red, Name = "Wacan Berry",   Description = "Reduces damage from a super effective attack." },
        new() { Id = 39, Type = BerryEnergyType.Red, Name = "Rindo Berry",   Description = "Reduces damage from a super effective attack." },
        new() { Id = 40, Type = BerryEnergyType.Red, Name = "Yache Berry",   Description = "Reduces damage from a super effective attack." },
        new() { Id = 41, Type = BerryEnergyType.Red, Name = "Chople Berry",  Description = "Reduces damage from a super effective attack." },
        new() { Id = 42, Type = BerryEnergyType.Red, Name = "Kebia Berry",   Description = "Reduces damage from a super effective attack." },
        new() { Id = 43, Type = BerryEnergyType.Red, Name = "Shuca Berry",   Description = "Reduces damage from a super effective attack." },
        new() { Id = 44, Type = BerryEnergyType.Red, Name = "Coba Berry",    Description = "Reduces damage from a super effective attack." },
        new() { Id = 45, Type = BerryEnergyType.Red, Name = "Payapa Berry",  Description = "Reduces damage from a super effective attack." },
        new() { Id = 46, Type = BerryEnergyType.Red, Name = "Tanga Berry",   Description = "Reduces damage from a super effective attack." },
        new() { Id = 47, Type = BerryEnergyType.Red, Name = "Charti Berry",  Description = "Reduces damage from a super effective attack." },
        new() { Id = 48, Type = BerryEnergyType.Red, Name = "Kasib Berry",   Description = "Reduces damage from a super effective attack." },
        new() { Id = 49, Type = BerryEnergyType.Red, Name = "Haban Berry",   Description = "Reduces damage from a super effective attack." },
        new() { Id = 50, Type = BerryEnergyType.Red, Name = "Colbur Berry",  Description = "Reduces damage from a super effective attack." },
        new() { Id = 51, Type = BerryEnergyType.Red, Name = "Babiri Berry",  Description = "Reduces damage from a super effective attack." },
        new() { Id = 52, Type = BerryEnergyType.Red, Name = "Chilan Berry",  Description = "Reduces damage from normal attacks." },
        new() { Id = 53, Type = BerryEnergyType.Red, Name = "Liechi Berry",  Description = "Raises Attack when HP is low." },
        new() { Id = 54, Type = BerryEnergyType.Red, Name = "Ganlon Berry",  Description = "Raises Defense when HP is low." },
        new() { Id = 55, Type = BerryEnergyType.Red, Name = "Salac Berry",   Description = "Raises Speed when HP is low." },
        new() { Id = 56, Type = BerryEnergyType.Red, Name = "Petaya Berry",  Description = "Raises Special Attack when HP is low." },
        new() { Id = 57, Type = BerryEnergyType.Red, Name = "Apicot Berry",  Description = "Raises Special Defense when HP is low." },
        new() { Id = 58, Type = BerryEnergyType.Red, Name = "Lansat Berry",  Description = "Greatly raises critical hit ratio when HP is low." },
        new() { Id = 59, Type = BerryEnergyType.Red, Name = "Starf Berry",   Description = "Greatly raises a random stat when HP is low." },
        new() { Id = 60, Type = BerryEnergyType.Red, Name = "Enigma Berry",  Description = "Restores HP when hit by a super effective move." },
        new() { Id = 61, Type = BerryEnergyType.Red, Name = "Micle Berry",   Description = "Boosts accuracy of the next move when HP is low." },
        new() { Id = 62, Type = BerryEnergyType.Red, Name = "Custap Berry",  Description = "Allows the holder to move first when HP is low." },
        new() { Id = 63, Type = BerryEnergyType.Red, Name = "Jaboca Berry",  Description = "Damages attackers using physical moves." },
        new() { Id = 64, Type = BerryEnergyType.Red, Name = "Rowap Berry",   Description = "Damages attackers using special moves." },
        new() { Id = 65, Type = BerryEnergyType.Red, Name = "Roseli Berry",  Description = "Reduces damage from a super effective attack." },
        new() { Id = 66, Type = BerryEnergyType.Red, Name = "Kee Berry",     Description = "Raises Defense when hit by a physical move." },
        new() { Id = 67, Type = BerryEnergyType.Red, Name = "Maranga Berry", Description = "Raises Special Defense when hit by a special move." },
        new() { Id = 68, Type = BerryEnergyType.Red, Name = "Hopo Berry",    Description = "Restores PP and affects wild Pokémon behavior." },
    ];
}
