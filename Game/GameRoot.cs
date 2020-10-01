using System;
using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShitGame;
using ShitGame.GUI;
using ShitGame.Scenes;

namespace ShitGame {
    public sealed class GameRoot : Game {
        private ICondition _quit =
            new AnyCondition(
                new KeyboardCondition(Keys.Escape),
                new GamePadCondition(GamePadButton.Back, 0)
            );

        public GameRoot() {
            Data.Root = this;
            Data.Window = Window;
            Data.Graphics = new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = GameSettings.StartWindowWidth,
                PreferredBackBufferHeight = GameSettings.StartWindowHeight,
                IsFullScreen = GameSettings.StartFullScreen,
                HardwareModeSwitch = false
            };
            Content.RootDirectory = "Content";
            Data.Content = Content;
        }

        protected override void Initialize() {
            Window.Title = "Shit Game";

            IsMouseVisible = true;
            IsFixedTimeStep = true;

            Window.AllowUserResizing = true;
            Window.AllowAltF4 = true;

            Data.Graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Data.Graphics.SynchronizeWithVerticalRetrace = true;

            Data.Graphics.ApplyChanges();
            base.Initialize();

            InputHelper.Setup(this);

            Window.ClientSizeChanged += OnScreenSizeChange;
            OnScreenSizeChange(null, null);
        }

        private void OnScreenSizeChange(object sender, EventArgs e) {
            Data.ScreenSize = new Vector2(Data.Graphics.PreferredBackBufferWidth, Data.Graphics.PreferredBackBufferHeight);

            float outputAspectRatio = Window.ClientBounds.Width / (float)Window.ClientBounds.Height;
            float preferredAspectRatio = GameSettings.StartWindowWidth / (float)GameSettings.StartWindowHeight;

            if (preferredAspectRatio > 0f) {
                if (outputAspectRatio <= preferredAspectRatio) {
                    // output is taller than it is wider, bars on top/bottom
                    int presentHeight = (int)((Window.ClientBounds.Width / preferredAspectRatio) + 0.5f);
                    int barHeight = (Window.ClientBounds.Height - presentHeight) / 2;
                    Data.RenderRect = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
                } else {
                    // output is wider than it is tall, bars left/right
                    int presentWidth = (int)((Window.ClientBounds.Height * preferredAspectRatio) + 0.5f);
                    int barWidth = (Window.ClientBounds.Width - presentWidth) / 2;
                    Data.RenderRect = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
                }
            }
        }

        protected override void LoadContent() {
            Data.SpriteBatch = new SpriteBatch(GraphicsDevice);
            Data.GameRenderTarget = new RenderTarget2D(Data.Graphics.GraphicsDevice, GameSettings.VirtualWindowWidth, GameSettings.VirtualWindowHeight);
            OnScreenSizeChange(null, null);
            Functions.LoadAssets();
            ScreenManager.Initialise();
        }

        protected override void Update(GameTime gameTime) {
            Time.GameTime = gameTime;
            Time.DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);

            InputHelper.UpdateSetup();

            if (_quit.Pressed()) {
                switch (ScreenManager.ScreenType) {
                    case ScreenTypes.EditorLevelSelectScreen:
                    case ScreenTypes.GameScreen:
                        ScreenManager.EnterScreen(ScreenTypes.MainMenuScreen);
                        break;
                    case ScreenTypes.MainMenuScreen:
                        ScreenManager.CurrentScreen.Close(ExitAction.ExitGame);
                        ScreenTransition.Begin(() => Exit());
                        break;
                    case ScreenTypes.EditorScreen:
                        ScreenManager.CurrentScreen.Close(ExitAction.ExitGame);
                        ScreenManager.EnterScreen(ScreenTypes.EditorLevelSelectScreen);
                        break;
                }
            }

            ScreenManager.UpdateScenes();

            Camera.Update();
            InputHelper.UpdateCleanup();
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.SetRenderTarget(Data.GameRenderTarget);
            GraphicsDevice.Clear(ScreenManager.CurrentScreen.ClearColour);

            Data.SpriteBatch.Begin(SpriteSortMode.Deferred, samplerState : SamplerState.PointClamp, transformMatrix : Camera.Transform);
            ScreenManager.DrawScenes();
            Data.SpriteBatch.End();
            
            Data.SpriteBatch.Begin(SpriteSortMode.Deferred, samplerState : SamplerState.PointClamp);
            ScreenManager.DrawSceneUI();
            Data.SpriteBatch.End();
            
            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.Clear(Color.Black);
            Data.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp);
            Data.SpriteBatch.Draw(Data.GameRenderTarget, Data.RenderRect, null, Data.ScreenColour, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            Data.SpriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void Dispose(bool disposing) {
            ScreenManager.CurrentScreen.Close(ExitAction.ExitGame);
            base.Dispose(disposing);
        }
    }
}