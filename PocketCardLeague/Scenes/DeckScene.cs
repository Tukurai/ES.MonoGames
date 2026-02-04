using Microsoft.Xna.Framework;
using Components;
using Helpers;
using PocketCardLeague.Enums;
using PocketCardLeague.SpriteMaps;

namespace PocketCardLeague.Scenes;

public class DeckScene() : Scene<SceneType>(SceneType.Deck)
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

        var btnLeft = new SpriteButton("button_to_main",
            new Anchor(new Vector2(140, 508)));
        btnLeft.SetNormalSprite(arrowsAtlas.GetTextureFromAtlas("arrow_left"));
        btnLeft.SetHoveredSprite(arrowsAtlas.GetTextureFromAtlas("arrow_left_hover"));
        btnLeft.SetPressedSprite(arrowsAtlas.GetTextureFromAtlas("arrow_left_active"));
        btnLeft.Bob = BobDirection.Left;
        btnLeft.Scale = new Vector2(4, 4);
        AddComponent(btnLeft);

        btnLeft.OnClicked += () => SceneManager.SetActiveScene(SceneType.Main, new SlideTransition(SlideDirection.Right));

        var leftLabel = new Label("main_label", "Main", font,
            new Anchor(new Vector2(14, 60), btnLeft.Position), true)
        { Border = new Border(4, Color.Black) };
        AddComponent(leftLabel);

        base.Initialize();
    }
}
