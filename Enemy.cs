using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Enemy
    {
        //usar imagenes precargardas del programa
        public Image sprite;
        private int width = 100;
        private int height = 100;

        public float posX { get; set; }
        public float posY { get; set; }

        public float Speed {  get; set; }
        public bool isActive = false;

        public int Width { get { return width; } }
        public int Heigth { get { return height; } }
        
        //revisar este metodo
        public void MoveToPlayer(float playerPosx, float playerPosY)
        {
            float deltaX = playerPosx - posX;
            float deltaY = playerPosY - posY;

            float distance = (float)Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));

            float directionX = deltaX / distance;
            float directionY = deltaY / distance;

            posX += directionX * Speed;
            posY += directionY * Speed;
        }
        public void Destroy()
        {

        }
    }
}
