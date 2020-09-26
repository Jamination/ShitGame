﻿﻿using Microsoft.Xna.Framework;

namespace ShitGame
{
    public static class GameSettings
    {
        public const int StartWindowWidth = 1280;
        public const int StartWindowHeight = 720;
        
        public const int VirtualWindowWidth = 640;
        public const int VirtualWindowHeight = 360;
        
        public const bool StartFullScreen = false;

        public static readonly Color ClearColour = new Color(0f, .05f, .1f, 1f);
    }
}