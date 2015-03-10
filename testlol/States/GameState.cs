using SFML.Graphics;
using NetEXT.Input;
using SFML.System;

namespace testlol.States
{
    public abstract class GameState
    {
        public GameState Next { get; protected set; }
        public bool Replacing { get; set; }
        protected bool Paused { get; set; }
        protected StateMachine Machine;
        protected RenderWindow Window;

        protected enum Actions
        {
            Up,
            Down,
            Enter,
            Quit,
            Left,
            Right,
            Shoot,
            Jump,
            Pause,
            Stop
        };
        protected readonly ActionMap<Actions> EventMap = new ActionMap<Actions>();
        protected readonly EventSystem<Actions, ActionContext<Actions>> EventSystem = ActionMap<Actions>.CreateCallbackSystem();

        
        protected GameState(StateMachine machine, RenderWindow window, bool replace = true)
        {
            Machine = machine;
            Window = window;
            Replacing = replace;
            Paused = false;
            
        }

        public void Switch()
        {
            EventSystem.ClearAllConnections();
        }

        public abstract void Pause();
        public abstract void Resume();
        public abstract void Update(Time dt);
        public abstract void Draw();
        public virtual void ProcessEvents()
        {
            EventMap.Update(Window);
            EventMap.InvokeCallbacks(EventSystem, Window);
        }
    }
}
