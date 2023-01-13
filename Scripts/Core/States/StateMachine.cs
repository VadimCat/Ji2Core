using System;
using System.Collections.Generic;

namespace Ji2Core.Core.States
{
    public class StateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states = new();
        private IExitableState currentState;

        public StateMachine(IStateFactory stateFactory)
        {
            _states = stateFactory.GetStates(this);
        }

        public void Enter<TState>() where TState : IState
        {
            currentState?.Exit();
            var state = GetState<TState>();
            currentState = state;
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : IPayloadedState<TPayload>
        {
            currentState?.Exit();
            var state = GetState<TState>();
            currentState = state;
            state.Enter(payload);
        }

        private TState GetState<TState>() where TState : IExitableState
        {
            return (TState)_states[typeof(TState)];
        }
    }
}