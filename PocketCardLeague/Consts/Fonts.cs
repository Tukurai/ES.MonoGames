using Helpers;
using Microsoft.Xna.Framework.Graphics;

namespace PocketCardLeague.Consts;

public static class Fonts
{
    public static SpriteFont Default => ContentHelper.LoadFont("DefaultFont");
    public static SpriteFont Header => ContentHelper.LoadFont("HeaderFont");
    public static SpriteFont Title => ContentHelper.LoadFont("TitleFont");
}