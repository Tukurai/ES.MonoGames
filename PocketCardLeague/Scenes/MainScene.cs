using Microsoft.Xna.Framework;
using Components;
using Helpers;
using PocketCardLeague.Enums;
using PocketCardLeague.SpriteMaps;

namespace PocketCardLeague.Scenes;

public class MainScene() : Scene<SceneType>(SceneType.Main)
{
    public override string? SceneTrack { get; set; } = "ms_main";

    public override void Initialize()
    {
        var locations = new LocationsSpriteAtlas();

        BackgroundMode = SceneBackgroundMode.Sprite;
        BackgroundSprite = locations.GetTextureFromAtlas(LocationsSpriteAtlas.Cave_11);

        // Virtual resolution is 2048x1152, downscaled to window


        var font = ContentHelper.LoadFont("DefaultFont");
        var arrowsAtlas = new ArrowsSpriteAtlas();

        var btnLeft = new SpriteButton("button_to_options",
            new Anchor(new Vector2(140, 508)));
        btnLeft.SetNormalSprite(arrowsAtlas.GetTextureFromAtlas("arrow_left"));
        btnLeft.SetHoveredSprite(arrowsAtlas.GetTextureFromAtlas("arrow_left_hover"));
        btnLeft.SetPressedSprite(arrowsAtlas.GetTextureFromAtlas("arrow_left_active"));
        btnLeft.Bob = BobDirection.Left;
        btnLeft.Scale = new Vector2(4, 4);
        AddComponent(btnLeft);

        btnLeft.OnClicked += () => SceneManager.SetActiveScene(SceneType.Options, new SlideTransition(SlideDirection.Right));

        var leftLabel = new Label("options_label", "Options", font,
            new Anchor(new Vector2(14, 60), btnLeft.Position), true)
        { Border = new Border(4, Color.Black) };
        AddComponent(leftLabel);

        var btnRight = new SpriteButton("button_to_deckbuilder",
            new Anchor(new Vector2(1852, 508)));
        btnRight.SetNormalSprite(arrowsAtlas.GetTextureFromAtlas("arrow_right"));
        btnRight.SetHoveredSprite(arrowsAtlas.GetTextureFromAtlas("arrow_right_hover"));
        btnRight.SetPressedSprite(arrowsAtlas.GetTextureFromAtlas("arrow_right_active"));
        btnRight.Scale = new Vector2(4, 4);
        btnRight.Bob = BobDirection.Right;
        AddComponent(btnRight);

        btnRight.OnClicked += () => SceneManager.SetActiveScene(SceneType.Deck, new SlideTransition(SlideDirection.Left));

        var rightLabel = new Label("deckbuilder_label", "Deck", font,
            new Anchor(new Vector2(14, 60), btnRight.Position), true)
        { Border = new Border(4, Color.Black) };
        AddComponent(rightLabel);

        base.Initialize();
    }
}
