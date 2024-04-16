using System;
using Ji2Core.UI.Background;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Background
{
    public class BackgroundService : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Image backRoot;
        [SerializeField] private Sprite loadingBack;

        [SerializeField] private Sprite[] levelBackgroundImages;

        private BackgroundsList _backgrounds;
        private Sprite _currentBackground;


        private void Awake()
        {
            _backgrounds = new BackgroundsList(levelBackgroundImages);
            SceneManager.activeSceneChanged += HandleSceneChanged;
        }

        private void HandleSceneChanged(Scene arg0, Scene arg1)
        {
            //TODO: REMOVE EAT SOME SHIT
            canvas.worldCamera = FindObjectOfType<Camera>();
            var pos = transform.position;
            pos.z = -1;
            transform.position = pos;
        }

        public void SwitchBackground(Sprite sprite)
        {
            backRoot.sprite = _currentBackground = sprite;
        }
        
        public void SwitchBackground(Background background)
        {
            switch (background)
            {
                case Background.Loading:
                    backRoot.sprite = loadingBack;
                    break;
                case Background.Game:
                    _currentBackground = _backgrounds.GetNext();
                    backRoot.sprite = _currentBackground;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(background), background, null);
            }
        }
        
        public enum Background
        {
            Loading,
            Game
        }
    }
}