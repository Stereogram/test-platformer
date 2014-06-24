using System;
using System.IO;
using NetEXT.Input;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;
using testlol.Util;
using testlol.World;
using testlol.World.Level;
using Action = NetEXT.Input.Action;

namespace testlol.States
{
    public class PlayState : GameState
    {
        Text t = new Text("Play State", Game.Font);
        private Text _posText = new Text("", Game.Font);
        private readonly Background _bg = new Background(new Texture(@"assets/maps/bg1.png"));
        private readonly Player _player;
        
        private readonly RectangleShape _testShape = new RectangleShape((Vector2f)Game.Size);

        private readonly TileMap _testTileMap;


        public PlayState(StateMachine machine, RenderWindow window, bool replace = true)
            : base(machine, window, replace)
        {
            _posText.Position = new Vector2f(300,0);
            Console.WriteLine("play created");
            _player = new Player(new Texture(@"assets/player/megaman.png"), AnimatedSprite.ReadAnimations(@"assets/player/megaman.txt"));
            _testShape.FillColor = new Color(50, 50, 50, 200);

            _testTileMap = new TileMap(new Texture(@"assets/maps/tileset1.png"), 32, Map.LoadMap(@"assets/maps/1.txt"));
            
            EventMap[Actions.Pause] = new Action(Keyboard.Key.Tab, ActionType.PressOnce);
            EventMap[Actions.Quit] = new Action(Keyboard.Key.Escape, ActionType.ReleaseOnce) | new Action(EventType.Closed);
            EventMap[Actions.Left] = new Action(Keyboard.Key.A, ActionType.Hold);
            EventMap[Actions.Right] = new Action(Keyboard.Key.D, ActionType.Hold);
            EventMap[Actions.Jump] = new Action(Keyboard.Key.Space, ActionType.Hold);
            EventMap[Actions.Shoot] = new Action(Keyboard.Key.LShift, ActionType.Hold);
            EventMap[Actions.Stop] = new Action(Keyboard.Key.A, ActionType.ReleaseOnce) | new Action(Keyboard.Key.D, ActionType.ReleaseOnce);
            
            

            EventSystem.Connect(Actions.Pause, c =>
            {
                if (!Paused) Pause();
                else Resume();
            });
            EventSystem.Connect(Actions.Quit, c => Machine.Running = false);
            EventSystem.Connect(Actions.Left, c => _player.Move(-1));
            EventSystem.Connect(Actions.Right, c => _player.Move(1));
            EventSystem.Connect(Actions.Jump, c => _player.Jump());
            EventSystem.Connect(Actions.Stop, c => _player.Move(0));
            EventSystem.Connect(Actions.Shoot, c => _testTileMap[(int)(_player.Position.X/32),(int)(_player.Position.Y/32)] = 1);
        }

        public override void Pause()
        {
            Paused = true;
            _testShape.Position = Window.DefaultView.Center - (Vector2f)(Game.Size/2);
            
        }

        public override void Resume()
        {
            Paused = false;
            Next = null;
        }

        public override void Update(Time dt)
        {
            if (Paused) return;

            _player.Update(dt);
            View v = Window.GetView();
            if (_player.Position.X > (Game.Size.X / 2.0))
                v.Center = _player.Position;
            Window.SetView(v);

            _posText.DisplayedString = _player.Velocity.ToString();
            _posText.Position = new Vector2f(300,_player.Position.Y-(Game.Size.Y/2));

        }

        public override void ProcessEvents()
        {
            base.ProcessEvents();
            if(Paused)
                _player.Velocity = new Vector2f(0,0);
        }

        public override void Draw()
        {
            Window.Clear();
            Window.Draw(_bg);
            Window.Draw(_testTileMap);
            Window.Draw(_player);
            Window.Draw(t);
            Window.Draw(_posText);
            if(Paused)
                Window.Draw(_testShape);
            Window.Display();
        }

    }
}
