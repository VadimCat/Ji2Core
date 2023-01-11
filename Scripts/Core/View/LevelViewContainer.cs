using Ji2Core.Core;
using UnityEngine;
using Utils;

namespace SceneView
{
    public class LevelViewContainer : MonoBehaviour, IUpdatable
    {
        public ReactiveProperty<float> Progress => progress;

        private UpdateService updateService;
        private ColoringLevelView levelView;

        private readonly ReactiveProperty<float> progress = new();

        public void SetDependencies(UpdateService updateService)
        {
            this.updateService = updateService;
        }

        public void SetColoringData(ColoringLevelView dirtView)
        {
            levelView = Instantiate(dirtView, transform);
        }

        public void EnableProgressUpdate(bool enable)
        {
            if (enable)
            {
                updateService.Add(this);
            }
            else
            {
                updateService.Remove(this);
            }
        }

        public void RemoveColoringObject()
        {
            Destroy(levelView.gameObject);
        }

        public void OnUpdate()
        {
            progress.Value = levelView.ProgressRatio;
        }
    }
}