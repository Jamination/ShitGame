using System;
using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ShitGame.GUI;

namespace ShitGame.Scenes
{
    public sealed class EditorScreen : Screen
    {
        public static EditorScreen Instance = new EditorScreen();

        public const float NormalCameraSpeed = 100f;

        public static float CameraSpeed = NormalCameraSpeed;

        public override void Open()
        {
            Camera.Position = Vector2.Zero;
            Zombies.Load();
            Functions.LoadLevel(LevelType.Level1);
        }

        public override void Update()
        {
            if (MouseCondition.Pressed(MouseButton.LeftButton))
                Functions.PlaceStaticObject(Data.MousePosition.X, Data.MousePosition.Y, ObjectType.Wall);

            Camera.Velocity = Vector2.Lerp(Camera.Velocity, Vector2.Zero, .25f);
            
            if (KeyboardCondition.Held(Keys.A))
                Camera.Velocity -= new Vector2(CameraSpeed, 0f);
            if (KeyboardCondition.Held(Keys.D))
                Camera.Velocity += new Vector2(CameraSpeed, 0f);
            if (KeyboardCondition.Held(Keys.W))
                Camera.Velocity -= new Vector2(0f, CameraSpeed);
            if (KeyboardCondition.Held(Keys.S))
                Camera.Velocity += new Vector2(0f, CameraSpeed);
            
            if (KeyboardCondition.Pressed(Keys.Enter))
                Functions.SaveLevel(LevelType.Level1);
            
            if (KeyboardCondition.Pressed(Keys.F))
                Functions.PlaceZombie(Data.MousePosition.X, Data.MousePosition.Y);
        }

        public override void Draw()
        {
            Functions.DrawObjects();
            Zombies.Draw();
        }
    }
}