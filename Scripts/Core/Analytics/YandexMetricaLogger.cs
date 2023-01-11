using System.Collections.Generic;
// using Ji2Core.Plugins.AppMetrica;

namespace Ji2Core.Core.Analytics
{
    public class YandexMetricaLogger : IAnalyticsLogger
    {
        // private readonly IYandexAppMetrica appMetrica;

        // public YandexMetricaLogger(IYandexAppMetrica appMetrica)
        // {
        //     this.appMetrica = appMetrica;
        // }
        
        public void LogEvent(string eventName)
        {
            // appMetrica.ReportEvent(eventName);
        }

        public void LogEvent(string eventName, IDictionary<string, object> data)
        {
            // appMetrica.ReportEvent(eventName, data);
        }

        public void LogEvent(string eventName, string json)
        {
            // appMetrica.ReportEvent(eventName, json);
        }

        public void ForceSend()
        {
            // appMetrica.SendEventsBuffer();
        }
    }
}