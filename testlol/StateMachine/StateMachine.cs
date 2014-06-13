using System;
using System.Collections.Generic;


namespace testlol.StateMachine
{
    class StateMachine
    {
        public bool Running { get; set; }
        public bool Resume { get; set; }
        private Stack<GameState> states = new Stack<GameState>();

        public StateMachine()
        {
            Running = false;
            Resume = false;
        }

        public void Run(GameState state)
        {

        }

        public void NextState()
        {

        }

        public void LastState()
        {

        }
    }
}
