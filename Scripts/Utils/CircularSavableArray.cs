using System.Collections.Generic;
using Ji2Core.Models;
using UnityEngine;

namespace Ji2.Utils
{
    public class CircularSavableArray<T> : ISavable
    {
        private readonly string _indexSaveKey;
        private readonly List<T> _array;
        private int _index;

        public CircularSavableArray(IEnumerable<T> array, string indexSaveKey)
        {
            this._array = new List<T>(array);
            this._indexSaveKey = indexSaveKey;   
            Load();
        }

        public T GetNext()
        {
            _index++;
            _index = _index == _array.Count ? 0 : _index;
            Save();
            return _array[_index];
        }


        public void Save() => PlayerPrefs.SetInt(_indexSaveKey, _index);

        public void Load() => _index = PlayerPrefs.GetInt(_indexSaveKey) - 1;

        public void ClearSave()
        {
        }
    }
}