using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using NetEXT.TimeFunctions;
using NetEXT.Resources;

namespace testlol
{
    class Game
    {
        readonly Time TimePerFrame = Time.FromSeconds(1.0 / 60.0);
        private static Vector2u size = new Vector2u(1366,768);
        public static Vector2u Size { get { return size; } }

        RenderWindow window = new RenderWindow(new VideoMode(size.X, size.Y), "testlol",Styles.Default); 
        MultiResourceCache<string> cache = CacheFactory.CreateMultiResourceCache<string>();
        Background bg;
        Player p;
        Platform pl;


        public Game()
        {
            p = new Player(new Texture("image.png"));
            bg = new Background(new Texture("background0.png"));
            pl = new Platform(new Texture("test1.png"), new Vector2f(700, 700));
            
            window.Closed += (sender, e) => { ((Window)sender).Close(); };
            window.KeyPressed += window_KeyPressed;
            window.KeyReleased += window_KeyReleased;
            
            
        }

        void window_KeyReleased(object sender, KeyEventArgs e)
        {
            p.Direction = 0;
        }

        void window_KeyPressed(object sender, KeyEventArgs e)
        {
            switch(e.Code)
            {
                case Keyboard.Key.W:
                    if (!p.Jumping)
                    {
                        p.Jumping = true;
                        p.Force += new Vector2f(0, -30000);
                    }
                    break;
                case Keyboard.Key.A:
                    p.Direction = -1;
                    p.Force += new Vector2f(-2000, 0);
                    break;
                case Keyboard.Key.S:
                    p.Force += new Vector2f(0, 2000);
                    break;
                case Keyboard.Key.D:
                    p.Direction = 1;
                    p.Force += new Vector2f(2000, 0);
                    break;
            }
        }

        public void Run()
        {
            Clock timer = new Clock();
            Time timeSinceLastUpdate = Time.Zero;
            while (window.IsOpen)
            {
                ProcessEvents();
                Time dt = timer.ElapsedTime;
                timer.Restart();
                timeSinceLastUpdate += dt;
                while (timeSinceLastUpdate > TimePerFrame)
                {
                    timeSinceLastUpdate -= TimePerFrame;
                    ProcessEvents();
                    Update(TimePerFrame);
                }
                Render();
            }
        }

        private void ProcessEvents()
        {
            window.DispatchEvents();
            if(Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                window.Close();
            }

        }

        private void Update(Time dt)
        {
            if(p.Position.Y <= Size.Y-p.Size.Y)
                p.Force += new Vector2f(0, 1000);
            p.Update(dt);
        }

        private void Render()
        {
            window.Clear();
            //Console.WriteLine("Velocity:{0}|Force{1}", p.Velocity, p.Force);
            window.Draw(bg);
            window.Draw(pl);
            window.Draw(p);
        	window.Display();
        }

    }
}
