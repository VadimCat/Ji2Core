using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Ji2.ScreenNavigation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ji2Core.Core.ScreenNavigation
{
    public class ScreenNavigator : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private List<BaseScreen> screens;
        [SerializeField] private RectTransform transform;
        [SerializeField] private CanvasScaler scaler;

        private Dictionary<Type, BaseScreen> _screenOrigins;

        private BaseScreen _currentScreen;

        public BaseScreen CurrentScreen => _currentScreen;
        public Vector2 Size => new(transform.rect.width, transform.rect.height);
        public float ScaleFactor => transform.rect.height / scaler.referenceResolution.y;

        public void Bootstrap()
        {
            SceneManager.sceneLoaded += SetCamera;

            _screenOrigins = new Dictionary<Type, BaseScreen>();
            foreach (var screen in screens)
            {
                _screenOrigins[screen.GetType()] = screen;
            }
        }

        private void SetCamera(Scene arg0, LoadSceneMode arg1)
        {
            canvas.worldCamera = Camera.main;
        }

        public async Task<BaseScreen> PushScreen(Type type)
        {
            if (_currentScreen != null)
            {
                await CloseCurrent();
            }

            _currentScreen = Instantiate(_screenOrigins[type], transform);
            await _currentScreen.AnimateShow();
            return _currentScreen;
        }

        public async UniTask<TScreen> PushScreen<TScreen>() where TScreen : BaseScreen
        {
            if (_currentScreen != null)
            {
                await CloseCurrent();
            }

            if (_currentScreen is TScreen screen)
            {
                return screen;
            }

            _currentScreen = Instantiate(_screenOrigins[typeof(TScreen)], transform);
            await _currentScreen.AnimateShow();
            return (TScreen)_currentScreen;
        }

        public async UniTask CloseScreen<TScreen>() where TScreen : BaseScreen
        {
            if (_currentScreen is TScreen)
            {
                await _currentScreen.AnimateClose();
                Destroy(_currentScreen.gameObject);
                _currentScreen = null;
            }
        }

        private async UniTask CloseCurrent()
        {
            await _currentScreen.AnimateClose();
            Destroy(_currentScreen.gameObject);
            _currentScreen = null;
        }
    }
}