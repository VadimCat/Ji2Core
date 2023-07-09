using UnityEngine;

namespace Ji2.Context
{
    public abstract class MonoInstaller<T> : MonoBehaviour, IInstaller<T> where T : class
    {
        protected abstract T Create(Context context);
        
        public T Install(Context context)
        {
            var instance = Create(context);
            
            context.Register(instance);
            return instance;
        }
    }
}