using UnityEngine;

namespace Ji2.Context
{
    public abstract class MonoInstaller<T> : MonoBehaviour, IInstaller<T> where T : class
    {
        protected abstract T Create(DiContext diContext);
        
        public T Install(DiContext diContext)
        {
            var instance = Create(diContext);
            
            diContext.Register(instance);
            return instance;
        }
    }
}