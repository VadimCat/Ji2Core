using System;
using Ji2.CommonCore;
using UnityEngine;

namespace Ji2Core.Core.UserInput
{
    public class MouseInput : IUpdatable, IDisposable
    {
        private readonly UpdateService updateService;
        public event Action<Vector2> PointerMoveScreenSpace;
        public event Action<Vector2> PointerDown;
        public event Action<Vector2> PointerUp;

        public Vector2 lastPos;
        
        private bool isEnabled;
        
        public MouseInput(UpdateService updateService)
        {
            this.updateService = updateService;

            updateService.Add(this);
        }

        public void OnUpdate()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                PointerDown?.Invoke(UnityEngine.Input.mousePosition);
                isEnabled = true;
            }
            else if (isEnabled && UnityEngine.Input.GetMouseButton(0))
            {
                PointerMoveScreenSpace?.Invoke(UnityEngine.Input.mousePosition);
            }
            else if (isEnabled && UnityEngine.Input.GetMouseButtonUp(0))
            {
                lastPos = UnityEngine.Input.mousePosition;
                PointerUp?.Invoke(lastPos);
                isEnabled = false;
            }
        }

        public void Dispose()
        {
            updateService.Remove(this);
            PointerMoveScreenSpace = null;
            PointerDown = null;
            PointerUp = null;
        }
    }
}