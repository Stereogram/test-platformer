using SFML.Graphics;
using SFML.Window;
using NetEXT.TimeFunctions;
using testlol.States;

namespace testlol
{
    /// <summary>
    /// Main game class initializes window and state machine. Ccontains the main game loop.
    /// </summary>
    class Game
    {
        private readonly Time _timePerFrame = Time.FromSeconds(1.0 / 60.0);
        private static readonly Vector2u _size = new Vector2u(720,480);
        public static Vector2u Size { get { return _size; } }
        private readonly RenderWindow _window = new RenderWindow(new VideoMode(_size.X, _size.Y), "testlol",Styles.None); 
        private readonly StateMachine _machine = new StateMachine();

        private static readonly Font _font = new Font("C:\\Windows\\Fonts\\arial.ttf");
        public static Font Font { get { return _font; } }


        public Game()
        {
            _window.Closed += (sender, e) => ((Window)sender).Close();
            _machine.Run(StateMachine.BuildState<IntroState>(_machine, _window, true));
        }
        /// <summary>
        /// main game loop
        /// </summary>
        public void Run()
        {
            Clock timer = new Clock();
            Time timeSinceLastUpdate = Time.Zero;

            while (_machine.Running)
            {
                _machine.NextState();
                _machine.ProcessEvents();
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

    }
}
