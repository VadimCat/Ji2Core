using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ji2.Pools
{
    public class Pool<TMono> : IDisposable where TMono : MonoBehaviour, IPoolable
    {
        private readonly Transform poolParent;
        private readonly TMono prefab;

        private readonly Stack<TMono> pool = new();
        private readonly HashSet<TMono> usedObjects = new();

        public Pool(TMono prefab, Transform poolParent)
        {
            this.prefab = prefab;
            this.poolParent = poolParent;
        }

        public Pool(TMono prefab, Transform poolParent, int initialSize) : this(prefab, poolParent)
        {
            pool = new Stack<TMono>(initialSize);
            usedObjects = new HashSet<TMono>(initialSize);

            for (int i = 0; i < initialSize; i++)
            {
                var poolable = CreatePoolable();
                poolable.DeSpawn();
                pool.Push(poolable);
            }
        }

        public TMono Spawn(Vector3 position = default, Quaternion rotation = default, Transform parent = null,
            bool isWorldSpace = false)
        {
            TMono poolable;
            if (pool.Count == 0)
            {
                poolable = CreatePoolable();
            }
            else
            {
                poolable = pool.Pop();
            }

            Transform transform = poolable.transform;
            transform.SetParent(parent);
            switch (isWorldSpace)
            {
                case true:
                    transform.position = position;
                    transform.rotation = rotation;
                    break;
                case false:
                    transform.localPosition = position;
                    transform.localRotation = rotation;
                    break;
            }

            poolable.Spawn();

            usedObjects.Add(poolable);

            return poolable;
        }


        public void DeSpawn(TMono poolable)
        {
            poolable.DeSpawn();
            poolable.transform.SetParent(poolParent);
            usedObjects.Remove(poolable);
            pool.Push(poolable);
        }


        public void Dispose()
        {
            foreach (var obj in pool)
            {
                Object.Destroy(obj);
            }

            foreach (var obj in usedObjects)
            {
                Object.Destroy(obj);
            }

            pool.Clear();
            usedObjects.Clear();
        }


        private TMono CreatePoolable()
        {
            return Object.Instantiate(prefab, poolParent);
        }
    }
}