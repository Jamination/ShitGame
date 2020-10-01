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

        public const int NormalCameraSpeed = 300;

        public static float CameraSpeed = NormalCameraSpeed;

        public static Layers Layer = Layers.StaticObjects;

        public static Body Grabbed;
        public static Vector2 GrabbedOffset;

        public static UI_Label Label_CurrentLayer;
        
        public static bool Playing = false;

        public override void Open()
        {
            Camera.Position = Vector2.Zero;
            
            Players.Init(1);
            Players.InsertLocal(Players.GetFreeID());
            
            Zombies.Load();
            Functions.LoadLevel(LevelType.Level1);
            Background.Setup();
            Players.Bodies[Players.LocalID].Position = Functions.ToSim(Data.PlayerSpawnPoint);
            Camera.Position = Functions.FromSim(Players.Bodies[Players.LocalID].Position);

            Label_CurrentLayer = new UI_Label(Enum.GetNames(typeof(Layers))[(byte)Layer], Color.White, new Vector2(12, 6), Data.SmallFont, false);
        }

        public override void Update()
        {
            if (KeyboardCondition.Pressed(Keys.R))
                Playing = !Playing;

            if (!Playing)
            {

                if (KeyboardCondition.Pressed(Keys.E))
                {
                    if ((byte) Layer < Enum.GetNames(typeof(Layers)).Length - 1)
                        Layer++;
                    Label_CurrentLayer.Text.Message = Enum.GetNames(typeof(Layers))[(byte) Layer];
                }
                else if (KeyboardCondition.Pressed(Keys.Q))
                {
                    if ((byte) Layer > 0)
                        Layer--;
                    Label_CurrentLayer.Text.Message = Enum.GetNames(typeof(Layers))[(byte) Layer];
                }

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
                    var thing = Data.World.TestPoint(Functions.ToSim(Data.MousePosition));

                    if (thing != null && thing.Body != null)
                    {
                        if (thing.Body.Tag is StaticObjectTag)
                            Pool.GameObjects_Static[Functions.ConvertBodyToStaticObject(thing.Body)].Active = false;
                        else if (thing.Body.Tag is ZombieTag)
                            Zombies.Active[Functions.ConvertBodyToStaticObject(thing.Body)] = false;
                    }
                }

                if (MouseCondition.Pressed(MouseButton.LeftButton))
                {
                    var thing = Data.World.TestPoint(Functions.ToSim(Data.MousePosition));

                    if (thing != null && thing.Body != null)
                    {
                        Grabbed = thing.Body;
                        GrabbedOffset = thing.Body.Position - Functions.ToSim(Data.MousePosition);
                    }
                }

                if (MouseCondition.Released(MouseButton.LeftButton))
                {
                    Grabbed = null;
                    GrabbedOffset = Vector2.Zero;
                }

                if (Grabbed != null)
                {
                    Grabbed.Position = Functions.ToSim(Data.MousePosition) + GrabbedOffset;
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
                
                Camera.Position = Vector2.Clamp(Camera.Position, new Vector2(-128 * 12) + Data.ScreenCentre,
                    new Vector2(128 * 12) - Data.ScreenCentre);

                if (KeyboardCondition.Pressed(Keys.Enter))
                    Functions.SaveLevel(LevelType.Level1);

                Label_CurrentLayer.Update();
            }
            else
                GameScreen.Instance.Update();
        }

        public override void Draw()
        {
            Background.Draw();
            Functions.DrawObjects();
            Zombies.Draw();
            Players.Draw();
        }

        public override void DrawUI()
        {
            if (!Playing)
                Label_CurrentLayer.Draw();
        }
    }
}