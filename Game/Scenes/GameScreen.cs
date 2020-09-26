using Microsoft.Xna.Framework;
using ShitGame.GUI;

namespace ShitGame.Scenes
{
    public sealed class GameScreen : Screen
    {
        public static GameScreen Instance = new GameScreen();
        
        public override void Open()
        {
            ClearColour = new Color(0f, .1f, .05f, 1f);
            Pool.Reset();
            Data.World.Clear();
            Players.Load();
            Camera.Position = Data.FromSim(Players.Bodies[0].Position);
        }

        public override void Update()
        {
            Data.World.Step((float)Time.GameTime.ElapsedGameTime.TotalMilliseconds);
            Players.Update();
            Camera.Position = Vector2.Lerp(Camera.Position, Data.FromSim(Players.Bodies[0].Position), .1f);
        }

        public override void Draw()
        {
            Players.Draw();
        }

        public override void Close(ExitAction exitAction)
        {
            
        }
    }
}