using DG.Tweening;
using UnityEngine;

namespace Ji2Core.Utils
{
    public static class TweenAnimations
    {
        public static Tween DoPulseScale(this Transform transform, float maxScale, float duraion, GameObject link,
            Ease ease = Ease.Linear, int loops = -1)
        {
            return transform.DOScale(maxScale, duraion)
                .SetLoops(loops, LoopType.Yoyo)
                .SetEase(Ease.Linear)
                .SetLink(link);
        }
    }
}