﻿using System;
using SFML.Graphics;
using SFML.System;

namespace testlol.States
{
    public class IntroState : GameState
    {
        Text t = new Text("intro State", Game.Font);

        private Time _time = Time.Zero;

        public IntroState(StateMachine machine, RenderWindow window, bool replace = true):base(machine,window,replace)
        {
            Console.WriteLine("intro created");

        }

        public override void Pause()
        {

        }

        public override void Resume()
        {

        }

        public override void Update(Time dt)
        {
            if ((_time += dt) >= Time.FromSeconds(0.5f))
            {
                Next = StateMachine.BuildState<MenuState>(Machine, Window, true);
            }
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
