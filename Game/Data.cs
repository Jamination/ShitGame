using System;
using Apos.Input;
using Dcrew.Spatial;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ShitGame;
using tainicom.Aether.Physics2D.Diagnostics;
using tainicom.Aether.Physics2D.Dynamics;

namespace ShitGame
{
    public static class Data
    {
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

        public static SpriteFont ButtonFont, SmallFont;

        public static Texture2D Texture_Player, Texture_Wall, Texture_Zombie, Texture_Background_Level1;

        public const uint MaxObjects_Static = 1000;
        public const uint MaxSprites = 500;

        public static LevelType CurrentLevel = LevelType.Undefined;

        public static Vector2 PlayerSpawnPoint = Vector2.Zero;
        
        public static readonly World World = new World(Vector2.Zero);
        public static readonly DebugView DebugView = new DebugView(World);

        public static Quadtree<SpatialItem> GameObjects = new Quadtree<SpatialItem>();

        public const float PIXELS_PER_METER = 100,
            SIM_UNITS_PER_PIXEL = 1 / PIXELS_PER_METER;
    }
}