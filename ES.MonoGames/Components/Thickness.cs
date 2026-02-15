namespace Components;

/// <summary>
/// Represents padding or margin with per-side values (left, top, right, bottom).
/// </summary>
public struct Thickness(int left, int top, int right, int bottom)
{
    public int Left { get; set; } = left;
    public int Top { get; set; } = top;
    public int Right { get; set; } = right;
    public int Bottom { get; set; } = bottom;

    /// <summary>
    /// Creates a uniform thickness with the same value on all sides.
    /// </summary>
    public Thickness(int uniform) : this(uniform, uniform, uniform, uniform) { }

    /// <summary>
    /// Total horizontal padding (Left + Right).
    /// </summary>
    public readonly int Horizontal => Left + Right;

    /// <summary>
    /// Total vertical padding (Top + Bottom).
    /// </summary>
    public readonly int Vertical => Top + Bottom;

    public static implicit operator Thickness(int uniform) => new(uniform);
}
