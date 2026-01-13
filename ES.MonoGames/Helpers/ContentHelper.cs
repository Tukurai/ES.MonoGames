using Config;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Helpers;

public static class ContentHelper
{
    private static ContentManager _contentManager = null!;
    private static BaseConfig _config = null!;
    private static bool _initialized;

    public static void Initialize(BaseConfig config, ContentManager contentManager)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _contentManager = contentManager ?? throw new ArgumentNullException(nameof(contentManager));
        _initialized = true;
    }

    private static void EnsureInitialized()
    {
        if (!_initialized)
            throw new InvalidOperationException("ContentHelper.Initialize must be called before use.");
    }

    private static string FontPath(string asset) => _config.FontsRoot + asset;
    private static string TexturePath(string asset) => _config.TexturesRoot + asset;
    private static string EffectPath(string asset) => _config.EffectsRoot + asset;
    private static string TrackPath(string asset) => _config.TracksRoot + asset;

    public static SpriteFont LoadFont(string name)
    {
        EnsureInitialized();
        return _contentManager.Load<SpriteFont>(FontPath(name));
    }

    public static Texture2D LoadTexture(string name)
    {
        EnsureInitialized();
        return _contentManager.Load<Texture2D>(TexturePath(name));
    }

    public static SoundEffect LoadEffect(string name)
    {
        EnsureInitialized();
        return _contentManager.Load<SoundEffect>(EffectPath(name));
    }

    public static Song LoadTrack(string name)
    {
        EnsureInitialized();
        return _contentManager.Load<Song>(TrackPath(name));
    }

    public static void PreloadFonts(IEnumerable<string> fontNames)
    {
        EnsureInitialized();
        foreach (var name in fontNames)
            LoadFont(name);
    }

    public static void PreloadTextures(IEnumerable<string> textureNames)
    {
        EnsureInitialized();
        foreach (var name in textureNames)
            LoadTexture(name);
    }

    public static void PreloadEffects(IEnumerable<string> effectNames)
    {
        EnsureInitialized();
        foreach (var name in effectNames)
            LoadEffect(name);
    }

    public static void PreloadTracks(IEnumerable<string> trackNames)
    {
        EnsureInitialized();
        foreach (var name in trackNames)
            LoadTrack(name);
    }

    public static void UnloadAll()
    {
        EnsureInitialized();
        _contentManager.Unload();
    }
}
