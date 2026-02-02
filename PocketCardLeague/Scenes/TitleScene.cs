using Microsoft.Xna.Framework;
using Components;
using Helpers;
using PocketCardLeague.Enums;
using PocketCardLeague.SpriteMaps;

namespace PocketCardLeague.Scenes;

public class TitleScene() : Scene<SceneType>(SceneType.Title)
{
    public override void Initialize()
    {
        BackgroundColor = Color.Black;

        var title = new Label("title", "Pocket Card League", ContentHelper.LoadFont("TitleFont"), new Anchor(new Vector2(0, 80), null), true, 800)
        {
            Border = new Border(2, Color.Black)
        };

        // Pokemon sprite examples
        var pokemonAtlas = new PokemonSpriteAtlas();

        // Display some starter Pokemon sprites (Bulbasaur line, Charmander line, Squirtle line)
        var pokemonSprites = new[]
        {
            ("0001_000_mf_n_00000000_n", 150f),  // Bulbasaur
            ("0002_000_mf_n_00000000_n", 230f),  // Ivysaur
            ("0003_000_mf_n_00000000_n", 310f),  // Venusaur
            ("0004_000_mf_n_00000000_n", 410f),  // Charmander
            ("0005_000_mf_n_00000000_n", 490f),  // Charmeleon
            ("0006_000_mf_n_00000000_n", 570f),  // Charizard
            ("0025_000_mf_n_00000000_n", 670f),  // Pikachu
        };

        for (int i = 0; i < pokemonSprites.Length; i++)
        {
            var (spriteName, xPos) = pokemonSprites[i];
            var sprite = new Sprite($"pokemon_{i}", new Anchor(new Vector2(xPos, 180)));
            sprite.SetFromAtlas(pokemonAtlas.GetTextureFromAtlas(spriteName));
            sprite.Scale = new Vector2(2f, 2f); // Scale up for visibility
            AddComponent(sprite);
        }

        var continueAction = new Label("continue", "Press SPACE to continue!", ContentHelper.LoadFont("DefaultFont"), new Anchor(new Vector2(0, 320), null), true, 800)
        {
            Border = new Border(2, Color.Black)
        };

        continueAction.OnHoveredEnter += () => continueAction.Color = Color.Yellow;
        continueAction.OnHoveredExit += () => continueAction.Color = Color.White;

        var button = new Button("button", "Options", ContentHelper.LoadFont("DefaultFont"), new Anchor(new Vector2(340, 400), null), new Vector2(120, 40), true)
        {
            Background = Color.Green,
            Border = new Border(2, Color.Black),
            TextBorder = new Border(2, Color.Black)
        };

        button.OnHoveredEnter += () => button.Background = Color.LightGreen;
        button.OnHoveredExit += () => button.Background = Color.Green;
        button.OnClicked += () => SceneManager.SetActiveScene(SceneType.Options);

        AddComponent(title);
        AddComponent(continueAction);
        AddComponent(button);

        base.Initialize();
    }
}
