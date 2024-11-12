using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player
    {
        //acomodar esto
        private Image sprite = Engine.LoadImage("assets/Player.png");
        private float speed = 20f;
        private int width = 100;
        private int height = 100;
        public Image Sprite { get { return sprite; } }

        public float Speed { get { return speed; } }

        public float PosX { get; set; }
        public float PosY { get; set; }
        public int Width { get { return height; } }
        public int Height { get { return height; } }
        public Player(float posX, float posY)
        {
            PosX = posX;
            PosY = posY;

        }

    }
}
