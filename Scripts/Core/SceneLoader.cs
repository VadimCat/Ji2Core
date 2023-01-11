using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ji2Core.Core
{
    public class SceneLoader : IUpdatable
    {
        private readonly UpdateService updateService;
        public event Action<float> OnProgressUpdate;
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
            var scene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(scene);
            currentLoadingOperation = null;
            updateService.Remove(this);
        } 
        
        public void OnUpdate()
        {            
            OnProgressUpdate?.Invoke(currentLoadingOperation.progress);
        }
    }
}