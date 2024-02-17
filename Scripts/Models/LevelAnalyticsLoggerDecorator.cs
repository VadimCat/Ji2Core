using System;
using System.Collections.Generic;
using Ji2.CommonCore.SaveDataContainer;
using Ji2.Models.Analytics;

namespace Ji2.Models
{
    public class LevelAnalyticsLoggerDecorator : ILevel 
    {
        private readonly IAnalytics _analytics;
        private readonly ISaveDataContainer _saveDataContainer;
        private readonly ILevel _level;

        public string Name => _level.Name;
        public int LevelCount => _level.LevelCount;
        public Difficulty Difficulty => _level.Difficulty;
        public LevelData LevelData => _level.LevelData;
        public ProgressBase Progress => _level.Progress;
        
        public event Action EventLevelCompleted
        {
            add => _level.EventLevelCompleted += value;
            remove => _level.EventLevelCompleted -= value;
        }

        public LevelAnalyticsLoggerDecorator(IAnalytics analytics, ISaveDataContainer saveDataContainer, ILevel level)
        {
            _analytics = analytics;
            _saveDataContainer = saveDataContainer;
            _level = level;
            
            CheckSave();
        }
        
        public void AppendPlayTime(float time)
        {
            _level.AppendPlayTime(time);
        }
        
        public void Start()
        {
            _level.Start();
            LogAnalyticsLevelStart();
        }

        public void LoadProgress(ProgressBase progress)
        {
            _level.LoadProgress(progress);
        }
        
        public void CreateProgress()
        {
            _level.CreateProgress();
        }
        
        public void Complete()
        {
            _level.Complete();
            _saveDataContainer.ResetKey(Name);
            LogAnalyticsLevelFinish();
        }

        private void LogAnalyticsLevelStart()
        {
            var eventData = new Dictionary<string, object>
            {
                [Constants.LevelNumberKey] = LevelData.UniqueLevelNumber,
                [Constants.LevelNameKey] = LevelData.Name,
                [Constants.LevelCountKey] = LevelData.LevelCount,
                [Constants.LevelLoopKey] = LevelData.LvlLoop,
                [Constants.LevelRandomKey] = LevelData.IsRandom,
                [Constants.DifficultyKey] = LevelData.Difficulty
            };
            _analytics.LogEventDirectlyTo<YandexMetricaLogger>(Constants.StartEvent, eventData);
            _analytics.ForceSendDirectlyTo<YandexMetricaLogger>();
        }

        private void LogAnalyticsLevelFinish(LevelExitType levelExitType = LevelExitType.win)
        {
            var eventData = new Dictionary<string, object>
            {
                [Constants.LevelNumberKey] = LevelData.UniqueLevelNumber,
                [Constants.LevelNameKey] = LevelData.Name,
                [Constants.LevelCountKey] = LevelData.LevelCount,
                [Constants.LevelLoopKey] = LevelData.LvlLoop,
                [Constants.LevelRandomKey] = LevelData.IsRandom,
                [Constants.DifficultyKey] = LevelData.Difficulty,
                [Constants.ResultKey] = (levelExitType).ToString(),
                [Constants.TimeKey] = (int)_level.Progress.playTime,
            };

            _analytics.LogEventDirectlyTo<YandexMetricaLogger>(Constants.FinishEvent, eventData);
            _analytics.ForceSendDirectlyTo<YandexMetricaLogger>();
        }
        
        private void CheckSave()
        {
            var progress = _saveDataContainer.GetValue<ProgressBase>(Name);

            if (progress == null)
            {
                _level.CreateProgress();
                return;
            }

            if (progress.LogFinishEventOnLoad)
            {
                LogAnalyticsLevelFinish(LevelExitType.game_closed);
                progress.playTime = 0;
                _saveDataContainer.ResetKey(Name);
                _level.CreateProgress();
            }
            else
            {
                _level.LoadProgress(progress);
            }
        }
    }
}