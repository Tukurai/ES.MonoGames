using Microsoft.Xna.Framework;
using Components;
using Helpers;
using PocketCardLeague.Enums;
using PocketCardLeague.SpriteMaps;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using PocketCardLeague.Consts;

namespace PocketCardLeague.Scenes;

public class DeckScene() : Scene<SceneType>(SceneType.Deck)
{
    public override string? SceneTrack { get; set; } = "ms_main";

    public override void Initialize()
    {
        BackgroundColor = new Color(25, 25, 25, 255);
        BackgroundMode = SceneBackgroundMode.Sprite;
        BackgroundSprite = LocationsSpriteAtlas.Cave_10;
        BackgroundOpacity = 0.25f;

        // Virtual resolution is 2048x1152, downscaled to window

        var btnLeft = new SpriteButton("button_to_main",
            new Anchor(new Vector2(140, 508)));

        btnLeft.SetNormalSprite(ArrowsSpriteAtlas.Arrow_left);
        btnLeft.SetHoveredSprite(ArrowsSpriteAtlas.Arrow_left);
        btnLeft.SetPressedSprite(ArrowsSpriteAtlas.Arrow_left_active);
        btnLeft.Bob = BobDirection.Left;
        btnLeft.Scale = new Vector2(4, 4);
        btnLeft.Opacity = 0.8f;
        btnLeft.OnHoveredEnter += () => btnLeft.Opacity = 1f;
        btnLeft.OnHoveredExit += () => btnLeft.Opacity = 0.8f;
        btnLeft.OnClicked += () => SceneManager.SetActiveScene(SceneType.Main, new SlideTransition(SlideDirection.Right));
        btnLeft.Children.Add(new Label("main_label", "Main", Fonts.Header,
            new Anchor(new Vector2(14, 60), btnLeft.Position), true)
        { Border = new Border(4, new Color(25, 25, 25, 170)) });
        AddComponent(btnLeft);

        base.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        var pressedKeys = ControlState.GetPressedKeys();
        if (pressedKeys.Contains(Keys.Left))
        {
            SceneManager.SetActiveScene(SceneType.Main, new SlideTransition(SlideDirection.Right));
            return;
        }

        base.Update(gameTime);
    }
}
