using Components;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Components;

public enum SceneState
{
    None,
    Initialized,
    Started,
    Paused,
    Stopped,
    Active
}

public enum SceneBackgroundMode
{
    Default,
    Color,
    Sprite
}

public interface IScene
{
    string Name { get; }
    string? SceneTrack { get; set; }
    SceneState State { get; set; }
    SceneBackgroundMode BackgroundMode { get; set; }
    Color BackgroundColor { get; set; }
    TextureResult? BackgroundSprite { get; set; }
    float BackgroundOpacity { get; set; }

    void AddComponent(BaseComponent component);
    void Draw(SpriteBatch spriteBatch);
    void DrawBackground(SpriteBatch spriteBatch);
    void DrawContent(SpriteBatch spriteBatch);
    void Pause();
    void RemoveComponent(BaseComponent component);
    void Reinitialize();
    void Reset();
    void Resume();
    void Start();
    void Stop();
    void Update(GameTime gameTime);

    /// <summary>
    /// Set background properties from XML loader.
    /// </summary>
    void SetBackgroundFromXml(SceneBackgroundMode mode, Color? color, TextureResult? sprite, float opacity);
}

public abstract class Scene<T> : IScene where T : Enum
{
    /// <summary>
    /// The default background sprite used when BackgroundMode is Default.
    /// Set this once at startup from the game project.
    /// </summary>
    public static TextureResult? DefaultBackground { get; set; }

    public string Name { get; set; }
    public virtual string? SceneTrack { get; set; }
    public SceneState State { get; set; } = SceneState.None;
    public SceneBackgroundMode BackgroundMode { get; set; } = SceneBackgroundMode.Color;

    public Color BackgroundColor
    {
        get;
        set { field = value; BackgroundMode = SceneBackgroundMode.Color; }
    } = Color.CornflowerBlue;

    public TextureResult? BackgroundSprite
    {
        get;
        set
        {
            field = value;
            BackgroundMode = value is not null ? SceneBackgroundMode.Sprite : SceneBackgroundMode.Default;
        }
    }

    /// <summary>
    /// Opacity of the background (0f = fully transparent, 1f = fully opaque). Default is 1f.
    /// </summary>
    public float BackgroundOpacity { get; set; } = 1f;

    private bool _pendingReinitialize = false;

    public event Action? OnSceneInitialized;
    public event Action? OnSceneUpdated;
    public event Action? OnSceneDrawn;
    public event Action? OnScenePaused;
    public event Action? OnSceneStopped;
    public event Action? OnSceneResumed;
    public event Action? OnSceneReset;
    public event Action? OnSceneStarted;

    public Scene(T sceneType)
    {
        Name = sceneType.ToString();

        Initialize();
    }

    public virtual void Initialize()
    {
        State = SceneState.Initialized;
        OnSceneInitialized?.Invoke();
    }

    private List<BaseComponent> Components { get; } = [];

    public void AddComponent(BaseComponent component)
        => Components.Add(component);

    public void RemoveComponent(BaseComponent component)
        => Components.Remove(component);

    public virtual void Pause()
    {
        State = SceneState.Paused;
        OnScenePaused?.Invoke();
    }

    public virtual void Stop()
    {
        State = SceneState.Stopped;
        OnSceneStopped?.Invoke();
    }

    public virtual void Resume()
    {
        State = SceneState.Active;
        OnSceneResumed?.Invoke();
    }

    public virtual void Reset()
    {
        State = SceneState.Initialized;
        OnSceneReset?.Invoke();
    }

    /// <summary>
    /// Schedules the scene to reinitialize on the next frame.
    /// This is deferred to avoid modifying the component collection during iteration.
    /// </summary>
    public virtual void Reinitialize()
    {
        _pendingReinitialize = true;
    }

    /// <summary>
    /// Performs the actual reinitialization (clears components and re-runs Initialize).
    /// </summary>
    private void PerformReinitialize()
    {
        Components.Clear();
        Initialize();
    }

    public virtual void Start()
    {
        State = SceneState.Started;
        OnSceneStarted?.Invoke();
    }

    public virtual void Update(GameTime gameTime)
    {
        // Handle deferred reinitialization
        if (_pendingReinitialize)
        {
            _pendingReinitialize = false;
            PerformReinitialize();
            return; // Skip this frame's update after reinitializing
        }

        // Clear overlay and tooltip registrations from previous frame
        OverlayManager.Clear();
        ToolTipManager.Clear();

        Components.ForEach(c => c.Update(gameTime));
        OnSceneUpdated?.Invoke();
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        DrawBackground(spriteBatch);
        DrawContent(spriteBatch);
    }

    public void DrawContent(SpriteBatch spriteBatch)
    {
        Components.ForEach(c => c.Draw(spriteBatch));

        // Draw overlays (dropdowns, popups) on top of everything
        OverlayManager.DrawOverlays(spriteBatch);

        // Draw tooltip on top of overlays
        ToolTipManager.Draw(spriteBatch);

        OnSceneDrawn?.Invoke();
    }

    public void DrawBackground(SpriteBatch spriteBatch)
    {
        var dest = new Rectangle(0, 0, ScaleManager.VirtualWidth, ScaleManager.VirtualHeight);

        switch (BackgroundMode)
        {
            case SceneBackgroundMode.Sprite when BackgroundSprite is not null:
                spriteBatch.GraphicsDevice.Clear(BackgroundColor);
                DrawTextureResult(spriteBatch, BackgroundSprite, dest, BackgroundOpacity);
                break;

            case SceneBackgroundMode.Default when DefaultBackground is not null:
                spriteBatch.GraphicsDevice.Clear(BackgroundColor);
                DrawTextureResult(spriteBatch, DefaultBackground, dest, BackgroundOpacity);
                break;

            default:
                spriteBatch.GraphicsDevice.Clear(BackgroundColor * BackgroundOpacity);
                break;
        }
    }

    private static void DrawTextureResult(SpriteBatch spriteBatch, TextureResult texture, Rectangle dest, float opacity)
    {
        var sourceRect = new Rectangle(
            texture.AtlasEntry.FrameX,
            texture.AtlasEntry.FrameY,
            texture.AtlasEntry.FrameWidth,
            texture.AtlasEntry.FrameHeight);

        spriteBatch.Draw(texture.Texture, dest, sourceRect, Color.White * opacity);
    }

    /// <summary>
    /// Set background properties from XML loader.
    /// </summary>
    public void SetBackgroundFromXml(SceneBackgroundMode mode, Color? color, TextureResult? sprite, float opacity)
    {
        BackgroundMode = mode;
        if (color.HasValue)
            BackgroundColor = color.Value;
        if (sprite is not null)
            BackgroundSprite = sprite;
        BackgroundOpacity = opacity;
    }
}
