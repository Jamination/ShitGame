﻿﻿using Microsoft.Xna.Framework;
  using ShitGame.GUI;

  namespace ShitGame.Scenes
{
    public abstract class Screen
    {
        public DisplayState DisplayState;

        public Color ClearColour = new Color(0f, .05f, .1f, 1f);

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
        public virtual void DrawUI() { }
    }
}