using Ji2Core.Models;
using UnityEngine;

namespace Ji2Core.Core.Audio
{
    public class AudioSettings : ISavable
    {
        public const string sfxLevelKey = "sfxLevelKey";
        public const string musicLevelKey = "musicLevelKey";
    
        public float SfxLevel = 1;
        public float MusicLevel = 1;

        public AudioSettings()
        {
            Load();
        }
        
        public void Save()
        {
            PlayerPrefs.SetFloat(sfxLevelKey, SfxLevel);
            PlayerPrefs.SetFloat(musicLevelKey, MusicLevel);
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(sfxLevelKey))
            {
                SfxLevel = PlayerPrefs.GetFloat(sfxLevelKey);
            }

            if (PlayerPrefs.HasKey(musicLevelKey))
            {
                MusicLevel = PlayerPrefs.GetFloat(musicLevelKey);
            }
        }

        public void ClearSave()
        {
            PlayerPrefs.DeleteKey(sfxLevelKey);
            PlayerPrefs.DeleteKey(musicLevelKey);
        }
    }
}