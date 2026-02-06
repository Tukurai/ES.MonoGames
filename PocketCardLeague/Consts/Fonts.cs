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
}