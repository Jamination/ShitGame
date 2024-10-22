﻿using System;
using Microsoft.Xna.Framework;
using ShitGame.GUI;

namespace ShitGame.Scenes
{
    public sealed class MainMenuScreen : Screen
    {
        public static MainMenuScreen Instance = new MainMenuScreen();

        public UI_Button Button_Play, Button_Quit, Button_Editor;
        
        public override void Open()
        {
            Data.World.Clear();
            Camera.Position = Data.ScreenCentre;
            
            Button_Play = new UI_Button("Play", Data.ScreenCentre - new Vector2(0f, 80f), () => ScreenManager.EnterScreen(ScreenTypes.GameScreen));
            Button_Play.Open();
            Button_Quit = new UI_Button("Quit", Data.ScreenCentre + new Vector2(0f, 80f), () => Close(ExitAction.ExitGame));
            Button_Quit.Open();
            Button_Editor = new UI_Button("Editor", new Vector2(60 * 4, 25 * 4), () => ScreenManager.EnterScreen(ScreenTypes.EditorLevelSelectScreen));
#if DEBUG
            Button_Editor.Open();
#endif
        }

        public override void Close(ExitAction exitAction)
        {
            Button_Play.Close();
            Button_Quit.Close();
#if DEBUG
            Button_Editor.Close();
#endif
            
            if (exitAction == ExitAction.ExitGame)
                ScreenTransition.Begin(() => Data.Root.Exit());
        }

        public override void Update()
        {
            Button_Play.Update();
            Button_Quit.Update();
#if DEBUG
            Button_Editor.Update();
#endif
        }

        public override void DrawUI()
        {
            Button_Play.Draw();
            Button_Quit.Draw();
#if DEBUG
            Button_Editor.Draw();
#endif
        }
    }
}