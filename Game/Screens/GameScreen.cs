using System;
using Microsoft.Xna.Framework;
using ShitGame.GUI;

namespace ShitGame.Scenes {
    public sealed class GameScreen : Screen {
        public static GameScreen Instance = new GameScreen();

        public override void Open() {
            ClearColour = new Color(0f, .1f, .05f, 1f);

            Players.Init(1);
            Players.InsertLocal(Players.GetFreeID());
            Players.Load();
            Camera.Position = Data.FromSim(Players.Bodies[Players.LocalID].Position);
            Functions.LoadLevel(LevelType.Level1);
        }

        public override void Update() {
            Data.World.Step(MathF.Min(Time.DeltaTime, (1 / 30f)));
            Players.Update();
            Camera.Position = Vector2.Lerp(Camera.Position, Data.FromSim(Players.Bodies[0].Position), .1f);
        }

        public override void Draw() {
            Players.Draw();
        }

        public override void Close(ExitAction exitAction) {

        }
    }
}