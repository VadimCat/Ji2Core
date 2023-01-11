using Ji2Core.Models;
using UnityEngine;

namespace Ji2Core.UI.Background
{
    public class BackgroundsList : ISavable
    {
        private const string BACKGROUND_INDEX_SAVE_KEY = "background_index";
        private readonly Sprite[] backgrounds;
        private int index;

        public BackgroundsList(Sprite[] backgrounds)
        {
            this.backgrounds = backgrounds;
            Load();
        }

        public Sprite GetNext()
        {
            index++;
            index = index == backgrounds.Length ? 0 : index;
            Save();
            return backgrounds[index];
        }


        public void Save() => PlayerPrefs.SetInt(BACKGROUND_INDEX_SAVE_KEY, index);

        public void Load() => index = PlayerPrefs.GetInt(BACKGROUND_INDEX_SAVE_KEY) - 1;

        public void ClearSave()
        {
        }
    }
}