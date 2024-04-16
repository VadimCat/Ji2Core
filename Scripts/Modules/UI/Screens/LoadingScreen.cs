using Cysharp.Threading.Tasks;
using Ji2.ScreenNavigation;
using Ji2.Utils;
using UnityEngine;

namespace Ji2.UI.Screens
{
    public class LoadingScreen : BaseScreen
    {
        [SerializeField] private Transform logo0;
        [SerializeField] private IProgressBar _progressBar;

        private void Awake()
        {
            AnimateLogo();
        }

        private void AnimateLogo()
        {
            logo0.DoPulseScale(1.04f, 1, gameObject);
        }

        public void SetProgress(float progress)
        {
            _progressBar.AnimateProgressAsync(progress);
        }

        public async UniTask AnimateLoadingBar(float duration)
        {
            await _progressBar.AnimateFakeProgressAsync(duration);
        }
    }
}