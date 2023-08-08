using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ji2Core.Core.States
{
    public class StateMachine
    {
        private readonly IStateFactory stateFactory;
        private Dictionary<Type, IExitableState> _states = new();
        private IExitableState currentState;

        public event Action<IExitableState> StateEntered;
        
        public StateMachine(IStateFactory stateFactory)
        {
            this.stateFactory = stateFactory;
        }

        public void Load()
        {
            _states = stateFactory.GetStates(this);
        }
        
        public void Enter<TState>() where TState : IState
        {
            currentState?.Exit();
            var state = GetState<TState>();
            currentState = state;
            state.Enter();
            StateEntered?.Invoke(currentState);
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : IPayloadedState<TPayload>
        {
            currentState?.Exit();
            var state = GetState<TState>();
            currentState = state;
            state.Enter(payload);
            
            StateEntered?.Invoke(currentState);
        }

        private TState GetState<TState>() where TState : IExitableState
        {
            return (TState)_states[typeof(TState)];
        }
    }
}