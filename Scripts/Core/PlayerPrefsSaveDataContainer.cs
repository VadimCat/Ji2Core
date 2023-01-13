using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Ji2Core.Core
{
    public class PlayerPrefsSaveDataContainer : ISaveDataContainer
    {
        private const string SAVE_FILE_KEY = "SaveData";

        private Dictionary<string, string> saves;

        public void Load()
        {
            if (PlayerPrefs.HasKey(SAVE_FILE_KEY))
            {
                var savesText = PlayerPrefs.GetString(SAVE_FILE_KEY);
                saves = JsonConvert.DeserializeObject<Dictionary<string, string>>(savesText);
            }
            else
            {
                saves = new Dictionary<string, string>();
            }
        }

        public void Save()
        {
            var text = JsonConvert.SerializeObject(saves);
            PlayerPrefs.SetString(SAVE_FILE_KEY, text);
        }

        public void SaveValue(string key, object value)
        {
            saves[key] = JsonConvert.SerializeObject(value);
            Save();
        }

        public T LoadValue<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(saves[key]);
        }
    }
}