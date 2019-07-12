using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class Player
    {
        private Button selectionButton;
        private string name;
        private int score;
        private Color color;

        public Player(string name, Color color, Button button)
        {
            this.name = name;
            this.color = color;
            selectionButton = button;
            score = 0;
        }
        

    }
}
