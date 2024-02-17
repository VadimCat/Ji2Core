﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Ji2.Configs.Levels
{
    public abstract class LevelsViewDataStorageBase<TLevel> : ScriptableObject, IBootstrapable where TLevel : ILevelViewData
    {
        [SerializeField] private List<TLevel> levels;

        private Dictionary<string, TLevel> levelsDict;

        public List<string> levelsList => levelsDict.Keys.ToList();

        public TLevel GetData(string levelId)
        {
            return levelsDict[levelId];
        }

        public void Bootstrap()
        {
            levelsDict = new Dictionary<string, TLevel>(levels.Count);
            foreach (var lvl in levels)
            {
                if (!levelsDict.TryAdd(lvl.Id, lvl))
                {
                    throw new DuplicateNameException("Levels with same id detected");
                }
            }
        }

        public string[] GetLevelsOrder()
        {
            return (from level in levels
                select level.Id).ToArray();
        }

#if UNITY_EDITOR
        public bool LevelIdExists(string id)
        {
            return levels.Any(lvl => lvl.Id == id);
        }

        public void AddLevel(TLevel level)
        {
            if (LevelIdExists(level.Id))
            {
                throw new LevelExistsException(level.Id);
            }

            levels.Add(level);
            EditorUtility.SetDirty(this);
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }
#endif
    }
}