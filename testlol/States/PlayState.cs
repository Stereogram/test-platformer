﻿using System;
using NetEXT.Input;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;
using testlol.World;
using Action = NetEXT.Input.Action;

namespace testlol.States
{
    class PlayState : GameState
    {
        Text t = new Text("Play State", Game.Font);
        private readonly Background _bg = new Background(new Texture("background0.png"));
        private readonly Player _player;
        private readonly Platform _pl = new Platform(new Texture("test1.png"), new Vector2f(700, 700));
        private readonly RectangleShape _testShape = new RectangleShape((Vector2f)Game.Size);

        private readonly TileMap _testTileMap;


        public PlayState(StateMachine machine, RenderWindow window, bool replace = true)
            : base(machine, window, replace)
        {
            Console.WriteLine("play created");
            _player = new Player(new Texture("megaman.png"), AnimatedSprite.ReadAnimations("ani.txt"));
            _testShape.FillColor = new Color(50, 50, 50, 200);
            _player.Position = new Vector2f(500,500);
            Map t = new Map(_bigmap);
            _testTileMap = new TileMap(new Texture("tileset.png"), 32, t);

            EventMap[Actions.Pause] = new Action(Keyboard.Key.Tab, ActionType.PressOnce);
            EventMap[Actions.Quit] = new Action(Keyboard.Key.Escape, ActionType.ReleaseOnce) | new Action(EventType.Closed);
            EventMap[Actions.Left] = new Action(Keyboard.Key.A, ActionType.PressOnce);
            EventMap[Actions.Right] = new Action(Keyboard.Key.D, ActionType.PressOnce);
            EventMap[Actions.Jump] = new Action(Keyboard.Key.Space, ActionType.PressOnce);
            EventMap[Actions.Stop] = new Action(Keyboard.Key.A, ActionType.ReleaseOnce) | new Action(Keyboard.Key.D, ActionType.ReleaseOnce);

            EventSystem.Connect(Actions.Pause, c =>
            {
                if (!Paused) Pause();
                else Resume();
            });
            EventSystem.Connect(Actions.Quit, c => Machine.Running = false);
            EventSystem.Connect(Actions.Left, c =>
            {
                _player.Direction = -1;
                _player.Play("test", true);
                _player.Force += new Vector2f(-2000, 0);
            });
            EventSystem.Connect(Actions.Right, c =>
            {
                _player.Direction = 1;
                _player.Force += new Vector2f(2000, 0);
            });
            EventSystem.Connect(Actions.Jump, c =>
            {
                if (!_player.Jumping)
                {
                    _player.Play("lol", false);
                    _player.Jumping = true;
                    _player.Force += new Vector2f(0, -30000);
                }
            });
            EventSystem.Connect(Actions.Stop, c =>
            {
                _player.Play("herp", true);
                _player.Direction = 0;
            });
        }

        public override void Pause()
        {
            Paused = true;
            _testShape.Position = _player.Position - (Vector2f)(Game.Size/2);
        }

        public override void Resume()
        {
            Paused = false;
            Next = null;
        }

        public override void Update(Time dt)
        {
            if (Paused) return;
            if (_player.Position.Y <= 768 - _player.Size.Y)
                _player.Force += new Vector2f(0, 1000);
            else
                _player.Jumping = false;
            _player.Update(dt);
            View v = Window.GetView();
            v.Center = _player.Position;
            Window.SetView(v);

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
            Window.Draw(_testTileMap);
            Window.Draw(_pl);
            Window.Draw(_player);
            Window.Draw(t);
            if(Paused)
                Window.Draw(_testShape);
            Window.Display();
        }

        private readonly int[,] _bigmap =
        {
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            },
            {
                1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3,
                4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7, 1, 2, 3, 4, 5, 6, 6, 5, 5, 6, 7,
            }

        };


    }
}
