﻿using System;

namespace ShitGame {
    static class Program {
        [STAThread]
        static void Main() {
            using var game = new GameRoot();
            game.Run();
        }
    }
}