using System.Collections.Generic;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Components;

public enum TextAlignment
{
    Left,
    Center,
    Right
}

public class PixelLabel : BaseComponent
{
    private readonly SpriteAtlas _atlas;
    private readonly Dictionary<char, string> _charMap;

    public string Text { get; set; } = "";
    public Color Tint { get; set; } = Color.White;
    public Vector2 CharScale { get; set; } = Vector2.One;
    public int Spacing { get; set; } = 0;
    public TextAlignment Alignment { get; set; } = TextAlignment.Left;
    public int MaxWidth { get; set; } = 0;

    // Cache the default char width from the atlas for space characters
    private int _defaultCharWidth = 7;

    public PixelLabel(string? name, SpriteAtlas atlas, Dictionary<char, string> charMap,
        Anchor? position = null) : base(name, position)
    {
        _atlas = atlas;
        _charMap = charMap;

        // Determine default char width from first entry in the atlas
        foreach (var mapped in atlas.Atlas)
        {
            if (mapped.SpriteAtlas.Count > 0)
            {
                _defaultCharWidth = mapped.SpriteAtlas[0].FrameWidth;
                break;
            }
        }
    }

    public float MeasureText()
    {
        if (string.IsNullOrEmpty(Text)) return 0;

        float totalWidth = 0;
        for (int i = 0; i < Text.Length; i++)
        {
            var c = Text[i];
            if (c == ' ')
            {
                totalWidth += (_defaultCharWidth - 1) * CharScale.X;
            }
            else if (_charMap.TryGetValue(c, out var entryName))
            {
                var result = _atlas.GetTextureFromAtlas(entryName);
                if (result != null)
                    totalWidth += (result.AtlasEntry.FrameWidth - 1) * CharScale.X;
                else
                    totalWidth += (_defaultCharWidth - 1) * CharScale.X;
            }

            if (i < Text.Length - 1)
                totalWidth += Spacing;
        }

        return totalWidth;
    }

    public override void Initialize()
    {
        if (_atlas == null)
        {
            base.Initialize();
            return;
        }

        var width = MeasureText();
        var height = _defaultCharWidth * CharScale.Y; // approximate
        // Try to get actual height from first atlas entry
        foreach (var mapped in _atlas.Atlas)
        {
            if (mapped.SpriteAtlas.Count > 0)
            {
                height = mapped.SpriteAtlas[0].UntrimmedHeight * CharScale.Y;
                break;
            }
        }

        Size = new Vector2(width, height);
        base.Initialize();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (string.IsNullOrEmpty(Text))
        {
            base.Draw(spriteBatch);
            return;
        }

        var pos = Position.GetVector2();
        var totalWidth = MeasureText();

        float X = Alignment switch
        {
            TextAlignment.Center => (MaxWidth - totalWidth) / 2f,
            TextAlignment.Right => MaxWidth - totalWidth,
            _ => 0
        };

        float drawX = pos.X + X;
        float drawY = pos.Y;

        for (int i = 0; i < Text.Length; i++)
        {
            var c = Text[i];

            if (c == ' ')
            {
                drawX += (_defaultCharWidth - 1) * CharScale.X;
                if (i < Text.Length - 1) drawX += Spacing;
                continue;
            }

            if (!_charMap.TryGetValue(c, out var entryName))
            {
                drawX += (_defaultCharWidth - 1) * CharScale.X;
                if (i < Text.Length - 1) drawX += Spacing;
                continue;
            }

            var result = _atlas.GetTextureFromAtlas(entryName);
            if (result == null)
            {
                drawX += (_defaultCharWidth - 1) * CharScale.X;
                if (i < Text.Length - 1) drawX += Spacing;
                continue;
            }

            var entry = result.AtlasEntry;
            var sourceRect = new Rectangle(entry.FrameX, entry.FrameY, entry.FrameWidth, entry.FrameHeight);

            spriteBatch.Draw(
                result.Texture,
                new Vector2(drawX, drawY),
                sourceRect,
                Tint,
                0f,
                Vector2.Zero,
                CharScale,
                SpriteEffects.None,
                0f
            );

            drawX += (entry.FrameWidth - 1) * CharScale.X;
            if (i < Text.Length - 1) drawX += Spacing;
        }

        base.Draw(spriteBatch);
    }

    /// <summary>
    /// Creates a character map for the Pixelfont sprite atlas.
    /// </summary>
    public static Dictionary<char, string> CreatePixelfontMap()
    {
        var map = new Dictionary<char, string>();

        // Lowercase letters: atlas names are "_a" through "_z"
        for (char c = 'a'; c <= 'z'; c++)
            map[c] = $"_{c}";

        // Uppercase letters: atlas names are "a" through "z" (no underscore)
        for (char c = 'A'; c <= 'Z'; c++)
            map[c] = char.ToLower(c).ToString();

        // Digits
        for (char c = '0'; c <= '9'; c++)
            map[c] = c.ToString();

        // Symbols
        map['.'] = "dot";
        map[','] = "comma";
        map[':'] = "colon";
        map['!'] = "exclaim";
        map['?'] = "question";
        map['*'] = "asterisk";
        map['+'] = "plus";
        map['-'] = "minus";
        map['='] = "equals_sign";
        map['/'] = "forward_slash";
        map['\\'] = "backward_slash";
        map['%'] = "percentage";
        map['('] = "round_bracket_open";
        map[')'] = "round_bracket_close";
        map['\''] = "single_quote";
        map['"'] = "double_quote";

        return map;
    }

    /// <summary>
    /// Creates a character map for the Scoreboard sprite atlas.
    /// </summary>
    public static Dictionary<char, string> CreateScoreboardMap()
    {
        var map = new Dictionary<char, string>();

        for (char c = '0'; c <= '9'; c++)
            map[c] = c.ToString();

        map['/'] = "slash";

        return map;
    }
}
