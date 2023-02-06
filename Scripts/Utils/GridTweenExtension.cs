using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ji2.Utils
{
    public static class GridTweenExtension
    {
        public static Tween DoSpacing(this GridLayoutGroup grid, Vector2 target, float time)
        {
            return DOTween.To(() => grid.spacing, spacing => grid.spacing = spacing, Vector2.zero, .2f);
        }
    }
}