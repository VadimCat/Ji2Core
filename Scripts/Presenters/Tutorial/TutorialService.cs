using System.Linq;
using Ji2.CommonCore.SaveDataContainer;

namespace Ji2.Presenters.Tutorial
{
    public class TutorialService
    {
        private readonly ISaveDataContainer saveDataContainer;

        private readonly ITutorialStep[] steps;
        
        public TutorialService(ISaveDataContainer saveDataContainer, ITutorialStep[] steps)
        {
            this.saveDataContainer = saveDataContainer;
            this.steps = steps;
        }

        public void TryRunSteps()
        {
            foreach (var step in steps)
            {
                if (!saveDataContainer.GetValue<bool>(step.SaveKey))
                {
                    step.Run();
                    step.Completed += () => saveDataContainer.SaveValue(step.SaveKey, true);
                }
            }
        }

        public bool CheckTutorialStep<TStep>() where TStep : ITutorialStep
        {
            var key =  steps.First(st => st is TStep).SaveKey;
            return saveDataContainer.GetValue<bool>(key);
        }
    }

    public interface ITutorialFactory
    {
        public ITutorialStep Create<TStep>() where TStep : ITutorialStep;
    }
}