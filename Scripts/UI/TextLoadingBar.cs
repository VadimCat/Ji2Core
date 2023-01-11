using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Screens
{
    public class TextLoadingBar : MonoBehaviour
    {
        private const float SPEED_PERCENT = .5f;
        [SerializeField] private Image loadingBar;
        [SerializeField] private TMP_Text progress;

        private Tween currentTween;
        private const string ProgressTemplate = "{0}%";

        public void UpdateLoadingProgress(float normalProgress)
        {
            var duration = normalProgress * SPEED_PERCENT;
            
            currentTween?.Kill();
            currentTween = loadingBar.DOFillAmount(normalProgress, duration).SetLink(gameObject);
            currentTween.onUpdate += () => SetTextProgress(loadingBar.fillAmount);
        }

        public void SetLoadingProgress(float normalProgress)
        {
            loadingBar.fillAmount = normalProgress;
            SetTextProgress(normalProgress);
        }

        public async UniTask AnimateProgress(float duration)
        {
            await loadingBar.DOFillAmount(1, duration)
                .OnUpdate(() => { SetTextProgress(loadingBar.fillAmount); })
                .SetLink(gameObject)
                .AwaitForComplete();
        }

        private void SetTextProgress(float normalProgress)
        {
            progress.text = string.Format(ProgressTemplate, (normalProgress * 100).ToString("N0"));
        }
    }
}