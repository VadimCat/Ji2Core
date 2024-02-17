using System.Collections.Generic;
using Ji2Core.Plugins.AppMetrica;

namespace Ji2.Analytics
{
    public class YandexMetricaLogger : IAnalyticsLogger
    {
        private readonly IYandexAppMetrica _appMetrica;

        public YandexMetricaLogger(IYandexAppMetrica appMetrica)
        {
            this._appMetrica = appMetrica;
        }
        
        public void LogEvent(string eventName)
        {
            _appMetrica.ReportEvent(eventName);
        }

        public void LogEvent(string eventName, IDictionary<string, object> data)
        {
            _appMetrica.ReportEvent(eventName, data);
        }

        public void LogEvent(string eventName, string json)
        {
            _appMetrica.ReportEvent(eventName, json);
        }

        public void ForceSend()
        {
            _appMetrica.SendEventsBuffer();
            
        }
    }
}