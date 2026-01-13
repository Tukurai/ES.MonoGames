using Components;
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

public interface IScene
{
    string Name { get; }
    SceneState State { get; set; }
    Color BackgroundColor { get; set; }

    void AddComponent(BaseComponent component);
    void Draw(SpriteBatch spriteBatch);
    void Pause();
    void RemoveComponent(BaseComponent component);
    void Reset();
    void Resume();
    void Start();
    void Stop();
    void Update(GameTime gameTime);
}

public abstract class Scene<T> : IScene where T : Enum
{
    public string Name { get; set; }
    public SceneState State { get; set; } = SceneState.None;
    public Color BackgroundColor { get; set; } = Color.CornflowerBlue;


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

    public virtual void Start()
    {
        State = SceneState.Started;
        OnSceneStarted?.Invoke();
    }

    public virtual void Update(GameTime gameTime)
    {
        Components.ForEach(c => c.Update(gameTime));
        OnSceneUpdated?.Invoke();
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.GraphicsDevice.Clear(BackgroundColor);

        Components.ForEach(c => c.Draw(spriteBatch));
        OnSceneDrawn?.Invoke();
    }
}
