using Microsoft.Xna.Framework;
using Components;
using Helpers;
using PocketCardLeague.Consts;
using PocketCardLeague.Enums;
using PocketCardLeague.Scenes.Popups;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace PocketCardLeague.Scenes;

public class CardDebugScene() : XmlScene<SceneType>(SceneType.CardDebug)
{
    protected override string XmlFileName => "CardDebugScene.xml";

    private Label _resultLabel = null!;

    protected override void OnXmlLoaded()
    {
        _resultLabel = Bind<Label>("popup_result_label");

        var btnInput = Bind<Button>("btn_test_input");
        btnInput.OnClicked += () =>
        {
            var popup = new InputPopup("Enter your name:", "Trainer") { Font = Fonts.M3Default };
            popup.Open(this);
            popup.OnClosed += result =>
            {
                if (result.Confirmed)
                    _resultLabel.Text = $"Input result: \"{result.TextValue}\"";
                else
                    _resultLabel.Text = "Input popup cancelled.";
            };
        };

        var btnDropdown = Bind<Button>("btn_test_dropdown");
        btnDropdown.OnClicked += () =>
        {
            var items = new List<string> { "Fire", "Water", "Grass", "Electric", "Psychic" };
            var popup = new DropdownPopup("Choose a type:", items) { Font = Fonts.M3Default };
            popup.Open(this);
            popup.OnClosed += result =>
            {
                if (result.Confirmed)
                    _resultLabel.Text = $"Selected: \"{result.SelectedItem}\" (index {result.SelectedIndex})";
                else
                    _resultLabel.Text = "Dropdown popup cancelled.";
            };
        };
    }

    public override void Update(GameTime gameTime)
    {
        var pressedKeys = ControlState.GetPressedKeys();
        var heldKeys = ControlState.GetHeldKeys();
        bool altHeld = heldKeys.Contains(Keys.LeftAlt) || heldKeys.Contains(Keys.RightAlt);

        if (!altHeld && pressedKeys.Contains(Keys.End))
        {
            SceneManager.SetActiveScene(SceneType.Title, new FadeTransition());
            return;
        }

        base.Update(gameTime);
    }
}
