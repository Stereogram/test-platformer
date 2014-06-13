using System;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;

namespace testlol.States
{
    class MenuState : GameState
    {
        public MenuState(StateMachine machine, RenderWindow window, bool replace = true) : base(machine, window, replace)
        {
            Console.WriteLine("menu test");
        }

        public override void Pause()
        {
            
        }

        public override void Resume()
        {
            
        }

        public override void Update(Time dt)
        {
            base.Update(dt);
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                Next = StateMachine.BuildState<PlayState>(Machine, Window, true);
            }
        }

        public override void Draw()
        {
            Window.Clear();
            //Window.Draw();
            Window.Display();
        }

    }
}
