using Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helpers;

public static class TransitionManager
{
    private static GraphicsDevice? _graphicsDevice;
    private static SceneTransition? _transition;
    private static IScene? _oldScene;
    private static IScene? _newScene;
    private static RenderTarget2D? _oldTarget;
    private static RenderTarget2D? _newTarget;
    private static bool _sharedBackground;

    public static bool IsTransitioning => _transition is not null && !_transition.IsComplete;

    public static void Initialize(GraphicsDevice graphicsDevice)
    {
        _graphicsDevice = graphicsDevice;
    }

    public static void StartTransition(IScene oldScene, IScene newScene, SceneTransition transition)
    {
        // If already transitioning, stop the old-old scene
        if (_oldScene is not null && _transition is not null)
            _oldScene.Stop();

        _oldScene = oldScene;
        _newScene = newScene;
        _transition = transition;

        // Check if backgrounds match
        _sharedBackground = BackgroundsMatch(oldScene, newScene);

        if (transition is SlideTransition slide)
            slide.SharedBackground = _sharedBackground;

        EnsureRenderTargets();
    }

    /// <summary>
    /// Starts a fade-in from black for the given scene (no old scene).
    /// Used for initial scene load.
    /// </summary>
    public static void StartFadeIn(IScene scene, float duration = 0.6f)
    {
        _oldScene = null;
        _newScene = scene;
        _transition = new FadeTransition(duration);
        _sharedBackground = false;

        EnsureRenderTargets();
    }

    public static void Update(GameTime gameTime)
    {
        if (_transition is null)
            return;

        _transition.Update(gameTime);

        if (_transition.IsComplete)
        {
            _oldScene?.Stop();
            _oldScene = null;
            _newScene = null;
            _transition = null;
            _sharedBackground = false;
        }
    }

    public static void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        if (_transition is null || _newScene is null)
            return;

        ScaleManager.EnsureInitialized(graphicsDevice);
        EnsureRenderTargets();

        var isSlideShared = _sharedBackground && _transition is SlideTransition;

        // Render old scene to its render target (black if no old scene)
        graphicsDevice.SetRenderTarget(_oldTarget);
        graphicsDevice.Clear(Color.Black);
        if (_oldScene is not null)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            if (isSlideShared)
                _oldScene.DrawContent(spriteBatch);
            else
                _oldScene.Draw(spriteBatch);
            spriteBatch.End();
        }

        // Render new scene to its render target
        graphicsDevice.SetRenderTarget(_newTarget);
        graphicsDevice.Clear(Color.Transparent);
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        if (isSlideShared)
            _newScene.DrawContent(spriteBatch);
        else
            _newScene.Draw(spriteBatch);
        spriteBatch.End();

        // Now composite onto ScaleManager's render target
        graphicsDevice.SetRenderTarget(ScaleManager.RenderTarget);
        graphicsDevice.Clear(Color.Black);

        spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        // If shared background, draw it stationary first
        if (isSlideShared && _oldScene is not null)
            _oldScene.DrawBackground(spriteBatch);

        var destRect = new Rectangle(0, 0, ScaleManager.VirtualWidth, ScaleManager.VirtualHeight);
        _transition.Draw(spriteBatch, graphicsDevice, _oldTarget!, _newTarget!, destRect);

        spriteBatch.End();
    }

    private static void EnsureRenderTargets()
    {
        if (_graphicsDevice is null)
            return;

        var width = ScaleManager.VirtualWidth;
        var height = ScaleManager.VirtualHeight;

        if (_oldTarget is null || _oldTarget.IsDisposed ||
            _oldTarget.Width != width || _oldTarget.Height != height)
        {
            _oldTarget?.Dispose();
            _oldTarget = new RenderTarget2D(_graphicsDevice, width, height);
        }

        if (_newTarget is null || _newTarget.IsDisposed ||
            _newTarget.Width != width || _newTarget.Height != height)
        {
            _newTarget?.Dispose();
            _newTarget = new RenderTarget2D(_graphicsDevice, width, height);
        }
    }

    private static bool BackgroundsMatch(IScene a, IScene b)
    {
        if (a.BackgroundMode != b.BackgroundMode)
            return false;

        return a.BackgroundMode switch
        {
            SceneBackgroundMode.Color => a.BackgroundColor == b.BackgroundColor,
            SceneBackgroundMode.Sprite => a.BackgroundSprite == b.BackgroundSprite,
            SceneBackgroundMode.Default => true,
            _ => false
        };
    }
}
