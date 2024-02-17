using System.Linq;
using Ji2.CommonCore.SaveDataContainer;

namespace Ji2.Presenters.Tutorial
{
    public class TutorialService
    {
        private readonly ISaveDataContainer _saveDataContainer;

        private readonly ITutorialStep[] _steps;
        
        public TutorialService(ISaveDataContainer saveDataContainer, ITutorialStep[] steps)
        {
            this._saveDataContainer = saveDataContainer;
            this._steps = steps;
        }

        public void TryRunSteps()
        {
            foreach (var step in _steps)
            {
                if (!_saveDataContainer.GetValue<bool>(step.SaveKey))
                {
                    step.Run();
                    step.Completed += () => _saveDataContainer.SaveValue(step.SaveKey, true);
                }
            }
        }

        public bool CheckTutorialStep<TStep>() where TStep : ITutorialStep
        {
            var key =  _steps.First(st => st is TStep).SaveKey;
            return _saveDataContainer.GetValue<bool>(key);
        }
    }

    public interface ITutorialFactory
    {
        public ITutorialStep Create<TStep>() where TStep : ITutorialStep;
    }
}