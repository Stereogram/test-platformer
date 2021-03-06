﻿using System;
using System.Collections.Generic;
using NetEXT.Input;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using testlol.Util;
using testlol.World.Entity;
using testlol.World.Level;
using Action = NetEXT.Input.Action;

namespace testlol.States
{
    public class PlayState : GameState
    {
        Text t = new Text("Play State", Game.Font);
        private readonly Text _posText = new Text("", Game.Font);

        private readonly List<Entity> _entities = new List<Entity>(); 

        private readonly Background _bg = new Background(Game.Textures[@"assets\maps\bg1.png"]);

        private readonly Player _player;
        
        private readonly RectangleShape _testShape = new RectangleShape((Vector2f)Game.Size);

        private readonly TileMap _testTileMap;
        private readonly Collision _testCollision;

        public PlayState(StateMachine machine, RenderWindow window, bool replace = true)
            : base(machine, window, replace)
        {
            _posText.Position = new Vector2f(300,0);
            _posText.CharacterSize = 16;
            Console.WriteLine("play created");
            _player = new Player();
            _entities.Add(_player);
            _testShape.FillColor = new Color(50, 50, 50, 200);
            
            _testTileMap = new TileMap(Game.Textures[@"assets\maps\1.png"], 32, Map.LoadMap(@"assets\maps\1.txt"));
            _testCollision = new Collision(_testTileMap, _entities);

// ReSharper disable RedundantArgumentDefaultValue
            EventMap[Actions.Pause] = new Action(Keyboard.Key.Tab, ActionType.PressOnce);
            EventMap[Actions.Quit] = new Action(Keyboard.Key.Escape, ActionType.ReleaseOnce) | new Action(EventType.Closed);
            EventMap[Actions.Left] = new Action(Keyboard.Key.A, ActionType.Hold);
            EventMap[Actions.Right] = new Action(Keyboard.Key.D, ActionType.Hold);
            EventMap[Actions.Jump] = new Action(Keyboard.Key.Space, ActionType.PressOnce);
            EventMap[Actions.Shoot] = new Action(Keyboard.Key.LShift, ActionType.Hold);
            EventMap[Actions.Stop] = new Action(Keyboard.Key.A, ActionType.ReleaseOnce) | new Action(Keyboard.Key.D, ActionType.ReleaseOnce);
// ReSharper restore RedundantArgumentDefaultValue

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
            EventSystem.Connect(Actions.Shoot, c => _player.Shoot());
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
            _testCollision.Update(dt);
            View v = Window.GetView();

            //scroll clamp
            if (_player.Position.X < (Game.Size.X/2.0))
                v.Center = new Vector2f(Game.Size.X / 2.0f,_player.Position.Y);
            else if (_player.Position.X > (_testTileMap.Size.X - (Game.Size.X/2)))
                v.Center = new Vector2f(_testTileMap.Size.X - (Game.Size.X/2), _player.Position.Y);
            else
                v.Center = _player.Position;
            
            //fixes random fucking 1px glitches in tilemap.
            v.Center = new Vector2f((float) Math.Round(v.Center.X * 100.0f)/100.0f, (float)Math.Round(v.Center.Y * 100.0f)/100.0f);

            Window.SetView(v);

            _posText.DisplayedString = _player.Position.ToString() + v.Center;
            _posText.Position = new Vector2f(_player.Position.X - (Game.Size.X / 2.0f), _player.Position.Y - (Game.Size.Y / 2.0f));

        }

        public override void ProcessEvents()
        {
            base.ProcessEvents();
            if(Keyboard.IsKeyPressed(Keyboard.Key.R))
                _player.Position = new Vector2f(0,0);
        }

        public override void Draw()
        {
            Window.Clear();
            Window.Draw(_bg);
            Window.Draw(_testTileMap);
            Window.Draw(_player);
            Window.Draw(t);
            //Window.Draw(_posText);
            if(Paused)
                Window.Draw(_testShape);
            Window.Display();
        }

    }
}
