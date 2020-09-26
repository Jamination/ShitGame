using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShitGame.Components
{
    public struct Text
    {
        public string Message;
        public SpriteFont SpriteFont;
        public Color Colour;
        public float Depth;
        public bool Centered;
        public SpriteEffects Effects;
    }
}