using System;
using Cysharp.Threading.Tasks;
using Ji2.CommonCore;
using Ji2.Context;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ji2
{
    public class SceneLoader : IUpdatable
    {
        public event Action<float> OnProgressUpdate;
        public event Action<Scene> SceneLoaded;
        
        private readonly UpdateService _updateService;
        private AsyncOperation _currentLoadingOperation;

        public SceneLoader(UpdateService updateService)
        {
            this._updateService = updateService;
        }
        
        public async UniTask LoadScene(string sceneName)
        {
            _currentLoadingOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            _updateService.Add(this);
            await _currentLoadingOperation.ToUniTask();
            _currentLoadingOperation = null;
            _updateService.Remove(this);

            await UniTask.NextFrame(PlayerLoopTiming.PostLateUpdate);
            
            SceneLoaded?.Invoke(SceneManager.GetActiveScene());
        } 
        
        public void OnUpdate()
        {            
            OnProgressUpdate?.Invoke(_currentLoadingOperation.progress);
        }
    }

    public static class ContextExtensions
    {
        public static SceneLoader SceneLoader(this DiContext diContext)
        {
            return diContext.Get<SceneLoader>();
        }
    }
}