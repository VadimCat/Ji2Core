using System;

namespace Ji2.Models
{
    public interface ILevel
    {
        public void Complete();
        public void Start();
        public ProgressBase Progress { get; }
        public event Action EventLevelCompleted;
        public string Name { get; }
        public int LevelCount { get; }
        public Difficulty Difficulty { get; }
        public LevelData LevelData { get; }
        public void AppendPlayTime(float time);
        public void LoadProgress(ProgressBase progress);
        public void CreateProgress();
    }
}