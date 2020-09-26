﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShitGame
{
    public static class Camera
    {
        public static Vector2 Position;
        public static float Zoom, Rotation;
        public static Matrix Transform;
        public static Viewport Viewport;
        
        public static Vector2 CameraUp => new Vector2(MathF.Cos(Rotation - MathF.PI / 2), MathF.Sin(Rotation - MathF.PI / 2));

        public static void Load()
        {
            Viewport = Data.Graphics.GraphicsDevice.Viewport;
            Position = Data.ScreenCentre;
            Zoom = 1f;
            Rotation = 0f;
        }

        public static void Update()
        {
            Transform = Matrix.CreateTranslation(new Vector3(
                            new Vector2((int) -Position.X, (int) -Position.Y), 0f)) * Matrix.CreateScale(Zoom) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateTranslation(new Vector3(Data.ScreenCentre.X,
                            Data.ScreenCentre.Y, 0f));
        }
    }
}