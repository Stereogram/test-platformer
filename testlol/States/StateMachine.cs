using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;


namespace testlol.States
{
    /// <summary>
    /// Activates the current state's Run, Update and ProcessEvents
    /// 
    /// From https://github.com/eXpl0it3r/SmallGameEngine
    /// </summary>
    public class StateMachine
    {
        /// <summary>
        /// false to shut down game.
        /// </summary>
        public bool Running { get; set; }
        private bool Resume { get; set; }
        private readonly Stack<GameState> _states = new Stack<GameState>();

        public StateMachine()
        {
            Running = false;
            Resume = false;
        }

        /// <summary>
        /// Creates a "T type" state
        /// </summary>
        /// <typeparam name="T">GameState type</typeparam>
        /// <param name="machine">StateMachine reference</param>
        /// <param name="window">Window reference</param>
        /// <param name="replace">Whether to shutdown current state or pause it.</param>
        /// <returns>new "T State" object</returns>
        public static GameState BuildState<T>(StateMachine machine, RenderWindow window, bool replace) where T : GameState
        {
            return Activator.CreateInstance(typeof(T), new object[] { machine, window, replace }) as T;
        }

        /// <summary>
        /// Initializes StateMachine
        /// </summary>
        /// <param name="state">First state</param>
        public void Run(GameState state)
        {
            Running = true;
            _states.Push(state);
        }

        /// <summary>
        /// Checks if theres a new state to push
        /// </summary>
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

        /// <summary>
        /// Resumes previous state if theres any paused
        /// </summary>
        public void LastState()
        {
            Resume = true;
        }

        /// <summary>
        /// Calls Update on current state.
        /// </summary>
        /// <param name="dt">delta time</param>
        public void Update(Time dt)
        {
            _states.Peek().Update(dt);
        }

        /// <summary>
        /// Calls Draw on current state.
        /// </summary>
        public void Draw()
        {
            _states.Peek().Draw();
        }

        /// <summary>
        /// Calls ProccessEvents on current state.
        /// </summary>
        public void ProcessEvents()
        {
            _states.Peek().ProcessEvents();
        }

    }
}
