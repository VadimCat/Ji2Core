using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ji2Core.Core.States
{
    public class StateMachine
    {
        private readonly IStateFactory _stateFactory;
        private readonly bool _enableLogging;
        private Dictionary<Type, IExitableState> _states = new();
        public IExitableState CurrentState { get; private set; }

        public event Action<IExitableState> StateEntered;
        
        public StateMachine(IStateFactory stateFactory, bool enableLogging = false)
        {
            _stateFactory = stateFactory;
            _enableLogging = enableLogging;
        }

        public void Load()
        {
            _states = _stateFactory.GetStates(this);
        }
        
        public async UniTask Enter<TState>() where TState : IState
        {
            if (_enableLogging)
            {
                Debug.Log($"Enter {typeof(TState)}");
            }
            var exitCurrent = ExitCurrent();
            var state = GetState<TState>();

            CurrentState = state;

            await exitCurrent;
            await state.Enter();
            StateEntered?.Invoke(CurrentState);
        }

        public async UniTask Enter<TState, TPayload>(TPayload payload) where TState : IPayloadedState<TPayload>
        {
            if (_enableLogging)
            {
                Debug.Log($"Enter {typeof(TState)}");
            }

            var exitCurrent = ExitCurrent();
            var state = GetState<TState>();
            
            CurrentState = state;
            
            await exitCurrent;
            await state.Enter(payload);

            StateEntered?.Invoke(CurrentState);
        }

        private async Task ExitCurrent()
        {
            if (CurrentState != null)
            {
                await CurrentState.Exit();
            }
        }

        private TState GetState<TState>() where TState : IExitableState
        {
            return (TState)_states[typeof(TState)];
        }
    }
}