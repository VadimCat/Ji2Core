using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ji2Core.Core.UserInput
{
    public class DragInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private float deadZone;

        private float _beginTime;
        private Vector2 _beginPosition;

        public bool Enabled { get; set; } = true;

        public event Action<Vector2> OnDragBegin;
        public event Action<Vector2> OnDragging;
        public event Action<Vector2> OnDragEnd;
        public event Action<Vector2, float> OnSwipe;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!Enabled)
                return;
            
            _beginTime = Time.timeSinceLevelLoad;
            _beginPosition = eventData.position;
            
            OnDragBegin?.Invoke(_beginPosition);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!Enabled)
                return;
            
            OnDragging?.Invoke(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!Enabled)
                return;
            
            OnDragEnd?.Invoke(eventData.position);
            
            Vector2 swipe = eventData.position - _beginPosition;

            if (swipe.magnitude < deadZone)
                return;

            OnSwipe?.Invoke(swipe, Time.timeSinceLevelLoad - _beginTime);
        }
    }
}