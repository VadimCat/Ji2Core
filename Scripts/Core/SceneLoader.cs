using System;
using Cysharp.Threading.Tasks;
using Ji2.CommonCore;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ji2Core.Core
{
    public class SceneLoader : IUpdatable
    {
        public event Action<float> OnProgressUpdate;
        public event Action<Scene> SceneLoaded;
        
        private readonly UpdateService updateService;
        private AsyncOperation currentLoadingOperation;

        public SceneLoader(UpdateService updateService)
        {
            this.updateService = updateService;
        }
        
        public async UniTask LoadScene(string sceneName)
        {
            currentLoadingOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            updateService.Add(this);
            await currentLoadingOperation.ToUniTask();
            currentLoadingOperation = null;
            updateService.Remove(this);

            await UniTask.NextFrame(PlayerLoopTiming.PostLateUpdate);
            
            SceneLoaded?.Invoke(SceneManager.GetActiveScene());
        } 
        
        public void OnUpdate()
        {            
            OnProgressUpdate?.Invoke(currentLoadingOperation.progress);
        }
    }

    public static class ContextExtensions
    {
        public static SceneLoader SceneLoader(this Context context)
        {
            return context.GetService<SceneLoader>();
        }
    }
}