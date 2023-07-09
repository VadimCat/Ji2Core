namespace Ji2.Context
{
    public interface IInstaller<T>
    {
        public T Install(Context context);
    }
}