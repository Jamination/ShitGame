using System;

namespace ShitGame
{
    public static class ScreenTransition
    {
        private static TransitionState _transitionState = TransitionState.None;
        private static Action _callBack;

        private static byte _transitionSpeed = 16;
        
        public static void Begin(Action callBack, byte transitionSpeed = 16)
        {
            if (_transitionState != TransitionState.None)
                return;
            
            _callBack = callBack;
            _transitionState = TransitionState.In;
            _transitionSpeed = transitionSpeed;
        }

        public static void Update()
        {
            switch (_transitionState)
            {
                case TransitionState.None:
                    Data.MainRenderTargetColour.R =
                        Data.MainRenderTargetColour.G = Data.MainRenderTargetColour.B = 255;
                    break;
                case TransitionState.In:
                    if (Data.MainRenderTargetColour.R <= 0)
                    {
                        Data.MainRenderTargetColour.R =
                            Data.MainRenderTargetColour.G = Data.MainRenderTargetColour.B = 0;
                        _callBack.Invoke();
                        _transitionState = TransitionState.Out;
                    }
                    else
                    {
                        if (Data.MainRenderTargetColour.R < _transitionSpeed)
                        {
                            Data.MainRenderTargetColour.R =
                                Data.MainRenderTargetColour.G = Data.MainRenderTargetColour.B = 0;
                        }
                        else
                        {
                            Data.MainRenderTargetColour.R -= _transitionSpeed;
                            Data.MainRenderTargetColour.G -= _transitionSpeed;
                            Data.MainRenderTargetColour.B -= _transitionSpeed;
                        }
                    }
                    break;
                case TransitionState.Out:
                    if (Data.MainRenderTargetColour.R < 255)
                    {
                        if (Data.MainRenderTargetColour.R > 255 - _transitionSpeed)
                        {
                            Data.MainRenderTargetColour.R =
                                Data.MainRenderTargetColour.G = Data.MainRenderTargetColour.B = 255;
                            _transitionState = TransitionState.None;
                        }
                        else
                        {
                            Data.MainRenderTargetColour.R += _transitionSpeed;
                            Data.MainRenderTargetColour.G += _transitionSpeed;
                            Data.MainRenderTargetColour.B += _transitionSpeed;
                        }
                    }
                    break;
            }
        }
    }
}