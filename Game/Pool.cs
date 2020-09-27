using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShitGame.Components;

namespace ShitGame
{
    public static class Pool
    {
        public static StaticObject[] GameObjects_Static = new StaticObject[Data.MaxObjects_Static];

        private static readonly Stack<uint> _freeStaticIDs = new Stack<uint>();

        static Pool()
        {
            for (uint i = 0; i < GameObjects_Static.Length; i++)
            {
                if (!GameObjects_Static[i].Active)
                    _freeStaticIDs.Push(i);
            }
        }

        public static void Reset()
        {
            _freeStaticIDs.Clear();
            for (uint i = 0; i < GameObjects_Static.Length; i++)
            {
                GameObjects_Static[i].Active = false;
                _freeStaticIDs.Push(i);
            }
        }

        public static uint GetInactiveGameObject_Static()
        {
            if (_freeStaticIDs.TryPop(out var id))
            {
                GameObjects_Static[id] = new StaticObject();
                GameObjects_Static[id].Active = true;
                GameObjects_Static[id].Sprite.Colour = Color.White;
                GameObjects_Static[id].ID = id;
                return id;
            }
            throw new Exception("Max game objects reached!");
        }
    }
}