using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using ShitGame.Components;
using tainicom.Aether.Physics2D.Dynamics;

namespace ShitGame
{
    public static class Zombies
    {
        public const uint MaxZombies = 500;

        public static float MoveSpeed { get; private set; } = Functions.ToSim(1000);

        public static Sprite[] Sprites = new Sprite[MaxZombies];
        public static Body[] Bodies = new Body[MaxZombies];
        public static bool[] Active = new bool[MaxZombies];
        public static ZombieType[] Types = new ZombieType[MaxZombies];
        public static float[] Angles = new float[MaxZombies];

        private static Stack<uint> _freeZombies = new Stack<uint>();

        public static void Load()
        {
            _freeZombies.Clear();
            for (uint i = 0; i < MaxZombies; i++)
                _freeZombies.Push(i);
        }

        public static uint GetInactiveZombie() => _freeZombies.Pop();

        public static void Update() {
            for (uint i = 0; i < MaxZombies; i++) {
                if (Active[i]) {
                    int closeP = -1;
                    float closePD = float.MaxValue;
                    for (int j = 0; j < Players.MaxPlayers; j++) {
                        float d = Vector2.DistanceSquared(Players.Bodies[j].Position, Bodies[i].Position);
                        if (d < closePD) {
                            closePD = d;
                            closeP = j;
                        }
                    }
                    if (closeP != -1 && Data.World.RayCast(Bodies[i].Position, Players.Bodies[closeP].Position).FirstOrDefault() == Players.Bodies[closeP].FixtureList.FirstOrDefault()) {
                        float a = MathF.Atan2(Players.Bodies[closeP].Position.Y - Bodies[i].Position.Y, Players.Bodies[closeP].Position.X - Bodies[i].Position.X);
                        Angles[i] = Functions.LerpAngle(Angles[i], a, .05f);
                        Bodies[i].ApplyForce(new Vector2(MathF.Cos(a) * MoveSpeed, MathF.Sin(a) * MoveSpeed));
                    }
                }
            }
        }

        public static void Draw() {
            for (uint i = 0; i < MaxZombies; i++) {
                if (Active[i]) {
                    Functions.Draw(ref Sprites[i], Functions.FromSim(Bodies[i].Position), Vector2.One, Angles[i]);
                }
            }
        }
    }
}