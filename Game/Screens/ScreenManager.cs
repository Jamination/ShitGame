 using ShitGame;
 using ShitGame.GUI;

 namespace ShitGame.Scenes
{
    public static class ScreenManager
    {
        public static Screen CurrentScreen { get; private set; }
        public static Screen NextScreen { get; private set; }

        public static ScreenTypes ScreenType;

        public static void Initialise()
        {
            CurrentScreen = MainMenuScreen.Instance;
            ScreenType = ScreenTypes.MainMenuScreen;
            
            Camera.Load();
            
            CurrentScreen.Open();
        }

        public static void EnterScreen(ScreenTypes screenType)
        {
            ScreenType = screenType;
            switch (screenType)
            {
                case ScreenTypes.MainMenuScreen:
                    NextScreen = MainMenuScreen.Instance;
                    break;
                case ScreenTypes.GameScreen:
                    NextScreen = GameScreen.Instance;
                    break;
                case ScreenTypes.EditorLevelSelectScreen:
                    NextScreen = EditorLevelSelectScreen.Instance;
                    break;
                case ScreenTypes.EditorScreen:
                    NextScreen = EditorScreen.Instance;
                    break;
            }
            CurrentScreen.Close(ExitAction.ExitScreen);
        }

        public static void RestartCurrentScene()
        {
            CurrentScreen.Close(ExitAction.Restart);
            CurrentScreen.Open();
        }

        public static void UpdateScenes()
        {
            if (NextScreen != null)
            {
                ScreenTransition.Begin(() =>
                {
                    Pool.Reset();
                    CurrentScreen = NextScreen;
                    NextScreen = null;
                    CurrentScreen.Open();
                });
            }
            ScreenTransition.Update();
            CurrentScreen.Update();
        }

        public static void DrawScenes()
        {
            CurrentScreen.Draw();
        }

        public static void DrawSceneUI()
        {
            CurrentScreen.DrawUI();
        }
    }
}