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

    // Virtual resolution - the internal resolution the game is designed for
    private const int VirtualWidth = 512;
    private const int VirtualHeight = 288;
    private const float DefaultScale = 3f;

    /// <summary>
    /// Initialize method called once per game.
    /// Initializes content helper, renderer helper, scale manager, settings, and scene manager.
    /// </summary>
    protected override void Initialize()
    {
        ContentHelper.Initialize(new PocketCardLeagueConfig(), Content);
        RendererHelper.Initialize(GraphicsDevice);
        SettingsManager.Initialize(); // Load settings first
        // Use saved scale, or default if no saved setting
        var initialScale = SettingsManager.Current.WindowScale > 0 ? SettingsManager.Current.WindowScale : DefaultScale;
        ScaleManager.Initialize(_graphics, VirtualWidth, VirtualHeight, initialScale);
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
    /// <remarks>
    /// Renders to a virtual resolution render target, then scales it to the window.
    /// This ensures consistent positioning regardless of window/fullscreen size.
    /// </remarks>
    protected override void Draw(GameTime gameTime)
    {
        // Begin rendering to the virtual resolution render target
        ScaleManager.BeginRender(GraphicsDevice);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        SceneManager.ActiveScene?.Draw(_spriteBatch);

        ControlState.Draw(_spriteBatch);

        _spriteBatch.End();

        // End render target and draw scaled to screen
        ScaleManager.EndRender(GraphicsDevice, _spriteBatch);

        base.Draw(gameTime);
    }
}
