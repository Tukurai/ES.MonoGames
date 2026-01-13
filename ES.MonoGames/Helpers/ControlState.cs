using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

    private static CursorType Cursor { get; set; } = CursorType.Arrow;

    public static void Update(GameTime gameTime)
    {
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
        var mousePos = new Point(_currentMouse.X, _currentMouse.Y);
        return area.Contains(mousePos);
    }

    public static (Point Previous, Point Current) GetMouseDelta()
    {
        var previousPos = new Point(_previousMouse.X, _previousMouse.Y);
        var currentPos = new Point(_currentMouse.X, _currentMouse.Y);
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

    public static Texture2D? CursorTexture { get; set; } = null;

    public static void Draw(SpriteBatch spriteBatch)
    {
        if (CursorTexture != null)
        {
            var mousePosition = new Vector2(_currentMouse.X, _currentMouse.Y);
            spriteBatch.Draw(CursorTexture, mousePosition, Color.White);
        }
    }
}
