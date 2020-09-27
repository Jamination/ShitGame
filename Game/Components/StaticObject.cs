using tainicom.Aether.Physics2D.Dynamics;

namespace ShitGame.Components
{
    public struct StaticObject
    {
        public uint ID;
        public bool Active;
        public Sprite Sprite;
        public Body Body;
        public ObjectType Type;
    }
}