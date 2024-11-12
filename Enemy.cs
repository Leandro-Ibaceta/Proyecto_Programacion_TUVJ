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
        public Image sprite = Engine.LoadImage("assets/rhombus.png");
        private int width = 100;
        private int height = 100;

        public float PosX { get; set; }
        public float PosY { get; set; }

        public float Speed {  get; set; }
        public bool isActive = false;

        public int Width { get { return width; } }
        public int Heigth { get { return height; } }
        
        
        //revisar este metodo
        public void MoveToPlayer(float playerPosx, float playerPosY , float deltaTime)
        {
            //direccion del vector
            float deltaX = playerPosx - PosX;
            float deltaY = playerPosY - PosY;
            //magnitud del vector
            float distance = (float)Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            //normalizacion del vector
            float directionX = deltaX / distance;
            float directionY = deltaY / distance;

            PosX += directionX * Speed * deltaTime;
            PosY += directionY * Speed * deltaTime;
        }
        public void SpawnEnemies(float SpawnposX, float SpawnposY)
        {
                       
            Random random = new Random();
            int selector = random.Next(4);
            switch(selector)
            {
                //arriba
                case 0:
                    {
                        SpawnposX = random.Next(0, Engine.Ancho);
                        SpawnposY = -100;
                        break;
                    }
                //derecha
                case 1:
                    {
                        SpawnposX = Engine.Alto + 100;
                        SpawnposY = random.Next(0, Engine.Alto);
                        break;
                    }
                //abajo
                case 2:
                    {
                        SpawnposX = random.Next(0, Engine.Alto);
                        SpawnposY = Engine.Alto + 100;
                        break;
                    }
                //izquierda
                case 3:
                    {
                        SpawnposX = -100;
                        SpawnposY = random.Next(0, Engine.Alto);
                        break;
                    }

            }
            PosX = SpawnposX;
            PosY = SpawnposY;

            
        }
        public void Destroy()
        {
            isActive = false;
        }
    }
}
