using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Ji2.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ji2.UI
{
    public class TutorialPointerView : MonoBehaviour
    {
        [SerializeField] private Image pointer;
        [SerializeField] private TMP_Text tooltip;

        private Camera _camera;

        public void SetCamera(Camera screenCamera)
        {
            _camera = screenCamera;
        }
        
        public async UniTask PlayClickAnimation(Vector3 pos, CancellationToken cancellationToken)
        {
            Transform pointerTransform = pointer.transform;
            
            pointerTransform.position = _camera.WorldToScreenPoint(pos);
            pointer.color = Color.white;
            pointerTransform.localScale = Vector3.one;
            await pointer.transform.DoPulseScale(.85f, .5f, gameObject)
                .AwaitForComplete(cancellationToken: cancellationToken);

            Hide();
        }

        public void Hide()
        {
            pointer.color = new Color(0, 0, 0, 0);
        }

        public void ShowTooltip()
        {
            tooltip.DOFade(1, .5f);
        }
        
        public void HideTooltip()
        {
            tooltip.DOFade(0, .5f);
        }
    }
}