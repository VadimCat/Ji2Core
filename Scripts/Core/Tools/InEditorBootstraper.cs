using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ji2Core.Core.Tools
{
    public class InEditorBootstraper : MonoBehaviour
    {
        private void Awake()
        {
            if (!FindObjectOfType<BootstraperBase>())
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}