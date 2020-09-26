namespace ShitGame.GUI
{
    public abstract class UI_Base
    {
        public DisplayState DisplayState;

        public virtual void Open()
        {
            DisplayState = DisplayState.Opening;
        }

        public virtual void Close()
        {
            DisplayState = DisplayState.Closing;
        }
        
        public virtual void Update() {}
        
        public virtual void Draw() {}
    }
}