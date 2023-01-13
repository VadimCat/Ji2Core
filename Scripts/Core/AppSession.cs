using System.Collections.Generic;
using Ji2Core.Core;
using Ji2Core.Core.States;
using Newtonsoft.Json;
using UnityEngine;

namespace Ji2Core.Core
{


    public class AppSession
    {
        public StateMachine StateMachine { get; }

        public AppSession(StateMachine stateStateMachine)
        {
            StateMachine = stateStateMachine;
        }
    }

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
            Debug.LogError(text);
        }

        public void SaveValue(string key, object value)
        {
            saves.Add(key, JsonConvert.SerializeObject(value));
            Save();
        }

        public T GetSave<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(saves[key]);
        }
    }

    public interface ISaveDataContainer
    {
        public void Load();
        public void Save();
        public void SaveValue(string key, object value);
    }
}