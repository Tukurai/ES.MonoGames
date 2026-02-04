using Components;
using System;
using System.Collections.Generic;

namespace Helpers;

public static class SceneManager
{
    private static ISceneManager? _instance;

    public static void Initialize<T>() where T : Enum
    {
        if (_instance != null)
            throw new InvalidOperationException("SceneManager already initialized.");

        _instance = new SceneManagerImpl<T>();
    }

    private static ISceneManager Instance =>
        _instance ?? throw new InvalidOperationException("SceneManager not initialized.");

    public static void AddScene(IScene scene) => Instance.AddScene(scene);
    public static void RemoveScene(IScene scene) => Instance.RemoveScene(scene);
    public static void SetActiveScene(Enum scene) => Instance.SetActiveScene(scene);
    public static void ReinitializeActiveScene() => Instance.ReinitializeActiveScene();
    public static IScene? ActiveScene => Instance.ActiveScene;
}

internal interface ISceneManager
{
    void AddScene(IScene scene);
    void RemoveScene(IScene scene);
    void SetActiveScene(Enum scene);
    void ReinitializeActiveScene();
    IScene? ActiveScene { get; }
}

internal sealed class SceneManagerImpl<T> : ISceneManager where T : Enum
{
    private readonly List<Scene<T>> _scenes = [];
    public IScene? ActiveScene { get; private set; }

    public void AddScene(IScene scene)
        => _scenes.Add((Scene<T>)scene);

    public void RemoveScene(IScene scene)
        => _scenes.Remove((Scene<T>)scene);

    public void SetActiveScene(Enum scene)
        => SetActiveScene((T)scene);

    private void SetActiveScene(T sceneType)
    {
        var scene = _scenes.Find(s => s.Name == sceneType.ToString())
            ?? throw new ArgumentException($"Scene '{sceneType}' not found.");

        ActiveScene?.Stop();
        ActiveScene = scene;
        ActiveScene.Start();

        if (ActiveScene.SceneTrack is not null)
            SoundManager.PlayTrack(ActiveScene.SceneTrack);
    }

    public void ReinitializeActiveScene()
    {
        ActiveScene?.Reinitialize();
    }
}