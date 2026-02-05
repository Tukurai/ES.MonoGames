using Config;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Helpers;

public static class ContentHelper
{
    private static ContentManager _contentManager = null!;
    private static BaseConfig _config = null!;
    private static bool _initialized;

    // Cache for scaled fonts: "FontName" -> { scale -> SpriteFont }
    private static readonly Dictionary<string, Dictionary<int, SpriteFont>> _scaledFonts = new();

    // List of font names that have scaled versions
    private static readonly List<string> _scaledFontNames = new();

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

    // ***

    public static void RegisterAtlas<T>() where T : SpriteAtlas, new()
    {
        EnsureInitialized();
        if (!AtlasesCache.ContainsKey(typeof(T).Name))
        {
            AtlasesCache[typeof(T).Name] = new T();
        }
    }

    private static Dictionary<string, SpriteAtlas> AtlasesCache { get; } = [];

    public static TextureResult? GetTextureResult<T>(string name) where T : SpriteAtlas, new()
    {
        EnsureInitialized();
        if (!AtlasesCache.TryGetValue(typeof(T).Name, out var atlas))
        {
            // Lazy load the atlas instance for type T
            atlas = Activator.CreateInstance<T>()!;
            AtlasesCache[typeof(T).Name] = atlas;
        }
        return atlas.GetTextureFromAtlas(name);
    }

    // ***

    /// <summary>
    /// Loads a font without scale suffix (for non-scaled fonts).
    /// </summary>
    public static SpriteFont LoadFont(string name)
    {
        EnsureInitialized();
        return _contentManager.Load<SpriteFont>(FontPath(name));
    }

    /// <summary>
    /// Loads a scaled font based on the current window scale.
    /// Font files should be named: {name}_x1, {name}_x2, {name}_x3, {name}_x4
    /// </summary>
    public static SpriteFont LoadScaledFont(string name)
    {
        EnsureInitialized();
        var scale = SettingsManager.Current?.WindowScale ?? (int)ScaleManager.Scale;
        return LoadFontAtScale(name, scale);
    }

    /// <summary>
    /// Loads a font at a specific scale.
    /// </summary>
    public static SpriteFont LoadFontAtScale(string name, int scale)
    {
        EnsureInitialized();

        // Check cache first
        if (_scaledFonts.TryGetValue(name, out var scaleDict) && scaleDict.TryGetValue(scale, out var cachedFont))
        {
            return cachedFont;
        }

        // Load the font with scale suffix
        var scaledName = $"{name}_x{scale}";
        var font = _contentManager.Load<SpriteFont>(FontPath(scaledName));

        // Cache it
        if (!_scaledFonts.ContainsKey(name))
        {
            _scaledFonts[name] = new Dictionary<int, SpriteFont>();
        }
        _scaledFonts[name][scale] = font;

        return font;
    }

    /// <summary>
    /// Preloads all scale variants (1x-4x) of the specified fonts.
    /// Call this during initialization to avoid loading delays during gameplay.
    /// </summary>
    public static void PreloadScaledFonts(IEnumerable<string> fontNames)
    {
        EnsureInitialized();
        foreach (var name in fontNames)
        {
            if (!_scaledFontNames.Contains(name))
            {
                _scaledFontNames.Add(name);
            }

            // Preload all 4 scales
            for (int scale = 1; scale <= 4; scale++)
            {
                LoadFontAtScale(name, scale);
            }
        }
    }

    /// <summary>
    /// Gets all registered scaled font names.
    /// </summary>
    public static IReadOnlyList<string> ScaledFontNames => _scaledFontNames;

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

    public static SoundEffect LoadTrack(string name)
    {
        EnsureInitialized();
        return _contentManager.Load<SoundEffect>(TrackPath(name));
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
