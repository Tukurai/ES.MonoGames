using System;
using System.Collections.Generic;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Components;

/// <summary>
/// The playback state of an animated sprite.
/// </summary>
public enum AnimationState
{
    Stopped,
    Playing,
    Paused
}

/// <summary>
/// A sprite component that displays a sequence of frames with configurable timing.
/// Supports automatic playback, manual stepping, and various playback controls.
/// </summary>
public class AnimatedSprite : BaseComponent
{
    private readonly List<(Texture2D texture, Rectangle? sourceRect)> _frames = [];
    private int _currentFrameIndex = 0;
    private float _frameTimer = 0f;
    private AnimationState _state = AnimationState.Stopped;

    /// <summary>
    /// The current animation state.
    /// </summary>
    public AnimationState State => _state;

    /// <summary>
    /// The current frame index.
    /// </summary>
    public int CurrentFrameIndex => _currentFrameIndex;

    /// <summary>
    /// Total number of frames in the animation.
    /// </summary>
    public int FrameCount => _frames.Count;

    /// <summary>
    /// Delay between frames in milliseconds.
    /// </summary>
    public float FrameDelayMs { get; set; } = 100f;

    /// <summary>
    /// Whether the animation should loop when it reaches the end.
    /// </summary>
    public bool Loop { get; set; } = true;

    /// <summary>
    /// Whether to play the animation in reverse.
    /// </summary>
    public bool Reverse { get; set; } = false;

    /// <summary>
    /// Tint color applied to the sprite.
    /// </summary>
    public Color Tint { get; set; } = Color.White;

    /// <summary>
    /// Sprite effects (flip horizontal/vertical).
    /// </summary>
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;

    /// <summary>
    /// Layer depth for draw order.
    /// </summary>
    public float LayerDepth { get; set; } = 0f;

    /// <summary>
    /// Origin point for rotation and scaling.
    /// </summary>
    public Vector2 Origin { get; set; } = Vector2.Zero;

    /// <summary>
    /// Whether to auto-start the animation when frames are added.
    /// </summary>
    public bool AutoStart { get; set; } = false;

    // Events
    /// <summary>
    /// Fired when a frame changes.
    /// </summary>
    public event Action<int>? OnFrameChanged;

    /// <summary>
    /// Fired when the animation completes (only when Loop is false).
    /// </summary>
    public event Action? OnAnimationComplete;

    /// <summary>
    /// Fired when the animation loops back to the start.
    /// </summary>
    public event Action? OnAnimationLoop;

    public AnimatedSprite(string? name = null, Anchor? position = null) : base(name, position)
    {
    }

    /// <summary>
    /// Adds a frame from a texture.
    /// </summary>
    public void AddFrame(Texture2D texture, Rectangle? sourceRect = null)
    {
        _frames.Add((texture, sourceRect));
        UpdateSizeFromCurrentFrame();

        if (AutoStart && _frames.Count == 1)
        {
            Play();
        }
    }

    /// <summary>
    /// Adds a frame from a TextureResult (from a SpriteAtlas).
    /// </summary>
    public void AddFrame(TextureResult? result)
    {
        if (result == null) return;

        var sourceRect = new Rectangle(
            result.AtlasEntry.FrameX,
            result.AtlasEntry.FrameY,
            result.AtlasEntry.FrameWidth,
            result.AtlasEntry.FrameHeight
        );

        AddFrame(result.Texture, sourceRect);
    }

    /// <summary>
    /// Adds multiple frames from TextureResults.
    /// </summary>
    public void AddFrames(params TextureResult?[] results)
    {
        foreach (var result in results)
        {
            AddFrame(result);
        }
    }

    /// <summary>
    /// Adds multiple frames from textures.
    /// </summary>
    public void AddFrames(params Texture2D[] textures)
    {
        foreach (var texture in textures)
        {
            AddFrame(texture);
        }
    }

    /// <summary>
    /// Clears all frames.
    /// </summary>
    public void ClearFrames()
    {
        _frames.Clear();
        _currentFrameIndex = 0;
        _frameTimer = 0f;
        _state = AnimationState.Stopped;
    }

    /// <summary>
    /// Starts or resumes the animation.
    /// </summary>
    public void Play()
    {
        if (_frames.Count == 0) return;
        _state = AnimationState.Playing;
    }

    /// <summary>
    /// Pauses the animation at the current frame.
    /// </summary>
    public void Pause()
    {
        if (_state == AnimationState.Playing)
        {
            _state = AnimationState.Paused;
        }
    }

    /// <summary>
    /// Stops the animation and resets to the first frame.
    /// </summary>
    public void Stop()
    {
        _state = AnimationState.Stopped;
        _currentFrameIndex = Reverse ? _frames.Count - 1 : 0;
        _frameTimer = 0f;
        OnFrameChanged?.Invoke(_currentFrameIndex);
    }

    /// <summary>
    /// Resets to the first frame without changing the play state.
    /// </summary>
    public void Reset()
    {
        _currentFrameIndex = Reverse ? _frames.Count - 1 : 0;
        _frameTimer = 0f;
        OnFrameChanged?.Invoke(_currentFrameIndex);
    }

    /// <summary>
    /// Advances to the next frame manually. Useful for input-driven animation.
    /// </summary>
    public void StepForward()
    {
        if (_frames.Count == 0) return;

        var previousIndex = _currentFrameIndex;

        if (Reverse)
        {
            _currentFrameIndex--;
            if (_currentFrameIndex < 0)
            {
                if (Loop)
                {
                    _currentFrameIndex = _frames.Count - 1;
                    OnAnimationLoop?.Invoke();
                }
                else
                {
                    _currentFrameIndex = 0;
                    OnAnimationComplete?.Invoke();
                }
            }
        }
        else
        {
            _currentFrameIndex++;
            if (_currentFrameIndex >= _frames.Count)
            {
                if (Loop)
                {
                    _currentFrameIndex = 0;
                    OnAnimationLoop?.Invoke();
                }
                else
                {
                    _currentFrameIndex = _frames.Count - 1;
                    OnAnimationComplete?.Invoke();
                }
            }
        }

        if (previousIndex != _currentFrameIndex)
        {
            OnFrameChanged?.Invoke(_currentFrameIndex);
        }

        _frameTimer = 0f;
    }

    /// <summary>
    /// Goes back to the previous frame manually.
    /// </summary>
    public void StepBackward()
    {
        if (_frames.Count == 0) return;

        var previousIndex = _currentFrameIndex;

        if (Reverse)
        {
            _currentFrameIndex++;
            if (_currentFrameIndex >= _frames.Count)
            {
                if (Loop)
                {
                    _currentFrameIndex = 0;
                    OnAnimationLoop?.Invoke();
                }
                else
                {
                    _currentFrameIndex = _frames.Count - 1;
                }
            }
        }
        else
        {
            _currentFrameIndex--;
            if (_currentFrameIndex < 0)
            {
                if (Loop)
                {
                    _currentFrameIndex = _frames.Count - 1;
                    OnAnimationLoop?.Invoke();
                }
                else
                {
                    _currentFrameIndex = 0;
                }
            }
        }

        if (previousIndex != _currentFrameIndex)
        {
            OnFrameChanged?.Invoke(_currentFrameIndex);
        }

        _frameTimer = 0f;
    }

    /// <summary>
    /// Jumps to a specific frame.
    /// </summary>
    public void GoToFrame(int frameIndex)
    {
        if (_frames.Count == 0) return;

        var clampedIndex = Math.Clamp(frameIndex, 0, _frames.Count - 1);
        if (_currentFrameIndex != clampedIndex)
        {
            _currentFrameIndex = clampedIndex;
            _frameTimer = 0f;
            OnFrameChanged?.Invoke(_currentFrameIndex);
        }
    }

    private void UpdateSizeFromCurrentFrame()
    {
        if (_frames.Count == 0) return;

        var (texture, sourceRect) = _frames[_currentFrameIndex];
        if (sourceRect.HasValue)
        {
            Size = new Vector2(sourceRect.Value.Width, sourceRect.Value.Height);
        }
        else
        {
            Size = new Vector2(texture.Width, texture.Height);
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_state != AnimationState.Playing || _frames.Count <= 1)
            return;

        _frameTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        if (_frameTimer >= FrameDelayMs)
        {
            _frameTimer -= FrameDelayMs;
            StepForward();
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (_frames.Count == 0) return;

        var (texture, sourceRect) = _frames[_currentFrameIndex];
        var pos = Position.GetVector2();

        spriteBatch.Draw(
            texture,
            pos,
            sourceRect,
            Tint,
            Rotation,
            Origin,
            Scale,
            Effects,
            LayerDepth
        );

        base.Draw(spriteBatch);
    }
}
