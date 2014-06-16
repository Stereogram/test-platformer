using System;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;
using testlol.World;

namespace testlol.States
{
    class PlayState : GameState
    {
        Text t = new Text("Play State", Game.Font);
        private readonly Background _bg = new Background(new Texture("background0.png"));
        private readonly Player _player = new Player(new Texture("image.png"));
        private readonly Platform _pl = new Platform(new Texture("test1.png"), new Vector2f(700, 700));
        private readonly RectangleShape _testShape = new RectangleShape((Vector2f)Game.Size);
        

        public PlayState(StateMachine machine, RenderWindow window, bool replace = true) : base(machine, window, replace)
        {
            Console.WriteLine("play created");
            KeyDownEventHandler = WindowOnKeyPressed;
            KeyUpEventHandler = WindowOnKeyReleased;
            Window.KeyPressed += KeyDownEventHandler;
            Window.KeyReleased += KeyUpEventHandler;

            _testShape.FillColor = new Color(50,50,50,200);

        }

        private void WindowOnKeyReleased(object sender, KeyEventArgs e)
        {
            _player.Direction = 0;
        }

        private void WindowOnKeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.E:
                    Console.WriteLine("play E");
                    break;
                case Keyboard.Key.Escape:
                    Next = StateMachine.BuildState<PauseState>(Machine, Window, false);
                    break;
                case Keyboard.Key.W:
                    if (!_player.Jumping)
                    {
                        _player.Jumping = true;
                        _player.Force += new Vector2f(0, -30000);
                    }
                    break;
                case Keyboard.Key.A:
                    _player.Direction = -1;
                    _player.Force += new Vector2f(-2000, 0);
                    break;
                case Keyboard.Key.S:
                    _player.Force += new Vector2f(0, 2000);
                    break;
                case Keyboard.Key.D:
                    _player.Direction = 1;
                    _player.Force += new Vector2f(2000, 0);
                    break;
                case Keyboard.Key.Tab:
                    Paused = !Paused;
                    break;
            }
        }

        public override void Pause()
        {
            Paused = true;
            Window.KeyPressed -= KeyDownEventHandler;
        }

        public override void Resume()
        {
            Window.KeyPressed += KeyDownEventHandler;
            Paused = false;
            Next = null;
        }

        public override void Update(Time dt)
        {
            if (Paused) return;
            if (_player.Position.Y <= Game.Size.Y - _player.Size.Y)
                _player.Force += new Vector2f(0, 1000);
            _player.Update(dt);
        }

        public override void ProcessEvents()
        {
            base.ProcessEvents();
            if(Paused)
                _player.Force = new Vector2f(0,0);
        }

        public override void Draw()
        {
            Window.Clear();
            Window.Draw(_bg);
            Window.Draw(_pl);
            Window.Draw(_player);
            Window.Draw(t);
            if(Paused)
                Window.Draw(_testShape);
            Window.Display();
        }

    }
}
