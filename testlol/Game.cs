using SFML.Graphics;
using SFML.Window;
using System;
using NetEXT.TimeFunctions;
using NetEXT.Resources;
using testlol.World;
using testlol.States;

namespace testlol
{
    class Game
    {
        private readonly Time _timePerFrame = Time.FromSeconds(1.0 / 60.0);
        private static Vector2u _size = new Vector2u(1366,768);
        public static Vector2u Size { get { return _size; } }
        private readonly RenderWindow _window = new RenderWindow(new VideoMode(_size.X, _size.Y), "testlol",Styles.Default); 
        private readonly MultiResourceCache<string> _cache = CacheFactory.CreateMultiResourceCache<string>();
        public MultiResourceCache<string> Cache { get { return _cache; } }
        private Background bg;
        private Player p;
        private Platform pl;
        private StateMachine _machine = new StateMachine();


        public Game()
        {
            p = new Player(new Texture("image.png"));
            bg = new Background(new Texture("background0.png"));
            pl = new Platform(new Texture("test1.png"), new Vector2f(700, 700));
            
            _window.Closed += (sender, e) => ((Window)sender).Close();
            _window.KeyPressed += window_KeyPressed;
            _window.KeyReleased += window_KeyReleased;
            
            _machine.Run(StateMachine.BuildState<IntroState>(_machine, _window, true));
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
            //Clock timer = new Clock();
            //Time timeSinceLastUpdate = Time.Zero;
            //while (_window.IsOpen)
            //{
            //    ProcessEvents();
            //    Time dt = timer.ElapsedTime;
            //    timer.Restart();
            //    timeSinceLastUpdate += dt;
            //    while (timeSinceLastUpdate > _timePerFrame)
            //    {
            //        timeSinceLastUpdate -= _timePerFrame;
            //        ProcessEvents();
            //        Update(_timePerFrame);
            //    }
            //    Render();
            //}


            Clock timer = new Clock();
            Time timeSinceLastUpdate = Time.Zero;

            while (_machine.Running)
            {
                _machine.NextState();
                Time dt = timer.ElapsedTime;
                timer.Restart();
                timeSinceLastUpdate += dt;
                while (timeSinceLastUpdate > _timePerFrame)
                {
                    timeSinceLastUpdate -= _timePerFrame;
                    _machine.Update(_timePerFrame);
                    _machine.Draw();
                }
                
            }
        }

        private void ProcessEvents()
        {
            _window.DispatchEvents();
            if(Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                _window.Close();
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
            _window.Clear();
            //Console.WriteLine("Velocity:{0}|Force{1}", p.Velocity, p.Force);
            _window.Draw(bg);
            _window.Draw(pl);
            _window.Draw(p);
        	_window.Display();
        }

    }
}
