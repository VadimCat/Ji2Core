using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Compliments
{
    public class TextCompliments : MonoBehaviour, ICompliments
    {
        [SerializeField] private TMP_Text complimentText;
        [SerializeField] private TextComplimentsAsset textComplimentsAsset;
        [SerializeField] private ComplimentsAnimationConfig complimentsAnimationConfig;

        private Sequence _animationsSequence;

        public void ShowRandomFromScreenPosition(Vector2 startPosition)
        {
            complimentText.text = textComplimentsAsset.GetRandomWord();
            complimentText.transform.position = startPosition;
            complimentText.color = textComplimentsAsset.GetRandomColor();

            var targetPosition = GetTargetPosition(startPosition);
            var angleFactor = targetPosition.x > startPosition.x ? -1 : 1;
            StartAnimation(targetPosition, angleFactor);
        }

        private void StartAnimation(Vector2 targetPosition, int angleFactor)
        {
            if(_animationsSequence != null && _animationsSequence.IsPlaying())
                return;
            
            complimentText.transform.rotation = Quaternion.identity;

            complimentText.alpha = 0;
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
            float x = Random.Range(0, Screen.width);
            float y = Screen.height;
            var distanceX = x - startPosition.x;
            var distanceY = y - startPosition.y;

            x = startPosition.x + distanceX * complimentsAnimationConfig.distancePercentX;
            y = startPosition.y + distanceY * complimentsAnimationConfig.distancePercentY;
            return new Vector2(x, y);
        }
    }

    public interface ICompliments
    {
        public void ShowRandomFromScreenPosition(Vector2 startPosition);
    }
}