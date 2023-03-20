using System.Collections.Generic;
using UnityEngine;

namespace Ji2.Utils
{
    public static class Vector2IntUtils
    {
        public static IEnumerable<Vector2Int> GetVector2IntDirections()
        {
            yield return Vector2Int.down;
            yield return Vector2Int.up;
            yield return Vector2Int.left;
            yield return Vector2Int.right;
        }
    }
}