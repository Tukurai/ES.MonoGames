using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Components;

/// <summary>
/// A container that holds multiple SubScenes and manages transitions between them.
/// Acts like a tabbed container or page view with animated transitions.
/// </summary>
public class SubSceneContainer : BaseComponent
{
    private readonly List<SubScene> _subScenes = [];
    private int _activeIndex = -1;
    private SceneTransition? _transition;
    private int _oldIndex = -1;
    private RenderTarget2D? _oldTarget;
    private RenderTarget2D? _newTarget;
    private RenderTarget2D? _backgroundTarget;
    private GraphicsDevice? _graphicsDevice;

    /// <summary>
    /// Background color of the container (shown when no SubScene is active or during transitions).
    /// </summary>
    public Color Background { get; set; } = Color.Transparent;

    /// <summary>
    /// Border around the container.
    /// </summary>
    public Border Border { get; set; } = new Border();

    /// <summary>
    /// Whether a transition is currently in progress.
    /// </summary>
    public bool IsTransitioning => _transition is not null && !_transition.IsComplete;

    /// <summary>
    /// The currently active SubScene index, or -1 if none.
    /// </summary>
    public int ActiveIndex => _activeIndex;

    /// <summary>
    /// The currently active SubScene, or null if none.
    /// </summary>
    public SubScene? ActiveSubScene => _activeIndex >= 0 && _activeIndex < _subScenes.Count
        ? _subScenes[_activeIndex]
        : null;

    /// <summary>
    /// Number of SubScenes in this container.
    /// </summary>
    public int Count => _subScenes.Count;

    /// <summary>
    /// Event fired when the active SubScene changes (after transition completes).
    /// </summary>
    public event Action<int>? OnSubSceneChanged;

    public SubSceneContainer(string? name = null) : base(name) { }

    /// <summary>
    /// Add a SubScene to the container.
    /// </summary>
    public void AddSubScene(SubScene subScene)
    {
        // Set SubScene size and position to match container
        subScene.Size = Size;
        subScene.Position = Position;
        _subScenes.Add(subScene);

        // If this is the first SubScene, make it active
        if (_subScenes.Count == 1)
        {
            _activeIndex = 0;
            subScene.IsActive = true;
        }
    }

    /// <summary>
    /// Get a SubScene by index.
    /// </summary>
    public SubScene? GetSubScene(int index)
    {
        if (index >= 0 && index < _subScenes.Count)
            return _subScenes[index];
        return null;
    }

    /// <summary>
    /// Get a SubScene by name.
    /// </summary>
    public SubScene? GetSubScene(string name)
    {
        return _subScenes.Find(s => s.Name == name);
    }

    /// <summary>
    /// Set the active SubScene by index with an optional transition.
    /// </summary>
    public void SetActiveSubScene(int index, SceneTransition? transition = null)
    {
        if (index < 0 || index >= _subScenes.Count)
            return;

        if (index == _activeIndex)
            return;

        // If already transitioning, complete immediately
        if (IsTransitioning)
            CompleteTransition();

        _oldIndex = _activeIndex;
        _activeIndex = index;

        if (transition is not null && _oldIndex >= 0)
        {
            _transition = transition;
            // Close any open dropdowns in both scenes before transitioning
            CloseDropdowns(_subScenes[_oldIndex]);
            CloseDropdowns(_subScenes[_activeIndex]);
        }
        else
        {
            // No transition - switch immediately
            if (_oldIndex >= 0)
                _subScenes[_oldIndex].IsActive = false;
            _subScenes[_activeIndex].IsActive = true;
            OnSubSceneChanged?.Invoke(_activeIndex);
        }
    }

    /// <summary>
    /// Set the active SubScene by name with an optional transition.
    /// </summary>
    public void SetActiveSubScene(string name, SceneTransition? transition = null)
    {
        var index = _subScenes.FindIndex(s => s.Name == name);
        if (index >= 0)
            SetActiveSubScene(index, transition);
    }

    /// <summary>
    /// Navigate to the next SubScene with an optional transition.
    /// </summary>
    public void Next(SceneTransition? transition = null)
    {
        if (_subScenes.Count == 0)
            return;
        var nextIndex = (_activeIndex + 1) % _subScenes.Count;
        SetActiveSubScene(nextIndex, transition);
    }

    /// <summary>
    /// Navigate to the previous SubScene with an optional transition.
    /// </summary>
    public void Previous(SceneTransition? transition = null)
    {
        if (_subScenes.Count == 0)
            return;
        var prevIndex = (_activeIndex - 1 + _subScenes.Count) % _subScenes.Count;
        SetActiveSubScene(prevIndex, transition);
    }

    public override void Update(GameTime gameTime)
    {
        // Update transition
        if (_transition is not null)
        {
            _transition.Update(gameTime);

            if (_transition.IsComplete)
                CompleteTransition();
        }

        // Update active SubScene (or both during transition)
        if (IsTransitioning)
        {
            if (_oldIndex >= 0)
                _subScenes[_oldIndex].Update(gameTime);
            if (_activeIndex >= 0)
                _subScenes[_activeIndex].Update(gameTime);
        }
        else if (_activeIndex >= 0)
        {
            _subScenes[_activeIndex].Update(gameTime);
        }

        // Don't call base.Update() - we manage children (SubScenes) ourselves
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Get graphics device for render targets
        _graphicsDevice ??= spriteBatch.GraphicsDevice;

        var pos = Position.GetVector2();
        var containerRect = new Rectangle((int)pos.X, (int)pos.Y, (int)Size.X, (int)Size.Y);

        // Draw container background
        if (Background.A > 0)
        {
            spriteBatch.Draw(
                RendererHelper.WhitePixel,
                containerRect,
                ApplyOpacity(Background)
            );
        }

        if (IsTransitioning && _oldIndex >= 0 && _activeIndex >= 0)
        {
            DrawTransition(spriteBatch, containerRect);
        }
        else if (_activeIndex >= 0)
        {
            // Draw active SubScene directly
            _subScenes[_activeIndex].Draw(spriteBatch);
        }

        // Draw container border (on top of everything)
        if (Border.Thickness > 0)
        {
            var border = EffectiveOpacity < 1f
                ? new Border(Border.Thickness, ApplyOpacity(Border.Color))
                : Border;
            RendererHelper.Draw(spriteBatch, border, pos, Size, Scale);
        }
    }

    private void DrawTransition(SpriteBatch spriteBatch, Rectangle containerRect)
    {
        if (_graphicsDevice is null || _transition is null)
            return;

        EnsureRenderTargets();

        // If render targets aren't ready, fall back to drawing old scene directly
        if (_oldTarget is null || _newTarget is null || _backgroundTarget is null)
        {
            if (_oldIndex >= 0)
                _subScenes[_oldIndex].Draw(spriteBatch);
            return;
        }

        var oldSubScene = _subScenes[_oldIndex];
        var newSubScene = _subScenes[_activeIndex];

        // End current batch - flushes scene background, header, etc. to ScaleManager.RenderTarget
        spriteBatch.End();

        // IMPORTANT: We must copy ScaleManager.RenderTarget IMMEDIATELY after switching,
        // before the GPU has a chance to discard its contents
        _graphicsDevice.SetRenderTarget(_backgroundTarget);
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        spriteBatch.Draw(ScaleManager.RenderTarget, Vector2.Zero, Color.White);
        spriteBatch.End();

        // Set up scissor rectangle to clip content to container bounds
        var previousScissor = _graphicsDevice.ScissorRectangle;
        var scissorState = new RasterizerState { ScissorTestEnable = true };

        // Render old SubScene content to render target with scissor clipping
        _graphicsDevice.SetRenderTarget(_oldTarget);
        _graphicsDevice.Clear(Color.Transparent);
        _graphicsDevice.ScissorRectangle = containerRect;
        spriteBatch.Begin(samplerState: SamplerState.PointClamp, rasterizerState: scissorState);
        oldSubScene.DrawContent(spriteBatch);
        spriteBatch.End();

        // Render new SubScene content to render target with scissor clipping
        _graphicsDevice.SetRenderTarget(_newTarget);
        _graphicsDevice.Clear(Color.Transparent);
        spriteBatch.Begin(samplerState: SamplerState.PointClamp, rasterizerState: scissorState);
        newSubScene.DrawContent(spriteBatch);
        spriteBatch.End();

        // Restore scissor state
        _graphicsDevice.ScissorRectangle = previousScissor;

        // Return to scene render target and restore the saved background with Opaque blend
        // (ensures we fully overwrite any garbage from potential discard)
        _graphicsDevice.SetRenderTarget(ScaleManager.RenderTarget);
        spriteBatch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.Opaque);
        spriteBatch.Draw(_backgroundTarget, Vector2.Zero, Color.White);
        spriteBatch.End();

        // Draw the transition with AlphaBlend so transparent parts show the restored background
        spriteBatch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend);
        _transition.Draw(spriteBatch, _graphicsDevice, _oldTarget!, _newTarget!, containerRect);
        spriteBatch.End();

        // Restart batch for subsequent drawing (footer, etc.)
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);
    }

    private void CompleteTransition()
    {
        if (_oldIndex >= 0 && _oldIndex < _subScenes.Count)
            _subScenes[_oldIndex].IsActive = false;

        if (_activeIndex >= 0 && _activeIndex < _subScenes.Count)
            _subScenes[_activeIndex].IsActive = true;

        _transition = null;
        _oldIndex = -1;

        OnSubSceneChanged?.Invoke(_activeIndex);
    }

    private void EnsureRenderTargets()
    {
        if (_graphicsDevice is null)
            return;

        // Use full screen size so components render at natural positions
        // (components like ScrollPanel do their own Begin/End and won't inherit transform matrices)
        var width = ScaleManager.VirtualWidth;
        var height = ScaleManager.VirtualHeight;

        if (width <= 0 || height <= 0)
            return;

        if (_oldTarget is null || _oldTarget.IsDisposed ||
            _oldTarget.Width != width || _oldTarget.Height != height)
        {
            _oldTarget?.Dispose();
            _oldTarget = new RenderTarget2D(_graphicsDevice, width, height, false,
                SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
        }

        if (_newTarget is null || _newTarget.IsDisposed ||
            _newTarget.Width != width || _newTarget.Height != height)
        {
            _newTarget?.Dispose();
            _newTarget = new RenderTarget2D(_graphicsDevice, width, height, false,
                SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
        }

        if (_backgroundTarget is null || _backgroundTarget.IsDisposed ||
            _backgroundTarget.Width != width || _backgroundTarget.Height != height)
        {
            _backgroundTarget?.Dispose();
            _backgroundTarget = new RenderTarget2D(_graphicsDevice, width, height, false,
                SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
        }
    }

    /// <summary>
    /// Recursively closes all dropdowns in a component tree.
    /// </summary>
    private static void CloseDropdowns(BaseComponent component)
    {
        if (component is Dropdown dropdown)
            dropdown.Close();

        foreach (var child in component.Children)
            CloseDropdowns(child);
    }

    /// <summary>
    /// Clean up render targets when the container is no longer needed.
    /// </summary>
    public void Dispose()
    {
        _oldTarget?.Dispose();
        _newTarget?.Dispose();
        _backgroundTarget?.Dispose();
        _oldTarget = null;
        _newTarget = null;
        _backgroundTarget = null;
    }
}
