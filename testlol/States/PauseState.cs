using System;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;
using testlol.World;

namespace testlol.States
{
     
    class PauseState : GameState
    {
        Platform p = new Platform(new Texture("image.png"), new Vector2f(300,300) );
        public PauseState(StateMachine machine, RenderWindow window, bool replace = true)
            : base(machine, window, replace)
        {
            Console.WriteLine("pause test");
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
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                Machine.LastState();
                //Next = StateMachine.BuildState<PlayState>(Machine, Window, false);
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
