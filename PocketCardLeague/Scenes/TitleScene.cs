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

        // Virtual resolution is 1536x864 (3x of 512x288)
        var font = ContentHelper.LoadFont("DefaultFont");

        var title = new Label("title", "Pocket Card League", ContentHelper.LoadFont("TitleFont"), new Anchor(new Vector2(0, 60), null), true, 1536)
        {
            Border = new Border(3, Color.Black)
        };

        // Pokemon sprite examples
        var pokemonAtlas = new PokemonSpriteAtlas();

        // Animated sprite example - cycling through Pokemon
        var animatedPokemon = new AnimatedSprite("animated_pokemon", new Anchor(new Vector2(672, 240)))
        {
            FrameDelayMs = 500f,
            Loop = true,
            Scale = new Vector2(6f, 6f)
        };

        var animationFrames = new[]
        {
            "0001_000_mf_n_00000000_n",  // Bulbasaur
            "0004_000_mf_n_00000000_n",  // Charmander
            "0007_000_mf_n_00000000_n",  // Squirtle
            "0025_000_mf_n_00000000_n",  // Pikachu
        };

        foreach (var frameName in animationFrames)
        {
            animatedPokemon.AddFrame(pokemonAtlas.GetTextureFromAtlas(frameName));
        }

        animatedPokemon.Play();

        var continueAction = new Label("continue", "Press SPACE", font, new Anchor(new Vector2(0, 660), null), true, 1536)
        {
            Border = new Border(3, Color.Black)
        };

        continueAction.OnHoveredEnter += () => continueAction.Color = Color.Yellow;
        continueAction.OnHoveredExit += () => continueAction.Color = Color.White;

        var button = new Button("button", "Options", font, new Anchor(new Vector2(588, 750), null), new Vector2(360, 90), true)
        {
            Background = Color.Green,
            Border = new Border(3, Color.Black),
            TextBorder = new Border(3, Color.Black)
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
