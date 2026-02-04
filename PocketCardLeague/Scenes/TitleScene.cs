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

        // Virtual resolution is 2048x1152, downscaled to window
        var font = ContentHelper.LoadFont("DefaultFont");

        var title = new Label("title", "Pocket Card League", ContentHelper.LoadFont("TitleFont"), new Anchor(new Vector2(0, 80), null), true, 2048)
        {
            Border = new Border(4, Color.Black)
        };

        // Pokemon sprite examples
        var pokemonAtlas = new PokemonSpriteAtlas();

        // Animated sprite example - cycling through Pokemon
        var animatedPokemon = new AnimatedSprite("animated_pokemon", new Anchor(new Vector2(448, 8)))
        {
            FrameDelayMs = 250f,
            Loop = true,
            Scale = new Vector2(16f, 16f)
        };

        var animationFrames = new[]
        {
            "0001_000_mf_n_00000000_n",  // Bulbasaur
            "0002_000_mf_n_00000000_n",  // Bulbasaur
            "0003_000_mf_n_00000000_n",  // Bulbasaur

            "0004_000_mf_n_00000000_n",  // Charmander
            "0005_000_mf_n_00000000_n",  // Charmander
            "0006_000_mf_n_00000000_n",  // Charmander
            
            "0007_000_mf_n_00000000_n",  // Squirtle
            "0008_000_mf_n_00000000_n",  // Squirtle
            "0009_000_mf_n_00000000_n",  // Squirtle
            
            "0010_000_mf_n_00000000_n",  // Squirtle
            "0011_000_mf_n_00000000_n",  // Squirtle
            "0012_000_mf_n_00000000_n",  // Squirtle
            
            "0013_000_mf_n_00000000_n",  // Squirtle
            "0014_000_mf_n_00000000_n",  // Squirtle
            "0015_000_mf_n_00000000_n",  // Squirtle
            
            "0016_000_mf_n_00000000_n",  // Squirtle
            "0017_000_mf_n_00000000_n",  // Squirtle
            "0018_000_mf_n_00000000_n",  // Squirtle
            
            "0019_000_mf_n_00000000_n",  // Squirtle
            "0020_000_mf_n_00000000_n",  // Squirtle
            "0021_000_mf_n_00000000_n",  // Squirtle
            
            "0022_000_mf_n_00000000_n",  // Squirtle
            "0023_000_mf_n_00000000_n",  // Squirtle
            "0024_000_mf_n_00000000_n",  // Squirtle

            "0025_000_mf_n_00000000_n",  // Pikachu
            "0026_000_mf_n_00000000_n",  // Pikachu
            
            "0150_000_mf_n_00000000_n",  // Pikachu
            "0151_000_mf_n_00000000_n",  // Pikachu
        };

        foreach (var frameName in animationFrames)
        {
            animatedPokemon.AddFrame(pokemonAtlas.GetTextureFromAtlas(frameName));
        }

        animatedPokemon.Play();

        var continueAction = new Label("continue", "Press SPACE", font, new Anchor(new Vector2(0, 880), null), true, 2048)
        {
            Border = new Border(4, Color.Black)
        };

        continueAction.OnHoveredEnter += () => continueAction.Color = Color.Yellow;
        continueAction.OnHoveredExit += () => continueAction.Color = Color.White;

        var button = new Button("button", "Options", font, new Anchor(new Vector2(784, 1000), null), new Vector2(480, 120), true)
        {
            Background = Color.Green,
            Border = new Border(4, Color.Black),
            TextBorder = new Border(4, Color.Black)
        };

        button.OnHoveredEnter += () => button.Background = Color.LightGreen;
        button.OnHoveredExit += () => button.Background = Color.Green;
        button.OnClicked += () => SceneManager.SetActiveScene(SceneType.Options);

        AddComponent(title);
        AddComponent(animatedPokemon);
        AddComponent(continueAction);
        AddComponent(button);

        base.Initialize();
    }
}
