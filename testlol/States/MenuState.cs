﻿using System;
using NetEXT.Input;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using testlol.Util;
using Action = NetEXT.Input.Action;

namespace testlol.States
{
    class MenuState : GameState
    {

        Text t = new Text("Menu State", Game.Font);

        public MenuState(StateMachine machine, RenderWindow window, bool replace = true) : base(machine, window, replace)
        {
            Console.WriteLine("menu created");

            EventMap[Actions.Enter] = new Action(Keyboard.Key.Return, ActionType.PressOnce);
            EventMap[Actions.Quit] = new Action(Keyboard.Key.Escape, ActionType.ReleaseOnce) | new Action(EventType.Closed);
            
            EventSystem.Connect(Actions.Enter, c => Next = StateMachine.BuildState<PlayState>(Machine, Window, true));
            EventSystem.Connect(Actions.Quit, c => Machine.Running = false);
            StuffLoader tLoader = new StuffLoader();
        }

        public override void Pause()
        {
            
        }

        public override void Resume()
        {
            
        }

        public override void Update(Time dt)
        {

        }

        public override void ProcessEvents()
        {
            base.ProcessEvents();
        }

        public override void Draw()
        {
            Window.Clear();
            Window.Draw(t);
            Window.Display();
        }

    }
}
