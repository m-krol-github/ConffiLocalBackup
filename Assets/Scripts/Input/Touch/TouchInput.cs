using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Conffi.Input
{
    [DefaultExecutionOrder(-1)]
    public sealed class TouchInput : Singleton<TouchInput>
    {
        #region PUBLIC_EVENTS

        public delegate void StartTouch(Vector2 position, float time);
        public event StartTouch OnStartTouch;
        public delegate void EndTouch(Vector2 position, float time);
        public event EndTouch OnEndTouch;

        #endregion

        private Camera camera;
        private UserInput input;

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }

        private void Awake()
        {
            input = new UserInput();
            camera = Camera.main;
        }

        private void Start()
        {
            input.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
            input.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        }

        private void StartTouchPrimary(InputAction.CallbackContext context)
        {
            if (OnStartTouch != null) OnStartTouch(ReferenceToMainCamera.ScreenToWorld(camera, input.Touch.PrimaryTouchPosition.ReadValue<Vector2>()), (float)context.startTime);
        }

        private void EndTouchPrimary(InputAction.CallbackContext context)
        {
            if (OnEndTouch != null) OnEndTouch(ReferenceToMainCamera.ScreenToWorld(camera, input.Touch.PrimaryTouchPosition.ReadValue<Vector2>()), (float)context.time);
        }

        public Vector2 PrimaryPosition()
        {
            return ReferenceToMainCamera.ScreenToWorld(camera, input.Touch.PrimaryTouchPosition.ReadValue<Vector2>());
        }

        private void Update()
        {
            Debug.Log(UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches);

            foreach (UnityEngine.InputSystem.EnhancedTouch.Touch touch in Touch.activeTouches)
            {
                Debug.Log(touch.phase == UnityEngine.InputSystem.TouchPhase.Began);
            }
            
            foreach (UnityEngine.InputSystem.EnhancedTouch.Touch touch in Touch.activeTouches)
            {
                Debug.Log(touch.phase == UnityEngine.InputSystem.TouchPhase.Ended);
            }
        }
    }
}