using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helpers;

/// <summary>
/// Manages game scaling - renders at a virtual resolution then scales to window size.
/// This ensures consistent positioning and fixes fullscreen hitbox issues.
/// </summary>
public static class ScaleManager
{
    private static GraphicsDeviceManager? _graphics;
    private static RenderTarget2D? _renderTarget;
    private static float _scale = 4f;
    private static int _renderScale = 1;
    private static Rectangle _destinationRect;
    private static Matrix _transformMatrix;

    /// <summary>
    /// The virtual/internal resolution width the game is designed for.
    /// </summary>
    public static int VirtualWidth { get; private set; } = 200;

    /// <summary>
    /// The virtual/internal resolution height the game is designed for.
    /// </summary>
    public static int VirtualHeight { get; private set; } = 150;

    /// <summary>
    /// Current window scale factor being applied.
    /// </summary>
    public static float Scale => _scale;

    /// <summary>
    /// Internal render scale for crisp fonts. The render target is this many times
    /// larger than the virtual resolution. Higher values = crisper text but more memory.
    /// </summary>
    public static int RenderScale => _renderScale;

    /// <summary>
    /// The transform matrix to apply when drawing. Scales coordinates from virtual to render target size.
    /// </summary>
    public static Matrix TransformMatrix => _transformMatrix;

    /// <summary>
    /// The render target that the game draws to.
    /// </summary>
    public static RenderTarget2D? RenderTarget => _renderTarget;

    /// <summary>
    /// The destination rectangle where the render target is drawn on screen.
    /// </summary>
    public static Rectangle DestinationRect => _destinationRect;

    /// <summary>
    /// Initializes the scale manager with a virtual resolution.
    /// </summary>
    /// <param name="graphics">The graphics device manager</param>
    /// <param name="virtualWidth">Internal game width</param>
    /// <param name="virtualHeight">Internal game height</param>
    /// <param name="defaultScale">Default window scale multiplier (e.g., 2 for 2x)</param>
    /// <param name="renderScale">Internal render scale for crisp fonts (e.g., 2 means render at 2x virtual resolution)</param>
    public static void Initialize(GraphicsDeviceManager graphics, int virtualWidth, int virtualHeight, float defaultScale = 2f, int renderScale = 1)
    {
        _graphics = graphics;
        VirtualWidth = virtualWidth;
        VirtualHeight = virtualHeight;
        _scale = defaultScale;
        _renderScale = renderScale;

        // Create transform matrix for scaling coordinates
        _transformMatrix = Matrix.CreateScale(_renderScale);

        // Set initial window size (don't call ApplyChanges during init - framework handles it)
        _graphics.PreferredBackBufferWidth = (int)(VirtualWidth * defaultScale);
        _graphics.PreferredBackBufferHeight = (int)(VirtualHeight * defaultScale);

        // Render target and destination rect will be created lazily in BeginRender
    }

    /// <summary>
    /// Creates or recreates the render target at virtual resolution * render scale.
    /// </summary>
    private static void CreateRenderTarget(GraphicsDevice graphicsDevice)
    {
        _renderTarget?.Dispose();
        _renderTarget = new RenderTarget2D(
            graphicsDevice,
            VirtualWidth * _renderScale,
            VirtualHeight * _renderScale,
            false,
            SurfaceFormat.Color,
            DepthFormat.None,
            0,
            RenderTargetUsage.DiscardContents
        );
    }

    /// <summary>
    /// Sets the scale and updates window size accordingly.
    /// </summary>
    public static void SetScale(float scale)
    {
        if (_graphics == null) return;

        _scale = scale;

        if (!_graphics.IsFullScreen)
        {
            _graphics.PreferredBackBufferWidth = (int)(VirtualWidth * scale);
            _graphics.PreferredBackBufferHeight = (int)(VirtualHeight * scale);
            _graphics.ApplyChanges();
        }

        UpdateDestinationRect();
    }

    /// <summary>
    /// Toggles fullscreen mode.
    /// </summary>
    public static void SetFullscreen(bool fullscreen)
    {
        if (_graphics == null) return;

        _graphics.IsFullScreen = fullscreen;

        if (fullscreen)
        {
            // Use native resolution in fullscreen
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }
        else
        {
            // Use scaled resolution in windowed
            _graphics.PreferredBackBufferWidth = (int)(VirtualWidth * _scale);
            _graphics.PreferredBackBufferHeight = (int)(VirtualHeight * _scale);
        }

        _graphics.ApplyChanges();
        UpdateDestinationRect();
    }

    /// <summary>
    /// Toggles fullscreen mode.
    /// </summary>
    public static void ToggleFullscreen()
    {
        if (_graphics != null)
        {
            SetFullscreen(!_graphics.IsFullScreen);
        }
    }

    /// <summary>
    /// Updates the destination rectangle to maintain aspect ratio.
    /// Call this when window size changes.
    /// </summary>
    public static void UpdateDestinationRect()
    {
        if (_graphics == null) return;

        var screenWidth = _graphics.PreferredBackBufferWidth;
        var screenHeight = _graphics.PreferredBackBufferHeight;

        // Calculate scale to fit while maintaining aspect ratio
        var scaleX = (float)screenWidth / VirtualWidth;
        var scaleY = (float)screenHeight / VirtualHeight;
        var fitScale = System.Math.Min(scaleX, scaleY);

        var scaledWidth = (int)(VirtualWidth * fitScale);
        var scaledHeight = (int)(VirtualHeight * fitScale);

        // Center on screen
        var x = (screenWidth - scaledWidth) / 2;
        var y = (screenHeight - scaledHeight) / 2;

        _destinationRect = new Rectangle(x, y, scaledWidth, scaledHeight);
    }

    /// <summary>
    /// Transforms screen coordinates to virtual coordinates.
    /// Use this to convert mouse position for hit detection.
    /// </summary>
    public static Point ScreenToVirtual(Point screenPoint)
    {
        // Offset by destination rect position
        var x = screenPoint.X - _destinationRect.X;
        var y = screenPoint.Y - _destinationRect.Y;

        // Scale down to virtual coordinates
        var scaleX = (float)VirtualWidth / _destinationRect.Width;
        var scaleY = (float)VirtualHeight / _destinationRect.Height;

        return new Point(
            (int)(x * scaleX),
            (int)(y * scaleY)
        );
    }

    /// <summary>
    /// Transforms virtual coordinates to screen coordinates.
    /// </summary>
    public static Point VirtualToScreen(Point virtualPoint)
    {
        var scaleX = (float)_destinationRect.Width / VirtualWidth;
        var scaleY = (float)_destinationRect.Height / VirtualHeight;

        return new Point(
            (int)(virtualPoint.X * scaleX) + _destinationRect.X,
            (int)(virtualPoint.Y * scaleY) + _destinationRect.Y
        );
    }

    /// <summary>
    /// Begins rendering to the virtual render target.
    /// Call this before drawing game content.
    /// </summary>
    public static void BeginRender(GraphicsDevice graphicsDevice)
    {
        // Lazy init render target if needed
        if (_renderTarget == null || _renderTarget.IsDisposed)
        {
            CreateRenderTarget(graphicsDevice);
        }

        // Update destination rect based on actual viewport
        if (_destinationRect.Width == 0 || _destinationRect.Height == 0)
        {
            var viewport = graphicsDevice.Viewport;
            var scaleX = (float)viewport.Width / VirtualWidth;
            var scaleY = (float)viewport.Height / VirtualHeight;
            var fitScale = System.Math.Min(scaleX, scaleY);

            var scaledWidth = (int)(VirtualWidth * fitScale);
            var scaledHeight = (int)(VirtualHeight * fitScale);

            var x = (viewport.Width - scaledWidth) / 2;
            var y = (viewport.Height - scaledHeight) / 2;

            _destinationRect = new Rectangle(x, y, scaledWidth, scaledHeight);
        }

        graphicsDevice.SetRenderTarget(_renderTarget);
        graphicsDevice.Clear(Color.Black);
    }

    /// <summary>
    /// Ends rendering to the render target and draws it scaled to the screen.
    /// Call this after drawing all game content.
    /// </summary>
    public static void EndRender(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
    {
        // Switch back to drawing to the screen
        graphicsDevice.SetRenderTarget(null);
        graphicsDevice.Clear(Color.Black); // Letterbox color

        // Draw the render target scaled to fit
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        spriteBatch.Draw(_renderTarget, _destinationRect, Color.White);
        spriteBatch.End();
    }

    /// <summary>
    /// Gets whether we're currently in fullscreen mode.
    /// </summary>
    public static bool IsFullscreen => _graphics?.IsFullScreen ?? false;
}
