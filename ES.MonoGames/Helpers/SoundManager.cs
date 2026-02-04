using Microsoft.Xna.Framework.Audio;

namespace Helpers;

public static class SoundManager
{
    private static SoundEffectInstance? _currentTrack;
    private static string? _currentTrackName;
    private static bool _initialized;

    public static string? CurrentTrackName => _currentTrackName;
    public static bool IsPlaying => _currentTrack is not null && _currentTrack.State == SoundState.Playing;

    public static void Initialize()
    {
        if (_initialized)
            return;

        SettingsManager.OnSettingsChanged += ApplyVolume;
        _initialized = true;
    }

    public static void PlayTrack(string name)
    {
        if (_currentTrackName == name)
            return;

        var sound = ContentHelper.LoadTrack(name);
        if (sound is null)
            return;

        _currentTrack?.Stop();
        _currentTrack?.Dispose();

        _currentTrack = sound.CreateInstance();
        _currentTrackName = name;
        _currentTrack.IsLooped = true;
        _currentTrack.Volume = SettingsManager.GetEffectiveMusicVolume();
        _currentTrack.Play();
    }

    public static void StopTrack()
    {
        _currentTrack?.Stop();
        _currentTrack?.Dispose();
        _currentTrack = null;
        _currentTrackName = null;
    }

    public static void PlayEffect(string name)
    {
        var effect = ContentHelper.LoadEffect(name);
        if (effect is null)
            return;

        effect.Play(SettingsManager.GetEffectiveSfxVolume(), 0f, 0f);
    }

    public static void ApplyVolume()
    {
        if (_currentTrack is not null)
            _currentTrack.Volume = SettingsManager.GetEffectiveMusicVolume();
    }
}
