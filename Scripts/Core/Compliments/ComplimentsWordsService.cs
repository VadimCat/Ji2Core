using Client.UI.Compliments;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ji2Core.Core.Compliments
{
    public class ComplimentsWordsService : MonoBehaviour
    {
        [SerializeField] private TMP_Text complimentText;
        [SerializeField] private ComplimentsWordsAsset complimentsWordsAsset;
        [SerializeField] private ComplimentsWordsConfig complimentsWordsConfig;

        private Sequence animationsSequence;

        public void ShowRandomFromScreenPosition(Vector2 startPosition)
        {
            complimentText.text = complimentsWordsAsset.GetRandomWord();
            complimentText.transform.position = startPosition;
            complimentText.color = complimentsWordsAsset.GetRandomColor();

            var targetPosition = GetTargetPosition(startPosition);
            var angleFactor = targetPosition.x > startPosition.x ? -1 : 1;
            StartAnimation(targetPosition, angleFactor);
        }

        private void StartAnimation(Vector2 targetPosition, int angleFactor)
        {
            complimentText.transform.rotation = Quaternion.identity;

            complimentText.alpha = 0;
            animationsSequence = DOTween.Sequence();
            animationsSequence.Join(complimentText
                .DOFade(1, complimentsWordsConfig.fadeOutDuration)
                .SetEase(complimentsWordsConfig.fadeOutEase));

            animationsSequence.Join(complimentText.transform
                .DOMove(targetPosition, complimentsWordsConfig.moveDuration)
                .SetEase(complimentsWordsConfig.moveEase));

            animationsSequence.Join(complimentText.transform
                .DORotate(new Vector3(0, 0, complimentsWordsConfig.angle * angleFactor),
                    complimentsWordsConfig.rotateDuration)
                .SetEase(complimentsWordsConfig.rotationEase));

            animationsSequence.Join(complimentText
                .DOFade(0, complimentsWordsConfig.fadeInDuration)
                .SetDelay(complimentsWordsConfig.moveDuration - complimentsWordsConfig.fadeInDuration)
                .SetEase(complimentsWordsConfig.fadeInEase));
            animationsSequence.Play();
        }

        private Vector2 GetTargetPosition(Vector2 startPosition)
        {
            float x = Random.Range(0, Screen.width);
            float y = Screen.height;
            var distanceX = x - startPosition.x;
            var distanceY = y - startPosition.y;

            x = startPosition.x + distanceX * complimentsWordsConfig.distancePercentX;
            y = startPosition.y + distanceY * complimentsWordsConfig.distancePercentY;
            return new Vector2(x, y);
        }
    }
}