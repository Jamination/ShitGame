using System;
using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ShitGame;

namespace ShitGame
{
    public static class Data
    {
        public static GraphicsDeviceManager Graphics;
        public static SpriteBatch SpriteBatch;
        public static GameWindow Window;
        public static ContentManager Content;
        public static Game GameInstance;

        public static RenderTarget2D MainRenderTarget;
        
        public static Rectangle RenderRect;

        public static Vector2 MousePosition => Functions.ScreenToWorld(InputHelper.NewMouse.Position.ToVector2() / VirtualToRealScreenRatio);
        
        public static Vector2 ScreenSize;
        public static readonly Vector2 ScreenCentre = new Vector2(GameSettings.VirtualWindowWidth * .5f, GameSettings.VirtualWindowHeight * .5f);
        
        public static Vector2 VirtualToRealScreenRatio => new Vector2(ScreenSize.X / GameSettings.VirtualWindowWidth, ScreenSize.Y / GameSettings.VirtualWindowHeight);
        
        public static Random Random = new Random();

        public static void LoadAssets()
        {
        }
    }
}