using Microsoft.Xna.Framework;
using ShitGame.Components;

namespace ShitGame
{
    public static class Background
    {
        public static Transform Transform;
        public static Sprite Sprite;

        static Background()
        {
            Transform = new Transform();
            Transform.Scale = Vector2.One * 12f;
        }

        public static void Setup()
        {
            Sprite = new Sprite();
            Sprite.Colour = Color.White;
            Sprite.Centered = true;

            switch (Data.CurrentLevel)
            {
                case LevelType.Level1:
                    Sprite.Texture = Data.Texture_Background_Level1;
                    break;
                case LevelType.Undefined:
                    Sprite = new Sprite();
                    break;
            }
        }

        public static void Draw()
        {
            Functions.Draw(ref Sprite, ref Transform);
        }
    }
}