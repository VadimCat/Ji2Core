using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TextProgressBar : MonoBehaviour, IProgressBar
    {
        [SerializeField] private float speedPercent = .5f;

        [SerializeField] private Image loadingBar;
        [SerializeField] private TMP_Text progress;

        private Tween currentTween;
        private const string ProgressTemplate = "{0}%";

        public async UniTask AnimateProgressAsync(float normalProgress)
        {
            var duration = normalProgress * speedPercent;

            currentTween?.Kill();
            currentTween =
                loadingBar.DOFillAmount(normalProgress, duration)
                    .OnUpdate(UpdateTextProgress)
                    .SetLink(gameObject);

            await currentTween.AwaitForComplete();
        }

        public async UniTask AnimateFakeProgressAsync(float duration)
        {
            await loadingBar.DOFillAmount(1, duration)
                .OnUpdate(UpdateTextProgress)
                .SetLink(gameObject)
                .AwaitForComplete();
        }

        private void UpdateTextProgress()
        {
            progress.text = string.Format(ProgressTemplate, (loadingBar.fillAmount * 100).ToString("N0"));
        }
    }

    public interface IProgressBar
    {
        public UniTask AnimateProgressAsync(float normalProgress);
        public UniTask AnimateFakeProgressAsync(float duration);
    }
}