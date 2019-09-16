using System;
using System.Linq;
using System.IO;
using static Raylib.Raylib;

namespace Memory
{
    class Program
    {
        private static readonly string backslash = ((char)92).ToString();

        static void Main(string[] args)
        {
            InitWindow(640, 480, "Memory");

            SetWindowIcon(LoadImage(GetIconPath()));
            
            var gameManager = new GameManager();
            gameManager.GameLoop();

            CloseWindow();

            Environment.Exit(1);
        }

        private static string GetIconPath()
        {
            var picturesPath = PathToPictures() + backslash + "icon";

            return Directory.GetFiles(picturesPath).FirstOrDefault();
        }

        public static string GetCheckMarkPath()
        {
            return Directory.GetFiles(PathToPictures() + backslash + "checkmark").FirstOrDefault();
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
