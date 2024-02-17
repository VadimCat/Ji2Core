using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ji2.CommonCore
{
    public class UpdateService : MonoBehaviour
    {
        private readonly List<IUpdatable> _updatables = new();
        private readonly List<IFixedUpdatable> _fixedUpdatables = new();
        private readonly List<ILateUpdatable> _lateUpdatables = new();

        private void Update()
        {
            for (int i = 0; i < _updatables.Count; i++)
            {
                _updatables[i].OnUpdate();
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _fixedUpdatables.Count; i++)
            {
                _fixedUpdatables[i].OnFixedUpdate();
            }
        }

        private void LateUpdate()
        {
            foreach(var lateUpdatable in _lateUpdatables)
            {
                lateUpdatable.OnLateUpdate();
            }
        }

        public void Add(object updatable)
        {
            if (updatable is IUpdatable upd && !_updatables.Contains(upd))
            {
                _updatables.Add(upd);
            }
            if (updatable is IFixedUpdatable fixUpd && !_fixedUpdatables.Contains(fixUpd))
            {
                _fixedUpdatables.Add(fixUpd);
            }
            if (updatable is ILateUpdatable lateUpd && !_lateUpdatables.Contains(lateUpd))
            {
                _lateUpdatables.Add(lateUpd);
            }
        }

        public void Remove(object updatable)
        {
            if (updatable is IUpdatable upd && _updatables.Contains(upd))
            {
                _updatables.Remove(upd);
            }
            if (updatable is IFixedUpdatable fixUpd && _fixedUpdatables.Contains(fixUpd))
            {
                _fixedUpdatables.Remove(fixUpd);
            }

            if (updatable is ILateUpdatable lateUpd && _lateUpdatables.Contains(lateUpd))
            {
                _lateUpdatables.Remove(lateUpd);
            }
        }
    }

    public interface IUpdatable
    {
        public void OnUpdate();
    }

    public interface IFixedUpdatable
    {
        public void OnFixedUpdate();
    }

    public interface ILateUpdatable
    {
        public void OnLateUpdate();
    }
}