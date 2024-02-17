using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Compliments
{
    public class ImageCompliments : MonoBehaviour, ICompliments
    {
        [SerializeField] private RectTransform rect;
        
        [SerializeField] private Image complimentText;
        [SerializeField] private ImageComplimentsAsset textComplimentsAsset;
        [SerializeField] private ComplimentsAnimationConfig complimentsAnimationConfig;

        private Sequence _animationsSequence;

        public void ShowRandomFromScreenPosition(Vector2 startPosition)
        {
            complimentText.sprite = textComplimentsAsset.GetRandomWord();
            complimentText.transform.localPosition = startPosition;

            var targetPosition = GetTargetPosition(startPosition);
            var angleFactor = targetPosition.x > startPosition.x ? -1 : 1;
            StartAnimation(targetPosition, angleFactor);
        }

        private void StartAnimation(Vector2 targetPosition, int angleFactor)
        {
            complimentText.transform.rotation = Quaternion.identity;

            complimentText.color = new Color(1,1,1, 0);
            _animationsSequence = DOTween.Sequence();
            _animationsSequence.Join(complimentText
                .DOFade(1, complimentsAnimationConfig.fadeOutDuration)
                .SetEase(complimentsAnimationConfig.fadeOutEase));

            _animationsSequence.Join(complimentText.transform
                .DOMove(targetPosition, complimentsAnimationConfig.moveDuration)
                .SetEase(complimentsAnimationConfig.moveEase));

            _animationsSequence.Join(complimentText.transform
                .DORotate(new Vector3(0, 0, complimentsAnimationConfig.angle * angleFactor),
                    complimentsAnimationConfig.rotateDuration)
                .SetEase(complimentsAnimationConfig.rotationEase));

            _animationsSequence.Join(complimentText
                .DOFade(0, complimentsAnimationConfig.fadeInDuration)
                .SetDelay(complimentsAnimationConfig.moveDuration - complimentsAnimationConfig.fadeInDuration)
                .SetEase(complimentsAnimationConfig.fadeInEase));
            _animationsSequence.Play();
        }

        private Vector2 GetTargetPosition(Vector2 startPosition)
        {
            float x = Random.Range(0, rect.rect.width);
            float y = rect.rect.height;
            var distanceX = x - startPosition.x;
            var distanceY = y - startPosition.y;

            x = startPosition.x + distanceX * complimentsAnimationConfig.distancePercentX;
            y = startPosition.y + distanceY * complimentsAnimationConfig.distancePercentY;
            return new Vector2(x, y);
        }
    }
}