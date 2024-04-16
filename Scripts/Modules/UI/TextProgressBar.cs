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

        private Tween _currentTween;
        private float _progress;

        private void UpdateTextProgress()
        {
            text.text = string.Format(ProgressTemplate, (_progress * 100).ToString("N0"));
        }

        public UniTask AnimateProgressAsync(float normalProgress)
        {
            var duration = normalProgress * speedPercent;

            _currentTween?.Kill();
            _currentTween = DOTween.To(() => _progress, pr =>
            {
                _progress = pr;
                UpdateTextProgress();
            }, normalProgress, duration)
                .SetLink(gameObject);
            
            return _currentTween.AwaitForComplete();
        }

        public UniTask AnimateFakeProgressAsync(float duration)
        {
            _currentTween?.Kill();
            _currentTween = DOTween.To(() => _progress, pr =>
            {
                _progress = pr;
                UpdateTextProgress();
            }, 1, duration)
                .SetLink(gameObject);
            
            return _currentTween.AwaitForComplete();
        }
    }
}