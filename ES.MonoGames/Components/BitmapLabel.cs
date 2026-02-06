using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace Components;

/// <summary>
/// A label that renders text using System.Drawing without anti-aliasing,
/// resulting in crisp, pixel-perfect text suitable for pixel art games.
/// </summary>
[SupportedOSPlatform("windows")]
public class BitmapLabel : BaseComponent, IDisposable
{
    private GraphicsDevice? _graphicsDevice;
    private Texture2D? _texture;
    private Font? _font;
    private bool _isDirty = true;

    private string _text = "";
    private string _fontFamily = "Arial";
    private float _fontSize = 12f;
    private FontStyle _fontStyle = FontStyle.Regular;

    /// <summary>
    /// The text to display.
    /// </summary>
    public string Text
    {
        get => _text;
        set
        {
            if (_text != value)
            {
                _text = value;
                _isDirty = true;
            }
        }
    }

    /// <summary>
    /// The font family name (e.g., "Arial", "Consolas", or path to a .ttf file).
    /// </summary>
    public string FontFamily
    {
        get => _fontFamily;
        set
        {
            if (_fontFamily != value)
            {
                _fontFamily = value;
                _font?.Dispose();
                _font = null;
                _isDirty = true;
            }
        }
    }

    /// <summary>
    /// The font size in points.
    /// </summary>
    public float FontSize
    {
        get => _fontSize;
        set
        {
            if (Math.Abs(_fontSize - value) > 0.01f)
            {
                _fontSize = value;
                _font?.Dispose();
                _font = null;
                _isDirty = true;
            }
        }
    }

    /// <summary>
    /// The font style (Regular, Bold, Italic, etc.).
    /// </summary>
    public FontStyle FontStyle
    {
        get => _fontStyle;
        set
        {
            if (_fontStyle != value)
            {
                _fontStyle = value;
                _font?.Dispose();
                _font = null;
                _isDirty = true;
            }
        }
    }

    /// <summary>
    /// Tint color applied to the text.
    /// </summary>
    public Color Tint { get; set; } = Color.White;

    /// <summary>
    /// The color of the text when rendered (before tinting).
    /// </summary>
    public Color TextColor { get; set; } = Color.White;

    /// <summary>
    /// Background color behind the text. Transparent by default.
    /// </summary>
    public Color Background { get; set; } = Color.Transparent;

    /// <summary>
    /// Border (outline) around each character for readability.
    /// </summary>
    public Border Border { get; set; } = new Border();

    /// <summary>
    /// Optional padding around the text.
    /// </summary>
    public int Padding { get; set; } = 0;

    public BitmapLabel(string? name = null, Anchor? position = null) : base(name, position)
    {
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _graphicsDevice ??= spriteBatch.GraphicsDevice;

        if (_isDirty || _texture is null)
            RegenerateTexture();

        if (_texture is not null)
        {
            var pos = Position.GetVector2();

            // Draw border (outline) at screen-space offsets, matching Label behavior
            if (Border.Thickness > 0 && Border.Color.A > 0)
            {
                var borderColor = ApplyOpacity(Border.Color);
                for (int x = -Border.Thickness; x <= Border.Thickness; x++)
                {
                    for (int y = -Border.Thickness; y <= Border.Thickness; y++)
                    {
                        if (x == 0 && y == 0)
                            continue;
                        spriteBatch.Draw(_texture, pos + new Vector2(x, y), null, borderColor, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                    }
                }
            }

            // Draw main text with combined TextColor and Tint
            var tint = ApplyOpacity(new Color(
                (TextColor.R * Tint.R) / 255,
                (TextColor.G * Tint.G) / 255,
                (TextColor.B * Tint.B) / 255,
                (TextColor.A * Tint.A) / 255));

            spriteBatch.Draw(_texture, pos, null, tint, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }

        base.Draw(spriteBatch);
    }

    private void RegenerateTexture()
    {
        if (_graphicsDevice is null || string.IsNullOrEmpty(_text))
        {
            _isDirty = false;
            return;
        }

        EnsureFont();
        if (_font is null)
        {
            _isDirty = false;
            return;
        }

        // Use GenericTypographic for proper kerning (removes GDI+ extra padding)
        using var format = new StringFormat(StringFormat.GenericTypographic);
        format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

        // Measure the text
        SizeF textSize;
        using (var measureBitmap = new Bitmap(1, 1))
        using (var measureGraphics = Graphics.FromImage(measureBitmap))
        {
            textSize = measureGraphics.MeasureString(_text, _font, PointF.Empty, format);
        }

        var width = (int)Math.Ceiling(textSize.Width) + Padding * 2;
        var height = (int)Math.Ceiling(textSize.Height) + Padding * 2;

        if (width <= 0 || height <= 0)
        {
            _isDirty = false;
            return;
        }

        // Create bitmap and render text without anti-aliasing
        using var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        using var graphics = Graphics.FromImage(bitmap);

        // Disable all smoothing for pixel-perfect rendering
        graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
        graphics.SmoothingMode = SmoothingMode.None;
        graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
        graphics.PixelOffsetMode = PixelOffsetMode.None;

        // Clear with background color
        var bgColor = System.Drawing.Color.FromArgb(Background.A, Background.R, Background.G, Background.B);
        graphics.Clear(bgColor);

        // Render white text so SpriteBatch tinting can apply TextColor and Border.Color at draw time
        using var brush = new SolidBrush(System.Drawing.Color.White);
        graphics.DrawString(_text, _font, brush, Padding, Padding, format);

        // Convert bitmap to Texture2D
        _texture?.Dispose();
        _texture = BitmapToTexture2D(bitmap);

        Size = new Vector2(width, height);
        _isDirty = false;
    }

    private void EnsureFont()
    {
        if (_font is not null)
            return;

        try
        {
            var fontPath = ResolveFontPath(_fontFamily);

            if (!string.IsNullOrEmpty(fontPath) && System.IO.File.Exists(fontPath))
            {
                var collection = new PrivateFontCollection();
                collection.AddFontFile(fontPath);
                _font = new Font(collection.Families[0], _fontSize, _fontStyle, GraphicsUnit.Pixel);
            }
            else
            {
                // Use as system font family name
                _font = new Font(_fontFamily, _fontSize, _fontStyle, GraphicsUnit.Pixel);
            }
        }
        catch
        {
            // Fallback to Arial
            _font = new Font("Arial", _fontSize, _fontStyle, GraphicsUnit.Pixel);
        }
    }

    /// <summary>
    /// Resolves a font name or path to an actual file path.
    /// Checks: 1) Direct path, 2) Content/fonts/{name}.ttf, 3) Content/fonts/{name}
    /// </summary>
    private static string? ResolveFontPath(string fontFamily)
    {
        // Already a valid path?
        if (System.IO.File.Exists(fontFamily))
            return fontFamily;

        // Try Content/fonts folder
        var basePath = System.IO.Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Content", "fonts");

        // Try with .ttf extension
        var withExtension = System.IO.Path.Combine(basePath, fontFamily + ".ttf");
        if (System.IO.File.Exists(withExtension))
            return withExtension;

        // Try as-is (might already have extension)
        var asIs = System.IO.Path.Combine(basePath, fontFamily);
        if (System.IO.File.Exists(asIs))
            return asIs;

        return null;
    }

    private Texture2D BitmapToTexture2D(Bitmap bitmap)
    {
        var width = bitmap.Width;
        var height = bitmap.Height;

        // Lock the bitmap data
        var rect = new System.Drawing.Rectangle(0, 0, width, height);
        var bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

        // Copy pixel data
        var pixelCount = width * height;
        var pixels = new byte[pixelCount * 4];
        Marshal.Copy(bitmapData.Scan0, pixels, 0, pixels.Length);

        bitmap.UnlockBits(bitmapData);

        // Convert BGRA to RGBA (System.Drawing uses BGRA, MonoGame uses RGBA)
        for (int i = 0; i < pixelCount; i++)
        {
            var offset = i * 4;
            (pixels[offset], pixels[offset + 2]) = (pixels[offset + 2], pixels[offset]);
        }

        // Create texture
        var texture = new Texture2D(_graphicsDevice!, width, height);
        texture.SetData(pixels);

        return texture;
    }

    /// <summary>
    /// Force regeneration of the texture on next draw.
    /// </summary>
    public void Invalidate()
    {
        _isDirty = true;
    }

    public void Dispose()
    {
        _texture?.Dispose();
        _font?.Dispose();
        _texture = null;
        _font = null;
        GC.SuppressFinalize(this);
    }
}
