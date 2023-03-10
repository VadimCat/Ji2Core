namespace Ji2Core.Core.Installers
{
    public abstract class Installer<T> : IInstaller<T> where T : class
    {
        protected abstract T Create();

        public T Install(Context context)
        {
            var instance = Create();
            
            context.Register(instance);
            return instance;
        }
    }
}