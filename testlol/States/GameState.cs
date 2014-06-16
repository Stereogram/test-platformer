using System;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;

namespace testlol.States
{
    public abstract class GameState
    {
        public GameState Next { get; protected set; }
        public bool Replacing { get; set; }
        protected bool Paused { get; set; }
        protected StateMachine Machine;
        protected RenderWindow Window;
        protected EventHandler<KeyEventArgs> KeyDownEventHandler;
        protected EventHandler<KeyEventArgs> KeyUpEventHandler;
        
        protected GameState(StateMachine machine, RenderWindow window, bool replace = true)
        {
            Machine = machine;
            Window = window;
            Replacing = replace;
            Paused = false;
        }

        public void Switch()
        {
            Window.KeyPressed -= KeyDownEventHandler;
            Window.KeyReleased -= KeyUpEventHandler;
        }

        public abstract void Pause();
        public abstract void Resume();
        public abstract void Update(Time dt);
        public abstract void Draw();
        public virtual void ProcessEvents()
        {
            Window.DispatchEvents();
            
        }
    }
}
