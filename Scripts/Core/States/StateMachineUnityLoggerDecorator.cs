using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ji2Core.Core.States
{
    public class StateMachineUnityLoggerDecorator : IStateMachine
    {
        private readonly IStateMachine _stateMachine;
        private readonly LogType _logType;

        public StateMachineUnityLoggerDecorator(IStateMachine stateMachine, LogType logType)
        {
            _stateMachine = stateMachine;
            _logType = logType;
        }

        public IExitableState CurrentState => _stateMachine.CurrentState;
        public event Action<IExitableState> StateEntered
        {
            add => _stateMachine.StateEntered += value;
            remove => _stateMachine.StateEntered -= value;
        }
        
        public async UniTask ExitCurrent()
        {
            LogMessage($"Exiting state {CurrentState.GetType()}");
            await _stateMachine.ExitCurrent();
            LogMessage($"Exited state {CurrentState.GetType()}");
        }

        public async UniTask Enter<TState, TPayload>(TPayload payload) where TState : IPayloadedState<TPayload>
        {
            LogMessage($"Entering state {typeof(TState)}");
            await _stateMachine.Enter<TState, TPayload>(payload);
            LogMessage($"Entered state {typeof(TState)}");
        }

        public async UniTask Enter<TState>() where TState : IState
        {
            LogMessage($"Entering state {typeof(TState)}");
            await _stateMachine.Enter<TState>();
            LogMessage($"Entered state {typeof(TState)}");
        }

        private void LogMessage(string message)
        {
            switch (_logType)
            {
                case LogType.Error:
                    Debug.LogError(message);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(message);
                    break;
                case LogType.Log:
                    Debug.Log(message);
                    break;
                case LogType.Assert:
                case LogType.Exception:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}