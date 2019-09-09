using System;
using System.Linq;
using System.IO;
using NAudio.Wave;
using static Raylib.Raylib;

namespace Memory
{
    class Program
    {
        static string backslash = ((char)92).ToString();
        public static string ButtonSound { get; private set; }
        public static string ThemeSound { get; private set; }
        public static string GameplaySound { get; private set; }

        static void Main(string[] args)
        {
            InitWindow(640, 480, "Memory");
            InitializeSoundFilePaths();

            SetWindowIcon(LoadImage(GetIconPath()));
            
            var gameManager = new GameManager();
            gameManager.GameLoop();

            CloseWindow();

            Environment.Exit(1);
        }

        private static void InitializeSoundFilePaths()
        {
            var soundPaths = Directory.GetFiles(Program.PathToSounds());
            GameplaySound = soundPaths[0];
            ButtonSound = soundPaths[1];
            ThemeSound = soundPaths[2];
        }

        private static string GetIconPath()
        {
            var picturesPath = PathToPictures() + backslash + "icon";

            return Directory.GetFiles(picturesPath).FirstOrDefault();
        }

        public static string GetCardsPath()
        {
            return PathToPictures() + backslash + "willdabeast";
        }

        private static string PathToAssets()
        {
            var path = Directory.GetCurrentDirectory().Split(backslash[0]);
            Array.Resize(ref path, path.Length - 2);

            var newPath = string.Join(backslash, path) + backslash + "Assets";

            return newPath;
        }

        public static string PathToPictures()
        {
            return PathToAssets() + backslash + "Pictures";
        }

        public static string PathToSounds()
        {
            return PathToAssets() + backslash + "Sounds";
        }
    }
}
