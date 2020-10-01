using System;
using System.Collections.Generic;
using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ShitGame.Components;
using tainicom.Aether.Physics2D.Dynamics;

namespace ShitGame
{
    public static class Players
    {
        public static float MoveSpeed { get; private set; } = Functions.ToSim(8000);

        public static int MaxPlayers { get; private set; }
        public static int LocalID { get; private set; } = -1;
        public static Sprite[] Sprites { get; private set; }
        public static Body[] Bodies { get; private set; }
        public static float[] Angles { get; private set; }
        public static Vector2[] Directions { get; private set; }

        static readonly LinkedList<int> _freeIDs = new LinkedList<int>();
        static readonly HashSet<int> _takenIDs = new HashSet<int>();

        internal static void Init(int maxPlayers)
        {
            MaxPlayers = maxPlayers;
            Sprites = new Sprite[maxPlayers];
            Bodies = new Body[maxPlayers];
            Angles = new float[maxPlayers];
            Directions = new Vector2[maxPlayers];
            
            for (int i = 0; i < maxPlayers; i++)
                _freeIDs.AddLast(i);
        }

        internal static void Insert(int i)
        {
            _freeIDs.Remove(i);
            _takenIDs.Add(i);

            Sprites[i] = new Sprite {
                Colour = Color.White,
                Centered = true,
                Texture = Data.Texture_Player
            };

            Bodies[i] = Data.World.CreateCircle(Functions.ToSim(16 * 3), 1, Functions.ToSim(Data.PlayerSpawnPoint), BodyType.Dynamic);
            Bodies[i].LinearDamping = 16f;
            Bodies[i].AngularDamping = 2f;
            Bodies[i].FixedRotation = true;
            Bodies[i].Position = Data.PlayerSpawnPoint;
        }

        internal static void Remove(int i)
        {
            if (!_takenIDs.Remove(i))
                return;
            Data.World.Remove(Bodies[i]);
            Bodies[i] = null;
            Sprites[i] = default;
            _freeIDs.AddLast(i);
        }

        internal static void InsertLocal(int i)
        {
            Insert(i);
            LocalID = i;
        }

        internal static int GetFreeID()
        {
            int i = _freeIDs.Last.Value;
            _freeIDs.RemoveLast();
            return i;
        }

        internal static void Clear()
        {
            foreach (int i in _takenIDs)
                Remove(i);
            LocalID = -1;
        }
        
        public static void Update()
        {
            Directions[LocalID] = Vector2.Zero;
            
            if (KeyboardCondition.Held(Keys.A))
                Directions[LocalID] -= new Vector2(MoveSpeed, 0f);
            if (KeyboardCondition.Held(Keys.D))
                Directions[LocalID] += new Vector2(MoveSpeed, 0f);
            if (KeyboardCondition.Held(Keys.W))
                Directions[LocalID] -= new Vector2(0f, MoveSpeed);
            if (KeyboardCondition.Held(Keys.S))
                Directions[LocalID] += new Vector2(0f, MoveSpeed);
            
            if (Directions[LocalID] != Vector2.Zero)
                Directions[LocalID].Normalize();
            
            Bodies[LocalID].ApplyForce(Directions[LocalID] * MoveSpeed);
            Bodies[LocalID].Position = Vector2.Clamp(Bodies[LocalID].Position, new Vector2(Functions.ToSim(-128 * 12)), new Vector2(Functions.ToSim(128 * 12)));

            Angles[LocalID] = MathF.Atan2(Data.MousePosition.Y - Functions.FromSim(Bodies[LocalID].Position.Y), Data.MousePosition.X - Functions.FromSim(Bodies[LocalID].Position.X));
        }

        public static void Draw()
        {
            foreach (int i in _takenIDs)
                Functions.Draw(ref Sprites[i], Functions.FromSim(Bodies[i].Position), Vector2.One * 6, Angles[i]);
        }
    }
}