namespace Ji2.CommonCore.SaveDataContainer
{
    public interface ISaveDataContainer
    {
        public void Load();
        public void Save();
        public void SaveValue(string key, object value);
        public T GetValue<T>(string key, T defaultValue = default);
        public void ResetKey(string key);
    }
}