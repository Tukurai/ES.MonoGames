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

        // Virtual resolution is 1536x864 (3x of 512x288)
        var font = ContentHelper.LoadFont("DefaultFont");

        var title = new Label("title", "Settings", ContentHelper.LoadFont("TitleFont"), new Anchor(new Vector2(0, 30), null), true, 1536)
        {
            Border = new Border(3, Color.Black),
        };

        // Graphics settings
        var graphicsLabel = new Label("graphics_label", "Graphics", font, new Anchor(new Vector2(72, 150)))
        {
            Color = Color.Gray
        };

        // Window scale dropdown - bound to SettingsManager
        var scaleDropdown = new Dropdown("scale_dropdown", new Anchor(new Vector2(72, 210)), new Vector2(360, 72))
        {
            Font = font,
            Placeholder = "Scale...",
            MaxVisibleItems = 4,
            Padding = 12
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
        var fullscreenCheckbox = new Checkbox("fullscreen_checkbox", new Anchor(new Vector2(72, 330)))
        {
            Label = "Fullscreen",
            Font = font,
            LabelColor = Color.White,
            IsChecked = SettingsManager.Current.Fullscreen,
            BoxBackground = new Color(60, 60, 60),
            CheckmarkColor = Color.LimeGreen,
            BoxSize = 48,
            LabelSpacing = 24
        };
        fullscreenCheckbox.OnCheckedChanged += () =>
        {
            SettingsManager.SetFullscreen(fullscreenCheckbox.IsChecked);
        };

        // Audio settings
        var audioLabel = new Label("audio_label", "Audio", font, new Anchor(new Vector2(840, 150)))
        {
            Color = Color.Gray
        };

        // Master volume slider
        var masterLabel = new Label("master_label", "Master", font, new Anchor(new Vector2(840, 240)))
        {
            Color = Color.White
        };
        var masterSlider = new Slider("master_volume", new Anchor(new Vector2(1050, 240)), new Vector2(390, 48))
        {
            MinValue = 0,
            MaxValue = 100,
            Value = SettingsManager.Current.MasterVolume,
            Font = font,
            ShowValue = false,
            TrackFillColor = Color.CornflowerBlue,
            TrackHeight = 18,
            ThumbWidth = 36,
            ThumbHeight = 48
        };
        masterSlider.OnValueChanged += (value) => SettingsManager.SetMasterVolume(value);

        // Music volume slider
        var musicLabel = new Label("music_label", "Music", font, new Anchor(new Vector2(840, 330)))
        {
            Color = Color.White
        };
        var musicSlider = new Slider("music_volume", new Anchor(new Vector2(1050, 330)), new Vector2(390, 48))
        {
            MinValue = 0,
            MaxValue = 100,
            Value = SettingsManager.Current.MusicVolume,
            Font = font,
            ShowValue = false,
            TrackFillColor = Color.Orange,
            TrackHeight = 18,
            ThumbWidth = 36,
            ThumbHeight = 48
        };
        musicSlider.OnValueChanged += (value) => SettingsManager.SetMusicVolume(value);

        // SFX volume slider
        var sfxLabel = new Label("sfx_label", "SFX", font, new Anchor(new Vector2(840, 420)))
        {
            Color = Color.White
        };
        var sfxSlider = new Slider("sfx_volume", new Anchor(new Vector2(1050, 420)), new Vector2(390, 48))
        {
            MinValue = 0,
            MaxValue = 100,
            Value = SettingsManager.Current.SfxVolume,
            Font = font,
            ShowValue = false,
            TrackFillColor = Color.LimeGreen,
            TrackHeight = 18,
            ThumbWidth = 36,
            ThumbHeight = 48
        };
        sfxSlider.OnValueChanged += (value) => SettingsManager.SetSfxVolume(value);

        // Back button
        var button = new Button("back_button", "Back", font, new Anchor(new Vector2(588, 750), null), new Vector2(360, 90), true)
        {
            Background = Color.Green,
            Border = new Border(3, Color.Black),
            TextBorder = new Border(3, Color.Black)
        };

        button.OnHoveredEnter += () => button.Background = Color.LightGreen;
        button.OnHoveredExit += () => button.Background = Color.Green;
        button.OnClicked += () => SceneManager.SetActiveScene(SceneType.Title);

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

        base.Initialize();
    }
}
