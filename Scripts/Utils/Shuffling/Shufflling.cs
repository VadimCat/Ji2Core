﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ji2.Utils.Shuffling
{
    public static class Shufflling
    {
        public static int[] CreateShuffledArray(int length)
        {
            var list = new int[length];
            for (int i = 0; i < length; i++)
            {
                var rnd = Random.Range(0, i);
                list[i] = list[rnd];
                list[rnd] = i;
            }

            return list;
        }
        
        public static Vector2Int[,] CreatedShuffled2DimensionalArray(Vector2Int size)
        {
            var list = new Vector2Int[size.x, size.y];
            var length = size.x * size.y;
            for (int i = 0; i < length; i++)
            {
                var rnd = GetTwoDimensionalVector2Int(Random.Range(0, i), size);
                var iVector = GetTwoDimensionalVector2Int(i, size);
                list[iVector.x, iVector.y] = list[rnd.x, rnd.y];
                list[rnd.x, rnd.y] = iVector;
            }

            return list;
        }

        public static Vector2Int GetTwoDimensionalVector2Int(int value, Vector2Int size)
        {
            if (value >= size.x * size.y)
            {
                throw new ArgumentOutOfRangeException("value is higher then max array element");
            }

            return new Vector2Int((value) % size.x, (value) / size.x);
        }

        public static int Vector2IntToInt(Vector2Int value, Vector2Int size)
        {
            if (value.x >= size.x)
            {
                throw new ArgumentOutOfRangeException("value.X is higher then X dimension of array");
            }
            if (value.y >= size.y)
            {
                throw new ArgumentOutOfRangeException("value.Y is higher then Y dimension of array");
            }

            return value.x + size.x * value.y;
        }
    }
}