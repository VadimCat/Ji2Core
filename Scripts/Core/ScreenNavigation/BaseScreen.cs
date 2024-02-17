using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;

namespace Ji2.ScreenNavigation
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