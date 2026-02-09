using Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Helpers;

/// <summary>
/// Static class that parses XML and creates component trees for scenes.
/// Supports event binding via reflection and hot reload.
/// </summary>
public static class SceneLoader
{
    /// <summary>
    /// Dictionary to store named components for relative positioning lookups during parsing.
    /// </summary>
    private static Dictionary<string, BaseComponent> _namedComponents = new();

    /// <summary>
    /// Parse XML string and create components, binding events to scene instance.
    /// </summary>
    public static List<BaseComponent> ParseXml(string xmlContent, IScene scene)
    {
        _namedComponents.Clear();
        var root = XElement.Parse(xmlContent);
        ApplySceneProperties(root, scene);
        return ParseChildren(root, scene);
    }

    /// <summary>
    /// Load components from XML file, bind events to scene instance.
    /// </summary>
    public static List<BaseComponent> LoadFromXml(string xmlPath, IScene scene)
    {
        var xmlContent = System.IO.File.ReadAllText(xmlPath);
        return ParseXml(xmlContent, scene);
    }

    /// <summary>
    /// Apply scene-level properties from XML root element.
    /// </summary>
    public static void ApplySceneProperties(XElement root, IScene scene)
    {
        // Background mode and color
        var bgMode = ParseAttribute<SceneBackgroundMode>(root, "Background", SceneBackgroundMode.Default);
        var bgColor = ParseColor(root.Attribute("BackgroundColor")?.Value);
        var bgOpacity = ParseFloat(root.Attribute("BackgroundOpacity")?.Value, 1f);

        scene.BackgroundMode = bgMode;
        if (bgColor.HasValue)
            scene.BackgroundColor = bgColor.Value;
        scene.BackgroundOpacity = bgOpacity;

        // Background sprite
        var bgSprite = root.Attribute("BackgroundSprite")?.Value;
        if (!string.IsNullOrEmpty(bgSprite))
        {
            var textureResult = ParseSprite(bgSprite);
            if (textureResult is not null)
            {
                scene.BackgroundSprite = textureResult;
                scene.BackgroundMode = SceneBackgroundMode.Sprite;
            }
        }

        // Scene track
        var sceneTrack = root.Attribute("SceneTrack")?.Value;
        if (!string.IsNullOrEmpty(sceneTrack))
            scene.SceneTrack = sceneTrack;
    }

    /// <summary>
    /// Parse child elements and create components.
    /// </summary>
    private static List<BaseComponent> ParseChildren(XElement parent, IScene scene)
    {
        var components = new List<BaseComponent>();

        foreach (var element in parent.Elements())
        {
            // Skip special child elements that aren't components
            if (IsSpecialElement(element.Name.LocalName))
                continue;

            var component = CreateComponent(element, scene);
            if (component is not null)
                components.Add(component);
        }

        return components;
    }

    /// <summary>
    /// Check if element name is a special element (not a component).
    /// </summary>
    private static bool IsSpecialElement(string name) => name switch
    {
        "Border" => true,
        "TextBorder" => true,
        "TrackBorder" => true,
        "ThumbBorder" => true,
        "FocusedBorder" => true,
        "ListBorder" => true,
        "Children" => true,
        "SubScenes" => true,
        _ => false
    };

    /// <summary>
    /// Create a component from an XML element.
    /// </summary>
    private static BaseComponent? CreateComponent(XElement element, IScene scene)
    {
        var component = element.Name.LocalName switch
        {
            "Label" => CreateLabel(element),
            "Button" => CreateButton(element),
            "SpriteButton" => CreateSpriteButton(element),
            "Checkbox" => CreateCheckbox(element),
            "Slider" => CreateSlider(element),
            "Dropdown" => CreateDropdown(element),
            "InputField" => CreateInputField(element),
            "Panel" => CreatePanel(element, scene),
            "ScrollPanel" => CreateScrollPanel(element, scene),
            "SubScene" => CreateSubScene(element, scene),
            "SubSceneContainer" => CreateSubSceneContainer(element, scene),
            "Sprite" => CreateSprite(element),
            "AnimatedSprite" => CreateAnimatedSprite(element),
            "PixelLabel" => CreatePixelLabel(element),
            "BitmapLabel" => CreateBitmapLabel(element),
            "Include" => CreateInclude(element, scene),
            _ => null
        };

        if (component is null)
            return null;

        // Apply common properties (including position)
        ApplyCommonProperties(component, element);

        // Register this component by name BEFORE parsing children
        // This allows children to use Anchor to reference their parent
        var name = element.Attribute("Name")?.Value;
        if (!string.IsNullOrEmpty(name))
            _namedComponents[name] = component;

        // Parse children (they can now reference this component via Anchor)
        var childrenElement = element.Element("Children");
        if (childrenElement is not null)
        {
            var children = ParseChildren(childrenElement, scene);
            foreach (var child in children)
                component.Children.Add(child);
        }

        return component;
    }

    /// <summary>
    /// Apply common properties shared by all components.
    /// </summary>
    private static void ApplyCommonProperties(BaseComponent component, XElement element)
    {
        // Name
        var name = element.Attribute("Name")?.Value;
        if (!string.IsNullOrEmpty(name))
            component.Name = name;

        // Position (absolute or relative)
        // Skip for Label - position is set in CreateLabel because Label.Initialize()
        // applies centering transforms that would be lost if we overwrote the position here
        if (component is not Label)
        {
            var position = ParseAnchor(element.Attribute("Position")?.Value);
            if (position is not null)
                component.Position = position;
        }

        // Size
        var size = ParseVector2(element.Attribute("Size")?.Value);
        if (size.HasValue)
            component.Size = size.Value;

        // Scale
        var scale = ParseVector2(element.Attribute("Scale")?.Value);
        if (scale.HasValue)
            component.Scale = scale.Value;

        // Rotation (degrees to radians)
        var rotation = ParseFloat(element.Attribute("Rotation")?.Value);
        if (rotation.HasValue)
            component.Rotation = MathHelper.ToRadians(rotation.Value);

        // Opacity
        var opacity = ParseFloat(element.Attribute("Opacity")?.Value);
        if (opacity.HasValue)
            component.Opacity = opacity.Value;

        // Bob
        var bob = ParseAttribute<BobDirection?>(element, "Bob", null);
        if (bob.HasValue)
            component.Bob = bob.Value;

        var bobDistance = ParseFloat(element.Attribute("BobDistance")?.Value);
        if (bobDistance.HasValue)
            component.BobDistance = bobDistance.Value;

        var bobInterval = ParseFloat(element.Attribute("BobInterval")?.Value);
        if (bobInterval.HasValue)
            component.BobInterval = bobInterval.Value;
    }

    #region Component Creators

    private static Label CreateLabel(XElement element)
    {
        var text = element.Attribute("Text")?.Value ?? "";
        var fontName = element.Attribute("Font")?.Value;
        var center = ParseBool(element.Attribute("Center")?.Value) ?? false;
        var maxWidth = ParseInt(element.Attribute("MaxWidth")?.Value) ?? 0;

        SpriteFont? font = null;
        if (!string.IsNullOrEmpty(fontName))
            font = ContentHelper.LoadFont(fontName);

        // Parse position here because Label.Initialize() uses it for centering
        // and Initialize() is called in the constructor before ApplyCommonProperties
        var position = ParseAnchor(element.Attribute("Position")?.Value);

        var label = new Label(null, text, font, position, center, maxWidth);

        // Label-specific properties
        var color = ParseColor(element.Attribute("Color")?.Value);
        if (color.HasValue)
            label.Color = color.Value;

        var quickDraw = ParseBool(element.Attribute("QuickDraw")?.Value);
        if (quickDraw.HasValue)
            label.QuickDraw = quickDraw.Value;

        // Border from child element
        var border = ParseBorder(element);
        if (border is not null)
            label.Border = border;

        return label;
    }

    private static Button CreateButton(XElement element)
    {
        var text = element.Attribute("Text")?.Value ?? "Button";
        var fontName = element.Attribute("Font")?.Value;
        // Default to centered=true for XML buttons (more common use case)
        var centered = ParseBool(element.Attribute("Centered")?.Value) ?? true;

        SpriteFont? font = null;
        if (!string.IsNullOrEmpty(fontName))
            font = ContentHelper.LoadFont(fontName);

        var size = ParseVector2(element.Attribute("Size")?.Value) ?? new Vector2(100, 40);

        var button = new Button(null, text, font, null, size, centered);

        // Button-specific properties
        var textColor = ParseColor(element.Attribute("TextColor")?.Value);
        if (textColor.HasValue)
            button.TextColor = textColor.Value;

        var background = ParseColor(element.Attribute("Background")?.Value);
        if (background.HasValue)
            button.Background = background.Value;

        var padding = ParseInt(element.Attribute("Padding")?.Value);
        if (padding.HasValue)
            button.Padding = padding.Value;

        var pressDepth = ParseInt(element.Attribute("PressDepth")?.Value);
        if (pressDepth.HasValue)
            button.PressDepth = pressDepth.Value;

        var quickDraw = ParseBool(element.Attribute("QuickDraw")?.Value);
        if (quickDraw.HasValue)
            button.QuickDraw = quickDraw.Value;

        // Border from child element
        var border = ParseBorder(element);
        if (border is not null)
            button.Border = border;

        // Text border from child element
        var textBorder = ParseBorder(element, "TextBorder");
        if (textBorder is not null)
            button.TextBorder = textBorder;

        return button;
    }

    private static SpriteButton CreateSpriteButton(XElement element)
    {
        var button = new SpriteButton();

        // Sprite properties
        var normalSprite = element.Attribute("NormalSprite")?.Value;
        if (!string.IsNullOrEmpty(normalSprite))
            button.SetNormalSprite(ParseSprite(normalSprite));

        var hoveredSprite = element.Attribute("HoveredSprite")?.Value;
        if (!string.IsNullOrEmpty(hoveredSprite))
            button.SetHoveredSprite(ParseSprite(hoveredSprite));

        var pressedSprite = element.Attribute("PressedSprite")?.Value;
        if (!string.IsNullOrEmpty(pressedSprite))
            button.SetPressedSprite(ParseSprite(pressedSprite));

        var disabledSprite = element.Attribute("DisabledSprite")?.Value;
        if (!string.IsNullOrEmpty(disabledSprite))
            button.SetDisabledSprite(ParseSprite(disabledSprite));

        // Other properties
        var isEnabled = ParseBool(element.Attribute("IsEnabled")?.Value);
        if (isEnabled.HasValue)
            button.IsEnabled = isEnabled.Value;

        var tint = ParseColor(element.Attribute("Tint")?.Value);
        if (tint.HasValue)
            button.Tint = tint.Value;

        var disabledTint = ParseColor(element.Attribute("DisabledTint")?.Value);
        if (disabledTint.HasValue)
            button.DisabledTint = disabledTint.Value;

        // Text overlay
        var text = element.Attribute("Text")?.Value;
        if (!string.IsNullOrEmpty(text))
            button.Text = text;

        var fontName = element.Attribute("Font")?.Value;
        if (!string.IsNullOrEmpty(fontName))
            button.Font = ContentHelper.LoadFont(fontName);

        var textColor = ParseColor(element.Attribute("TextColor")?.Value);
        if (textColor.HasValue)
            button.TextColor = textColor.Value;

        var textBorder = ParseBorder(element, "TextBorder");
        if (textBorder is not null)
            button.TextBorder = textBorder;

        return button;
    }

    private static Checkbox CreateCheckbox(XElement element)
    {
        var checkbox = new Checkbox();

        // State
        var isChecked = ParseBool(element.Attribute("IsChecked")?.Value);
        if (isChecked.HasValue)
            checkbox.IsChecked = isChecked.Value;

        var isEnabled = ParseBool(element.Attribute("IsEnabled")?.Value);
        if (isEnabled.HasValue)
            checkbox.IsEnabled = isEnabled.Value;

        // Sprite mode
        var checkedSprite = element.Attribute("CheckedSprite")?.Value;
        if (!string.IsNullOrEmpty(checkedSprite))
            checkbox.SetCheckedSprite(ParseSprite(checkedSprite));

        var uncheckedSprite = element.Attribute("UncheckedSprite")?.Value;
        if (!string.IsNullOrEmpty(uncheckedSprite))
            checkbox.SetUncheckedSprite(ParseSprite(uncheckedSprite));

        // Box mode
        var boxSize = ParseInt(element.Attribute("BoxSize")?.Value);
        if (boxSize.HasValue)
            checkbox.BoxSize = boxSize.Value;

        var boxBackground = ParseColor(element.Attribute("BoxBackground")?.Value);
        if (boxBackground.HasValue)
            checkbox.BoxBackground = boxBackground.Value;

        var boxBorderColor = ParseColor(element.Attribute("BoxBorderColor")?.Value);
        if (boxBorderColor.HasValue)
            checkbox.BoxBorderColor = boxBorderColor.Value;

        var checkmarkColor = ParseColor(element.Attribute("CheckmarkColor")?.Value);
        if (checkmarkColor.HasValue)
            checkbox.CheckmarkColor = checkmarkColor.Value;

        var boxBorderThickness = ParseInt(element.Attribute("BoxBorderThickness")?.Value);
        if (boxBorderThickness.HasValue)
            checkbox.BoxBorderThickness = boxBorderThickness.Value;

        // Label
        var label = element.Attribute("Label")?.Value;
        if (!string.IsNullOrEmpty(label))
            checkbox.Label = label;

        var fontName = element.Attribute("Font")?.Value;
        if (!string.IsNullOrEmpty(fontName))
            checkbox.Font = ContentHelper.LoadFont(fontName);

        var labelColor = ParseColor(element.Attribute("LabelColor")?.Value);
        if (labelColor.HasValue)
            checkbox.LabelColor = labelColor.Value;

        var labelSpacing = ParseInt(element.Attribute("LabelSpacing")?.Value);
        if (labelSpacing.HasValue)
            checkbox.LabelSpacing = labelSpacing.Value;

        return checkbox;
    }

    private static Slider CreateSlider(XElement element)
    {
        var size = ParseVector2(element.Attribute("Size")?.Value) ?? new Vector2(200, 20);

        var slider = new Slider(null, null, size);

        // Range
        var min = ParseInt(element.Attribute("Min")?.Value);
        if (min.HasValue)
            slider.MinValue = min.Value;

        var max = ParseInt(element.Attribute("Max")?.Value);
        if (max.HasValue)
            slider.MaxValue = max.Value;

        var value = ParseInt(element.Attribute("Value")?.Value);
        if (value.HasValue)
            slider.Value = value.Value;

        var isEnabled = ParseBool(element.Attribute("IsEnabled")?.Value);
        if (isEnabled.HasValue)
            slider.IsEnabled = isEnabled.Value;

        // Track appearance
        var trackHeight = ParseInt(element.Attribute("TrackHeight")?.Value);
        if (trackHeight.HasValue)
            slider.TrackHeight = trackHeight.Value;

        var trackColor = ParseColor(element.Attribute("TrackColor")?.Value);
        if (trackColor.HasValue)
            slider.TrackColor = trackColor.Value;

        var trackFillColor = ParseColor(element.Attribute("TrackFillColor")?.Value);
        if (trackFillColor.HasValue)
            slider.TrackFillColor = trackFillColor.Value;

        // Thumb appearance
        var thumbSize = ParseVector2(element.Attribute("ThumbSize")?.Value);
        if (thumbSize.HasValue)
            slider.ThumbSize = thumbSize.Value;

        var thumbColor = ParseColor(element.Attribute("ThumbColor")?.Value);
        if (thumbColor.HasValue)
            slider.ThumbColor = thumbColor.Value;

        // Value display
        var showValue = ParseBool(element.Attribute("ShowValue")?.Value);
        if (showValue.HasValue)
            slider.ShowValue = showValue.Value;

        var valueFormat = element.Attribute("ValueFormat")?.Value;
        if (!string.IsNullOrEmpty(valueFormat))
            slider.ValueFormat = valueFormat;

        var fontName = element.Attribute("Font")?.Value;
        if (!string.IsNullOrEmpty(fontName))
            slider.Font = ContentHelper.LoadFont(fontName);

        // Borders
        var trackBorder = ParseBorder(element, "TrackBorder");
        if (trackBorder is not null)
            slider.TrackBorder = trackBorder;

        var thumbBorder = ParseBorder(element, "ThumbBorder");
        if (thumbBorder is not null)
            slider.ThumbBorder = thumbBorder;

        return slider;
    }

    private static Dropdown CreateDropdown(XElement element)
    {
        var size = ParseVector2(element.Attribute("Size")?.Value) ?? new Vector2(200, 32);

        var dropdown = new Dropdown(null, null, size);

        // Items (comma-separated)
        var items = element.Attribute("Items")?.Value;
        if (!string.IsNullOrEmpty(items))
            dropdown.Items = items.Split(',').Select(s => s.Trim()).ToList();

        var selectedIndex = ParseInt(element.Attribute("SelectedIndex")?.Value);
        if (selectedIndex.HasValue)
            dropdown.SelectedIndex = selectedIndex.Value;

        var placeholder = element.Attribute("Placeholder")?.Value;
        if (!string.IsNullOrEmpty(placeholder))
            dropdown.Placeholder = placeholder;

        var fontName = element.Attribute("Font")?.Value;
        if (!string.IsNullOrEmpty(fontName))
            dropdown.Font = ContentHelper.LoadFont(fontName);

        // Appearance
        var background = ParseColor(element.Attribute("Background")?.Value);
        if (background.HasValue)
            dropdown.Background = background.Value;

        var textColor = ParseColor(element.Attribute("TextColor")?.Value);
        if (textColor.HasValue)
            dropdown.TextColor = textColor.Value;

        var maxVisibleItems = ParseInt(element.Attribute("MaxVisibleItems")?.Value);
        if (maxVisibleItems.HasValue)
            dropdown.MaxVisibleItems = maxVisibleItems.Value;

        var padding = ParseInt(element.Attribute("Padding")?.Value);
        if (padding.HasValue)
            dropdown.Padding = padding.Value;

        var arrowWidth = ParseInt(element.Attribute("ArrowWidth")?.Value);
        if (arrowWidth.HasValue)
            dropdown.ArrowWidth = arrowWidth.Value;

        var arrowSize = ParseInt(element.Attribute("ArrowSize")?.Value);
        if (arrowSize.HasValue)
            dropdown.ArrowSize = arrowSize.Value;

        var trianglePixelSize = ParseInt(element.Attribute("TrianglePixelSize")?.Value);
        if (trianglePixelSize.HasValue)
            dropdown.TrianglePixelSize = trianglePixelSize.Value;

        var scrollbarWidth = ParseInt(element.Attribute("ScrollbarWidth")?.Value);
        if (scrollbarWidth.HasValue)
            dropdown.ScrollbarWidth = scrollbarWidth.Value;

        // Border
        var border = ParseBorder(element);
        if (border is not null)
            dropdown.Border = border;

        // Focused border
        var focusedBorder = ParseBorder(element, "FocusedBorder");
        if (focusedBorder is not null)
            dropdown.FocusedBorder = focusedBorder;

        // List border
        var listBorder = ParseBorder(element, "ListBorder");
        if (listBorder is not null)
            dropdown.ListBorder = listBorder;

        return dropdown;
    }

    private static InputField CreateInputField(XElement element)
    {
        var text = element.Attribute("Text")?.Value ?? "";
        var placeholder = element.Attribute("Placeholder")?.Value ?? "";
        var fontName = element.Attribute("Font")?.Value;
        var size = ParseVector2(element.Attribute("Size")?.Value) ?? new Vector2(200, 32);

        SpriteFont? font = null;
        if (!string.IsNullOrEmpty(fontName))
            font = ContentHelper.LoadFont(fontName);

        var inputField = new InputField(null, text, placeholder, font, null, size);

        // Appearance
        var textColor = ParseColor(element.Attribute("TextColor")?.Value);
        if (textColor.HasValue)
            inputField.TextColor = textColor.Value;

        var background = ParseColor(element.Attribute("Background")?.Value);
        if (background.HasValue)
            inputField.Background = background.Value;

        var multiline = ParseBool(element.Attribute("Multiline")?.Value);
        if (multiline.HasValue)
            inputField.Multiline = multiline.Value;

        var placeholderColor = ParseColor(element.Attribute("PlaceholderColor")?.Value);
        if (placeholderColor.HasValue)
            inputField.PlaceholderColor = placeholderColor.Value;

        var padding = ParseInt(element.Attribute("Padding")?.Value);
        if (padding.HasValue)
            inputField.Padding = padding.Value;

        var cursorWidth = ParseInt(element.Attribute("CursorWidth")?.Value);
        if (cursorWidth.HasValue)
            inputField.CursorWidth = cursorWidth.Value;

        // Border
        var border = ParseBorder(element);
        if (border is not null)
            inputField.Border = border;

        // Focused border
        var focusedBorder = ParseBorder(element, "FocusedBorder");
        if (focusedBorder is not null)
            inputField.FocusedBorder = focusedBorder;

        return inputField;
    }

    private static Panel CreatePanel(XElement element, IScene scene)
    {
        var panel = new Panel();

        // Panel-specific properties
        var background = ParseColor(element.Attribute("Background")?.Value);
        if (background.HasValue)
            panel.Background = background.Value;

        // Border
        var border = ParseBorder(element);
        if (border is not null)
            panel.Border = border;

        return panel;
    }

    private static ScrollPanel CreateScrollPanel(XElement element, IScene scene)
    {
        var scrollPanel = new ScrollPanel();

        // Panel properties
        var background = ParseColor(element.Attribute("Background")?.Value);
        if (background.HasValue)
            scrollPanel.Background = background.Value;

        // Border
        var border = ParseBorder(element);
        if (border is not null)
            scrollPanel.Border = border;

        // Content size
        var contentSize = ParseVector2(element.Attribute("ContentSize")?.Value) ?? new Vector2(0, 0);
        if (contentSize != Vector2.Zero)
            scrollPanel.ContentSize = contentSize;

        // Scroll behavior
        var scrollSpeed = ParseFloat(element.Attribute("ScrollSpeed")?.Value);
        if (scrollSpeed.HasValue)
            scrollPanel.ScrollSpeed = scrollSpeed.Value;

        var enableHorizontal = ParseBool(element.Attribute("EnableHorizontalScroll")?.Value);
        if (enableHorizontal.HasValue)
            scrollPanel.EnableHorizontalScroll = enableHorizontal.Value;

        var enableVertical = ParseBool(element.Attribute("EnableVerticalScroll")?.Value);
        if (enableVertical.HasValue)
            scrollPanel.EnableVerticalScroll = enableVertical.Value;

        var scrollbarWidth = ParseInt(element.Attribute("ScrollbarWidth")?.Value);
        if (scrollbarWidth.HasValue)
            scrollPanel.ScrollbarWidth = scrollbarWidth.Value;

        var scrollbarPadding = ParseInt(element.Attribute("ScrollbarPadding")?.Value);
        if (scrollbarPadding.HasValue)
            scrollPanel.ScrollbarPadding = scrollbarPadding.Value;

        return scrollPanel;
    }

    private static SubScene CreateSubScene(XElement element, IScene scene)
    {
        var name = element.Attribute("Name")?.Value;
        var subScene = new SubScene(name);

        // Background
        var background = ParseColor(element.Attribute("Background")?.Value);
        if (background.HasValue)
            subScene.Background = background.Value;

        // Border
        var border = ParseBorder(element);
        if (border is not null)
            subScene.Border = border;

        return subScene;
    }

    private static SubSceneContainer CreateSubSceneContainer(XElement element, IScene scene)
    {
        var name = element.Attribute("Name")?.Value;
        var container = new SubSceneContainer(name);

        // Background
        var background = ParseColor(element.Attribute("Background")?.Value);
        if (background.HasValue)
            container.Background = background.Value;

        // Border
        var border = ParseBorder(element);
        if (border is not null)
            container.Border = border;

        // Parse SubScene children from the SubScenes element
        var subScenesElement = element.Element("SubScenes");
        if (subScenesElement is not null)
        {
            foreach (var subSceneElement in subScenesElement.Elements("SubScene"))
            {
                var subScene = CreateSubScene(subSceneElement, scene);
                ApplyCommonProperties(subScene, subSceneElement);

                // Register by name before parsing children
                var subSceneName = subSceneElement.Attribute("Name")?.Value;
                if (!string.IsNullOrEmpty(subSceneName))
                    _namedComponents[subSceneName] = subScene;

                // Parse children inside the SubScene
                var childrenElement = subSceneElement.Element("Children");
                if (childrenElement is not null)
                {
                    var children = ParseChildren(childrenElement, scene);
                    foreach (var child in children)
                        subScene.Children.Add(child);
                }

                container.AddSubScene(subScene);
            }
        }

        // Active index
        var activeIndex = ParseInt(element.Attribute("ActiveIndex")?.Value);
        if (activeIndex.HasValue && activeIndex.Value >= 0)
            container.SetActiveSubScene(activeIndex.Value);

        return container;
    }

    private static Sprite CreateSprite(XElement element)
    {
        var sprite = new Sprite();

        // Texture from atlas
        var textureName = element.Attribute("Texture")?.Value;
        if (!string.IsNullOrEmpty(textureName))
            sprite.SetFromAtlas(ParseSprite(textureName));

        // Tint
        var tint = ParseColor(element.Attribute("Tint")?.Value);
        if (tint.HasValue)
            sprite.Tint = tint.Value;

        return sprite;
    }

    private static PixelLabel? CreatePixelLabel(XElement element)
    {
        // Atlas attribute specifies which preset atlas/map to use
        var atlasName = element.Attribute("Atlas")?.Value;
        if (string.IsNullOrEmpty(atlasName))
            return null;

        // Get atlas and character map based on preset name
        SpriteAtlas? atlas = null;
        Dictionary<char, string>? charMap = null;

        // Look for the atlas type dynamically
        var atlasTypeName = atlasName + "SpriteAtlas";
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            var atlasType = assembly.GetTypes()
                .FirstOrDefault(t => t.Name.Equals(atlasTypeName, StringComparison.OrdinalIgnoreCase) &&
                                     typeof(SpriteAtlas).IsAssignableFrom(t));

            if (atlasType is not null)
            {
                atlas = Activator.CreateInstance(atlasType) as SpriteAtlas;
                break;
            }
        }

        if (atlas is null)
            return null;

        // Determine character map based on atlas name
        charMap = atlasName.ToLowerInvariant() switch
        {
            "pixelfont" => PixelLabel.CreatePixelfontMap(),
            "scoreboard" => PixelLabel.CreateScoreboardMap(),
            _ => PixelLabel.CreatePixelfontMap() // Default to pixelfont map
        };

        var pixelLabel = new PixelLabel(null, atlas, charMap);

        // Text
        var text = element.Attribute("Text")?.Value;
        if (!string.IsNullOrEmpty(text))
            pixelLabel.Text = text;

        // Tint
        var tint = ParseColor(element.Attribute("Tint")?.Value);
        if (tint.HasValue)
            pixelLabel.Tint = tint.Value;

        // CharScale
        var charScale = ParseVector2(element.Attribute("CharScale")?.Value);
        if (charScale.HasValue)
            pixelLabel.CharScale = charScale.Value;

        // Spacing
        var spacing = ParseInt(element.Attribute("Spacing")?.Value);
        if (spacing.HasValue)
            pixelLabel.Spacing = spacing.Value;

        // Alignment
        var alignment = ParseAttribute<TextAlignment>(element, "Alignment", TextAlignment.Left);
        pixelLabel.Alignment = alignment;

        // MaxWidth
        var maxWidth = ParseInt(element.Attribute("MaxWidth")?.Value);
        if (maxWidth.HasValue)
            pixelLabel.MaxWidth = maxWidth.Value;

        return pixelLabel;
    }

    private static BitmapLabel CreateBitmapLabel(XElement element)
    {
        var bitmapLabel = new BitmapLabel();

        // Text
        var text = element.Attribute("Text")?.Value;
        if (!string.IsNullOrEmpty(text))
            bitmapLabel.Text = text;

        // Font properties
        var fontFamily = element.Attribute("FontFamily")?.Value;
        if (!string.IsNullOrEmpty(fontFamily))
            bitmapLabel.FontFamily = fontFamily;

        var fontSize = ParseFloat(element.Attribute("FontSize")?.Value);
        if (fontSize.HasValue)
            bitmapLabel.FontSize = fontSize.Value;

        var fontStyle = element.Attribute("FontStyle")?.Value;
        if (!string.IsNullOrEmpty(fontStyle))
        {
            if (Enum.TryParse<System.Drawing.FontStyle>(fontStyle, true, out var style))
                bitmapLabel.FontStyle = style;
        }

        // Colors
        var tint = ParseColor(element.Attribute("Tint")?.Value);
        if (tint.HasValue)
            bitmapLabel.Tint = tint.Value;

        var textColor = ParseColor(element.Attribute("TextColor")?.Value);
        if (textColor.HasValue)
            bitmapLabel.TextColor = textColor.Value;

        var background = ParseColor(element.Attribute("Background")?.Value);
        if (background.HasValue)
            bitmapLabel.Background = background.Value;

        // Padding
        var padding = ParseInt(element.Attribute("Padding")?.Value);
        if (padding.HasValue)
            bitmapLabel.Padding = padding.Value;

        // Alignment
        var alignment = ParseAttribute<TextAlignment>(element, "Alignment", TextAlignment.Left);
        bitmapLabel.Alignment = alignment;

        // MaxWidth
        var maxWidth = ParseInt(element.Attribute("MaxWidth")?.Value);
        if (maxWidth.HasValue)
            bitmapLabel.MaxWidth = maxWidth.Value;

        // Border
        var border = ParseBorder(element);
        if (border is not null)
            bitmapLabel.Border = border;

        return bitmapLabel;
    }

    private static AnimatedSprite? CreateAnimatedSprite(XElement element)
    {
        var animSprite = new AnimatedSprite();

        // FrameDelayMs
        var frameDelay = ParseFloat(element.Attribute("FrameDelayMs")?.Value);
        if (frameDelay.HasValue)
            animSprite.FrameDelayMs = frameDelay.Value;

        // Loop
        var loop = ParseBool(element.Attribute("Loop")?.Value);
        if (loop.HasValue)
            animSprite.Loop = loop.Value;

        // Reverse
        var reverse = ParseBool(element.Attribute("Reverse")?.Value);
        if (reverse.HasValue)
            animSprite.Reverse = reverse.Value;

        // Tint
        var tint = ParseColor(element.Attribute("Tint")?.Value);
        if (tint.HasValue)
            animSprite.Tint = tint.Value;

        // AutoStart - whether to call Play() after loading
        var autoStart = ParseBool(element.Attribute("AutoStart")?.Value);
        if (autoStart.HasValue)
            animSprite.AutoStart = autoStart.Value;

        // Frames - comma-separated list of sprite references (e.g., "Pokemon._0001,Pokemon._0004,Pokemon._0007")
        var frames = element.Attribute("Frames")?.Value;
        if (!string.IsNullOrEmpty(frames))
        {
            var frameList = frames.Split(',').Select(s => s.Trim());
            foreach (var frame in frameList)
            {
                var textureResult = ParseSprite(frame);
                if (textureResult is not null)
                    animSprite.AddFrame(textureResult);
            }
        }

        // If AutoStart is true and we have frames, play the animation
        if (animSprite.AutoStart && animSprite.FrameCount > 0)
            animSprite.Play();

        return animSprite;
    }

    /// <summary>
    /// Handle Include element for reusable component templates.
    /// </summary>
    private static BaseComponent? CreateInclude(XElement element, IScene scene)
    {
        var src = element.Attribute("Src")?.Value;
        if (string.IsNullOrEmpty(src))
            return null;

        // Get override attributes (all attributes except Src)
        var overrides = element.Attributes()
            .Where(a => a.Name.LocalName != "Src")
            .ToDictionary(a => a.Name.LocalName, a => a.Value);

        // Get nested property overrides from child elements (e.g., <card_title Text="Custom Title"/>)
        var nestedOverrides = element.Elements()
            .Where(e => !IsSpecialElement(e.Name.LocalName))
            .ToDictionary(
                e => e.Name.LocalName,
                e => e.Attributes().ToDictionary(a => a.Name.LocalName, a => a.Value)
            );

        return LoadTemplate(src, scene, overrides, nestedOverrides);
    }

    /// <summary>
    /// Load a template file and create a component with optional overrides.
    /// Can be called programmatically from code to create template instances.
    /// </summary>
    /// <param name="templateName">Template filename (e.g., "Card.xml")</param>
    /// <param name="scene">The scene to bind events to</param>
    /// <param name="overrides">Dictionary of attribute overrides for the root component</param>
    /// <param name="nestedOverrides">Dictionary of child component overrides (key = component name, value = attribute overrides)</param>
    /// <returns>The created component or null if loading fails</returns>
    public static BaseComponent? LoadTemplate(
        string templateName,
        IScene scene,
        Dictionary<string, string>? overrides = null,
        Dictionary<string, Dictionary<string, string>>? nestedOverrides = null)
    {
        // Resolve path relative to Content/Scenes/Templates/
        var basePath = System.IO.Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Content", "Scenes", "Templates");
        var templatePath = System.IO.Path.Combine(basePath, templateName);

        if (!System.IO.File.Exists(templatePath))
        {
            System.Diagnostics.Debug.WriteLine($"Template not found: {templatePath}");
            return null;
        }

        try
        {
            var templateContent = System.IO.File.ReadAllText(templatePath);
            var templateRoot = XElement.Parse(templateContent);

            // The template should have a single root component element
            var componentElement = templateRoot.Elements().FirstOrDefault();
            if (componentElement is null)
                return null;

            // Apply root-level overrides
            if (overrides is not null)
            {
                foreach (var (key, value) in overrides)
                {
                    var existingAttr = componentElement.Attribute(key);
                    if (existingAttr is not null)
                        existingAttr.Value = value;
                    else
                        componentElement.Add(new XAttribute(key, value));
                }
            }

            // Apply nested overrides to child components by name
            if (nestedOverrides is not null)
                ApplyNestedOverrides(componentElement, nestedOverrides);

            return CreateComponent(componentElement, scene);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to load template {templateName}: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Recursively apply overrides to named child components.
    /// </summary>
    private static void ApplyNestedOverrides(
        XElement element,
        Dictionary<string, Dictionary<string, string>> overrides)
    {
        // Check if this element matches any override by Name attribute
        var name = element.Attribute("Name")?.Value;
        if (!string.IsNullOrEmpty(name) && overrides.TryGetValue(name, out var attrs))
        {
            foreach (var (key, value) in attrs)
            {
                var existingAttr = element.Attribute(key);
                if (existingAttr is not null)
                    existingAttr.Value = value;
                else
                    element.Add(new XAttribute(key, value));
            }
        }

        // Recurse into children (both direct and inside Children element)
        foreach (var child in element.Elements())
            ApplyNestedOverrides(child, overrides);
    }

    #endregion

    #region Parsing Helpers

    /// <summary>
    /// Parse a Vector2 from string (e.g., "100,200" or "100" for uniform).
    /// </summary>
    private static Vector2? ParseVector2(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        var parts = value.Split(',');

        var width = parts.Length >= 1 ? ParseInt(parts[0]) ?? 0 : 0;
        var height = parts.Length >= 2 ? ParseInt(parts[1]) ?? 0 : 0;

        return new Vector2(width, parts.Length >= 2 ? height : width);
    }

    /// <summary>
    /// Parse an Anchor from string (e.g., "100,200" or "100,200,relative_to").
    /// </summary>
    private static Anchor ParseAnchor(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return new Anchor(new Vector2(0, 0));

        var parts = value.Split(',');

        var x = parts.Length >= 1 ? ParseInt(parts[0]) ?? 0 : 0;
        var y = parts.Length >= 2 ? ParseInt(parts[1]) ?? 0 : 0;
        var anchor = parts.Length >= 3 ? parts[2] : null;

        if (!string.IsNullOrEmpty(anchor) && _namedComponents.TryGetValue(anchor, out var relativeComponent))
            return new Anchor(new Vector2(x, y), relativeComponent.Position);

        // Absolute position
        return new Anchor(new Vector2(x, y));
    }

    /// <summary>
    /// Parse a Color from string (e.g., "Red", "255,128,0", "255,128,0,255", or "#FF8000").
    /// </summary>
    private static Color? ParseColor(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        // Try named color
        var colorType = typeof(Color);
        var property = colorType.GetProperty(value, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
        if (property is not null)
            return (Color?)property.GetValue(null);

        // Try hex color
        if (value.StartsWith('#'))
        {
            var hex = value.Substring(1);
            if (hex.Length == 6)
            {
                if (int.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var rgb))
                {
                    return new Color(
                        (rgb >> 16) & 0xFF,
                        (rgb >> 8) & 0xFF,
                        rgb & 0xFF
                    );
                }
            }
            else if (hex.Length == 8)
            {
                if (uint.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var rgba))
                {
                    return new Color(
                        (int)((rgba >> 24) & 0xFF),
                        (int)((rgba >> 16) & 0xFF),
                        (int)((rgba >> 8) & 0xFF),
                        (int)(rgba & 0xFF)
                    );
                }
            }
        }

        // Try RGB/RGBA values
        var parts = value.Split(',');
        if (parts.Length == 3)
        {
            if (int.TryParse(parts[0].Trim(), out var r) &&
                int.TryParse(parts[1].Trim(), out var g) &&
                int.TryParse(parts[2].Trim(), out var b))
                return new Color(r, g, b);
        }
        else if (parts.Length == 4)
        {
            if (int.TryParse(parts[0].Trim(), out var r) &&
                int.TryParse(parts[1].Trim(), out var g) &&
                int.TryParse(parts[2].Trim(), out var b) &&
                int.TryParse(parts[3].Trim(), out var a))
                return new Color(r, g, b, a);
        }

        return null;
    }

    /// <summary>
    /// Parse a sprite from atlas notation (e.g., "Arrows.Arrow_left").
    /// </summary>
    private static TextureResult? ParseSprite(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        // Parse "AtlasName.SpriteName" format
        var parts = value.Split('.');
        if (parts.Length != 2)
            return null;

        var atlasName = parts[0];
        var spriteName = parts[1];

        // Get the atlas type dynamically
        return GetTextureFromAtlas(atlasName, spriteName);
    }

    /// <summary>
    /// Get texture from atlas by name using reflection.
    /// </summary>
    private static TextureResult? GetTextureFromAtlas(string atlasName, string spriteName)
    {
        // Try to find registered atlas
        // The atlases are registered via ContentHelper.RegisterAtlas<T>()
        // We need to look up the atlas by its name

        // Common pattern: AtlasName + "SpriteAtlas" e.g., "Arrows" -> "ArrowsSpriteAtlas"
        var atlasTypeName = atlasName + "SpriteAtlas";

        // Search through loaded assemblies for the atlas type
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            var atlasType = assembly.GetTypes()
                .FirstOrDefault(t => t.Name.Equals(atlasTypeName, StringComparison.OrdinalIgnoreCase) &&
                                     typeof(SpriteAtlas).IsAssignableFrom(t));

            if (atlasType is not null)
            {
                // Use ContentHelper.GetTextureResult<T> via reflection
                var method = typeof(ContentHelper).GetMethod("GetTextureResult")?.MakeGenericMethod(atlasType);
                if (method is not null)
                {
                    var result = method.Invoke(null, new object[] { spriteName.ToLowerInvariant() });
                    return result as TextureResult;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Parse a Border from a child element.
    /// </summary>
    private static Border? ParseBorder(XElement parent, string elementName = "Border")
    {
        var borderElement = parent.Element(elementName);
        if (borderElement is null)
            return null;

        var thickness = ParseInt(borderElement.Attribute("Thickness")?.Value) ?? 0;
        var color = ParseColor(borderElement.Attribute("Color")?.Value) ?? Color.Transparent;

        return new Border(thickness, color);
    }

    /// <summary>
    /// Parse an enum attribute.
    /// </summary>
    private static T ParseAttribute<T>(XElement element, string attributeName, T defaultValue)
    {
        var value = element.Attribute(attributeName)?.Value;
        if (string.IsNullOrEmpty(value))
            return defaultValue;

        if (typeof(T).IsEnum || (Nullable.GetUnderlyingType(typeof(T))?.IsEnum ?? false))
        {
            var enumType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
            if (Enum.TryParse(enumType, value, true, out var result))
                return (T)result!;
        }

        return defaultValue;
    }

    private static float? ParseFloat(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return null;
        if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
            return result;
        return null;
    }

    private static float ParseFloat(string? value, float defaultValue)
        => ParseFloat(value) ?? defaultValue;

    private static int? ParseInt(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return null;
        if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result))
            return result;
        return null;
    }

    private static bool? ParseBool(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return null;
        if (bool.TryParse(value, out var result))
            return result;
        // Also support "1" and "0"
        if (value == "1")
            return true;
        if (value == "0")
            return false;
        return null;
    }

    #endregion
}
