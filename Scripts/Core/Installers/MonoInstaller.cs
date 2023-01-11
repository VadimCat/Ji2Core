using UnityEngine;

namespace Ji2Core.Core.Installers
{
    public abstract class MonoInstaller<T> : MonoBehaviour, IInstaller<T>
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