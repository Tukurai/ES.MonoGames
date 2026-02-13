using Components;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PocketCardLeague.Scenes.Popups;

public class DropdownPopup : Popup
{
    private readonly string _prompt;
    private readonly List<string> _items;
    private readonly int _defaultIndex;
    private Dropdown? _dropdown;

    public DropdownPopup(string prompt, List<string> items, int defaultIndex = 0) : base("dropdown_popup")
    {
        _prompt = prompt;
        _items = items;
        _defaultIndex = defaultIndex;
        Title = "Select";
        ContentSize = new Vector2(400, 220);
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

        _dropdown = new Dropdown("popup_dropdown")
        {
            Position = new Anchor(new Vector2(16, 80), contentPanel.Position),
            Size = new Vector2(ContentSize.X - 32, 36),
            Font = Font,
            Items = _items,
            SelectedIndex = _defaultIndex,
            Background = new Color(40, 45, 55),
            BackgroundHovered = new Color(50, 55, 68),
            TextColor = Color.White,
            Border = new Border(2, new Color(70, 75, 90)),
            FocusedBorder = new Border(2, new Color(120, 140, 180)),
            ListBackground = new Color(35, 38, 48),
            ItemHoveredBackground = new Color(60, 68, 85),
            ListBorder = new Border(2, new Color(70, 75, 90)),
            Padding = 6,
            MaxVisibleItems = 8,
        };
        contentPanel.Children.Add(_dropdown);
    }

    protected override PopupResult BuildResult(bool confirmed)
    {
        return new PopupResult
        {
            Confirmed = confirmed,
            SelectedIndex = _dropdown?.SelectedIndex ?? -1,
            SelectedItem = _dropdown is not null && _dropdown.SelectedIndex >= 0 && _dropdown.SelectedIndex < _items.Count
                ? _items[_dropdown.SelectedIndex]
                : null,
        };
    }
}
