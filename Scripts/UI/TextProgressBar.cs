using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Ji2.UI
{
    public class TextProgressBar : MonoBehaviour, IProgressBar
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private float speedPercent = .5f;

        private const string ProgressTemplate = "{0}%";

        private Tween currentTween;
        private float progress;

        private void UpdateTextProgress()
        {
            text.text = string.Format(ProgressTemplate, (progress * 100).ToString("N0"));
        }

        public UniTask AnimateProgressAsync(float normalProgress)
        {
            var duration = normalProgress * speedPercent;

            currentTween?.Kill();
            currentTween = DOTween.To(() => progress, pr =>
            {
                progress = pr;
                UpdateTextProgress();
            }, normalProgress, duration)
                .SetLink(gameObject);
            
            return currentTween.AwaitForComplete();
        }

        public UniTask AnimateFakeProgressAsync(float duration)
        {
            currentTween?.Kill();
            currentTween = DOTween.To(() => progress, pr =>
            {
                progress = pr;
                UpdateTextProgress();
            }, 1, duration)
                .SetLink(gameObject);
            
            return currentTween.AwaitForComplete();
        }
    }
}