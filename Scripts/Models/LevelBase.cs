using System;
using Ji2.CommonCore.SaveDataContainer;
using UnityEngine;

namespace Ji2.Models
{
    public class LevelBase: ILevel
    {
        private const float SaveDeltaTime = 5;
        public event Action EventLevelCompleted;

        private readonly ISaveDataContainer _saveDataContainer;
        public ProgressBase Progress { get; set; }
        private float _lastSaveTime;
        
        public string Name => LevelData.Name;
        public int LevelCount => LevelData.UniqueLevelNumber;
        public Difficulty Difficulty => LevelData.Difficulty;
        public LevelData LevelData { get; }

        public LevelBase(LevelData levelData, ISaveDataContainer saveDataContainer)
        {
            LevelData = levelData;
            _saveDataContainer = saveDataContainer;
        }

        public void AppendPlayTime(float time)
        {
            Progress.playTime += time;
            if (_lastSaveTime + SaveDeltaTime < Progress.playTime)
            {
                SaveProgress();
            }
        }

        public void LoadProgress(ProgressBase progress)
        {
            Progress = progress;
        }

        public void CreateProgress()
        {
            Progress = new ProgressBase();
        }

        private void SaveProgress()
        {
            _lastSaveTime = Progress.playTime;
            _saveDataContainer.SaveValue(Name, Progress);
        }

        public virtual void Start()
        {
        }
        
        public virtual void Complete()
        {
            EventLevelCompleted?.Invoke();
        }
    }

    [Serializable]
    public class ProgressBase
    {
        public virtual bool LogFinishEventOnLoad => false;
        public float playTime;
    }

    public enum Difficulty
    {
        Easy,
        Normal,
        Hard,
        Harder,
        Insane
    }
}