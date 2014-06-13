using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;

namespace testlol.States
{
    public abstract class GameState
    {
        protected GameState(StateMachine machine, RenderWindow window, bool replace = true)
        {
            Machine = machine;
            Window = window;
            Replacing = replace;
        }

        public abstract void Pause();
        public abstract void Resume();
        public virtual void Update(Time dt)
        {
            Window.DispatchEvents();
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                Window.Close();
            }
        }

        public abstract void Draw();
        public GameState Next { get; protected set; }
        public bool Replacing { get; set; }

        protected StateMachine Machine;
        protected RenderWindow Window;

    }
}
