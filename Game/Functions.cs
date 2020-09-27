using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShitGame.Components;

namespace ShitGame
{
    public static class Functions
    {
        public static void Draw(ref Sprite sprite, ref Transform transform)
        {
            var centerOrigin = Vector2.Zero;
            
            if (sprite.Centered)
                centerOrigin = sprite.Texture.Bounds.Size.ToVector2() * .5f;
            
            Data.SpriteBatch.Draw(
                sprite.Texture,
                new Vector2((int)transform.Position.X, (int)transform.Position.Y),
                sprite.SourceRect,
                sprite.Colour,
                transform.Rotation,
                centerOrigin + sprite.Origin,
                transform.Scale,
                sprite.Effects,
                sprite.Depth
            );
        }
        
        public static void Draw(ref Sprite sprite, Vector2 position, Vector2 scale, float rotation = 0f)
        {
            var centerOrigin = Vector2.Zero;
            
            if (sprite.Centered)
                centerOrigin = sprite.Texture.Bounds.Size.ToVector2() * .5f;
            
            Data.SpriteBatch.Draw(
                sprite.Texture,
                new Vector2((int)position.X, (int)position.Y),
                sprite.SourceRect,
                sprite.Colour,
                rotation,
                centerOrigin + sprite.Origin,
                scale,
                sprite.Effects,
                sprite.Depth
            );
        }
        
        public static void Draw(ref Text text, ref Transform transform)
        {
            var centerOrigin = Vector2.Zero;

            if (text.Centered)
                centerOrigin = text.SpriteFont.MeasureString(text.Message) * .5f;

            Data.SpriteBatch.DrawString(
                text.SpriteFont,
                text.Message,
                transform.Position,
                text.Colour,
                transform.Rotation,
                centerOrigin,
                transform.Scale,
                text.Effects,
                text.Depth
            );
        }

        public static void LoadLevel(LevelType level)
        {
            
        }
        
        public static T Choose<T>(params T[] list) => list[Data.Random.Next(0, list.ToArray().Length)];
        
        public static float Map(float value, float fromLow, float fromHigh, float toLow, float toHigh) => 
            (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        
        public static Vector2 ScreenToWorld(Vector2 onScreen) => 
            Vector2.Transform(onScreen, Matrix.Invert(Camera.Transform));
    }
}