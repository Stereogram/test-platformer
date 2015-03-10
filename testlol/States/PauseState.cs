using System;
using SFML.Graphics;
using SFML.System;

namespace testlol.States
{
     
    class PauseState : GameState
    {
        Text t = new Text("Pause State", Game.Font);
        public PauseState(StateMachine machine, RenderWindow window, bool replace = true) : base(machine, window, replace)
        {
            Console.WriteLine("pause created");

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
            Window.Clear(Color.Transparent);
            Window.Draw(t);
            Window.Display();
        }
        
    }
}
