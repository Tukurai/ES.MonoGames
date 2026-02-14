using Components;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PocketCardLeague.Config;
using PocketCardLeague.Consts;
using PocketCardLeague.Enums;
using PocketCardLeague.Scenes;
using PocketCardLeague.SpriteMaps;
using System;
using PocketCardLeague.Helpers;
using System.IO;
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
        TransitionManager.Initialize(GraphicsDevice);

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
        _ = PokeDex.Entries; // Hit static constructor to load pokedex entries

        // Register pixel cursors
        ControlState.RegisterCursors(
            PixelCursorsSpriteAtlas.Arrow,
            PixelCursorsSpriteAtlas.Pointer,
            PixelCursorsSpriteAtlas.Grabber,
            arrowOrigin: new Vector2(1, 1),
            pointerOrigin: new Vector2(3, 1),
            grabOrigin: new Vector2(3, 1));

        // Initialize card layout loader and register card component factories
        CardLayoutLoader.Initialize();
        CardComponentFactory.Register();

        // Load saved games
        GameStateManager.LoadAll();

        // Set default scene background
        Scene<SceneType>.DefaultBackground = LocationsSpriteAtlas.Border;

        // Set default tooltip font
        ToolTipManager.DefaultFont = Fonts.Default;

        SceneManager.AddScene(new TitleScene());
        SceneManager.AddScene(new OptionsScene());
        SceneManager.AddScene(new DebugScene());
        SceneManager.AddScene(new MainScene());
        SceneManager.AddScene(new DecksScene());
        SceneManager.AddScene(new CardDebugScene());
        SceneManager.AddScene(new DeckBuilderScene());
        SceneManager.AddScene(new CardsScene());
        SceneManager.AddScene(new BoosterDebugScene());

        SceneManager.SetActiveScene(SceneType.Title, new FadeTransition());

        // Initialize hot reload for XML scenes (only in debug mode)
#if DEBUG
        var scenesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content", "Scenes");
        HotReloadManager.Initialize(scenesPath);
#endif
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

        // Check for hot reload (safe point - before scene update)
#if DEBUG
        HotReloadManager.Update();
#endif

        var heldKeys = ControlState.GetHeldKeys();

        if (heldKeys.Contains(Keys.Escape))
            Exit();

        if (TransitionManager.IsTransitioning)
            TransitionManager.Update(gameTime);
        else
            SceneManager.ActiveScene?.Update(gameTime);

        base.Update(gameTime);

        IsMouseVisible = !ControlState.HasCustomCursor && ControlState.CursorTexture is null;
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
        if (TransitionManager.IsTransitioning)
        {
            TransitionManager.Draw(_spriteBatch, GraphicsDevice);
            ScaleManager.EndRender(GraphicsDevice, _spriteBatch);
        }
        else
        {
            // Begin rendering to the virtual resolution render target
            ScaleManager.BeginRender(GraphicsDevice);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            SceneManager.ActiveScene?.Draw(_spriteBatch);

            ControlState.Draw(_spriteBatch);

            _spriteBatch.End();

            // End render target and draw scaled to screen
            ScaleManager.EndRender(GraphicsDevice, _spriteBatch);
        }

        // Draw custom cursor at screen resolution (crisp, no render target scaling)
        if (ControlState.HasCustomCursor)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            ControlState.DrawScreenCursor(_spriteBatch);
            _spriteBatch.End();
        }

        base.Draw(gameTime);
    }
}
