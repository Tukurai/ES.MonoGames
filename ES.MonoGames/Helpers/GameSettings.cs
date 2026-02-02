using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;

namespace Helpers;

/// <summary>
/// Represents the game's configurable settings.
/// </summary>
public class GameSettings
{
    /// <summary>
    /// Window scale multiplier. The game renders at virtual resolution * scale.
    /// </summary>
    public int WindowScale { get; set; } = 1;

    /// <summary>
    /// Whether the game runs in fullscreen mode.
    /// </summary>
    public bool Fullscreen { get; set; } = false;

    /// <summary>
    /// Master volume (0-100).
    /// </summary>
    public int MasterVolume { get; set; } = 100;

    /// <summary>
    /// Music volume (0-100).
    /// </summary>
    public int MusicVolume { get; set; } = 75;

    /// <summary>
    /// Sound effects volume (0-100).
    /// </summary>
    public int SfxVolume { get; set; } = 75;

    /// <summary>
    /// Creates a copy of the current settings.
    /// </summary>
    public GameSettings Clone()
    {
        return new GameSettings
        {
            WindowScale = WindowScale,
            Fullscreen = Fullscreen,
            MasterVolume = MasterVolume,
            MusicVolume = MusicVolume,
            SfxVolume = SfxVolume
        };
    }
}

/// <summary>
/// Manages game settings including saving, loading, and applying them.
/// </summary>
public static class SettingsManager
{
    private static GameSettings _current = new();
    private static string _settingsPath = "settings.json";

    /// <summary>
    /// The current game settings.
    /// </summary>
    public static GameSettings Current => _current;

    /// <summary>
    /// Available window scale options.
    /// </summary>
    public static readonly (int Scale, string Label)[] AvailableScales =
    [
        (1, "1x (1536x864)"),
        (2, "2x (3072x1728)"),
    ];

    /// <summary>
    /// Event fired when settings are changed and applied.
    /// </summary>
    public static event Action? OnSettingsChanged;

    /// <summary>
    /// Initializes the settings manager.
    /// Call this after ScaleManager.Initialize().
    /// </summary>
    public static void Initialize(string? settingsPath = null)
    {
        if (settingsPath != null)
            _settingsPath = settingsPath;

        // Try to load existing settings
        if (!Load())
        {
            // If no settings file exists, save defaults
            Save();
        }

        // Note: Don't apply settings here - ScaleManager.Initialize already set up
        // the initial window size. Settings will be applied when user changes them.
    }

    /// <summary>
    /// Saves the current settings to file.
    /// </summary>
    public static bool Save()
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(_current, options);
            File.WriteAllText(_settingsPath, json);
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to save settings: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Loads settings from file.
    /// </summary>
    public static bool Load()
    {
        try
        {
            if (!File.Exists(_settingsPath))
                return false;

            var json = File.ReadAllText(_settingsPath);
            var loaded = JsonSerializer.Deserialize<GameSettings>(json);

            if (loaded != null)
            {
                _current = loaded;
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to load settings: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Sets the window scale and applies it immediately.
    /// </summary>
    public static void SetWindowScale(int scale)
    {
        _current.WindowScale = Math.Clamp(scale, 1, 4);
        ScaleManager.SetScale(_current.WindowScale);
        Save();
        OnSettingsChanged?.Invoke();
    }

    /// <summary>
    /// Gets the current scale index from AvailableScales, or -1 if not found.
    /// </summary>
    public static int GetCurrentScaleIndex()
    {
        for (int i = 0; i < AvailableScales.Length; i++)
        {
            if (AvailableScales[i].Scale == _current.WindowScale)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Sets fullscreen mode and applies it immediately.
    /// </summary>
    public static void SetFullscreen(bool fullscreen)
    {
        _current.Fullscreen = fullscreen;
        ScaleManager.SetFullscreen(fullscreen);
        Save();
        OnSettingsChanged?.Invoke();
    }

    /// <summary>
    /// Toggles fullscreen mode.
    /// </summary>
    public static void ToggleFullscreen()
    {
        SetFullscreen(!_current.Fullscreen);
    }

    /// <summary>
    /// Sets the master volume (0-100).
    /// </summary>
    public static void SetMasterVolume(int volume)
    {
        _current.MasterVolume = Math.Clamp(volume, 0, 100);
        Save();
        OnSettingsChanged?.Invoke();
    }

    /// <summary>
    /// Sets the music volume (0-100).
    /// </summary>
    public static void SetMusicVolume(int volume)
    {
        _current.MusicVolume = Math.Clamp(volume, 0, 100);
        Save();
        OnSettingsChanged?.Invoke();
    }

    /// <summary>
    /// Sets the sound effects volume (0-100).
    /// </summary>
    public static void SetSfxVolume(int volume)
    {
        _current.SfxVolume = Math.Clamp(volume, 0, 100);
        Save();
        OnSettingsChanged?.Invoke();
    }

    /// <summary>
    /// Gets the effective music volume (master * music / 100).
    /// </summary>
    public static float GetEffectiveMusicVolume()
    {
        return (_current.MasterVolume / 100f) * (_current.MusicVolume / 100f);
    }

    /// <summary>
    /// Gets the effective SFX volume (master * sfx / 100).
    /// </summary>
    public static float GetEffectiveSfxVolume()
    {
        return (_current.MasterVolume / 100f) * (_current.SfxVolume / 100f);
    }

    /// <summary>
    /// Applies all settings.
    /// </summary>
    private static void ApplyAllSettings()
    {
        ScaleManager.SetScale(_current.WindowScale);
        ScaleManager.SetFullscreen(_current.Fullscreen);
    }

    /// <summary>
    /// Resets all settings to defaults.
    /// </summary>
    public static void ResetToDefaults()
    {
        _current = new GameSettings();
        ApplyAllSettings();
        Save();
        OnSettingsChanged?.Invoke();
    }
}
