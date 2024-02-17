using System;
using Cysharp.Threading.Tasks;

namespace Ji2.States
{
    public interface IStateMachine
    {
        public IExitableState CurrentState { get; }

        public event Action<IExitableState> StateEntered;

        public UniTask ExitCurrent();
        public UniTask Enter<TState, TPayload>(TPayload payload) where TState : IPayloadedState<TPayload>;
        public UniTask Enter<TState>() where TState : IState;

    }
}