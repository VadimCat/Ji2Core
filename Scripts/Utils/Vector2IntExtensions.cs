using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ji2.Utils
{
    public static class Vector2IntExtensions
    {
        public static IEnumerable<Vector2Int> GetVector2IntDirections()
        {
            yield return Vector2Int.down;
            yield return Vector2Int.up;
            yield return Vector2Int.left;
            yield return Vector2Int.right;
        }
    }
    
    public static class SpriteExtensions
    {
        /// <summary>
        /// width / height (a = x/y)
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public static float Aspect(this Sprite sprite)
        {
            return (float)sprite.texture.width / sprite.texture.width;
        }
    }

    public static class ArrayExtensions
    {
        public static bool IsInRange1D(this Array array, int x)
        {
            return x >= array.GetLowerBound(0) && x < array.GetUpperBound(0);
        }
        
        public static bool IsInRange2D(this Array array, int x, int y)
        {
            return x >= 0 && x < array.GetLength(0) && y >= 0 && y < array.GetLength(0);
        }
    }
}