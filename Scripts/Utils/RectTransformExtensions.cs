using UnityEngine;

namespace Ji2.Utils
{
    public static class RectTransformExtensions
    {
        public static void SetWidth(this RectTransform transform, float width)
        {
            var size = transform.sizeDelta;
            size.x = width;
            transform.sizeDelta = size;
        }
    }
}