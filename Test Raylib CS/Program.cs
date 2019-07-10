using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class Program
    {
        static void Main(string[] args)
        {
            InitWindow(640, 480, "Memory");
            SetWindowIcon(LoadImage("icon.png"));
            
            var gameManager = new GameManager();

            gameManager.GameLoop();

            CloseWindow();
            System.Environment.Exit(1);
        }
    }
}
