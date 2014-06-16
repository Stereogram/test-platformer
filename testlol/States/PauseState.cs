using System;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;
using testlol.World;

namespace testlol.States
{
     
    class PauseState : GameState
    {
        Text t = new Text("Pause State", Game.Font);
        public PauseState(StateMachine machine, RenderWindow window, bool replace = true) : base(machine, window, replace)
        {
            Console.WriteLine("pause created");
            KeyDownEventHandler = WindowOnKeyPressed;
            Window.KeyPressed += KeyDownEventHandler;
        }

        private void WindowOnKeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.W:
                    Console.WriteLine("pause W");
                    break;
                case Keyboard.Key.Space:
                    Machine.LastState();
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
            Window.Clear(Color.Transparent);
            Window.Draw(t);
            Window.Display();
        }
        
    }
}
