using ShitGame.GUI;

namespace ShitGame.Scenes
{
    public sealed class GameScreen : Screen
    {
        public static GameScreen Instance = new GameScreen();
        
        public override void Open()
        {
            Pool.Reset();
            Players.Load();
        }

        public override void Update()
        {
            Data.World.Step(Time.DeltaTime);
            Players.Update();
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