using System;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;
using testlol.World;

namespace testlol.States
{
    class PlayState : GameState
    {
        Platform p = new Platform(new Texture("test1.png"),new Vector2f(200,200) );
        public PlayState(StateMachine machine, RenderWindow window, bool replace = true) : base(machine, window, replace)
        {
            Console.WriteLine("play test");
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
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                Next = StateMachine.BuildState<PauseState>(Machine, Window, false);
            }
        }

        public override void Draw()
        {
            Window.Clear();
            Window.Draw(p);
            Window.Display();
        }

    }
}
