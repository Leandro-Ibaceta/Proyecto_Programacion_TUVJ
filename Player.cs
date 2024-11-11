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
        public Image Sprite { get { return sprite; } }

        public float Speed { get { return speed; } }

        public int width = 100;
        public int height = 100;

        public float PosX { get; set; }
        public float PosY { get; set; }

        public Player(float posX, float posY)
        {
            this.PosX = posX;
            this.PosY = posY;
        }
        




        public void Shoot()
        {

        }

        public void Move()
        {

        }

        public void MoveDiagonal(int x, int y)
        {

        }
    }
}
