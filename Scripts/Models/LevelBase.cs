using System;
using System.Collections.Generic;
using Ji2.CommonCore.SaveDataContainer;
using Ji2.Models.Analytics;
using UnityEngine;

namespace Ji2.Models
{
    public abstract class LevelBase
    {
        public event Action EventLevelCompled;
        private readonly Analytics.IAnalytics analytics;
        private readonly LevelData levelData;
        protected readonly ISaveDataContainer saveDataContainer;
        private float playTime = 0;

        public string Name => levelData.name;
        public int LevelCount => levelData.uniqueLevelNumber;
        public Difficulty Difficulty => levelData.difficulty;
        
        protected LevelBase(Analytics.IAnalytics analytics, LevelData levelData, ISaveDataContainer saveDataContainer)
        {
            this.analytics = analytics;
            this.levelData = levelData;
            this.saveDataContainer = saveDataContainer;
            
            CheckPlayTimeForAnalytics();
        }

        private void CheckPlayTimeForAnalytics()
        {
            playTime = saveDataContainer.GetValue<float>(Name);
            if (!Mathf.Approximately(0, playTime))
            {
                LogAnalyticsLevelFinish(LevelExitType.game_closed);
                playTime = 0;
                saveDataContainer.ResetKey(Name);
            }
        }
        
        public void AppendPlayTime(float time)
        {
            playTime += time;
            saveDataContainer.SaveValue(Name, time);
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
                [Constants.TimeKey] = (int)playTime,
            };

            analytics.LogEventDirectlyTo<YandexMetricaLogger>(Constants.FinishEvent, eventData);
            analytics.ForceSendDirectlyTo<YandexMetricaLogger>();
        }

        protected virtual void Complete()
        {
            EventLevelCompled?.Invoke();
        }
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