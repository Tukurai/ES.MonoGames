using Microsoft.Xna.Framework;
using Components;
using Helpers;
using PocketCardLeague.Enums;
using PocketCardLeague.SpriteMaps;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using PocketCardLeague.Consts;

namespace PocketCardLeague.Scenes;

public class MainScene() : Scene<SceneType>(SceneType.Main)
{
    public override string? SceneTrack { get; set; } = "ms_main";

    public override void Initialize()
    {
        BackgroundColor = new Color(25, 25, 25, 255);
        BackgroundMode = SceneBackgroundMode.Sprite;
        BackgroundSprite = LocationsSpriteAtlas.Cave_11;
        BackgroundOpacity = 0.25f;

        // Virtual resolution is 2048x1152, downscaled to window

        var btnLeft = new SpriteButton("button_to_options",
            new Anchor(new Vector2(140, 508)));

        btnLeft.SetNormalSprite(ArrowsSpriteAtlas.Arrow_left);
        btnLeft.SetHoveredSprite(ArrowsSpriteAtlas.Arrow_left);
        btnLeft.SetPressedSprite(ArrowsSpriteAtlas.Arrow_left_active);
        btnLeft.Bob = BobDirection.Left;
        btnLeft.Scale = new Vector2(4, 4);
        btnLeft.Opacity = 0.8f;
        btnLeft.OnHoveredEnter += () => btnLeft.Opacity = 1f;
        btnLeft.OnHoveredExit += () => btnLeft.Opacity = 0.8f;
        btnLeft.OnClicked += () => SceneManager.SetActiveScene(SceneType.Options, new SlideTransition(SlideDirection.Right));
        btnLeft.Children.Add(new Label("options_label", "Options", Fonts.Header,
            new Anchor(new Vector2(14, 60), btnLeft.Position), true)
        { Border = new Border(4, new Color(25, 25, 25, 170)) });
        AddComponent(btnLeft);

        var btnRight = new SpriteButton("button_to_deckbuilder",
            new Anchor(new Vector2(1852, 508)));
        btnRight.SetNormalSprite(ArrowsSpriteAtlas.Arrow_right);
        btnRight.SetHoveredSprite(ArrowsSpriteAtlas.Arrow_right);
        btnRight.SetPressedSprite(ArrowsSpriteAtlas.Arrow_right_active);
        btnRight.Scale = new Vector2(4, 4);
        btnRight.Bob = BobDirection.Right;
        btnRight.Opacity = 0.8f;
        btnRight.OnHoveredEnter += () => btnRight.Opacity = 1f;
        btnRight.OnHoveredExit += () => btnRight.Opacity = 0.8f;
        btnRight.OnClicked += () => SceneManager.SetActiveScene(SceneType.Deck, new SlideTransition(SlideDirection.Left));
        btnRight.Children.Add(new Label("deckbuilder_label", "Deck", Fonts.Header,
            new Anchor(new Vector2(14, 60), btnRight.Position), true)
        { Border = new Border(4, new Color(25, 25, 25, 170)) });
        AddComponent(btnRight);

        base.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        var pressedKeys = ControlState.GetPressedKeys();
        if (pressedKeys.Contains(Keys.Left))
        {
            SceneManager.SetActiveScene(SceneType.Options, new SlideTransition(SlideDirection.Right));
            return;
        }

        if (pressedKeys.Contains(Keys.Right))
        {
            SceneManager.SetActiveScene(SceneType.Deck, new SlideTransition(SlideDirection.Left));
            return;
        }

        if (pressedKeys.Contains(Keys.Down))
        {
            SceneManager.SetActiveScene(SceneType.Title, new FadeTransition(0.8f));
            return;
        }

        base.Update(gameTime);
    }
}
