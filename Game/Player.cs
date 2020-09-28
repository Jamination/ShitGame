using System;
using System.Collections.Generic;
using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ShitGame.Components;
using tainicom.Aether.Physics2D.Dynamics;

namespace ShitGame {
    public static class Players {
        public static float MoveSpeed { get; private set; } = Data.ToSim(200);

        public static int MaxPlayers { get; private set; }
        public static int LocalID { get; private set; } = -1;
        public static Sprite[] Sprites { get; private set; }
        public static Body[] Bodies { get; private set; }
        public static float[] Angles { get; private set; }

        static readonly LinkedList<int> _freeIDs = new LinkedList<int>();
        static readonly HashSet<int> _takenIDs = new HashSet<int>();

        internal static void Init(int maxPlayers) {
            Sprites = new Sprite[maxPlayers];
            Bodies = new Body[maxPlayers];
            Angles = new float[maxPlayers];
            for (int i = 0; i < maxPlayers; i++)
                _freeIDs.AddLast(i);
            MaxPlayers = maxPlayers;
        }

        internal static void Insert(int i) {
            _freeIDs.Remove(i);
            _takenIDs.Add(i);

            Sprites[i] = new Sprite {
                Colour = Color.White,
                Centered = true,
                Texture = Data.Texture_Player
            };

            Bodies[i] = Data.World.CreateCircle(Data.ToSim(16), 1, Data.ToSim(Data.PlayerSpawnPoint), BodyType.Dynamic);
            Bodies[i].LinearDamping = 16f;
            Bodies[i].AngularDamping = 2f;
            Bodies[i].FixedRotation = true;
        }

        internal static void Remove(int i) {
            if (!_takenIDs.Remove(i))
                return;
            Data.World.Remove(Bodies[i]);
            Bodies[i] = null;
            Sprites[i] = default;
            _freeIDs.AddLast(i);
        }

        internal static void InsertLocal(int i) {
            Insert(i);
            LocalID = i;
        }

        internal static int GetFreeID() {
            int i = _freeIDs.Last.Value;
            _freeIDs.RemoveLast();
            return i;
        }

        internal static void Clear() {
            foreach (int i in _takenIDs)
                Remove(i);
            LocalID = -1;
        }

        public static void Update() {
            if (KeyboardCondition.Held(Keys.A))
                Bodies[LocalID].ApplyForce(new Vector2(-MoveSpeed, 0f));
            if (KeyboardCondition.Held(Keys.D))
                Bodies[LocalID].ApplyForce(new Vector2(MoveSpeed, 0f));
            if (KeyboardCondition.Held(Keys.W))
                Bodies[LocalID].ApplyForce(new Vector2(0f, -MoveSpeed));
            if (KeyboardCondition.Held(Keys.S))
                Bodies[LocalID].ApplyForce(new Vector2(0f, MoveSpeed));

            Angles[LocalID] = MathF.Atan2(Data.MousePosition.Y - Data.FromSim(Bodies[LocalID].Position.Y), Data.MousePosition.X - Data.FromSim(Bodies[LocalID].Position.X));
        }

        public static void Draw() {
            foreach (int i in _takenIDs)
                Functions.Draw(ref Sprites[i], Data.FromSim(Bodies[i].Position), Vector2.One * 2, Angles[i]);
        }
    }
}