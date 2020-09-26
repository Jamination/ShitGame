using System;
using Microsoft.Xna.Framework;
using ShitGame.Components;
using tainicom.Aether.Physics2D.Dynamics;

namespace ShitGame
{
    public static class Players
    {
        public const uint MaxPlayers = 1;
        
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

                Bodies[i] = Data.World.CreateRectangle(32, 32, 1, Data.ScreenCentre, 0f, BodyType.Dynamic);
                Bodies[i].IgnoreGravity = true;

                Ids[i] = i;
            }
        }

        public static void Update()
        {
            for (uint i = 0; i < MaxPlayers; i++)
            {
                Bodies[i].ApplyForce(new Vector2(Data.ToSim(1000f), 0f));
            }
        }

        public static void Draw()
        {
            for (uint i = 0; i < MaxPlayers; i++)
            {
                Functions.Draw(ref Sprites[i], Bodies[i].Position, Vector2.One, Bodies[i].Rotation);
            }
        }
    }
}