using Cysharp.Threading.Tasks;

namespace Ji2Core.Core.SaveDataContainer
{
    public interface ISaveDataContainer
    {
        public UniTask Load();
        public void Save();
        public void SaveValue(string key, object value);
        public T GetValue<T>(string key, T defaultValue = default);
        public void ResetKey(string key);
    }
}