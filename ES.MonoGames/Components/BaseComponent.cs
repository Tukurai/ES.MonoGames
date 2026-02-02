using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Components;

public abstract class BaseComponent
{
    /// <summary>
    /// This is a unique identifier for the component. Can be used to track components across sessions.
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();

    /// <summary>
    /// This is a human-readable name for the component. Does not have to be unique.
    /// </summary>
    public string Name
    {
        get;
        set { field = value; OnNameChanged?.Invoke(); }
    }

    /// <summary>
    /// Gets or sets the position of the object in 2D space.
    /// </summary>
    public Anchor Position
    {
        get;
        set { field = value; OnPositionChanged?.Invoke(); }
    }

    /// <summary>
    /// Gets or sets the size of the component. The default is (0,0). This is the unscaled size.
    /// </summary>
    public Vector2 Size
    {
        get;
        set { field = value; OnSizeChanged?.Invoke(); }
    }

    /// <summary>
    /// Gets or sets the rotation represented by this object as a float. The default is 0f.
    /// </summary>
    public float Rotation
    {
        get;
        set { field = value; OnRotationChanged?.Invoke(); }
    } = 0f;

    /// <summary>
    /// Local scale of the component, default is (1,1). This is applied before any parent scale.
    /// </summary>
    public Vector2 Scale
    {
        get;
        set { field = value; OnScaleChanged?.Invoke(); }
    } = new Vector2(1, 1);

    public bool Hovered { get; set; } = false;
    public bool Dragging { get; set; } = false;
    public bool Pressed { get; set; } = false;

    /// <summary>
    /// Gets the collection of child components contained within this component.
    /// </summary>
    /// <remarks>The returned list provides access to all direct child components. Modifying the collection
    /// affects the component hierarchy. The order of items in the list reflects the order in which child components are
    /// added.</remarks>
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
        {
            child.Initialize();
        }
    }

    public virtual void Update(GameTime gameTime)
    {
        // Check if mouse is blocked by an overlay (dropdown, popup, etc.)
        var mousePos = ControlState.GetMousePosition();
        var isBlockedByOverlay = OverlayManager.IsPointBlocked(mousePos);

        // determine on click area and fire events
        var componentArea = new Rectangle(Position.GetVector2().ToPoint(), Size.ToPoint());
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

        foreach (var child in Children)
        {
            child.Update(gameTime);
        }

        OnComponentUpdated?.Invoke();
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        foreach (var child in Children)
        {
            child.Draw(spriteBatch);
        }
    }

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
