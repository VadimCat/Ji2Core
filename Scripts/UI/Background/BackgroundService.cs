using System;
using Ji2Core.UI.Background;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Client.UI.Screens
{
    public class BackgroundService : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Image backRoot;
        [SerializeField] private Sprite loadingBack;

        [SerializeField] private Sprite[] levelBackgroundImages;

        private BackgroundsList backgrounds;
        private Sprite currentBackground;


        private void Awake()
        {
            backgrounds = new BackgroundsList(levelBackgroundImages);
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

        public void SwitchBackground(Background background)
        {
            switch (background)
            {
                case Background.Loading:
                    backRoot.sprite = loadingBack;
                    break;
                case Background.Game:
                    currentBackground = backgrounds.GetNext();
                    backRoot.sprite = currentBackground;
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