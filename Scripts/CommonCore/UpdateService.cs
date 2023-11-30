using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ji2.CommonCore
{
    public class UpdateService : MonoBehaviour
    {
        private readonly List<IUpdatable> updatables = new();
        private readonly List<IFixedUpdatable> fixedUpdatables = new();
        private readonly List<ILateUpdatable> lateUpdatables = new();

        private void Update()
        {
            for (int i = 0; i < updatables.Count; i++)
            {
                updatables[i].OnUpdate();
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < fixedUpdatables.Count; i++)
            {
                fixedUpdatables[i].OnFixedUpdate();
            }
        }

        private void LateUpdate()
        {
            foreach(var lateUpdatable in lateUpdatables)
            {
                lateUpdatable.OnLateUpdate();
            }
        }

        public void Add(object updatable)
        {
            if (updatable is IUpdatable upd && !updatables.Contains(upd))
            {
                updatables.Add(upd);
            }
            if (updatable is IFixedUpdatable fixUpd && !fixedUpdatables.Contains(fixUpd))
            {
                fixedUpdatables.Add(fixUpd);
            }
            if (updatable is ILateUpdatable lateUpd && !lateUpdatables.Contains(lateUpd))
            {
                lateUpdatables.Add(lateUpd);
            }
        }

        public void Remove(object updatable)
        {
            if (updatable is IUpdatable upd && updatables.Contains(upd))
            {
                updatables.Remove(upd);
            }
            if (updatable is IFixedUpdatable fixUpd && fixedUpdatables.Contains(fixUpd))
            {
                fixedUpdatables.Remove(fixUpd);
            }

            if (updatable is ILateUpdatable lateUpd && lateUpdatables.Contains(lateUpd))
            {
                lateUpdatables.Remove(lateUpd);
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