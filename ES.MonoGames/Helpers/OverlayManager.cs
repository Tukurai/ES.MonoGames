using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Helpers;

/// <summary>
/// Manages overlay UI elements like dropdown lists, popups, and tooltips.
/// Ensures they render on top of other components and block input appropriately.
/// </summary>
public static class OverlayManager
{
    private static readonly List<Rectangle> _inputBlockers = [];
    private static readonly List<Action<SpriteBatch>> _overlayDrawActions = [];

    /// <summary>
    /// Registers a rectangle that should block input to components underneath.
    /// Call this during Update() when an overlay is active.
    /// </summary>
    public static void RegisterInputBlocker(Rectangle rect)
    {
        _inputBlockers.Add(rect);
    }

    /// <summary>
    /// Registers a draw action to be executed after all regular components are drawn.
    /// Call this during Draw() for overlay content.
    /// </summary>
    public static void RegisterOverlayDraw(Action<SpriteBatch> drawAction)
    {
        _overlayDrawActions.Add(drawAction);
    }

    /// <summary>
    /// Checks if a point is inside any registered input blocker.
    /// Components should check this before processing input.
    /// </summary>
    public static bool IsPointBlocked(Point point)
    {
        foreach (var rect in _inputBlockers)
        {
            if (rect.Contains(point))
                return true;
        }
        return false;
    }

    /// <summary>
    /// Checks if a rectangle overlaps with any input blocker.
    /// </summary>
    public static bool IsAreaBlocked(Rectangle area)
    {
        foreach (var rect in _inputBlockers)
        {
            if (rect.Intersects(area))
                return true;
        }
        return false;
    }

    /// <summary>
    /// Draws all registered overlays. Call this after drawing all regular components.
    /// </summary>
    public static void DrawOverlays(SpriteBatch spriteBatch)
    {
        var actions = _overlayDrawActions.ToArray();
        foreach (var drawAction in actions)
            drawAction(spriteBatch);
    }

    /// <summary>
    /// Clears all registered blockers and draw actions.
    /// Call this at the start of each frame's Update().
    /// </summary>
    public static void Clear()
    {
        _inputBlockers.Clear();
        _overlayDrawActions.Clear();
    }

    /// <summary>
    /// Returns true if there are any active overlays.
    /// </summary>
    public static bool HasActiveOverlays => _inputBlockers.Count > 0;
}
