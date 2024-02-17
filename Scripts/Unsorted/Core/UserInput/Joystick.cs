using Ji2.CommonCore;
using UnityEngine;
using UnityEngine.UI;

namespace Ji2.UserInput
{
    public class Joystick : MonoBehaviour, IUpdatable
    {
        [SerializeField] private Image thumb;
        [SerializeField] private float movementAreaRadius = 75f;
        [SerializeField] private float deadZoneRadius;
        [SerializeField] private bool isDynamicJoystick = false;
        [SerializeField] private RectTransform dynamicJoystickMovementArea;
        [SerializeField] private bool canFollowPointer = false;

        private RectTransform _joystickTransform;
        private Graphic _background;

        private RectTransform _thumbTransform;

        private bool _joystickHeld = false;
        private Vector2 _pointerInitialPos;

        private float _overMovementAreaRadius;
        private float _movementAreaRadiusSqr;
        private float _deadZoneRadiusSqr;

        private Vector2 _joystickInitialPos;

        private float _opacity = 1f;

        private Vector2 _axis = Vector2.zero;
        private CameraProvider _cameraProvider;
        private UpdateService _updateService;

        public Vector2 Value => _axis;

        public void SetDependencies(CameraProvider cameraProvider, UpdateService updateService)
        {
            this._cameraProvider = cameraProvider;
            this._updateService = updateService;

            updateService.Add(this);
        }

        public void OnPointerDown(Vector2 inputPos)
        {
            _joystickHeld = true;

            if (isDynamicJoystick)
            {
                _pointerInitialPos = Vector2.zero;

                RectTransformUtility.ScreenPointToWorldPointInRectangle(dynamicJoystickMovementArea, inputPos,
                    _cameraProvider.MainCamera, out var joystickPos);
                _joystickTransform.position = joystickPos;
            }
            else
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickTransform, inputPos,
                    _cameraProvider.MainCamera, out _pointerInitialPos);
            }
        }

        public void OnPointerMove(Vector2 inputPos)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickTransform, inputPos,
                _cameraProvider.MainCamera, out var pointerPos);

            Vector2 direction = pointerPos - _pointerInitialPos;
            // if (movementAxes == MovementAxes.X)
            //     direction.y = 0f;
            // else if (movementAxes == MovementAxes.Y)
            //     direction.x = 0f;

            if (direction.sqrMagnitude <= _deadZoneRadiusSqr)
            {
                _axis.Set(0f, 0f);
            }
            else
            {
                if (direction.sqrMagnitude > _movementAreaRadiusSqr)
                {
                    Vector2 directionNormalized = direction.normalized * movementAreaRadius;
                    if (canFollowPointer)
                        _joystickTransform.localPosition += (Vector3)(direction - directionNormalized);

                    direction = directionNormalized;
                }

                _axis = direction * _overMovementAreaRadius;
            }

            _thumbTransform.localPosition = direction;
        }

        public void OnPointerUp(Vector2 inputPos)
        {
            _joystickHeld = false;
            _axis = Vector2.zero;

            _thumbTransform.localPosition = Vector3.zero;
            if (!isDynamicJoystick && canFollowPointer)
                _joystickTransform.anchoredPosition = _joystickInitialPos;
        }

        public void OnUpdate()
        {
            if (!isDynamicJoystick)
                return;

            if (_joystickHeld)
                _opacity = Mathf.Min(1f, _opacity + Time.unscaledDeltaTime * 4f);
            else
                _opacity = Mathf.Max(0f, _opacity - Time.unscaledDeltaTime * 4f);

            Color c = thumb.color;
            c.a = _opacity;
            thumb.color = c;

            if (_background != null)
            {
                c = _background.color;
                c.a = _opacity;
                _background.color = c;
            }
        }

        private void Awake()
        {
            _joystickTransform = (RectTransform)transform;
            _thumbTransform = thumb.rectTransform;
            _background = GetComponent<Graphic>();

            if (isDynamicJoystick)
            {
                _opacity = 0f;
                thumb.raycastTarget = false;
                if (_background)
                    _background.raycastTarget = false;

                OnUpdate();
            }
            else
            {
                thumb.raycastTarget = true;
                if (_background)
                    _background.raycastTarget = true;
            }

            _overMovementAreaRadius = 1f / movementAreaRadius;
            _movementAreaRadiusSqr = movementAreaRadius * movementAreaRadius;
            _deadZoneRadiusSqr = deadZoneRadius * deadZoneRadius;

            _joystickInitialPos = _joystickTransform.anchoredPosition;
            _thumbTransform.localPosition = Vector3.zero;


            if (isDynamicJoystick && dynamicJoystickMovementArea != null)
            {
                dynamicJoystickMovementArea = new GameObject("Dynamic Joystick Movement Area", typeof(RectTransform))
                    .GetComponent<RectTransform>();

                dynamicJoystickMovementArea.SetParent(thumb.canvas.transform, false);
                dynamicJoystickMovementArea.SetAsFirstSibling();
                dynamicJoystickMovementArea.anchorMin = Vector2.zero;
                dynamicJoystickMovementArea.anchorMax = Vector2.one;
                dynamicJoystickMovementArea.sizeDelta = Vector2.zero;
                dynamicJoystickMovementArea.anchoredPosition = Vector2.zero;
            }
        }

        private void OnDestroy()
        {
            _updateService.Remove(this);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _overMovementAreaRadius = 1f / movementAreaRadius;
            _movementAreaRadiusSqr = movementAreaRadius * movementAreaRadius;
            _deadZoneRadiusSqr = deadZoneRadius * deadZoneRadius;
        }
#endif
    }

    //TODO: Use flags
    public enum MovementAxes
    {
        XY,
        X,
        Y
    };
}