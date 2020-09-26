using System;
using System.Collections.Generic;
using ShitGame.Components;

namespace ShitGame
{
    public static class Pool
    {
        public static StaticObject[] GameObjects_Static = new StaticObject[Data.MaxObjects_Static];

        private static readonly Stack<int> _freeIDs = new Stack<int>();

        static Pool()
        {
            for (int i = 0; i < GameObjects_Static.Length; i++)
            {
                if (!GameObjects_Static[i].Active)
                    _freeIDs.Push(i);
            }
        }

        public static void Reset()
        {
            _freeIDs.Clear();
            for (int i = 0; i < GameObjects_Static.Length; i++)
            {
                GameObjects_Static[i].Active = false;
                _freeIDs.Push(i);
            }
        }

        public static int GetInactiveGameObject()
        {
            if (_freeIDs.TryPop(out var id))
                return id;
            throw new Exception("Max game objects reached!");
        }
    }
}