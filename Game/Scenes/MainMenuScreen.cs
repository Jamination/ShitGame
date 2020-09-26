using System;
using Microsoft.Xna.Framework;
using ShitGame.GUI;

namespace ShitGame.Scenes
{
    public sealed class MainMenuScreen : Screen
    {
        public static MainMenuScreen Instance = new MainMenuScreen();

        public UI_Button Button_Play, Button_Quit;
        
        public override void Open()
        {
            Button_Play = new UI_Button("Play", Data.ScreenCentre, () => ScreenManager.EnterScreen(ScreenTypes.GameScreen));
            Button_Play.Open();
            Button_Quit = new UI_Button("Quit", Data.ScreenCentre + new Vector2(0f, 40f), () => Close(ExitAction.ExitGame));
            Button_Quit.Open();
        }

        public override void Close(ExitAction exitAction)
        {
            Button_Play.Close();
            Button_Quit.Close();
            
            if (exitAction == ExitAction.ExitGame)
                ScreenTransition.Begin(() => Data.Root.Exit());
        }

        public override void Update()
        {
            Button_Play.Update();
            Button_Quit.Update();
        }

        public override void Draw()
        {
            Button_Play.Draw();
            Button_Quit.Draw();
        }
    }
}