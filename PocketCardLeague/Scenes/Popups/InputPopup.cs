using Components;
using Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace PocketCardLeague.Scenes.Popups;

public class InputPopup : Popup
{
    private readonly string _prompt;
    private readonly string? _defaultValue;
    private InputField? _input;

    public InputPopup(string prompt, string? defaultValue = null) : base("input_popup")
    {
        _prompt = prompt;
        _defaultValue = defaultValue;
        Title = "Input";
        ContentSize = new Vector2(400, 200);
    }

    protected override void BuildContent(Panel contentPanel)
    {
        var promptLabel = new BitmapLabel("prompt_label")
        {
            Text = _prompt,
            FontFamily = "m3x6.ttf",
            FontSize = 20,
            TextColor = Color.White,
            Position = new Anchor(new Vector2(16, 48), contentPanel.Position),
        };
        contentPanel.Children.Add(promptLabel);

        _input = new InputField("popup_input", _defaultValue ?? "", "Type here...", Font)
        {
            Position = new Anchor(new Vector2(16, 80), contentPanel.Position),
            Size = new Vector2(ContentSize.X - 32, 36),
            TextColor = Color.White,
            Background = new Color(40, 45, 55),
            Border = new Border(2, new Color(70, 75, 90)),
            FocusedBorder = new Border(2, new Color(120, 140, 180)),
            Padding = 8,
        };
        contentPanel.Children.Add(_input);
    }

    protected override PopupResult BuildResult(bool confirmed)
    {
        return new PopupResult
        {
            Confirmed = confirmed,
            TextValue = _input?.Text,
        };
    }
}
