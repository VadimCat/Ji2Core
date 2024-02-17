using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Ji2.CommonCore.SaveDataContainer
{
    public class PlayerPrefsSaveDataContainer : ISaveDataContainer
    {
        private const string SaveFileKey = "SaveData";

        private Dictionary<string, string> _saves;

        public void Load()
        {
            if (PlayerPrefs.HasKey(SaveFileKey))
            {
                var savesText = PlayerPrefs.GetString(SaveFileKey);
                _saves = JsonConvert.DeserializeObject<Dictionary<string, string>>(savesText);
            }
            else
            {
                _saves = new Dictionary<string, string>();
            }
        }

        public void Save()
        {
            var text = JsonConvert.SerializeObject(_saves);
            PlayerPrefs.SetString(SaveFileKey, text);
        }

        public void SaveValue(string key, object value)
        {
            var serialized = JsonConvert.SerializeObject(value);
            _saves[key] = serialized;
            Save();
        }

        public T GetValue<T>(string key, T defaultValue = default)
        {
            if (!_saves.ContainsKey(key))
            {
                return defaultValue;
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(_saves[key]);
            }
        }

        public void ResetKey(string key)
        {
            _saves.Remove(key);
            Save();
        }
    }
}