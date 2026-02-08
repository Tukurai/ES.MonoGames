using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Components;

public enum BobDirection
{
    Left,
    Right,
    Up,
    Down
}

public abstract class BaseComponent
{
    /// <summary>
    /// This is a unique identifier for the component. Can be used to track components across sessions.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// This is a human-readable name for the component. Does not have to be unique.
    /// </summary>
    [JsonIgnore]
    public string Name
    {
        get;
        set { field = value; OnNameChanged?.Invoke(); }
    }

    /// <summary>
    /// Gets or sets the position of the object in 2D space.
    /// </summary>
    [JsonIgnore]
    public Anchor Position
    {
        get;
        set { field = value; OnPositionChanged?.Invoke(); }
    }

    /// <summary>
    /// Gets or sets the size of the component. The default is (0,0). This is the unscaled size.
    /// </summary>
    [JsonIgnore]
    public Vector2 Size
    {
        get;
        set { field = value; OnSizeChanged?.Invoke(); }
    }

    /// <summary>
    /// Gets or sets the rotation represented by this object as a float. The default is 0f.
    /// </summary>
    [JsonIgnore]
    public float Rotation
    {
        get;
        set { field = value; OnRotationChanged?.Invoke(); }
    } = 0f;

    /// <summary>
    /// Local scale of the component, default is (1,1). This is applied before any parent scale.
    /// </summary>
    [JsonIgnore]
    public Vector2 Scale
    {
        get;
        set { field = value; OnScaleChanged?.Invoke(); }
    } = new Vector2(1, 1);

    [JsonIgnore] public bool Hovered { get; set; } = false;
    [JsonIgnore] public bool Dragging { get; set; } = false;
    [JsonIgnore] public bool Pressed { get; set; } = false;
    [JsonIgnore] public ToolTip? ToolTip { get; set; }

    /// <summary>
    /// The direction the component bobs in. Set to null to disable bobbing.
    /// </summary>
    [JsonIgnore]
    public BobDirection? Bob { get; set; }

    /// <summary>
    /// How many scaled pixels the bob shifts. Default is 4.
    /// </summary>
    [JsonIgnore]
    public float BobDistance { get; set; } = 4f;

    /// <summary>
    /// How often the bob toggles in milliseconds. Default is 600ms.
    /// </summary>
    [JsonIgnore]
    public float BobInterval { get; set; } = 650f;

    /// <summary>
    /// Opacity of this component (0f = fully transparent, 1f = fully opaque). Default is 1f.
    /// Children inherit their parent's effective opacity multiplicatively.
    /// </summary>
    [JsonIgnore]
    public float Opacity { get; set; } = 1f;

    /// <summary>
    /// The computed opacity including parent opacity. Use this when drawing.
    /// </summary>
    [JsonIgnore]
    public float EffectiveOpacity => Opacity * _parentOpacity;

    private float _parentOpacity = 1f;

    /// <summary>
    /// Returns the given color with EffectiveOpacity applied to its alpha channel.
    /// </summary>
    protected Color ApplyOpacity(Color color)
        => color * EffectiveOpacity;

    private float _bobTimer;
    private bool _bobOffset;

    /// <summary>
    /// Gets the collection of child components contained within this component.
    /// </summary>
    /// <remarks>The returned list provides access to all direct child components. Modifying the collection
    /// affects the component hierarchy. The order of items in the list reflects the order in which child components are
    /// added.</remarks>
    [JsonIgnore]
    public List<BaseComponent> Children { get; } = [];

    // Possible events on a component.
    public event Action? OnPositionChanged;
    public event Action? OnRotationChanged;
    public event Action? OnScaleChanged;
    public event Action? OnSizeChanged;
    public event Action? OnNameChanged;
    public event Action? OnComponentUpdated;

    /// <summary>
    /// Occurs when the control is clicked.
    /// </summary>
    /// <remarks>Subscribe to this event to execute custom logic in response to a click action that is being held. The event does
    /// not provide event data; use this event for simple notification scenarios.</remarks>
    public event Action? OnHeld;

    /// <summary>
    /// Occurs when the control is hovered over.
    /// </summary>
    public event Action? OnHoveredEnter;

    /// <summary>
    /// Occurs when the pointer or cursor exits the hover area of the associated element.
    /// </summary>
    /// <remarks>Subscribe to this event to perform actions when a hover interaction ends, such as reverting
    /// visual changes or triggering exit animations. The event does not provide event arguments; additional context
    /// must be managed separately if needed.</remarks>
    public event Action? OnHoveredExit;

    /// <summary>
    /// Occurs when the control is pressed.
    /// </summary>
    /// <remarks>Subscribe to this event to execute custom logic when the control receives a press action,
    /// such as a button click or tap. The event does not provide event data; use this event for simple notification
    /// scenarios.</remarks>
    public event Action? OnPressed;

    /// <summary>
    /// Occurs when the resource is released.
    /// </summary>
    /// <remarks>Subscribe to this event to execute custom logic when the control is released after a press action,
    /// such as a button click or tap. The event does not provide event data; use this event for simple notification
    public event Action? OnReleased;

    public event Action? OnClicked;

    /// <summary>
    /// Occurs when the control has been dragged by the user.
    /// </summary>
    public event Action<Vector2>? OnDragged;

    public BaseComponent(string? name = null, Anchor? position = null, Vector2? size = null)
    {
        Name = name ?? "Unknown";
        Position = position ?? new Anchor(Vector2.Zero);
        Size = size ?? Vector2.Zero;

        Initialize();
    }

    public virtual void Initialize()
    {
        foreach (var child in Children)
            child.Initialize();
    }

    public virtual void Update(GameTime gameTime)
    {
        // Check if mouse is blocked by an overlay (dropdown, popup, etc.)
        var mousePos = ControlState.GetMousePosition();
        var isBlockedByOverlay = OverlayManager.IsPointBlocked(mousePos);

        // determine on click area and fire events
        var scaledSize = new Vector2(Size.X * Scale.X, Size.Y * Scale.Y);
        var componentArea = new Rectangle(Position.GetVector2().ToPoint(), scaledSize.ToPoint());
        if (!isBlockedByOverlay && ControlState.MouseInArea(componentArea))
        {
            if (!Hovered)
            {
                Hovered = true;
                OnHoveredEnter?.Invoke();
            }

            if (ControlState.GetPressedMouseButtons().Length != 0)
            {
                Pressed = true;
                OnPressed?.Invoke();
            }

            if (ControlState.GetHeldMouseButtons().Length != 0)
            {
                OnHeld?.Invoke();

                if (OnDragged is not null && !Dragging && Pressed) // Don't start dragging unless pressed
                    Dragging = true;
            }

            if (ControlState.GetReleasedMouseButtons().Length != 0)
            {
                if (Pressed)
                    OnClicked?.Invoke();

                Pressed = false;
                OnReleased?.Invoke();
            }
        }
        else
        {
            if (Hovered)
            {
                Hovered = false;
                OnHoveredExit?.Invoke();
            }

            if (Pressed)
            {
                Pressed = false; // Soft release if we leave the area without emitting the event
            }
        }

        if (Dragging)
        {
            if (ControlState.GetHeldMouseButtons().Length == 0)
            {
                Dragging = false;
            }
            else
            {
                var (Previous, Current) = ControlState.GetMouseDelta();
                if (OnDragged is not null && Previous != Current)
                    OnDragged?.Invoke((Current - Previous).ToVector2());
            }
        }

        // Request cursor type based on component state
        if (Dragging)
            ControlState.RequestCursor(CursorType.Grab);
        else if (Hovered && (OnClicked is not null || OnHoveredEnter is not null || OnPressed is not null))
            ControlState.RequestCursor(CursorType.Pointer);

        // Request tooltip when hovered
        if (Hovered && ToolTip is not null)
            ToolTipManager.Request(ToolTip);

        // Bob effect
        if (Bob is not null)
        {
            _bobTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_bobTimer >= BobInterval)
            {
                _bobTimer -= BobInterval;
                _bobOffset = !_bobOffset;

                var delta = GetBobDelta(Bob.Value);
                Position.TransformPosition(_bobOffset ? delta : -delta);
            }
        }

        foreach (var child in Children)
        {
            child._parentOpacity = EffectiveOpacity;
            child.Update(gameTime);
        }

        OnComponentUpdated?.Invoke();
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        foreach (var child in Children)
        {
            child._parentOpacity = EffectiveOpacity;
            child.Draw(spriteBatch);
        }
    }

    private Vector2 GetBobDelta(BobDirection direction) => direction switch
    {
        BobDirection.Left => new Vector2(-BobDistance, 0),
        BobDirection.Right => new Vector2(BobDistance, 0),
        BobDirection.Up => new Vector2(0, -BobDistance),
        BobDirection.Down => new Vector2(0, BobDistance),
        _ => Vector2.Zero
    };

    public override string ToString()
    {
        return $"""
            # {GetType().Name}
            Id: {Id}
            Name: {Name}
            Instance: {this.GetHashCode()}
            Position: {Position.GetVector2()}
            Size: {Size}
            Rotation: {Rotation}
            Scale: {Scale}
            """;
    }
}
