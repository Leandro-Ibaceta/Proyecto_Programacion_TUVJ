using System;
using System.Media;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Tao.Sdl;
using static System.Net.Mime.MediaTypeNames;

namespace MyGame
{

    class Program
    {
        //fondo
        static Image background = Engine.LoadImage("assets/Background.png");
        static Image introGame = Engine.LoadImage("assets/IntroJuego.png");
        static Image finalscreen = Engine.LoadImage("assets/PantallaFinal.png");
        //selector de pantalla
        static int screenSelector = 0;

        //fuente
        static Font font;
        static string playerTest = "";


        static Player player = new Player(300f, 300f);
        static Enemy[] enemies = new Enemy[10];
        static Bullet[] bullets = new Bullet[10];
        static float speed = 150;
        static float enemyPosx = 10;
        static float enemyPosy = 10;
        static float enemySpeed = 5;
        static int points = 0;

        static int Timer;
        static DateTime startTime = DateTime.Now;
        static float deltaTime;
        static float lastTimeFrame;
        static float respawnTime = 2;
        static TimeSpan interval = TimeSpan.FromSeconds(1);

        static Random random = new Random();
        static DateTime timeLastShoot = DateTime.Now;
        static float timeLastSpawn = 0;

        static float fireRate = 0.7f;

        static string tecla = "";

        static void Main(string[] args)
        {
            Engine.Initialize();
            font = Engine.LoadFont("assets/Font/RobotoCondensed-Bold.ttf", 40);
            Timer = 30;

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new Enemy();
                enemies[i].Speed = 250;
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
                if(screenSelector == 0 || screenSelector == 3)
                    screenSelector = 1;
            }
        }
        //move 

        static void Update()
        {
            float curentTime = (float)(DateTime.Now - startTime).TotalSeconds;
            deltaTime = curentTime - lastTimeFrame;
            lastTimeFrame = curentTime;
            if (screenSelector == 1)
            {

                StartTimer(curentTime);

                timeLastSpawn += deltaTime;
                //checkea colision de bala con enemigos
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
                                points += 5;
                                break;
                            }
                        }
                    }
                }
                //cheackea colision de enemigos con player
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].isActive)
                    {
                        if (checkPlayerCollision(player, enemies[i]))
                        {
                            screenSelector = 3;
                            break;
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
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].isActive)
                        enemies[i].MoveToPlayer(player.PosX, player.PosY, deltaTime);

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
            
        }

        static void Render()
        {
            Engine.Clear();
            if (screenSelector == 0)
                Engine.Draw(introGame, 0, 0);
            else
            {
                Engine.Draw(background, 0, 0);
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].isActive)
                    {
                        Engine.Draw(enemies[i].sprite, enemies[i].PosX, enemies[i].PosY);
                    }
                }



                Engine.Draw(player.Sprite, player.PosX, player.PosY);
                for (int i = 0; i < bullets.Length; i++)
                {
                    if (bullets[i].isActive)
                        Engine.Draw(bullets[i].Sprite, bullets[i].PosX, bullets[i].PosY);

                }

                Engine.DrawText("Tiempo: " + Timer, 0, 0, 255, 255, 255, font);

            }
            if (screenSelector == 3)
            {
                Engine.Draw(finalscreen, 0, 0);
                Engine.DrawText(points.ToString() , Engine.Ancho /2, (Engine.Alto /2) + 100 , 255, 255, 255, font);

            }


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
        static void StartTimer(float currentTime)
        {
            Timer = (int)currentTime;

            if (Timer == 30)
            {
                screenSelector = 3;
            }

        }

    }

}

