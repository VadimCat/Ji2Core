using Ji2Core.Models;
using UnityEngine;

namespace Ji2.Utils
{
    public class CircularSavableArray<T> : ISavable
    {
        private readonly string indexSaveKey;
        private readonly T[] array;
        private int index;

        public CircularSavableArray(T[] array, string indexSaveKey)
        {
            this.array = array;
            this.indexSaveKey = indexSaveKey;   
            Load();
        }

        public T GetNext()
        {
            index++;
            index = index == array.Length ? 0 : index;
            Save();
            return array[index];
        }


        public void Save() => PlayerPrefs.SetInt(indexSaveKey, index);

        public void Load() => index = PlayerPrefs.GetInt(indexSaveKey) - 1;

        public void ClearSave()
        {
        }
    }
}