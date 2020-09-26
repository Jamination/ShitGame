using System;
using System.Collections.Generic;
using ShitGame.Components;

namespace ShitGame
{
    public static class Pool
    {
        public static GameObject[] GameObjects = new GameObject[Data.MaxObjects];

        static readonly Stack<int> _freeIDs = new Stack<int>();

static Pool(){
            for (int i = 0; i < GameObjects.Length; i++)
            {
                if (!GameObjects[i].Active)
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