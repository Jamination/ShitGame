using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShitGame.Components;
using tainicom.Aether.Physics2D.Dynamics;

namespace ShitGame
{
    public static class Zombies
    {
        public const uint MaxZombies = 500;
        
        public static Sprite[] Sprites = new Sprite[MaxZombies];
        public static Body[] Bodies = new Body[MaxZombies];
        public static bool[] Active = new bool[MaxZombies];

        private static Stack<uint> _freeZombies = new Stack<uint>();

        public static void Load()
        {
            _freeZombies.Clear();
            for (uint i = 0; i < MaxZombies; i++)
                _freeZombies.Push(i);
        }

        public static uint GetInactiveZombie() => _freeZombies.Pop();

        public static void Update()
        {
        }

        public static void Draw()
        {
            for (uint i = 0; i < MaxZombies; i++)
            {
                if (Active[i])
                {
                    Functions.Draw(ref Sprites[i], Bodies[i], Vector2.One * .25f);
                }
            }
        }
    }
}