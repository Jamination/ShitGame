using tainicom.Aether.Physics2D.Dynamics;

namespace ShitGame.Components
{
    public struct GameObject
    {
        public uint ID;
        public bool Active;
        public Sprite Sprite;
        public Transform Transform;
        public Body Body;
    }
}