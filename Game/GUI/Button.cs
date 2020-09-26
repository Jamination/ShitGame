using System;
using Apos.Input;
using Microsoft.Xna.Framework;
using ShitGame.Components;

namespace ShitGame.GUI
{
    public class UI_Button : UI_Base
    {
        public Text Text;
        public Transform Transform;
        
        public Action Callback;

        public Color 
            IdleColour, IdleTextColour,
            HoveringColour, HoveringTextColour,
            PressedColour, PressedTextColour;

        public Rectangle Bounds;

        public UI_ButtonState ButtonState = UI_ButtonState.Idle;
        
        private ICondition _buttonPress =
            new AnyCondition(
                new MouseCondition(MouseButton.LeftButton)
            );

        private float _tick = 0f;

        public UI_Button(string message, Vector2 position, Action callback)
        {
            Callback = callback;

            IdleTextColour = Color.White;
            HoveringTextColour = Color.LightGray;
            PressedTextColour = Color.Yellow;
            
            Transform = new Transform();
            Transform.Position = position;
            Transform.Scale = Vector2.Zero;
            
            Text = new Text();
            Text.Centered = true;
            Text.Message = message;
            Text.Colour = IdleTextColour;
            Text.SpriteFont = Data.ButtonFont;
            
            DisplayState = DisplayState.Opening;

            var fontSize = Text.SpriteFont.MeasureString(message);
            Bounds = new Rectangle(0, 0, (int)fontSize.X, (int)fontSize.Y);
        }

        public override void Open()
        {
        }

        public override void Update()
        {
            switch (DisplayState)
            {
                case DisplayState.Opening:
                    if (Transform.Scale.X >= 1f || Transform.Scale.Y >= 1f)
                        DisplayState = DisplayState.Opened;
                    Transform.Scale += new Vector2(.05f);
                    break;
                case DisplayState.Opened:
                    if (new Rectangle(Transform.Position.ToPoint() - (Bounds.Size.ToVector2() * .5f).ToPoint(),
                        Bounds.Size).Contains(Data.MousePosition))
                    {
                        if (Transform.Scale == Vector2.One)
                            ButtonState = UI_ButtonState.Hovering;
                        
                        if (_buttonPress.Pressed())
                        {
                            ButtonState = UI_ButtonState.Pressed;
                        }
                        else if (_buttonPress.Held())
                            ButtonState = UI_ButtonState.Down;
                        else if (_buttonPress.Released())
                        {
                            ButtonState = UI_ButtonState.Released;
                            Callback.Invoke();
                        }
                    }
                    else
                        ButtonState = UI_ButtonState.Idle;
                    
                    switch (ButtonState)
                    {
                        case UI_ButtonState.Idle:
                            Text.Colour = Color.Lerp(Text.Colour, IdleTextColour, .25f);
                            Transform.Scale = Vector2.Lerp(Transform.Scale, Vector2.One, .5f);
                            
                            if (Transform.Scale.X >= .95 && Transform.Scale.Y >= .95 && Transform.Scale.X <= 1.05f && Transform.Scale.Y <= 1.05f)
                                Transform.Scale = Vector2.One;
                            
                            _tick = 0f;
                            break;
                        case UI_ButtonState.Hovering:
                            Text.Colour = Color.Lerp(Text.Colour, HoveringTextColour, .25f);
                            Transform.Scale = Vector2.Lerp(Transform.Scale, new Vector2(1.25f) - new Vector2((float)Math.Sin(_tick)) * .05f, .25f);
                            _tick += .1f;
                            break;
                        case UI_ButtonState.Down:
                            Text.Colour = Color.Lerp(Text.Colour, PressedTextColour, .5f);
                            Transform.Scale = Vector2.Lerp(Transform.Scale, Vector2.One, .5f);
                            break;
                        case UI_ButtonState.Released:
                            Transform.Scale = new Vector2(1.25f);
                            ButtonState = UI_ButtonState.Hovering;
                            break;
                    }
                    break;
                case DisplayState.Closing:
                    Transform.Scale = Vector2.Lerp(Transform.Scale, new Vector2(.75f), .1f);
                    Text.Colour.A = (byte)MathHelper.Lerp(Text.Colour.A, 0f, .1f);

                    if (Text.Colour.A == 0)
                        DisplayState = DisplayState.Closed;
                    break;
                case DisplayState.Closed:
                    break;
            }
        }

        public override void Draw()
        {
            Functions.Draw(ref Text, ref Transform);
        }

        public override void Close()
        {
            base.Close();
        }
    }
}