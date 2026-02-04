using Microsoft.Xna.Framework;
using Components;
using Helpers;
using PocketCardLeague.Enums;
using PocketCardLeague.SpriteMaps;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace PocketCardLeague.Scenes;

public class TitleScene() : Scene<SceneType>(SceneType.Title)
{
    public override string? SceneTrack { get; set; } = "ms_main";

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

        AddComponent(title);
        AddComponent(animatedPokemon);
        AddComponent(continueAction);

        base.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        var pressedKeys = ControlState.GetPressedKeys();
        var heldKeys = ControlState.GetHeldKeys();
        bool altHeld = heldKeys.Contains(Keys.LeftAlt) || heldKeys.Contains(Keys.RightAlt);

        if (altHeld && pressedKeys.Contains(Keys.Home))
        {
            SceneManager.SetActiveScene(SceneType.Debug, new FadeTransition());
            return;
        }

        if (pressedKeys.Contains(Keys.Space))
        {
            SceneManager.SetActiveScene(SceneType.Main, new FadeTransition());
            return;
        }

        base.Update(gameTime);
    }
}
