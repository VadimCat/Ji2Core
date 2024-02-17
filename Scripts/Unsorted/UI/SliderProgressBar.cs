using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ji2.UI
{
    public class SliderProgressBar : MonoBehaviour, IProgressBar
    {
        [SerializeField] private float speedPercent = .5f;
        [SerializeField] private Slider slider;
        
        private Tween _currentTween;

        public async UniTask AnimateProgressAsync(float normalProgress)
        {
            var duration = normalProgress * speedPercent;

            _currentTween?.Kill();
            _currentTween = slider.DOValue(normalProgress, duration)
                .SetLink(gameObject);

            await _currentTween.AwaitForComplete();
        }

        public async UniTask AnimateFakeProgressAsync(float duration)
        {
            await slider.DOValue(1, duration)
                .SetLink(gameObject)
                .AwaitForComplete();
        }
    }
}