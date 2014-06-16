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
       
        //private readonly MultiResourceCache<string> _cache = CacheFactory.CreateMultiResourceCache<string>();
        //public MultiResourceCache<string> Cache { get { return _cache; } }
        
        private readonly StateMachine _machine = new StateMachine();

        private static readonly Font font = new Font("C:\\Windows\\Fonts\\arial.ttf");
        public static Font Font { get { return font; } }


        public Game()
        {
            _window.Closed += (sender, e) => ((Window)sender).Close();
            //_window.KeyPressed += window_KeyPressed;
            //_window.KeyReleased += window_KeyReleased;
            
            _machine.Run(StateMachine.BuildState<IntroState>(_machine, _window, true));
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
                    _machine.ProcessEvents();
                    _machine.Update(_timePerFrame);
                    
                }
                _machine.Draw();
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
            
        }

        private void Render()
        {
            _window.Clear();
            
        	_window.Display();
        }

    }
}
