using Microsoft.Xna.Framework;
using System;

namespace Components;

/// <summary>
/// Represents a position in 2D space that can be defined relative to another anchor point.
/// </summary>
/// <remarks>Anchors can be chained to create positions relative to other anchors, allowing for flexible
/// hierarchical positioning. The final position is calculated by recursively adding the positions of all relative
/// anchors.</remarks>
/// <param name="position">The absolute or local position of the anchor in 2D space.</param>
/// <param name="relative">An optional anchor to which this anchor's position is relative. If null, the position is absolute.</param>
public class Anchor(Vector2 position, Anchor? relative = null)
{
    private Vector2 _position { get; set; } = position;
    private Anchor? _relative { get; set; } = relative;

    /// <summary>
    /// Returns the absolute position as a <see cref="Vector2"/> value.
    /// </summary>
    /// <returns>A <see cref="Vector2"/> representing the sum of this instance's position and any relative positions. If there
    /// are no relative positions, returns this instance's position.</returns>
    public Vector2 GetVector2() 
        => _relative is not null ? _position + _relative.GetVector2() : _position;

    /// <summary>
    /// Gets the current anchor, if one is set.
    /// </summary>
    /// <returns>The current <see cref="Anchor"/> if available; otherwise, <see langword="null"/>.</returns>
    public Anchor? GetAnchor() 
        => _relative;

    /// <summary>
    /// Adds or changes the relative anchor for this instance.
    /// </summary>
    /// <param name="newPosition"></param>
    public void TransformPosition(Vector2 transformation)
        => _position += transformation;

    internal void UpdateRelative(Vector2 newRelative) 
        => _position = newRelative;
}
