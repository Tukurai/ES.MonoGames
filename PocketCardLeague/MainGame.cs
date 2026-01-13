using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PocketCardLeague.Config;
using PocketCardLeague.Enums;
using PocketCardLeague.Scenes;
using System.Linq;

namespace PocketCardLeague;

public class MainGame : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch = null!;

    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    /// <summary>
    /// Initialize method called once per game.
    /// Initializes content helper, renderer helper, and scene manager.
    /// </summary>
    protected override void Initialize()
    {
        ContentHelper.Initialize(new PocketCardLeagueConfig(), Content);
        RendererHelper.Initialize(GraphicsDevice);
        SceneManager.Initialize<SceneType>();
        base.Initialize();
    }

    /// <summary>
    /// LoadContent method called once per game.
    /// Loads all game content and initializes scenes.
    /// </summary>
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        SceneManager.AddScene(new TitleScene());
        SceneManager.AddScene(new OptionsScene());

        SceneManager.SetActiveScene(SceneType.Title);
    }

    /// <summary>
    /// Update method called every frame.
    /// </summary>
    /// <remarks>
    /// First updates the ControlState, checks for the Escape key to exit the game,
    /// and updates the active scene. Lastly calls the base Update method and sets 
    /// the mouse visibility based on the cursor texture.
    /// </remarks>
    /// <param name="gameTime"></param>
    protected override void Update(GameTime gameTime)
    {
        ControlState.Update(gameTime);

        if (ControlState.GetHeldKeys().Contains(Keys.Escape))
            Exit();

        SceneManager.ActiveScene?.Update(gameTime);

        base.Update(gameTime);

        IsMouseVisible = ControlState.CursorTexture is null; 
    }

    /// <summary>
    /// Draws the current game scene and user interface elements to the screen.
    /// </summary>
    /// <remarks>This method is typically called once per frame by the game loop. It renders the active scene
    /// and overlays any control or UI elements. Override this method to customize rendering behavior, but ensure that
    /// base. Draw(gameTime) is called to maintain proper rendering order.</remarks>
    /// <param name="gameTime">Provides a snapshot of timing values for the current frame, including elapsed game time and total game time
    /// since the start of the game.</param>
    protected override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin(); 

        SceneManager.ActiveScene?.Draw(_spriteBatch);

        ControlState.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
