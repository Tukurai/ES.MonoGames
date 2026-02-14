using System.Linq;
using Components;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PocketCardLeague.Enums;
using PocketCardLeague.Helpers;

namespace PocketCardLeague.Scenes;

public class DebugScene() : XmlScene<SceneType>(SceneType.Debug)
{
    private const int TotalPages = 2;
    protected override string XmlFileName => "DebugScene.xml";

    private SubSceneContainer _pageContainer = null!;
    private Label _header = null!;

    protected override void OnXmlLoaded()
    {
        // Bind components from XML
        _header = Bind<Label>("page_header");
        _pageContainer = Bind<SubSceneContainer>("page_container");

        // Listen for page changes to update header
        _pageContainer.OnSubSceneChanged += index =>
        {
            _header.Text = $"Debug - Page {index + 1}/{TotalPages}";
        };

        // Bind events for interactive components
        BindPage1Events();
        BindPage2Events();
    }

    public override void Update(GameTime gameTime)
    {
        var pressedKeys = ControlState.GetPressedKeys();
        var heldKeys = ControlState.GetHeldKeys();
        bool altHeld = heldKeys.Contains(Keys.LeftAlt) || heldKeys.Contains(Keys.RightAlt);

        if (!altHeld && pressedKeys.Contains(Keys.Home))
        {
            SceneManager.SetActiveScene(SceneType.Title, new FadeTransition());
            return;
        }

        bool ctrlHeld = heldKeys.Contains(Keys.LeftControl) || heldKeys.Contains(Keys.RightControl);

        if (ctrlHeld && (pressedKeys.Contains(Keys.OemPlus) || pressedKeys.Contains(Keys.Add)))
        {
            var index = SettingsManager.GetCurrentScaleIndex();
            if (index < SettingsManager.AvailableScales.Length - 1)
                SettingsManager.SetWindowScale(index + 1);
            return;
        }

        if (ctrlHeld && (pressedKeys.Contains(Keys.OemMinus) || pressedKeys.Contains(Keys.Subtract)))
        {
            var index = SettingsManager.GetCurrentScaleIndex();
            if (index > 0)
                SettingsManager.SetWindowScale(index - 1);
            return;
        }

        // Navigate with slide transitions
        if (pressedKeys.Contains(Keys.Right) && !_pageContainer.IsTransitioning)
        {
            _pageContainer.Next(new SlideTransition(SlideDirection.Left, 0.3f));
            return;
        }

        if (pressedKeys.Contains(Keys.Left) && !_pageContainer.IsTransitioning)
        {
            _pageContainer.Previous(new SlideTransition(SlideDirection.Right, 0.3f));
            return;
        }

        base.Update(gameTime);
    }

    // ─── Event Binding ────────────────────────────────────────

    private void BindPage1Events()
    {
        var button = TryBind<Button>("p1_button");
        if (button is not null)
        {
            button.OnHoveredEnter += () => button.Background = new Color(70, 85, 110);
            button.OnHoveredExit += () => button.Background = new Color(50, 60, 80);
        }

        var deleteSave = TryBind<Button>("p1_delete_save");
        if (deleteSave is not null)
        {
            deleteSave.OnHoveredEnter += () => deleteSave.Background = new Color(150, 50, 50);
            deleteSave.OnHoveredExit += () => deleteSave.Background = new Color(120, 40, 40);
            deleteSave.OnClicked += () =>
            {
                var save = GameStateManager.ActiveSave;
                GameStateManager.Delete(save);
                GameStateManager.ActiveSave = new GameSave();
                deleteSave.Text = "Save Deleted!";
            };
        }
    }

    private void BindPage2Events()
    {
        var trackDropdown = TryBind<Dropdown>("p2_track_dropdown");
        var toggleButton = TryBind<Button>("p2_toggle");

        if (trackDropdown is not null && toggleButton is not null)
        {
            toggleButton.OnHoveredEnter += () => toggleButton.Background = SoundManager.IsPlaying
                ? new Color(110, 70, 70) : new Color(70, 110, 70);
            toggleButton.OnHoveredExit += () => toggleButton.Background = SoundManager.IsPlaying
                ? new Color(80, 50, 50) : new Color(50, 80, 50);

            trackDropdown.OnSelectionChanged += _ =>
            {
                if (trackDropdown.SelectedItem is not null)
                {
                    SoundManager.PlayTrack(trackDropdown.SelectedItem);
                    toggleButton.Text = "Stop Music";
                    toggleButton.Background = new Color(80, 50, 50);
                }
            };

            toggleButton.OnClicked += () =>
            {
                if (SoundManager.IsPlaying)
                {
                    SoundManager.StopTrack();
                    toggleButton.Text = "Play Music";
                    toggleButton.Background = new Color(50, 80, 50);
                }
                else if (trackDropdown.SelectedItem is not null)
                {
                    SoundManager.PlayTrack(trackDropdown.SelectedItem);
                    toggleButton.Text = "Stop Music";
                    toggleButton.Background = new Color(80, 50, 50);
                }
            };
        }

        // Volume sliders
        var masterSlider = TryBind<Slider>("p2_master_slider");
        var musicSlider = TryBind<Slider>("p2_music_slider");
        var sfxSlider = TryBind<Slider>("p2_sfx_slider");

        if (masterSlider is not null)
        {
            masterSlider.Value = SettingsManager.Current.MasterVolume;
            masterSlider.OnValueChanged += SettingsManager.SetMasterVolume;
        }

        if (musicSlider is not null)
        {
            musicSlider.Value = SettingsManager.Current.MusicVolume;
            musicSlider.OnValueChanged += SettingsManager.SetMusicVolume;
        }

        if (sfxSlider is not null)
        {
            sfxSlider.Value = SettingsManager.Current.SfxVolume;
            sfxSlider.OnValueChanged += SettingsManager.SetSfxVolume;
        }

        // Sound effect buttons
        var effects = new[]
        {
            "sn_card", "sn_click", "sn_click_2", "sn_coin", "sn_enterview",
            "sn_event", "sn_faint", "sn_flipcard", "sn_hurt", "sn_money",
            "sn_noise", "sn_rare", "sn_rare_2", "sn_text", "sn_upgrade"
        };

        foreach (var effect in effects)
        {
            var btn = TryBind<Button>($"p2_fx_{effect}");
            if (btn is not null)
            {
                var effectName = effect;
                btn.OnHoveredEnter += () => btn.Background = new Color(70, 85, 110);
                btn.OnHoveredExit += () => btn.Background = new Color(50, 60, 80);
                btn.OnClicked += () => SoundManager.PlayEffect(effectName);
            }
        }
    }
}
