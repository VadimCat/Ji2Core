using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ji2.UI
{
    public class FillingProgressBar : MonoBehaviour, IProgressBar
    {
        [SerializeField] private float speedPercent = .5f;

        [SerializeField] private Image loadingBar;

        private Tween currentTween;

        public async UniTask AnimateProgressAsync(float normalProgress)
        {
            var duration = normalProgress * speedPercent;

            currentTween?.Kill();
            currentTween =
                loadingBar.DOFillAmount(normalProgress, duration)
                    .SetLink(gameObject);

            await currentTween.AwaitForComplete();
        }

        public async UniTask AnimateFakeProgressAsync(float duration)
        {
            await loadingBar.DOFillAmount(1, duration)
                .SetLink(gameObject)
                .AwaitForComplete();
        }
    }
}