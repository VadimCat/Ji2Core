using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ji2.CommonCore
{
    public class UpdateService : MonoBehaviour
    {
        private readonly List<IUpdatable> updatables = new();
        private readonly List<IFixedUpdatable> fixedUpdatables = new();

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
}