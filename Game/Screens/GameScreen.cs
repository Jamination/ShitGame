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

            Zombies.Load();

            Functions.LoadLevel(LevelType.Level1);
            Background.Setup();
            Players.Bodies[Players.LocalID].Position = Functions.ToSim(Data.PlayerSpawnPoint);
            Camera.Position = Functions.FromSim(Players.Bodies[Players.LocalID].Position);
        }

        public override void Update() {
            const float MAX_STEP = 1 / 30f;
            Data.World.Step(MathF.Min(Time.DeltaTime, MAX_STEP));
            Zombies.Update();
            Players.Update();
            Camera.FollowPlayer();
        }

        public override void Draw() {
            Background.Draw();
            Functions.DrawObjects();
            Zombies.Draw();
            Players.Draw();
        }
    }
}