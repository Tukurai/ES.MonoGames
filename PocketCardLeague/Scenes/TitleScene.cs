using Microsoft.Xna.Framework;
using Components;
using Helpers;
using PocketCardLeague.Enums;
using PocketCardLeague.SpriteMaps;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using PocketCardLeague.Consts;

namespace PocketCardLeague.Scenes;

public class TitleScene() : Scene<SceneType>(SceneType.Title)
{
    public override string? SceneTrack { get; set; } = "ms_main";

    public override void Initialize()
    {
        BackgroundColor = Color.Black;

        // Virtual resolution is 2048x1152, downscaled to window

        var title = new Label("title", "Pocket Card League", Fonts.Title, new Anchor(new Vector2(0, 80), null), true, 2048)
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
            PokemonSpriteAtlas._0001_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0001_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0002_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0003_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0004_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0005_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0006_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0007_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0008_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0009_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0010_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0011_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0012_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0013_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0014_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0015_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0016_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0017_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0018_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0019_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0020_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0021_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0022_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0023_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0024_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0025_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0026_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0150_000_mf_n_00000000_n,
            PokemonSpriteAtlas._0151_000_mf_n_00000000_n,
        };

        foreach (var frame in animationFrames)
        {
            animatedPokemon.AddFrame(frame);
        }

        animatedPokemon.Play();

        var continueAction = new Label("continue", "Press SPACE", Fonts.Default, new Anchor(new Vector2(0, 880), null), true, 2048)
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
