using System.Collections.Generic;
using System.Linq;
using Components;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PocketCardLeague.Enums;
using PocketCardLeague.SpriteMaps;

namespace PocketCardLeague.Scenes;

public class DebugScene() : Scene<SceneType>(SceneType.Debug)
{
    private int _currentPage = 0;
    private const int TotalPages = 6;

    public override void Initialize()
    {
        BackgroundColor = new Color(20, 20, 30);

        var font = ContentHelper.LoadFont("DefaultFont");
        var titleFont = ContentHelper.LoadFont("TitleFont");

        // Page header
        var header = new Label("page_header",
            $"Debug - Page {_currentPage + 1}/{TotalPages}",
            titleFont,
            new Anchor(new Vector2(0, 24), null), true, 2048)
        {
            Border = new Border(4, Color.Black)
        };
        AddComponent(header);

        // Build current page
        switch (_currentPage)
        {
            case 0: BuildPage1(font); break;
            case 1: BuildPage2(font); break;
            case 2: BuildPage3(font); break;
            case 3: BuildPage4(font); break;
            case 4: BuildPage5(font); break;
            case 5: BuildPage6(font); break;
        }

        // Navigation footer
        var footer = new Label("nav_footer",
            "< > Navigate Pages | CTRL +/- Scale | HOME to return",
            font,
            new Anchor(new Vector2(0, 1080), null), true, 2048)
        {
            Color = Color.Gray
        };
        AddComponent(footer);

        base.Initialize();
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

        if (pressedKeys.Contains(Keys.Right))
        {
            _currentPage = (_currentPage + 1) % TotalPages;
            Reinitialize();
            return;
        }

        if (pressedKeys.Contains(Keys.Left))
        {
            _currentPage = (_currentPage - 1 + TotalPages) % TotalPages;
            Reinitialize();
            return;
        }

        base.Update(gameTime);
    }

    // ─── Page Builders ────────────────────────────────────────

    private void BuildPage1(SpriteFont font)
    {
        var sectionLabel = new Label("p1_section", "Text & Labels", font,
            new Anchor(new Vector2(96, 140)))
        { Color = Color.Gray };
        AddComponent(sectionLabel);

        // Regular Label
        var label = new Label("p1_label", "Regular SpriteFont Label", font,
            new Anchor(new Vector2(96, 220)))
        { Color = Color.White };
        AddComponent(label);

        // PixelLabel - Pixelfont left-aligned
        var pixelfontAtlas = new PixelfontSpriteAtlas();
        var pixelfontMap = PixelLabel.CreatePixelfontMap();

        var leftDesc = new Label("p1_left_desc", "PixelLabel (Pixelfont, Left)", font,
            new Anchor(new Vector2(96, 310)))
        { Color = Color.DarkGray };
        AddComponent(leftDesc);

        var pixelLeft = new PixelLabel("p1_pixel_left", pixelfontAtlas, pixelfontMap,
            new Anchor(new Vector2(96, 366)))
        {
            Text = "Left aligned pixel text!",
            CharScale = new Vector2(4, 4),
            Spacing = 0,
            Tint = Color.White
        };
        AddComponent(pixelLeft);

        // PixelLabel - Pixelfont center-aligned
        var centerDesc = new Label("p1_center_desc", "PixelLabel (Pixelfont, Center)", font,
            new Anchor(new Vector2(96, 460)))
        { Color = Color.DarkGray };
        AddComponent(centerDesc);

        var pixelCenter = new PixelLabel("p1_pixel_center", pixelfontAtlas, pixelfontMap,
            new Anchor(new Vector2(96, 516)))
        {
            Text = "Centered text",
            CharScale = new Vector2(4, 4),
            Spacing = 0,
            Alignment = TextAlignment.Center,
            MaxWidth = 1856,
            Tint = Color.CornflowerBlue
        };
        AddComponent(pixelCenter);

        // PixelLabel - Pixelfont right-aligned
        var rightDesc = new Label("p1_right_desc", "PixelLabel (Pixelfont, Right)", font,
            new Anchor(new Vector2(96, 610)))
        { Color = Color.DarkGray };
        AddComponent(rightDesc);

        var pixelRight = new PixelLabel("p1_pixel_right", pixelfontAtlas, pixelfontMap,
            new Anchor(new Vector2(96, 666)))
        {
            Text = "Right aligned",
            CharScale = new Vector2(4, 4),
            Spacing = 0,
            Alignment = TextAlignment.Right,
            MaxWidth = 1856,
            Tint = Color.LimeGreen
        };
        AddComponent(pixelRight);

        // PixelLabel - Scoreboard
        var scoreDesc = new Label("p1_score_desc", "PixelLabel (Scoreboard)", font,
            new Anchor(new Vector2(96, 780)))
        { Color = Color.DarkGray };
        AddComponent(scoreDesc);

        var scoreAtlas = new ScoreboardSpriteAtlas();
        var scoreMap = PixelLabel.CreateScoreboardMap();

        var scoreLabel = new PixelLabel("p1_score", scoreAtlas, scoreMap,
            new Anchor(new Vector2(96, 836)))
        {
            Text = "12/34",
            CharScale = new Vector2(6, 6),
            Spacing = 0,
            Tint = Color.Gold
        };
        AddComponent(scoreLabel);
    }

    private void BuildPage2(SpriteFont font)
    {
        var sectionLabel = new Label("p2_section", "Buttons & Inputs", font,
            new Anchor(new Vector2(96, 140)))
        { Color = Color.Gray };
        AddComponent(sectionLabel);

        // Button
        var buttonDesc = new Label("p2_btn_desc", "Button", font,
            new Anchor(new Vector2(96, 210)))
        { Color = Color.DarkGray };
        AddComponent(buttonDesc);

        var button = new Button("p2_button", "Click Me", font,
            new Anchor(new Vector2(96, 266)), new Vector2(400, 96), true)
        {
            Background = new Color(50, 60, 80),
            Border = new Border(4, Color.Gray),
            TextBorder = new Border(4, Color.Black),
            PressDepth = 4
        };
        button.OnHoveredEnter += () => button.Background = new Color(70, 85, 110);
        button.OnHoveredExit += () => button.Background = new Color(50, 60, 80);
        AddComponent(button);

        // SpriteButton
        var buttonDesc2 = new Label("p2_sprbtn_desc", "SpriteButton", font,
            new Anchor(new Vector2(96, 400)))
        { Color = Color.DarkGray };
        AddComponent(buttonDesc2);

        var arrowsSpriteAtlas = new ArrowsSpriteAtlas();
        var spriteBtn = new SpriteButton("p2_sprite_button",
            new Anchor(new Vector2(96, 456)));
        spriteBtn.SetNormalSprite(arrowsSpriteAtlas.GetTextureFromAtlas("arrow_right"));
        spriteBtn.SetHoveredSprite(arrowsSpriteAtlas.GetTextureFromAtlas("arrow_right_hover"));
        spriteBtn.SetPressedSprite(arrowsSpriteAtlas.GetTextureFromAtlas("arrow_right_active"));
        spriteBtn.Scale = new Vector2(6, 6);
        AddComponent(spriteBtn);

        // Checkboxes
        var cbDesc = new Label("p2_cb_desc", "Checkbox", font,
            new Anchor(new Vector2(600, 210)))
        { Color = Color.DarkGray };
        AddComponent(cbDesc);

        var cbChecked = new Checkbox("p2_cb_checked",
            new Anchor(new Vector2(600, 276)))
        {
            Label = "Checked",
            Font = font,
            LabelColor = Color.White,
            IsChecked = true,
            BoxBackground = new Color(60, 60, 60),
            BoxBorderColor = Color.Gray,
            CheckmarkColor = Color.LimeGreen,
            BoxSize = 64,
            BoxBorderThickness = 4,
            LabelSpacing = 32
        };
        AddComponent(cbChecked);

        var cbUnchecked = new Checkbox("p2_cb_unchecked",
            new Anchor(new Vector2(600, 396)))
        {
            Label = "Unchecked",
            Font = font,
            LabelColor = Color.White,
            IsChecked = false,
            BoxBackground = new Color(60, 60, 60),
            BoxBorderColor = Color.Gray,
            CheckmarkColor = Color.LimeGreen,
            BoxSize = 64,
            BoxBorderThickness = 4,
            LabelSpacing = 32
        };
        AddComponent(cbUnchecked);

        // Sprite Checkboxes (toggles)
        var sprCbDesc = new Label("p2_sprcb_desc", "Sprite Checkbox (toggle)", font,
            new Anchor(new Vector2(600, 520)))
        { Color = Color.DarkGray };
        AddComponent(sprCbDesc);

        var buttonsAtlas = new ButtonsSpriteAtlas();
        var toggleNames = new[] { "atk", "def", "dex", "innate", "level" };
        for (int i = 0; i < toggleNames.Length; i++)
        {
            var toggle = new Checkbox($"p2_toggle_{toggleNames[i]}",
                new Anchor(new Vector2(600 + i * 110, 576)));
            toggle.SetUncheckedSprite(buttonsAtlas.GetTextureFromAtlas($"toggle_{toggleNames[i]}_off"));
            toggle.SetCheckedSprite(buttonsAtlas.GetTextureFromAtlas($"toggle_{toggleNames[i]}_on"));
            toggle.Scale = new Vector2(6, 6);
            toggle.IsChecked = i % 2 == 0;
            AddComponent(toggle);
        }

        // InputField
        var inputDesc = new Label("p2_input_desc", "InputField", font,
            new Anchor(new Vector2(96, 580)))
        { Color = Color.DarkGray };
        AddComponent(inputDesc);

        var inputField = new InputField(
            name: "p2_input",
            placeholderText: "Type something...",
            font: font,
            position: new Anchor(new Vector2(96, 636)),
            size: new Vector2(400, 100))
        {
            Background = new Color(40, 40, 40),
            Border = new Border(4, Color.Gray),
            FocusedBorder = new Border(8, Color.CornflowerBlue),
            TextColor = Color.White,
            PlaceholderColor = Color.DarkGray,
            Padding = 24,
            CursorWidth = 4
        };
        AddComponent(inputField);
    }

    private void BuildPage3(SpriteFont font)
    {
        var sectionLabel = new Label("p3_section", "Sliders & Dropdowns", font,
            new Anchor(new Vector2(96, 140)))
        { Color = Color.Gray };
        AddComponent(sectionLabel);

        // Slider
        var sliderDesc = new Label("p3_slider_desc", "Slider", font,
            new Anchor(new Vector2(96, 210)))
        { Color = Color.DarkGray };
        AddComponent(sliderDesc);

        var slider = new Slider("p3_slider",
            new Anchor(new Vector2(96, 276)),
            new Vector2(600, 64))
        {
            MinValue = 0,
            MaxValue = 100,
            Value = 42,
            Font = font,
            ShowValue = true,
            TrackFillColor = Color.CornflowerBlue,
            TrackHeight = 24,
            ThumbWidth = 48,
            ThumbHeight = 64,
            TrackBorder = new Border(4, Color.Gray),
            ThumbBorder = new Border(4, Color.Black)
        };
        AddComponent(slider);

        // Second slider
        var slider2Desc = new Label("p3_slider2_desc", "Slider (alternate style)", font,
            new Anchor(new Vector2(96, 380)))
        { Color = Color.DarkGray };
        AddComponent(slider2Desc);

        var slider2 = new Slider("p3_slider2",
            new Anchor(new Vector2(96, 436)),
            new Vector2(600, 64))
        {
            MinValue = 0,
            MaxValue = 255,
            Value = 128,
            Font = font,
            ShowValue = true,
            TrackFillColor = Color.Orange,
            TrackHeight = 24,
            ThumbWidth = 48,
            ThumbHeight = 64,
            TrackBorder = new Border(4, Color.Gray),
            ThumbBorder = new Border(4, Color.Black)
        };
        AddComponent(slider2);

        // Dropdown
        var ddDesc = new Label("p3_dd_desc", "Dropdown", font,
            new Anchor(new Vector2(96, 560)))
        { Color = Color.DarkGray };
        AddComponent(ddDesc);

        var dropdown = new Dropdown("p3_dropdown",
            new Anchor(new Vector2(96, 616)),
            new Vector2(480, 96))
        {
            Font = font,
            Placeholder = "Select item...",
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
        dropdown.Items.Add("Option A");
        dropdown.Items.Add("Option B");
        dropdown.Items.Add("Option C");
        dropdown.Items.Add("Option D");
        dropdown.Items.Add("Option E");
        AddComponent(dropdown);
    }

    private void BuildPage4(SpriteFont font)
    {
        var sectionLabel = new Label("p4_section", "Sprites", font,
            new Anchor(new Vector2(96, 140)))
        { Color = Color.Gray };
        AddComponent(sectionLabel);

        var pokemonAtlas = new PokemonSpriteAtlas();

        // Static Sprite
        var spriteDesc = new Label("p4_sprite_desc", "Sprite (static)", font,
            new Anchor(new Vector2(96, 210)))
        { Color = Color.DarkGray };
        AddComponent(spriteDesc);

        var sprite = new Sprite("p4_sprite",
            new Anchor(new Vector2(144, 276)));
        sprite.SetFromAtlas(pokemonAtlas.GetTextureFromAtlas("0025_000_mf_n_00000000_n"));
        sprite.Scale = new Vector2(8, 8);
        AddComponent(sprite);

        // AnimatedSprite
        var animDesc = new Label("p4_anim_desc", "Sprite (Animated)", font,
            new Anchor(new Vector2(800, 210)))
        { Color = Color.DarkGray };
        AddComponent(animDesc);

        var animSprite = new AnimatedSprite("p4_anim",
            new Anchor(new Vector2(800, 276)))
        {
            FrameDelayMs = 500f,
            Loop = true,
            Scale = new Vector2(8, 8)
        };

        var animFrames = new[]
        {
            "0001_000_mf_n_00000000_n",
            "0004_000_mf_n_00000000_n",
            "0007_000_mf_n_00000000_n",
            "0025_000_mf_n_00000000_n",
        };

        foreach (var frameName in animFrames)
            animSprite.AddFrame(pokemonAtlas.GetTextureFromAtlas(frameName));

        animSprite.Play();
        AddComponent(animSprite);
    }

    private void BuildPage5(SpriteFont font)
    {
        var sectionLabel = new Label("p5_section", "Panels", font,
            new Anchor(new Vector2(96, 140)))
        { Color = Color.Gray };
        AddComponent(sectionLabel);

        // ─── Regular Panel ───
        var panelDesc = new Label("p5_panel_desc", "Panel", font,
            new Anchor(new Vector2(96, 210)))
        { Color = Color.DarkGray };
        AddComponent(panelDesc);

        var panel = new Panel("p5_panel")
        {
            Position = new Anchor(new Vector2(96, 276)),
            Size = new Vector2(500, 300),
            Background = new Color(40, 45, 55),
            Border = new Border(4, Color.Gray)
        };

        var panelLabel1 = new Label("p5_panel_label1", "Inside a Panel", font,
            new Anchor(new Vector2(120, 296)))
        { Color = Color.White };
        panel.Children.Add(panelLabel1);

        var panelLabel2 = new Label("p5_panel_label2", "Panels are containers", font,
            new Anchor(new Vector2(120, 356)))
        { Color = Color.CornflowerBlue };
        panel.Children.Add(panelLabel2);

        var panelBtn = new Button("p5_panel_btn", "Panel Button", font,
            new Anchor(new Vector2(120, 430)), new Vector2(340, 80), true)
        {
            Background = new Color(60, 70, 90),
            Border = new Border(4, Color.Gray),
            TextBorder = new Border(4, Color.Black),
            PressDepth = 4
        };
        panelBtn.OnHoveredEnter += () => panelBtn.Background = new Color(80, 95, 120);
        panelBtn.OnHoveredExit += () => panelBtn.Background = new Color(60, 70, 90);
        panel.Children.Add(panelBtn);

        AddComponent(panel);

        // ─── ScrollPanel ───
        var scrollDesc = new Label("p5_scroll_desc", "ScrollPanel", font,
            new Anchor(new Vector2(700, 210)))
        { Color = Color.DarkGray };
        AddComponent(scrollDesc);

        var scrollPanel = new ScrollPanel("p5_scroll")
        {
            Position = new Anchor(new Vector2(700, 276)),
            Size = new Vector2(600, 700),
            ContentSize = new Vector2(600, 1600),
            Background = new Color(35, 40, 50),
            Border = new Border(4, Color.Gray),
            ScrollbarWidth = 16,
            ScrollbarPadding = 4,
            ScrollSpeed = 60f,
            EnableHorizontalScroll = false
        };

        // Fill scroll panel with varied components
        float y = 290;

        var scrollLabel1 = new Label("p5_sl1", "Scrollable Content", font,
            new Anchor(new Vector2(724, y)))
        { Color = Color.White };
        scrollPanel.Children.Add(scrollLabel1);
        y += 80;

        var scrollBtn1 = new Button("p5_sb1", "Button A", font,
            new Anchor(new Vector2(724, y)), new Vector2(300, 80), true)
        {
            Background = new Color(60, 70, 90),
            Border = new Border(4, Color.Gray),
            TextBorder = new Border(4, Color.Black),
            PressDepth = 4
        };
        scrollBtn1.OnHoveredEnter += () => scrollBtn1.Background = new Color(80, 95, 120);
        scrollBtn1.OnHoveredExit += () => scrollBtn1.Background = new Color(60, 70, 90);
        scrollPanel.Children.Add(scrollBtn1);
        y += 120;

        var scrollCb1 = new Checkbox("p5_scb1",
            new Anchor(new Vector2(724, y)))
        {
            Label = "Option 1",
            Font = font,
            LabelColor = Color.White,
            IsChecked = true,
            BoxBackground = new Color(60, 60, 60),
            BoxBorderColor = Color.Gray,
            CheckmarkColor = Color.LimeGreen,
            BoxSize = 64,
            BoxBorderThickness = 4,
            LabelSpacing = 32
        };
        scrollPanel.Children.Add(scrollCb1);
        y += 100;

        var scrollCb2 = new Checkbox("p5_scb2",
            new Anchor(new Vector2(724, y)))
        {
            Label = "Option 2",
            Font = font,
            LabelColor = Color.White,
            IsChecked = false,
            BoxBackground = new Color(60, 60, 60),
            BoxBorderColor = Color.Gray,
            CheckmarkColor = Color.LimeGreen,
            BoxSize = 64,
            BoxBorderThickness = 4,
            LabelSpacing = 32
        };
        scrollPanel.Children.Add(scrollCb2);
        y += 120;

        var scrollSlider = new Slider("p5_ss1",
            new Anchor(new Vector2(724, y)),
            new Vector2(500, 64))
        {
            MinValue = 0,
            MaxValue = 100,
            Value = 65,
            Font = font,
            ShowValue = true,
            TrackFillColor = Color.CornflowerBlue,
            TrackHeight = 24,
            ThumbWidth = 48,
            ThumbHeight = 64,
            TrackBorder = new Border(4, Color.Gray),
            ThumbBorder = new Border(4, Color.Black)
        };
        scrollPanel.Children.Add(scrollSlider);
        y += 120;

        var scrollBtn2 = new Button("p5_sb2", "Button B", font,
            new Anchor(new Vector2(724, y)), new Vector2(300, 80), true)
        {
            Background = new Color(80, 50, 50),
            Border = new Border(4, Color.Gray),
            TextBorder = new Border(4, Color.Black),
            PressDepth = 4
        };
        scrollBtn2.OnHoveredEnter += () => scrollBtn2.Background = new Color(110, 70, 70);
        scrollBtn2.OnHoveredExit += () => scrollBtn2.Background = new Color(80, 50, 50);
        scrollPanel.Children.Add(scrollBtn2);
        y += 120;

        var scrollLabel2 = new Label("p5_sl2", "More content below...", font,
            new Anchor(new Vector2(724, y)))
        { Color = Color.Gray };
        scrollPanel.Children.Add(scrollLabel2);
        y += 80;

        var scrollSlider2 = new Slider("p5_ss2",
            new Anchor(new Vector2(724, y)),
            new Vector2(500, 64))
        {
            MinValue = 0,
            MaxValue = 255,
            Value = 180,
            Font = font,
            ShowValue = true,
            TrackFillColor = Color.Orange,
            TrackHeight = 24,
            ThumbWidth = 48,
            ThumbHeight = 64,
            TrackBorder = new Border(4, Color.Gray),
            ThumbBorder = new Border(4, Color.Black)
        };
        scrollPanel.Children.Add(scrollSlider2);
        y += 120;

        var scrollCb3 = new Checkbox("p5_scb3",
            new Anchor(new Vector2(724, y)))
        {
            Label = "Option 3",
            Font = font,
            LabelColor = Color.White,
            IsChecked = true,
            BoxBackground = new Color(60, 60, 60),
            BoxBorderColor = Color.Gray,
            CheckmarkColor = Color.Gold,
            BoxSize = 64,
            BoxBorderThickness = 4,
            LabelSpacing = 32
        };
        scrollPanel.Children.Add(scrollCb3);
        y += 100;

        var scrollBtn3 = new Button("p5_sb3", "Button C", font,
            new Anchor(new Vector2(724, y)), new Vector2(300, 80), true)
        {
            Background = new Color(50, 70, 50),
            Border = new Border(4, Color.Gray),
            TextBorder = new Border(4, Color.Black),
            PressDepth = 4
        };
        scrollBtn3.OnHoveredEnter += () => scrollBtn3.Background = new Color(70, 100, 70);
        scrollBtn3.OnHoveredExit += () => scrollBtn3.Background = new Color(50, 70, 50);
        scrollPanel.Children.Add(scrollBtn3);

        AddComponent(scrollPanel);
    }

    private void BuildPage6(SpriteFont font)
    {
        var sectionLabel = new Label("p6_section", "Audio", font,
            new Anchor(new Vector2(96, 140)))
        { Color = Color.Gray };
        AddComponent(sectionLabel);

        // ─── Music Track (left side) ───
        var trackDesc = new Label("p6_track_desc", "Music Track", font,
            new Anchor(new Vector2(96, 210)))
        { Color = Color.DarkGray };
        AddComponent(trackDesc);

        var trackDropdown = new Dropdown("p6_track_dropdown",
            new Anchor(new Vector2(96, 276)),
            new Vector2(480, 96))
        {
            Font = font,
            Placeholder = "Select track...",
            MaxVisibleItems = 5,
            Padding = 16,
            ArrowWidth = 64,
            ArrowSize = 24,
            TrianglePixelSize = 4,
            ScrollbarWidth = 12,
            Border = new Border(4, Color.Gray),
            FocusedBorder = new Border(4, Color.CornflowerBlue),
            ListBorder = new Border(4, Color.Gray)
        };

        var tracks = new[]
        {
            "ms_battle", "ms_battle_2", "ms_battle_3", "ms_battle_4",
            "ms_battle_intro", "ms_defeat", "ms_ending",
            "ms_league", "ms_main", "ms_tutorial", "ms_victory"
        };

        foreach (var track in tracks)
            trackDropdown.Items.Add(track);

        var toggleButton = new Button("p6_toggle", SoundManager.IsPlaying ? "Stop Music" : "Play Music", font,
            new Anchor(new Vector2(96, 400)), new Vector2(480, 96), true)
        {
            Background = SoundManager.IsPlaying ? new Color(80, 50, 50) : new Color(50, 80, 50),
            Border = new Border(4, Color.Gray),
            TextBorder = new Border(4, Color.Black),
            PressDepth = 4
        };
        toggleButton.OnHoveredEnter += () => toggleButton.Background = SoundManager.IsPlaying ? new Color(110, 70, 70) : new Color(70, 110, 70);
        toggleButton.OnHoveredExit += () => toggleButton.Background = SoundManager.IsPlaying ? new Color(80, 50, 50) : new Color(50, 80, 50);

        trackDropdown.OnSelectionChanged += index =>
        {
            if (trackDropdown.SelectedItem is not null)
            {
                SoundManager.PlayTrack(trackDropdown.SelectedItem);
                toggleButton.Text = "Stop Music";
                toggleButton.Background = new Color(80, 50, 50);
            }
        };
        AddComponent(trackDropdown);

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
        AddComponent(toggleButton);

        // ─── Sound Effects (right side) ───
        var effectsDesc = new Label("p6_effects_desc", "Sound Effects", font,
            new Anchor(new Vector2(700, 210)))
        { Color = Color.DarkGray };
        AddComponent(effectsDesc);

        var effects = new[]
        {
            "sn_card", "sn_click", "sn_click_2", "sn_coin", "sn_enterview",
            "sn_event", "sn_faint", "sn_flipcard", "sn_hurt", "sn_money",
            "sn_noise", "sn_rare", "sn_rare_2", "sn_text", "sn_upgrade"
        };

        var scrollPanel = new ScrollPanel("p6_effects_scroll")
        {
            Position = new Anchor(new Vector2(700, 276)),
            Size = new Vector2(600, 700),
            ContentSize = new Vector2(600, effects.Length * 80 + 20),
            Background = new Color(35, 40, 50),
            Border = new Border(4, Color.Gray),
            ScrollbarWidth = 16,
            ScrollbarPadding = 4,
            ScrollSpeed = 60f,
            EnableHorizontalScroll = false
        };

        float y = 290;
        foreach (var effect in effects)
        {
            var effectName = effect;
            var btn = new Button($"p6_fx_{effectName}", effectName, font,
                new Anchor(new Vector2(724, y)), new Vector2(520, 64), true)
            {
                Background = new Color(50, 60, 80),
                Border = new Border(4, Color.Gray),
                TextBorder = new Border(4, Color.Black),
                PressDepth = 4
            };
            btn.OnHoveredEnter += () => btn.Background = new Color(70, 85, 110);
            btn.OnHoveredExit += () => btn.Background = new Color(50, 60, 80);
            btn.OnClicked += () => SoundManager.PlayEffect(effectName);
            scrollPanel.Children.Add(btn);
            y += 80;
        }

        AddComponent(scrollPanel);
    }
}
