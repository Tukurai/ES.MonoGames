using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Components;

public class Popup : BaseComponent
{
    private IScene? _scene;
    private float _fadeProgress;
    private bool _fadingIn;
    private bool _fadingOut;
    private bool _closing;
    private bool _confirmedOnClose;

    private const float FadeInDuration = 300f;
    private const float FadeOutDuration = 200f;

    public string Title { get; set; } = "Popup";
    public Vector2 ContentSize { get; set; } = new(600, 400);
    public Color BackdropColor { get; set; } = new(0, 0, 0, 160);
    public Color PanelBackground { get; set; } = new(35, 38, 48);
    public Border PanelBorder { get; set; } = new(2, new Color(70, 80, 100));
    public string OkText { get; set; } = "OK";
    public string CancelText { get; set; } = "Cancel";
    public SpriteFont? Font { get; set; }

    protected Panel ContentPanel { get; private set; } = null!;

    public event Action<PopupResult>? OnClosed;

    public Popup(string? name = null) : base(name)
    {
        Size = new Vector2(ScaleManager.VirtualWidth, ScaleManager.VirtualHeight);
    }

    public void Open(IScene scene)
    {
        _scene = scene;
        _fadingIn = true;
        _fadingOut = false;
        _closing = false;
        _fadeProgress = 0f;
        Opacity = 0f;

        BuildPopupLayout();
        scene.AddComponent(this);
    }

    public void Close(bool confirmed)
    {
        if (_closing)
            return;

        _closing = true;
        _confirmedOnClose = confirmed;
        _fadingIn = false;
        _fadingOut = true;
        _fadeProgress = 0f;
    }

    private void BuildPopupLayout()
    {
        Children.Clear();

        var screenW = ScaleManager.VirtualWidth;
        var screenH = ScaleManager.VirtualHeight;

        // Content panel (centered)
        var panelX = (screenW - ContentSize.X) / 2f;
        var panelY = (screenH - ContentSize.Y) / 2f;

        ContentPanel = new Panel("popup_content")
        {
            Position = new Anchor(new Vector2(panelX, panelY)),
            Size = ContentSize,
            Background = PanelBackground,
            Border = PanelBorder,
        };

        // Title label
        var titleLabel = new BitmapLabel("popup_title")
        {
            Text = Title,
            FontFamily = "m3x6.ttf",
            FontSize = 24,
            TextColor = Color.White,
            Position = new Anchor(new Vector2(16, 12), ContentPanel.Position),
        };
        ContentPanel.Children.Add(titleLabel);

        // Build subclass content
        BuildContent(ContentPanel);

        // Button row at bottom
        var btnY = ContentSize.Y - 50;
        var btnW = 120;
        var btnH = 36;
        var btnSpacing = 12;

        var okBtn = new Button("popup_ok", OkText, Font, null, new Vector2(btnW, btnH), true)
        {
            Position = new Anchor(new Vector2(ContentSize.X - btnW * 2 - btnSpacing - 16, btnY), ContentPanel.Position),
            Background = new Color(50, 120, 70),
            TextColor = Color.White,
            Border = new Border(2, new Color(80, 170, 100)),
            Padding = 8,
        };
        okBtn.OnClicked += () => Close(true);
        ContentPanel.Children.Add(okBtn);

        var cancelBtn = new Button("popup_cancel", CancelText, Font, null, new Vector2(btnW, btnH), true)
        {
            Position = new Anchor(new Vector2(ContentSize.X - btnW - 16, btnY), ContentPanel.Position),
            Background = new Color(120, 50, 50),
            TextColor = Color.White,
            Border = new Border(2, new Color(170, 80, 80)),
            Padding = 8,
        };
        cancelBtn.OnClicked += () => Close(false);
        ContentPanel.Children.Add(cancelBtn);

        Children.Add(ContentPanel);
    }

    protected virtual void BuildContent(Panel contentPanel)
    {
        // Override in subclasses to add custom content
    }

    protected virtual PopupResult BuildResult(bool confirmed)
    {
        return new PopupResult { Confirmed = confirmed };
    }

    public override void Update(GameTime gameTime)
    {
        var dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        // Fade animation
        if (_fadingIn)
        {
            _fadeProgress += dt;
            Opacity = MathHelper.Clamp(_fadeProgress / FadeInDuration, 0f, 1f);
            if (_fadeProgress >= FadeInDuration)
            {
                _fadingIn = false;
                Opacity = 1f;
            }
        }
        else if (_fadingOut)
        {
            _fadeProgress += dt;
            Opacity = MathHelper.Clamp(1f - _fadeProgress / FadeOutDuration, 0f, 1f);
            if (_fadeProgress >= FadeOutDuration)
            {
                _fadingOut = false;
                Opacity = 0f;

                var result = BuildResult(_confirmedOnClose);
                _scene?.RemoveComponent(this);
                OnClosed?.Invoke(result);
                return;
            }
        }

        // Update children first so they can process input before we block everything.
        // Don't call base.Update — it would check the blocker and skip all input.
        foreach (var child in Children)
            child.Update(gameTime);

        // Block all input behind the popup (prevents scene components from receiving clicks)
        var fullScreen = new Rectangle(0, 0, ScaleManager.VirtualWidth, ScaleManager.VirtualHeight);
        OverlayManager.RegisterInputBlocker(fullScreen);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Register overlay draw so popup renders on top of everything
        OverlayManager.RegisterOverlayDraw(DrawPopup);
    }

    private void DrawPopup(SpriteBatch spriteBatch)
    {
        if (Opacity <= 0f)
            return;

        // Dark backdrop
        spriteBatch.Draw(
            RendererHelper.WhitePixel,
            new Rectangle(0, 0, ScaleManager.VirtualWidth, ScaleManager.VirtualHeight),
            BackdropColor * Opacity
        );

        // Content panel and children
        ContentPanel.Draw(spriteBatch);
    }
}
