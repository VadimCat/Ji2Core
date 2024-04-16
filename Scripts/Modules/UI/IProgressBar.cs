using Cysharp.Threading.Tasks;

namespace Ji2.UI
{
    public interface IProgressBar
    {
        public UniTask AnimateProgressAsync(float normalProgress);
        public UniTask AnimateFakeProgressAsync(float duration);
    }

    public class DummyProgressBar : IProgressBar
    {
        public UniTask AnimateProgressAsync(float normalProgress)
        {
            return UniTask.CompletedTask;
        }

        public UniTask AnimateFakeProgressAsync(float duration)
        {
            return UniTask.CompletedTask;
        }
    }
}