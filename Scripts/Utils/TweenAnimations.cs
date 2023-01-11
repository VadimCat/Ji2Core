using DG.Tweening;
using UnityEngine;

namespace Ji2Core.Scripts.Utils
{
    public static class TweenAnimations
    {
        public static Tween DoPulseScale(this Transform transform, float maxScale, float duraion, GameObject link,
            Ease ease = Ease.Linear, int loops = -1)
        {
            return transform.DOScale(maxScale, duraion)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear)
                .SetLink(link);
        }
    }
}