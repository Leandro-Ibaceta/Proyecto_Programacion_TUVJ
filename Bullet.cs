using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Bullet
    {
        private string tag = "bullet";
        private int width = 20;
        private int height = 20;
        private Image sprite;
        public int Width {get { return width;}}
        public int Height {get { return height;}}
        public float posX { get; set; }
        public float posY { get; set; }
        public string Tag { get { return Tag; } }
        public Image Sprite { get { return sprite; } }

        public Bullet(float posX, float posY)
        {
            this.posX = posX;
            this.posY = posY;
        }
    }
}
