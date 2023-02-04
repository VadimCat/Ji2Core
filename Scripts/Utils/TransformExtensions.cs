using UnityEngine;

namespace Ji2Core.Utils
{
    public static class TransformExtensions
    {
        public static void SetLocalX(this Transform transform, float x)
        {
            var localPos = transform.localPosition;
            localPos.x = x;
            transform.localPosition = localPos;
        }
    }
}