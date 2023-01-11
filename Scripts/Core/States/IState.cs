using Cysharp.Threading.Tasks;

namespace Ji2Core.Core.States
{
    public interface IState
    {
        public UniTask Enter();
        public UniTask Exit();
    }
}