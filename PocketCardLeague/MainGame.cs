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

    // Virtual resolution - designed at 4x (2048x1152), downscaled to window size
    private const int VirtualWidth = 2048;
    private const int VirtualHeight = 1152;
    private const float DefaultScale = 1f;

    /// <summary>
    /// Initialize method called once per game.
    /// Initializes content helper, renderer helper, scale manager, settings, and scene manager.
    /// </summary>
    protected override void Initialize()
    {
        ContentHelper.Initialize(new PocketCardLeagueConfig(), Content);
        RendererHelper.Initialize(GraphicsDevice);
        SettingsManager.Initialize(); // Load settings first
        SoundManager.Initialize();
        ScaleManager.Initialize(_graphics, VirtualWidth, VirtualHeight, SettingsManager.GetCurrentScale());
        SceneManager.Initialize<SceneType>();
        base.Initialize();

        // Apply saved settings now that the graphics device is fully initialized
        ScaleManager.SetScale(SettingsManager.GetCurrentScale());
        if (SettingsManager.Current.Fullscreen)
            ScaleManager.SetFullscreen(true);
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
        SceneManager.AddScene(new DebugScene());

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

        var pressedKeys = ControlState.GetPressedKeys();
        var heldKeys = ControlState.GetHeldKeys();
        bool altHeld = heldKeys.Contains(Keys.LeftAlt) || heldKeys.Contains(Keys.RightAlt);

        if (altHeld && pressedKeys.Contains(Keys.Home))
        {
            SceneManager.SetActiveScene(SceneType.Debug);
            return;
        }

        if (heldKeys.Contains(Keys.Escape))
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
