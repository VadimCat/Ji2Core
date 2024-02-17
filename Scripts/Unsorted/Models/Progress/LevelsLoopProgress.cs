using System.Collections.Generic;
using Ji2.CommonCore.SaveDataContainer;
using Ji2.Models;
using Random = System.Random;

namespace Client
{
    public class LevelsLoopProgress
    {
        private readonly Random _rnd = new(19021999);
        private readonly ISaveDataContainer _save;
        private readonly string[] _levelOrder;

        private const string LastLevelNumberKey = "LastLevelNumber";
        private const string LastLevelCountKey = "LastLevelCount";
        private const string RandomLevelsKey = "RandomLevels";

        public List<int> RandomLevels;

        private LevelData _currentLevelData;
        
        public LevelsLoopProgress(ISaveDataContainer save, string[] levelOrder)
        {
            this._save = save;
            this._levelOrder = levelOrder;
        }

        public void Load()
        {
            RandomLevels = _save.GetValue(RandomLevelsKey, new List<int>());
            for (int i = 0; i < RandomLevels.Count; i++)
            {
                _rnd.Next(_levelOrder.Length);
            }
        }

        public LevelData GetNextLevelData()
        {
            int playedTotal = _save.GetValue<int>(LastLevelNumberKey);
            return GetLevelData(playedTotal, true);
        }

        public LevelData GetRetryLevelData()
        {
            int playedTotal = _save.GetValue<int>(LastLevelNumberKey) - 1;
            return GetLevelData(playedTotal, false);
        }

        public void IncLevel()
        {
            if (_currentLevelData.IsUnique)
            {
                int playedTotal = _save.GetValue<int>(LastLevelNumberKey);
                _save.SaveValue(LastLevelNumberKey, playedTotal + 1);
            }
        }

        public void Reset()
        {
            _save.ResetKey(LastLevelCountKey);
            _save.ResetKey(LastLevelNumberKey);
            _save.ResetKey(RandomLevelsKey);
        }
        
        private LevelData GetLevelData(int playedUnique, bool isUnique)
        {
            int levelCount = _save.GetValue<int>(LastLevelCountKey);
            _save.SaveValue(LastLevelCountKey, levelCount);
            
            int lvlLoop = playedUnique / _levelOrder.Length;

            string lvlId = GetLevelName(playedUnique);

            _currentLevelData = new LevelData
            {
                Name = lvlId,
                UniqueLevelNumber = playedUnique,
                LevelCount = levelCount,
                LvlLoop = lvlLoop,
                IsUnique = isUnique
            };
            return _currentLevelData;
        }

        private string GetLevelName(int playedUniqueTotal)
        {
            if (playedUniqueTotal >= _levelOrder.Length)
            {
                int randomLvl = playedUniqueTotal - _levelOrder.Length;
                if (randomLvl >= RandomLevels.Count)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        RandomLevels.Add(_rnd.Next(_levelOrder.Length));
                        _save.SaveValue(RandomLevelsKey, RandomLevels);
                    }
                }

                return _levelOrder[RandomLevels[randomLvl]];
            }
            else
            {
                return _levelOrder[playedUniqueTotal % _levelOrder.Length];
            }
        }
    }
}

public class LevelData
{
    public string Name = string.Empty;
    public int UniqueLevelNumber;
    public int LevelCount;
    public int LvlLoop;
    public int IsRandom;
    public Difficulty Difficulty;
    public bool IsUnique;
}