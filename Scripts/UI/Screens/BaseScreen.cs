using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ji2Core.UI.Screens
{
    public abstract class BaseScreen : SerializedMonoBehaviour
    {
        public virtual UniTask AnimateShow()
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask AnimateClose()
        {
            return UniTask.CompletedTask;
        }
    }
}