using System;

namespace ShitGame
{
    public static class ScreenTransition
    {
        private static TransitionState _transitionState = TransitionState.Idle;
        private static Action _callBack;

        private static byte _transitionSpeed = 16;
        
        public static void Begin(Action callBack, byte transitionSpeed = 16)
        {
            _callBack = callBack;
            _transitionState = TransitionState.In;
            _transitionSpeed = transitionSpeed;
        }

        public static void Update()
        {
            switch (_transitionState)
            {
                case TransitionState.Idle:
                    Data.ScreenColour.R =
                        Data.ScreenColour.G = Data.ScreenColour.B = 255;
                    break;
                case TransitionState.In:
                    if (Data.ScreenColour.R <= 0)
                    {
                        Data.ScreenColour.R =
                            Data.ScreenColour.G = Data.ScreenColour.B = 0;
                        _callBack.Invoke();
                        _transitionState = TransitionState.Out;
                    }
                    else
                    {
                        if (Data.ScreenColour.R < _transitionSpeed)
                        {
                            Data.ScreenColour.R =
                                Data.ScreenColour.G = Data.ScreenColour.B = 0;
                        }
                        else
                        {
                            Data.ScreenColour.R -= _transitionSpeed;
                            Data.ScreenColour.G -= _transitionSpeed;
                            Data.ScreenColour.B -= _transitionSpeed;
                        }
                    }
                    break;
                case TransitionState.Out:
                    if (Data.ScreenColour.R < 255)
                    {
                        if (Data.ScreenColour.R > 255 - _transitionSpeed)
                        {
                            Data.ScreenColour.R =
                                Data.ScreenColour.G = Data.ScreenColour.B = 255;
                            _transitionState = TransitionState.Idle;
                        }
                        else
                        {
                            Data.ScreenColour.R += _transitionSpeed;
                            Data.ScreenColour.G += _transitionSpeed;
                            Data.ScreenColour.B += _transitionSpeed;
                        }
                    }
                    break;
            }
        }
    }
}