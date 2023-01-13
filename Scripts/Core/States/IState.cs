using Cysharp.Threading.Tasks;

namespace Ji2Core.Core.States
{
    public interface IState : IExitableState
    {
        public UniTask Enter();
    }

    public interface IPayloadedState<TPayload> : IExitableState
    {
        public UniTask Enter(TPayload payload);
    }

    public interface IExitableState
    {
        public UniTask Exit();
    }
}