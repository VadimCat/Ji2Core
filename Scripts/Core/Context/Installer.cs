namespace Ji2.Context
{
    public abstract class Installer<T> : IInstaller<T> where T : class
    {
        protected abstract T Create();

        public T Install(DiContext diContext)
        {
            var instance = Create();
            
            diContext.Register(instance);
            return instance;
        }
    }
}