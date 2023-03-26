using System;
using System.Collections.Generic;
using Ji2.CommonCore.SaveDataContainer;
using Ji2.Models.Analytics;
using UnityEngine;

namespace Ji2.Models
{
    public abstract class LevelBase<TProgress> where TProgress : ProgressBase, new()
    {
        private const float SaveDeltaTime = 5;

        public event Action EventLevelCompleted;

        protected readonly ISaveDataContainer saveDataContainer;
        protected TProgress progress;
        private readonly IAnalytics analytics;
        private readonly LevelData levelData;
        private ProgressBase progressBase;
        private float lastSaveTime;

        public string Name => levelData.name;
        public int LevelCount => levelData.uniqueLevelNumber;
        public Difficulty Difficulty => levelData.difficulty;

        protected LevelBase(IAnalytics analytics, LevelData levelData, ISaveDataContainer saveDataContainer)
        {
            this.analytics = analytics;
            this.levelData = levelData;
            this.saveDataContainer = saveDataContainer;

            CheckSave();
        }

        private void CheckSave()
        {
            var progress = saveDataContainer.GetValue<TProgress>(Name);

            if (progress == null)
            {
                this.progress = CreateProgress();
                return;
            }

            if (progress.LogFinishEventOnLoad)
            {
                LogAnalyticsLevelFinish(LevelExitType.game_closed);
                progress.playTime = 0;
                saveDataContainer.ResetKey(Name);
                this.progress = CreateProgress();
            }
            else
            {
                this.progress = progress;
            }
        }

        protected virtual TProgress CreateProgress()
        {
            return new TProgress();
        }

        public void AppendPlayTime(float time)
        {
            progress.playTime += time;
            if (lastSaveTime + 5 < progress.playTime)
            {
                SaveProgress();
            }
        }

        protected void SaveProgress()
        {
            lastSaveTime = progress.playTime;
            saveDataContainer.SaveValue(Name, progress);
        }

        public void LogAnalyticsLevelStart()
        {
            var eventData = new Dictionary<string, object>
            {
                [Constants.LevelNumberKey] = levelData.uniqueLevelNumber,
                [Constants.LevelNameKey] = levelData.name,
                [Constants.LevelCountKey] = levelData.levelCount,
                [Constants.LevelLoopKey] = levelData.lvlLoop,
                [Constants.LevelRandomKey] = levelData.isRandom,
                [Constants.DifficultyKey] = levelData.difficulty
            };
            analytics.LogEventDirectlyTo<YandexMetricaLogger>(Constants.StartEvent, eventData);
            analytics.ForceSendDirectlyTo<YandexMetricaLogger>();
        }

        public void LogAnalyticsLevelFinish(LevelExitType levelExitType = LevelExitType.win)
        {
            var eventData = new Dictionary<string, object>
            {
                [Constants.LevelNumberKey] = levelData.uniqueLevelNumber,
                [Constants.LevelNameKey] = levelData.name,
                [Constants.LevelCountKey] = levelData.levelCount,
                [Constants.LevelLoopKey] = levelData.lvlLoop,
                [Constants.LevelRandomKey] = levelData.isRandom,
                [Constants.DifficultyKey] = levelData.difficulty,
                [Constants.ResultKey] = (levelExitType).ToString(),
                [Constants.TimeKey] = (int)progress.playTime,
            };

            analytics.LogEventDirectlyTo<YandexMetricaLogger>(Constants.FinishEvent, eventData);
            analytics.ForceSendDirectlyTo<YandexMetricaLogger>();
        }

        protected virtual void Complete()
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
        easy,
        normal,
        hard,
        harder,
        insane
    }
}