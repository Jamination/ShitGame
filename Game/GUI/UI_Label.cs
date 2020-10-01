using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShitGame.Components;

namespace ShitGame.GUI
{
    public class UI_Label : UI_Base
    {
        public Text Text;
        public Transform Transform;

        public UI_Label(string message, Color colour, Vector2 position, SpriteFont font, bool centered = true)
        {
            Text = new Text();
            Text.Message = message;
            Text.Centered = centered;
            Text.Colour = colour;
            Text.SpriteFont = font;
            
            Transform = new Transform();
            Transform.Position = position;
            Transform.Scale = Vector2.One;
        }

        public override void Update()
        {
            
        }

        public override void Draw()
        {
            Functions.Draw(ref Text, ref Transform);
        }
    }
}