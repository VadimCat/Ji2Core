using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ji2.UI
{
    public class MarkerProgressBar : MonoBehaviour, IProgressBar
    {
        [SerializeField] private float speedPercent = .5f;
        [SerializeField] private Slider slider;
        
        private Tween currentTween;

        public async UniTask AnimateProgressAsync(float normalProgress)
        {
            var duration = normalProgress * speedPercent;

            currentTween?.Kill();
            currentTween = slider.DOValue(normalProgress, duration)
                .SetLink(gameObject);

            await currentTween.AwaitForComplete();
        }

        public async UniTask AnimateFakeProgressAsync(float duration)
        {
            await slider.DOValue(1, duration)
                .SetLink(gameObject)
                .AwaitForComplete();
        }
    }
}