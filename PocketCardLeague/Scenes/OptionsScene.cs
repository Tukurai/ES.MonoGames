using Components;
using Helpers;
using Microsoft.Xna.Framework;
using PocketCardLeague.Enums;

namespace PocketCardLeague.Scenes;

public class OptionsScene() : Scene<SceneType>(SceneType.Options)
{
    public override void Initialize()
    {
        BackgroundColor = Color.Black;

        // Virtual resolution is 512x288 (default window scale 3x = 1536x864)
        var font = ContentHelper.LoadFont("DefaultFont");

        var title = new Label("title", "Settings", ContentHelper.LoadFont("TitleFont"), new Anchor(new Vector2(0, 10), null), true, 512)
        {
            Border = new Border(1, Color.Black),
        };

        // Graphics settings
        var graphicsLabel = new Label("graphics_label", "Graphics", font, new Anchor(new Vector2(24, 50)))
        {
            Color = Color.Gray
        };

        // Window scale dropdown - bound to SettingsManager
        var scaleDropdown = new Dropdown("scale_dropdown", new Anchor(new Vector2(24, 70)), new Vector2(120, 24))
        {
            Font = font,
            Placeholder = "Scale...",
            MaxVisibleItems = 4,
            Padding = 4
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
                SettingsManager.SetWindowScale(SettingsManager.AvailableScales[index].Scale);
            }
        };

        // Fullscreen checkbox - bound to SettingsManager
        var fullscreenCheckbox = new Checkbox("fullscreen_checkbox", new Anchor(new Vector2(24, 110)))
        {
            Label = "Fullscreen",
            Font = font,
            LabelColor = Color.White,
            IsChecked = SettingsManager.Current.Fullscreen,
            BoxBackground = new Color(60, 60, 60),
            CheckmarkColor = Color.LimeGreen,
            BoxSize = 16,
            LabelSpacing = 8
        };
        fullscreenCheckbox.OnCheckedChanged += () =>
        {
            SettingsManager.SetFullscreen(fullscreenCheckbox.IsChecked);
        };

        // Audio settings
        var audioLabel = new Label("audio_label", "Audio", font, new Anchor(new Vector2(280, 50)))
        {
            Color = Color.Gray
        };

        // Master volume slider
        var masterLabel = new Label("master_label", "Master", font, new Anchor(new Vector2(280, 80)))
        {
            Color = Color.White
        };
        var masterSlider = new Slider("master_volume", new Anchor(new Vector2(350, 80)), new Vector2(130, 16))
        {
            MinValue = 0,
            MaxValue = 100,
            Value = SettingsManager.Current.MasterVolume,
            Font = font,
            ShowValue = false,
            TrackFillColor = Color.CornflowerBlue,
            TrackHeight = 6,
            ThumbWidth = 12,
            ThumbHeight = 16
        };
        masterSlider.OnValueChanged += (value) => SettingsManager.SetMasterVolume(value);

        // Music volume slider
        var musicLabel = new Label("music_label", "Music", font, new Anchor(new Vector2(280, 110)))
        {
            Color = Color.White
        };
        var musicSlider = new Slider("music_volume", new Anchor(new Vector2(350, 110)), new Vector2(130, 16))
        {
            MinValue = 0,
            MaxValue = 100,
            Value = SettingsManager.Current.MusicVolume,
            Font = font,
            ShowValue = false,
            TrackFillColor = Color.Orange,
            TrackHeight = 6,
            ThumbWidth = 12,
            ThumbHeight = 16
        };
        musicSlider.OnValueChanged += (value) => SettingsManager.SetMusicVolume(value);

        // SFX volume slider
        var sfxLabel = new Label("sfx_label", "SFX", font, new Anchor(new Vector2(280, 140)))
        {
            Color = Color.White
        };
        var sfxSlider = new Slider("sfx_volume", new Anchor(new Vector2(350, 140)), new Vector2(130, 16))
        {
            MinValue = 0,
            MaxValue = 100,
            Value = SettingsManager.Current.SfxVolume,
            Font = font,
            ShowValue = false,
            TrackFillColor = Color.LimeGreen,
            TrackHeight = 6,
            ThumbWidth = 12,
            ThumbHeight = 16
        };
        sfxSlider.OnValueChanged += (value) => SettingsManager.SetSfxVolume(value);

        // Back button
        var button = new Button("back_button", "Back", font, new Anchor(new Vector2(196, 250), null), new Vector2(120, 30), true)
        {
            Background = Color.Green,
            Border = new Border(1, Color.Black),
            TextBorder = new Border(1, Color.Black)
        };

        button.OnHoveredEnter += () => button.Background = Color.LightGreen;
        button.OnHoveredExit += () => button.Background = Color.Green;
        button.OnClicked += () => SceneManager.SetActiveScene(SceneType.Title);

        var nameInput = new InputField(
            name: "name_input",
            placeholderText: "Enter your name...",
            font: ContentHelper.LoadFont("DefaultFont"),
            position: new Anchor(new Vector2(150, 175)),
            size: new Vector2(250, 32))
        {
            Background = new Color(40, 40, 40),
            Border = new Border(1, Color.Gray),
            FocusedBorder = new Border(2, Color.CornflowerBlue),
            TextColor = Color.White,
            PlaceholderColor = Color.DarkGray
        };

        nameInput.OnTextChanged += (text) => System.Diagnostics.Debug.WriteLine($"Name changed: {text}");
        nameInput.OnSubmit += () => System.Diagnostics.Debug.WriteLine($"Name submitted: {nameInput.Text}");


        AddComponent(title);
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
}
