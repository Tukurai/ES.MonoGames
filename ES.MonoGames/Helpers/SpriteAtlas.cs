using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Helpers;

public sealed class AtlasEntry(string name, bool rotated, int frameX, int frameY, int frameWidth, int frameHeight, int untrimmedWidth, int untrimmedHeight, float pivotX, float pivotY)
{
    public string Name { get; init; } = name;

    public bool Rotated { get; init; } = rotated;

    // Frame rectangle in the atlas
    public int FrameX { get; init; } = frameX;
    public int FrameY { get; init; } = frameY;
    public int FrameWidth { get; init; } = frameWidth;
    public int FrameHeight { get; init; } = frameHeight;

    // Original (untrimmed) sprite size
    public int UntrimmedWidth { get; init; } = untrimmedWidth;
    public int UntrimmedHeight { get; init; } = untrimmedHeight;
    // Pivot after trimming (usually normalized or pixel-based depending on exporter)
    public float PivotX { get; init; } = pivotX;
    public float PivotY { get; init; } = pivotY;
}

public sealed class MappedTextureAtlas(string textureName)
{
    public string Name { get; init; } = string.Empty;
    public Texture2D? SpriteSheet { get; set; } = ContentHelper.LoadTexture(textureName);
    public string SmartUpdateKey { get; set; } = string.Empty;
    public List<AtlasEntry> SpriteAtlas { get; init; } = [];
}

public record TextureResult(Texture2D Texture, AtlasEntry AtlasEntry);

public abstract class SpriteAtlas()
{
    public List<MappedTextureAtlas> Atlas { get; protected set; } = [];

    /// <summary>
    /// Get a texture from the atlas by name, returns null if not found.
    /// </summary>
    /// <remarks>Searches through all mapped texture atlases for the specified texture name.</remarks>
    /// <param name="name">The name of the texture to retrieve.</param>
    /// <returns>A TextureResult containing the texture and its atlas entry, or null if not found.</returns>
    protected TextureResult? GetTextureFromAtlas(string name)
    {
        foreach (var mappedAtlas in Atlas)
        {
            var entry = mappedAtlas.SpriteAtlas.Find(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (entry != null && mappedAtlas.SpriteSheet != null)
                return new TextureResult(mappedAtlas.SpriteSheet, entry);
        }
        return null;
    }
}
