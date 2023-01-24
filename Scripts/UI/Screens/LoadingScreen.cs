using Client.Screens;
using Cysharp.Threading.Tasks;
using Ji2Core.Scripts.Utils;
using UnityEngine;

namespace Ji2Core.UI.Screens
{
    public class LoadingScreen : BaseScreen
    {
        [SerializeField] private Transform logo0;
        [SerializeField] private TextLoadingBar loadingBar;

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
            loadingBar.SetLoadingProgress(progress);
        }

        public async UniTask AnimateLoadingBar(float duration)
        {
            await loadingBar.AnimateProgress(duration);
        }
    }
}