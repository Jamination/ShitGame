using System;
using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ShitGame.Components;
using tainicom.Aether.Physics2D.Dynamics;

namespace ShitGame
{
    public static class Players
    {
        public const uint MaxPlayers = 1;
        public const float MoveSpeed = .25f;
        
        public static Sprite[] Sprites = new Sprite[MaxPlayers];
        public static Body[] Bodies = new Body[MaxPlayers];
        
        public static uint[] Ids = new uint[MaxPlayers];
        public static uint LocalID = 0;

        public static void Load()
        {
            for (uint i = 0; i < MaxPlayers; i++)
            {
                Sprites[i] = new Sprite();
                Sprites[i].Colour = Color.White;
                Sprites[i].Centered = true;
                Sprites[i].Texture = Data.Texture_Player;

                Bodies[i] = Data.World.CreateRectangle(32, 32, 1, Data.ToSim(Vector2.Zero), 0f, BodyType.Dynamic);
                Bodies[i].IgnoreGravity = true;
                Bodies[i].LinearDamping = .01f;
                Bodies[i].FixedRotation = true;

                Ids[i] = i;
            }
        }

        public static void Update()
        {
            for (uint i = 0; i < MaxPlayers; i++)
            {
                if (KeyboardCondition.Held(Keys.A))
                    Bodies[i].ApplyForce(new Vector2(-MoveSpeed, 0f));
                if (KeyboardCondition.Held(Keys.D))
                    Bodies[i].ApplyForce(new Vector2(MoveSpeed, 0f));
                if (KeyboardCondition.Held(Keys.W))
                    Bodies[i].ApplyForce(new Vector2(0f, -MoveSpeed));
                if (KeyboardCondition.Held(Keys.S))
                    Bodies[i].ApplyForce(new Vector2(0f, MoveSpeed));
            }
        }

        public static void Draw()
        {
            for (uint i = 0; i < MaxPlayers; i++)
            {
                Functions.Draw(ref Sprites[i], Data.FromSim(Bodies[i].Position), Vector2.One * .1f, Data.FromSim(Bodies[i].Rotation));
            }
        }
    }
}