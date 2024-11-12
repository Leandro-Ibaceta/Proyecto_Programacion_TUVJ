using System;
using System.Media;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using Tao.Sdl;

namespace MyGame
{

    class Program
    {
        //fondo
        static Image image = Engine.LoadImage("assets/fondo.png");
        //fuente
        static Font font;
        

        static Player player = new Player(300f, 300f);
        static Enemy[] enemies = new Enemy[10];
        static Bullet[] bullets = new Bullet[10];
        static float speed = 200;
        static float enemyPosx = 10;
        static float enemyPosy = 10;
        static float enemySpeed = 5;

        static DateTime startTime = DateTime.Now;
        static float deltaTime;
        static float lastTimeFrame;
        static float respawnTime = 2;

        static Random random = new Random();
        static DateTime timeLastShoot = DateTime.Now;
        static float timeLastSpawn = 0;

        static float fireRate = 0.7f;

        static string tecla = "";

        static void Main(string[] args)
        {
            Engine.Initialize();
            font = Engine.LoadFont("assets/Font/RobotoCondensed-Bold.ttf", 40);


            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new Enemy();
                enemies[i].Speed = 100;
                enemies[i].isActive = false;
                
            }
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = new Bullet(player.PosX, player.PosY);
                bullets[i].isActive = false;
                
                
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
                tecla = "left";
                if ((DateTime.Now - timeLastShoot).TotalSeconds > fireRate)
                {
                    for (int i = 0; i < bullets.Length; i++)
                    {
                        if (bullets[i].isActive == false)
                        {
                            bullets[i].isActive = true;
                            bullets[i].PosX = player.PosX;
                            bullets[i].PosY = player.PosY + player.Height / 2;
                            timeLastShoot = DateTime.Now;
                            break;
                        }
                    }
                }
            }
            if (Engine.KeyPress(Engine.KEY_RIGHT))
            {
                tecla = "right";
                if ((DateTime.Now - timeLastShoot).TotalSeconds > fireRate)
                {
                    for (int i = 0; i < bullets.Length; i++)
                    {
                        if (bullets[i].isActive == false)
                        {
                            bullets[i].isActive = true;
                            bullets[i].PosX = player.PosX + player.Width;
                            bullets[i].PosY = player.PosY + player.Height / 2;
                            timeLastShoot = DateTime.Now;
                            break;

                        }

                    }
                }
            }

            if (Engine.KeyPress(Engine.KEY_UP))
            {
                tecla = "up";
                if ((DateTime.Now - timeLastShoot).TotalSeconds > fireRate)
                {
                    for (int i = 0; i < bullets.Length; i++)
                    {
                        if (bullets[i].isActive == false)
                        {
                            bullets[i].isActive = true;
                            bullets[i].PosX = player.PosX + player.Width / 2;
                            bullets[i].PosY = player.PosY;
                            timeLastShoot = DateTime.Now;
                            break;

                        }

                    }
                }

            }

            if (Engine.KeyPress(Engine.KEY_DOWN))
            {
                tecla = "down";
                if ((DateTime.Now - timeLastShoot).TotalSeconds > fireRate)
                {
                    for (int i = 0; i < bullets.Length; i++)
                    {
                        if (bullets[i].isActive == false)
                        {
                            bullets[i].isActive = true;
                            bullets[i].PosX = player.PosX + player.Width / 2;
                            bullets[i].PosY = player.PosY + player.Height;
                            timeLastShoot = DateTime.Now;
                            break;

                        }

                    }
                }
            }
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
        //move 

        static void Update()
        {
            float curentTime = (float)(DateTime.Now - startTime).TotalSeconds;
            deltaTime = curentTime - lastTimeFrame;
            lastTimeFrame = curentTime;

            timeLastSpawn += deltaTime;

            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i].isActive)
                {
                    for (int j = 0; j < enemies.Length; j++)
                    {
                        if (checkBulletCollision(bullets[i], enemies[j]))
                        {
                            bullets[i].isActive = false;
                            enemies[j].isActive = false;
                            break;
                        }
                    }
                }
            }

            //si esta dentro del tiempo spawnea el enemigo en una posicion aleatoria
            if (shouldRespawn())
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (!enemies[i].isActive)
                    {
                        enemies[i].isActive = true;
                        enemies[i].SpawnEnemies(0, 0);
                        timeLastSpawn = 0;
                        break;

                    }
                }
            }
            //mueve a ese enemigo desde ese spawn
            for(int i = 0; i < enemies.Length; i++)
            {
                if(enemies[i].isActive)
                enemies[i].MoveToPlayer(player.PosX, player.PosY,deltaTime);
                
            }

            


            for (int i = 0; i < bullets.Length; i++)
            {

                if (bullets[i].isActive)
                {
                    switch (tecla)
                    {
                        case "up":
                            bullets[i].PosY -= bullets[i].Speed * deltaTime;
                            if (bullets[i].PosY < 0)
                                bullets[i].isActive = false;
                            break;
                        case "down":
                            bullets[i].PosY += bullets[i].Speed * deltaTime;
                            if (bullets[i].PosY > 780)
                                bullets[i].isActive = false;
                            break;
                        case "left":
                            bullets[i].PosX -= bullets[i].Speed * deltaTime;
                            if (bullets[i].PosX < 0)
                                bullets[i].isActive = false;
                            break;
                        case "right":
                            bullets[i].PosX += bullets[i].Speed * deltaTime;
                            if (bullets[i].PosX > 1030)
                                bullets[i].isActive = false;
                            break;

                    }
                }
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
                    Engine.Draw(enemies[i].sprite, enemies[i].PosX, enemies[i].PosY);
                }
            }


            //Engine.Draw(prueba.sprite, prueba.posX, prueba.posY);
            Engine.Draw(player.Sprite, player.PosX, player.PosY);
            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i].isActive)
                    Engine.Draw(bullets[i].Sprite, bullets[i].PosX, bullets[i].PosY);

            }

            Engine.DrawText("Tiempo: ", 0, 0, 255, 255, 255, font);
            Engine.Show();
        }

        static void Debug(int posX, int posY)
        {
            //Engine.DrawText("X= " + posX, 0, 600, 255, 0, 0, debugFont);
        }
        static bool shouldRespawn()
        {
            return timeLastSpawn >= respawnTime;
        }
        static bool checkBulletCollision(Bullet a, Enemy b)
        {
            bool collission = (a.PosX < b.PosX + b.Width) && (a.PosX + a.Width > b.PosX) && (a.PosY < b.PosY + b.Heigth) && (a.PosY + a.Height > b.PosY);

            return collission;
        }
        static bool checkPlayerCollision(Player a, Enemy b)
        {
            bool collission = (a.PosX < b.PosX + b.Width) && (a.PosX + a.Width > b.PosX) && (a.PosY < b.PosY + b.Heigth) && (a.PosY + a.Height > b.PosY);

            return collission;
        }

    }

}

