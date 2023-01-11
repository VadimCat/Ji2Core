namespace Ji2Core.Core.Installers
{
    public interface IInstaller<T>
    {
        public T Install(Context context);
    }
}