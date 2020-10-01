using Microsoft.Xna.Framework;
using ShitGame.GUI;

namespace ShitGame.Scenes
{
    public sealed class EditorLevelSelectScreen : Screen
    {
        public static EditorLevelSelectScreen Instance = new EditorLevelSelectScreen();
        
        public static UI_Button[,] Buttons_Levels = new UI_Button[5, 3];
        
        public override void Open()
        {
            Camera.Position = Data.ScreenCentre;

            uint level = 0;

            for (uint i = 0; i < Buttons_Levels.GetLength(0); i++)
            {
                for (uint j = 0; j < Buttons_Levels.GetLength(1); j++)
                {
                    Buttons_Levels[i, j] = new UI_Button($"Level {level}", new Vector2(60 + j * 150 * 4, 25 + i * 50 * 4),
                        () =>
                        {
                            ScreenManager.EnterScreen(ScreenTypes.EditorScreen);
                        });
                    Buttons_Levels[i, j].Text.Centered = false;
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

        public override void DrawUI()
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