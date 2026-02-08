using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Components;

public class ToolTip
{
    public string Text { get; set; }
    public Color Background { get; set; }
    public Color TextColor { get; set; }
    public SpriteFont? Font { get; set; }
    public int Padding { get; set; }

    public ToolTip(string text, Color? background = null, Color? textColor = null, SpriteFont? font = null, int padding = 8)
    {
        Text = text;
        Background = background ?? new Color(30, 30, 30, 220);
        TextColor = textColor ?? Color.White;
        Font = font;
        Padding = padding;
    }
}
