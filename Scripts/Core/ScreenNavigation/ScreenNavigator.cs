using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Ji2Core.UI.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ji2Core.Core.ScreenNavigation
{
    public class ScreenNavigator : MonoBehaviour, IBootstrapable
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private List<BaseScreen> screens;

        private Dictionary<Type, BaseScreen> screenOrigins;

        private BaseScreen currentScreen;
        public BaseScreen CurrentScreen => currentScreen;

        public void Bootstrap()
        {
            SceneManager.sceneLoaded += SetCamera;
            
            screenOrigins = new Dictionary<Type, BaseScreen>();
            foreach (var screen in screens)
            {
                screenOrigins[screen.GetType()] = screen;
            }
        }

        private void SetCamera(Scene arg0, LoadSceneMode arg1)
        {
            canvas.worldCamera = Camera.main;
        }
        
        public async Task<BaseScreen> PushScreen(Type type)
        {
            if (currentScreen != null)
            {
                await CloseCurrent();
            }

            currentScreen = Instantiate(screenOrigins[type], transform);
            await currentScreen.AnimateShow();
            return currentScreen;    
        }
        
        public async UniTask<TScreen> PushScreen<TScreen>() where TScreen : BaseScreen
        {
            if (currentScreen != null)
            {
                await CloseCurrent();
            }

            if (currentScreen is TScreen screen)
            {
                return screen;
            }

            currentScreen = Instantiate(screenOrigins[typeof(TScreen)], transform);
            await currentScreen.AnimateShow();
            return (TScreen)currentScreen;
        }

        public async UniTask CloseScreen<TScreen>() where TScreen : BaseScreen
        {
            if (currentScreen is TScreen)
            {
                await currentScreen.AnimateClose();
                Destroy(currentScreen.gameObject);
                currentScreen = null;
            }
        }

        private async UniTask CloseCurrent()
        {
            await currentScreen.AnimateClose();
            Destroy(currentScreen.gameObject);
            currentScreen = null;
        }
    }
}