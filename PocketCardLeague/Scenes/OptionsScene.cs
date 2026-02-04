using Components;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PocketCardLeague.Enums;
using PocketCardLeague.SpriteMaps;
using System.Linq;

namespace PocketCardLeague.Scenes;

public class OptionsScene() : Scene<SceneType>(SceneType.Options)
{
    public override void Initialize()
    {
        var locations = new LocationsSpriteAtlas();

        BackgroundMode = SceneBackgroundMode.Sprite;
        BackgroundSprite = locations.GetTextureFromAtlas(LocationsSpriteAtlas.Cave_11);

        // Virtual resolution is 2048x1152, downscaled to window
        var font = ContentHelper.LoadFont("DefaultFont");
        var arrowsAtlas = new ArrowsSpriteAtlas();

        var btnRight = new SpriteButton("button_to_main",
            new Anchor(new Vector2(1852, 508)));
        btnRight.SetNormalSprite(arrowsAtlas.GetTextureFromAtlas("arrow_right"));
        btnRight.SetHoveredSprite(arrowsAtlas.GetTextureFromAtlas("arrow_right"));
        btnRight.SetPressedSprite(arrowsAtlas.GetTextureFromAtlas("arrow_right_active"));
        btnRight.Scale = new Vector2(4, 4);
        btnRight.Bob = BobDirection.Right;
        btnRight.Opacity = 0.8f;
        btnRight.OnHoveredEnter += () => btnRight.Opacity = 1f;
        btnRight.OnHoveredExit += () => btnRight.Opacity = 0.8f;
        btnRight.OnClicked += () => SceneManager.SetActiveScene(SceneType.Main, new SlideTransition(SlideDirection.Left));
        btnRight.Children.Add(new Label("main_label", "Main", font,
            new Anchor(new Vector2(14, 60), btnRight.Position), true)
        { Border = new Border(4, Color.Black) });
        AddComponent(btnRight);

        // Graphics settings
        var graphicsLabel = new Label("graphics_label", "Graphics", font, new Anchor(new Vector2(96, 200)))
        {
            Color = Color.Gray
        };

        // Window scale dropdown - bound to SettingsManager
        var scaleDropdown = new Dropdown("scale_dropdown", new Anchor(new Vector2(96, 280)), new Vector2(480, 96))
        {
            Font = font,
            Placeholder = "Scale...",
            MaxVisibleItems = 4,
            Padding = 16,
            ArrowWidth = 64,
            ArrowSize = 24,
            TrianglePixelSize = 4,
            ScrollbarWidth = 12,
            Border = new Border(4, Color.Gray),
            FocusedBorder = new Border(4, Color.CornflowerBlue),
            ListBorder = new Border(4, Color.Gray)
        };
        foreach (var scale in SettingsManager.AvailableScales)
        {
            scaleDropdown.Items.Add(scale.Label);
        }
        scaleDropdown.SelectedIndex = SettingsManager.GetCurrentScaleIndex();
        scaleDropdown.OnSelectionChanged += (index) =>
        {
            if (index >= 0 && index < SettingsManager.AvailableScales.Length)
            {
                SettingsManager.SetWindowScale(index);
            }
        };

        // Fullscreen checkbox - bound to SettingsManager
        var fullscreenCheckbox = new Checkbox("fullscreen_checkbox", new Anchor(new Vector2(96, 440)))
        {
            Label = "Fullscreen",
            Font = font,
            LabelColor = Color.White,
            IsChecked = SettingsManager.Current.Fullscreen,
            BoxBackground = new Color(60, 60, 60),
            BoxBorderColor = Color.Gray,
            CheckmarkColor = Color.LimeGreen,
            BoxSize = 64,
            BoxBorderThickness = 4,
            LabelSpacing = 32
        };
        fullscreenCheckbox.OnCheckedChanged += () =>
        {
            SettingsManager.SetFullscreen(fullscreenCheckbox.IsChecked);
        };

        // Audio settings
        var audioLabel = new Label("audio_label", "Audio", font, new Anchor(new Vector2(1120, 200)))
        {
            Color = Color.Gray
        };

        // Master volume slider
        var masterLabel = new Label("master_label", "Master", font, new Anchor(new Vector2(1120, 320)))
        {
            Color = Color.White
        };
        var masterSlider = new Slider("master_volume", new Anchor(new Vector2(1400, 320)), new Vector2(520, 64))
        {
            MinValue = 0,
            MaxValue = 100,
            Value = SettingsManager.Current.MasterVolume,
            Font = font,
            ShowValue = false,
            TrackFillColor = Color.CornflowerBlue,
            TrackHeight = 24,
            ThumbWidth = 48,
            ThumbHeight = 64,
            TrackBorder = new Border(4, Color.Gray),
            ThumbBorder = new Border(4, Color.Black)
        };
        masterSlider.OnValueChanged += (value) => SettingsManager.SetMasterVolume(value);

        // Music volume slider
        var musicLabel = new Label("music_label", "Music", font, new Anchor(new Vector2(1120, 440)))
        {
            Color = Color.White
        };
        var musicSlider = new Slider("music_volume", new Anchor(new Vector2(1400, 440)), new Vector2(520, 64))
        {
            MinValue = 0,
            MaxValue = 100,
            Value = SettingsManager.Current.MusicVolume,
            Font = font,
            ShowValue = false,
            TrackFillColor = Color.Orange,
            TrackHeight = 24,
            ThumbWidth = 48,
            ThumbHeight = 64,
            TrackBorder = new Border(4, Color.Gray),
            ThumbBorder = new Border(4, Color.Black)
        };
        musicSlider.OnValueChanged += (value) => SettingsManager.SetMusicVolume(value);

        // SFX volume slider
        var sfxLabel = new Label("sfx_label", "SFX", font, new Anchor(new Vector2(1120, 560)))
        {
            Color = Color.White
        };
        var sfxSlider = new Slider("sfx_volume", new Anchor(new Vector2(1400, 560)), new Vector2(520, 64))
        {
            MinValue = 0,
            MaxValue = 100,
            Value = SettingsManager.Current.SfxVolume,
            Font = font,
            ShowValue = false,
            TrackFillColor = Color.LimeGreen,
            TrackHeight = 24,
            ThumbWidth = 48,
            ThumbHeight = 64,
            TrackBorder = new Border(4, Color.Gray),
            ThumbBorder = new Border(4, Color.Black)
        };
        sfxSlider.OnValueChanged += (value) => SettingsManager.SetSfxVolume(value);

        // Back button
        var button = new Button("back_button", "Back", font, new Anchor(new Vector2(784, 1000), null), new Vector2(480, 120), true)
        {
            Background = Color.Green,
            Border = new Border(4, Color.Black),
            TextBorder = new Border(4, Color.Black)
        };

        button.OnHoveredEnter += () => button.Background = Color.LightGreen;
        button.OnHoveredExit += () => button.Background = Color.Green;
        button.OnClicked += () => SceneManager.SetActiveScene(SceneType.Main, new SlideTransition(SlideDirection.Left));

        var nameInput = new InputField(
            name: "name_input",
            placeholderText: "Enter your name...",
            font: ContentHelper.LoadFont("DefaultFont"),
            position: new Anchor(new Vector2(600, 700)),
            size: new Vector2(600, 100))
        {
            Background = new Color(40, 40, 40),
            Border = new Border(4, Color.Gray),
            FocusedBorder = new Border(8, Color.CornflowerBlue),
            TextColor = Color.White,
            PlaceholderColor = Color.DarkGray,
            Padding = 24,
            CursorWidth = 4
        };

        nameInput.OnTextChanged += (text) => System.Diagnostics.Debug.WriteLine($"Name changed: {text}");
        nameInput.OnSubmit += () => System.Diagnostics.Debug.WriteLine($"Name submitted: {nameInput.Text}");


        AddComponent(graphicsLabel);
        AddComponent(scaleDropdown);
        AddComponent(fullscreenCheckbox);
        AddComponent(audioLabel);
        AddComponent(masterLabel);
        AddComponent(masterSlider);
        AddComponent(musicLabel);
        AddComponent(musicSlider);
        AddComponent(sfxLabel);
        AddComponent(sfxSlider);
        AddComponent(button);
        AddComponent(nameInput);

        base.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        var pressedKeys = ControlState.GetPressedKeys();
        if (pressedKeys.Contains(Keys.Right))
        {
            SceneManager.SetActiveScene(SceneType.Main, new SlideTransition(SlideDirection.Left));
            return;
        }

        base.Update(gameTime);
    }
}
