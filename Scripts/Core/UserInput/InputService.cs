using System;
using UnityEngine;

namespace Ji2Core.Core.UserInput
{
    public class InputService : IUpdatable, IDisposable
    {
        private readonly UpdateService updateService;
        public event Action<Vector2> PointerMoveScreenSpace;
        public event Action<Vector2> PointerDown;
        public event Action<Vector2> PointerUp;

        public Vector2 lastPos;
        
        private bool isEnabled;
        
        public InputService(UpdateService updateService)
        {
            this.updateService = updateService;

            updateService.Add(this);
        }

        public void OnUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PointerDown?.Invoke(Input.mousePosition);
                isEnabled = true;
            }
            else if (isEnabled && Input.GetMouseButton(0))
            {
                PointerMoveScreenSpace?.Invoke(Input.mousePosition);
            }
            else if (isEnabled && Input.GetMouseButtonUp(0))
            {
                lastPos = Input.mousePosition;
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