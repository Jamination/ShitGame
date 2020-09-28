using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShitGame.Components;

namespace ShitGame {
    public static class Pool {
        public static StaticObject[] GameObjects_Static = new StaticObject[Data.MaxObjects_Static];
        public static SpriteObject[] SpriteObjects = new SpriteObject[Data.MaxObjects_Static];

        private static readonly Stack<uint> _freeStaticObjectIDs = new Stack<uint>();
        private static readonly Stack<uint> _freeSpriteIDs = new Stack<uint>();

        static Pool() {
            for (uint i = 0; i < GameObjects_Static.Length; i++) {
                if (!GameObjects_Static[i].Active)
                    _freeStaticObjectIDs.Push(i);
            }
            
            for (uint i = 0; i < SpriteObjects.Length; i++) {
                if (!SpriteObjects[i].Active)
                    _freeSpriteIDs.Push(i);
            }
        }

        public static void Reset() {
            _freeStaticObjectIDs.Clear();
            for (uint i = 0; i < GameObjects_Static.Length; i++) {
                GameObjects_Static[i].Active = false;
                if (GameObjects_Static[i].Body != null) {
                    Data.World.Remove(GameObjects_Static[i].Body);
                    GameObjects_Static[i].Body = null;
                }
                _freeStaticObjectIDs.Push(i);
            }
            _freeSpriteIDs.Clear();
            for (uint i = 0; i < SpriteObjects.Length; i++) {
                SpriteObjects[i].Active = false;
                _freeSpriteIDs.Push(i);
            }
        }

        public static uint GetInactiveGameObject_Static() {
            if (_freeStaticObjectIDs.TryPop(out var id)) {
                GameObjects_Static[id] = new StaticObject();
                GameObjects_Static[id].Active = true;
                GameObjects_Static[id].Sprite.Colour = Color.White;
                GameObjects_Static[id].ID = id;
                return id;
            }
            throw new Exception("Max game objects reached!");
        }
        
        public static uint GetInactiveSpriteObject() {
            if (_freeSpriteIDs.TryPop(out var id)) {
                SpriteObjects[id] = new SpriteObject();
                SpriteObjects[id].Active = true;
                SpriteObjects[id].Sprite.Colour = Color.White;
                SpriteObjects[id].ID = id;
                return id;
            }
            throw new Exception("Max sprite objects reached!");
        }
    }
}