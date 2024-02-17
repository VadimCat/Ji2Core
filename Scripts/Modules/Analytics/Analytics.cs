using System;
using System.Collections.Generic;

namespace Ji2.Analytics
{
    public class Analytics : IAnalytics
    {
        private readonly Dictionary<Type, IAnalyticsLogger> _loggers = new();

        public void AddLogger(IAnalyticsLogger analyticsLogger)
        {
            if(analyticsLogger == this)
                return;
            
            _loggers[analyticsLogger.GetType()] = analyticsLogger;
        }

        public void LogEvent(string eventName)
        {
            foreach (var key in _loggers.Keys)
            {
                _loggers[key].LogEvent(eventName);
            }
        }

        public void LogEventDirectlyTo<TAnalyticsLogger>(string eventName)
        {
            _loggers[typeof(TAnalyticsLogger)].LogEvent(eventName);
        }

        public void LogEvent(string eventName, IDictionary<string, object> data)
        {
            foreach (var key in _loggers.Keys)
            {
                _loggers[key].LogEvent(eventName, data);
            }
        }

        public void LogEventDirectlyTo<TAnalyticsLogger>(string eventName, Dictionary<string, object> data)
        {
            _loggers[typeof(TAnalyticsLogger)].LogEvent(eventName, data);
        }

        public void LogEvent(string eventName, string json)
        {
            foreach (var key in _loggers.Keys)
            {
                _loggers[key].LogEvent(eventName, json);
            }
        }

        public void LogEventDirectlyTo<TAnalyticsLogger>(string eventName, string json)
        {
            _loggers[typeof(TAnalyticsLogger)].LogEvent(eventName, json);
        }
        
        public void ForceSend()
        {
            foreach (var key in _loggers.Keys)
            {
                _loggers[key].ForceSend();
            }
        }

        public void ForceSendDirectlyTo<TAnalyticsLogger>()
        {
            _loggers[typeof(TAnalyticsLogger)].ForceSend();
        }
    }

    public interface IAnalytics : IAnalyticsLogger
    {
        public void AddLogger(IAnalyticsLogger analyticsLogger);
        public void LogEventDirectlyTo<TAnalyticsLogger>(string eventName);
        public void LogEventDirectlyTo<TAnalyticsLogger>(string eventName, Dictionary<string, object> data);
        public void ForceSendDirectlyTo<TAnalyticsLogger>();
    }
    
    public enum LevelExitType
    {
        // ReSharper disable once InconsistentNaming
        win,

        // ReSharper disable once InconsistentNaming
        game_closed
    }
}