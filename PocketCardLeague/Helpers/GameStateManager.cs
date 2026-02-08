using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PocketCardLeague.Helpers;

public static class GameStateManager
{
    public static List<GameSave> Saves { get; set; } = [];
    public static GameSave ActiveSave { get; set; } = new GameSave();

    private static readonly string SaveDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "PocketCardLeague",
        "Saves");

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() },
    };

    // HMAC-SHA256 key for save integrity verification. Change this before shipping.
    private static readonly byte[] HmacKey =
    [
        0x4C, 0x65, 0x61, 0x67, 0x75, 0x65, 0x4F, 0x66,
        0x50, 0x6F, 0x63, 0x6B, 0x65, 0x74, 0x43, 0x61,
        0x72, 0x64, 0x73, 0x21, 0x53, 0x65, 0x63, 0x72,
        0x65, 0x74, 0x4B, 0x65, 0x79, 0x5F, 0x76, 0x31,
    ];

    private const int HmacSize = 32; // SHA256 produces 32 bytes

#if DEBUG
    private const string SaveExtension = ".json";
#else
    private const string SaveExtension = ".sav";
#endif

    /// <summary>
    /// Saves a single GameSave to disk.
    /// Debug: plain JSON (.json). Release: HMAC-signed binary (.sav).
    /// </summary>
    public static void Save(GameSave save)
    {
        Directory.CreateDirectory(SaveDirectory);
        var path = Path.Combine(SaveDirectory, $"{save.Id}{SaveExtension}");
        var json = JsonSerializer.Serialize(save, JsonOptions);

#if DEBUG
        File.WriteAllText(path, json);
#else
        var jsonBytes = Encoding.UTF8.GetBytes(json);
        var hmac = ComputeHmac(jsonBytes);
        using var fs = File.Create(path);
        fs.Write(hmac);
        fs.Write(jsonBytes);
#endif
    }

    /// <summary>
    /// Saves all saves in the Saves list to disk.
    /// </summary>
    public static void SaveAll()
    {
        foreach (var save in Saves)
            Save(save);
    }

    /// <summary>
    /// Loads a single GameSave by its Id. Returns null if not found or tampered with.
    /// </summary>
    public static GameSave? Load(Guid id)
    {
        var path = Path.Combine(SaveDirectory, $"{id}{SaveExtension}");
        if (!File.Exists(path))
            return null;

        return ReadSave(path);
    }

    /// <summary>
    /// Loads all save files from the save directory into the Saves list.
    /// </summary>
    public static void LoadAll()
    {
        Saves.Clear();

        if (!Directory.Exists(SaveDirectory))
            return;

        foreach (var file in Directory.GetFiles(SaveDirectory, $"*{SaveExtension}"))
        {
            var save = ReadSave(file);
            if (save is not null)
                Saves.Add(save);
        }
    }

    /// <summary>
    /// Deletes a save file from disk and removes it from the Saves list.
    /// </summary>
    public static void Delete(GameSave save)
    {
        var path = Path.Combine(SaveDirectory, $"{save.Id}{SaveExtension}");
        if (File.Exists(path))
            File.Delete(path);

        Saves.Remove(save);
    }

    private static GameSave? ReadSave(string path)
    {
#if DEBUG
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<GameSave>(json, JsonOptions);
#else
        var data = File.ReadAllBytes(path);
        if (data.Length <= HmacSize)
            return null;

        var storedHmac = data.AsSpan(0, HmacSize);
        var jsonBytes = data.AsSpan(HmacSize);

        var computedHmac = ComputeHmac(jsonBytes);
        if (!CryptographicOperations.FixedTimeEquals(storedHmac, computedHmac))
            return null;

        var json = Encoding.UTF8.GetString(jsonBytes);
        return JsonSerializer.Deserialize<GameSave>(json, JsonOptions);
#endif
    }

    private static byte[] ComputeHmac(ReadOnlySpan<byte> data)
    {
        using var hmac = new HMACSHA256(HmacKey);
        return hmac.ComputeHash(data.ToArray());
    }
}
