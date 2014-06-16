using System;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;

namespace testlol.States
{
    class MenuState : GameState
    {
        Text t = new Text("Menu State", Game.Font);
        public MenuState(StateMachine machine, RenderWindow window, bool replace = true) : base(machine, window, replace)
        {
            Console.WriteLine("menu created");
            KeyDownEventHandler = WindowOnKeyPressed;
            Window.KeyPressed += KeyDownEventHandler;
            
        }

        private void WindowOnKeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.W:
                    Console.WriteLine("menu W");
                    break;
                case Keyboard.Key.Space:
                    Next = StateMachine.BuildState<PlayState>(Machine, Window, true);
                    break;
            }
        }

        public override void Pause()
        {
            Window.KeyPressed -= KeyDownEventHandler;
        }

        public override void Resume()
        {
            Window.KeyPressed += KeyDownEventHandler;
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
