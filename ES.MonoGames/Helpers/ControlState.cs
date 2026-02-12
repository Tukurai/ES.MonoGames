using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Helpers;

public enum MouseButton
{
    Left,
    Middle,
    Right
}

public enum CursorType
{
    Arrow,
    Pointer,
    Grab
}

public static class ControlState
{
    private static KeyboardState _previousKeyboard;
    private static KeyboardState _currentKeyboard;

    private static GamePadState _previousGamePad;
    private static GamePadState _currentGamePad;

    private static MouseState _previousMouse;
    private static MouseState _currentMouse;

    private static bool _initialized;

    private static CursorType _activeCursorType = CursorType.Arrow;
    private static readonly Dictionary<CursorType, (TextureResult Texture, Vector2 Origin)> _cursors = [];

    /// <summary>
    /// Scale applied to cursor sprites when drawing.
    /// </summary>
    public static Vector2 CursorScale { get; set; } = new Vector2(4, 4);

    /// <summary>
    /// Offset applied to mouse position for hit detection.
    /// Used by ScrollPanel to adjust child component hit detection.
    /// </summary>
    public static Vector2 MouseOffset { get; set; } = Vector2.Zero;

    /// <summary>
    /// Registers cursor sprites for each cursor type with optional per-cursor origin offsets.
    /// Origins are in sprite-pixel coordinates (scaled by CursorScale when drawn).
    /// </summary>
    public static void RegisterCursors(
        TextureResult arrow, TextureResult pointer, TextureResult grab,
        Vector2? arrowOrigin = null, Vector2? pointerOrigin = null, Vector2? grabOrigin = null)
    {
        _cursors[CursorType.Arrow] = (arrow, arrowOrigin ?? Vector2.Zero);
        _cursors[CursorType.Pointer] = (pointer, pointerOrigin ?? Vector2.Zero);
        _cursors[CursorType.Grab] = (grab, grabOrigin ?? Vector2.Zero);
    }

    /// <summary>
    /// Requests a cursor type for this frame. Higher priority types override lower ones.
    /// Call from component Update methods. Resets to Arrow each frame.
    /// </summary>
    public static void RequestCursor(CursorType type)
    {
        if (type > _activeCursorType)
            _activeCursorType = type;
    }

    public static void Update(GameTime gameTime)
    {
        // Reset cursor to Arrow each frame; components will request upgrades
        _activeCursorType = CursorType.Arrow;

        if (!_initialized)
        {
            _currentKeyboard = Keyboard.GetState();
            _currentGamePad = GamePad.GetState(PlayerIndex.One);
            _currentMouse = Mouse.GetState();

            _previousKeyboard = _currentKeyboard;
            _previousGamePad = _currentGamePad;
            _previousMouse = _currentMouse;

            _initialized = true;
            return;
        }

        _previousKeyboard = _currentKeyboard;
        _previousGamePad = _currentGamePad;
        _previousMouse = _currentMouse;

        _currentKeyboard = Keyboard.GetState();
        _currentGamePad = GamePad.GetState(PlayerIndex.One);
        _currentMouse = Mouse.GetState();
    }

    /* ---------- Keyboard ---------- */

    public static Keys[] GetPressedKeys()
    {
        var prev = _previousKeyboard.GetPressedKeys();
        var curr = _currentKeyboard.GetPressedKeys();

        return [.. curr.Where(k => !prev.Contains(k))];
    }

    public static Keys[] GetHeldKeys()
        => _currentKeyboard.GetPressedKeys();

    public static Keys[] GetReleasedKeys()
    {
        var prev = _previousKeyboard.GetPressedKeys();
        var curr = _currentKeyboard.GetPressedKeys();

        return [.. prev.Where(k => !curr.Contains(k))];
    }

    /* ---------- Mouse ---------- */

    public static bool MouseInArea(Rectangle area)
    {
        var mousePos = GetMousePosition();
        return area.Contains(mousePos);
    }

    public static (Point Previous, Point Current) GetMouseDelta()
    {
        // Transform both positions through ScaleManager
        var previousPos = ScaleManager.ScreenToVirtual(new Point(_previousMouse.X, _previousMouse.Y));
        var currentPos = ScaleManager.ScreenToVirtual(new Point(_currentMouse.X, _currentMouse.Y));
        return (previousPos, currentPos);
    }

    public static MouseButton[] GetPressedMouseButtons()
    {
        List<MouseButton> pressed = [];

        if (_previousMouse.LeftButton == ButtonState.Released && _currentMouse.LeftButton == ButtonState.Pressed)
            pressed.Add(MouseButton.Left);

        if (_previousMouse.MiddleButton == ButtonState.Released && _currentMouse.MiddleButton == ButtonState.Pressed)
            pressed.Add(MouseButton.Middle);

        if (_previousMouse.RightButton == ButtonState.Released && _currentMouse.RightButton == ButtonState.Pressed)
            pressed.Add(MouseButton.Right);

        return [.. pressed];
    }

    public static MouseButton[] GetHeldMouseButtons()
    {
        List<MouseButton> held = [];

        if (_currentMouse.LeftButton == ButtonState.Pressed)
            held.Add(MouseButton.Left);

        if (_currentMouse.MiddleButton == ButtonState.Pressed)
            held.Add(MouseButton.Middle);

        if (_currentMouse.RightButton == ButtonState.Pressed)
            held.Add(MouseButton.Right);

        return [.. held];
    }

    public static MouseButton[] GetReleasedMouseButtons()
    {
        List<MouseButton> released = [];

        if (_previousMouse.LeftButton == ButtonState.Pressed && _currentMouse.LeftButton == ButtonState.Released)
            released.Add(MouseButton.Left);

        if (_previousMouse.MiddleButton == ButtonState.Pressed && _currentMouse.MiddleButton == ButtonState.Released)
            released.Add(MouseButton.Middle);

        if (_previousMouse.RightButton == ButtonState.Pressed && _currentMouse.RightButton == ButtonState.Released)
            released.Add(MouseButton.Right);

        return [.. released];
    }

    public static int GetScrollWheelDelta()
    {
        return _currentMouse.ScrollWheelValue - _previousMouse.ScrollWheelValue;
    }

    /// <summary>
    /// Gets mouse position transformed to virtual coordinates with offset applied.
    /// </summary>
    public static Point GetMousePosition()
    {
        // Transform screen coordinates to virtual coordinates
        var virtualPos = ScaleManager.ScreenToVirtual(new Point(_currentMouse.X, _currentMouse.Y));
        return new Point(
            virtualPos.X + (int)MouseOffset.X,
            virtualPos.Y + (int)MouseOffset.Y
        );
    }

    /// <summary>
    /// Gets mouse position in virtual coordinates without any offset applied.
    /// Use this for components that handle their own offset (like ScrollPanel).
    /// </summary>
    public static Point GetRawMousePosition()
    {
        return ScaleManager.ScreenToVirtual(new Point(_currentMouse.X, _currentMouse.Y));
    }

    /// <summary>
    /// Gets the actual screen mouse position (not transformed).
    /// </summary>
    public static Point GetScreenMousePosition()
    {
        return new Point(_currentMouse.X, _currentMouse.Y);
    }

    /// <summary>
    /// Whether custom cursors are registered and should be drawn.
    /// </summary>
    public static bool HasCustomCursor => _cursors.Count > 0;

    public static Texture2D? CursorTexture { get; set; } = null;

    /// <summary>
    /// Draws the cursor in virtual-resolution space (inside the render target).
    /// Only used for the legacy CursorTexture fallback.
    /// </summary>
    public static void Draw(SpriteBatch spriteBatch)
    {
        if (HasCustomCursor)
            return;

        if (CursorTexture is not null)
        {
            var mousePosition = ScaleManager.ScreenToVirtual(
                new Point(_currentMouse.X, _currentMouse.Y)).ToVector2();
            spriteBatch.Draw(CursorTexture, mousePosition, Color.White);
        }
    }

    /// <summary>
    /// Draws the custom atlas cursor directly to the screen at screen coordinates.
    /// Call this after EndRender, in its own SpriteBatch pass.
    /// </summary>
    public static void DrawScreenCursor(SpriteBatch spriteBatch)
    {
        if (!_cursors.TryGetValue(_activeCursorType, out var entry))
            return;

        var mousePosition = new Vector2(_currentMouse.X, _currentMouse.Y);

        var sourceRect = new Rectangle(
            entry.Texture.AtlasEntry.FrameX,
            entry.Texture.AtlasEntry.FrameY,
            entry.Texture.AtlasEntry.FrameWidth,
            entry.Texture.AtlasEntry.FrameHeight);

        // Scale relative to window size so cursor stays proportional
        var screenScale = (float)ScaleManager.DestinationRect.Width / ScaleManager.VirtualWidth;
        var finalScale = CursorScale * screenScale;

        spriteBatch.Draw(
            entry.Texture.Texture,
            mousePosition,
            sourceRect,
            Color.White,
            0f,
            entry.Origin,
            finalScale,
            SpriteEffects.None,
            0f);
    }
}
