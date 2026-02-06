using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Components;

/// <summary>
/// Base class for scenes that load their layout from XML files.
/// Provides automatic XML loading, named component lookup, and error handling.
/// </summary>
public abstract class XmlScene<T> : Scene<T> where T : Enum
{
    /// <summary>
    /// Path to the XML file relative to Content/Scenes/.
    /// Override to specify the XML file for this scene.
    /// </summary>
    protected abstract string XmlFileName { get; }

    /// <summary>
    /// Full path to the XML file.
    /// </summary>
    protected string XmlPath => Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Content", "Scenes", XmlFileName);

    /// <summary>
    /// Dictionary of named components for easy lookup after load.
    /// Key is the component's Name attribute from XML.
    /// </summary>
    protected Dictionary<string, BaseComponent> NamedComponents { get; } = new();

    /// <summary>
    /// Whether the XML loaded successfully.
    /// </summary>
    protected bool XmlLoadedSuccessfully { get; private set; }

    /// <summary>
    /// The exception that occurred during XML loading, if any.
    /// </summary>
    protected Exception? XmlLoadException { get; private set; }

    public XmlScene(T sceneType) : base(sceneType) { }

    public override void Initialize()
    {
        NamedComponents.Clear();
        XmlLoadedSuccessfully = false;
        XmlLoadException = null;

        try
        {
            if (!File.Exists(XmlPath))
                throw new FileNotFoundException($"Scene XML file not found: {XmlPath}");

            // Load and apply scene properties
            var xml = File.ReadAllText(XmlPath);
            var components = SceneLoader.ParseXml(xml, this);

            foreach (var component in components)
            {
                AddComponent(component);
                IndexNamedComponents(component);
            }

            XmlLoadedSuccessfully = true;
        }
        catch (Exception ex)
        {
            XmlLoadException = ex;
            OnXmlLoadError(ex);
        }

        // Call base to invoke OnSceneInitialized
        base.Initialize();

        // Allow derived class to do post-load setup
        if (XmlLoadedSuccessfully)
            OnXmlLoaded();
    }

    /// <summary>
    /// Called after XML is successfully loaded. Override to bind additional events
    /// or perform setup that can't be done in XML.
    /// </summary>
    protected virtual void OnXmlLoaded() { }

    /// <summary>
    /// Called when XML loading fails. Default shows error overlay.
    /// Override for custom error handling.
    /// </summary>
    protected virtual void OnXmlLoadError(Exception ex)
    {
        BackgroundColor = new Color(40, 0, 0);
        BackgroundMode = SceneBackgroundMode.Color;

        // Create error panel
        var panel = new Panel("error_panel")
        {
            Position = new Anchor(new Vector2(50, 50)),
            Size = new Vector2(ScaleManager.VirtualWidth - 100, ScaleManager.VirtualHeight - 100),
            Background = new Color(60, 20, 20),
            Border = new Border(4, Color.Red)
        };

        // Title
        var titleFont = TryLoadFont("HeaderFont") ?? TryLoadFont("DefaultFont");
        if (titleFont is not null)
        {
            var title = new Label("error_title", "XML Parse Error", titleFont,
                new Anchor(new Vector2(20, 20)))
            {
                Color = Color.Red
            };
            panel.Children.Add(title);
        }

        // Error message
        var messageFont = TryLoadFont("DefaultFont");
        if (messageFont is not null)
        {
            var message = new Label("error_message", ex.Message, messageFont,
                new Anchor(new Vector2(20, 80)))
            {
                Color = Color.White,
                MaxWidth = (int)panel.Size.X - 40
            };
            panel.Children.Add(message);

            // Stack trace (truncated)
            var stackTrace = ex.StackTrace ?? "";
            if (stackTrace.Length > 500)
                stackTrace = stackTrace.Substring(0, 500) + "...";

            var trace = new Label("error_trace", stackTrace, messageFont,
                new Anchor(new Vector2(20, 160)))
            {
                Color = new Color(200, 200, 200),
                MaxWidth = (int)panel.Size.X - 40
            };
            panel.Children.Add(trace);

            // Hint
            var hint = new Label("error_hint", "Fix the XML file and save to reload.", messageFont,
                new Anchor(new Vector2(20, panel.Size.Y - 60)))
            {
                Color = Color.Gray
            };
            panel.Children.Add(hint);
        }

        AddComponent(panel);
    }

    /// <summary>
    /// Get a component by name for event binding or manipulation.
    /// Returns null if not found or if the type doesn't match.
    /// </summary>
    protected TComponent? GetComponent<TComponent>(string name) where TComponent : BaseComponent
    {
        if (NamedComponents.TryGetValue(name, out var component))
            return component as TComponent;
        return null;
    }

    /// <summary>
    /// Get a component by name (non-generic version).
    /// Returns null if not found.
    /// </summary>
    protected BaseComponent? GetComponent(string name)
    {
        NamedComponents.TryGetValue(name, out var component);
        return component;
    }

    /// <summary>
    /// Get a typed component by name for fluent event binding.
    /// Throws if the component is not found or type doesn't match.
    /// Usage: Bind&lt;Button&gt;("my_button").OnClicked += HandleClick;
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when component not found or type mismatch.</exception>
    protected TComponent Bind<TComponent>(string name) where TComponent : BaseComponent
    {
        if (!NamedComponents.TryGetValue(name, out var component))
            throw new InvalidOperationException($"Component '{name}' not found. Make sure it has a Name attribute in XML.");

        if (component is not TComponent typed)
            throw new InvalidOperationException($"Component '{name}' is {component.GetType().Name}, not {typeof(TComponent).Name}.");

        return typed;
    }

    /// <summary>
    /// Try to get a typed component by name for fluent event binding.
    /// Returns null if not found or type doesn't match (no exception).
    /// Usage: TryBind&lt;Button&gt;("my_button")?.OnClicked += HandleClick;
    /// </summary>
    protected TComponent? TryBind<TComponent>(string name) where TComponent : BaseComponent
    {
        if (NamedComponents.TryGetValue(name, out var component))
            return component as TComponent;
        return null;
    }

    /// <summary>
    /// Recursively index all named components in the component tree.
    /// </summary>
    private void IndexNamedComponents(BaseComponent component)
    {
        if (!string.IsNullOrEmpty(component.Name) && component.Name != "Unknown")
            NamedComponents[component.Name] = component;

        // Handle SubSceneContainer specially - its SubScenes are not in Children
        if (component is SubSceneContainer container)
        {
            for (int i = 0; i < container.Count; i++)
            {
                var subScene = container.GetSubScene(i);
                if (subScene is not null)
                    IndexNamedComponents(subScene);
            }
        }

        foreach (var child in component.Children)
            IndexNamedComponents(child);
    }

    /// <summary>
    /// Try to load a font, returning null if it fails.
    /// </summary>
    private static SpriteFont? TryLoadFont(string fontName)
    {
        try
        {
            return ContentHelper.LoadFont(fontName);
        }
        catch
        {
            return null;
        }
    }
}
