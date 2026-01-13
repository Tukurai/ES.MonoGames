using Microsoft.Xna.Framework;

namespace Components;

public class Border(int thickness = 0, Color? color = null)
{
    public int Thickness { get; set; } = thickness;
    public Color Color { get; set; } = color ?? Color.Transparent;
}
