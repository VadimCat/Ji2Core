using System;
using System.Collections.Generic;

namespace Ji2Core.Core.States
{
    public class StateMachine
    {
        private readonly Dictionary<Type, IState> _states = new();
        private IState currentState;

        public StateMachine(IStateFactory stateFactory)
        {
            _states = stateFactory.GetStates(this);
        }

        public void Enter<TState>() where TState : IState
        {
            currentState?.Exit();
            currentState = _states[typeof(TState)];
            currentState.Enter();
        }
    }
}