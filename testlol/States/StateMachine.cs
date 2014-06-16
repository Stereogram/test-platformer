using System;
using System.Collections.Generic;
using NetEXT.TimeFunctions;
using SFML.Graphics;


namespace testlol.States
{
    public class StateMachine
    {
        public bool Running { get; set; }
        public bool Resume { get; set; }
        private readonly Stack<GameState> _states = new Stack<GameState>();

        public StateMachine()
        {
            Running = false;
            Resume = false;
        }

        public static GameState BuildState<T>(StateMachine machine, RenderWindow window, bool replace) where T : GameState
        {
            return Activator.CreateInstance(typeof(T), new object[] { machine, window, replace }) as T;
        }

        public void Run(GameState state)
        {
            Running = true;
            _states.Push(state);
        }

        public void NextState()
        {
            if (Resume)
            {
                // Cleanup the current state
                if (_states.Count != 0)
                {
                    _states.Pop().Switch();
                }

                // Resume previous state
                if (_states.Count != 0)
                {
                    _states.Peek().Resume();
                }

                Resume = false;
            }

            // There needs to be a state
            if (_states.Count != 0)
            {
               GameState temp = _states.Peek().Next;

                // Only change states if there's a next one existing
                if (temp != null)
                {
                    // Replace the running state
                    if (temp.Replacing)
                    {
                        _states.Pop().Switch();
                    }
                    else // Pause the running state
                    {
                        _states.Peek().Pause();
                    }

                    _states.Push(temp);
                }
            }
        }

        public void LastState()
        {
            Resume = true;
        }

        public void Update(Time dt)
        {
            _states.Peek().Update(dt);
        }

        public void Draw()
        {
            _states.Peek().Draw();
        }

        public void ProcessEvents()
        {
            _states.Peek().ProcessEvents();
        }

    }
}
