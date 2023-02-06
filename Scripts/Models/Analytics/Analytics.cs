using System;
using System.Collections.Generic;

namespace Ji2.Models.Analytics
{
    public class Analytics : IAnalyticsLogger
    {
        private readonly Dictionary<Type, IAnalyticsLogger> loggers = new();

        public void AddLogger(IAnalyticsLogger analyticsLogger)
        {
            loggers[analyticsLogger.GetType()] = analyticsLogger;
        }

        public void LogEvent(string eventName)
        {
            foreach (var key in loggers.Keys)
            {
                loggers[key].LogEvent(eventName);
            }
        }

        public void LogEventDirectlyTo<TAnalyticsLogger>(string eventName)
        {
            loggers[typeof(TAnalyticsLogger)].LogEvent(eventName);
        }

        public void LogEvent(string eventName, IDictionary<string, object> data)
        {
            foreach (var key in loggers.Keys)
            {
                loggers[key].LogEvent(eventName, data);
            }
        }

        public void LogEventDirectlyTo<TAnalyticsLogger>(string eventName, Dictionary<string, object> data)
        {
            loggers[typeof(TAnalyticsLogger)].LogEvent(eventName, data);
        }

        public void LogEvent(string eventName, string json)
        {
            foreach (var key in loggers.Keys)
            {
                loggers[key].LogEvent(eventName, json);
            }
        }

        public void LogEventDirectlyTo<TAnalyticsLogger>(string eventName, string json)
        {
            loggers[typeof(TAnalyticsLogger)].LogEvent(eventName, json);
        }
        
        public void ForceSend()
        {
            foreach (var key in loggers.Keys)
            {
                loggers[key].ForceSend();
            }
        }

        public void ForceSendDirectlyTo<TAnalyticsLogger>()
        {
            loggers[typeof(TAnalyticsLogger)].ForceSend();
        }
    }

    public enum LevelExitType
    {
        // ReSharper disable once InconsistentNaming
        win,

        // ReSharper disable once InconsistentNaming
        game_closed
    }
}