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
        public static Vector2 CameraVelocity;

        public override void Open()
        {
            Camera.Position = Vector2.Zero;
            Functions.LoadLevel(LevelType.Level1);
        }

        public override void Close(ExitAction exitAction)
        {
            
        }

        public override void Update()
        {
            if (MouseCondition.Pressed(MouseButton.LeftButton))
                Functions.PlaceStaticObject(Data.MousePosition.X, Data.MousePosition.Y, ObjectType.Wall);

            CameraVelocity = Vector2.Lerp(CameraVelocity, Vector2.Zero, .25f);
            
            if (KeyboardCondition.Held(Keys.A))
                CameraVelocity -= new Vector2(CameraSpeed, 0f);
            if (KeyboardCondition.Held(Keys.D))
                CameraVelocity += new Vector2(CameraSpeed, 0f);
            if (KeyboardCondition.Held(Keys.W))
                CameraVelocity -= new Vector2(0f, CameraSpeed);
            if (KeyboardCondition.Held(Keys.S))
                CameraVelocity += new Vector2(0f, CameraSpeed);
            
            if (KeyboardCondition.Pressed(Keys.Enter))
                Functions.SaveLevel(LevelType.Level1);

            Camera.Position += CameraVelocity * Time.DeltaTime;
        }

        public override void Draw()
        {
        }
    }
}