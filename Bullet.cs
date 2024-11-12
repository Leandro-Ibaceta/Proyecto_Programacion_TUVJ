using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Bullet
    {
        //static DateTime timeLastShoot = DateTime.Now;
        private Image sprite = Engine.LoadImage("assets/Bullet.png");

        private string tag = "bullet";
        private int width = 20;
        private int height = 20;
        
        public bool isActive;
        public float Speed { get; set; } = 1000f;
        public Image Sprite { get { return sprite; } }
        public string Tag { get { return Tag; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public float PosX { get; set; }
        public float PosY { get; set; }
        

        public Bullet(float posX, float posY)
        {
            this.PosX = posX;
            this.PosY = posY;
            
        }
    }
}
