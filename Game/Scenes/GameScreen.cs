using Microsoft.Xna.Framework;
using Penumbra;
using ShitGame.GUI;

namespace ShitGame.Scenes
{
    public sealed class GameScreen : Screen
    {
        public static GameScreen Instance = new GameScreen();

        public static Light Light;
        
        public override void Open()
        {
            ClearColour = new Color(0f, .1f, .05f, 1f);
            
            Pool.Reset();
            Data.World.Clear();
            Data.World.Enabled = true;
            Data.PenumbraComponent.Lights.Clear();
            Data.PenumbraComponent.Enabled = true;
            Data.PenumbraComponent.Visible = true;
            
            Light = new PointLight();
            Light.Scale = Vector2.One * 10000f;
            Light.Color = Color.Red;
            Light.Intensity = 1;
            Data.PenumbraComponent.Lights.Add(Light);
            
            Players.Load();
            Camera.Position = Data.FromSim(Players.Bodies[0].Position);
        }

        public override void Update()
        {
            Data.World.Step((float)Time.GameTime.ElapsedGameTime.TotalMilliseconds);
            Players.Update();
            Camera.Position = Vector2.Lerp(Camera.Position, Data.FromSim(Players.Bodies[0].Position), .1f);
            Light.Position = Vector2.Zero;
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