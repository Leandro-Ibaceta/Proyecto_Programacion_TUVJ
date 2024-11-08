using System;
using System.Media;
using System.Security.Permissions;
using Tao.Sdl;

namespace MyGame
{
    
    class Program
    {
        
        static Image image = Engine.LoadImage("assets/fondo.png");

        static Player player = new Player();
        static int speed = 2;
        static int posX;
        static int posY;
        
        
        
        
        
        static void Main(string[] args)
        {
            Engine.Initialize();
            
            
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
            if (Engine.KeyPress(Engine.KEY_LEFT))
            {
               //meter en un metodo
                posX -= 2;
            }

            if (Engine.KeyPress(Engine.KEY_RIGHT))
            {
                posX += 2;
            }

            if (Engine.KeyPress(Engine.KEY_LEFT))
            {

            }

            if (Engine.KeyPress(Engine.KEY_LEFT))
            {

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
        
        }

        static void Render()
        {
            Engine.Clear();
            Engine.Draw(image, 0, 0);
            Engine.Draw(player.LoadSprite(), 300 + posX, 300);
            Engine.Show();
        }

        static void Debug(int posX, int posY)
        {
            //Engine.DrawText("X= " + posX, 0, 600, 255, 0, 0, debugFont);
        }

        
    }
}