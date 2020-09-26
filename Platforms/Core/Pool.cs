using System;
using ShitGame.Components;

namespace ShitGame
{
    public static class Pool
    {
        public static GameObject[] GameObjects = new GameObject[Data.MaxObjects];

        public static int GetInactiveGameObject()
        {
            for (int i = 0; i < GameObjects.Length; i++)
            {
                if (!GameObjects[i].Active)
                    return (int)GameObjects[i].ID;
            }
            throw new Exception("Max game objects reached!");
            return -1;
        }
    }
}