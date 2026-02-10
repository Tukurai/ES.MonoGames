using System.Collections.Generic;

namespace ES.MonoGames.Components.Graphics;

public class Animation
{
    public List<AnimationFrame> AnimationFrames { get; set; }
    public uint StartTileId { get; set; }
    
    public Animation()
    {
        AnimationFrames = [];
        StartTileId = 0;
    }

    public Animation(List<AnimationFrame> animationFrames, uint startTileId)
    {
        AnimationFrames = animationFrames;
        StartTileId = startTileId;
    }
}