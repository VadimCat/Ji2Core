using UnityEngine;
using UnityEngine.UI;

namespace Ji2Core.Core.UserInput
{
    public class Joystick : MonoBehaviour, IUpdatable
    {
        [SerializeField] private Image thumb;
        [SerializeField] private float movementAreaRadius = 75f;
        [SerializeField] private float deadZoneRadius;
        [SerializeField] private bool isDynamicJoystick = false;
        [SerializeField] private RectTransform dynamicJoystickMovementArea;
        [SerializeField] private bool canFollowPointer = false;

        private RectTransform joystickTransform;
        private Graphic background;

        private RectTransform thumbTransform;

        private bool joystickHeld = false;
        private Vector2 pointerInitialPos;

        private float overMovementAreaRadius;
        private float movementAreaRadiusSqr;
        private float deadZoneRadiusSqr;

        private Vector2 joystickInitialPos;

        private float opacity = 1f;

        private Vector2 axis = Vector2.zero;
        private CameraProvider cameraProvider;
        private UpdateService updateService;

        public Vector2 Value => axis;

        public void SetDependencies(CameraProvider cameraProvider, UpdateService updateService)
        {
            this.cameraProvider = cameraProvider;
            this.updateService = updateService;

            updateService.Add(this);
        }

        public void OnPointerDown(Vector2 inputPos)
        {
            joystickHeld = true;

            if (isDynamicJoystick)
            {
                pointerInitialPos = Vector2.zero;

                RectTransformUtility.ScreenPointToWorldPointInRectangle(dynamicJoystickMovementArea, inputPos,
                    cameraProvider.MainCamera, out var joystickPos);
                joystickTransform.position = joystickPos;
            }
            else
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickTransform, inputPos,
                    cameraProvider.MainCamera, out pointerInitialPos);
            }
        }

        public void OnPointerMove(Vector2 inputPos)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickTransform, inputPos,
                cameraProvider.MainCamera, out var pointerPos);

            Vector2 direction = pointerPos - pointerInitialPos;
            // if (movementAxes == MovementAxes.X)
            //     direction.y = 0f;
            // else if (movementAxes == MovementAxes.Y)
            //     direction.x = 0f;

            if (direction.sqrMagnitude <= deadZoneRadiusSqr)
            {
                axis.Set(0f, 0f);
            }
            else
            {
                if (direction.sqrMagnitude > movementAreaRadiusSqr)
                {
                    Vector2 directionNormalized = direction.normalized * movementAreaRadius;
                    if (canFollowPointer)
                        joystickTransform.localPosition += (Vector3)(direction - directionNormalized);

                    direction = directionNormalized;
                }

                axis = direction * overMovementAreaRadius;
            }

            thumbTransform.localPosition = direction;
        }

        public void OnPointerUp(Vector2 inputPos)
        {
            joystickHeld = false;
            axis = Vector2.zero;

            thumbTransform.localPosition = Vector3.zero;
            if (!isDynamicJoystick && canFollowPointer)
                joystickTransform.anchoredPosition = joystickInitialPos;
        }

        public void OnUpdate()
        {
            if (!isDynamicJoystick)
                return;

            if (joystickHeld)
                opacity = Mathf.Min(1f, opacity + Time.unscaledDeltaTime * 4f);
            else
                opacity = Mathf.Max(0f, opacity - Time.unscaledDeltaTime * 4f);

            Color c = thumb.color;
            c.a = opacity;
            thumb.color = c;

            if (background != null)
            {
                c = background.color;
                c.a = opacity;
                background.color = c;
            }
        }

        private void Awake()
        {
            joystickTransform = (RectTransform)transform;
            thumbTransform = thumb.rectTransform;
            background = GetComponent<Graphic>();

            if (isDynamicJoystick)
            {
                opacity = 0f;
                thumb.raycastTarget = false;
                if (background)
                    background.raycastTarget = false;

                OnUpdate();
            }
            else
            {
                thumb.raycastTarget = true;
                if (background)
                    background.raycastTarget = true;
            }

            overMovementAreaRadius = 1f / movementAreaRadius;
            movementAreaRadiusSqr = movementAreaRadius * movementAreaRadius;
            deadZoneRadiusSqr = deadZoneRadius * deadZoneRadius;

            joystickInitialPos = joystickTransform.anchoredPosition;
            thumbTransform.localPosition = Vector3.zero;


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
            updateService.Remove(this);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            overMovementAreaRadius = 1f / movementAreaRadius;
            movementAreaRadiusSqr = movementAreaRadius * movementAreaRadius;
            deadZoneRadiusSqr = deadZoneRadius * deadZoneRadius;
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