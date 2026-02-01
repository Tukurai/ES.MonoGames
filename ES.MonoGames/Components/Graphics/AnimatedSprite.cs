using System;
using Microsoft.Xna.Framework;

namespace ES.MonoGames.Components.Graphics;

public class AnimatedSprite : Sprite
{
    private int _currentFrame;
    private TimeSpan _elapsed;
    private Animation _animation;

    public Animation Animation
    {
        get => _animation;
        set
        {
            _animation = value;
            TextureRegion = _animation.AnimationFrames[0].TextureRegion;
        }
    }
    
    public AnimatedSprite() { }

    public AnimatedSprite(Animation animation)
    {
        Animation = animation;
    }

    public void Update(GameTime gameTime)
    {
        if (_currentFrame > _animation.AnimationFrames.Count)
        {
            _currentFrame = 0;
            return;
        }
        
        _elapsed += gameTime.ElapsedGameTime;
        
        if (_elapsed < _animation.AnimationFrames[_currentFrame].Delay)
        {
            return;
        }
        
        _elapsed -= _animation.AnimationFrames[_currentFrame].Delay;
        _currentFrame++;
        
        if (_currentFrame >= _animation.AnimationFrames.Count)
        {
            _currentFrame = 0;
        }
            
        TextureRegion = _animation.AnimationFrames[_currentFrame].TextureRegion;
    }
}