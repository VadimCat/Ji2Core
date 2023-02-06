using System;

namespace Ji2.Presenters.Tutorial
{
    public interface ITutorialStep
    {
        public string SaveKey { get; }
        public void Run();
        public event Action Completed;
    }
}