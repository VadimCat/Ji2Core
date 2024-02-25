using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ji2.Pools
{
    public class Pool<TMono> : IDisposable where TMono : MonoBehaviour, IPoolable
    {
        private readonly Transform _poolParent;
        private readonly TMono _prefab;

        private readonly Stack<TMono> _pool = new();
        private readonly HashSet<TMono> _usedObjects = new();

        public Pool(TMono prefab, Transform poolParent)
        {
            this._prefab = prefab;
            this._poolParent = poolParent;
        }

        public Pool(TMono prefab, Transform poolParent, int initialSize) : this(prefab, poolParent)
        {
            _pool = new Stack<TMono>(initialSize);
            _usedObjects = new HashSet<TMono>(initialSize);

            for (int i = 0; i < initialSize; i++)
            {
                var poolable = CreatePoolable();
                poolable.DeSpawn();
                _pool.Push(poolable);
            }
        }

        public TMono Spawn(Vector3 position = default, Quaternion rotation = default, Transform parent = null,
            bool isWorldSpace = false)
        {
            TMono poolable;
            if (_pool.Count == 0)
            {
                poolable = CreatePoolable();
            }
            else
            {
                poolable = _pool.Pop();
            }

            Transform transform = poolable.transform;
            transform.SetParent(parent);
            switch (isWorldSpace)
            {
                case true:
                    transform.SetPositionAndRotation(position, rotation);
                    break;
                case false:
                    transform.SetLocalPositionAndRotation(position, rotation);
                    break;
            }

            poolable.Spawn();

            _usedObjects.Add(poolable);

            return poolable;
        }


        public void DeSpawn(TMono poolable)
        {
            poolable.DeSpawn();
            poolable.transform.SetParent(_poolParent);
            _usedObjects.Remove(poolable);
            _pool.Push(poolable);
        }


        public void Dispose()
        {
            foreach (var obj in _pool)
            {
                Object.Destroy(obj);
            }

            foreach (var obj in _usedObjects)
            {
                Object.Destroy(obj);
            }

            _pool.Clear();
            _usedObjects.Clear();
        }


        private TMono CreatePoolable()
        {
            return Object.Instantiate(_prefab, _poolParent);
        }
    }
}