using System;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;

namespace testlol.States
{
    public class IntroState : GameState
    {
        public IntroState(StateMachine machine, RenderWindow window, bool replace = true):base(machine,window,replace)
        {
            Console.WriteLine("intro test");
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
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                Next = StateMachine.BuildState<MenuState>(Machine, Window, true);
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
