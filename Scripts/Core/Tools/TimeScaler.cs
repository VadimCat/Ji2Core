using UnityEngine;

namespace Ji2Core.Core.Tools
{
    public class TimeScaler
    {
        private readonly float _defaultTimeScale;
        private readonly float _defaultFixedDeltaTime;

        public float TimeScale { get; private set; } = 1;
        public bool IsPaused { get; private set; } = false;
        
        public TimeScaler()
        {
            _defaultTimeScale = Time.timeScale;
            _defaultFixedDeltaTime = Time.fixedDeltaTime;
        }

        public void ScaleTime(float timeScale)
        {
            TimeScale = timeScale;

            if (IsPaused)
                return;
            
            ApplyScale();
        }

        public void ResetScale()
        {
            IsPaused = false;
            TimeScale = 1;
            ApplyScale();
        }

        public void Pause()
        {
            Time.timeScale = 0;
            Time.fixedDeltaTime = 0;
            IsPaused = true;
        }

        public void Confirm()
        {
            ApplyScale();
            IsPaused = false;
        }

        private void ApplyScale()
        {
            Time.timeScale = _defaultTimeScale * TimeScale;
            Time.fixedDeltaTime = _defaultFixedDeltaTime * TimeScale;
        }
    }
}