using DG.Tweening;
using UnityEngine;

namespace Core.Compliments
{
    [CreateAssetMenu]
    public class ComplimentsAnimationConfig : ScriptableObject
    {
        [SerializeField] [Range(0, 1)] public float distancePercentY;
        [SerializeField] [Range(0, 1)] public float distancePercentX ;
        [SerializeField] public float moveDuration;
        [SerializeField] public Ease moveEase = Ease.Linear;
        [SerializeField] public float fadeInDuration ;
        [SerializeField] public Ease fadeInEase = Ease.Linear;
        [SerializeField] public float fadeOutDuration;
        [SerializeField] public Ease fadeOutEase = Ease.Linear;
        [SerializeField] public float rotateDuration;
        [SerializeField] public float angle;
        [SerializeField] public Ease rotationEase;
    }
}
