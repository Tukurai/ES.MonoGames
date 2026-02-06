using Helpers;
using Microsoft.Xna.Framework.Graphics;

namespace PocketCardLeague.Consts;

public static class Fonts
{
    public static SpriteFont Default => ContentHelper.LoadFont("DefaultFont");
    public static SpriteFont Header => ContentHelper.LoadFont("HeaderFont");
    public static SpriteFont Title => ContentHelper.LoadFont("TitleFont");

    public static SpriteFont M3Default => ContentHelper.LoadFont("m3Default");
    public static SpriteFont M5Default => ContentHelper.LoadFont("m5Default");
    public static SpriteFont M6Default => ContentHelper.LoadFont("m6Default");

    public const string Jersey10 = "Jersey10-Regular.ttf";
    public const string M3x6 = "m3x6.ttf";
    public const string M5x7 = "m5x7.ttf";
    public const string M6x11 = "m6x11.ttf";
}