using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ji2
{
    public class CameraProvider
    {
        private Camera _mainCamera;

        public Camera MainCamera => _mainCamera;

        public CameraProvider()
        {
            ChangeCamera();
            SceneManager.sceneLoaded += ChangeCamera;
        }
        
        private void ChangeCamera(Scene arg0, LoadSceneMode arg1)
        {
            ChangeCamera();
        }
        
        private void ChangeCamera()
        {
            _mainCamera = Camera.main;
        }
    }
}