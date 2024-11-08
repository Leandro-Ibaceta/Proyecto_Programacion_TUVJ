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
        Image sprite = Engine.LoadImage("assets/Player.png");
        //public Image Sprite { get;  }

        public int width = 100;
        public int height = 100;
        //alto

        
        public Image LoadSprite()
        {
            return sprite;
        }

        public void Shoot()
        {

        }

        public void Move()
        {

        }
    }
}
