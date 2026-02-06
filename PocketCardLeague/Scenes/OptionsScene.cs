using Components;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PocketCardLeague.Consts;
using PocketCardLeague.Enums;
using PocketCardLeague.SpriteMaps;
using System.Linq;

namespace PocketCardLeague.Scenes;

public class OptionsScene() : Scene<SceneType>(SceneType.Options)
{
    public override void Initialize()
    {
        var locations = new LocationsSpriteAtlas();

        BackgroundColor = new Color(25, 25, 25, 255);
        BackgroundMode = SceneBackgroundMode.Sprite;
        BackgroundSprite = LocationsSpriteAtlas.Cave_11;
        BackgroundOpacity = 0.25f;

        // Virtual resolution is 2048x1152, downscaled to window

        var btnRight = new SpriteButton("button_to_main",
            new Anchor(new Vector2(1852, 508)));

        btnRight.SetNormalSprite(ArrowsSpriteAtlas.Arrow_right);
        btnRight.SetHoveredSprite(ArrowsSpriteAtlas.Arrow_right);
        btnRight.SetPressedSprite(ArrowsSpriteAtlas.Arrow_right_active);
        btnRight.Scale = new Vector2(4, 4);
        btnRight.Bob = BobDirection.Right;
        btnRight.Opacity = 0.8f;
        btnRight.OnHoveredEnter += () => btnRight.Opacity = 1f;
        btnRight.OnHoveredExit += () => btnRight.Opacity = 0.8f;
        btnRight.OnClicked += () => SceneManager.SetActiveScene(SceneType.Main, new SlideTransition(SlideDirection.Left));
        btnRight.Children.Add(new Label("main_label", "Main", Fonts.Header,
            new Anchor(new Vector2(14, 60), btnRight.Position), true)
        { Border = new Border(4, new Color(25, 25, 25, 170)) });
        AddComponent(btnRight);

        var title = new Label("options_label", "Options", Fonts.Header, new Anchor(new Vector2(96, 64)))
        {
            Color = Color.White,
            Border = new Border(4, new Color(25, 25, 25, 170))
        };
        AddComponent(title);

        var panel_graphics = new Panel("graphics_panel")
        {
            Position = new Anchor(new Vector2(64, 160)),
            Size = new Vector2(896, 256),
            Background = new Color(20, 20, 20, 200),
            Border = new Border(4, Color.Gray)
        };
        AddComponent(panel_graphics);

        var panel_graphics_title = new Label("graphics_panel_title", "Graphics", Fonts.Header,
            new Anchor(new Vector2(24, 4), panel_graphics.Position))
        {
            Color = Color.White,
            Border = new Border(4, new Color(25, 25, 25, 170))
        };
        panel_graphics.Children.Add(panel_graphics_title);
        
        // Window scale dropdown - bound to SettingsManager
        var scaleDropdown = new Dropdown("scale_dropdown", new Anchor(new Vector2(0, 80), panel_graphics_title.Position), new Vector2(480, 56))
        {
            Font = Fonts.Default,
            Placeholder = "Scale...",
            MaxVisibleItems = 4,
            Padding = 16,
            ArrowWidth = 56,
            ArrowSize = 20,
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
        panel_graphics.Children.Add(scaleDropdown);

        // Fullscreen checkbox - bound to SettingsManager
        var fullscreenCheckbox = new Checkbox("fullscreen_checkbox", new Anchor(new Vector2(0, 80), scaleDropdown.Position))
        {
            Label = "Fullscreen",
            Font = Fonts.Default,
            LabelColor = Color.White,
            IsChecked = SettingsManager.Current.Fullscreen,
            BoxBackground = new Color(60, 60, 60),
            BoxBorderColor = Color.Gray,
            CheckmarkColor = Color.LimeGreen,
            BoxSize = 40,
            BoxBorderThickness = 4,
            LabelSpacing = 20
        };
        fullscreenCheckbox.OnCheckedChanged += () =>
        {
            SettingsManager.SetFullscreen(fullscreenCheckbox.IsChecked);
        };
        panel_graphics.Children.Add(fullscreenCheckbox);

        var panel_audio = new Panel("audio_panel")
        {
            Position = new Anchor(new Vector2(0, 40 + panel_graphics.Size.Y), panel_graphics.Position),
            Size = new Vector2(896, 344),
            Background = new Color(20, 20, 20, 200),
            Border = new Border(4, Color.Gray)
        };
        AddComponent(panel_audio);

        var panel_audio_title = new Label("audio_panel_title", "Audio", Fonts.Header,
            new Anchor(new Vector2(24, 4), panel_audio.Position))
        {
            Color = Color.White,
            Border = new Border(4, new Color(25, 25, 25, 170))
        };
        panel_audio.Children.Add(panel_audio_title);

        // Master volume slider
        var masterLabel = new Label("master_label", "Master", Fonts.Default, new Anchor(new Vector2(0, 80), panel_audio_title.Position))
        {
            Color = Color.White
        };
        var masterSlider = new Slider("master_volume", new Anchor(new Vector2(180, -12), masterLabel.Position), new Vector2(520, 64))
        {
            MinValue = 0,
            MaxValue = 100,
            Value = SettingsManager.Current.MasterVolume,
            Font = Fonts.Default,
            ShowValue = true,
            TrackFillColor = Color.CornflowerBlue,
            TrackHeight = 24,
            ThumbSize = new Vector2(36, 44),
            TrackBorder = new Border(4, Color.Gray),
            ThumbBorder = new Border(4, Color.DarkGray)
        };
        masterSlider.OnValueChanged += (value) => SettingsManager.SetMasterVolume(value);

        panel_audio.Children.Add(masterLabel);
        panel_audio.Children.Add(masterSlider);

        // Music volume slider
        var musicLabel = new Label("music_label", "Music", Fonts.Default, new Anchor(new Vector2(0, 60), masterLabel.Position))
        {
            Color = Color.White
        };
        var musicSlider = new Slider("music_volume", new Anchor(new Vector2(180, -12), musicLabel.Position), new Vector2(520, 64))
        {
            MinValue = 0,
            MaxValue = 100,
            Value = SettingsManager.Current.MusicVolume,
            Font = Fonts.Default,
            ShowValue = true,
            TrackFillColor = Color.Orange,
            TrackHeight = 24,
            ThumbSize = new Vector2(36, 44),
            TrackBorder = new Border(4, Color.Gray),
            ThumbBorder = new Border(4, Color.DarkGray)
        };
        musicSlider.OnValueChanged += (value) => SettingsManager.SetMusicVolume(value);

        panel_audio.Children.Add(musicLabel);
        panel_audio.Children.Add(musicSlider);

        // SFX volume slider
        var sfxLabel = new Label("sfx_label", "SFX", Fonts.Default, new Anchor(new Vector2(0, 60), musicLabel.Position))
        {
            Color = Color.White
        };
        var sfxSlider = new Slider("sfx_volume", new Anchor(new Vector2(180, -12), sfxLabel.Position), new Vector2(520, 64))
        {
            MinValue = 0,
            MaxValue = 100,
            Value = SettingsManager.Current.SfxVolume,
            Font = Fonts.Default,
            ShowValue = true,
            TrackFillColor = Color.LimeGreen,
            TrackHeight = 24,
            ThumbSize = new Vector2(36, 44),
            TrackBorder = new Border(4, Color.Gray),
            ThumbBorder = new Border(4, Color.DarkGray)
        };
        sfxSlider.OnValueChanged += (value) => SettingsManager.SetSfxVolume(value);

        panel_audio.Children.Add(sfxLabel);
        panel_audio.Children.Add(sfxSlider);

        var panel_gameplay = new Panel("gameplay_panel")
        {
            Position = new Anchor(new Vector2(0, 40 + panel_audio.Size.Y), panel_audio.Position),
            Size = new Vector2(896, 256),
            Background = new Color(20, 20, 20, 200),
            Border = new Border(4, Color.Gray)
        }; 
        AddComponent(panel_gameplay);

        var panel_gameplay_title = new Label("gameplay_panel_title", "Gameplay", Fonts.Header,
            new Anchor(new Vector2(24, 4), panel_gameplay.Position))
        {
            Color = Color.White,
            Border = new Border(4, new Color(25, 25, 25, 170))
        };
        panel_gameplay.Children.Add(panel_gameplay_title);

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
