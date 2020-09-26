﻿﻿using ShitGame.GUI;

  namespace ShitGame.Scenes
{
    public abstract class Screen
    {
        public DisplayState DisplayState;

        public virtual void Open()
        {
            DisplayState = DisplayState.Opened;
        }

        public virtual void Close(ExitAction exitAction)
        {
            DisplayState = DisplayState.Closed;
        }
        
        public virtual void Update() { }
        public virtual void Draw() { }
    }
}