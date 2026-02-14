using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Components;

/// <summary>
/// Windows clipboard helper using P/Invoke
/// </summary>
internal static class ClipboardHelper
{
    [DllImport("user32.dll")]
    private static extern bool OpenClipboard(IntPtr hWndNewOwner);

    [DllImport("user32.dll")]
    private static extern bool CloseClipboard();

    [DllImport("user32.dll")]
    private static extern bool EmptyClipboard();

    [DllImport("user32.dll")]
    private static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

    [DllImport("user32.dll")]
    private static extern IntPtr GetClipboardData(uint uFormat);

    [DllImport("user32.dll")]
    private static extern bool IsClipboardFormatAvailable(uint format);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GlobalLock(IntPtr hMem);

    [DllImport("kernel32.dll")]
    private static extern bool GlobalUnlock(IntPtr hMem);

    [DllImport("kernel32.dll")]
    private static extern UIntPtr GlobalSize(IntPtr hMem);

    private const uint CF_UNICODETEXT = 13;
    private const uint GMEM_MOVEABLE = 0x0002;

    public static void SetText(string text)
    {
        if (!OpenClipboard(IntPtr.Zero)) return;

        try
        {
            EmptyClipboard();
            var bytes = (text.Length + 1) * 2;
            var hGlobal = GlobalAlloc(GMEM_MOVEABLE, (UIntPtr)bytes);
            if (hGlobal == IntPtr.Zero) return;

            var target = GlobalLock(hGlobal);
            if (target == IntPtr.Zero) return;

            try
            {
                Marshal.Copy(text.ToCharArray(), 0, target, text.Length);
                Marshal.WriteInt16(target, text.Length * 2, 0);
            }
            finally
            {
                GlobalUnlock(hGlobal);
            }

            SetClipboardData(CF_UNICODETEXT, hGlobal);
        }
        finally
        {
            CloseClipboard();
        }
    }

    public static string? GetText()
    {
        if (!IsClipboardFormatAvailable(CF_UNICODETEXT)) return null;
        if (!OpenClipboard(IntPtr.Zero)) return null;

        try
        {
            var hGlobal = GetClipboardData(CF_UNICODETEXT);
            if (hGlobal == IntPtr.Zero) return null;

            var target = GlobalLock(hGlobal);
            if (target == IntPtr.Zero) return null;

            try
            {
                return Marshal.PtrToStringUni(target);
            }
            finally
            {
                GlobalUnlock(hGlobal);
            }
        }
        finally
        {
            CloseClipboard();
        }
    }

    public static bool ContainsText() => IsClipboardFormatAvailable(CF_UNICODETEXT);
}

public class InputField(
    string? name = null,
    string text = "",
    string placeholderText = "",
    SpriteFont? font = null,
    Anchor? position = null,
    Vector2? size = null) : BaseComponent(name, position, size)
{
    // Static focus tracking - only one InputField can be focused at a time
    private static InputField? _focusedField = null;

    // Text content
    public string Text { get; set; } = text;
    public string PlaceholderText { get; set; } = placeholderText;

    // Appearance
    public Color TextColor { get; set; } = Color.White;
    public Color PlaceholderColor { get; set; } = Color.Gray;
    public Color Background { get; set; } = Color.DarkGray;
    public Border Border { get; set; } = new Border(1, Color.Gray);
    public Border FocusedBorder { get; set; } = new Border(2, Color.White);
    public int Padding { get; set; } = 5;
    public bool QuickDraw { get; set; } = false;

    // Focus state
    public bool IsFocused => _focusedField == this;

    // Cursor state
    private int _cursorPosition = 0;
    private float _cursorBlinkTimer = 0f;
    private bool _cursorVisible = true;
    private const float CursorBlinkRate = 0.5f;
    public Color CursorColor { get; set; } = Color.White;
    public int CursorWidth { get; set; } = 2;

    // Selection state
    private int _selectionStart = -1;
    private int _selectionEnd = -1;
    private bool _isMouseSelecting = false;
    public Color SelectionColor { get; set; } = new Color(0, 120, 215, 150);

    public bool HasSelection => _selectionStart >= 0 && _selectionStart != _selectionEnd;
    private int SelectionMin => Math.Min(_selectionStart, _selectionEnd);
    private int SelectionMax => Math.Max(_selectionStart, _selectionEnd);

    // Scroll state for text overflow
    private float _scrollOffset = 0f;
    private int _verticalScrollOffset = 0; // In lines

    // Multiline support
    public bool Multiline { get; set; } = false;

    // Key repeat state
    private Keys? _heldKey = null;
    private float _keyHoldTimer = 0f;
    private float _keyRepeatTimer = 0f;
    private const float KeyRepeatInitialDelay = 0.4f;  // Delay before repeat starts
    private const float KeyRepeatIntervalStart = 0.1f; // Initial repeat interval
    private const float KeyRepeatIntervalMin = 0.03f;  // Fastest repeat interval
    private const float KeyRepeatAcceleration = 0.5f;  // How fast it accelerates

    // Rasterizer state for scissor clipping
    private static readonly RasterizerState _scissorRasterizer = new() { ScissorTestEnable = true };

    public SpriteFont? Font { get; set; } = font;

    // Events
    public event Action<string>? OnTextChanged;
    public event Action? OnSubmit;
    public event Action? OnFocusGained;
    public event Action? OnFocusLost;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        var mouseInArea = ControlState.MouseInArea(new Rectangle(Position.GetVector2().ToPoint(), Size.ToPoint()));

        if (mouseInArea)
            ControlState.RequestCursor(CursorType.Pointer);
        var mouseDelta = ControlState.GetMouseDelta();

        // Handle mouse input
        if (ControlState.GetPressedMouseButtons().Length != 0)
        {
            if (mouseInArea)
            {
                // Click inside - gain focus and position cursor
                if (_focusedField != this)
                {
                    _focusedField = this;
                    OnFocusGained?.Invoke();
                }

                _cursorPosition = GetCharacterIndexFromX(mouseDelta.Current.X);
                _selectionStart = _cursorPosition;
                _selectionEnd = _cursorPosition;
                _isMouseSelecting = true;
                _cursorVisible = true;
                _cursorBlinkTimer = 0f;
            }
            else if (IsFocused)
            {
                // Click outside - lose focus
                _focusedField = null;
                ClearSelection();
                OnFocusLost?.Invoke();
            }
        }

        // Handle mouse drag selection
        if (_isMouseSelecting && ControlState.GetHeldMouseButtons().Length != 0)
        {
            var newPos = GetCharacterIndexFromX(mouseDelta.Current.X);
            if (newPos != _selectionEnd)
            {
                _selectionEnd = newPos;
                _cursorPosition = newPos;
                _cursorVisible = true;
                _cursorBlinkTimer = 0f;
            }
        }

        if (_isMouseSelecting && ControlState.GetReleasedMouseButtons().Length != 0)
        {
            _isMouseSelecting = false;
            // If start == end, clear selection (was just a click)
            if (_selectionStart == _selectionEnd)
                ClearSelection();
        }

        // Handle keyboard input when focused
        if (IsFocused)
        {
            // Update cursor blink
            _cursorBlinkTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_cursorBlinkTimer >= CursorBlinkRate)
            {
                _cursorBlinkTimer = 0f;
                _cursorVisible = !_cursorVisible;
            }

            var pressedKeys = ControlState.GetPressedKeys();
            var heldKeys = ControlState.GetHeldKeys();
            var shiftHeld = heldKeys.Contains(Keys.LeftShift) || heldKeys.Contains(Keys.RightShift);
            var ctrlHeld = heldKeys.Contains(Keys.LeftControl) || heldKeys.Contains(Keys.RightControl);
            var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Process newly pressed keys
            foreach (var key in pressedKeys)
            {
                ProcessKey(key, shiftHeld, ctrlHeld);

                // Start tracking for repeat if this is a repeatable key
                if (IsRepeatableKey(key))
                {
                    _heldKey = key;
                    _keyHoldTimer = 0f;
                    _keyRepeatTimer = 0f;
                }
            }

            // Handle key repeat for held keys
            if (_heldKey.HasValue)
            {
                if (heldKeys.Contains(_heldKey.Value))
                {
                    _keyHoldTimer += elapsed;

                    if (_keyHoldTimer >= KeyRepeatInitialDelay)
                    {
                        _keyRepeatTimer += elapsed;

                        // Calculate current repeat interval (accelerates over time)
                        float holdTime = _keyHoldTimer - KeyRepeatInitialDelay;
                        float repeatInterval = Math.Max(
                            KeyRepeatIntervalMin,
                            KeyRepeatIntervalStart - (holdTime * KeyRepeatAcceleration)
                        );

                        if (_keyRepeatTimer >= repeatInterval)
                        {
                            _keyRepeatTimer = 0f;
                            ProcessKey(_heldKey.Value, shiftHeld, ctrlHeld);
                        }
                    }
                }
                else
                {
                    // Key was released
                    _heldKey = null;
                }
            }
        }
        else
        {
            // Not focused, clear held key state
            _heldKey = null;
        }
    }

    private static bool IsRepeatableKey(Keys key)
    {
        return key == Keys.Back || key == Keys.Delete ||
               key == Keys.Left || key == Keys.Right ||
               key == Keys.Up || key == Keys.Down ||
               key == Keys.Home || key == Keys.End ||
               KeyToChar(key, false).HasValue; // Character keys
    }

    private void ProcessKey(Keys key, bool shiftHeld, bool ctrlHeld)
    {
        // Reset cursor visibility on any key press
        _cursorVisible = true;
        _cursorBlinkTimer = 0f;

        // Ctrl+A - Select All
        if (ctrlHeld && key == Keys.A)
        {
            _selectionStart = 0;
            _selectionEnd = Text.Length;
            _cursorPosition = Text.Length;
            return;
        }

        // Ctrl+C - Copy
        if (ctrlHeld && key == Keys.C)
        {
            if (HasSelection)
            {
                var selectedText = Text.Substring(SelectionMin, SelectionMax - SelectionMin);
                ClipboardHelper.SetText(selectedText);
            }
            return;
        }

        // Ctrl+X - Cut
        if (ctrlHeld && key == Keys.X)
        {
            if (HasSelection)
            {
                var selectedText = Text.Substring(SelectionMin, SelectionMax - SelectionMin);
                ClipboardHelper.SetText(selectedText);
                DeleteSelection();
            }
            return;
        }

        // Ctrl+V - Paste
        if (ctrlHeld && key == Keys.V)
        {
            var clipText = ClipboardHelper.GetText();
            if (!string.IsNullOrEmpty(clipText))
            {
                InsertTextAtCursor(clipText);
            }
            return;
        }

        switch (key)
        {
            case Keys.Escape:
                _focusedField = null;
                ClearSelection();
                OnFocusLost?.Invoke();
                break;
            case Keys.Enter:
                if (Multiline)
                {
                    InsertTextAtCursor("\n");
                }
                else
                {
                    OnSubmit?.Invoke();
                }
                break;
            case Keys.Back:
                if (HasSelection)
                {
                    DeleteSelection();
                }
                else if (_cursorPosition > 0)
                {
                    Text = Text.Remove(_cursorPosition - 1, 1);
                    _cursorPosition--;
                    OnTextChanged?.Invoke(Text);
                }
                break;
            case Keys.Delete:
                if (HasSelection)
                {
                    DeleteSelection();
                }
                else if (_cursorPosition < Text.Length)
                {
                    Text = Text.Remove(_cursorPosition, 1);
                    OnTextChanged?.Invoke(Text);
                }
                break;
            case Keys.Left:
                {
                    if (ctrlHeld)
                    {
                        // Word jump
                        var newPos = FindPreviousWordBoundary(_cursorPosition);
                        if (shiftHeld)
                        {
                            if (!HasSelection) _selectionStart = _cursorPosition;
                            _selectionEnd = newPos;
                        }
                        else ClearSelection();
                        _cursorPosition = newPos;
                    }
                    else if (shiftHeld)
                    {
                        // Extend selection
                        if (!HasSelection) _selectionStart = _cursorPosition;
                        if (_cursorPosition > 0) _cursorPosition--;
                        _selectionEnd = _cursorPosition;
                    }
                    else
                    {
                        // Normal movement
                        if (HasSelection)
                        {
                            _cursorPosition = SelectionMin;
                            ClearSelection();
                        }
                        else if (_cursorPosition > 0)
                            _cursorPosition--;
                    }

                    break;
                }

            case Keys.Right:
                {
                    if (ctrlHeld)
                    {
                        // Word jump
                        var newPos = FindNextWordBoundary(_cursorPosition);
                        if (shiftHeld)
                        {
                            if (!HasSelection) _selectionStart = _cursorPosition;
                            _selectionEnd = newPos;
                        }
                        else ClearSelection();
                        _cursorPosition = newPos;
                    }
                    else if (shiftHeld)
                    {
                        // Extend selection
                        if (!HasSelection) _selectionStart = _cursorPosition;
                        if (_cursorPosition < Text.Length) _cursorPosition++;
                        _selectionEnd = _cursorPosition;
                    }
                    else
                    {
                        // Normal movement
                        if (HasSelection)
                        {
                            _cursorPosition = SelectionMax;
                            ClearSelection();
                        }
                        else if (_cursorPosition < Text.Length)
                            _cursorPosition++;
                    }

                    break;
                }

            case Keys.Home:
                if (shiftHeld)
                {
                    if (!HasSelection)
                        _selectionStart = _cursorPosition;
                    _selectionEnd = 0;
                }
                else ClearSelection();
                _cursorPosition = 0;
                break;
            case Keys.End:
                if (shiftHeld)
                {
                    if (!HasSelection)
                        _selectionStart = _cursorPosition;
                    _selectionEnd = Text.Length;
                }
                else ClearSelection();
                _cursorPosition = Text.Length;
                break;
            case Keys.Up when Multiline:
                {
                    int currentLine = GetLineFromPosition(_cursorPosition);
                    if (currentLine > 0)
                    {
                        int column = GetColumnFromPosition(_cursorPosition);
                        int newPos = GetPositionFromLineColumn(currentLine - 1, column);
                        if (shiftHeld)
                        {
                            if (!HasSelection)
                                _selectionStart = _cursorPosition;
                            _selectionEnd = newPos;
                        }
                        else ClearSelection();
                        _cursorPosition = newPos;
                    }

                    break;
                }

            case Keys.Down when Multiline:
                {
                    int currentLine = GetLineFromPosition(_cursorPosition);
                    int lineCount = GetLineCount();
                    if (currentLine < lineCount - 1)
                    {
                        int column = GetColumnFromPosition(_cursorPosition);
                        int newPos = GetPositionFromLineColumn(currentLine + 1, column);
                        if (shiftHeld)
                        {
                            if (!HasSelection)
                                _selectionStart = _cursorPosition;
                            _selectionEnd = newPos;
                        }
                        else ClearSelection();
                        _cursorPosition = newPos;
                    }

                    break;
                }

            default:
                {
                    var c = KeyToChar(key, shiftHeld);
                    if (c.HasValue)
                    {
                        InsertTextAtCursor(c.Value.ToString());
                    }

                    break;
                }
        }
    }

    private static char? KeyToChar(Keys key, bool shift)
    {
        // Letters
        if (key >= Keys.A && key <= Keys.Z)
        {
            var c = (char)('a' + (key - Keys.A));
            return shift ? char.ToUpper(c) : c;
        }

        // Numbers (top row)
        if (key >= Keys.D0 && key <= Keys.D9)
        {
            if (shift)
            {
                return key switch
                {
                    Keys.D1 => '!',
                    Keys.D2 => '@',
                    Keys.D3 => '#',
                    Keys.D4 => '$',
                    Keys.D5 => '%',
                    Keys.D6 => '^',
                    Keys.D7 => '&',
                    Keys.D8 => '*',
                    Keys.D9 => '(',
                    Keys.D0 => ')',
                    _ => null
                };
            }
            return (char)('0' + (key - Keys.D0));
        }

        // Numpad
        if (key >= Keys.NumPad0 && key <= Keys.NumPad9)
            return (char)('0' + (key - Keys.NumPad0));

        // Common symbols
        return key switch
        {
            Keys.Space => ' ',
            Keys.OemPeriod => shift ? '>' : '.',
            Keys.OemComma => shift ? '<' : ',',
            Keys.OemMinus => shift ? '_' : '-',
            Keys.OemPlus => shift ? '+' : '=',
            Keys.OemQuestion => shift ? '?' : '/',
            Keys.OemSemicolon => shift ? ':' : ';',
            Keys.OemQuotes => shift ? '"' : '\'',
            Keys.OemOpenBrackets => shift ? '{' : '[',
            Keys.OemCloseBrackets => shift ? '}' : ']',
            Keys.OemPipe => shift ? '|' : '\\',
            Keys.OemTilde => shift ? '~' : '`',
            _ => null
        };
    }

    private void ClearSelection()
    {
        _selectionStart = -1;
        _selectionEnd = -1;
    }

    private void DeleteSelection()
    {
        if (!HasSelection) return;

        Text = Text.Remove(SelectionMin, SelectionMax - SelectionMin);
        _cursorPosition = SelectionMin;
        ClearSelection();
        OnTextChanged?.Invoke(Text);
    }

    private void InsertTextAtCursor(string insertText)
    {
        if (HasSelection)
        {
            Text = Text.Remove(SelectionMin, SelectionMax - SelectionMin);
            _cursorPosition = SelectionMin;
            ClearSelection();
        }

        Text = Text.Insert(_cursorPosition, insertText);
        _cursorPosition += insertText.Length;
        OnTextChanged?.Invoke(Text);
    }

    private int GetCharacterIndexFromX(float pixelX)
    {
        if (Font is null || string.IsNullOrEmpty(Text)) return 0;

        var pos = Position.GetVector2();
        var textStartX = pos.X + Padding - _scrollOffset;
        var relativeX = pixelX - textStartX;

        if (relativeX <= 0) return 0;

        float accumulatedWidth = 0;
        for (int i = 0; i < Text.Length; i++)
        {
            var charWidth = Font.MeasureString(Text[i].ToString()).X;
            if (accumulatedWidth + charWidth / 2 > relativeX)
                return i;
            accumulatedWidth += charWidth;
        }

        return Text.Length;
    }

    private int FindPreviousWordBoundary(int fromPos)
    {
        if (fromPos <= 0) return 0;

        int pos = fromPos - 1;
        // Skip whitespace
        while (pos > 0 && char.IsWhiteSpace(Text[pos]))
            pos--;
        // Skip word characters
        while (pos > 0 && !char.IsWhiteSpace(Text[pos - 1]))
            pos--;

        return pos;
    }

    private int FindNextWordBoundary(int fromPos)
    {
        if (fromPos >= Text.Length) return Text.Length;

        int pos = fromPos;
        // Skip word characters
        while (pos < Text.Length && !char.IsWhiteSpace(Text[pos]))
            pos++;
        // Skip whitespace
        while (pos < Text.Length && char.IsWhiteSpace(Text[pos]))
            pos++;

        return pos;
    }

    // Multiline helpers
    private int GetLineFromPosition(int charPos)
    {
        int line = 0;
        for (int i = 0; i < charPos && i < Text.Length; i++)
            if (Text[i] == '\n') line++;
        return line;
    }

    private int GetColumnFromPosition(int charPos)
    {
        int lastNewline = -1;
        for (int i = charPos - 1; i >= 0; i--)
        {
            if (Text[i] == '\n')
            {
                lastNewline = i;
                break;
            }
        }
        return charPos - lastNewline - 1;
    }

    private int GetLineStart(int line)
    {
        if (line <= 0) return 0;
        int currentLine = 0;
        for (int i = 0; i < Text.Length; i++)
        {
            if (Text[i] == '\n')
            {
                currentLine++;
                if (currentLine == line)
                    return i + 1;
            }
        }
        return Text.Length;
    }

    private int GetLineEnd(int line)
    {
        int currentLine = 0;
        for (int i = 0; i < Text.Length; i++)
        {
            if (Text[i] == '\n')
            {
                if (currentLine == line)
                    return i;
                currentLine++;
            }
        }
        return Text.Length;
    }

    private int GetLineCount()
    {
        int count = 1;
        for (int i = 0; i < Text.Length; i++)
            if (Text[i] == '\n') count++;
        return count;
    }

    private string GetLineText(int line)
    {
        int start = GetLineStart(line);
        int end = GetLineEnd(line);
        return Text.Substring(start, end - start);
    }

    private int GetPositionFromLineColumn(int line, int column)
    {
        int start = GetLineStart(line);
        int end = GetLineEnd(line);
        int lineLength = end - start;
        return start + Math.Min(column, lineLength);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var pos = Position.GetVector2();
        var graphicsDevice = spriteBatch.GraphicsDevice;

        // Draw background
        if (Background.A > 0)
        {
            spriteBatch.Draw(
                RendererHelper.WhitePixel,
                new Rectangle((int)pos.X, (int)pos.Y, (int)Size.X, (int)Size.Y),
                Background
            );
        }

        // Draw border (use FocusedBorder when focused)
        var activeBorder = IsFocused ? FocusedBorder : Border;
        RendererHelper.Draw(spriteBatch, activeBorder, pos, Size, Scale);

        // Draw text or placeholder with clipping
        if (Font is not null)
        {
            var visibleWidth = Size.X - (Padding * 2);
            var visibleHeight = Size.Y - (Padding * 2);
            var showPlaceholder = string.IsNullOrEmpty(Text) && !IsFocused;
            var displayText = showPlaceholder ? PlaceholderText : Text;
            var textColor = showPlaceholder ? PlaceholderColor : TextColor;
            var lineHeight = Font.LineSpacing;

            if (Multiline)
            {
                // Multiline: update vertical scroll to keep cursor visible
                int cursorLine = GetLineFromPosition(_cursorPosition);
                int visibleLines = (int)(visibleHeight / lineHeight);
                if (visibleLines < 1) visibleLines = 1;

                if (cursorLine < _verticalScrollOffset)
                    _verticalScrollOffset = cursorLine;
                else if (cursorLine >= _verticalScrollOffset + visibleLines)
                    _verticalScrollOffset = cursorLine - visibleLines + 1;

                _scrollOffset = 0; // No horizontal scroll in multiline
            }
            else
            {
                // Single line: update horizontal scroll
                _verticalScrollOffset = 0;
                if (!showPlaceholder && !string.IsNullOrEmpty(Text))
                {
                    var cursorLine = GetLineFromPosition(_cursorPosition);
                    var lineStart = GetLineStart(cursorLine);
                    var textBeforeCursorOnLine = Text.Substring(lineStart, _cursorPosition - lineStart);
                    var cursorPixelX = Font.MeasureString(textBeforeCursorOnLine).X;

                    if (cursorPixelX - _scrollOffset > visibleWidth)
                        _scrollOffset = cursorPixelX - visibleWidth;
                    else if (cursorPixelX < _scrollOffset)
                        _scrollOffset = cursorPixelX;
                }
                else
                {
                    _scrollOffset = 0f;
                }
            }

            // End current batch to apply scissor clipping
            spriteBatch.End();

            // Set up scissor rectangle for text area
            var previousScissor = graphicsDevice.ScissorRectangle;
            graphicsDevice.ScissorRectangle = new Rectangle(
                (int)(pos.X + Padding),
                (int)(pos.Y + Padding),
                (int)visibleWidth,
                (int)visibleHeight
            );

            // Begin new batch with scissor test enabled
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, rasterizerState: _scissorRasterizer);

            if (Multiline)
            {
                // Multiline rendering
                var lines = displayText.Split('\n');
                int lineCount = lines.Length;

                for (int i = 0; i < lineCount; i++)
                {
                    int displayLine = i - _verticalScrollOffset;
                    if (displayLine < 0) continue;

                    var lineY = pos.Y + Padding + (displayLine * lineHeight);
                    if (lineY > pos.Y + Size.Y) break;

                    var lineText = lines[i];
                    var linePos = new Vector2(pos.X + Padding, lineY);

                    // Draw selection for this line if applicable
                    if (HasSelection && !showPlaceholder)
                    {
                        int lineStartPos = GetLineStart(i);
                        int lineEndPos = lineStartPos + lineText.Length;

                        if (SelectionMax > lineStartPos && SelectionMin < lineEndPos)
                        {
                            int selStartInLine = Math.Max(0, SelectionMin - lineStartPos);
                            int selEndInLine = Math.Min(lineText.Length, SelectionMax - lineStartPos);

                            var beforeSel = lineText.Substring(0, selStartInLine);
                            var selText = lineText.Substring(selStartInLine, selEndInLine - selStartInLine);

                            var selX = linePos.X + (selStartInLine > 0 ? Font.MeasureString(beforeSel).X : 0);
                            var selWidth = Font.MeasureString(selText).X;

                            spriteBatch.Draw(
                                RendererHelper.WhitePixel,
                                new Rectangle((int)selX, (int)lineY, (int)selWidth, lineHeight),
                                SelectionColor
                            );
                        }
                    }

                    // Draw line text
                    if (!string.IsNullOrEmpty(lineText))
                    {
                        if (QuickDraw)
                            RendererHelper.DrawOutlinedStringFast(spriteBatch, Font, lineText, linePos, textColor, new Border());
                        else
                            RendererHelper.DrawOutlinedString(spriteBatch, Font, lineText, linePos, textColor, new Border());
                    }
                }

                // Draw cursor
                if (IsFocused && _cursorVisible && !HasSelection)
                {
                    int cursorLine = GetLineFromPosition(_cursorPosition);
                    int displayLine = cursorLine - _verticalScrollOffset;
                    if (displayLine >= 0)
                    {
                        var cursorY = pos.Y + Padding + (displayLine * lineHeight);
                        int lineStart = GetLineStart(cursorLine);
                        var textBeforeCursor = Text.Substring(lineStart, _cursorPosition - lineStart);
                        var cursorX = pos.X + Padding + (string.IsNullOrEmpty(textBeforeCursor) ? 0 : Font.MeasureString(textBeforeCursor).X);

                        spriteBatch.Draw(
                            RendererHelper.WhitePixel,
                            new Rectangle((int)cursorX, (int)cursorY, CursorWidth, lineHeight),
                            CursorColor
                        );
                    }
                }
            }
            else
            {
                // Single line rendering
                var textPos = new Vector2(pos.X + Padding - _scrollOffset, pos.Y + Padding);

                // Draw selection highlight
                if (HasSelection && !string.IsNullOrEmpty(Text))
                {
                    var selMin = SelectionMin;
                    var selMax = SelectionMax;
                    var textBeforeSelection = Text.Substring(0, selMin);
                    var selectedText = Text.Substring(selMin, selMax - selMin);

                    var selectionStartX = textPos.X + (selMin > 0 ? Font.MeasureString(textBeforeSelection).X : 0);
                    var selectionWidth = Font.MeasureString(selectedText).X;

                    spriteBatch.Draw(
                        RendererHelper.WhitePixel,
                        new Rectangle((int)selectionStartX, (int)textPos.Y, (int)selectionWidth, lineHeight),
                        SelectionColor
                    );
                }

                if (!string.IsNullOrEmpty(displayText))
                {
                    if (QuickDraw)
                        RendererHelper.DrawOutlinedStringFast(spriteBatch, Font, displayText, textPos, textColor, new Border());
                    else
                        RendererHelper.DrawOutlinedString(spriteBatch, Font, displayText, textPos, textColor, new Border());
                }

                // Draw cursor when focused (not when there's a selection)
                if (IsFocused && _cursorVisible && !HasSelection)
                {
                    var textBeforeCursor = Text.Substring(0, _cursorPosition);
                    var cursorX = textPos.X + (string.IsNullOrEmpty(textBeforeCursor) ? 0 : Font.MeasureString(textBeforeCursor).X);

                    spriteBatch.Draw(
                        RendererHelper.WhitePixel,
                        new Rectangle((int)cursorX, (int)textPos.Y, CursorWidth, lineHeight),
                        CursorColor
                    );
                }
            }

            // End clipped batch and restore
            spriteBatch.End();
            graphicsDevice.ScissorRectangle = previousScissor;

            // Resume normal drawing
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        }

        base.Draw(spriteBatch);
    }
}
