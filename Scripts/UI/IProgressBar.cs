using Cysharp.Threading.Tasks;

namespace Ji2.UI
{
    public interface IProgressBar
    {
        public UniTask AnimateProgressAsync(float normalProgress);
        public UniTask AnimateFakeProgressAsync(float duration);
    }
}