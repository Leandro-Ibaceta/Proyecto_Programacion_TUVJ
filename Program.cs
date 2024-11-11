using System;
using System.Media;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using Tao.Sdl;

namespace MyGame
{

    class Program
    {

        static Image image = Engine.LoadImage("assets/fondo.png");
        static Image enemy = Engine.LoadImage("assets/rhombus.png");
        static Image bullet = Engine.LoadImage("assets/Bullet.png");

        static Font font;

        static Player player = new Player(300f, 300f);
        static Enemy[] enemies = new Enemy[5];
        static float speed = 200;
        static float posX = 300;
        static float posY = 300;
        static float enemyPosx = 10;
        static float enemyPosy = 10;
        static float enemySpeed = 5;

        static DateTime startTime = DateTime.Now;
        static float deltaTime;
        static float lastTimeFrame;

        static Random random = new Random();
        //prueba para bala
        static bool bulletIsActive = false;
        static float bulletSpeed = 20f;
        static float fireRate = 0.5f;
        static DateTime timeLastShoot = DateTime.Now;
        static float bulletPositionX;
        static float bulletPositionY;


        static void Main(string[] args)
        {
            Engine.Initialize();
            font = Engine.LoadFont("assets/Font/RobotoCondensed-Bold.ttf", 40);


            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new Enemy();
                enemies[i].posX = i * 30;
                enemies[i].posY = i * 30;
                enemies[i].sprite = enemy;
                enemies[i].Speed = 10;
                enemies[i].isActive = true;
            }


            while (true)
            {

                CheckInputs();
                Update();
                Render();
                Sdl.SDL_Delay(20);
            }
        }

        static void CheckInputs()
        {
            //shoot
            if (Engine.KeyPress(Engine.KEY_LEFT))
            {
            }
            if (Engine.KeyPress(Engine.KEY_RIGHT))
            {
            }
            if (Engine.KeyPress(Engine.KEY_UP))
            {

                    bulletIsActive = true;
                if ((DateTime.Now - timeLastShoot).TotalSeconds > fireRate)
                {
                    bulletPositionX = player.PosX + player.width / 2;
                    bulletPositionY = player.PosY;
                    timeLastShoot = DateTime.Now;

                }

            }
            if (Engine.KeyPress(Engine.KEY_DOWN))
            {
            }
            //move 
            if (Engine.KeyPress(Engine.KEY_W))
            {
                player.PosY -= speed * deltaTime;

            }
            if (Engine.KeyPress(Engine.KEY_A))
            {
                player.PosX -= speed * deltaTime;

            }
            if (Engine.KeyPress(Engine.KEY_S))
            {

                player.PosY += speed * deltaTime;
            }
            if (Engine.KeyPress(Engine.KEY_D))
            {
                player.PosX += speed * deltaTime;

            }

            if (Engine.KeyPress(Engine.KEY_ESC))
            {
                Environment.Exit(0);
            }
            if (Engine.KeyPress(Engine.KEY_ENTER))
            {

            }
        }

        static void Update()
        {
            float curentTime = (float)(DateTime.Now - startTime).TotalSeconds;
            deltaTime = curentTime - lastTimeFrame;
            lastTimeFrame = curentTime;

            //movimiento de sprite en diagonal
            //pasar todo esto a un metodo adentro de enemy
            //float deltaX = posX - prueba.posX ;
            //float deltaY = posY - prueba.posY;

            //float distance = (float)Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));

            //float directionX = deltaX / distance;
            //float directionY = deltaY / distance;

            //prueba.posX += directionX * prueba.Speed;
            //prueba.posY += directionY * prueba.Speed;

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].MoveToPlayer(player.PosX, player.PosY);
            }


            if (bulletIsActive)
            {
                bulletPositionY -= bulletSpeed;
                if(bulletPositionY < 0)
                    bulletIsActive = false;
                
            }





        }

        static void Render()
        {
            Engine.Clear();
            Engine.Draw(image, 0, 0);
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].isActive)
                {
                    Engine.Draw(enemies[i].sprite, enemies[i].posX, enemies[i].posY);
                }
            }


            //Engine.Draw(prueba.sprite, prueba.posX, prueba.posY);
            Engine.Draw(player.Sprite, player.PosX, player.PosY);
            if (bulletIsActive)
                Engine.Draw(bullet, bulletPositionX, bulletPositionY);
            Engine.DrawText("Tiempo: ", 0, 0, 255, 255, 255, font);
            Engine.Show();
        }

        static void Debug(int posX, int posY)
        {
            //Engine.DrawText("X= " + posX, 0, 600, 255, 0, 0, debugFont);
        }


    }
}