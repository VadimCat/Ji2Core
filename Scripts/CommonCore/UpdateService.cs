using System.Collections.Generic;
using UnityEngine;

namespace Ji2.CommonCore
{
    public class UpdateService : MonoBehaviour
    {
        private List<IUpdatable> updatables = new();

        private void Update()
        {
            for (int i = 0; i < updatables.Count; i++)
            {
                updatables[i].OnUpdate();
            }
        }

        public void Add(IUpdatable updatable)
        {
            if (!updatables.Contains(updatable))
                updatables.Add(updatable);
        }

        public void Remove(IUpdatable updatable)
        {
            if (updatables.Contains(updatable))
                updatables.Remove(updatable);
        }
    }

    public interface IUpdatable
    {
        public void OnUpdate();
    }
}