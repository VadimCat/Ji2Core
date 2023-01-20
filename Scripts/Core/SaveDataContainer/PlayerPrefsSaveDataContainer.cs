using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Ji2Core.Core.SaveDataContainer
{
    public class PlayerPrefsSaveDataContainer : ISaveDataContainer
    {
        private const string SAVE_FILE_KEY = "SaveData";

        private Dictionary<string, string> saves;

        public async UniTask Load()
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

            await UniTask.CompletedTask;
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

        public T GetValue<T>(string key, T defaultValue = default)
        {
            if (!saves.ContainsKey(key))
            {
                return defaultValue;
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(saves[key]);
            }
        }

        public void ResetKey(string key)
        {
            saves.Remove(key);
            Save();
        }
    }
}