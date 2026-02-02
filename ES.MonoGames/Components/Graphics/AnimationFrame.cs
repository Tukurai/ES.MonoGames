using System;

namespace ES.MonoGames.Components.Graphics;

public class AnimationFrame
{
    public TextureRegion TextureRegion { get; set; }
    public TimeSpan Delay { get; set; }
    
    public AnimationFrame()
    {
        TextureRegion = new TextureRegion();
        Delay = TimeSpan.FromMilliseconds(100);
    }

    public AnimationFrame(TextureRegion region, TimeSpan delay)
    {
        TextureRegion = region;
        Delay = delay;
    }
}