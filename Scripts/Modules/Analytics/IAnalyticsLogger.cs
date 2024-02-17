using System.Collections.Generic;

namespace Ji2.Analytics
{
    public interface IAnalyticsLogger
    {
        public void LogEvent(string eventName);

        public void LogEvent(string eventName, IDictionary<string, object> data);

        public void LogEvent(string eventName, string json);

        public void ForceSend();
    }
}