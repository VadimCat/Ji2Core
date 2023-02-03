using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Compliments
{
    public class ImageCompliments : MonoBehaviour, ICompliments
    {
        [SerializeField] private Image complimentText;
        [SerializeField] private ImageComplimentsAsset textComplimentsAsset;
        [SerializeField] private ComplimentsAnimationConfig complimentsAnimationConfig;

        private Sequence animationsSequence;

        public void ShowRandomFromScreenPosition(Vector2 startPosition)
        {
            complimentText.sprite = textComplimentsAsset.GetRandomWord();
            complimentText.transform.position = startPosition;

            var targetPosition = GetTargetPosition(startPosition);
            var angleFactor = targetPosition.x > startPosition.x ? -1 : 1;
            StartAnimation(targetPosition, angleFactor);
        }

        private void StartAnimation(Vector2 targetPosition, int angleFactor)
        {
            complimentText.transform.rotation = Quaternion.identity;

            complimentText.color = new Color(1,1,1, 0);
            animationsSequence = DOTween.Sequence();
            animationsSequence.Join(complimentText
                .DOFade(1, complimentsAnimationConfig.fadeOutDuration)
                .SetEase(complimentsAnimationConfig.fadeOutEase));

            animationsSequence.Join(complimentText.transform
                .DOMove(targetPosition, complimentsAnimationConfig.moveDuration)
                .SetEase(complimentsAnimationConfig.moveEase));

            animationsSequence.Join(complimentText.transform
                .DORotate(new Vector3(0, 0, complimentsAnimationConfig.angle * angleFactor),
                    complimentsAnimationConfig.rotateDuration)
                .SetEase(complimentsAnimationConfig.rotationEase));

            animationsSequence.Join(complimentText
                .DOFade(0, complimentsAnimationConfig.fadeInDuration)
                .SetDelay(complimentsAnimationConfig.moveDuration - complimentsAnimationConfig.fadeInDuration)
                .SetEase(complimentsAnimationConfig.fadeInEase));
            animationsSequence.Play();
        }

        private Vector2 GetTargetPosition(Vector2 startPosition)
        {
            float x = Random.Range(0, Screen.width);
            float y = Screen.height;
            var distanceX = x - startPosition.x;
            var distanceY = y - startPosition.y;

            x = startPosition.x + distanceX * complimentsAnimationConfig.distancePercentX;
            y = startPosition.y + distanceY * complimentsAnimationConfig.distancePercentY;
            return new Vector2(x, y);
        }
    }
}