using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Helpers;

public record AtlasEntry(string Name, int X, int Y, int Width, int Height);
public record MappedTextureAtlas(string Name, Texture2D SpriteSheet, List<AtlasEntry> SpriteAtlas);
public record TextureResult(Texture2D Texture, AtlasEntry AtlasEntry);

public abstract class SpriteAtlas(List<MappedTextureAtlas> atlas)
{
    protected List<MappedTextureAtlas> Atlas { get; } = atlas;

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
            if (entry != null)
                return new TextureResult(mappedAtlas.SpriteSheet, entry);
        }
        return null;
    }
}
