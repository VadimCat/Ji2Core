using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

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
        
        public async UniTask Enter<TState>() where TState : IState
        {
            await ExitCurrent();
            var state = GetState<TState>();
            await state.Enter();
            currentState = state;
            StateEntered?.Invoke(currentState);
        }

        public async UniTask Enter<TState, TPayload>(TPayload payload) where TState : IPayloadedState<TPayload>
        {
            await ExitCurrent();
            var state = GetState<TState>();
            await state.Enter(payload);
            currentState = state;

            StateEntered?.Invoke(currentState);
        }

        private async Task ExitCurrent()
        {
            if (currentState != null)
            {
                await currentState.Exit();
            }
        }

        private TState GetState<TState>() where TState : IExitableState
        {
            return (TState)_states[typeof(TState)];
        }
    }
}