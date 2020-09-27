using Microsoft.Xna.Framework;
using ShitGame.GUI;

namespace ShitGame.Scenes
{
    public sealed class EditorLevelSelectScreen : Screen
    {
        public static EditorLevelSelectScreen Instance = new EditorLevelSelectScreen();
        
        public static UI_Button[,] Buttons_Levels = new UI_Button[3, 5];
        
        public override void Open()
        {
            Data.World.Enabled = false;

            uint level = 0;

            for (uint i = 0; i < Buttons_Levels.GetLength(0); i++)
            {
                for (uint j = 0; j < Buttons_Levels.GetLength(1); j++)
                {
                    Buttons_Levels[i, j] = new UI_Button($"Level {level}", new Vector2(60 + i * 150, 25 + j * 50),
                        () =>
                        {
                            ScreenManager.EnterScreen(ScreenTypes.EditorScreen);
                            Functions.LoadLevel((LevelType)level);
                        });
                    level++;
                }
            }
        }

        public override void Close(ExitAction exitAction)
        {
            for (uint i = 0; i < Buttons_Levels.GetLength(0); i++)
            {
                for (uint j = 0; j < Buttons_Levels.GetLength(1); j++)
                {
                    Buttons_Levels[i, j].Close();
                }
            }
        }

        public override void Update()
        {
            for (uint i = 0; i < Buttons_Levels.GetLength(0); i++)
            {
                for (uint j = 0; j < Buttons_Levels.GetLength(1); j++)
                {
                    Buttons_Levels[i, j].Update();
                }
            }
        }

        public override void Draw()
        {
            for (uint i = 0; i < Buttons_Levels.GetLength(0); i++)
            {
                for (uint j = 0; j < Buttons_Levels.GetLength(1); j++)
                {
                    Buttons_Levels[i, j].Draw();
                }
            }
        }
    }
}