using System;
using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ShitGame;
using tainicom.Aether.Physics2D.Dynamics;

namespace ShitGame {
    public static class Data {
        public static GraphicsDeviceManager Graphics;
        public static SpriteBatch SpriteBatch;
        public static GameWindow Window;
        public static ContentManager Content;
        public static Game Root;

        public static RenderTarget2D GameRenderTarget;
        public static Color ScreenColour = Color.White;

        public static Rectangle RenderRect;

        public static Vector2 MousePosition => Functions.ScreenToWorld(InputHelper.NewMouse.Position.ToVector2() / VirtualToRealScreenRatio);

        public static Vector2 ScreenSize;
        public static readonly Vector2 ScreenCentre = new Vector2(GameSettings.VirtualWindowWidth * .5f, GameSettings.VirtualWindowHeight * .5f);

        public static Vector2 VirtualToRealScreenRatio => new Vector2(ScreenSize.X / GameSettings.VirtualWindowWidth, ScreenSize.Y / GameSettings.VirtualWindowHeight);

        public static Random Random = new Random();

        public static SpriteFont ButtonFont;

        public static Texture2D Texture_Player, Texture_Wall;

        public const uint MaxObjects_Static = 1000;

        public static LevelType CurrentLevel = LevelType.Undefined;

        public static Vector2 PlayerSpawnPoint = Vector2.Zero;

        public static readonly World World = new World(Vector2.Zero);

        public const float PIXELS_PER_METER = 100,
            SIM_UNITS_PER_PIXEL = 1 / PIXELS_PER_METER;

        public static float FromSim(float simUnits) => simUnits * PIXELS_PER_METER;
        public static float FromSim(double simUnits) => (float)(simUnits * PIXELS_PER_METER);
        public static float FromSim(int simUnits) => simUnits * PIXELS_PER_METER;
        public static Vector2 FromSim(Vector2 simUnits) => simUnits * PIXELS_PER_METER;
        public static float ToSim(float displayUnits) => displayUnits * SIM_UNITS_PER_PIXEL;
        public static float ToSim(double displayUnits) => (float)(displayUnits * SIM_UNITS_PER_PIXEL);
        public static float ToSim(int displayUnits) => displayUnits * SIM_UNITS_PER_PIXEL;
        public static Vector2 ToSim(Vector2 displayUnits) => displayUnits * SIM_UNITS_PER_PIXEL;

        public static void LoadAssets() {
            ButtonFont = Content.Load<SpriteFont>("Fonts/ButtonFont");

            Texture_Player = Content.Load<Texture2D>("Sprites/Shit");
            Texture_Wall = Content.Load<Texture2D>("Sprites/Wall_1x1");
        }
    }
}