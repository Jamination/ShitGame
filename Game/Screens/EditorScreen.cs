using System;
using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ShitGame.GUI;
using tainicom.Aether.Physics2D.Collision;
using tainicom.Aether.Physics2D.Dynamics;

namespace ShitGame.Scenes
{
    public sealed class EditorScreen : Screen
    {
        public static EditorScreen Instance = new EditorScreen();

        public const float NormalCameraSpeed = 100f;

        public static float CameraSpeed = NormalCameraSpeed;

        public static Layers Layer = Layers.StaticObjects;

        public static Body Grabbed;
        public static Vector2 GrabbedOffset;

        public override void Open()
        {
            Camera.Position = Vector2.Zero;
            Zombies.Load();
            Functions.LoadLevel(LevelType.Level1);
        }

        public override void Update()
        {
            if (KeyboardCondition.Pressed(Keys.E))
                Layer++;
            else if (KeyboardCondition.Pressed(Keys.Q))
                Layer--;
            
            if (MouseCondition.Pressed(MouseButton.RightButton))
            {
                switch (Layer)
                {
                    case Layers.StaticObjects:
                        Functions.PlaceStaticObject(Data.MousePosition.X, Data.MousePosition.Y, ObjectType.Wall);
                        break;
                    case Layers.Zombies:
                        Functions.PlaceZombie(Data.MousePosition.X, Data.MousePosition.Y, ZombieType.Regular);
                        break;
                }
            }
            
            if (MouseCondition.Pressed(MouseButton.MiddleButton))
            {
                var thing = Data.World.TestPoint(Data.ToSim(Data.MousePosition));

                if (thing != null && thing.Body != null)
                {
                    Pool.GameObjects_Static[Functions.ConvertBodyToStaticObject(thing.Body)].Active = false;
                }
            }
            
            if (MouseCondition.Pressed(MouseButton.LeftButton))
            {
                var thing = Data.World.TestPoint(Data.ToSim(Data.MousePosition));

                if (thing != null && thing.Body != null)
                {
                    Grabbed = thing.Body;
                    GrabbedOffset = thing.Body.Position - Data.ToSim(Data.MousePosition);
                }
            }

            if (MouseCondition.Released(MouseButton.LeftButton))
            {
                Grabbed = null;
                GrabbedOffset = Vector2.Zero;
            }

            if (Grabbed != null)
            {
                Grabbed.Position = Data.ToSim(Data.MousePosition) + GrabbedOffset;
            }

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
        }

        public override void Draw()
        {
            Functions.DrawObjects();
            Zombies.Draw();
        }
    }
}