using UnityEngine;

namespace Ji2Core.UI.Background
{
    public class BackgroundsList
    {
        private const string BackgroundIndexSaveKey = "background_index";
        private readonly Sprite[] _backgrounds;
        private int _index;

        public BackgroundsList(Sprite[] backgrounds)
        {
            this._backgrounds = backgrounds;
            Load();
        }

        public Sprite GetNext()
        {
            _index++;
            _index = _index == _backgrounds.Length ? 0 : _index;
            Save();
            return _backgrounds[_index];
        }


        public void Save() => PlayerPrefs.SetInt(BackgroundIndexSaveKey, _index);

        public void Load() => _index = PlayerPrefs.GetInt(BackgroundIndexSaveKey) - 1;

        public void ClearSave()
        {
        }
    }
}